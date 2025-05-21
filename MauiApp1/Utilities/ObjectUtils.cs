using System.Reflection;

namespace MauiApp1.Utilities;

public class ObjectUtils
{
    public static bool AreEqualByProperties<T>(T a, T b)
    {
        if (a == null || b == null)
            return false;

        var type = typeof(T);
        foreach (var prop in type.GetProperties(BindingFlags.Public | BindingFlags.Instance))
        {
            var valueA = prop.GetValue(a);
            var valueB = prop.GetValue(b);

            if (valueA == null && valueB == null)
                continue;

            if (valueA == null || valueB == null || !valueA.Equals(valueB))
                return false;
        }

        return true;
    }
}