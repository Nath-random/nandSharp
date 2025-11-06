namespace nandSharp.Gates32Bit;
using LogicGates;
using Connecters;

public class Add32 : LogicGate
{
    public static readonly string NAME = "ADD32";

    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public ConnectorPlug InC = new(NAME); // C = Carry-Bit
    public List<FullAdder> Adders = new();
    public BusConnector Out1 = new(NAME);
    public ConnectorPlug OutC = new(NAME); // C = Carry-Bit

    public Add32()
    {
        for (int i = 0; i < 32; i++)
        {
            Adders.Add(new FullAdder());
        }
        Cable.Connect(In1[0], Adders[0].In1);
        Cable.Connect(In2[0], Adders[0].In2);
        Cable.Connect(InC, Adders[0].InC);
        for (int i = 1; i < 32; i++)
        {
            Cable.Connect(In1[i], Adders[i].In1);
            Cable.Connect(In2[i], Adders[i].In2);
            Cable.Connect(Adders[i - 1].OutH, Adders[i].InC); // Der Übertrag vom letzten Adder wird übertragen.
            Cable.Connect(Adders[i - 1].OutL, Out1[i - 1]);
        }
        Cable.Connect(Adders[31].OutL, Out1[31]);
        Cable.Connect(Adders[31].OutH, OutC);
        InitStats();
        
    }
    
    public override void InitStats()
    {
        foreach (FullAdder fAdder in Adders)
        {
            NandCount += fAdder.NandCount;
        }
    }
    
    public override void Compute()
    {
        int i = 0;
        foreach (FullAdder adder in Adders)
        {
            i++;
            adder.Compute();
        }
    }

    public override void Tick()
    {
        foreach (FullAdder adder in Adders)
        {
            adder.Tick();
        }
    }
}