using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using VchyCalculator;

namespace vchy_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToolsController : ControllerBase
    {
        [Route("calculator")]
        [HttpGet()]
        [HttpPost()]
        public string Calculator([FromBody]JObject data)
        {
            CalculatorStack stack = new CalculatorStack();
            return stack.Calculator(data.Value<string>("exception")).ToString();
        }
    }
}