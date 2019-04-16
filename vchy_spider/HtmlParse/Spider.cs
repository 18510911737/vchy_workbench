using Less.Html;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SpiderHttp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HtmlParse
{
    /// <summary>
    /// 代理IP
    /// </summary>
    public class Spider
    {
        private ConfigHelper _configHelper = null;
        private Queue<string> _queue = new Queue<string>();
        private List<SpiderConfig> _config;
        private string _table;
        public Spider(string json, string urls, string config, string table = null)
        {
            _table = table;
            _configHelper = new ConfigHelper(json);
            _config = _configHelper.Configuration.GetSection(config).Get<List<SpiderConfig>>();
            var array = _configHelper.Configuration.GetSection(urls).Get<string[]>();
            foreach (var item in array)
            {
                _queue.Enqueue(item);
            }
        }

        public async Task<List<T>> SpiderAsync<T>()
            where T : class
        {
            List<T> r = null;
            await Task.Run(() =>
               {
                   if (_queue.TryDequeue(out string url))
                   {
                       var result = new SpiderHelper().HttpGet(url);
                       if (result.HttpCode == HttpStatusCode.OK)
                       {
                           r = new List<T>();
                           var q = HtmlParser.Query(result.Content);
                           if (_table != null)
                           {
                               foreach (var item in q(_configHelper.Configuration[_table]))
                               {
                                   var t = GetHtmlParser<T>(q(item));
                                   if (t != null)
                                   {
                                       r.Add(t);
                                   }
                               }
                           }
                           else
                           {

                           }
                       }
                   }
               });
            return r;
        }

        private T GetHtmlParser<T>(Query query)
            where T : class
        {
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var item in _config)
            {
                var val = GetValues(query, item)?.Trim();
                if (item.IsCheckNull && string.IsNullOrWhiteSpace(val))
                {
                    return null;
                }
                dict.Add(item.Name, val);
            }
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(dict));
        }

        private string GetValues(Query query, SpiderConfig config)
        {
            query = query.find(config.Jquery);
            switch (config.ValueType)
            {
                case SpiderValueType.Html:
                    return query.html();
                case SpiderValueType.Attr:
                    return query.attr(config.ValueTypeName);
                case SpiderValueType.Href:
                    return query.href();
                case SpiderValueType.Text:
                    return query.text();
                default:
                    return "";
            }
        }
    }
}
