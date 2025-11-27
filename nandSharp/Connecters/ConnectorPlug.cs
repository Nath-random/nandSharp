namespace nandSharp.Connecters;

public class ConnectorPlug : Plug
{
    public Cable? DestCable;
    public String BelongsTo = "unknown";
    public bool LastVoltage = false; // Only for Debugging!!

    public ConnectorPlug(String belongsTo)
    {
        BelongsTo = belongsTo;
    }
    public override void Propagate(bool voltage)
    {
        LastVoltage = voltage;
        if (DestCable == null)
        {
            Console.WriteLine($"ConnectorPlug von {BelongsTo} hat kein Kabel angeschlossen");
            throw new InvalidOperationException($"ConnectorPlug von {BelongsTo} hat kein Kabel angeschlossen");
        }
        DestCable.Propagate(voltage);
    }
}