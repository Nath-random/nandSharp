namespace nandSharp.LogicGates;
using Connecters;

public class And : LogicGate
{
    public static readonly string NAME = "AND";

    public ConnectorPlug In1 = new(NAME);
    public ConnectorPlug In2 = new(NAME);
    public Nand Nand1 = new();
    public Not Not1 = new();
    public ConnectorPlug Out1 = new(NAME);
    
    public And()
    {
        Cable.Connect(In1, Nand1.In1);
        Cable.Connect(In2, Nand1.In2);
        Cable.Connect(Nand1.Out1, Not1.In1);
        Cable.Connect(Not1.Out1, Out1);
        InitStats();
    }

    public override void InitStats()
    {
        NandCount = Nand1.NandCount + Not1.NandCount;
        NeededTicks = Nand1.NeededTicks + Not1.NeededTicks;
    }

    public override void Compute()
    {
        Nand1.Compute();
        Not1.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
        Not1.Tick();
    }

}