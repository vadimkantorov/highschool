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
		<asp:BoundColumn DataField="�����" HeaderText="�����"></asp:BoundColumn>
		<asp:BoundColumn DataField="�������" HeaderText="�������"></asp:BoundColumn>
		<asp:BoundColumn DataField="������" HeaderText="������"></asp:BoundColumn>
		<asp:BoundColumn DataField="����" HeaderText="����"></asp:BoundColumn>
		<asp:TemplateColumn HeaderText="������">
			<ItemTemplate>
				<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.������") %>'>
				</asp:Label>
			</ItemTemplate>
			<EditItemTemplate>
				<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.������") %>'>
				</asp:TextBox>
			</EditItemTemplate>
		</asp:TemplateColumn>
		<asp:BoundColumn DataField="���� �" HeaderText="���� �"></asp:BoundColumn>
		<asp:BoundColumn DataField="����� ������" HeaderText="����� ������"></asp:BoundColumn>
		<asp:BoundColumn DataField="�������� ������" HeaderText="�������� ������"></asp:BoundColumn>
	</Columns>
	<PagerStyle NextPageText="�����" PrevPageText="�����" BorderStyle="None"></PagerStyle>
</asp:datagrid>
