using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SpiderHttp
{
    public class SpiderHelper
    {
        #region 字段

        private const int ConnectionLimit = 100;
        private Encoding _encoding = Encoding.UTF8;
        //浏览器类型
        private string[] _userAgents = new string[]{
            "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.90 Safari/537.36",
            "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; WOW64; Trident/7.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; Media Center PC 6.0)",
            "Mozilla/5.0 (Windows NT 6.1; rv:36.0) Gecko/20100101 Firefox/36.0",
            "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:31.0) Gecko/20130401 Firefox/31.0"
        };
        private string _userAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/42.0.2311.90 Safari/537.36";
        //接受类型
        private string _accept = "text/html, application/xhtml+xml, application/xml, */*";
        //超时时间
        private int _timeout = 2 * 1000;
        private string _contentType = "application/x-www-form-urlencoded";
        //cookies
        private string _cookies = "";
        private CookieCollection _cookiecollection;
        //custom heads
        private Dictionary<string, string> _headers = new Dictionary<string, string>();

        #endregion

        #region 构造
        public SpiderHelper()
        {
            _headers.Clear();
            //随机一个useragent
            _userAgent = _userAgents[new Random().Next(0, _userAgents.Length)];
            //解决性能问题?
            ServicePointManager.DefaultConnectionLimit = ConnectionLimit;
        }

        #endregion

        #region public method

        public void AddHeader(string key, string value)
        {
            _headers[key] = value;
        }

        public void ClearHeader()
        {
            _headers.Clear();
        }

        public SpiderResult HttpGet(string url, IWebProxy proxy = null) => HttpGet(url, url, proxy);

        public SpiderResult HttpGet(string url, string refer, IWebProxy proxy = null)
        {
            SpiderResult spider = null;
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckCertificate);
                var request = (HttpWebRequest)HttpWebRequest.Create(url);
                request.UserAgent = _userAgent;
                request.Timeout = _timeout;
                request.ContentType = _contentType;
                request.Accept = _accept;
                request.Method = "GET";
                request.Referer = refer;
                request.KeepAlive = true;
                request.AllowAutoRedirect = true;
                request.UnsafeAuthenticatedConnectionSharing = true;
                request.CookieContainer = new CookieContainer();
                //据说设为null可以提高性能
                request.Proxy = proxy;
                if (_cookiecollection != null)
                {
                    foreach (Cookie c in _cookiecollection)
                    {
                        c.Domain = request.Host;
                    }
                    request.CookieContainer.Add(_cookiecollection);
                }
                foreach (KeyValuePair<String, String> hd in _headers)
                {
                    request.Headers[hd.Key] = hd.Value;
                }
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    spider = GetFromResponse(response);
                    if (request.CookieContainer != null)
                    {
                        response.Cookies = request.CookieContainer.GetCookies(request.RequestUri);
                    }

                    if (response.Cookies != null)
                    {
                        _cookiecollection = response.Cookies;
                    }
                    if (response.Headers["Set-Cookie"] != null)
                    {
                        var cookie = response.Headers["Set-Cookie"];
                        _cookiecollection.Add(ConvertCookie(cookie));
                    }
                }
            }
            catch (Exception ex)
            {
                spider = new SpiderResult
                {
                    HttpCode = HttpStatusCode.InternalServerError,
                    Content = ex.Message
                };
            }
            return spider;
        }

        #endregion

        #region private method

        private SpiderResult GetFromResponse(HttpWebResponse response)
        {
            var r = new SpiderResult();
            try
            {
                using (var stream = response.GetResponseStream())
                {
                    using (var read = new StreamReader(stream, _encoding))
                    {
                        r.HttpCode = response.StatusCode;
                        r.Content = read.ReadToEnd();
                    }
                }

            }
            catch (Exception ex)
            {
                r.HttpCode = HttpStatusCode.InternalServerError;
                r.Content = ex.Message;
            }
            return r;
        }

        private bool CheckCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {
            return true;
        }

        private CookieCollection ConvertCookie(string cookie)
        {
            var cc = new CookieCollection();
            var cookies = cookie.Split(";".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < cookies.Length; i++)
            {
                string[] cookies_2 = cookies[i].Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                for (int j = 0; j < cookies_2.Length; j++)
                {
                    string[] cookies_3 = cookies_2[j].Trim().Split("=".ToCharArray());
                    if (cookies_3.Length == 2)
                    {
                        string cname = cookies_3[0].Trim();
                        string cvalue = cookies_3[1].Trim();
                        if (cname.ToLower() != "domain" && cname.ToLower() != "path" && cname.ToLower() != "expires")
                        {
                            Cookie c = new Cookie(cname, cvalue);
                            cc.Add(c);
                        }
                    }
                }
            }
            return cc;
        }

        #endregion
    }
}
