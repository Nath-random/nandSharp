namespace nandSharp;
using LogicGates;
using Gates32Bit;
using Connecters;
class Program
{
    static void Main(string[] args)
    {
        SignalProvider32 numbers = new SignalProvider32(4294967292);
        Add32Test();
    }

    public static void Add32Test()
    {
        SignalProvider32 number1 = new(100);
        SignalProvider32 number2 = new(200);
        Add32 add = new();
        Air32 air1 = new();
        Air air2 = new();
        
        Bus32.Connect(number1.Out1, add.In1);
        Bus32.Connect(number2.Out1, add.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(add.Out1[i], air1.In1[i]);
        }
        Cable.Connect(add.OutC, air2.In1);
        for (int i = 0; i < 100; i++)
        {
            number1.Compute();
            number2.Compute();
            add.Compute();
            // air.Compute();
            add.Tick();
            air1.Tick();
        }
        air1.Compute();
        Console.WriteLine("fertig");
    }





}