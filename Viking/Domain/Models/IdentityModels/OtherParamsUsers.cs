using System.ComponentModel.DataAnnotations;

namespace Viking.Domain.Models.IdentityModels;

public class OtherParamsUsers : ParamsUsers
{
    public string UserName { get; set; }
    public bool SeniorManager { get; set; }
    [Required]
    public EnumProfession Profession { get; set; }
}