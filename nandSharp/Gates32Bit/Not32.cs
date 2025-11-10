namespace nandSharp.Gates32Bit;

using Connecters;
using LogicGates;

public class Not32 : LogicGate
{
    
    public static readonly string NAME = "NOT32";
    
    public BusConnector In1 = new(NAME);
    public List<Not> Nots = new();
    public BusConnector Out1 = new(NAME);
    
    
    public Not32()
    {
        for (int i = 0; i < 32; i++)
        {
            Nots.Add(new Not());
        }

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(In1[i], Nots[i].In1);
            Cable.Connect(Nots[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (Not not in Nots)
        {
            NandCount += not.NandCount;
        }
        NeededTicks = Nots[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Not not in Nots)
        {
            not.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Not not in Nots)
        {
            not.Tick();
        }
    }
}