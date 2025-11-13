using nandSharp.Connecters;

namespace nandSharp;

public class SignalProvider32 : LogicGate
{
    public static readonly string NAME = "SIGNAL_PROVIDER32";
    public static readonly long HIGHEST_UNSIGNED = 4294967295;
    public static readonly long HIGHEST_SIGNED = 2147483647;
    public static readonly long LOWEST_SIGNED = -2147483640;
    public List<bool> Bits = new(); // Bit at Index 0 has value 1
    public BusConnector Out1 = new(NAME);

    public SignalProvider32(long value, bool signed = true)
    {
        for (int i = 0; i < 32; i++)
        {
            Bits.Add(false);
        } 
        SetBits(value, signed);
        InitStats();
    }
    
    public override void InitStats()
    {
        NandCount = 0;
        NeededTicks = 1;
    }

    public void SetBits(long value, bool signed = true)
    {
        if (signed) // Interpret as signed number
        {
            if (value > HIGHEST_SIGNED || value < LOWEST_SIGNED)
            {
                throw new ArgumentOutOfRangeException("Illegaler Value für signed beim Signal-Provider!!");
            }
            bool negative = value < 0;
            if (negative)
            {
                value += 2 * HIGHEST_SIGNED + 2;
            }
            for (int i = 0; i < 32; i++)
            {
                Bits[i] = (value % 2 == 1);
                value /= 2;
            }
        }
        else // Interpret as unsigned number
        {
            if (value < 0 || value > HIGHEST_UNSIGNED)
            {
                throw new ArgumentOutOfRangeException("Illegaler Value für unsigned beim Signal-Provider!!");
            }
            for (int i = 0; i < 32; i++)
            {
                Bits[i] = value % 2 == 1;
                value /= 2;
            }
        }
    }

    public override void Compute()
    {
        for (int i = 0; i < 32; i++)
        {
            Out1[i].Propagate(Bits[i]);
        }
    }

    public override void Tick()
    {
        return;
    }
}