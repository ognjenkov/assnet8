using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Services.Utils;

public interface INextJsRevalidationService
{
    Task<bool> RevalidatePathAsync(string path);
    Task<bool> RevalidateTagAsync(string tag);
}