<%@ Page Language="c#" Inherits="Ne.Judge.StatusPage" CodeFile="status.aspx.cs" MasterPageFile="~/design.master"
	Title="Посланные решения" %>
<%@ Register TagPrefix="nejudge" TagName="SubmissionsFilter" Src="~/UC/SubmissionsFilter.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<nejudge:SubmissionsFilter ID="filter" runat="server" />
	<asp:GridView ID="statusGV" runat="server" CellPadding="5" DataSourceID="data" BorderWidth="0px"
		Width="100%" AllowPaging="True" EmptyDataText="Нет посылок, соответствующих фильтру" 
		OnDataBound="statusGV_DataBound" OnPageIndexChanging="statusGV_PageIndexChanging" OnRowDataBound="statusGV_RowDataBound">
		<HeaderStyle CssClass="gridHeader" />
		<PagerStyle BorderStyle="None" />
		<PagerSettings Mode="NextPrevious" NextPageText="Вперед" PreviousPageText="Назад" />
	</asp:GridView>
	<asp:ObjectDataSource ID="data" runat="server" TypeName="Ne.Helpers.StatusDataProvider" EnablePaging="true"
		SelectCountMethod="GetTotalNumberOfRows" SelectMethod="GetRows" OnSelecting="data_Selecting">
		<SelectParameters>
			<asp:Parameter Name="Filter" Type="Object" />
		</SelectParameters>
	</asp:ObjectDataSource>
</asp:Content>
