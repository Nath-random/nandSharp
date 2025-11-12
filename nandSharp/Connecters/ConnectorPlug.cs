namespace nandSharp.Connecters;

public class ConnectorPlug : Plug
{
    public Cable? DestCable;
    public string BelongsTo = "unknown";
    public bool lastVoltage = false; // Only for Debugging!!

    public ConnectorPlug(string belongsTo)
    {
        BelongsTo = belongsTo;
    }
    public override void Propagate(bool voltage)
    {
        lastVoltage = voltage;
        if (DestCable == null)
        {
            Console.WriteLine($"ConnectorPlug von {BelongsTo} hat kein Kabel angeschlossen");
        }
        DestCable.Propagate(voltage);
    }
}