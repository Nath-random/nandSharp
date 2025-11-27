namespace nandSharp.Connecters;

public class PinIsolation : Plug //used when a Output is not connected to anything, it has to be Isolated
{
    public override void Propagate(bool voltage)
    {
        return;
    }

}