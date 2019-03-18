using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace VchyMusic
{
    public static class NeteaseAPI
    {
        /// <summary>
        /// 搜索API
        /// </summary>
        /// <param name="s">要搜索的内容</param>
        /// <param name="limit">要返回的条数</param>
        /// <param name="offset">设置偏移量 用于分页</param>
        /// <param name="type">类型 [1 单曲] [10 专辑] [100 歌手] [1000 歌单] [1002 用户]</param>
        /// <returns>JSON</returns>
        public static string Search(string s = null, int limit = 30, int offset = 0, int type = 1)
        {
            return Request(new NeteaseSearch { FormData = new { s, limit, offset, type } });
        }

        /// <summary>
        /// 请求网易云音乐接口
        /// </summary>
        /// <typeparam name="T">要请求的接口类型</typeparam>
        /// <param name="config">要请求的接口类型的对象</param>
        /// <returns>请求结果(JSON)</returns>
        public static string Request<T>(T config)
            where T : RequestData, new()
        {
            // 请求URL
            string requestURL = config.Url;
            // 将数据包对象转换成QueryString形式的字符串
            string @params = config.FormData.ParseQueryString();
            bool isPost = config.Method.Equals("post", StringComparison.CurrentCultureIgnoreCase);

            if (!isPost)
            {
                // get方式 拼接请求url
                string sep = requestURL.Contains('?') ? "&" : "?";
                requestURL += sep + @params;
            }

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(requestURL);
            req.Accept = "*/*";
            req.Headers.Add("Accept-Language", "zh-CN,zh;q=0.8,gl;q=0.6,zh-TW;q=0.4");
            // 如果服务端启用了GZIP，那么下面必须解压，否则一直乱码。
            // 参见：http://www.crifan.com/set_accept_encoding_header_to_gzip_deflate_return_messy_code/
            req.Headers.Add("Accept-Encoding", "gzip,deflate,sdch");
            req.ContentType = "application/x-www-form-urlencoded";
            req.KeepAlive = true;
            req.Host = "music.163.com";
            req.Referer = "http://music.163.com/search/";
            req.UserAgent = "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_9_2) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.152 Safari/537";
            // 设置cookies
            req.Headers["Cookie"] = "appver=1.5.2";
            req.Method = config.Method;
            req.AutomaticDecompression = DecompressionMethods.GZip;
            if (isPost)
            {
                // 写入post请求包
                byte[] formData = Encoding.UTF8.GetBytes(@params);
                // 设置HTTP请求头  参考：https://github.com/darknessomi/musicbox/blob/master/NEMbox/api.py          
                req.GetRequestStream().Write(formData, 0, formData.Length);
            }
            // 发送http请求 并读取响应内容返回
            return new StreamReader(req.GetResponse().GetResponseStream(), Encoding.GetEncoding("UTF-8")).ReadToEnd();
        }

        /// <summary>
        /// 将对象转换成QueryString形式的字符串
        /// </summary>
        /// <param name="obj">要转换的对象</param>
        /// <returns></returns>
        private static string ParseQueryString(this object obj)
        {
            return string.Join("&", obj.GetType().GetProperties().Select(x => string.Format("{0}={1}", x.Name, x.GetValue(obj))));
        }

        /// <summary>
        /// 为字符串添加指定样式占位符
        /// </summary>
        /// <param name="s">要修改的字符串</param>
        /// <param name="placeholder">占位符规则</param>
        /// <returns></returns>
        private static string AddBrackets(this string s, string placeholder = "[{0}]")
        {
            return string.Format(placeholder, s ?? string.Empty);
        }
    }
}
