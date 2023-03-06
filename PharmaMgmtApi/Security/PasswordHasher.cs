namespace PharmaMgmtApi.Security;

public class PasswordHasher
{
    private const string Key = "7fbd7843-a7e8-4f18-8f99-74cd4dba3039";

    public static (string Hash, string Salt) Hash(string password)
    {
        var salt = Guid.NewGuid().ToString();
        var hash = BCrypt.Net.BCrypt.HashPassword(salt + password + Key);
        return (hash, salt);
    }
    public static bool Verify(string password, string salt, string hash)
        => BCrypt.Net.BCrypt.Verify(salt + password + Key, hash);
}