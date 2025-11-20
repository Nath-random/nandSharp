namespace nandSharp;
using LogicGates;
using Gates32Bit;
using Connecters;
class Program
{
    static void Main(string[] args)
    {
        int byteSize = 17;
        int layers = 1; //Minimum Size = 2 Registers = 8 Bytes

        if (byteSize > 8) 
        {
            layers = (int)Math.Ceiling(Math.Log2(byteSize)) - 2; //Size is rounded up to the next power of 2.
        }

        int registerCount = 2 ^ (layers - 2);

        // registerCount = (int)Math.Pow(2, layers);
        registerCount = 1 << layers;
        Console.WriteLine(2^3);
        Mux mux = new();
        Console.WriteLine(mux.NandCount);
        Console.WriteLine(mux.NeededTicks);
    }

   





}