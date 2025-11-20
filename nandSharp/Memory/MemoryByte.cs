using nandSharp.Connecters;

namespace nandSharp.Memory;

public class MemoryByte : LogicGate
{
    public static readonly string NAME = "MEMORYBYTE";
    public ConnectorPlug InSt = new(NAME); // St = Store
    public ConnectorPlug InCl = new(NAME); // Cl = Clock
    public List<ConnectorPlug> InD = new();
    public List<FlipFlop> Bits = new();
    public List<ConnectorPlug> Out1 = new();


    public MemoryByte()
    {
        for (int i = 0; i < 8; i++)
        {
            InD.Add(new ConnectorPlug(NAME));
            Bits.Add(new FlipFlop());
            Out1.Add(new ConnectorPlug(NAME));
        }

        for (int i = 0; i < 8; i++)
        {
            Cable.Connect(InSt, Bits[i].InSt);
            Cable.Connect(InCl, Bits[i].InCl);
            Cable.Connect(InD[i], Bits[i].InD);
            Cable.Connect(Bits[i].Out1, Out1[i]);
        }
        InitStats();
    }
    public override void InitStats()
    {
        foreach (FlipFlop bit in Bits)
        {
            NandCount += bit.NandCount;
        }

        NeededTicks = Bits[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (FlipFlop bit in Bits)
        {
            bit.Compute();
        }
    }

    public override void Tick()
    {
        foreach (FlipFlop bit in Bits)
        {
            bit.Tick();
        }
    }
}