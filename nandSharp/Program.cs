namespace nandSharp;
using LogicGates;
using Gates32Bit;
using Connecters;
class Program
{
    static void Main(string[] args)
    {
        Add32Test();
        // NegativeNumberTest();
        //todo unittests add32
        //todo neededticks
    }

    public static void NegativeNumberTest()
    {
        SignalProvider32 number1 = new(-2147483640);
        Air32 air1 = new();

        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(number1.Out1[i], air1.In1[i]);
        }
        number1.Compute();
        // air1.Compute();
        number1.Tick();
        air1.Tick();
        // number1.Compute();
        // number2.Compute();
        // add.Compute();
        air1.Compute();
        Console.WriteLine("fertisch");
    }
    public static void Add32Test()
    {
        SignalProvider32 number1 = new(-300);
        SignalProvider32 number2 = new(-400);
        Add32 add = new();
        Air32 air1 = new();
        Air air2 = new();
        Console.WriteLine(add.NeededTicks);
        Bus32.Connect(number1.Out1, add.In1);
        Bus32.Connect(number2.Out1, add.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(add.Out1[i], air1.In1[i]);
        }
        Cable.Connect(add.OutC, air2.In1);
        for (int i = 0; i < 1000; i++)
        {
            number1.Compute();
            number2.Compute();
            add.Compute();
            // air.Compute();
            add.Tick();
            air1.Tick();
        }
        // number1.Compute();
        // number2.Compute();
        // add.Compute();
        air1.Compute();
        Console.WriteLine("fertisch");
    }





}