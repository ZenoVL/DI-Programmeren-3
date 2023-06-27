namespace KdG.DI.components.attributes;

[AttributeUsage(AttributeTargets.Method)]
public class KeyInputGetByNumber:Attribute
{
    public int Number { get; }
    
    public KeyInputGetByNumber(int number)
    {
        Number = number;
    }
}