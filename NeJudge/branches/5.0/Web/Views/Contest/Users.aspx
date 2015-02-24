<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Model.User>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Список пользователей
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Список пользователей</h2>
	<ul>
		<%foreach(var user in Model){ %>
		<li><%=user.DisplayName %></li>
		<%} %>
	</ul>
</asp:Content>
