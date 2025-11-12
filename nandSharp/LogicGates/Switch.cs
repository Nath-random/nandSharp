namespace nandSharp.LogicGates;

using Connecters;

public class Switch : LogicGate
{
    public static readonly string NAME = "SWITCH";
    
    public ConnectorPlug InS = new(NAME); // S = Select
    public ConnectorPlug In1 = new(NAME);
    public And And1 = new();
    public And And2 = new();
    public Not Not1 = new();
    public ConnectorPlug Out0 = new(NAME);
    public ConnectorPlug Out1 = new(NAME);

    public Switch()
    {
        Cable.Connect(InS, Not1.In1);
        Cable.Connect(Not1.Out1, And1.In1);
        Cable.Connect(In1, And1.In2);
        Cable.Connect(InS, And2.In1);
        Cable.Connect(In1, And2.In2);
        Cable.Connect(And1.Out1, Out0);
        Cable.Connect(And2.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = And1.NandCount + And2.NandCount + Not1.NandCount;
        NeededTicks = Not1.NeededTicks + And1.NeededTicks;
  
    }
    public override void Compute()
    {
        Not1.Compute();
        And1.Compute();
        And2.Compute();
    }

    public override void Tick()
    {
        Not1.Tick();
        And1.Tick();
        And2.Tick();
    }
}