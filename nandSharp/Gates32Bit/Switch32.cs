using nandSharp.LogicGates;
using nandSharp.Connecters;

namespace nandSharp.Gates32Bit;

public class Switch32 : LogicGate
{
    public static readonly string NAME = "SWITCH32";

    public ConnectorPlug InS = new(NAME); // S = Select
    public BusConnector In1 = new(NAME);
    public List<Switch> Switches = new();
    public BusConnector Out0 = new(NAME);
    public BusConnector Out1 = new(NAME);

    public Switch32()
    {
        for (int i = 0; i < 32; i++)
        {
            Switches.Add(new Switch());
        }
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(InS, Switches[i].InS);
            Cable.Connect(In1[i], Switches[i].In1);
            Cable.Connect(Switches[i].Out0, Out0[i]);
            Cable.Connect(Switches[i].Out1, Out1[i]);
        }
        InitStats();
    }

    public override void InitStats()
    {
        foreach (Switch swi in Switches)
        {
            NandCount += swi.NandCount;
        }

        NeededTicks = Switches[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Switch swi in Switches)
        {
            swi.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Switch swi in Switches)
        {
            swi.Tick();
        }
    }
}