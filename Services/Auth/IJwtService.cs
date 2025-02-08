using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Auth
{
    public interface IJwtService
    {
        string GenerateAccessToken(User user, DateTime? expires);
        string GenerateRefreshToken(User user, DateTime? expires);
        DecodedRefreshToken DecodeRefreshToken(string refreshtoken);
    }
}