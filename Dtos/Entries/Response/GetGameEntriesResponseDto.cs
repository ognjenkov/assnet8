using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Dtos.Entries.Response;

public class GetGameEntriesResponseDto
{
    public DateTime CreateDateTime { get; set; }
    public int OpNumber { get; set; }
    public int RentNumber { get; set; }
    public string? Message { get; set; }
    public UserSimpleDto? User { get; set; }
}