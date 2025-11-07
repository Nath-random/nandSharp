namespace nandSharp;

public abstract class LogicGate
{
    public int NandCount = 0;
    public int NeededTicks = 0;
        
        
    // Tick and Calculate are called alternating
    public abstract void InitStats();
    public abstract void Compute(); //Calculates the Nands

    public abstract void Tick(); //Updates the Voltages
    

}
