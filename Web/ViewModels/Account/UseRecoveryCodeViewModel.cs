using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Web.ViewModels.Account;

public class UseRecoveryCodeViewModel
{
    [Required]
    public string Code { get; set; }

    public string ReturnUrl { get; set; }
}
