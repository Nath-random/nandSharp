namespace nandSharp;
using LogicGates;
using Gates32Bit;
using Connecters;
class Program
{
    static void Main(string[] args)
    {
        Add32Test();
    }

    public static void Add32Test()
    {
        Air32 air = new();
        Air air2 = new();
        Add32 add = new();
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(add.Out1[i], air.In1[i]);
        }
        Cable.Connect(add.OutC, air2.In1);
        add.In1[0].Propagate(false);
        
        add.In1[1].Propagate(true);
        Console.WriteLine(add.NandCount);
        for (int i = 0; i < 1000; i++)
        {
            add.Compute();
            air.Compute();
            add.Tick();
            air.Tick();
        }

        Console.WriteLine("fertig");
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


    public static void XorTest()
    {
        
        Air air = new();
        Xor xor = new();
        Cable.Connect(xor.Out1, air.In1);
        xor.In1.Propagate(false);
        xor.In2.Propagate(true);
        
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
        xor.Compute();
        air.Compute();
        xor.Tick();
        air.Tick();
    }

    public static void FullAdderTest()
    {
        Air air1 = new("High-Bit");
        Air air2 = new("Low-Bit");
        FullAdder fAdd = new();
        Cable.Connect(fAdd.OutH, air1.In1);
        Cable.Connect(fAdd.OutL, air2.In1);
        fAdd.In1.Propagate(true);
        fAdd.In2.Propagate(true);
        fAdd.InC.Propagate(false);

        for (int i = 0; i < 30; i++)
        {
            fAdd.Compute();
            air1.Compute();
            air2.Compute();
            fAdd.Tick();
            air1.Tick();
            air2.Tick();
            Console.WriteLine();
        }
    }
}