namespace NeoCart.Application.DTOs;

public class UserProfileDto
{ 
    public string Username { get; set; }  
    public string Email { get; set; }
    public IEnumerable<string> Roles { get; set; }
}