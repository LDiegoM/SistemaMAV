using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Web.ViewModels.Manage;

public class ChangePasswordViewModel
{
    [Required(ErrorMessage = "Debe ingresar la contraseña actual")]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña actual")]
    public string OldPassword { get; set; }

    [Required(ErrorMessage = "Debe ingresar la contraseña nueva")]
    [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
    [DataType(DataType.Password)]
    [Display(Name = "Contraseña nueva")]
    public string NewPassword { get; set; }

    [Required(ErrorMessage = "Debe confirmar la contraseña nueva")]
    [DataType(DataType.Password)]
    [Display(Name = "Confirme la nueva contraseña")]
    [Compare("NewPassword", ErrorMessage = "Las contraseñas no coinciden.")]
    public string ConfirmPassword { get; set; }
}
