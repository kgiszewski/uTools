using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.BusinessLogic;

namespace uTools
{
    public partial class Log : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();

            DropDownList ddl = new DropDownList();
            ddl.CssClass = "logType";

            //log types
            ddl.Items.Add(new ListItem("All",""));
            foreach (var logType in Enum.GetValues(typeof(LogTypes)).Cast<LogTypes>())
            {
                ddl.Items.Add(new ListItem(logType.ToString(), Convert.ToInt32(logType).ToString()));
            }
            logTypes.Controls.Add(ddl);


            ddl = new DropDownList();
            ddl.Items.Add(new ListItem("All", ""));
            ddl.CssClass = "userName";

            foreach (User thisUser in umbraco.BusinessLogic.User.getAll())
            {
                ddl.Items.Add(new ListItem(thisUser.LoginName, thisUser.Id.ToString()));
            }
            userName.Controls.Add(ddl);
        }
    }
}