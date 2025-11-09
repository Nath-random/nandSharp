namespace NandTests;

using nandSharp.Gates32Bit;
using nandSharp.Connecters;
using nandSharp;


public class ALUGatesTests
{
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void Add32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Add32 add = new();
        Air32 air1 = new();
        Air carryAir = new();
        int ticks = number1.NeededTicks + add.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, add.In1);
        Bus32.Connect(number2.Out1, add.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(add.Out1[i], air1.In1[i]);
        }
        Cable.Connect(add.OutC, carryAir.In1);
        List<LogicGate> gates = new List<LogicGate>() { number1, number2, add, air1, carryAir };

        
        number1.SetBits(150);
        number2.SetBits(220);
        Simulate(gates, ticks);
        add.Compute();
        Assert.That(air1.lastUnsigned, Is.EqualTo(370));
        Assert.That(air1.lastInt, Is.EqualTo(370));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));

        number1.SetBits(-550);
        number2.SetBits(100);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED -449));
        Assert.That(air1.lastInt, Is.EqualTo(-450));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(-550);
        number2.SetBits(549);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
        Assert.That(air1.lastInt, Is.EqualTo(-1));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(-300000000);
        number2.SetBits(300000001);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(1));
        Assert.That(air1.lastInt, Is.EqualTo(1));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(1);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        Assert.That(air1.lastInt, Is.EqualTo(0));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(1);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        Assert.That(air1.lastInt, Is.EqualTo(0));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));
    }
    
    
    [Test]
    public void Inc32Test()
    {
        SignalProvider32 number1 = new(0);
        Inc32 inc = new();
        Air32 air1 = new();
        Air carryAir = new();
        Air testAir = new();
        int ticks = number1.NeededTicks + inc.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, inc.In1);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(inc.Out1[i], air1.In1[i]);
        }
        Cable.Connect(inc.OutC, carryAir.In1);
        List<LogicGate> gates = new List<LogicGate>() { number1, inc, air1, carryAir, testAir };
        Cable.Connect(inc.Out1[3], testAir.In1);
        
        number1.SetBits(0);
        Simulate(gates, 500);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(1));
        Assert.That(air1.lastInt, Is.EqualTo(1));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));

        number1.SetBits(999333);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(999334));
        Assert.That(air1.lastInt, Is.EqualTo(999334));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(1023);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(1024));
        Assert.That(air1.lastInt, Is.EqualTo(1024));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        Assert.That(air1.lastInt, Is.EqualTo(0));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));
        
        number1.SetBits(-25000);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED -24998));
        Assert.That(air1.lastInt, Is.EqualTo(-24999));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
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