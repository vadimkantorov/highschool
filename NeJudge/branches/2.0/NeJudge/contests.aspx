<%@ Page Language="c#" Inherits="Ne.Judge.ContestsPage" CodeFile="contests.aspx.cs" MasterPageFile="~/design.master"
	Title="Список соревнований" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<asp:GridView ID="contestsGV" runat="server" AutoGenerateColumns="False" CssClass="grid" Width="100%" CellPadding="5"
	BorderWidth="0px" GridLines="None" EmptyDataText="Нет доступных соревнований" EnableViewState="False">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>
			<asp:BoundField DataField="Name" HeaderText="Название" HtmlEncode="False" />
			<asp:BoundField DataField="Monitor" HeaderText="Монитор" HtmlEncode="False" />
			<asp:BoundField DataField="Beginning" HeaderText="Дата начала"/>
			<asp:BoundField DataField="Ending" HeaderText="Дата конца"/>
			<asp:BoundField DataField="Status" HeaderText="Статус"/>
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
		<RowStyle CssClass="gridLight" />
	</asp:GridView>
</asp:Content>
