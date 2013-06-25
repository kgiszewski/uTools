using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using umbraco.cms.businesslogic.datatype;
using System.Web.UI.HtmlControls;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;
using umbraco.cms.businesslogic.propertytype;
using umbraco.cms.businesslogic.media;
using System.Xml;
using System.Xml.XPath;

namespace uTools
{
    public partial class DataTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();

            HtmlGenericControl table = new HtmlGenericControl("table");
            resultsWrapper.Controls.Add(table);
            HtmlGenericControl th;
            HtmlGenericControl tr;
            HtmlGenericControl td;

            HtmlGenericControl thead = new HtmlGenericControl("thead");
            table.Controls.Add(thead);

            tr = new HtmlGenericControl("tr");
            thead.Controls.Add(tr);

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>ID</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Instance Name</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Base Data Type</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>DocTypes Using</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>MediaTypes Using</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Edit</a>";

            th = new HtmlGenericControl("th");
            tr.Controls.Add(th);
            th.InnerHtml = "<a href='#'>Prevalues (Read-Only)</a>";
           
            HtmlGenericControl tbody = new HtmlGenericControl("tbody");
            table.Controls.Add(tbody);

            foreach (DataTypeDefinition dtd in DataTypeDefinition.GetAll())
            {

                tr = new HtmlGenericControl("tr");
                tbody.Controls.Add(tr);

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = dtd.Id.ToString();

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = dtd.Text;

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = dtd.DataType.DataTypeName;
              
                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a href='#'></a>";

                HtmlGenericControl docTypeDiv = new HtmlGenericControl("div");
                docTypeDiv.Attributes["class"] = "docTypeDiv";
                td.Controls.Add(docTypeDiv);

                docTypeDiv.Controls.Add(getDocTypeList(dtd.Id));

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a href='#'></a>";

                HtmlGenericControl mediaTypeDiv = new HtmlGenericControl("div");
                mediaTypeDiv.Attributes["class"] = "mediaTypeDiv";
                td.Controls.Add(mediaTypeDiv);
                mediaTypeDiv.Controls.Add(getMediaTypeList(dtd.Id));

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                td.InnerHtml = "<a class='editDataType' href='#' rel='" + dtd.Id + "'>Edit</a>";

                td = new HtmlGenericControl("td");
                tr.Controls.Add(td);
                try
                {
                    XPathNodeIterator xn= umbraco.library.GetPreValues(dtd.Id);
                    xn.MoveNext();
                    td.InnerHtml = "<textarea class='prevalues'>" + xn.Current.InnerXml + "</textarea>";
                }
                catch (Exception e2) { }
              }
        }

        public HtmlGenericControl getDocTypeList(int dtdID)
        {

            HtmlGenericControl ul = new HtmlGenericControl("ul");
            HtmlGenericControl li;

            List<string> aliases = new List<string>() { };

            foreach (DocumentType docType in DocumentType.GetAllAsList())
            {
                foreach (PropertyType prop in docType.PropertyTypes)
                {
                    if (prop.DataTypeDefinition.Id == dtdID && !aliases.Contains(docType.Alias))
                    {
                        li = new HtmlGenericControl("li");
                        ul.Controls.Add(li);
                        li.InnerHtml = docType.Alias;
                        aliases.Add(docType.Alias);
                    }
                }
            }

            return ul;
        }

        public HtmlGenericControl getMediaTypeList(int dtdID)
        {

            HtmlGenericControl ul=new HtmlGenericControl("ul");
            HtmlGenericControl li;

            List<string> aliases = new List<string>() { };

            foreach (MediaType mediaType in MediaType.GetAllAsList())
            {
                foreach (PropertyType prop in mediaType.PropertyTypes)
                {
                    if (prop.DataTypeDefinition.Id == dtdID && !aliases.Contains(mediaType.Alias))
                    {
                        li = new HtmlGenericControl("li");
                        ul.Controls.Add(li);
                        li.InnerHtml = mediaType.Alias;
                        aliases.Add(mediaType.Alias);
                    }
                }
            }

            return ul;
        }
    }
}