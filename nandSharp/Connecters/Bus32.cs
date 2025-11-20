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
    
    public static void Connect(List<ConnectorPlug> from, List<ConnectorPlug> to, int fromFirst, int fromLast, int toFirst, int toLast)
    {
        if (fromLast > fromFirst || toLast > toFirst)
        {
            throw new ArgumentException("last is greater than first!");
        }

        if (fromLast - fromFirst != toLast - toFirst)
        {
            throw new ArgumentException("the ranges need to have the same length!");
        }

        if (from.Count < fromLast - fromFirst || fromLast > from.Count)
        {
            throw new ArgumentException("the from-List is too small!");
        }
        
        if (to.Count < toLast - toFirst || toLast > to.Count)
        {
            throw new ArgumentException("the to-List is too small!");
        }

        int offset = toFirst - fromFirst;
        for (int i = fromFirst; i <= fromLast; i++)
        {
            Cable.Connect(from[i], to[i + offset]);
        }
        
    }
    
}