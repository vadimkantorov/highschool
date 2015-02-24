<%@ Page Language="c#" Inherits="Ne.Judge.QuestionsPage" CodeFile="questions.aspx.cs"
	MasterPageFile="~/design.master" Title="Просмотр вопросов" %>

<%@ Register TagPrefix="nejudge" TagName="MessagesFilter" Src="~/UC/MessagesFilter.ascx" %>
<%@ Register TagPrefix="nejudge" TagName="ErrorMessage" Src="~/UC/ErrorMessage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<nejudge:MessagesFilter ID="selmess" runat="server" />
	<asp:GridView ID="questionsGV" runat="server" AutoGenerateColumns="False" EmptyDataText="Нет сообщений, соответствующих фильтру"
		Width="100%" OnRowCancelingEdit="questionsGV_RowCancelingEdit" OnRowEditing="questionsGV_RowEditing" OnRowUpdating="questionsGV_RowUpdating" OnRowDataBound="questionsGV_RowDataBound" DataKeyNames="ID">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
			<asp:TemplateField HeaderText="Задача">
				<ItemTemplate><%#Ne.Database.Classes.Problem.GetProblem(Convert.ToInt32(Eval("ProblemID"))).ShortName%></ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Пользователь">
				<ItemTemplate><%#Ne.Database.Classes.User.GetUser(Eval("UserID").ToString()).Name %></ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="ContestantMessage" HeaderText="Вопрос" ReadOnly="True" />
			<asp:TemplateField HeaderText="Ответ">
				<ItemTemplate><%#Eval("JuryMessage") %></ItemTemplate>
				<EditItemTemplate><asp:TextBox ID="answerTB" runat="server" /></EditItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="Быстрый ответ" Visible="False">
				<EditItemTemplate>
					<button id="Yes" runat="server">Да</button>
					<button id="No" runat="server">Нет</button>
					<button id="Nc" runat="server">Без комментариев</button>
				</EditItemTemplate>
			</asp:TemplateField>
			<asp:CommandField ShowEditButton="True" CancelText="Отменить" EditText="Изменить" UpdateText="Сохранить" />
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
	</asp:GridView>
</asp:Content>
