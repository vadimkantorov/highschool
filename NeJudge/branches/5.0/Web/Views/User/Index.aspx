<%@ Import Namespace="Model"%>
<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<User>>" %>



<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Пользователи
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%=Html.ActionLink<UserController>(x => x.MassNew(), "Создать массово") %></h2>
	<table>
		<tr>
			<th>Id</th>
			<th>Имя</th>
			<th>Логин</th>
		</tr>
    <%
    	foreach (var user in Model)
    	{%>
    	<tr>
    		<td><%= user.Id %></td>
    		<td><%= user.DisplayName %></td>
    		<td><%= user.UserName %></td>
    	</tr>
    	<%} %>
    </table>

</asp:Content>
