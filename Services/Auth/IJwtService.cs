using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Auth
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken(User user);
    }
}