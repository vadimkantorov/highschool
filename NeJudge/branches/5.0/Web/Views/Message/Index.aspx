<%@ Import Namespace="Web.Controllers"%>
<%@ Import Namespace="Model"%>
<%@ Import Namespace="Web.ViewModels"%>
<%@ Import Namespace="Microsoft.Web.Mvc"%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MessagesForm>" %>


<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Сообщения
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%=Html.ActionLink<MessageController>(x => x.NewClarification(Model.Contest.Id),"Сделать исправление")%><br />
    <%=Html.ActionLink<MessageController>(x => x.NewAnnouncement(Model.Contest.Id),"Сделать объявление")%><br />
    <%=Html.ActionLink<MessageController>(x => x.NewQuestion(Model.Contest.Id),"Задать вопрос")%><br />
    <table>
		<tr>
			<th>Id</th>
			<th>Время</th>
			<th>Автор</th>
			<th>Тема</th>
			<th>Текст</th>
			<th>Тип</th>
			<th>Ответить</th>
		</tr>
		<%foreach (var msg in Model.Messages)
		{%>
		<%
			if (!msg.IsRead)
			{%>
				<tr style="background-color:gray">
			<%}
			else
			{%>
				<tr>
			<%} %>
				<td><%=msg.Message.Id%></td>
				<td><%=msg.Message.SentAt %></td>
				<td><%=msg.Message.Author.DisplayName %></td>
				<td><%=msg.Message.Subject %></td>
				<td><%=Html.ActionLink<MessageController>(x => x.View(msg.Message.Id), msg.Message.Text.Length > 10 ? msg.Message.Text.Substring(0, 10) + "..." : msg.Message.Text)%></td>
				<td><%=msg.Message.GetType().Name %></td>
				<%if (msg.Message is Question && msg.Message.Next == null)
				{%>
					<td><%=Html.ActionLink<MessageController>(x => x.NewAnswer(msg.Message.Id), "Ответить")%></td>
				<%} %>
			</tr>
		<%} %>
    </table>

</asp:Content>
