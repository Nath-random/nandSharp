namespace nandSharp.Gates32Bit;

using Connecters;
using LogicGates;

public class Nand32 : LogicGate
{
    
    public static readonly string NAME = "NAND32";
    
    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public List<Nand> Nands = new();
    public BusConnector Out1 = new(NAME);
    
    
    public Nand32()
    {
        for (int i = 0; i < 32; i++)
        {
            Nands.Add(new Nand());
        }

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(In1[i], Nands[i].In1);
            Cable.Connect(In2[i], Nands[i].In2);
            Cable.Connect(Nands[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (Nand nand in Nands)
        {
            NandCount += nand.NandCount;
        }
        NeededTicks = Nands[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Nand nand in Nands)
        {
            nand.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Nand nand in Nands)
        {
            nand.Tick();
        }
    }
}