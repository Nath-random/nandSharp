namespace nandSharp.LogicGates;
using Connecters;
public class FullAdder : LogicGate
{
    public static readonly string NAME = "FULL_ADDER";
    
    public ConnectorPlug In1 = new(NAME);
    public ConnectorPlug In2 = new(NAME);
    public ConnectorPlug InC = new(NAME); // C = Carry bit
    public HalfAdder HalfAdd1 = new();
    public And And1 = new();
    public Or Or1 = new();
    public Xor Xor1 = new();
    public ConnectorPlug OutH = new(NAME); // H = High bit
    public ConnectorPlug OutL = new(NAME); // L = Low bit

    public FullAdder()
    {
        Cable.Connect(In1, HalfAdd1.In1);
        Cable.Connect(In2, HalfAdd1.In2);
        Cable.Connect(HalfAdd1.OutL, And1.In1);
        Cable.Connect(HalfAdd1.OutL, Xor1.In1);
        Cable.Connect(InC, And1.In2);
        Cable.Connect(InC, Xor1.In2);
        Cable.Connect(HalfAdd1.OutH, Or1.In1);
        Cable.Connect(And1.Out1, Or1.In2);
        Cable.Connect(Or1.Out1, OutH);
        Cable.Connect(Xor1.Out1, OutL);
        InitStats();
    }

    public override void InitStats()
    {
        NandCount = HalfAdd1.NandCount + And1.NandCount + Or1.NandCount + Xor1.NandCount;
        NeededTicks = Math.Max(Xor1.NeededTicks, HalfAdd1.NeededTicks + And1.NeededTicks + Or1.NeededTicks);
    }
    public override void Compute()
    {
        HalfAdd1.Compute();
        And1.Compute();
        Or1.Compute();
        Xor1.Compute();
    }

    public override void Tick()
    {
        HalfAdd1.Tick();
        And1.Tick();
        Or1.Tick();
        Xor1.Tick();
    }
}