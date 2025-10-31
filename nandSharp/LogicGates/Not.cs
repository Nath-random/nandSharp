namespace nandSharp.LogicGates;

public class Not : LogicGate
{
    public ConnectorPlug In1 = new ConnectorPlug();
    public Nand Nand1 = new Nand();
    public ConnectorPlug Out1 = new ConnectorPlug();
    public Not()
    {
        Cable.Connect(In1, Nand1.In1);
        Cable.Connect(In1, Nand1.In2);
        Cable.Connect(Nand1.Out1, Out1);
    }

    public override void Compute()
    {
        Nand1.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
    }
}