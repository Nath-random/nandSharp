namespace nandSharp.LogicGates;


public class Or : LogicGate
{
    public ConnectorPlug In1 = new();
    public ConnectorPlug In2 = new();
    public Not Not1 = new();
    public Not Not2 = new();
    public Nand Nand1 = new();
    public ConnectorPlug Out1 = new();
    public Or()
    {
        Cable.Connect(In1, Not1.In1);
        Cable.Connect(In2, Not2.In1);
        Cable.Connect(Not1.Out1, Nand1.In1);
        Cable.Connect(Not2.Out1, Nand1.In2);
        Cable.Connect(Nand1.Out1, Out1);
    }

    public override void Compute()
    {
        Not1.Compute();
        Not2.Compute();
        Nand1.Compute();
    }

    public override void Tick()
    {
        Not1.Tick();
        Not2.Tick();
        Nand1.Tick();
    }
}