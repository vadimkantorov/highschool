<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<ViewProblemForm>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Просмотр задачи
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<h2><%=Model.Name %></h2>
	<h4><%=Model.ShortName %></h4>
    
	<table border="1">
		<tr>
			<td>Время (в секундах):</td>
			<td><%=Model.Limits.TimeInSeconds %></td>
		</tr>
		<tr>
			<td>Память (в мегабайтах):</td>
			<td><%=Model.Limits.MemoryInMegabytes %></td>
		</tr>
	</table>

	<%=Model.RenderedBody %>


</asp:Content>