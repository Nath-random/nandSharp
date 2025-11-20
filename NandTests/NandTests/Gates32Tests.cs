namespace NandTests;

using nandSharp.Gates32Bit;
using nandSharp.Connecters;
using nandSharp;
using nandSharp.IO;


public class Gates32Tests
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
        int ticks = number1.NeededTicks + inc.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, inc.In1);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(inc.Out1[i], air1.In1[i]);
        }
        Cable.Connect(inc.OutC, carryAir.In1);
        List<LogicGate> gates = new List<LogicGate>() { number1, inc, air1, carryAir };
        
        number1.SetBits(0);
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


    [Test]
    public void And32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        And32 and = new();
        Air32 air1 = new();
        int ticks = number1.NeededTicks + and.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, and.In1);
        Bus32.Connect(number2.Out1, and.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(and.Out1[i], air1.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { number1, number2, and, air1 };

        number1.SetBits(0);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        
        number1.SetBits(7);
        number2.SetBits(3);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(3));

        number1.SetBits(200083);
        number2.SetBits(163592);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(134400));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
    }

    [Test]
    public void Or32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Or32 or = new();
        Air32 air1 = new();
        int ticks = number1.NeededTicks + or.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, or.In1);
        Bus32.Connect(number2.Out1, or.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(or.Out1[i], air1.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { number1, number2, or, air1 };

        number1.SetBits(0);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        
        number1.SetBits(7);
        number2.SetBits(3);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(7));

        number1.SetBits(200083);
        number2.SetBits(163592);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(229275));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
    }
    
    [Test]
    public void Nand32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Nand32 nand = new();
        Air32 air1 = new();
        int ticks = number1.NeededTicks + nand.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, nand.In1);
        Bus32.Connect(number2.Out1, nand.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(nand.Out1[i], air1.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { number1, number2, nand, air1 };

        number1.SetBits(0);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
        
        number1.SetBits(7);
        number2.SetBits(3);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED - 3));

        number1.SetBits(200083);
        number2.SetBits(163592);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED - 134400));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
    }
    
    [Test]
    public void Xor32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Xor32 xor = new();
        Air32 air1 = new();
        int ticks = number1.NeededTicks + xor.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, xor.In1);
        Bus32.Connect(number2.Out1, xor.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(xor.Out1[i], air1.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { number1, number2, xor, air1 };

        number1.SetBits(7);
        number2.SetBits(3);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(4));

        number1.SetBits(200083);
        number2.SetBits(163592);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(94875));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
    }
    
    [Test]
    public void Nor32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Nor32 nor = new();
        Air32 air1 = new();
        int ticks = number1.NeededTicks + nor.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, nor.In1);
        Bus32.Connect(number2.Out1, nor.In2);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(nor.Out1[i], air1.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { number1, number2, nor, air1 };

        number1.SetBits(7);
        number2.SetBits(3);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED - 7));

        number1.SetBits(200083);
        number2.SetBits(163592);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED - 229275));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(0);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
    }

    [Test]
    public void Not32Test()
    {
        SignalProvider32 number1 = new(0);
        Not32 not = new();
        Air32 air1 = new();
        int ticks = number1.NeededTicks + not.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, not.In1);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(not.Out1[i], air1.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { number1, not, air1 };

        number1.SetBits(7);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED - 7));


        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(0));
        
        number1.SetBits(0, false);
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
    }
    
    
    [Test]
    public void Sub32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Source carryIn = new Source(false);
        Sub32 sub = new();
        Air32 air1 = new();
        Air carryAir = new();
        int ticks = Math.Max(number1.NeededTicks, carryIn.NeededTicks) + sub.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, sub.In1);
        Bus32.Connect(number2.Out1, sub.In2);
        Cable.Connect(carryIn.Out1, sub.InB);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(sub.Out1[i], air1.In1[i]);
        }
        Cable.Connect(sub.OutB, carryAir.In1);
        List<LogicGate> gates = new List<LogicGate>() { number1, number2, carryIn, sub, air1, carryAir };

        
        number1.SetBits(150);
        number2.SetBits(220);
        carryIn.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(-70));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));

        number1.SetBits(77000);
        number2.SetBits(150);
        carryIn.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(76850));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(0);
        number2.SetBits(0);
        carryIn.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(0));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(500);
        number2.SetBits(0);
        carryIn.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(500));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(0);
        number2.SetBits(600);
        carryIn.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(-600));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));
        
        number1.SetBits(500);
        number2.SetBits(100);
        carryIn.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(399));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(false));
        
        number1.SetBits(20);
        number2.SetBits(30);
        carryIn.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(-11));
        Assert.That(carryAir.In1.Voltage, Is.EqualTo(true));
    }
    
    [Test]
    public void Mux32Test()
    {
        SignalProvider32 number1 = new(0);
        SignalProvider32 number2 = new(0);
        Source selectBit = new(false);
        Mux32 mux = new();
        Air32 air1 = new();
        int ticks = Math.Max(number1.NeededTicks, selectBit.NeededTicks) + mux.NeededTicks + air1.NeededTicks;
        Bus32.Connect(number1.Out1, mux.In0);
        Bus32.Connect(number2.Out1, mux.In1);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(mux.Out1[i], air1.In1[i]);
        }
        Cable.Connect(selectBit.Out1, mux.InS);
        List<LogicGate> gates = new List<LogicGate>() { number1, number2, selectBit, mux, air1 };

        
        number1.SetBits(0);
        number2.SetBits(0);
        selectBit.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(0));
        
        number1.SetBits(0);
        number2.SetBits(500);
        selectBit.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(0));
        
        number1.SetBits(30890);
        number2.SetBits(0);
        selectBit.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(0));
        
        number1.SetBits(55000);
        number2.SetBits(130);
        selectBit.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(55000));

        number1.SetBits(389134);
        number2.SetBits(999888);
        selectBit.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.lastInt, Is.EqualTo(999888));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        number2.SetBits(10);
        selectBit.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
    }
    
    
    [Test]
    public void Switch32Test()
    {
        SignalProvider32 number1 = new(0);
        Source selectBit = new(false);
        Switch32 swi = new();
        Air32 air0 = new();
        Air32 air1 = new();
        int ticks = Math.Max(number1.NeededTicks, selectBit.NeededTicks) + swi.NeededTicks + air1.NeededTicks;
        Cable.Connect(selectBit.Out1, swi.InS);
        Bus32.Connect(number1.Out1, swi.In1);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(swi.Out0[i], air0.In1[i]);
            Cable.Connect(swi.Out1[i], air1.In1[i]);
        }
        List<LogicGate> gates = new List<LogicGate>() { number1, selectBit, swi, air0, air1 };
        
        number1.SetBits(0);
        selectBit.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air0.lastInt, Is.EqualTo(0));
        Assert.That(air1.lastInt, Is.EqualTo(0));

        number1.SetBits(55000);
        selectBit.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air0.lastInt, Is.EqualTo(55000));
        Assert.That(air1.lastInt, Is.EqualTo(0));

        number1.SetBits(999111);
        selectBit.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air0.lastInt, Is.EqualTo(0));
        Assert.That(air1.lastInt, Is.EqualTo(999111));

        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        selectBit.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air0.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
        Assert.That(air1.lastInt, Is.EqualTo(0));
        
        number1.SetBits(SignalProvider32.HIGHEST_UNSIGNED, false);
        selectBit.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air0.lastInt, Is.EqualTo(0));
        Assert.That(air1.lastUnsigned, Is.EqualTo(SignalProvider32.HIGHEST_UNSIGNED));
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