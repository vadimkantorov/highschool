<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<NewContestForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Новый контест
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <% using(Html.BeginForm<ContestController>(x => x.Create(null))) {%>
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
			<td><%= Html.DropDownListFor(x => x.ContestTypes.SelectedValue, Model.ContestTypes) %></td>
		</tr>
	</table>
	<%= Html.SubmitButton("","Создать") %>
    <% } %>

</asp:Content>
