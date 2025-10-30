namespace nandSharp.LogicGates;

public class Nand : LogicGate
{
    public InPlug In1;
    public InPlug In2;
    public Nand() : base()
    {
        In1 = new InPlug(this);
        In2 = new InPlug(this);
    }

    public override void AppendDestination(InPlug plug)
    {
        
    }


    public override void Compute()
    {
        bool result = !(In1.Voltage && In2.Voltage); // This line is the core of the nandSharp Computer
        Out1.DestCable?.Propagate(result);
    }

    public override void Tick()
    {
        In1.Voltage = In1.NextVoltage;
        In2.Voltage = In2.NextVoltage;
    }
}