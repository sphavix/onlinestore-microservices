namespace ecommerce.Core.Dtos;
public record AuthResponse(
    Guid UserId,
    string? Email,
    string? FullName,
    string? Gender,
    string? Token,
    bool Success)
{
    public AuthResponse() : this(default, default, default, default, default, default) { } // Default constructor for deserialization
}
