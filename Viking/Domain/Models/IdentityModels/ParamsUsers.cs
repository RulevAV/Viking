using System.ComponentModel.DataAnnotations;

namespace Viking.Domain.Models.IdentityModels;

public class ParamsUsers
{
    [Required]
    public string login { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string password { get; set; }
}