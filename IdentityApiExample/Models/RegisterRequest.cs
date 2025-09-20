using System.ComponentModel.DataAnnotations;

namespace IdentityApiExample.Models;

public class RegisterRequest
{
    [Required]
    [EmailAddress]
    public string Email { get; set; } 

    [Required]
    public string Password { get; set; } 
}


