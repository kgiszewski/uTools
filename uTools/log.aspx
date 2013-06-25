<%@ Page Language="C#"  MasterPageFile="/umbraco/masterpages/umbracoPage.Master" AutoEventWireup="true" CodeBehind="log.aspx.cs" Inherits="uTools.Log" %>
<%@ Register Namespace="umbraco.uicontrols" Assembly="controls" TagPrefix="umb"%>
<%@ Register TagPrefix="umb" Namespace="ClientDependency.Core.Controls" Assembly="ClientDependency.Core" %>

<asp:Content ID="Content" ContentPlaceHolderID="body" runat="server">
    <umb:JsInclude ID="JsInclude1" runat="server" FilePath="/plugins/uTools/js/common.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude2" runat="server" FilePath="/plugins/uTools/js/tablesorter.js" PathNameAlias="UmbracoRoot" />
    <umb:JsInclude ID="JsInclude3" runat="server" FilePath="/plugins/uTools/js/log.js" PathNameAlias="UmbracoRoot" />
    <umb:CssInclude ID="CssInclude1" runat="server" FilePath="/plugins/uTools/css/log.css" PathNameAlias="UmbracoRoot" />
    <umb:CssInclude ID="CssInclude2" runat="server" FilePath="/plugins/uTools/css/common.css" PathNameAlias="UmbracoRoot" />
        
     <div id="controlsWrapper">
        <table>
          <tr>
            <td colspan="2"><img src="/umbraco/images/tray/utoolstray.png"/>uTools</td>
          </tr>
          <tr>
            <th>Log Types</th>
            <td id="logTypes" runat="server"></td>
            <td><label>Max Results</label><input id="maxResults" type="text" value="25"/></td>
            <td><input id="hardRefresh" type="button" value="Refresh"/></td>
          </tr>
          
          <tr>
            <th>User</th>
            <td id="userName" runat="server"></td>
            <td></td>
            <td><input id="clearResults" type="button" value="Clear Results"/></td>
          </tr>
          
        </table>
     </div>
      
    <div id="resultsWrapper">
      <table>
        <tr>
          <th>Date/Time</th>
          <th>Type</th>
          <th>User</th>
          <th>NodeID</th>
          <th>Comment</th>
        </tr>
      </table>
    </div>
        
    
</asp:Content>
