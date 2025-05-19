using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Listings.Response;

public class GetOwnedListingsResponseDto : GetListingsResponseDto
{
    public DateTimeOffset CreateDateTime { get; set; }
}