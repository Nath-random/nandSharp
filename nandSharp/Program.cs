namespace nandSharp;
using LogicGates;
class Program
{
    static void Main(string[] args)
    {
        Air air = new Air();
        Plug p1 = new Plug();
        Plug p2 = new Plug();
        air.In1 = p1;
        air.In2 = p2;
        p1.gate = air;
        p2.gate = air;
        Nand nand = new Nand();
        Plug p3 = new Plug();
        Plug p4 = new Plug();
        Cable c1 = new Cable(p1);
        nand.In1 = p3;
        nand.In2 = p4;
        p3.gate = nand;
        p4.gate = nand;
        nand.Out1 = c1;
        c1.Input = nand;

        p3.Voltage = false;
        p4.Voltage = true;
        nand.Tick();
        air.Tick();

        // nand.Signal(true, false);
        // Console.WriteLine("Hello, World!");
    }
}