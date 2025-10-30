namespace nandSharp.LogicGates;

public class Nand : LogicGate
{
    public Nand() : base() { }
    // public Nand(Plug out1) : base(out1) {}


    // public override void AppendDestGate(LogicGate gate)
    // {
    //     
    // }
    public override void Tick()
    {
        bool result = !(In1.Voltage && In2.Voltage); // This line is the core of the nandSharp Computer
        Out1.Propagate(result);
    }
}