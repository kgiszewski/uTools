<%@ Page Language="C#" MasterPageFile="/umbraco/masterpages/umbracoPage.Master" AutoEventWireup="true" CodeBehind="mediacache.aspx.cs" Inherits="uTools.MediaCache" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb"%>
<%@ Register TagPrefix="umb" Namespace="ClientDependency.Core.Controls" Assembly="ClientDependency.Core" %>

<asp:Content ID="Content" ContentPlaceHolderID="body" runat="server">
    <umb:JsInclude ID="JsInclude1" runat="server" FilePath="/plugins/uTools/js/common.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude2" runat="server" FilePath="/plugins/uTools/js/tablesorter.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude3" runat="server" FilePath="/plugins/uTools/js/mediacache.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude4" runat="server" FilePath="/plugins/uTools/js/nicexml.js" PathNameAlias="UmbracoRoot" />
    
    <umb:CssInclude ID="CssInclude1" runat="server" FilePath="/plugins/uTools/css/mediacache.css" PathNameAlias="UmbracoRoot" />
    <umb:CssInclude ID="CssInclude2" runat="server" FilePath="/plugins/uTools/css/common.css" PathNameAlias="UmbracoRoot" />
    <umb:CssInclude ID="CssInclude3" runat="server" FilePath="/plugins/uTools/css/nicexml.css" PathNameAlias="UmbracoRoot" />
        
    <umb:UmbracoPanel ID="Panel1" runat="server" hasMenu="false" Text="Media Cache Viewer">
        
        <div id="controlWrapper" runat="server">
          <label>Media ID</label>
          <input class='mediaID' type='text'/>
          <input class='search' type='button' value='Search'/>
        </div>
        
        <div id="resultsWrapper">
        
        </div>
        
    </umb:UmbracoPanel>
</asp:Content>
