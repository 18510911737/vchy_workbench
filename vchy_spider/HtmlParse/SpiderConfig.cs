using System;
using System.Collections.Generic;
using System.Text;

namespace HtmlParse
{
    public class SpiderConfig
    {
        /// <summary>
        /// 爬取名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// jquery 规则
        /// </summary>
        public string Jquery { get; set; }

        /// <summary>
        /// 获取value 类型
        /// </summary>
        public SpiderValueType ValueType { get; set; }

        /// <summary>
        /// value name
        /// </summary>
        public string ValueTypeName { get; set; }

        /// <summary>
        /// 检查是否为空
        /// </summary>
        public bool IsCheckNull { get; set; }
    }

    public enum SpiderValueType
    {
        Html,
        Attr,
        Href,
        Text
    }
}
