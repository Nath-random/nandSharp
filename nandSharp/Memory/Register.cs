using nandSharp.Connecters;

namespace nandSharp.Memory;

//A Register consists of 4 Bytes = 32bit.
//This simplifies the data fetch for 32bit Registers
public class Register : LogicGate 
{
    public static readonly string NAME = "REGISTER";

    public ConnectorPlug InSt = new(NAME);
    public BusConnector InD = new(NAME);
    public ConnectorPlug InCl = new(NAME);
    public MemoryByte Byte0 = new();
    public MemoryByte Byte1 = new();
    public MemoryByte Byte2 = new();
    public MemoryByte Byte3 = new();
    public BusConnector Out1 = new(NAME);
    
    public Register()
    {
        Cable.Connect(InSt, Byte0.InSt);
        Cable.Connect(InSt, Byte1.InSt);
        Cable.Connect(InSt, Byte2.InSt);
        Cable.Connect(InSt, Byte3.InSt);
        Cable.Connect(InCl, Byte0.InCl);
        Cable.Connect(InCl, Byte1.InCl);
        Cable.Connect(InCl, Byte2.InCl);
        Cable.Connect(InCl, Byte3.InCl);
        Bus32.Connect(InD.Pins, Byte0.InD, 0, 7, 0, 7);
        Bus32.Connect(InD.Pins, Byte1.InD, 8, 15, 0, 7);
        Bus32.Connect(InD.Pins, Byte2.InD, 16, 23, 0, 7);
        Bus32.Connect(InD.Pins, Byte3.InD, 24, 31, 0, 7);

        Bus32.Connect(Byte0.Out1, Out1.Pins, 0, 7, 0, 7);
        Bus32.Connect(Byte1.Out1, Out1.Pins, 0, 7, 8, 15);
        Bus32.Connect(Byte2.Out1, Out1.Pins, 0, 7, 16, 23);
        Bus32.Connect(Byte3.Out1, Out1.Pins, 0, 7, 24, 31);
        InitStats();
    }
    
    
    public override void InitStats()
    {
        NandCount = Byte0.NandCount + Byte1.NandCount + Byte2.NandCount + Byte3.NandCount;
        NeededTicks = Byte0.NeededTicks;
    }

    public override void Compute()
    {
        Byte0.Compute();
        Byte1.Compute();
        Byte2.Compute();
        Byte3.Compute();
    }

    public override void Tick()
    {
        Byte0.Tick();
        Byte1.Tick();
        Byte2.Tick();
        Byte3.Tick();
    }
}