<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	NeJudge
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%=Html.ActionLink<ContestController>(x => x.Index(), "Контесты") %></h2>
    <h2><%=Html.ActionLink<UserController>(x => x.Index(), "Пользователи") %></h2>

</asp:Content>
