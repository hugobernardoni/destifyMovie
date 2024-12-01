using System.Collections.Generic;

namespace DestifyMovie.Services.Interfaces;

public interface IJwtService
{
    string GenerateToken(string userId, string username, IEnumerable<string> roles);
}
