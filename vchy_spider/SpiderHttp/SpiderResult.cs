using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace SpiderHttp
{
    public class SpiderResult
    {
        public HttpStatusCode HttpCode { get; set; }

        public string Content { get; set; }

    }
}
