namespace nandSharp.IO;
using Connecters;
public class Air32 : LogicGate
{
    public static readonly long HIGHEST_UNSIGNED = 4294967295;
    public static readonly long HIGHEST_SIGNED = 2147483647;
    public List<InPlug> In1 = new();
    public string Name = "unnamed";
    public long lastInt;
    public long lastUnsigned;
    
    public Air32(string name) : this()
    {
        Name = name;
    }

    public override void InitStats()
    {
        NandCount = 0;
        NeededTicks = 1;
    }
    public Air32()
    {
        for (int i = 0; i < 32; i++)
        {
            In1.Add(new InPlug());
        }
        InitStats();
    }
    
    
    public override void Compute()
    {
        string text = "";
        long sum = 0;
        long value = 1;
        for (int i = 0; i < 32; i++)
        {
            text += In1[i].Voltage ? "1" : "0";
            sum += In1[i].Voltage ? value : 0;
            value *= 2;

        }

        long signed = sum;
        if (sum > HIGHEST_SIGNED)
        {
            signed = sum - HIGHEST_UNSIGNED - 1;
        }

        lastUnsigned = sum;
        lastInt = signed;
        var reversed = text.ToCharArray();
        Array.Reverse(reversed);
        text = new string(reversed);
        Console.WriteLine($"Air {Name} reports: {text}. Unsigned:{sum}. Signed:{signed}");
        
    }

    public override void Tick()
    {
        for (int i = 0; i < 32; i++)
        {
            In1[i].Voltage = In1[i].NextVoltage;
        }
    }
}