<%@ Page Language="c#" Inherits="Ne.Judge.StatusPage" CodeFile="status.aspx.cs" MasterPageFile="~/design.master"
	Title="��������� �������" %>
<%@ Import Namespace="Ne.Helpers" %>
<%@ Register TagPrefix="nejudge" TagName="SubmissionsFilter" Src="~/UC/SubmissionsFilter.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<nejudge:SubmissionsFilter ID="filter" runat="server" />
	<asp:GridView ID="statusGV" runat="server" CellPadding="5" DataSourceID="data" BorderWidth="0px"
		Width="100%" AllowPaging="True" EmptyDataText="��� �������, ��������������� �������" 
		OnPageIndexChanging="statusGV_PageIndexChanging" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" />
			<asp:BoundField DataField="Time" HeaderText="�����" />
			<asp:BoundField DataField="Team" HeaderText="�������" />
			<asp:HyperLinkField DataTextField="ProblemShortName" 
				DataNavigateUrlFields="ProblemID" DataNavigateUrlFormatString="~/problem.aspx?problemID={0}"
				HeaderText="������" />
			 <asp:BoundField DataField="Language" HeaderText="����" />
			 <asp:BoundField DataField="Outcome" HeaderText="������" />
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
		<RowStyle CssClass="gridRow" />
		<AlternatingRowStyle CssClass="gridAlternateRow" />
		<PagerStyle BorderStyle="None" />
		<PagerSettings Mode="NextPrevious" NextPageText="������" PreviousPageText="�����" />
	</asp:GridView>
	<asp:ObjectDataSource ID="data" runat="server" TypeName="Ne.Helpers.StatusDataProvider" EnablePaging="true"
		 SelectMethod="GetRows" OnSelecting="data_Selecting">
		<SelectParameters>
			<asp:Parameter Name="filter" Type="Object" />
		</SelectParameters>
	</asp:ObjectDataSource>
</asp:Content>
