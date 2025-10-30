namespace nandSharp;

public class Air : LogicGate
{
    public InPlug? In1;
    public Air()
    {
        In1 = new InPlug(this);
        
    }
    public override void Compute()
    {
        Console.WriteLine("in1: " + In1.Voltage);
    }

    public override void Tick()
    {
        return;
    }
    
}