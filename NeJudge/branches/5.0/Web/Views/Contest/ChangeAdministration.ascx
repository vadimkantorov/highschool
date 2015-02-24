<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Web.ViewModels.ChangeContestAdministrationForm>" %>


<% using(Html.BeginForm<ContestController>(x => x.ChangeAdministration(null))) {%>
	<%=Html.HiddenFor(x => x.ContestId) %>
	 <table>
		<tr>
			<td>Владелец:</td>
			<td><%=Html.DropDownListFor(x => Model.Owners.SelectedValue, Model.Owners) %></td>
		</tr>
		<tr>
			<td>Судья:</td>
			<td><%=Html.DropDownListFor(x => Model.Judges.SelectedValue, Model.Judges) %></td>
		</tr>
	 </table>
	 <%=Html.SubmitButton(null, "Поменять") %>
	 <%} %>