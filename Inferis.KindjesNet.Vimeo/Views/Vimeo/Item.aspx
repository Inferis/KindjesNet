<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<VimeoVideo>" %>

<%@ Import Namespace="Inferis.KindjesNet.Vimeo.Models" %>
<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Vimeo
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <object width="500" height="275">
        <param name="allowfullscreen" value="true" />
        <param name="allowscriptaccess" value="always" />
        <param name="movie" value="http://vimeo.com/moogaloop.swf?clip_id=<%= Model.VimeoId %>&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=0&amp;color=ffb300&amp;fullscreen=1" />
        <embed src="http://vimeo.com/moogaloop.swf?clip_id=<%= Model.VimeoId %>&amp;server=vimeo.com&amp;show_title=1&amp;show_byline=1&amp;show_portrait=0&amp;color=ffb300&amp;fullscreen=1"
            type="application/x-shockwave-flash" allowfullscreen="true" allowscriptaccess="always"
            width="500" height="275"></embed></object>
</asp:Content>
