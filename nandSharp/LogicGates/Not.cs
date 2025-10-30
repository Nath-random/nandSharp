namespace nandSharp.LogicGates;

public class Not : DerivedGate
{
    public ConnectorPlug In1;
    public Nand Nand1;
    public Cable Cable1;
    public Cable Cable2;
    public Cable Cable3;
    public Not()
    {
        In1 = new InPlug(this);
        Nand1 = new Nand();
        Cable1 = new Cable();
        Cable2 = new Cable();
        Cable3 = new Cable();
        Cable1.Dests.Add(Nand1.In1);
        Cable1.Dests.Add(Nand1.In2);
        
        Nand1.Out1 = Cable2; //geht nicht weil es kein Cable von OutPlug zu Outplug gibt
        Cable2.Input = Nand1;
        Cable2.Dests.Add(Out1);
    }

    public override void Compute()
    {
        
    }

    public override void Tick()
    {
        
    }
}