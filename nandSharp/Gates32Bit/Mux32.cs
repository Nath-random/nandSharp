using nandSharp.Connecters;

namespace nandSharp.Gates32Bit;

using LogicGates;

public class Mux32 : LogicGate
{
    public static readonly string NAME = "MUX32";

    public ConnectorPlug InS = new(NAME); // S = Select
    public BusConnector In0 = new(NAME);
    public BusConnector In1 = new(NAME);
    public List<Mux> Muxes = new();
    public BusConnector Out1 = new(NAME);
    public Mux32()
    {
        for (int i = 0; i < 32; i++)
        {
            Muxes.Add(new Mux());
        }
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(InS, Muxes[i].InS);
            Cable.Connect(In0[i], Muxes[i].In0);
            Cable.Connect(In1[i], Muxes[i].In1);
            Cable.Connect(Muxes[i].Out1, Out1[i]);
        }
        InitStats();
    }
    public override void InitStats()
    {
        foreach (Mux mux in Muxes)
        {
            NandCount += mux.NandCount;
        }
        NeededTicks = Muxes[0].NeededTicks;
    }

    public override void Compute()
    {
        foreach (Mux mux in Muxes)
        {
            mux.Compute();
        }
    }

    public override void Tick()
    {
        foreach (Mux mux in Muxes)
        {
            mux.Tick();
        }
    }
}