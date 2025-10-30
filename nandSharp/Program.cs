namespace nandSharp;
using LogicGates;
class Program
{
    static void Main(string[] args)
    {
        Air air = new Air();
        Nand nand = new Nand();
        Not not = new Not();
        // nand.AppendDestination(air.In1);
        not.In1.Propagate(false);
        // not.In1.Voltage = true;
        // nand.In2.Voltage = true;
        Cable cable1 = new Cable();
        not.Out1.DestCable = cable1;
        not.Out1.DestCable.AddDest(air.In1);
        
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
    }
}