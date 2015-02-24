<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NewProblemForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Новая задача
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<% using (Html.BeginForm<ProblemController>(x => x.Create(null), FormMethod.Post, new { enctype = "multipart/form-data" }))
	{ %>
	<%= Html.HiddenFor(x => x.ContestId) %>
	<table border="0">
		<tr>
			<td>Название:</td>
			<td><%=Html.TextBoxFor(x => x.Name) %></td>
		</tr>
		<tr>
			<td>Короткое имя:</td>
			<td><%=Html.TextBoxFor(x => x.ShortName) %></td>
		</tr>
	</table>
	<%=Html.SubmitButton(null, "Создать")%>
	<% } %>

</asp:Content>
