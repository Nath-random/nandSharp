namespace nandSharp;
using LogicGates;
class Program
{
    static void Main(string[] args)
    {
        AndTest();
    }

    public static void NotTest()
    {
        Air air = new Air();
        Not not = new Not();

        Cable cable1 = new Cable();
        not.Out1.DestCable = cable1;
        not.Out1.DestCable.AddDest(air.In1);
        not.In1.Propagate(false);
        
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

    public static void AndTest()
    {
        Air air = new();
        And and = new();
        Cable.Connect(and.Out1, air.In1);
        and.In1.Propagate(true);
        and.In2.Propagate(true);
        
        and.Compute();
        air.Compute();
        and.Tick();
        air.Tick();
        and.Compute();
        air.Compute();
        and.Tick();
        air.Tick();
        and.Compute();
        air.Compute();
        and.Tick();
        air.Tick();
        and.Compute();
        air.Compute();
        and.Tick();
        air.Tick();
        and.Compute();
        air.Compute();
        and.Tick();
        air.Tick();
        and.Compute();
        air.Compute();
        and.Tick();
        air.Tick();

    }
}