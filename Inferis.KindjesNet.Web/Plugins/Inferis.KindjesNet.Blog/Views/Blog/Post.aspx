<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inferis.KindjesNet.Blog.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	<%= Html.Encode(Model.Title) %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%= Html.Encode(Model.Title) %></h2>

    <p>Door <%= Model.Author.Nick %></p>
    <div class="postbody">
        <%= Model.LegacyId.GetValueOrDefault(0) > 0 ? Model.Body.Replace("<center>", "<div align='center'>").Replace("</center>", "</div>") : Html.Encode(Model.Body)%>
    </div>

</asp:Content>
