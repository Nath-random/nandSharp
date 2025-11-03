namespace nandSharp;

public class InPlug : Plug
{
    public bool Voltage = false;
    public bool NextVoltage = false;

    public InPlug() { }


    // public void UpdateVoltage()
    // {
    //     Voltage = NextVoltage;
    // }

    public override void Propagate(bool voltage)
    {
        //nothing to do, since it is at end of cable chain
        NextVoltage = voltage;
    }
    
}