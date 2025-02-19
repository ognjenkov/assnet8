using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace assnet8.Utils;
public static class Utils
{
    public static string GenerateImageFrontendLink(Guid imageId)
    {
        return "localhost////////" + imageId;
    }
}