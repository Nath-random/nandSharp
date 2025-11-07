namespace nandSharp.Connecters;

public class Source : LogicGate
{
    public static readonly string NAME = "SOURCE";
    public bool Voltage;
    public ConnectorPlug Out1 = new(NAME);
    public Source(bool voltage)
    {
        Voltage = voltage;
    }
    
    
    public override void InitStats()
    {
        NandCount = 0;
    }
    
    public override void Compute()
    {
        Out1.Propagate(Voltage);
    }

    public override void Tick()
    {
    }
}