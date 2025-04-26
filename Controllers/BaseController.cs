using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;
using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

namespace assnet8.Controllers;
[ApiController]
[Route("api")]
public abstract class BaseController : Controller
{
}