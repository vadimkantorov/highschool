<%@ Page Language="c#" Inherits="Ne.Judge.ProblemPage" CodeFile="problem.aspx.cs"
	MasterPageFile="~/design.master" Title="Просмотр задачи" %>

<%@ Register TagPrefix="nejudge" TagName="problemview" Src="~/UC/problemview.ascx" %>
<%@ Register TagPrefix="nejudge" TagName="SelectProblem" Src="~/UC/SelectProblem.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<table>
		<tr>
			<td>
				<nejudge:SelectProblem ID="selprob" Forthcoming="false" Current="true" Past="true" runat="server" />
			</td>
			<td valign="bottom" id="tohide">
				<asp:Button runat="server" ID="goButton" Text="Вперед!" OnClick="goButton_Click" />
			</td>
		</tr>
	</table>

	<table id="problemTable" runat="server" width="100%">
		<tr>
			<td>
				<nejudge:problemview ID="problemView" runat="server" />
			</td>
		</tr>
		<tr>
			<td>
				<asp:HyperLink ID="printProblemHL" runat="server" EnableViewState="False">Версия для печати</asp:HyperLink>&nbsp;
				<asp:HyperLink ID="submitHL" runat="server" EnableViewState="False">Послать на проверку</asp:HyperLink>&nbsp;
				<asp:HyperLink ID="questionsHL" runat="server" EnableViewState="False">Вопросы</asp:HyperLink>&nbsp;
				<asp:HyperLink ID="askHL" runat="server" EnableViewState="False">Задать вопрос</asp:HyperLink>&nbsp;
				<asp:HyperLink ID="editHL" runat="server" EnableViewState="False" Visible="false">Редактировать</asp:HyperLink>
			</td>
		</tr>
	</table>
</asp:Content>
