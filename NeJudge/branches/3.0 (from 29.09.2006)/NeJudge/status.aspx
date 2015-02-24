<%@ Page Language="c#" Inherits="Ne.Judge.StatusPage" CodeFile="status.aspx.cs" MasterPageFile="~/design.master"
	Title="Посланные решения" %>
<%@ Import Namespace="Ne.Helpers" %>
<%@ Register TagPrefix="nejudge" TagName="SubmissionsFilter" Src="~/UC/SubmissionsFilter.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<nejudge:SubmissionsFilter ID="filter" runat="server" />
	<asp:GridView ID="statusGV" runat="server" CellPadding="5" DataSourceID="data" BorderWidth="0px"
		Width="100%" AllowPaging="True" EmptyDataText="Нет посылок, соответствующих фильтру" 
		OnPageIndexChanging="statusGV_PageIndexChanging" AutoGenerateColumns="false">
		<Columns>
			<asp:BoundField DataField="ID" HeaderText="ID" />
			<asp:BoundField DataField="Time" HeaderText="Время" />
			<asp:BoundField DataField="Team" HeaderText="Команда" />
			<asp:HyperLinkField DataTextField="ProblemShortName" 
				DataNavigateUrlFields="ProblemID" DataNavigateUrlFormatString="~/problem.aspx?problemID={0}"
				HeaderText="Задача" />
			 <asp:BoundField DataField="Language" HeaderText="Язык" />
			 <asp:BoundField DataField="Outcome" HeaderText="Статус" />
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
		<RowStyle CssClass="gridRow" />
		<AlternatingRowStyle CssClass="gridAlternateRow" />
		<PagerStyle BorderStyle="None" />
		<PagerSettings Mode="NextPrevious" NextPageText="Вперед" PreviousPageText="Назад" />
	</asp:GridView>
	<asp:ObjectDataSource ID="data" runat="server" TypeName="Ne.Helpers.StatusDataProvider" EnablePaging="true"
		 SelectMethod="GetRows" OnSelecting="data_Selecting">
		<SelectParameters>
			<asp:Parameter Name="filter" Type="Object" />
		</SelectParameters>
	</asp:ObjectDataSource>
</asp:Content>
