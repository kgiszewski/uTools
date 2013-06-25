using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.template;
using System.Web.UI.HtmlControls;
using umbraco.cms.businesslogic.macro;

using umbraco.DataLayer;


namespace uTools
{
    public partial class Macro : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();

            umbraco.cms.businesslogic.macro.Macro[] allMacros = umbraco.cms.businesslogic.macro.Macro.GetAll();
            
            HtmlGenericControl table, thead, tr, th, td, ul, li;

            table = new HtmlGenericControl("table");
            resultsWrapper.Controls.Add(table);

            thead = new HtmlGenericControl("thead");
            table.Controls.Add(thead);

            tr = new HtmlGenericControl("tr");
            thead.Controls.Add(tr);

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>ID</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Name</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Alias</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Cache By Page</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Cache Personalized</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Cache Refresh</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>UC Assembly</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Scripting File</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>XSLT File</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Properties</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Edit</a>";

            foreach (umbraco.cms.businesslogic.macro.Macro thisMacro in allMacros)
            {
                tr = new HtmlGenericControl("tr");
                table.Controls.Add(tr);

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.Id.ToString();

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.Name;
                
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.Alias;
                
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.CacheByPage.ToString();
                
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.CachePersonalized.ToString();

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.RefreshRate.ToString();

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisMacro.Assembly;

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a rel='" + thisMacro.ScriptingFile + "' class='editMacroScript'>"+thisMacro.ScriptingFile+"</a>";
                //td.InnerHtml = thisMacro.ScriptingFile;
                
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a rel='" + thisMacro.Xslt + "' class='editMacroXslt'>" + thisMacro.Xslt + "</a>";
                //td.InnerHtml = thisMacro.Xslt;

                ul = new HtmlGenericControl("ul");

                foreach (MacroProperty property in thisMacro.Properties)
                {
                    li = new HtmlGenericControl("li");
                    ul.Controls.Add(li);
                    li.InnerHtml = property.Alias;
                }

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.Controls.Add(ul);

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a rel='" + thisMacro.Id + "' class='editMacro'>Edit</a>";
            }
        }

        public static ISqlHelper SqlHelper
        {
            get
            {
                return umbraco.BusinessLogic.Application.SqlHelper;
            }
        }
    }
}