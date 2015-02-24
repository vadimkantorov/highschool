<%@ Import Namespace="Microsoft.Web.Mvc" %>
<%@ Import Namespace="Web.Controllers" %>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<IEnumerable<SelectListItem>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Массовое создание пользователей
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%using (Html.BeginForm<UserController>(x => x.MassCreate(null, null))) {%>
	Вводите по имени и фамилии на строку. Пустые строки учитываться не будут.
	<br />Если выберете контест, будут созданы и одобрены заявки на участие.
	<br /><br /><%=Html.DropDownList("ContestId", Model,"---Выберите контест---") %>
	<br /><br /><%=Html.TextArea("FamilyNames", "", 20, 30, new { })%>
	<br /><%=Html.SubmitButton(null, "Создать") %>
	<%} %>

</asp:Content>
