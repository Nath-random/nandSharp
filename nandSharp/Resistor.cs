using nandSharp.Connecters;

namespace nandSharp;

public class Resistor : LogicGate //Resistor is a Gate which Delays a Signal by 1 tick
{
    public static readonly string NAME = "RESISTOR";
    public InPlug In1 = new();
    public ConnectorPlug Out1 = new(NAME);

    public Resistor()
    {
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = 0;
        NeededTicks = 1;
    }

    public override void Compute()
    {
        Out1.Propagate(In1.Voltage);
    }

    public override void Tick()
    {
        In1.Voltage = In1.NextVoltage;
    }
}