using market.Models;
using Microsoft.EntityFrameworkCore;

namespace market.Services.implement;

public class DataSeeder
{
    private readonly marketdBContext _context;

    public DataSeeder(marketdBContext context)
    {
        _context = context;
    }

    public async Task SeedAdminAsync()
    {
        // Check if admin already exists
        var adminExists = await _context.Users
            .AnyAsync(u => u.Username == "admin" || u.Role == "Admin");

        if (!adminExists)
        {
            var admin = new User
            {
                Username = "admin",
                Email = "admin@market.com",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("Admin@123"), // Default password
                Role = "Admin",
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(admin);
            await _context.SaveChangesAsync();
        }
    }
}

