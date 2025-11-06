namespace nandSharp.Connecters;

public class ConnectorPlug : Plug
{
    public Cable? DestCable;
    public string BelongsTo = "unknown";

    public ConnectorPlug(string belongsTo)
    {
        BelongsTo = belongsTo;
    }
    public override void Propagate(bool voltage)
    {
        if (DestCable == null)
        {
            Console.WriteLine($"ConnectorPlug von {BelongsTo} hat kein Kabel angeschlossen");
        }
        DestCable.Propagate(voltage);
    }
}