using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using VchyMusic;

namespace vchy_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NeteaseController : ControllerBase
    {
        [Route("search")]
        [HttpGet()]
        public string Search(string s = null, int limit = 30, int offset = 0, int type = 1)
        {
            return  NeteaseAPI.Search(s, limit, offset, type);
        }
    }
}