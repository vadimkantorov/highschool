<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EditContestForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Редактировать контест
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using(Html.BeginForm<ContestController>(x => x.Update(null))) {%>
    <%=Html.HiddenFor(x => x.ContestId) %>
	<table>
		<tr>
			<td>Название:</td>
			<td><%= Html.TextBoxFor(x => x.Name) %></td>
		</tr>
		<tr>
			<td>Начало:</td>
			<td><%= Html.TextBoxFor(x => x.Beginning) %></td>
		</tr>
		<tr>
			<td>Конец:</td>
			<td><%= Html.TextBoxFor(x => x.Ending) %></td>
		</tr>
		<tr>
			<td>Тип контеста:</td>
			<td><%= Html.DropDownListFor(x => x.ContestTypes.SelectedValue, Model.ContestTypes)%></td>
		</tr>
		<tr>
			<td>Публичный:</td>
			<td><%= Html.CheckBoxFor(x => x.IsPublic) %></td>
		</tr>
	</table>
	<%=Html.SubmitButton(null, "Обновить") %>
    <% } %>

	<% Html.RenderAction<ContestController>(x => x.ChangeAdministration(Model.ContestId)); %>


	<h2><%=Html.ActionLink<ParticipationApplicationController>(x => x.Index(Model.ContestId), "Заявки на участие") %></h2>
	<h2>Задачи:</h2>
	<h3><%=Html.ActionLink<ProblemController>(x => x.New(Model.ContestId),"Добавить") %></h3>
    <table>
		<tr>
			<th>Id</th>
			<th>Короткое название</th>
			<th>Название</th>
			<th>Редактировать</th>
		</tr>
		<%foreach (var problem in Model.Problems)
			{ %>
			<tr>
				<td><%=problem.Id %></td>
				<td><%=problem.ShortName %></td>
				<td><%=Html.ActionLink<ProblemController>(x => x.View(problem.Id), problem.Statement.Name)%></td>
				<td><%=Html.ActionLink<ProblemController>(x => x.Edit(problem.Id), "Редактировать") %></td>
			</tr>
		<%} %>
    </table>

</asp:Content>
