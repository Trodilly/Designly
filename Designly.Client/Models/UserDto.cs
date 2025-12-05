namespace Designly.Client.Models;

public class UserDto
{
    public Guid Id { get; set; }
    public string Username { get; set; } = string.Empty;
    // Password is returned by the API (unfortunately), but we won't display it.
}
