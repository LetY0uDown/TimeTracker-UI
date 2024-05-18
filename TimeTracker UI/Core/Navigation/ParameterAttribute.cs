namespace TimeTracker.UI.Core.Navigation;

[AttributeUsage(AttributeTargets.Field)]
public class ParameterAttribute (string name) : Attribute
{
    public string Name { get; } = name;

    public override bool Equals (object? obj)
    {
        if (obj is ParameterAttribute second)
            return Name == second.Name;

        return false;
    }

    public override int GetHashCode ()
    {
        return HashCode.Combine(base.GetHashCode(), Name);
    }
}