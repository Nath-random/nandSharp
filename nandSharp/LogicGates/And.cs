namespace nandSharp.LogicGates;
using static Nand;
using static Not;

public class And
{
    public ConnectorPlug In1 = new ConnectorPlug();
    public ConnectorPlug In2 = new ConnectorPlug();
    public ConnectorPlug Out1 = new ConnectorPlug();


    // public static bool GAnd(bool in1, bool in2) => GNot(GNand(in1, in2));
}