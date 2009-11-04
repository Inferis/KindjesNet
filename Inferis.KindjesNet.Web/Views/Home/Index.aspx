<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<IFeedItem>>" %>

<%@ Import Namespace="Inferis.KindjesNet.Core.Utils" %>
<%@ Import Namespace="Inferis.KindjesNet.Core.Models" %>
<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>
<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <% 
        
        IFeedItem last = null;
        var count = 0;
        foreach (var item in Model) {
            if (last != null && item.Provider != last.Provider) {
                if (count > 2) {
                    Response.Write("<p>nog " + (count-2) + " gelijkaardige berichten</p>");
                }

                Response.Write(item.GenerateHtml());
                count = 0;
            }
            else {
                if (count < 2) {
                    Response.Write(item.GenerateHtml());
                }
                count++;
            }

            last = item;
        }
    %>
</asp:Content>
