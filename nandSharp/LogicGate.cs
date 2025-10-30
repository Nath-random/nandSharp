namespace nandSharp;

public abstract class LogicGate
{
    //nur Nand hat Inplug und outplug
    //alle anderen haben connector plug
    //todo statische methode die plug zu plug verbindet machen
    //eventuell alle Verbindungen ändern, dass es nur noch vorwärts zeigt
    // public ConnectorPlug Out1;
    //
    // protected LogicGate()
    // {
    //     Out1 = new ConnectorPlug();
    // }
    //
    //
    // public virtual void AppendDestination(InPlug plug) // nur Nand überschreibt es, weil es einen OutPlug hat
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
    
    // Tick and Calculate are called alternating
    public abstract void Compute(); //Calculates the Nands

    public abstract void Tick(); //Updates the Voltages
}