namespace NandTests;

using nandSharp.Connecters;
using nandSharp.IO;
using nandSharp.Memory;
using nandSharp;

public class MemoryTests
{
    
    [SetUp]
    public void Setup()
    {
    }
    
    [Test]
    public void SRLatchSetTest()
    {
        Source signalS = new(false);
        Source signalR = new(false);
        SRLatch srLatch = new();
        Air air1 = new();
        List<LogicGate> gates = new() { signalS, signalR, srLatch, air1 };
        int ticks = signalS.NeededTicks + srLatch.NeededTicks + air1.NeededTicks;
        Cable.Connect(signalS.Out1, srLatch.InS);
        Cable.Connect(signalR.Out1, srLatch.InR);
        Cable.Connect(srLatch.Out1, air1.In1);

        signalS.Voltage = true;
        signalR.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalR.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalR.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
    }
    
    [Test]
    public void SRLatchResetTest()
    {
        Source signalS = new(false);
        Source signalR = new(false);
        SRLatch srLatch = new();
        Air air1 = new();
        List<LogicGate> gates = new() { signalS, signalR, srLatch, air1 };
        int ticks = signalS.NeededTicks + srLatch.NeededTicks + air1.NeededTicks;
        Cable.Connect(signalS.Out1, srLatch.InS);
        Cable.Connect(signalR.Out1, srLatch.InR);
        Cable.Connect(srLatch.Out1, air1.In1);

        signalS.Voltage = false;
        signalR.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalS.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalS.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
    }
    
    
    [Test]
    public void DLatchNegativeTest()
    {
        Source signalSt = new(false);
        Source signalD = new(false);
        DLatch dLatch = new();
        Air air1 = new();
        List<LogicGate> gates = new() { signalSt, signalD, dLatch, air1 };
        int ticks = signalSt.NeededTicks + dLatch.NeededTicks + air1.NeededTicks;
        Cable.Connect(signalSt.Out1, dLatch.InSt);
        Cable.Connect(signalD.Out1, dLatch.InD);
        Cable.Connect(dLatch.Out1, air1.In1);

        signalSt.Voltage = true;
        signalD.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalSt.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalSt.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalSt.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
    }
    
    [Test]
    public void DLatchPositiveTest()
    {
        Source signalSt = new(false);
        Source signalD = new(false);
        DLatch dLatch = new();
        Air air1 = new();
        List<LogicGate> gates = new() { signalSt, signalD, dLatch, air1 };
        int ticks = signalSt.NeededTicks + dLatch.NeededTicks + air1.NeededTicks;
        Cable.Connect(signalSt.Out1, dLatch.InSt);
        Cable.Connect(signalD.Out1, dLatch.InD);
        Cable.Connect(dLatch.Out1, air1.In1);

        signalSt.Voltage = true;
        signalD.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalD.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalD.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalD.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalSt.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalD.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalD.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        signalSt.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalSt.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalD.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        signalSt.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
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