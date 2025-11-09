namespace nandSharp.Gates32Bit;

using nandSharp.Connecters;
using nandSharp.LogicGates;

public class Or32 : LogicGate
{
    public static readonly string NAME = "OR32";
    
    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public List<Or> Ors = new();
    public BusConnector Out1 = new(NAME);
    
    
    public Or32()
    {
        for (int i = 0; i < 32; i++)
        {
            Ors.Add(new Or());
        }

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(In1[i], Ors[i].In1);
            Cable.Connect(In2[i], Ors[i].In2);
            Cable.Connect(Ors[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (Or or in Ors)
        {
            NandCount += or.NandCount;
        }
        NeededTicks = Ors[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Or or in Ors)
        {
            or.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Or or in Ors)
        {
            or.Tick();
        }
    }
}