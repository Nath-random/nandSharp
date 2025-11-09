namespace nandSharp.Gates32Bit;

using LogicGates;
using Connecters;

public class Xor32 : LogicGate
{
    
    public static readonly string NAME = "XOR32";
    
    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public List<Xor> Xors = new();
    public BusConnector Out1 = new(NAME);
    
    
    public Xor32()
    {
        for (int i = 0; i < 32; i++)
        {
            Xors.Add(new Xor());
        }

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(In1[i], Xors[i].In1);
            Cable.Connect(In2[i], Xors[i].In2);
            Cable.Connect(Xors[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (Xor xor in Xors)
        {
            NandCount += xor.NandCount;
        }
        NeededTicks = Xors[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Xor xor in Xors)
        {
            xor.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Xor xor in Xors)
        {
            xor.Tick();
        }
    }
}