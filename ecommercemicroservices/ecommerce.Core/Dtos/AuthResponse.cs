namespace ecommerce.Core.Dtos;
public record AuthResponse(
    Guid UserId,
    string? Email,
    string? Password,
    string? FullName,
    string? Gender,
    string? Token,
    bool Success);
