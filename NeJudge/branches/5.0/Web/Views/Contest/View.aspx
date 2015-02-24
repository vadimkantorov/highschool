<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Model"%>
<%@ Import Namespace="Web"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Contest>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Просмотр контеста
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h1><%=Model.Announcement.Name %></h1>
	<%=Html.ActionLink<ParticipationApplicationController>(x => x.New(Model.Id), "Подать заявку на участие") %> <br />
    <%=Html.ActionLink<ContestController>(x => x.Status(Model.Id), "Статус") %> <br />
	<%=Html.ActionLink<ContestController>(x => x.Users(Model.Id), "Пользователи") %> <br />
    <%=Html.ActionLink<ContestController>(x => x.Monitor(Model.Id), "Монитор") %> <br />
    <%=Html.ActionLink<MessageController>(x => x.Index(Model.Id), "Сообщения") %> <br />
    Задачи:
    <table>
		<tr>
			<th>Id</th>
			<th>Короткое название</th>
			<th>Название</th>
			<th>Проверка</th>
		</tr>
		<%foreach (var problem in Model.Problems)
			{ %>
			<tr>
				<td><%=problem.Id %></td>
				<td><%=problem.ShortName %></td>
				<td><%=Html.ActionLink<ProblemController>(x => x.View(problem.Id), problem.Statement.Name)%></td>
				<td><%=Html.ActionLink<SubmissionController>(x => x.New(problem.Id),"Послать") %></td>
			</tr>
		<%} %>
    </table>

</asp:Content>
