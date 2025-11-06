namespace nandSharp.Connecters;

public static class Bus32
{

    public static void Connect(BusConnector from, BusConnector to)
    {
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(from[i], to[i]);
        }
    }

    // First and Last inclusive
    public static void Connect(BusConnector from, BusConnector to, int first, int last)
    {
        if (last > first)
        {
            throw new ArgumentException("last is greater than first!");
        }
        
        for (int i = first; i <= last; i++)
        {
            Cable.Connect(from[i], to[i]);
        }
        
    }
}