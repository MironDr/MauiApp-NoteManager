using System.Reflection;
using System.Security.Cryptography;

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
    public static void CreatePasswordHash(string password, out byte[] hash, out byte[] salt)
    {
        using var hmac = new Rfc2898DeriveBytes(password, 16, 100_000, HashAlgorithmName.SHA256);
        salt = hmac.Salt;
        hash = hmac.GetBytes(32);
    }
}