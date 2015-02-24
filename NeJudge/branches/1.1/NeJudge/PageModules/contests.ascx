<%@ Control Language="c#" AutoEventWireup="false" Codebehind="contests.ascx.cs" Inherits="Ne.Judge.contests" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:DataGrid CssClass="grid" id="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="5"
	Width="100%" BorderWidth="0px" EnableViewState="False">
	<ItemStyle CssClass="grid_first"></ItemStyle>
	<HeaderStyle CssClass="grid_head"></HeaderStyle>
	<Columns>
		<asp:BoundColumn DataField="TID" HeaderText="ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Name" HeaderText="Название"></asp:BoundColumn>
		<asp:BoundColumn DataField="Monitor" HeaderText="Монитор"></asp:BoundColumn>
		<asp:BoundColumn DataField="Beginning" HeaderText="Дата начала"></asp:BoundColumn>
		<asp:BoundColumn DataField="Ending" HeaderText="Дата конца"></asp:BoundColumn>
		<asp:BoundColumn DataField="Status" HeaderText="Статус"></asp:BoundColumn>
	</Columns>
</asp:DataGrid>
