using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.template;
using System.Web.UI.HtmlControls;

using umbraco.DataLayer;

namespace uTools
{
    public partial class Template : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();
            List<umbraco.cms.businesslogic.template.Template> allTemplates=umbraco.cms.businesslogic.template.Template.GetAllAsList();

            //get the template/doc counts
            Dictionary<int, int> counts = new Dictionary<int, int>();

            string sql = "SELECT templateId, count(*) as count FROM cmsDocument cd LEFT JOIN umbracoNode un on un.id=cd.nodeId WHERE id is not null and trashed=0 and newest=1 and templateId is not null group by templateId  order by templateId";

            IRecordsReader reader = SqlHelper.ExecuteReader(sql);

            while (reader.Read())
            {
                counts[reader.Get<int>("templateId")] = reader.Get<int>("count");
            }

            SqlHelper.Dispose();

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
            th.InnerHtml = "<a href='#'>GUID</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>File Path</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Node Path</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Master Template</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Children?</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Content Placeholders</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'># Docs Using</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Edit</a>";   

            foreach (umbraco.cms.businesslogic.template.Template thisTemplate in allTemplates)
            {
                tr = new HtmlGenericControl("tr");
                table.Controls.Add(tr);

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.Id.ToString();
                
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.Text;

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.Alias;

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.UniqueId.ToString();

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.TemplateFilePath;

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.Path;

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.MasterTemplate.ToString();

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = thisTemplate.HasChildren.ToString();
                
                ul=new HtmlGenericControl("ul");

                foreach (string thisPlaceHolderID in thisTemplate.contentPlaceholderIds())
                {
                    li = new HtmlGenericControl("li");
                    ul.Controls.Add(li);
                    li.InnerHtml = thisPlaceHolderID;
                }

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.Controls.Add(ul);

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);

                try
                {
                    td.InnerHtml = counts[thisTemplate.Id].ToString();
                }
                catch (Exception e2)
                {
                    td.InnerHtml = "0";
                }

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a rel='"+thisTemplate.Id+"' class='editTemplate'>Edit</a>";
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