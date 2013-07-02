using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;

using umbraco.cms.businesslogic.macro;
using umbraco.cms.businesslogic.template;
using umbraco.DataLayer;
using umbraco.BusinessLogic;

using HtmlAgilityPack;

namespace uTools
{
    public partial class Macro : System.Web.UI.Page    {

        private Dictionary<string, string> templates = new Dictionary<string, string>();

        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();

            umbraco.cms.businesslogic.macro.Macro[] allMacros = umbraco.cms.businesslogic.macro.Macro.GetAll();
            ReadInTemplates();
            
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
            th.InnerHtml = "<a href='#'>Template Usage</a>";

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

                //templates
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                ul = new HtmlGenericControl("ul");
                td.Controls.Add(ul);

                List<string> macroList = new List<string>();

                foreach(KeyValuePair<string, string> thisTemplate in templates){

                    try
                    {
                        HtmlDocument document = new HtmlDocument();
                        document.LoadHtml(thisTemplate.Value);

                        //umbraco.BusinessLogic.Log.Add(LogTypes.Custom, 0, "tHtml=>"+thisTemplate.Value);

                        foreach (HtmlNode macro in document.DocumentNode.SelectNodes("//div"))
                        {
                            //umbraco.BusinessLogic.Log.Add(LogTypes.Custom, 0, "macro found=>" +macro.OuterHtml);

                            HtmlAttribute attribute = macro.Attributes["Alias"];

                            if (attribute != null && attribute.Value == thisMacro.Alias && !macroList.Contains(thisTemplate.Key))
                            {
                                li = new HtmlGenericControl("li");
                                ul.Controls.Add(li);
                                li.InnerHtml = thisTemplate.Key;

                                macroList.Add(thisTemplate.Key);
                            }
                        }
                    }
                    catch {
                                            
                    }
                }

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a rel='" + thisMacro.Id + "' class='editMacro'>Edit</a>";
            }
        }

        private void ReadInTemplates()
        {
            string[] masterpages = Directory.GetFiles(Server.MapPath("~/masterpages"));
            foreach (string filename in masterpages)
            {
                StringBuilder sb = new StringBuilder();
                using (StreamReader sr = new StreamReader(filename))
                {
                    String line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        sb.AppendLine(line);
                    }
                }

                if(!templates.ContainsKey(Path.GetFileName(filename))){
                    templates.Add(Path.GetFileName(filename), sb.ToString().Replace("umbraco:Macro","div"));
                }
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