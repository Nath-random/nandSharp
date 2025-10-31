namespace nandSharp;

public class Cable
{
    public bool FastCable = false; //if true: cable goes from Output to Output and changes propagate instantly
    public Plug? Source;
    public List<Plug> Dests;


    // public Cable(List<Plug> dests)
    // {
    //     Dests = dests;
    // }
//todo only default ctor
    // public Cable(Plug dest)
    // {
    //     Dests = new List<Plug>{dest};
    // }

    public Cable()
    {
        Dests = new List<Plug>();
    }

    public void AddDest(Plug dest)
    {
        if (Dests == null)
        {
            Dests = new List<Plug> { dest };
        }
        else
        {
            Dests.Add(dest);
        }
    }
    public void Propagate(bool voltage)
    {
        foreach (Plug dest in Dests)
        {
            if (dest is InPlug inPlug)
            {
                inPlug.NextVoltage = voltage;
            }
            else
            {
                dest.Propagate(voltage);
            }
        }
        
    }
}