<%@ Control Language="c#" AutoEventWireup="false" Codebehind="contests.ascx.cs" Inherits="Ne.Judge.contests" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<asp:DataGrid CssClass="grid" id="DataGrid1" runat="server" AutoGenerateColumns="False" CellPadding="5"
	Width="100%" BorderWidth="0px" EnableViewState="False">
	<ItemStyle CssClass="grid_first"></ItemStyle>
	<HeaderStyle CssClass="grid_head"></HeaderStyle>
	<Columns>
		<asp:BoundColumn DataField="TID" HeaderText="ID"></asp:BoundColumn>
		<asp:BoundColumn DataField="Name" HeaderText="��������"></asp:BoundColumn>
		<asp:BoundColumn DataField="Monitor" HeaderText="�������"></asp:BoundColumn>
		<asp:BoundColumn DataField="Beginning" HeaderText="���� ������"></asp:BoundColumn>
		<asp:BoundColumn DataField="Ending" HeaderText="���� �����"></asp:BoundColumn>
		<asp:BoundColumn DataField="Status" HeaderText="������"></asp:BoundColumn>
	</Columns>
</asp:DataGrid>
