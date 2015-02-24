<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Model"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Message>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Сообщение #<%=Model.Id %>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

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
			<td>Тип:</td>
			<td><%=Model.GetType().Name %></td>
		</tr>
		<%if(Model is Question) {%>
		<tr>
			<td>Ответ:</td>
			<td><%= Model.Next != null ? Html.ActionLink<MessageController>(x => x.View(Model.Next.Id), Model.Next.Subject).ToString() : "пока нет" %></td>
		</tr>
		<% }%>
		<tr>
			<td>Тема:</td>
			<td><%=Model.Subject %></td>
		</tr>
		<tr>
			<td>Текст:</td>
			<td><%=Model.Text %></td>
		</tr>
    </table>

</asp:Content>
