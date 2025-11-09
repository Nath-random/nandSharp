namespace nandSharp.Gates32Bit;

using Connecters;
using LogicGates;

public class Inc32 : LogicGate // Nath Inc. = NathIncorporated
{
    public static readonly string NAME = "INC32";
    
    public BusConnector In1 = new(NAME);
    public Add32 Add = new Add32();
    public Source NoVoltage = new Source(false);
    public Not Not1 = new();
    public BusConnector Out1 = new(NAME);
    public ConnectorPlug OutC = new(NAME);
    
    public Inc32()
    {
        Bus32.Connect(In1, Add.In1);
        Cable.Connect(NoVoltage.Out1, Not1.In1);
        Cable.Connect(Not1.Out1, Add.In2[0]);
        Bus32.Connect(Add.Out1, Out1);
        Cable.Connect(Add.OutC, OutC);
        InitStats();
    }
    
    
    
    public override void InitStats()
    {
        NandCount = Add.NandCount + NoVoltage.NandCount + Not1.NandCount;
        NeededTicks = NoVoltage.NeededTicks + Not1.NeededTicks + Add.NeededTicks;
    }
    
    public override void Compute()
    {
        NoVoltage.Compute();
        Not1.Compute();
        Add.Compute();
    }

    public override void Tick()
    {
        NoVoltage.Tick();
        Not1.Tick();
        Add.Tick();
    }
}