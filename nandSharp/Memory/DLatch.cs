namespace nandSharp.Memory;

using Connecters;
using LogicGates;
public class DLatch : LogicGate
{
    
    public static readonly string NAME = "DLATCH";

    public ConnectorPlug InSt = new(NAME); // St = Store
    public ConnectorPlug InD = new(NAME); // D = Data
    public Nand Nand1 = new();
    public Nand Nand2 = new();
    public SRLatch SR = new();
    public ConnectorPlug Out1 = new(NAME);

    public DLatch()
    {
        Cable.Connect(InSt, Nand1.In1);
        Cable.Connect(InSt, Nand1.In2);
        Cable.Connect(InD, Nand2.In2);
        Cable.Connect(Nand2.Out1, Nand1.In2);
        Cable.Connect(Nand1.Out1, SR.InS);
        Cable.Connect(Nand2.Out1, SR.InR);
        Cable.Connect(SR.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = Nand1.NandCount + Nand2.NandCount + SR.NandCount;
        NeededTicks = Nand2.NeededTicks + Nand1.NeededTicks + SR.NandCount;
    }

    public override void Compute()
    {
        Nand1.Compute();
        Nand2.Compute();
        SR.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
        Nand2.Tick();
        SR.Tick();
    }
}