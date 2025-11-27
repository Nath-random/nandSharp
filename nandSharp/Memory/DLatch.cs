namespace nandSharp.Memory;

using Connecters;
using LogicGates;
public class DLatch : LogicGate // State is undefined until first Store (oscillating)
{
    
    public static readonly string NAME = "DLATCH";

    public ConnectorPlug InSt = new(NAME); // St = Store
    public ConnectorPlug InD = new(NAME); // D = Data
    // public Nand Nand1 = new();
    // public Nand Nand2 = new();
    public Nand And1 = new();
    public Nand And2 = new();
    public Not Not1 = new();
    public SRLatch SR = new();
    public ConnectorPlug Out1 = new(NAME);

    public DLatch()
    {
        Cable.Connect(InSt, And1.In1);
        Cable.Connect(InSt, And2.In1);
        Cable.Connect(InD, And2.In2);
        Cable.Connect(InD, Not1.In1);
        Cable.Connect(Not1.Out1, And1.In2);
        Cable.Connect(And1.Out1, SR.InS);
        Cable.Connect(And2.Out1, SR.InR);
        Cable.Connect(SR.Out1, Out1);
        
        
        // Cable.Connect(InSt, Nand1.In1);
        // Cable.Connect(InSt, Nand2.In1);
        // Cable.Connect(InD, Nand2.In2);
        // Cable.Connect(Nand2.Out1, Nand1.In2);
        // Cable.Connect(Nand1.Out1, SR.InS);
        // Cable.Connect(Nand2.Out1, SR.InR);
        // Cable.Connect(SR.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        // NandCount = Nand1.NandCount + Nand2.NandCount + SR.NandCount;
        // NeededTicks = Nand2.NeededTicks + Nand1.NeededTicks + SR.NandCount;
    }

    public override void Compute()
    {
        // Nand1.Compute();
        // Nand2.Compute();
        And1.Compute();
        And2.Compute();
        Not1.Compute();
        SR.Compute();
    }

    public override void Tick()
    {
        And1.Tick();
        And2.Tick();
        Not1.Tick();
        // Nand1.Tick();
        // Nand2.Tick();
        SR.Tick();
    }
}