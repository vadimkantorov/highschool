<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NewSolutionForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Новое решение
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<% using(Html.BeginForm<SubmissionController>(x => x.Create(null))) { %>
	<table>
		<tr>
			<td>Номер задачи:</td>
			<td><%=Html.TextBoxFor(x => x.ProblemId,Model.ProblemId) %></td>
		</tr>
		<tr>
			<td>Язык:</td>
			<td><%=Html.DropDownListFor(x => x.Languages.SelectedValue, Model.Languages) %></td>
		</tr>
		<tr>
			<td>Исходный код:</td>
			<td><%=Html.TextAreaFor(x => x.Code, 20, 50, new {}) %></td>
		</tr>
	</table>
	<%=Html.SubmitButton(null, "Отправить") %>
	<% } %>
</asp:Content>
