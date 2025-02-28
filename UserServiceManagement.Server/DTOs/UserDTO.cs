using System.ComponentModel.DataAnnotations;

public class UserDTO
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public string? ProfilePictureUrl { get; set; }
    public required string Password { get; set; }
}