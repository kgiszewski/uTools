using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using umbraco.BusinessLogic;
using umbraco.cms.businesslogic.web;

namespace uTools
{
    public partial class DocTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            uToolsCore.Authorize();

            List<DocumentType> docTypes=new List<DocumentType>();
                
            docTypes.AddRange(DocumentType.GetAllAsList());
            
            HtmlGenericControl table = new HtmlGenericControl("table");

            HtmlGenericControl thead = new HtmlGenericControl("thead");
            table.Controls.Add(thead);

            HtmlGenericControl htr = new HtmlGenericControl("tr");
            thead.Controls.Add(htr);

            HtmlGenericControl thumbTH = new HtmlGenericControl("th");
            thumbTH.InnerHtml = "<a href='#'>Thumbnail</a>";
            htr.Controls.Add(thumbTH);

            HtmlGenericControl iconTH = new HtmlGenericControl("th");
            iconTH.InnerHtml = "<a href='#'>Icon</a>";
            htr.Controls.Add(iconTH);

            HtmlGenericControl nameTH = new HtmlGenericControl("th");
            nameTH.InnerHtml = "<a href='#'>Name</a>";
            htr.Controls.Add(nameTH);

            HtmlGenericControl aliasTH = new HtmlGenericControl("th");
            aliasTH.InnerHtml = "<a href='#'>Alias</a>";
            htr.Controls.Add(aliasTH);

            HtmlGenericControl hasChildrenTH = new HtmlGenericControl("th");
            hasChildrenTH.InnerHtml = "<a href='#'>Children?</a>";
            htr.Controls.Add(hasChildrenTH);

            HtmlGenericControl numDocsTH = new HtmlGenericControl("th");
            numDocsTH.InnerHtml = "<a href='#'># Docs</a>";
            htr.Controls.Add(numDocsTH);

            HtmlGenericControl numDocsUnderTH = new HtmlGenericControl("th");
            numDocsUnderTH.InnerHtml = "<a href='#'># Docs Under</a>";
            htr.Controls.Add(numDocsUnderTH);

            HtmlGenericControl masterDocTypeTH = new HtmlGenericControl("th");
            masterDocTypeTH.InnerHtml = "<a href='#'>Master</a>";
            htr.Controls.Add(masterDocTypeTH);

            HtmlGenericControl safeTH = new HtmlGenericControl("th");
            safeTH.InnerHtml = "<a href='#'>Dependencies?</a>";
            htr.Controls.Add(safeTH);

            HtmlGenericControl templateTH = new HtmlGenericControl("th");
            templateTH.InnerHtml = "<a href='#'>Default Template</a>";
            htr.Controls.Add(templateTH);

            HtmlGenericControl editTH = new HtmlGenericControl("th");
            templateTH.InnerHtml = "<a href='#'>Edit</a>";
            htr.Controls.Add(editTH);


            HtmlGenericControl tbody = new HtmlGenericControl("tbody");
            table.Controls.Add(tbody);

            resultsWrapper.Controls.Add(table);

            foreach(DocumentType thisDocType in docTypes){
                if (thisDocType.IsTrashed) continue;

                HtmlGenericControl tr = new HtmlGenericControl("tr");
                tbody.Controls.Add(tr);

                HtmlGenericControl thumbnail = new HtmlGenericControl("td");
                thumbnail.InnerHtml = "<img title='Thumbnail' src='/umbraco/images/thumbnails/" + thisDocType.Thumbnail + "'/>";
                tr.Controls.Add(thumbnail);

                HtmlGenericControl icon = new HtmlGenericControl("td");
                icon.InnerHtml = "<img title='icon' src='/umbraco/images/umbraco/" + thisDocType.IconUrl + "'/>";
                tr.Controls.Add(icon);

                HtmlGenericControl name = new HtmlGenericControl("td");
                name.InnerHtml = thisDocType.Text;
                tr.Controls.Add(name);

                HtmlGenericControl alias = new HtmlGenericControl("td");
                alias.InnerHtml = thisDocType.Alias;
                tr.Controls.Add(alias);

                HtmlGenericControl hasChildren = new HtmlGenericControl("td");
                hasChildren.InnerHtml = (thisDocType.HasChildren)?"Yes":"No";
                tr.Controls.Add(hasChildren);
   
                HtmlGenericControl numDocs = new HtmlGenericControl("td");
                int num=Document.GetDocumentsOfDocumentType(thisDocType.Id).Where(x=>!x.IsTrashed).Count();
                numDocs.InnerHtml = num.ToString();
                tr.Controls.Add(numDocs);

                HtmlGenericControl numDocsUnder = new HtmlGenericControl("td");
                IEnumerable<Document> documents = Document.GetDocumentsOfDocumentType(thisDocType.Id).Where(x => !x.IsTrashed);

                int count=0;
                foreach (Document document in documents)
                {
                    count += countDocumentsUnder(document, 0);
                }

                numDocsUnder.InnerHtml = count.ToString();
                tr.Controls.Add(numDocsUnder);

                HtmlGenericControl masterDocType = new HtmlGenericControl("td");
                string masterAlias="None";
                try
                {
                    masterAlias=new DocumentType(thisDocType.MasterContentType).Alias;
                }
                catch (Exception e2){}
                masterDocType.InnerHtml = masterAlias;
                tr.Controls.Add(masterDocType);

                HtmlGenericControl safe = new HtmlGenericControl("td");
                safe.InnerHtml = (count==0&&num==0&&!thisDocType.HasChildren)?"No":"Yes";
                tr.Controls.Add(safe);

                HtmlGenericControl template = new HtmlGenericControl("td");
                template.InnerHtml = new umbraco.template(thisDocType.DefaultTemplate).TemplateAlias;
                tr.Controls.Add(template);

                HtmlGenericControl editDocType = new HtmlGenericControl("td");
                template.InnerHtml = "<a class='editDocType' href='#' rel='"+thisDocType.Id+"'>Edit</a>";
                tr.Controls.Add(editDocType);
            }
        }

        private int countDocumentsUnder(Document document, int count)
        {
            if (document.HasChildren)
            {
                count += document.Children.Count();
                foreach (Document childDocument in document.Children)
                {
                    return countDocumentsUnder(childDocument, count);
                }
            }
            else
            {
                return count++;
            }
            return 1;
        }
    }
}