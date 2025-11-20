using nandSharp.Connecters;
using nandSharp.LogicGates;
using nandSharp.Gates32Bit;

namespace nandSharp.Memory;

public class RAM : LogicGate
{
    public static readonly string NAME = "RAM";
    public BusConnector InAd = new(NAME); // Ad = Address
    public ConnectorPlug InSt = new(NAME); // St = Store
    public BusConnector InD = new(NAME); // D = Data
    public List<Register> Blocks = new();
    public List<And> Ands = new();
    public List<Not> Nots = new();
    public List<Mux32> Muxes = new();
    public BusConnector Out1 = new(NAME);

    public RAM(long byteSize)
    {
        /* RAM can be created in sizes that are powers of 2.
         The smallest size is with 2 Registers = 8 Bytes
         
         1 layer = 2 Registers = 8 byte
         2 layers = 4 Registers = 16 byte
         3 layers = 8 Registers = 32 byte
         4 layers = 16 Registers = 64 byte
         5 layers = 32 Registers = 128 byte
         6 layers = 64 Registers = 256 byte
         7 layers = 128 Registers = 512 byte
         8 layers = 256 Registers = 1024 byte = 1kB
         Example: byteSize = 100, then 128 bytes will be wired in 5 layers
         */
        int layers = 1; //Minimum Size = 2 Registers = 8 Bytes

        if (byteSize > 8) 
        {
            layers = (int)Math.Ceiling(Math.Log2(byteSize)) - 1; //Size is rounded up to the next power of 2.
        }
        ulong registerCount = 1ul << layers; //Calculates the needed Registers


        ulong selectCount = (1ul << layers) - 1ul;
        // ulong andCount = 
        
        Blocks.Add(new Register());
        Blocks.Add(new Register());
        Ands.Add(new And());
        Ands.Add(new And());
        Nots.Add(new Not());
        Nots.Add(new Not());
        Muxes.Add(new Mux32());





    }
    
    
    public override void InitStats()
    {
        throw new NotImplementedException();
    }

    public override void Compute()
    {
        
        throw new NotImplementedException();
    }

    public override void Tick()
    {
        throw new NotImplementedException();
    }
}