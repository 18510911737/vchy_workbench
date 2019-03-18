using System;
using System.Collections.Generic;
using System.Text;

namespace VchyMusic
{
    public abstract class RequestData
    {
        public abstract string Url { get; }

        public virtual string Method
        {
            get => "Get";
            set => Method = value;
        }

        public object FormData { get; set; }
    }
}
