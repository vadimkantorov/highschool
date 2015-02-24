<%@ Page Language="c#" Inherits="Ne.Judge.QuestionsPage" CodeFile="questions.aspx.cs"
	MasterPageFile="~/design.master" Title="�������� ��������" %>

<%@ Register TagPrefix="nejudge" TagName="MessagesFilter" Src="~/UC/MessagesFilter.ascx" %>
<%@ Register TagPrefix="nejudge" TagName="ErrorMessage" Src="~/UC/ErrorMessage.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<nejudge:MessagesFilter ID="selmess" runat="server" />
	<asp:GridView ID="questionsGV" runat="server" AutoGenerateColumns="False" EmptyDataText="��� ���������, ��������������� �������"
		Width="100%" OnRowCancelingEdit="questionsGV_RowCancelingEdit" OnRowEditing="questionsGV_RowEditing" OnRowUpdating="questionsGV_RowUpdating" OnRowDataBound="questionsGV_RowDataBound" DataKeyNames="ID">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" ReadOnly="True" />
			<asp:TemplateField HeaderText="������">
				<ItemTemplate><%#Ne.Database.Classes.Problem.GetProblem(Convert.ToInt32(Eval("ProblemID"))).ShortName%></ItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="������������">
				<ItemTemplate><%#Ne.Database.Classes.User.GetUser(Eval("UserID").ToString()).Name %></ItemTemplate>
			</asp:TemplateField>
			<asp:BoundField DataField="ContestantMessage" HeaderText="������" ReadOnly="True" />
			<asp:TemplateField HeaderText="�����">
				<ItemTemplate><%#Eval("JuryMessage") %></ItemTemplate>
				<EditItemTemplate><asp:TextBox ID="answerTB" runat="server" /></EditItemTemplate>
			</asp:TemplateField>
			<asp:TemplateField HeaderText="������� �����" Visible="False">
				<EditItemTemplate>
					<button id="Yes" runat="server">��</button>
					<button id="No" runat="server">���</button>
					<button id="Nc" runat="server">��� ������������</button>
				</EditItemTemplate>
			</asp:TemplateField>
			<asp:CommandField ShowEditButton="True" CancelText="��������" EditText="��������" UpdateText="���������" />
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
	</asp:GridView>
</asp:Content>
