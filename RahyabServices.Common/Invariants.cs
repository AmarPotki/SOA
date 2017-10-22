using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RahyabServices.Common
{
    public class Invariants
    {
        public class CookieKeys
        {
            public const string UserFirstName = "_ZAUTH:NAME";
            public const string UserHash = "_ZAUTH:HASH";
            public const string UserType = "_ZAUTH:TYPE";
            public const string UserLastName = "_ZAUTH:LASTNAME";
            public const string UserFullName = "_ZAUTH:FULLNAME";
        }

        public class ViewData
        {
            public const string UserSession = "Z:USER:SESSION:MODEL";
        }
    }
}
