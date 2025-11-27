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
    public ConnectorPlug InCl = new(NAME); // Cl = Clock
    public List<Register> Blocks = new();
    public List<And16In> And16s = new();
    public List<And> AndsForSt = new();
    public List<And> AndsForBits = new();
    public List<Not> Nots = new();
    public List<ClampPlug> Clamps = new();
    public BusConnector Out1 = new(NAME);

    public RAM(long byteSize)
    {
        BuildRAM(byteSize);
        InitStats();
    }
    
    
    
    private void BuildRAM(long byteSize)
    { 
        /* In NandSharp theoretically RAM could be created in sizes that are powers of 2.
         The smallest size would be 1 Layer with 2 Registers = 8 Bytes
         But here there are only 4 Sizes:
         8 layers = 2^8 Registers = 256 Registers = 1024 byte = 1kB
         10 layers = 2^10 Registers = 1024 Registers = 4096 byte = 4kB
         12 layers = 2^12 Registers = 4096 Registers = 16384 byte = 16kB
         14 layers = 2^14 Registers = 16384 Registers = 65536 byte = 64kB

         The layers correspond to how many bits of the address bus are used.
         Example: byteSize = 500, then 1024 bytes will be wired in 8 layers using bits 0-7 of the address bus.

         More Memory is not allowed since the simulation would be too slow.
         And the C# Datatypes only allow 2^31 - 1 Entries because their index is int (signed).
         */
        
        int layers = 8; //Minimum Size
        
        if (byteSize <= 1024)
        {
            layers = 8;
        } else if (byteSize <= 4096)
        {
            layers = 10;
        } else if (byteSize <= 16384)
        {
            layers = 12;
        } else if (byteSize <= 65536)
        {
            layers = 14;
        }
        else
        {
            throw new ArgumentOutOfRangeException("The requested number of bytes in RAM is too big!");
        }
        int registerCount = 1 << layers; //Calculates the needed Registers = needed Ands
        int muxCount = registerCount - 1;

        for (int i = 0; i < registerCount; i++)
        {
            And16s.Add(new And16In());
            AndsForSt.Add(new And());
            Blocks.Add(new Register());
            Cable.Connect(InCl, Blocks[i].InCl); //Connect all the clock-signals
            
            /*Every Register has one big And-Gate with all Adress Bits it's St input
             and a And that requires the St Signal. The big and is reused for output of the registers*/
            Cable.Connect(InSt, AndsForSt[i].In1);
            Cable.Connect(And16s[i].Out1, AndsForSt[i].In2);
            Cable.Connect(AndsForSt[i].Out1, Blocks[i].InSt);
            Bus32.Connect(InD, Blocks[i].InD); //All Registers are connected to the Data Bus
        }
        
        
        int notCounter = 0; //Keeps track how many nots there are
        for (int i = 0; i < registerCount; i++) //for every register one iteration
        {
            int addressValue = i; 
            for (int j = 0; j < layers; j++) //for every address bit one iteration
            {
                if (addressValue % 2 == 0) //Address Bit gets inverted, because it should be true if the address bit is 0
                {
                    Nots.Add(new Not());
                    notCounter++;
                    
                    //j + 2, because one register is 4 byte (last two address bits are ignored)
                    //notCounter - 1, because size is 1 bigger than last index
                    Cable.Connect(InAd[j + 2], Nots[notCounter - 1].In1);
                    Cable.Connect(Nots[notCounter - 1]. Out1, And16s[i].Ins[j]);
                }
                else
                {
                    Cable.Connect(InAd[j + 2], And16s[i].Ins[j]);

                }
                addressValue /= 2;
            }

            //all other And16 inputs have to be 1. Solution: a Not with no input is always true
            for (int r = layers; r < 16; r++) //iterations 32 - layers
            {
                Nots.Add(new Not());
                notCounter++;
                Cable.Connect(Nots[notCounter - 1].Out1, And16s[i].Ins[r]);
            }
        }
        
        
        //Connect Output of Registers
        for (int i = 0; i < 32; i++)
        {
            Clamps.Add(new ClampPlug(NAME));
            Cable.ConnectFromClamp(Clamps[i], Out1[i]);
        }
        for (int i = 0; i < registerCount; i++) //every output bit of every Register is connected to an And
        {
            for (int j = 0; j < 32; j++)
            {
                And and = new();
                Cable.Connect(Blocks[i].Out1[j], and.In1);
                Cable.Connect(And16s[i].Out1, and.In2);
                Cable.Connect(and.Out1, Clamps[j]);
                AndsForBits.Add(and);
            }
        }
        
        
        //Connect the unused Address Bits to Isolation. The idea is, that every Output needs to be connected to something for easier debugging
        Cable.Connect(InAd[0], new PinIsolation()); //the first bit are unused, because only multiples of 4 bytes can be addressed
        Cable.Connect(InAd[1], new PinIsolation());
        for (int i = layers; i < 32; i++)
        {
            Cable.Connect(InAd[i], new PinIsolation());
        }
  

    }
    
    
    public override void InitStats()
    {
        NandCount = Blocks.Count * Blocks[0].NandCount
                    + And16s.Count * And16s[0].NandCount
                    + AndsForSt.Count * AndsForSt[0].NandCount
                    + AndsForBits.Count * AndsForBits[0].NandCount
                    + Nots.Count * Nots[0].NandCount;
        NeededTicks = Nots[0].NeededTicks + And16s[0].NeededTicks + AndsForSt[0].NeededTicks
                      + Blocks[0].NeededTicks + AndsForBits[0].NandCount;
    }

    public override void Compute()
    {
        foreach (ClampPlug clamp in Clamps)
        {
            clamp.Reset(); //This needs to be at the beginning of the Compute Method
        }
        foreach (And16In and in And16s)
        {
            and.Compute();
        }

        foreach (And and in AndsForSt)
        {
            and.Compute();
        }        

        foreach (Register block in Blocks)
        {
            block.Compute();
        }
        
        foreach (And and in AndsForBits)
        {
            and.Compute();
        }

        foreach (Not not in Nots)
        {
            not.Compute();
        }
    }

    public override void Tick()
    {
        foreach (And16In and in And16s)
        {
            and.Tick();
        }

        foreach (And and in AndsForSt)
        {
            and.Tick();
        }

        foreach (Register block in Blocks)
        {
            block.Tick();
        }
        
        foreach (And and in AndsForBits)
        {
            and.Tick();
        }

        foreach (Not not in Nots)
        {
            not.Tick();
        }
    }
}