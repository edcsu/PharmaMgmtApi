using System.ComponentModel.DataAnnotations;

namespace PharmaMgmtApi.ViewModels.Users;

public class EmailAddress
{
    [Required(ErrorMessage = "Email is required")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Code is required")]
    public string Code { get; set; } = string.Empty;
}