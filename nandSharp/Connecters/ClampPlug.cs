namespace nandSharp.Connecters;

/*Is a realistic Plug, it can have multiple Inputs and if one Propagates true, then it keeps true
 For all other Plugs it is not allowed to connect more than one Cable to an Input.
 */
public class ClampPlug : Plug 
{
    public Cable? DestCable;
    public String BelongsTo = "unkown";
    public bool Voltage = false;

    public ClampPlug(String belongTo)
    {
        BelongsTo = belongTo;
    }
    public override void Propagate(bool voltage)
    {
        Voltage = Voltage || voltage;
        if (DestCable == null)
        {
            Console.WriteLine($"ConnectorPlug von {BelongsTo} hat kein Kabel angeschlossen");
            throw new InvalidOperationException($"ConnectorPlug von {BelongsTo} hat kein Kabel angeschlossen");
        }
        DestCable.Propagate(Voltage);
    }

    public void Reset()
    {
        Voltage = false;
    }
}