namespace nandSharp;

public abstract class Plug
{
    public abstract void ConnectTo(Plug dest);
    public abstract void Propagate(bool voltage);
}