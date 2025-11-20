using nandSharp.Connecters;
using nandSharp.LogicGates;

namespace nandSharp.Memory;

/*
 Plan wie Clock-Cycle funktionieren wird:
 1. Die Clock gibt n Ticks lange 0 aus.
 2. Dann wechselt es auf 1 und gibt n Ticks lange 1 aus.
 3. wieder auf 0 usw.
 n ist die Anzahl benötigter Ticks die garantiert, dass alle Bauteile fertig berechnet haben/stabil sind.
 In Nandsharp ist mit "Tick" die Zeit gemeint in der sich der Strom von einem Nand zum nächsten bewegt.
 Tick simuliert also die Zeit und in echt ist es anders.
 
 Clock hat 2 Phasen: 0 und 1.
 Bei 0 kann sich Store und Data beliebig ändern und output ändert sich nicht.
 Bei 1 soll sich Store und Data nicht mehr ändern und FlipFlop speichert das Bit
 Bei wechsel auf 0 gibt FlipFlop den neuen Output aus.
 
 Bevor mit Store und Clock etwas gespeichert wird, ist der Zustand undefiniert (oszilliert)
 */
public class FlipFlop : LogicGate // Precise: Data Flip-Flop
{
    public static readonly string NAME = "FLIPFLOP";

    public ConnectorPlug InSt = new(NAME); // St = Store
    public ConnectorPlug InD = new(NAME); // D = Data
    public ConnectorPlug InCl = new(NAME); // Cl = Clock
    public Nand Nand1 = new();
    public Mux Mux1 = new();
    public DLatch DL = new();
    public ConnectorPlug Out1 = new(NAME);

    public FlipFlop()
    {
        Cable.Connect(InSt, Nand1.In1);
        Cable.Connect(InCl, Nand1.In2);
        Cable.Connect(InD, Mux1.In0);
        Cable.Connect(Nand1.Out1, Mux1.InS);
        Cable.Connect(Mux1.Out1, Mux1.In1);
        Cable.Connect(Nand1.Out1, DL.InSt);
        Cable.Connect(Mux1.Out1, DL.InD);
        Cable.Connect(DL.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        NandCount = Nand1.NandCount + Mux1.NandCount + DL.NandCount;
        NeededTicks = Nand1.NeededTicks + Mux1.NeededTicks + DL.NeededTicks;
    }

    public override void Compute()
    {
        Nand1.Compute();
        Mux1.Compute();
        DL.Compute();
    }

    public override void Tick()
    {
        Nand1.Tick();
        Mux1.Tick();
        DL.Tick();
    }
}