namespace nandSharp;

public class Cable
{
    public LogicGate Input;
    public List<Plug> Dests;


    public Cable(List<Plug> dests)
    {
        Dests = dests;
    }

    public Cable(Plug dest)
    {
        Dests = new List<Plug>{dest};
    }

    public Cable()
    {
        Dests = new List<Plug>();
    }
    public void Propagate(bool voltage)
    {
        foreach (Plug dest in Dests)
        {
            dest.Voltage = voltage;
        }
        
    }
}