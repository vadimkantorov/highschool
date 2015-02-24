<%@ Register TagPrefix="uc1" TagName="selectcontest" Src="../UC/selectcontest.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="monitor.ascx.cs" Inherits="Ne.Judge.monitor" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="ErrorMessage" Src="../UC/ErrorMessage.ascx" %>
<uc1:selectcontest id="selcon" Future="true" Now="true" Old="true" EnableViewState="true" runat="server"
	Address="monitor.aspx"></uc1:selectcontest>
<uc1:ErrorMessage id="ErrorMessage" runat="server" EnableViewState="False"></uc1:ErrorMessage>
<div id="info" runat="server">
	<table class="grid" id="time_table" width="100%" border="0">
		<tr>
			<td>
				<h1 id="st_label" align="center" runat="server">Положение участников</h1>
			</td>
			<td align="right">
				<TABLE class="grid" cellSpacing="0" cellPadding="5">
					<tr class="grid_head">
						<th colSpan="2">
							Сведения о времени</th></tr>
					<TR class="grid_second">
						<TD>Прошло:
						</TD>
						<TD><asp:literal id="refresh" runat="server" EnableViewState="False"></asp:literal></TD>
					</TR>
					<TR class="grid_second">
						<TD>Осталось:
						</TD>
						<TD><asp:literal id="left" runat="server" EnableViewState="False"></asp:literal></TD>
					</TR>
					<TR class="grid_second">
						<TD>Продолжительность соревнования:
						</TD>
						<TD><asp:literal id="period" runat="server" EnableViewState="False"></asp:literal></TD>
					</TR>
					<TR class="grid_second">
						<TD>Последняя успешная сдача:
						</TD>
						<TD><asp:literal id="lastac" runat="server" EnableViewState="False"></asp:literal></TD>
					</TR>
				</TABLE>
			</td>
		</tr>
	</table>
	<asp:datagrid id="monitorDG" EnableViewState="False" runat="server" BorderWidth="0px" CssClass="grid"
		CellPadding="5" Width="100%">
		<ItemStyle CssClass="grid_second"></ItemStyle>
		<HeaderStyle CssClass="grid_head"></HeaderStyle>
	</asp:datagrid>
	<h2>Статистика</h2>
	<asp:datagrid id="statDG" EnableViewState="False" runat="server" BorderWidth="0px" CellPadding="5"
		Width="100%">
		<ItemStyle CssClass="grid_second"></ItemStyle>
		<HeaderStyle CssClass="grid_head"></HeaderStyle>
	</asp:datagrid>
</div>
