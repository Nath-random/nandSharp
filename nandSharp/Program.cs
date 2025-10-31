namespace nandSharp;
using LogicGates;
class Program
{
    static void Main(string[] args)
    {
        Air air = new Air();
        Not not = new Not();

        Cable cable1 = new Cable();
        not.Out1.DestCable = cable1;
        not.Out1.DestCable.AddDest(air.In1);
        not.In1.Propagate(true);
        
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
        not.Compute();
        air.Compute();
        not.Tick();
        air.Tick();
    }
}