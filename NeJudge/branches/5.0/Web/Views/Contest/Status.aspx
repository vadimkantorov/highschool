<%@ Import Namespace="Web.Controllers" %>
<%@ Import namespace="Web.ViewModels" %>
<%@ Import namespace="Microsoft.Web.Mvc" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ContestStatusForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Новый контест
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <table style="width:100%">
		<tr>
			<th>ID</th>
			<th>Автор</th>
			<th>Контест</th>
			<th>Задача</th>
			<th>Время отправления</th>
			<th>Статус</th>
			<th>Вердикт</th>
			<th>Число</th>
		</tr>
		<% foreach (var s in Model.Submissions)
			{ 
		%>
			<tr>
				<td><%=Html.ActionLink<SubmissionController>(x => x.View(s.Id),s.Id.ToString()) %></td>
				<td><%=s.Author.DisplayName %></td>
				<td><%=Html.ActionLink<ContestController>(x => x.View(s.Problem.Contest.Id), s.Problem.Contest.Announcement.Name) %></td>
				<td><%=Html.ActionLink<ProblemController>(x => x.View(s.Problem.Id), s.Problem.ShortName) %></td>
				<td><%=s.SubmittedAt %></td>
				<td><%=s.TestingStatus %></td>
				<td><%=s.Result != null && s.Result.Verdict != null ? s.Result.Verdict : "" %></td>
				<td><%=s.Result != null && s.Result.Value != null ? s.Result.Value.ToString() : "" %></td>
			</tr>
		<%	} %>
	</table>

</asp:Content>
