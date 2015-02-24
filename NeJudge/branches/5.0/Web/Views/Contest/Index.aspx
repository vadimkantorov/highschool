<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Model"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<Contest>>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Список контестов
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

	<h2><%=Html.ActionLink<ContestController>(x => x.New(), "Создать") %></h2>
    <table>
		<tr>
			<th>Id</th>
			<th>Начало</th>
			<th>Конец</th>
			<th>Название</th>
			<th>Редактировать</th>
		</tr>
		<% foreach (var contest in Model) { %>
			<tr>
				<td><%=contest.Id %></td>
				<td><%=contest.Beginning %></td>
				<td><%=contest.Ending %></td>
				<td><%=Html.ActionLink<ContestController>(x => x.View(contest.Id), contest.Announcement.Name) %></td>
				<td><%=Html.ActionLink<ContestController>(x => x.Edit(contest.Id), "Редактировать") %></td>
			</tr>	
		<%} %>
    </table>

</asp:Content>
