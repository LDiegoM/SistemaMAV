using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Web.ViewModels.Account;

public class ForgotPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
