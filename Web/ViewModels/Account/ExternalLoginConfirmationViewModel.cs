using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Web.ViewModels.Account;

public class ExternalLoginConfirmationViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
}
