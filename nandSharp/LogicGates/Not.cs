namespace nandSharp.LogicGates;
using Connecters;

public class Not : LogicGate
{
    public static readonly string NAME = "NAND";

    public ConnectorPlug In1 = new (NAME);
    public Nand Nand1 = new ();
    public ConnectorPlug Out1 = new (NAME);
    public Not()
    {
        Cable.Connect(In1, Nand1.In1);
        Cable.Connect(In1, Nand1.In2);
        Cable.Connect(Nand1.Out1, Out1);
        InitStats();
    }

    public override void InitStats()
    {
        NandCount = Nand1.NandCount;
    }
    public override void Compute()
    {
        Nand1.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
    }
}