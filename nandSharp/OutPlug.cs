namespace nandSharp;
public class OutPlug : Plug 
{
    public LogicGate? SourceGate;
    public Cable? DestCable;

    public OutPlug() { }

    public OutPlug(LogicGate gate)
    {
        SourceGate = gate;
    }

    public override void Propagate(bool voltage)
    {
        DestCable?.Propagate(voltage);
    }
}