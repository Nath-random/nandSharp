namespace nandSharp;

public class Air : LogicGate
{
    // public Air() : base(null, null, null) {}
    public override void Tick()
    {
        Console.WriteLine("in1: " + In1.Voltage);
        Console.WriteLine("in2: " + In2.Voltage);
    }
    
    
}