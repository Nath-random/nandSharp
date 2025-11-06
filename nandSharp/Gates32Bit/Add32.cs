namespace nandSharp.Gates32Bit;
using LogicGates;
using Connecters;

public class Add32 : LogicGate
{
    public BusConnector In1 = new();
    public BusConnector In2 = new();
    public ConnectorPlug InC = new(); // C = Carry-Bit
    public List<FullAdder> Adders = new();
    public BusConnector Out1 = new();
    public ConnectorPlug OutC = new(); // C = Carry-Bit

    public Add32()
    {
        for (int i = 0; i < 32; i++)
        {
            Adders[i] = new FullAdder();
        }
        Cable.Connect(In1[0], Adders[0].In1);
        Cable.Connect(In2[0], Adders[0].In2);
        Cable.Connect(InC, Adders[0].InC);
        Cable.Connect(Adders[0].OutL, Out1[0]);
        for (int i = 1; i < 32; i++)
        {
            Cable.Connect(In1[i], Adders[i].In1);
            Cable.Connect(In2[i], Adders[i].In2);
            Cable.Connect(Adders[i - 1].OutL, Adders[i].InC); // Der Übertrag vom letzten Adder wird übertragen.
        }
        Cable.Connect(Adders[31].OutL, Out1[31]);
        Cable.Connect(Adders[31].OutH, OutC);
    }
    
    public override void InitStats()
    {
        
    }
    
    public override void Compute()
    {
        foreach (FullAdder adder in Adders)
        {
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