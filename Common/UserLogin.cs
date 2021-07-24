using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace langfvn.Common
{
    [Serializable]
    public class UserLogin
    {
        public int UserID { set; get; }
        public String UserName { set; get; }
        public int RoleID { set; get; }

    }
}