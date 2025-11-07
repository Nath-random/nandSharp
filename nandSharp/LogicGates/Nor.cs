namespace nandSharp.LogicGates;
using Connecters;


public class Nor : LogicGate
{
    public static readonly string NAME = "NOR";

    public ConnectorPlug In1 = new(NAME);
    public ConnectorPlug In2 = new(NAME);
    public Or Or1 = new();
    public Not Not1 = new();
    public ConnectorPlug Out1 = new(NAME);

    public Nor()
    {
        Cable.Connect(In1, Or1.In1);
        Cable.Connect(In2, Or1.In2);
        Cable.Connect(Or1.Out1, Not1.In1);
        Cable.Connect(Not1.Out1, Out1);
        InitStats();
    }

    public override void InitStats()
    {
        NandCount = Or1.NandCount + Not1.NandCount;
        NeededTicks = Or1.NeededTicks + Not1.NeededTicks;
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