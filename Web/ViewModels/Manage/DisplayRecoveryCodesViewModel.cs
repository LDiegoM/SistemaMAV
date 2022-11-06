using System.ComponentModel.DataAnnotations;

namespace SistemaMAV.Web.ViewModels.Manage;

public class DisplayRecoveryCodesViewModel
{
    [Required]
    public IEnumerable<string> Codes { get; set; }

}
