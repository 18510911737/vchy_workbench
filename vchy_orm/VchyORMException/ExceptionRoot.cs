using System;
using System.Collections.Generic;
using System.Text;

namespace VchyORMException
{
    public static class ExceptionRoot
    {
        private static readonly string _helpLink = "http://msdn.microsoft.com";

        public static string HelpLink() => _helpLink;
        public static string HelpLink(string search) => _helpLink;
    }
}
