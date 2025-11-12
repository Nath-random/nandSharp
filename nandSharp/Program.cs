namespace nandSharp;
using LogicGates;
using Gates32Bit;
using Connecters;
class Program
{
    static void Main(string[] args)
    {
        Mux mux = new();
        Console.WriteLine(mux.NandCount);
        Console.WriteLine(mux.NeededTicks);
    }

   





}