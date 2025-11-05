namespace nandSharp.LogicGates;

public class HalfAdder : LogicGate
{
    public ConnectorPlug In1 = new();
    public ConnectorPlug In2 = new();
    public And And1 = new();
    public Xor Xor1 = new();
    public ConnectorPlug OutH = new(); // H = High bit
    public ConnectorPlug OutL = new(); // L = Low bit

    public HalfAdder()
    {
        Cable.Connect(In1, And1.In1);
        Cable.Connect(In2, And1.In2);
        Cable.Connect(In1, Xor1.In1);
        Cable.Connect(In2, Xor1.In2);
        Cable.Connect(And1.Out1, OutH);
        Cable.Connect(Xor1.Out1, OutL);
    }
    
    public override void Compute()
    {
        And1.Compute();
        Xor1.Compute();
    }

    public override void Tick()
    {
        And1.Tick();
        Xor1.Tick();
    }
}