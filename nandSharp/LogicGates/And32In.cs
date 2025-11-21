using nandSharp.Connecters;

namespace nandSharp.LogicGates;

public class And32In : LogicGate
{
    
    public static readonly string NAME = "AND16In";

    public List<ConnectorPlug> Ins = new(); //Has 32 Ins
    public And16In And1 = new();
    public And16In And2 = new();
    public And And3 = new();
    public ConnectorPlug Out1 = new(NAME);
    
    public And32In()
    {
        Bus32.Connect(Ins, And1.Ins, 0, 15, 0, 15);
        Bus32.Connect(Ins, And2.Ins, 16, 31, 0, 15);
        Cable.Connect(And1.Out1, And3.In1);
        Cable.Connect(And2.Out1, And3.In2);
        Cable.Connect(And3.Out1, Out1);
        InitStats();
    }
    
    public override void InitStats()
    {
        NandCount = And1.NandCount + And2.NandCount + And3.NandCount;
        NeededTicks = And1.NeededTicks + And3.NeededTicks;
    }

    public override void Compute()
    {
        And1.Compute();
        And2.Compute();
        And3.Compute();
    }

    public override void Tick()
    {
        And1.Tick();
        And2.Tick();
        And3.Tick();
    }
}