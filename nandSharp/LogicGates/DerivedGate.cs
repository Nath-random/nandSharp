namespace nandSharp.LogicGates;

public abstract class DerivedGate : LogicGate
{
    // public ConnectorPlug Out1;
    
    protected DerivedGate()
    {
        // Out1 = new ConnectorPlug();
    }
    
    
    // public virtual void AppendDestination(Plug plug)
    // {
    //     if (Out1.DestCable == null)
    //     {
    //         Cable cable = new Cable();
    //         Out1.DestCable = cable;
    //         cable.Source = Out1;
    //     }
    //     Out1.DestCable.Dests.Add(plug);
    //     plug.SourceCable = Out1.DestCable;
    // }

}