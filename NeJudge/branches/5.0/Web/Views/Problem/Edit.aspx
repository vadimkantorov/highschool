<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<EditProblemForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Редактирование задачи
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using (Html.BeginForm<ProblemController>(x => x.Update(null), FormMethod.Post, new { enctype = "multipart/form-data" })) {%>
    
	<%= Html.HiddenFor(x => x.ProblemId) %>
	<table>
		<tr>
			<td>Название:</td>
			<td><%=Html.TextBoxFor(x => x.Name) %></td>
		</tr>

		<tr>
			<td>Короткое имя:</td>
			<td><%=Html.TextBoxFor(x => x.ShortName) %></td>
		</tr>
		
		<tr>
			<td>Ограничения</td>
			<td>
				<table border="1">
					<tr>
						<td>Время (в секундах):</td>
						<td><%=Html.TextBoxFor(x => x.Limits.TimeInSeconds) %></td>
					</tr>
					<tr>
						<td>Память (в мегабайтах):</td>
						<td><%=Html.TextBoxFor(x => x.Limits.MemoryInMegabytes) %></td>
					</tr>
				</table>
			</td>
		</tr>
		<tr>
			<td>Форматтер текста:</td>
			<td><%=Html.DropDownListFor(x => x.DocumentFormatters.SelectedValue, Model.DocumentFormatters) %></td>
		</tr>
		<tr>
			<td>Текст:</td>
			<td><%=Html.TextAreaFor(x => x.ProblemBody, 20, 50, new {}) %></td>
		</tr>
		<tr>
			<td>Язык чекера:</td>
			<td><%=Html.DropDownListFor(x => x.CheckerLanguages.SelectedValue, Model.CheckerLanguages) %></td>
		</tr>
		<tr>
			<td>Исходник чекера:</td>
			<td><%=Html.TextAreaFor(x => x.CheckerSource, 20, 50, new { })%></td>
		</tr>
		<tr>
			<td>Аргументы чекеру:</td>
			<td><%=Html.TextBoxFor(x => x.CheckerArguments) %></td>
		</tr>
		<tr>
			<td>Архив с тестами:</td>
			<td><input type="file" name="NewTestsArchive" /></td>
		</tr>
		<tr>
			<td>Вставить тесты:</td>
			<td><%=Html.TextBoxFor(x => x.InsertNewTestsAt) %></td>
		</tr>
	</table>
	<%=Html.SubmitButton(null, "Обновить") %>
	<% } %>
	Тесты:
	<table border="1">
		<tr>
			<th>Id</th>
			<th>Описание</th>
			<th>Вход</th>
			<th>Выход</th>
			<th>Удалить</th>
		</tr>
		<%foreach(var test in Model.Tests){ %>
			<tr>
				<td><%=test.Id %></td>
				<td><%=test.Description %></td>
				<td><%=Html.ActionLink<TestController>(x => x.ViewInput(test.Id), "Вход") %></td>
				<td><%=Html.ActionLink<TestController>(x => x.ViewOutput(test.Id), "Выход") %></td>
				<td>
					<%using(Html.BeginForm<TestController>(x => x.Delete(test.Id))) { %>
						<%=Html.SubmitButton(null, "Удалить") %>
					<%} %>
				</td>
			</tr>
		<%} %>
	</table>
	<h2>
	
	<%using(Html.BeginForm<ProblemController>(x => x.PackTestInfo(Model.ProblemId))) {%>
	<%=Html.SubmitButton(null,"Скачать все в .ZIP") %>
	<%} %>
</asp:Content>
