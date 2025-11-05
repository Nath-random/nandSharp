namespace nandSharp.LogicGates;

public class And : LogicGate
{
    public ConnectorPlug In1 = new();
    public ConnectorPlug In2 = new();
    public Nand Nand1 = new();
    public Not Not1 = new();
    public ConnectorPlug Out1 = new();

    public And()
    {
        Cable.Connect(In1, Nand1.In1);
        Cable.Connect(In2, Nand1.In2);
        Cable.Connect(Nand1.Out1, Not1.In1);
        Cable.Connect(Not1.Out1, Out1);
    }

    public override void Compute()
    {
        Nand1.Compute();
        Not1.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
        Not1.Tick();
    }

}