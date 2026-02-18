using market.dtos;
using market.Models;

namespace market.Services.internalinterface;

public interface IAuthService
{
    Task<AuthResponseDto?> LoginAsync(LoginDto loginDto);
    Task<AuthResponseDto?> RegisterAsync(RegisterDto registerDto);
    string GenerateJwtToken(User user);
}

