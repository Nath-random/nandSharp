namespace nandSharp;

public abstract class LogicGate
{
    public Plug? In1;
    public Plug? In2;
    public Cable Out1;


    protected LogicGate() { }

    // protected LogicGate(Cable out1)
    // {
    //     Out1 = out1;
    // }
    // protected LogicGate(Plug in1, Plug in2, Cable out1)
    // {
    //     In1 = in1;
    //     In2 = in2;
    //     Out1 = out1;
    // }

    public void AttachPlugIn1(Plug plug)
    {
        In1 = plug;
        plug.gate = this;
    }

    public void AttachPlugIn2(Plug plug)
    {
        
    }

    // public abstract void AppendDestGate(LogicGate logicGate);
    
    public abstract void Tick();
}