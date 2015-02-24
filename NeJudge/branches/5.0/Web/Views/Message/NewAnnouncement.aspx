<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SelectListItem>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Сделать объявление
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Объявление по <%=Model.First(x => x.Selected).Text %></h2>
    <% using(Html.BeginForm<MessageController>(x => x.CreateAnnouncement(0,null, null))) { %>
	<table>
		<tr>
			<td>Контест:</td>
			<td><%=Html.DropDownList("Id", Model) %></td>
		</tr>
		<tr>
			<td>Тема:</td>
			<td><%=Html.TextBox("Subject", "") %></td>
		</tr>
		<tr>
			<td>Текст:</td>
			<td><%=Html.TextArea("Text", "", 10, 15, new {}) %></td>
		</tr>
	</table>
	<%=Html.SubmitButton(null,"Отправить") %>
	<% } %>

</asp:Content>
