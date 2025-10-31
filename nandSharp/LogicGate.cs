namespace nandSharp;

public abstract class LogicGate
{

    // Tick and Calculate are called alternating
    public abstract void Compute(); //Calculates the Nands

    public abstract void Tick(); //Updates the Voltages
}