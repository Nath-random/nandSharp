namespace nandSharp;
using LogicGates;
class Program
{
    static void Main(string[] args)
    {
        Air air = new Air();
        Nand nand = new Nand();
        nand.AppendDestination(air.In1);
        nand.In1.Voltage = true;
        nand.In2.Voltage = true;
        nand.Compute();
        air.Compute();
        nand.Tick();
        air.Tick();
        nand.Compute();
        air.Compute();
    }
}