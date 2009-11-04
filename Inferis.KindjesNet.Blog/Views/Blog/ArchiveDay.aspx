<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Inferis.KindjesNet.Blog.Models.Controllers.ArchiveModel>" %>
<%@ Import Namespace="System.Linq"%>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	ArchiveDay
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Berichten op <%= Model.Day %>/<%= Model.Month %>/<%= Model.Year %></h2>
    
    <p><%= Model.Posts.Count() %> berichten:</p>
    <ul>
    <%
    foreach (var post in Model.Posts) {
        %>
        <li><a href="blog/<%= Html.Encode(post.Slug) %>"><%= Html.Encode(post.Title) %></a></li>
        
        <%    
    } 
    %>
    </ul>

</asp:Content>
