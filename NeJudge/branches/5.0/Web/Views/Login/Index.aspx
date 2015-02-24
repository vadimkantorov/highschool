<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<LoginForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Вход
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%using (Html.BeginForm<LoginController>(x => x.LogIn(null, null)))
	  {%>
	  <span style="color:red"><%=Model.Error %></span>
    <table>
		<tr>
			<td>Логин:</td>
			<td><%= Html.TextBoxFor(x => x.UserName)%></td>
		</tr>
		<tr>
			<td>Пароль:</td>
			<td><%=Html.TextBoxFor(x => x.Password)%></td>
		</tr>
    </table>
    <%=Html.SubmitButton(null, "Вход") %>
    <%} %>

</asp:Content>
