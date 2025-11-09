namespace nandSharp.Gates32Bit;

using Connecters;
using LogicGates;

public class Nor32 : LogicGate
{
    
    public static readonly string NAME = "OR32";
    
    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public List<Nor> Nors = new();
    public BusConnector Out1 = new(NAME);
    
    
    public Nor32()
    {
        for (int i = 0; i < 32; i++)
        {
            Nors.Add(new Nor());
        }

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(In1[i], Nors[i].In1);
            Cable.Connect(In2[i], Nors[i].In2);
            Cable.Connect(Nors[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (Nor nor in Nors)
        {
            NandCount += nor.NandCount;
        }
        NeededTicks = Nors[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Nor nor in Nors)
        {
            nor.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Nor nor in Nors)
        {
            nor.Tick();
        }
    }
}