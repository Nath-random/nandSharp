namespace nandSharp.LogicGates;

public class Xor : LogicGate
{
    public ConnectorPlug In1 = new();
    public ConnectorPlug In2 = new();
    public Not Not1 = new();
    public Not Not2 = new();
    public And And1 = new();
    public And And2 = new();
    public Or Or1 = new();
    public ConnectorPlug Out1 = new();

    public Xor()
    {
        Cable.Connect(In1, Not1.In1);
        Cable.Connect(In1, And2.In1);
        Cable.Connect(In2, And1.In2);
        Cable.Connect(In2, Not2.In1);
        Cable.Connect(Not1.Out1, And1.In1);
        Cable.Connect(Not2.Out1, And2.In2);
        Cable.Connect(And1.Out1, Or1.In1);
        Cable.Connect(And2.Out1, Or1.In2);
        Cable.Connect(Or1.Out1, Out1);
    }
    
    public override void Compute()
    {
        Not1.Compute();
        Not2.Compute();
        And1.Compute();
        And2.Compute();
        Or1.Compute();
    }

    public override void Tick()
    { 
        Not1.Tick();
        Not2.Tick();
        And1.Tick();
        And2.Tick();
        Or1.Tick();
    }
}