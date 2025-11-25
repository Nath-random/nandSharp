using nandSharp.Connecters;

namespace nandSharp.LogicGates;

public class And16In : LogicGate
{
    public static readonly string NAME = "AND16IN";

    public List<ConnectorPlug> Ins = new(); //Has 16 Ins
    public And8In And1 = new();
    public And8In And2 = new();
    public And And3 = new();
    public ConnectorPlug Out1 = new(NAME);
    
    public And16In()
    {
        for (int i = 0; i < 16; i++)
        {
            Ins.Add(new ConnectorPlug(NAME));
        }
        Bus32.Connect(Ins, And1.Ins, 0, 7, 0, 7);
        Bus32.Connect(Ins, And2.Ins, 8, 15, 0, 7);
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