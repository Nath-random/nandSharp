namespace nandSharp.LogicGates;

public class Not : LogicGate
{
    public ConnectorPlug In1;
    public Nand Nand1;
    public Cable Cable1;
    public Cable Cable2;
    public ConnectorPlug Out1;
    public Not()
    {
        In1 = new ConnectorPlug();
        Out1 = new ConnectorPlug();
        Nand1 = new Nand();
        Cable1 = new Cable();
        Cable2 = new Cable();
        Cable1.Dests.Add(Nand1.In1);
        Cable1.Source = In1;
        In1.DestCable = Cable1;
        Nand1.In1.SourceCable = Cable1;
        Cable1.Dests.Add(Nand1.In2);
        Nand1.In2.SourceCable = Cable2;
        Nand1.Out1.DestCable = Cable2;
        Cable2.Source = Nand1.Out1;
        Cable2.Dests.Add(Out1);
        
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