﻿<%@ Page Language="C#" MasterPageFile="/umbraco/masterpages/umbracoPage.Master" AutoEventWireup="true" CodeBehind="doctype.aspx.cs" Inherits="uTools.DocTypes" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb"%>
<%@ Register TagPrefix="umb" Namespace="ClientDependency.Core.Controls" Assembly="ClientDependency.Core" %>

<asp:Content ID="Content" ContentPlaceHolderID="body" runat="server">
    <umb:JsInclude ID="JsInclude1" runat="server" FilePath="/plugins/uTools/js/common.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude2" runat="server" FilePath="/plugins/uTools/js/tablesorter.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude3" runat="server" FilePath="/plugins/uTools/js/doctypes.js" PathNameAlias="UmbracoRoot" />
    <umb:CssInclude ID="CssInclude1" runat="server" FilePath="/plugins/uTools/css/doctypes.css" PathNameAlias="UmbracoRoot" />
    <umb:CssInclude ID="CssInclude2" runat="server" FilePath="/plugins/uTools/css/common.css" PathNameAlias="UmbracoRoot" />
        
    <umb:UmbracoPanel ID="Panel1" runat="server" hasMenu="false" Text="Document Types">
        
        <div id="resultsWrapper" runat="server">
    
        </div>
        
    </umb:UmbracoPanel>
</asp:Content>
