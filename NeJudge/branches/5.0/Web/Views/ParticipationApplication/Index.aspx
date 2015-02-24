<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Web.ViewModels.EditParticipationApplicationsForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Заявки на участие
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Заявки на участие в контесте <%= Model.ContestName %></h2>
	<% using (Html.BeginForm<ParticipationApplicationController>(x => x.Update(null)))
	   {%>
	   <%=Html.HiddenFor(x => x.ContestId) %>
    <table>
		<tr>
			<th>Id</th>
			<th>Дата отправки</th>
			<th>Участник</th>
			<th>Одобрить</th>
		</tr>
		<%for(int i = 0; i < Model.Applications.Count; i++) {%>
		<tr>
			<td><%=Model.Applications[i].Id%> <%=Html.HiddenFor(x => Model.Applications[i].Id)%></td>
			<td><%=Model.Applications[i].SubmittedAt%></td>
			<td><%=Model.Applications[i].UserDisplayName%></td>
			<td><%=Html.CheckBoxFor(x => Model.Applications[i].IsApproved)%></td>
		</tr>
		<%} %>
	</table>
	<%=Html.SubmitButton(null,"Обновить") %>
	<%} %>

</asp:Content>
