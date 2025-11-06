using nandSharp.Connecters;

namespace nandSharp;

public class SignalProvider32 : LogicGate
{
    public static string NAME = "SIGNAL_PROVIDER32";
    public List<bool> bits = new(); // Bit at Index 0 has value 1
    public BusConnector Out1 = new(NAME);

    public SignalProvider32(long unsignedValue)
    {
        if (unsignedValue < 0 || unsignedValue > 4294967295)
        {
            throw new ArgumentException("Value beim Signal-Provider zu gross!!");
        }
        for (int i = 0; i < 32; i++)
        {
            if (unsignedValue % 2 == 1)
            {
                bits.Add(true);
            }
            else
            {
                bits.Add(false);
            }
            unsignedValue /= 2;
        }
    }
    public override void InitStats()
    {
        NandCount = 0;
    }

    public override void Compute()
    {
        for (int i = 0; i < 32; i++)
        {
            Out1[i].Propagate(bits[i]);
        }
    }

    public override void Tick()
    {
        return;
    }
}