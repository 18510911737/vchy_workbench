using System;
using System.Collections.Generic;
using System.Text;
using VchyModel;
using VchyORMAttribute;

namespace SpiderModel
{
    [Table(table: "Proxy")]
    public class Proxy:BaseEntity
    {
        [Field(field: "ID", IsKey = true)]
        public int ID { get; set; }

        [Field(field: "Country")]
        public string Country { get; set; }

        [Field(field: "IP")]
        public string IP { get; set; }

        [Field(field: "Port")]
        public string Port { get; set; }

        [Field(field: "Address")]
        public string Address { get; set; }

        [Field(field: "Anonymous")]
        public string Anonymous { get; set; }

        [Field(field: "Method")]
        public string Method { get; set; }

        [Field(field: "Speed")]
        public string Speed { get; set; }

        [Field(field: "Connection")]
        public string Connection { get; set; }

        [Field(field: "Life")]
        public string Life { get; set; }

        [Field(field: "CheckTime")]
        public string CheckTime { get; set; }
    }
}
