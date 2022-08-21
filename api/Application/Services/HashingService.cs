using System.Security.Cryptography;
using System.Text;

namespace Application;

public class HashingService : IHashingService
{
    public string Compute(string text)
    {
        using var sha256 = SHA256.Create();
        var hashed = sha256.ComputeHash(Encoding.UTF8.GetBytes(text));

        var sb = new StringBuilder();

        foreach (var t in hashed)
        {
            sb.Append(t.ToString("x2"));
        }
            
        return sb.ToString();
    }

    public bool Verify(string text, string hashedText)
    {
        var result = Compute(text);

        var comparer = StringComparer.OrdinalIgnoreCase;

        return comparer.Compare(result, hashedText) == 0;
    }
}