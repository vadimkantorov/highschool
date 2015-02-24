<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Model.Question>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Ответ
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2>Ответить на <%=Model.Subject %></h2>
    <% using(Html.BeginForm<MessageController>(x => x.CreateAnswer(0,null))) { %>
	<%=Html.HiddenFor(x => Model.Id) %>
	<table>
		<tr>
			<td>Id:</td>
			<td><%=Model.Id %></td>
		</tr>
		<tr>
			<td>От:</td>
			<td><%=Model.Author.DisplayName %></td>
		</tr>
		<tr>
			<td>Время отправления:</td>
			<td><%=Model.SentAt %></td>
		</tr>
		<tr>
			<td>Тема:</td>
			<td><%=Model.Subject %></td>
		</tr>
		<tr>
			<td>Текст:</td>
			<td><%=Model.Text %></td>
		</tr>
		<tr>
			<td>Ответ:</td>
			<td><%=Html.TextArea("Answer", "", 10, 15, new {}) %></td>
		</tr>
    </table>
	<%=Html.SubmitButton(null,"Отправить") %>
	<% } %>
</asp:Content>
