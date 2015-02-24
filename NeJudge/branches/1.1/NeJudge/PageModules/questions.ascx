<%@ Register TagPrefix="uc1" TagName="selectcontest" Src="../UC/selectcontest.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorMessage" Src="../UC/ErrorMessage.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="questions.ascx.cs" Inherits="Ne.Judge.questions" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<uc1:selectcontest id="selcon" EnableViewState="true" runat="server" Address="questions.aspx" Old="true"
	Now="true" Future="false"></uc1:selectcontest><uc1:errormessage id="ErrorMessage" EnableViewState="False" runat="server"></uc1:errormessage>
<asp:datagrid id="questionsGrid" runat="server" CellPadding="5" BorderWidth="0px" CssClass="grid"
	Width="100%" AutoGenerateColumns="False" EnableViewState="False">
	<ItemStyle CssClass="grid_second"></ItemStyle>
	<HeaderStyle CssClass="grid_head"></HeaderStyle>
	<Columns>
		<asp:BoundColumn DataField="QID" ReadOnly="True" HeaderText="ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="PID" ReadOnly="True" HeaderText="PID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Question" ReadOnly="True" HeaderText="Вопрос"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Ответ">
			<ItemTemplate>
				<%# DataBinder.Eval(Container, "DataItem.Answer") %>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox ID="answerTextBox" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Answer") %>'>
				</asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:TemplateColumn HeaderText="Быстрый ответ">
			<ItemTemplate>
				<asp:Button id="Yes" runat="server" Text="Yes" Visible="False"></asp:Button>
				<asp:Button id="No" runat="server" Text="No" Visible="False"></asp:Button>
				<asp:Button id="Nc" runat="server" Text="No comments" Visible="False"></asp:Button>
			</ItemTemplate>
		</asp:TemplateColumn>
		<asp:EditCommandColumn ButtonType="LinkButton" UpdateText="Обновить" CancelText="Отмена" EditText="Изменить"></asp:EditCommandColumn>
	</Columns>
</asp:datagrid>
