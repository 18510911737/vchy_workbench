using HtmlParse;
using SpiderHttp;
using SpiderModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using VchyORMFactory.Factotry;

namespace SpiderProxy
{
    class Program
    {
        private static string _json = "Proxy_xicidaili.json";
        private static string _urls = "urls";
        private static string _table = "table";
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var spider = new Spider(_json, _urls, "config", "table");
            ProxyTask task = new ProxyTask();
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"task{i + 1}");
                task.Proxy(spider);
                Thread.Sleep(10 * 1000);
            }
            while (true)
            {
                Console.ReadLine();
            }
        }

    }

    public class ProxyTask
    {
        private SpiderHelper _http = new SpiderHelper();
        private DapperFactory _factory = new DapperFactory();
        public async void Proxy(Spider spider)
        {
            await Task.Run(async () =>
            {
                while (true)
                {
                    var r = await spider.SpiderAsync<Proxy>();
                    if (r == null)
                    {
                        return;
                    }
                    var q = new Queue<Proxy>();
                    r.ForEach(f =>
                    {
                        q.Enqueue(f);
                    });
                    while (q.TryDequeue(out Proxy p))
                    {
                        try
                        {
                            Console.WriteLine($"dequeue:{p.IP}:{p.Port}");
                            IWebProxy proxy = new WebProxy(p.IP, Convert.ToInt32(p.Port));
                            var response = _http.HttpGet("http://www.baidu.com", proxy);
                            if (response.HttpCode == HttpStatusCode.OK)
                            {
                                Console.WriteLine($"insert:{p.IP}:{p.Port}");
                                _factory.ExcuteInsert(p);
                            }
                        }
                        catch
                        {
                        }

                    }
                }
            });
            Console.WriteLine("task over");
        }
    }
}
