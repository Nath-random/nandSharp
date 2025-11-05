namespace nandSharp.LogicGates;


public class Nor : LogicGate
{
    public ConnectorPlug In1 = new();
    public ConnectorPlug In2 = new();
    public Or Or1 = new();
    public Not Not1 = new();
    public ConnectorPlug Out1 = new();

    public Nor()
    {
        Cable.Connect(In1, Or1.In1);
        Cable.Connect(In2, Or1.In2);
        Cable.Connect(Or1.Out1, Not1.In1);
        Cable.Connect(Not1.Out1, Out1);
    }
    
    public override void Compute()
    {
        Or1.Compute();
        Not1.Compute();
    }

    public override void Tick()
    {
        Or1.Tick();
        Not1.Tick();
    }
}