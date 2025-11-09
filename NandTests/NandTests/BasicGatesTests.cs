
namespace NandTests;

using nandSharp.Connecters;
using nandSharp.LogicGates;
using nandSharp.Gates32Bit;
using nandSharp;

//Debugging tipps:
//1. Wie viele Ticks macht es wirklich?
//2. Wenn man es mit z.B. 10000 Ticks macht, ist das Resultat anders?
//3. Sind alle Gates richtig verbunden?
//4. Sind alle Gates in der gates Liste?
public class BasicGatesTests
{
    static int TICKS = 100;
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void NandTest()
    {
        Air air = new();
        Nand nand = new();
        List<LogicGate> gates = new() { air, nand };
        Cable.Connect(nand.Out1, air.In1);
        nand.In1.Propagate(false);
        nand.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        nand.In1.Propagate(false);
        nand.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        nand.In1.Propagate(true);
        nand.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        nand.In1.Propagate(true);
        nand.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
    }
    
    [Test]
    public void AndTest()
    {
        Air air = new();
        And and = new();
        List<LogicGate> gates = new() { air, and };
        Cable.Connect(and.Out1, air.In1);
        and.In1.Propagate(false);
        and.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        and.In1.Propagate(false);
        and.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        and.In1.Propagate(true);
        and.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        and.In1.Propagate(true);
        and.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
    }
    
    [Test]
    public void OrTest()
    {
        Air air = new();
        Or or = new();
        List<LogicGate> gates = new() { air, or };
        Cable.Connect(or.Out1, air.In1);
        or.In1.Propagate(false);
        or.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        or.In1.Propagate(false);
        or.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        or.In1.Propagate(true);
        or.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        or.In1.Propagate(true);
        or.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
    }
    
    [Test]
    public void NorTest()
    {
        Air air = new();
        Nor nor = new();
        List<LogicGate> gates = new() { air, nor };
        Cable.Connect(nor.Out1, air.In1);
        nor.In1.Propagate(false);
        nor.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        nor.In1.Propagate(false);
        nor.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        nor.In1.Propagate(true);
        nor.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        nor.In1.Propagate(true);
        nor.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
    }
    
    [Test]
    public void XorTest()
    {
        Air air = new();
        Xor xor = new();
        List<LogicGate> gates = new() { air, xor };
        Cable.Connect(xor.Out1, air.In1);
        xor.In1.Propagate(false);
        xor.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
        xor.In1.Propagate(false);
        xor.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        xor.In1.Propagate(true);
        xor.In2.Propagate(false);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(true));
        xor.In1.Propagate(true);
        xor.In2.Propagate(true);
        Simulate(gates);
        Assert.That(air.In1.Voltage, Is.EqualTo(false));
    }
    
    [Test]
    public void HalfAdderTest()
    {
        Air airL = new();
        Air airH = new();
        HalfAdder halfAdder = new();
        List<LogicGate> gates = new() { airL, airH, halfAdder };
        Cable.Connect(halfAdder.OutL, airL.In1);
        Cable.Connect(halfAdder.OutH, airH.In1);
        halfAdder.In1.Propagate(false);
        halfAdder.In2.Propagate(false);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(false));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
        halfAdder.In1.Propagate(false);
        halfAdder.In2.Propagate(true);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(true));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
        halfAdder.In1.Propagate(true);
        halfAdder.In2.Propagate(false);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(true));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
        halfAdder.In1.Propagate(true);
        halfAdder.In2.Propagate(true);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(false));
        Assert.That(airH.In1.Voltage, Is.EqualTo(true));
    }
    
    [Test]
    public void FullAdderTest()
    {
        Air airL = new();
        Air airH = new();
        FullAdder fullAdder = new();
        List<LogicGate> gates = new() { airL, airH, fullAdder };
        Cable.Connect(fullAdder.OutL, airL.In1);
        Cable.Connect(fullAdder.OutH, airH.In1);
        fullAdder.In1.Propagate(false);
        fullAdder.In2.Propagate(false);
        fullAdder.InC.Propagate(false);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(false));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
        
        fullAdder.In1.Propagate(false);
        fullAdder.In2.Propagate(false);
        fullAdder.InC.Propagate(true);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(true));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
        
        fullAdder.In1.Propagate(false);
        fullAdder.In2.Propagate(true);
        fullAdder.InC.Propagate(false);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(true));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
      
        fullAdder.In1.Propagate(false);
        fullAdder.In2.Propagate(true);
        fullAdder.InC.Propagate(true);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(false));
        Assert.That(airH.In1.Voltage, Is.EqualTo(true));

        fullAdder.In1.Propagate(true);
        fullAdder.In2.Propagate(false);
        fullAdder.InC.Propagate(false);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(true));
        Assert.That(airH.In1.Voltage, Is.EqualTo(false));
        
        fullAdder.In1.Propagate(true);
        fullAdder.In2.Propagate(false);
        fullAdder.InC.Propagate(true);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(false));
        Assert.That(airH.In1.Voltage, Is.EqualTo(true));
        
        fullAdder.In1.Propagate(true);
        fullAdder.In2.Propagate(true);
        fullAdder.InC.Propagate(false);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(false));
        Assert.That(airH.In1.Voltage, Is.EqualTo(true));
        
        fullAdder.In1.Propagate(true);
        fullAdder.In2.Propagate(true);
        fullAdder.InC.Propagate(true);
        Simulate(gates);
        Assert.That(airL.In1.Voltage, Is.EqualTo(true));
        Assert.That(airH.In1.Voltage, Is.EqualTo(true));
        
    }
    
    
    private void Simulate(List<LogicGate> gates, int ticks=100)
    {
        for (int i = 0; i < ticks; i++)
        {
            foreach (LogicGate gate in gates)
            {
                gate.Compute();
            }

            foreach (LogicGate gate in gates)
            {
                gate.Tick();
            }
        }
    }
    
}