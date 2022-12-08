using System.ComponentModel.DataAnnotations;

namespace WebApp.Models.Users;

public class UserToSignIn
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
}