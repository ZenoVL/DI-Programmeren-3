namespace KdG.DI.components.attributes;

[AttributeUsage(AttributeTargets.Method)]
public class KeyInputGetByMethodName:Attribute
{
    private string Route { get; set; }
    
    public KeyInputGetByMethodName(string route)
    {
        Route = route.ToLower();
    }
}