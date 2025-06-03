namespace ecommerce.Core.Models;
public class ApplicationUser
{
    public Guid UserId { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? FullName { get; set; }
    public string? Gender { get; set; }
}
