using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using blog.Data;

namespace blog.Controllers;

[ApiController]
[Route("")]  // Health Check - para pingar a API na rota base/raiz e saber se a API está online ou offline.
public class HomeController : ControllerBase
{
    // Health Check - para pingar a API na rota base/raiz e saber se a API está online ou offline.
   [HttpGet("")]
    public IActionResult Get()
    {
        return Ok();
    }
}
