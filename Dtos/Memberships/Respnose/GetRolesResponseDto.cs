using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Memberships.Response;

public class GetRolesResponseDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Name { get; set; }
}