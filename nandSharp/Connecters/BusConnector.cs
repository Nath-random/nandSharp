namespace nandSharp.Connecters;

public class BusConnector
{
    public List<ConnectorPlug> Pins = new(32); // Pins 0-31

    public BusConnector(string belongsTo)
    {
        for (int i = 0; i < 32; i++)
        {
            Pins.Add(new ConnectorPlug(belongsTo));
        }
    }

    public ConnectorPlug this[int pin]
    {
        get => Pins[pin];
        set => Pins[pin] = value;
    }
}