using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uTools
{
    public class uToolsCore
    {
        public static void Authorize()
        {
            if (umbraco.BusinessLogic.User.GetCurrent() == null)
            {
                HttpContext.Current.Response.StatusCode = 403;
                HttpContext.Current.Response.End();
            }
        }
    }
}