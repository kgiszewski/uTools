using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Text;

namespace uTools
{
    public partial class XpathCache : System.Web.UI.Page
    {
        private string filename = "~/App_Data/umbraco.config"; //path to the config file
        private XmlDocument config = new XmlDocument();

        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsWebService.Authorize();

            string xpath = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["xpath"]);
            config.Load(HttpContext.Current.Server.MapPath(filename));

            if (xpath != "")
            {

                XmlDocument resultDocument = new XmlDocument();
                try
                {
                    XmlNodeList selection = config.SelectNodes(xpath);

                    StringBuilder sb = new StringBuilder();

                    sb.Append("<uTools>");
                    foreach (XmlNode xn in selection)
                    {
                        sb.Append(xn.OuterXml);
                    }
                    sb.Append("</uTools>");

                    resultDocument.LoadXml(sb.ToString());                    
                }
                catch (Exception e2) {
                    resultDocument.LoadXml("<uTools><noResults/></uTools>");
                }

                HttpContext.Current.Response.Clear();
                HttpContext.Current.Response.ContentType = "text/xml";
                HttpContext.Current.Response.ContentEncoding = Encoding.UTF8;
                resultDocument.Save(HttpContext.Current.Response.Output);
                HttpContext.Current.Response.End();
            }
        }
    }
}