using nandSharp.Connecters;

namespace nandSharp.LogicGates;

//if InS Outputs In0, else In1
public class Mux : LogicGate // Same as Selector
{
    public static readonly string NAME = "MUX";
    
    public ConnectorPlug InS = new(NAME); // S = Select
    public ConnectorPlug In0 = new(NAME);
    public ConnectorPlug In1 = new(NAME);
    public And And1 = new();
    public And And2 = new();
    public Not Not1 = new();
    public Or Or1 = new();
    public ConnectorPlug Out1 = new(NAME);

    public Mux()
    {
        Cable.Connect(InS, And1.In1);
        Cable.Connect(In1, And1.In2);
        Cable.Connect(InS, Not1.In1);
        Cable.Connect(Not1.Out1, And2.In1);
        Cable.Connect(In0, And2.In2);
        Cable.Connect(And1.Out1, Or1.In1);
        Cable.Connect(And2.Out1, Or1.In2);
        Cable.Connect(Or1.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = Not1.NandCount + And1.NandCount + And2.NandCount + Or1.NandCount;
        NeededTicks = Not1.NeededTicks + And2.NeededTicks + Or1.NeededTicks;
    }
    public override void Compute()
    {
        Not1.Compute();
        And1.Compute();
        And2.Compute();
        Or1.Compute();
    }

    public override void Tick()
    {
        Not1.Tick();
        And1.Tick();
        And2.Tick();
        Or1.Tick();
    }
}