namespace ParlayRMS.Legacy.ViewModel.ManageAgent;

using System.ComponentModel.DataAnnotations;

public class AddAgentModel
{
    [Required]
    [Display(Name = "Username", ResourceType = typeof(ViewLang))]
    [DisplayFormat(ConvertEmptyStringToNull = false)]
        
    public string UserName { get; set; }
}