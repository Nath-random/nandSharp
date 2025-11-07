namespace nandSharp.LogicGates;
using Connecters;


public class Or : LogicGate
{
    public static readonly string NAME = "OR";
    
    public ConnectorPlug In1 = new(NAME);
    public ConnectorPlug In2 = new(NAME);
    public Not Not1 = new();
    public Not Not2 = new();
    public Nand Nand1 = new();
    public ConnectorPlug Out1 = new(NAME);
    public Or()
    {
        Cable.Connect(In1, Not1.In1);
        Cable.Connect(In2, Not2.In1);
        Cable.Connect(Not1.Out1, Nand1.In1);
        Cable.Connect(Not2.Out1, Nand1.In2);
        Cable.Connect(Nand1.Out1, Out1);
        InitStats();
    }
    
    public override void InitStats()
    {
        NandCount = Not1.NandCount + Not2.NandCount + Nand1.NandCount;
        NeededTicks = Not1.NeededTicks + Nand1.NeededTicks;
    }

    public override void Compute()
    {
        Not1.Compute();
        Not2.Compute();
        Nand1.Compute();
    }

    public override void Tick()
    {
        Not1.Tick();
        Not2.Tick();
        Nand1.Tick();
    }
}