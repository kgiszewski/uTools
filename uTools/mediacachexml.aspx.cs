using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.XPath;
using System.Text;

namespace uTools
{
    public partial class MediaCacheXml : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();

            string mediaID = HttpUtility.UrlDecode(HttpContext.Current.Request.QueryString["mediaID"]);
            XmlDocument resultDocument = new XmlDocument();

            try
            {

                XPathNodeIterator xn = umbraco.library.GetMedia(Convert.ToInt32(mediaID), true);
                xn.MoveNext();

                resultDocument.LoadXml("<uTools>" + xn.Current.OuterXml + "</uTools>");
            }

            catch (Exception e2)
            {
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