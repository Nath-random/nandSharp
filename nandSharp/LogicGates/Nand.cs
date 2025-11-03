namespace nandSharp.LogicGates;

public class Nand : LogicGate
{
    public InPlug In1 = new ();
    public InPlug In2 = new ();
    public ConnectorPlug Out1 = new (); 
    public Nand() { }
    
    public override void Compute()
    {
        bool result = !(In1.Voltage && In2.Voltage); // This line is the core of the nandSharp Computer
        Out1.DestCable.Propagate(result);
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