using nandSharp.Connecters;

namespace nandSharp.Memory;

using LogicGates;
public class SRLatch : LogicGate
{
    public static readonly string NAME = "SRLATCH";

    public ConnectorPlug InS = new(NAME); // S = Set
    public ConnectorPlug InR = new(NAME); // R = Reset
    public Nand Nand1 = new();
    public Nand Nand2 = new();
    public Or Nand3 = new();
    public Not Not1 = new(); //todo
    // public Not Not2 = new();
    public ConnectorPlug Out1 = new(NAME);

    public SRLatch()
    {
        // Cable.Connect(InS, Nand1.In1);
        Cable.Connect(InS, Nand3.In1);
        Cable.Connect(InR, Not1.In1);
        Cable.Connect(Not1.Out1, Nand3.In2);
        Cable.Connect(Nand3.Out1, Nand2.In2);

        
        // Cable.Connect(InS, Not1.In1);
        // Cable.Connect(Not1.Out1, Not2.In1);
        // Cable.Connect(Not2.Out1, Nand1.In1);
        // Cable.Connect(InR, Nand2.In2);
        Cable.Connect(Nand1.Out1, Nand2.In1);
        Cable.Connect(Nand2.Out1, Nand1.In2);
        Cable.Connect(Nand2.Out1, Out1);
        InitStats();
        // PreventOscilating();

    }

    public void PreventOscilating() //this prevents the output from oscilating in the default state
    {
        InS.Propagate(false);
        Tick();
    }
    public override void InitStats()
    {
        NandCount = Nand1.NandCount + Nand2.NandCount;
        NeededTicks = Nand1.NandCount; // Falls Memory nicht funktioniert, doch auf 2 stellen...
    }

    public override void Compute()
    {
        Nand1.Compute();
        Nand2.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
        Nand2.Tick();
    }
}