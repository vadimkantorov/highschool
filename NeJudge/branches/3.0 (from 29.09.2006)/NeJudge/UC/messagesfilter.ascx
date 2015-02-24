<%@ Control Language="c#" Inherits="Ne.Judge.MessagesFilterControl"
CodeFile="messagesfilter.ascx.cs" %>
<%@ Register Src="selectproblem.ascx" TagName="selectproblem" TagPrefix="nejudge" %>
<%@ Register TagPrefix="nejudge" TagName="helpmsg" Src="../UC/HelpMessage.ascx" %>
<table>
	<tr>
		<td colspan="2">
			<nejudge:selectproblem EveryContestTime="Current,Forthcoming" Past="true" Current="true"
			 ID="selprob" runat="server" SpecialProblemName="--�����--" />
		</td>
	</tr>
	<tr id="userTR" runat="server">
		<td>������������:</td>
		<td><asp:DropDownList EnableViewState="true" ID="usersDDL" Runat="server" /></td>
	</tr>
	<tr>
		<td colspan="2"><asp:CheckBox ID="ansCB" runat="server" Text="����������" /></td>
	</tr>
	
	<tr>
		<td align="center" colspan="2">
			<asp:Button EnableViewState="True" ID="filterButton" Runat="server" Text="������!" OnClick="filterButton_Click" />
		</td>
	</tr>
</table>
