<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Import Namespace="Model"%>
<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<User>" %>

<div>
<% if(Model == null) {%>
	<%=Html.ActionLink<LoginController>(x => x.Index(), "Войти") %>
<%} else {%>
	<strong><%=Model.DisplayName %></strong>
	<%using(Html.BeginForm<LoginController>(x => x.LogOut())) {%>
		<input type="submit" value="Выйти" />
<%} } %>
</div>