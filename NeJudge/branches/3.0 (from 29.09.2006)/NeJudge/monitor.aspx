<%@ Page Language="c#" Inherits="Ne.Judge.MonitorPage" CodeFile="monitor.aspx.cs" MasterPageFile="~/design.master"
	Title="Монитор" %>
<%@ Register TagPrefix="nejudge" TagName="SelectContest" Src="~/UC/SelectContest.ascx" %>
<%@ Register TagPrefix="nejudge" TagName="ErrorMessage" Src="~/UC/ErrorMessage.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Content" runat="server">
	<nejudge:SelectContest ID="selcon"  runat="server" Current="true" Past="true" Address="monitor.aspx" />
	<div id="info" runat="server">
		<table class="grid" id="time_table" width="100%" border="0">
			<tr>
				<td>
					<h1 id="st_label" align="center" runat="server">
						Положение участников</h1>
				</td>
				<td align="right">
					<table class="grid" cellspacing="0" cellpadding="5">
						<tr class="gridHeader">
							<th colspan="2">Сведения о времени</th>
						</tr>
						<tr class="gridAlternateRow">
							<td>Прошло:</td>
							<td><asp:Literal ID="refresh" runat="server" EnableViewState="False" /></td>
						</tr>
						<tr class="gridAlternateRow">
							<td>Осталось:</td>
							<td><asp:Literal ID="left" runat="server" EnableViewState="False" /></td>
						</tr>
						<tr class="gridAlternateRow">
							<td>Продолжительность соревнования:</td>
							<td><asp:Literal ID="period" runat="server" EnableViewState="False" /></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		<asp:PlaceHolder ID="monitorPH" runat="server" />
	</div>
</asp:Content>
