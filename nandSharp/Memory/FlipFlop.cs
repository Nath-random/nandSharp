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
    public And And1 = new();
    public And And2 = new();
    public And And3 = new();
    public Not Not1 = new();
    public DLatch DL1 = new();
    public DLatch DL2 = new();
    public ConnectorPlug Out1 = new(NAME);

    public FlipFlop()
    {
        Cable.Connect(InSt, And1.In1);
        Cable.Connect(InD, And2.In1);
        Cable.Connect(InCl, And1.In2);
        Cable.Connect(InCl, And2.In2);
        Cable.Connect(InCl, Not1.In1);
        Cable.Connect(And1.Out1, DL1.InSt);
        Cable.Connect(And2.Out1, DL1.InD);
        Cable.Connect(DL1.Out1, And3.In1);
        Cable.Connect(Not1.Out1, DL2.InSt);
        Cable.Connect(Not1.Out1, And3.In2);
        Cable.Connect(And3.Out1, DL2.InD);
        Cable.Connect(DL2.Out1, Out1);
        
        // Cable.Connect(InSt, Nand1.In1);
        // Cable.Connect(InCl, Nand1.In2);
        // Cable.Connect(InD, Mux1.In0);
        // Cable.Connect(Nand1.Out1, Mux1.InS);
        // Cable.Connect(Mux1.Out1, Mux1.In1);
        // Cable.Connect(Nand1.Out1, DL.InSt);
        // Cable.Connect(Mux1.Out1, DL.InD);
        // Cable.Connect(DL.Out1, Out1);
        InitStats();
    }
    public override void InitStats()
    {
        // NandCount = Nand1.NandCount + Mux1.NandCount + DL.NandCount;
        // NeededTicks = Nand1.NeededTicks + Mux1.NeededTicks + DL.NeededTicks;
    }

    public override void Compute()
    {
        And1.Compute();
        And2.Compute();
        And3.Compute();
        Not1.Compute();
        DL1.Compute();
        DL2.Compute();
        // Nand1.Compute();
        // Mux1.Compute();
        // DL.Compute();
    }

    public override void Tick()
    {
        And1.Tick();
        And2.Tick();
        And3.Tick();
        Not1.Tick();
        DL1.Tick();
        DL2.Tick();
        // Nand1.Tick();
        // Mux1.Tick();
        // DL.Tick();
    }
}