using PBL3_2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Hosting;

namespace PBL3_2.BBL
{
    public class AccounsAccountInfo
    {
        public Account Account { get; set; }
        public AccountInfo AccountInfo { get; set; }
    }
    public class PhanQuyen{
        public static IPrincipal GetCurrentUser()
        {
            if (HostingEnvironment.IsHosted)
            {
                HttpContext current = HttpContext.Current;
                if (current != null)
                {
                    return current.User;
                }
            }
            
            return Thread.CurrentPrincipal;
        }
    }
}