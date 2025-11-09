using nandSharp.Connecters;
using nandSharp.LogicGates;

namespace nandSharp.Gates32Bit;

public class And32 : LogicGate
{
    public static readonly string NAME = "AND32";
    
    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public List<And> Ands = new();
    public BusConnector Out1 = new(NAME);
    
    
    public And32()
    {
        for (int i = 0; i < 32; i++)
        {
            Ands.Add(new And());
        }

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(In1[i], Ands[i].In1);
            Cable.Connect(In2[i], Ands[i].In2);
            Cable.Connect(Ands[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (And and in Ands)
        {
            NandCount += and.NandCount;
        }
        NeededTicks = Ands[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (And and in Ands)
        {
            and.Compute();
        }
    }

    public override void Tick()
    {
        foreach (And and in Ands)
        {
            and.Tick();
        }
    }
}