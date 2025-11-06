namespace nandSharp;
using Connecters;
public class Air32 : LogicGate
{
    public List<InPlug> In1 = new();
    public string Name = "unnamed";
    
    public Air32(string name) : this()
    {
        Name = name;
    }

    public override void InitStats()
    {
        NandCount = 0;
    }
    public Air32()
    {
        for (int i = 0; i < 32; i++)
        {
            In1.Add(new InPlug());
        }
    }
    
    
    public override void Compute()
    {
        string text = "";
        int sum = 0;
        int value = 1;
        for (int i = 0; i < 31; i++)
        {
            text += In1[i].Voltage ? "1" : "0";
            sum += In1[i].Voltage ? value : 0;
            value *= 2;

        }

        var reversed = text.ToCharArray();
        Array.Reverse(reversed);
        text = new string(reversed);
        Console.WriteLine($"Air {Name} reports: {text}. Unsigned:{sum}");
        
    }

    public override void Tick()
    {
        for (int i = 0; i < 32; i++)
        {
            In1[i].Voltage = In1[i].NextVoltage;
        }
    }
}