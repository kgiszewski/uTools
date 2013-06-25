using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using umbraco.cms.presentation.Trees;
using umbraco.BusinessLogic;


namespace uTools.Trees
{
    public class uToolsTree : BaseTree
    {
        public uToolsTree(string application) :
            base(application)
        {
        }

        protected override void CreateRootNode(ref XmlTreeNode rootNode)
        {
            rootNode.Icon = FolderIcon;
            rootNode.OpenIcon = FolderIconOpen;
            rootNode.NodeType = "uToolsRoot";
            rootNode.NodeID = "init";
        }

        public override void RenderJS(ref System.Text.StringBuilder Javascript)
        {
            Javascript.Append(
                @"
                    function openuTool(page){
                        parent.right.document.location.href='/umbraco/plugins/uTools/'+page;
                    }

                    function openuToolTab(page){
                        var popup=window.open(page,'_blank');
	                    if (window.focus) {popup.focus()}
	                    return false;
                    }
                ");
        }

        public override void Render(ref XmlTree tree)
        {
            
            XmlTreeNode xNode;

            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Log";
            xNode.Icon = "doc.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuToolTab('/umbraco/plugins/uTools/log.aspx')";
            tree.Add(xNode);

            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Data Types";
            xNode.Icon = "developerDatatype.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuTool('datatype.aspx')";
            tree.Add(xNode);
            
            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Document Types";
            xNode.Icon = "settingDatatype.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuTool('doctype.aspx')";
            tree.Add(xNode);

            //xNode = XmlTreeNode.Create(this);
            //xNode.Text = "Content";
            //xNode.Icon = "docPic.gif";
            //xNode.NodeType = "uTool";
            //xNode.Action = "javascript:openuTool('content.aspx')";
            //tree.Add(xNode);

            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Templates";
            xNode.Icon = "settingTemplate.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuTool('template.aspx')";
            tree.Add(xNode);

            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Macros";
            xNode.Icon = "developerMacro.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuTool('macro.aspx')";
            tree.Add(xNode);

            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Content Cache Viewer";
            xNode.Icon = "settingXML.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuTool('cache.aspx')";
            tree.Add(xNode);

            xNode = XmlTreeNode.Create(this);
            xNode.Text = "Media Cache Viewer";
            xNode.Icon = "mediaPhoto.gif";
            xNode.NodeType = "uTool";
            xNode.Action = "javascript:openuTool('mediacache.aspx')";
            tree.Add(xNode);
        }
    }
}