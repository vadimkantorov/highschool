<%@ Page Language="c#" Inherits="Ne.Judge.ContestsPage" CodeFile="contests.aspx.cs" MasterPageFile="~/design.master"
	Title="������ ������������" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<asp:GridView ID="contestsGV" runat="server" AutoGenerateColumns="False" CssClass="grid" Width="100%" CellPadding="5"
	BorderWidth="0px" GridLines="None" EmptyDataText="��� ��������� ������������" EnableViewState="False">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>
			<asp:BoundField DataField="Name" HeaderText="��������" HtmlEncode="False" />
			<asp:BoundField DataField="Monitor" HeaderText="�������" HtmlEncode="False" />
			<asp:BoundField DataField="Beginning" HeaderText="���� ������"/>
			<asp:BoundField DataField="Ending" HeaderText="���� �����"/>
			<asp:BoundField DataField="Status" HeaderText="������"/>
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
		<RowStyle CssClass="gridLight" />
	</asp:GridView>
</asp:Content>
