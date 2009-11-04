<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<IEnumerable<string>>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Index</title>
</head>
<body>
    <div>
        Spider URLs:
        <ul>
        <% foreach (var url in Model) {
            %><li><%= url %></li><%
               
           } %>
        </ul>
    </div>
</body>
</html>
