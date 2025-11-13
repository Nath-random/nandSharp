namespace nandSharp.Gates32Bit;

using Connecters;
using IO;

public class Inc32 : LogicGate // Nath Inc. = NathIncorporated
{
    public static readonly string NAME = "INC32";
    
    public BusConnector In1 = new(NAME);
    public Add32 Add = new Add32();
    public Source HighVoltage = new Source(true);
    public BusConnector Out1 = new(NAME);
    public ConnectorPlug OutC = new(NAME);
    
    public Inc32()
    {
        Bus32.Connect(In1, Add.In1);
        Cable.Connect(HighVoltage.Out1, Add.In2[0]);
        Bus32.Connect(Add.Out1, Out1);
        Cable.Connect(Add.OutC, OutC);
        InitStats();
    }
    
    
    
    public override void InitStats()
    {
        NandCount = Add.NandCount + HighVoltage.NandCount;
        NeededTicks = HighVoltage.NeededTicks + Add.NeededTicks;
    }
    
    public override void Compute()
    {
        HighVoltage.Compute();
        Add.Compute();
    }

    public override void Tick()
    {
        HighVoltage.Tick();
        Add.Tick();
    }
}