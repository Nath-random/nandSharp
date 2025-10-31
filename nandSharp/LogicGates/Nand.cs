namespace nandSharp.LogicGates;

public class Nand : LogicGate
{
    public InPlug In1;
    public InPlug In2;
    public OutPlug Out1;
    public Nand() : base()
    {
        In1 = new InPlug(this);
        In2 = new InPlug(this);
        Out1 = new OutPlug(this);
    }
    


    public override void Compute()
    {
        bool result = !(In1.Voltage && In2.Voltage); // This line is the core of the nandSharp Computer
        Out1.DestCable?.Propagate(result);
        // Console.WriteLine(In1.Voltage + " 2: " + In2.Voltage);
    }

    public override void Tick()
    {
        In1.Voltage = In1.NextVoltage;
        In2.Voltage = In2.NextVoltage;
        // In1.NextVoltage = false;
        // In2.NextVoltage = false;
        //if those two lines are on: "endpoint" inputs have to be set every tick if you want to send constant true
        //on the other hand, it is more realistic if the voltage falls (because nothing is connected) that the NextVoltage is false
    }
}