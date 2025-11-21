using nandSharp.Connecters;

namespace nandSharp.LogicGates;

public class And8In : LogicGate
{
    public static readonly string NAME = "AND8IN";

    public List<ConnectorPlug> Ins = new(); //Has 8 Ins
    public List<And> Ands = new(); //Needs 7 Ands
    public ConnectorPlug Out1 = new(NAME);
    
    public And8In()
    {
        for (int i = 0; i < 7; i++)
        {
            Ins.Add(new ConnectorPlug(NAME));
            Ands.Add(new And());
        }
        Ins.Add(new ConnectorPlug(NAME));

        Cable.Connect(Ins[0], Ands[0].In1);
        Cable.Connect(Ins[1], Ands[0].In2);
        Cable.Connect(Ins[2], Ands[1].In1);
        Cable.Connect(Ins[3], Ands[1].In2);
        Cable.Connect(Ins[4], Ands[2].In1);
        Cable.Connect(Ins[5], Ands[2].In2);
        Cable.Connect(Ins[6], Ands[3].In1);
        Cable.Connect(Ins[7], Ands[3].In2);
        
        Cable.Connect(Ands[0].Out1, Ands[4].In1);
        Cable.Connect(Ands[1].Out1, Ands[4].In2);
        Cable.Connect(Ands[2].Out1, Ands[5].In1);
        Cable.Connect(Ands[3].Out1, Ands[5].In2);

        Cable.Connect(Ands[4].Out1, Ands[6].In1);
        Cable.Connect(Ands[5].Out1, Ands[6].In2);
        Cable.Connect(Ands[6].Out1, Out1);
        InitStats();
    }


    public override void InitStats()
    {
        foreach (And and in Ands)
        {
            NandCount += and.NandCount;
        }
        NeededTicks = Ands[0].NeededTicks + Ands[4].NeededTicks + Ands[6].NeededTicks;
    }

    public override void Compute()
    {
        foreach (And and in Ands)
        {
            and.Compute();
        }
    }

    public override void Tick()
    {
        foreach (And and in Ands)
        {
            and.Tick();
        }
    }
}