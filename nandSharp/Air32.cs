namespace nandSharp;
using Connecters;
public class Air32 : LogicGate
{
    public List<InPlug> In1 = new();
    public string Name;
    
    public Air32(string name) : this()
    {
        Name = name;
    }

    public override void InitStats()
    {
        
    }
    public Air32()
    {
        for (int i = 0; i < 32; i++)
        {
            In1[i] = new InPlug();
        }
    }
    
    
    
    
    
    public override void Compute()
    {
        string text = $"Air {Name} reports: ";
        for (int i = 0; i < 32; i++)
        {
            text += In1[i].Voltage ? "1" : "0";
        }

        Console.WriteLine(text);
    }

    public override void Tick()
    {
        for (int i = 0; i < 32; i++)
        {
            In1[i].Voltage = In1[i].NextVoltage;
        }
    }
}