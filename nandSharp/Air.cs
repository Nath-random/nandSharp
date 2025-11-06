using nandSharp.LogicGates;

namespace nandSharp;
using Connecters;
public class Air : LogicGate
{
    public InPlug In1;
    public string Name = "unnamed";
    public Air()
    {
        In1 = new InPlug();
    }

    public Air(string name) : this()
    {
        Name = name;
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = 0;
    }
    public override void Compute()
    {
        Console.WriteLine($"Air {Name} reports: in1: " + In1.Voltage);
    }

    public override void Tick()
    {
        In1.Voltage = In1.NextVoltage;
    }
    
}