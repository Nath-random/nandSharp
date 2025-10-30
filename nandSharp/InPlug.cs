namespace nandSharp;

public class InPlug : Plug
{
    public bool Voltage = false;
    public bool NextVoltage = false;
    public Cable? SourceCable;
    public LogicGate? Gate;

    public InPlug()
    {
        // IsInputPlug = isInputPlug
    }

    public InPlug(LogicGate gate)
    {
        Gate = gate;
    }
    
    public void UpdateVoltage()
    {
        Voltage = NextVoltage;
    }

    public override void Propagate(bool voltage)
    {
        //nothing to do, since it is at end of cable chain
    }
    
}