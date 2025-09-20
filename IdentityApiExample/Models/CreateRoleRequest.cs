using System.ComponentModel.DataAnnotations;

namespace IdentityApiExample.Models;

public class CreateRoleRequest
{
    [Required]
    [MinLength(2)]
    public string Name { get; set; }
}
