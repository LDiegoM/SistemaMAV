using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Web.ViewModels.Account;

public class LoginViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña")]
    public string Password { get; set; }

    [Display(Name = "Mantener la sesión iniciada")]
    public bool RememberMe { get; set; }
}
