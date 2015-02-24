<%@ Register TagPrefix="uc1" TagName="selectcontest" Src="../UC/selectcontest.ascx" %>
<%@ Register TagPrefix="uc1" TagName="submfilter" Src="../UC/SubmissionsFilter.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="status.ascx.cs" Inherits="Ne.Judge.status" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<uc1:submfilter id="filter" runat="server" Url="status.aspx"></uc1:submfilter>
<!-- <uc1:selectcontest id="selcon" Future="true" Now="true" Old="true" Address="status.aspx" runat="server"></uc1:selectcontest> -->
<asp:datagrid id="statusGrid" runat="server" CellPadding="5" CssClass="grid" AllowCustomPaging="True"
	AutoGenerateColumns="False" BorderWidth="0px" Width="100%" AllowPaging="True">
	<HeaderStyle CssClass="grid_head"></HeaderStyle>
	<Columns>
		<asp:BoundColumn DataField="ID" HeaderText="ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Время" HeaderText="Время"></asp:BoundColumn>
		<asp:BoundColumn DataField="Команда" HeaderText="Команда"></asp:BoundColumn>
		<asp:BoundColumn DataField="Задача" HeaderText="Задача"></asp:BoundColumn>
		<asp:BoundColumn DataField="Язык" HeaderText="Язык"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="Статус">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Статус") %>'>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Статус") %>'>
				</asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="Тест №" HeaderText="Тест №"></asp:BoundColumn>
		<asp:BoundColumn DataField="Время работы" HeaderText="Время работы"></asp:BoundColumn>
		<asp:BoundColumn DataField="Выделено памяти" HeaderText="Выделено памяти"></asp:BoundColumn>
	</Columns>
	<PagerStyle NextPageText="Вперёд" PrevPageText="Назад" BorderStyle="None"></PagerStyle>
</asp:datagrid>
