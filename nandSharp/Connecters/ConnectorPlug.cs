namespace nandSharp.Connecters;

public class ConnectorPlug : Plug
{
    public Cable? SourceCable;
    public Cable? DestCable;

    public ConnectorPlug()
    {
        
    }
    public override void Propagate(bool voltage)
    {
        DestCable.Propagate(voltage);
    }
}