namespace NeoCart.Application.DTOs;

public class UserProfileDto
{ 
    public required string Username { get; set; }  
    public required string Email { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
}