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
    
    [Test]
    public void FlipFlopTest()
    {
        Source signalSt = new(false);
        Source signalD = new(false);
        Source clock = new(false);
        FlipFlop ff = new();
        Air air1 = new();
        List<LogicGate> gates = new() { signalSt, signalD, clock, ff, air1 };
        int ticks = signalSt.NeededTicks + ff.NeededTicks + air1.NeededTicks;
        Cable.Connect(signalSt.Out1, ff.InSt);
        Cable.Connect(signalD.Out1, ff.InD);
        Cable.Connect(clock.Out1, ff.InCl);
        Cable.Connect(ff.Out1, air1.In1);
        
        
        //state is undefined until bit is set, so it doesn't make sense to test it before.
        signalSt.Voltage = true;
        signalD.Voltage = false;
        clock.Voltage = true;
        Simulate(gates, ticks);
        
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        //should ignore D
        signalSt.Voltage = false;
        signalD.Voltage = true;
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        signalSt.Voltage = true;
        signalD.Voltage = true;
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        signalSt.Voltage = false;
        signalD.Voltage = false;
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        signalSt.Voltage = true;
        signalD.Voltage = false;
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        clock.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        signalSt.Voltage = true;
        signalD.Voltage = true;
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
        
        clock.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));

        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        
        signalSt.Voltage = true;
        signalD.Voltage = false;
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        
        clock.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(true));
        
        clock.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(air1.In1.Voltage, Is.EqualTo(false));
    }
    
    
    [Test]
    public void MemoryByteTest()
    {
        List<Source> inputs = new();
        Source storeSource = new(false);
        Source clockSource = new(false);
        MemoryByte memByte = new();
        List<Air> airs = new();
        for (int i = 0; i < 8; i++)
        {
            inputs.Add(new Source(false));
            airs.Add(new Air());
        }
        List<LogicGate> gates = new() {storeSource, clockSource, memByte};
        int ticks = storeSource.NeededTicks + memByte.NeededTicks + airs[0].NeededTicks;
        Cable.Connect(storeSource.Out1, memByte.InSt);
        Cable.Connect(clockSource.Out1, memByte.InCl);
        for (int i = 0; i < 8; i++)
        {
            gates.Add(inputs[i]);
            gates.Add(airs[i]);
            Cable.Connect(inputs[i].Out1, memByte.InD[i]);
            Cable.Connect(memByte.Out1[i], airs[i].In1);
        }

        //state is undefined until bit is set, so it doesn't make sense to test it before.
        storeSource.Voltage = true;
        clockSource.Voltage = true;
        inputs[0].Voltage = false;
        inputs[1].Voltage = false;
        inputs[2].Voltage = false;
        inputs[3].Voltage = false;
        inputs[4].Voltage = false;
        inputs[5].Voltage = false;
        inputs[6].Voltage = false;
        inputs[7].Voltage = false;
        Simulate(gates, ticks);
        //Bits aren't set yet
        
        //Clock 0
        clockSource.Voltage = false;
        Simulate(gates, ticks); //Now all Bits should be 0
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(false));

        storeSource.Voltage = true; 
        Simulate(gates, ticks); //should still all be false
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(false));
        
        //set some bits
        storeSource.Voltage = false;
        clockSource.Voltage = false;
        inputs[0].Voltage = true;
        inputs[1].Voltage = false;
        inputs[2].Voltage = false;
        inputs[3].Voltage = true;
        inputs[4].Voltage = true;
        inputs[5].Voltage = false;
        inputs[6].Voltage = true;
        inputs[7].Voltage = true;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(false)); 
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(false));
        
        //store
        storeSource.Voltage = true;
        clockSource.Voltage = false;
        inputs[0].Voltage = true;
        inputs[1].Voltage = false;
        inputs[2].Voltage = false;
        inputs[3].Voltage = true;
        inputs[4].Voltage = true;
        inputs[5].Voltage = false;
        inputs[6].Voltage = true;
        inputs[7].Voltage = true;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(false));
        
        //store bits
        clockSource.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(false));
        
        clockSource.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(true));
        
        //set two bits to 0
        storeSource.Voltage = true;
        clockSource.Voltage = false;
        inputs[6].Voltage = false;
        inputs[7].Voltage = false;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(true));

        clockSource.Voltage = true;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(true));

        clockSource.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(airs[0].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[1].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[2].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[3].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[4].In1.Voltage, Is.EqualTo(true));
        Assert.That(airs[5].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[6].In1.Voltage, Is.EqualTo(false));
        Assert.That(airs[7].In1.Voltage, Is.EqualTo(false));
    }

    [Test]
    public void RAMTest()
    {
        SignalProvider32 inputData = new(0);
        SignalProvider32 inputAddress = new(0);
        Source inputSt = new(false);
        Source inputCl = new(false);
        RAM ram = new(1024);
        Assert.That(ram.Blocks.Count * 4, Is.GreaterThanOrEqualTo(1024));
        Air32 output = new();
        
        Bus32.Connect(inputData.Out1, ram.InD);
        Bus32.Connect(inputAddress.Out1, ram.InAd);
        Cable.Connect(inputSt.Out1, ram.InSt);
        Cable.Connect(inputCl.Out1, ram.InCl);
        for (int i = 0; i < 32; i++)
        {
            Cable.Connect(ram.Out1[i], output.In1[i]);
        }

        List<LogicGate> gates = new List<LogicGate>() { inputData, inputAddress, inputSt, inputCl, ram, output };

        int ticks = inputSt.NeededTicks + ram.NeededTicks + output.NeededTicks;
        //Registers that are not written to, are oscillating between 0 and 1, so it doesn't make sense to test it before writing
        
        //set 0x0 to 0
        inputData.SetBits(0);
        inputAddress.SetBits(0); //Register 0
        inputSt.Voltage = true;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(0));
        
        inputData.SetBits(9);
        inputAddress.SetBits(4); //Register 1
        inputSt.Voltage = true;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(9));
        
        inputData.SetBits(5001);
        inputAddress.SetBits(64); //Register 16
        inputSt.Voltage = true;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(5001));
        
        inputData.SetBits(338001);
        inputAddress.SetBits(100); //Register 25
        inputSt.Voltage = true;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(338001));

        
        //Read saved data
        inputData.SetBits(0);
        inputAddress.SetBits(0);
        inputSt.Voltage = false;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(0));
        
        inputData.SetBits(0);
        inputAddress.SetBits(4);
        inputSt.Voltage = false;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(9));
        
        inputData.SetBits(0);
        inputAddress.SetBits(64);
        inputSt.Voltage = false;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(5001));
        
        inputData.SetBits(0);
        inputAddress.SetBits(100);
        inputSt.Voltage = false;
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        inputCl.Voltage = true;
        Simulate(gates, ticks);
        inputCl.Voltage = false;
        Simulate(gates, ticks);
        Assert.That(output.lastUnsigned, Is.EqualTo(338001));
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