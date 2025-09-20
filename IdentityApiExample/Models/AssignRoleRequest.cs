using System.ComponentModel.DataAnnotations;

namespace IdentityApiExample.Models;

public class AssignRoleRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }

    [Required]
    public string RoleName { get; set; }
}
