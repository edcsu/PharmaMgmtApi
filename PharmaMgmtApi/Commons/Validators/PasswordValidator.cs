namespace PharmaMgmtApi.Commons.Validators;

public class PasswordValidator
{
    public static (bool IsValid, string Message) IsStrong(string password)
    {
        var isLower = false;
        var isUpper = false;
        var isDigit = false;
        var isChar = false;

        foreach (var k in password.Select(t => (int)t))
        {
            switch (k)
            {
                case >= 97 and <= 122:
                    isLower = true;
                    break;
                case >= 65 and <= 90:
                    isUpper = true;
                    break;
                case >= 48 and <= 57:
                    isDigit = true;
                    break;
                case > 32 and < 127:
                    isChar = true;
                    break;
            }
        }
        if (!isLower)
            return (IsValid: false, Message: "Password must be at least one lower letter");
        if (!isUpper)
            return (IsValid: false, Message: "Password must be at least one upper letter");
        if (!isDigit)
            return (IsValid: false, Message: "Password must be at least one digit");
        return !isChar ? (IsValid: false, Message: "Password must be at least one character as (!@#$%^&*()-+)") : (IsValid: true, Message: "Password is strong!");
    }
}