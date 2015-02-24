<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Model.AuthorizationResult>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	В авторизации отказано
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>В авторизации отказано</h2>
    <table>
		<tr>
			<td>Операция:</td>
			<td><%=Model.Operation.ToString() %></td>
		</tr>
		<tr>
			<td>Объяснение:</td>
			<td><%=Model.Explanation %></td>
		</tr>
    </table>

</asp:Content>
