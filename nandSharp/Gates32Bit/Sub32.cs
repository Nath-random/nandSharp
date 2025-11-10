namespace nandSharp.Gates32Bit;

using Connecters;
using LogicGates;

// Out1 = In1 - In2 - Carry
public class Sub32 : LogicGate
{
    public static readonly string NAME = "SUB32";
    
    public BusConnector In1 = new(NAME);
    public BusConnector In2 = new(NAME);
    public ConnectorPlug InC = new(NAME); // C = Carry Bit
    public Add32 Add = new();
    public Not32 Not1 = new();
    public Not NotC1 = new();
    public Not NotC2 = new();
    public BusConnector Out1 = new(NAME);
    public ConnectorPlug OutC = new(NAME);
    public Sub32()
    {
        /* Erklärung Carry: wegen Zweierkomplement muss es vor oder nach dem Add inkrementiert werden.
        Wenn das Carry auf 1 ist, muss aber noch eins abgezogen werden.
        Wenn man das Carry negiert dann hat es den gleichen Effekt wie +1 - carry = neg(carry) 
        Also kann man das Carry einfach beim Add-Carry anschliessen.
        Das Carry, das bei Add rauskommt muss man negieren.
        */
        Bus32.Connect(In1, Add.In1);
        Bus32.Connect(In2, Not1.In1);
        Bus32.Connect(Not1.Out1, Add.In2);
        Cable.Connect(InC, NotC1.In1);
        Cable.Connect(NotC1.Out1, Add.InC);
        Bus32.Connect(Add.Out1, Out1);
        Cable.Connect(Add.OutC, NotC2.In1);
        Cable.Connect(NotC2.Out1, OutC);
        InitStats();
    }

    public override void InitStats()
    {
        NandCount = Not1.NandCount + NotC1.NandCount + Add.NandCount + NotC2.NandCount;
        NeededTicks = Math.Max(Not1.NeededTicks, NotC1.NeededTicks) + Add.NeededTicks + NotC2.NeededTicks;
    }

    public override void Compute()
    {
        Not1.Compute();
        NotC1.Compute();
        Add.Compute();
        NotC2.Compute();
    }

    public override void Tick()
    {
        Not1.Tick();
        NotC1.Tick();
        Add.Tick();
        NotC2.Tick();
    }
}