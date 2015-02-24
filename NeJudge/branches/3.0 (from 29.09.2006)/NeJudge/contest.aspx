<%@ Page Language="c#" Inherits="Ne.Judge.ContestPage" CodeFile="contest.aspx.cs" MasterPageFile="~/design.master" Title="Список задач" %>
<%@ Register TagPrefix="nejudge" TagName="SelectContest" Src="~/UC/SelectContest.ascx" %>
<%@ Register TagPrefix="nejudge" TagName="ErrorMessage" Src="~/UC/ErrorMessage.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<%--<nejudge:SelectContest ID="selcon" EnableViewState="true" Address="contest.aspx"
		runat="server" Old="true" Now="true" Future="true" />--%>
	<asp:GridView runat="server" ID="problemsGV" EnableViewState="False" AutoGenerateColumns="False" CssClass="grid" Width="100%"
		CellPadding="5" BorderWidth="0px" GridLines="None" EmptyDataText="В этом соревновании нет задач" DataSourceID="testods">
		<Columns>
			<asp:BoundField HeaderText="ID" DataField="ID" >
				<ItemStyle Width="5%" />
			</asp:BoundField>
			<asp:BoundField HeaderText="Короткое название" DataField="ShortName" >
				<ItemStyle Width="10%" />
			</asp:BoundField>
			<asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="problem.aspx?problemID={0}"
				DataTextField="Name" HeaderText="Название" >
				<ItemStyle HorizontalAlign="Left" />
			</asp:HyperLinkField>
		</Columns>
		<HeaderStyle CssClass="gridHeader" />
		<RowStyle CssClass="gridRow" />
		<AlternatingRowStyle CssClass="gridAlternateRow" />
	</asp:GridView>
	<asp:ObjectDataSource runat="server" ID="testods" SelectMethod="GetProblems" TypeName="Ne.Database.Classes.Problem">
		<SelectParameters>
			<asp:QueryStringParameter Type="Int32" QueryStringField="contestID" Name="contestID" />
		</SelectParameters>
	</asp:ObjectDataSource>
	<%if(problemsGV.Rows.Count != 0) { %>
	<asp:HyperLink runat="server" ID="printHL" EnableViewState="False">Версия для печати</asp:HyperLink>&nbsp;
	<asp:HyperLink runat="server" ID="monHL" EnableViewState="False">Результаты</asp:HyperLink>&nbsp;
	<asp:HyperLink runat="server" ID="statHL" EnableViewState="False">Submissions</asp:HyperLink>&nbsp;
	<asp:HyperLink runat="server" ID="quesHL" EnableViewState="False">Вопросы</asp:HyperLink>&nbsp;&nbsp;
	<%} %>		  
	<asp:HyperLink runat="server" ID="editHL" EnableViewState="False" Visible="False">Редактировать</asp:HyperLink>
</asp:Content>
