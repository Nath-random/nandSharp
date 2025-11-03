namespace nandSharp;

public class Cable
{
    public List<Plug> Dests;

    public Cable()
    {
        Dests = new List<Plug>();
    }
    
    public static void Connect(ConnectorPlug from, Plug to)
    {
        from.DestCable ??= new Cable();
        from.DestCable.AddDest(to);
    }
    public void AddDest(Plug dest)
    {
        Dests.Add(dest);
    }
    public void Propagate(bool voltage)
    {
        foreach (Plug dest in Dests)
        {
            // dest.Propagate(voltage);
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