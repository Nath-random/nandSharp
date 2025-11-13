using nandSharp.Connecters;
using nandSharp.LogicGates;

namespace nandSharp.Memory;

public class FlipFlop : LogicGate // Precise: Data Flip-Flop
{
    public static readonly string NAME = "FLIPFLOP";

    public ConnectorPlug InSt = new(NAME); // St = Store
    public ConnectorPlug InD = new(NAME); // D = Data
    public ConnectorPlug InCl = new(NAME); // Cl = Clock
    public Nand Nand1 = new();
    public Mux Mux1 = new();
    public DLatch DL = new();
    public ConnectorPlug Out1 = new(NAME);

    public FlipFlop()
    {
        Cable.Connect(InSt, Nand1.In1);
        Cable.Connect(InCl, Nand1.In2);
        Cable.Connect(InD, Mux1.In0);
        Cable.Connect(Nand1.Out1, Mux1.InS);
        Cable.Connect(Mux1.Out1, Mux1.In1);
        Cable.Connect(Nand1.Out1, DL.InSt);
        Cable.Connect(Mux1.Out1, DL.InD);
        Cable.Connect(DL.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = Nand1.NandCount + Mux1.NandCount + DL.NandCount;
        NeededTicks = Nand1.NeededTicks + Mux1.NeededTicks + DL.NeededTicks;
    }

    public override void Compute()
    {
        Nand1.Compute();
        Mux1.Compute();
        DL.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
        Mux1.Tick();
        DL.Tick();
    }
}