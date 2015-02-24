<%@ Import Namespace="Web.Controllers" %>
<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<int>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Подать заявку на участие в контесте
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    Подать заявку?
	
	<%using(Html.BeginForm<ParticipationApplicationController>(x => x.Create(Model))) {%>
		<%=Html.SubmitButton(null,"Подать") %>
	<%} %>

</asp:Content>
