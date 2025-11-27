using nandSharp.IO;

namespace nandSharp;
using LogicGates;
using Gates32Bit;
using Connecters;
using Memory;
class Program
{
    static void Main(string[] args)
    {
        RAM ram = new(100);
        SignalProvider32 inputData = new(2047);
        SignalProvider32 inputAddress = new(3 * 4);

        Bus32.Connect(inputData.Out1, ram.InD);
        Bus32.Connect(inputAddress.Out1, ram.InAd);
        Air32 output = new();
        
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(ram.Out1[i], output.In1[i]);
        }
        
        ram.InSt.Propagate(true);
        for (int i = 0; i < 30; i++)
        {
            inputData.Compute();
            inputAddress.Compute();
            ram.Compute();
            inputData.Tick();
            inputAddress.Tick();
            ram.Tick();
        }
        ram.InCl.Propagate(true);
        for (int i = 0; i < 30; i++)
        {
            inputData.Compute();
            inputAddress.Compute();
            ram.Compute();
            inputData.Tick();
            inputAddress.Tick();
            ram.Tick();
        }
        ram.InCl.Propagate(false);
        for (int i = 0; i < 30; i++)
        {
            inputData.Compute();
            inputAddress.Compute();
            ram.Compute();
            inputData.Tick();
            inputAddress.Tick();
            ram.Tick();
        }
        // ram.InCl.Propagate(true);
        // for (int i = 0; i < 100; i++)
        // {
        //     inputData.Compute();
        //     inputAddress.Compute();
        //     ram.Compute();
        //     inputData.Tick();
        //     inputAddress.Tick();
        //     ram.Tick();
        // }
        inputData.Compute();
        inputAddress.Compute();
        ram.Compute();
        inputData.Tick();
        inputAddress.Tick();
        ram.Tick();
        
        
        // SignalProvider32 input = new(4047);
        // And16In ands = new();
        // for (int i = 0; i < 16; i++)
        // {
        //     Cable.Connect(new Source(false).Out1, ands.Ins[i]);
        //     Cable.Connect(ands.Out1, new PinIsolation());
        // }
        // ands.Compute();
        // ands.Tick();
        
        
        
        // Bus32.Connect(input.Out1, ram.InAd);
        // Air32 output = new();

        // int byteSize = 17;
        // int layers = 1; //Minimum Size = 2 Registers = 8 Bytes
        //
        // if (byteSize > 8) 
        // {
        //     layers = (int)Math.Ceiling(Math.Log2(byteSize)) - 2; //Size is rounded up to the next power of 2.
        // }
        //
        // int registerCount = 2 ^ (layers - 2);
        //
        // // registerCount = (int)Math.Pow(2, layers);
        // registerCount = 1 << layers;
        // Console.WriteLine(2^3);
        // Mux mux = new();
        // Console.WriteLine(mux.NandCount);
        // Console.WriteLine(mux.NeededTicks);
    }

   





}