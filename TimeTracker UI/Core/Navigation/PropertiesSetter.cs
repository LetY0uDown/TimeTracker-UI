using System.Reflection;

namespace TimeTracker.UI.Core.Navigation;

public static class PropertiesSetter
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="target">Объект, принимающий параметры</param>
    /// <param name="parameters">Список параметров</param>
    public static void SetParameters (in object target, (object value, string fieldName)[] parameters)
    {
        var targetType = target.GetType();

        var fields = targetType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance);

        if (fields.Length == 0) {
            throw new InvalidOperationException("Target object has no fields");
        }

        foreach (var field in fields) {
            var attribute = field.GetCustomAttributes(typeof(ParameterAttribute), false).FirstOrDefault() as ParameterAttribute;

            if (attribute is null) { continue; }

            (object value, _) = parameters.Where(param => param.fieldName == attribute.Name).FirstOrDefault();

            if (value is null) {
                throw new ArgumentNullException("No parameter found with name " + attribute.Name);
            }

            field.SetValue(target, value);
        }
    }
}