<%@ Control Language="c#" AutoEventWireup="false" Codebehind="header.ascx.cs" Inherits="Ne.Judge.header" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<table width="100%" cellpadding="0" cellspacing="0" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid">
	<tr valign="middle" class="ne_first">
		<td align="left">
			<h1 class="ne_first" style="MARGIN-TOP:8px;MARGIN-BOTTOM:8px;MARGIN-LEFT:8px"><a href="./" style="COLOR:red">Ne</a><span style="COLOR:black">Judge</span>
				Online Judge</h1>
		</td>
		<td align="left">
			<h2 class="ne_first" style="MARGIN:2%"><asp:Label Runat="server" ID="pname" /></h2>
		</td>
		<td></td>
	</tr>
	<tr class="ne_second">
		<td style="PADDING-RIGHT:5px;PADDING-LEFT:5px;PADDING-BOTTOM:5px;PADDING-TOP:5px" colspan="2"><asp:LinkButton id="signOut" runat="server" CausesValidation="False"></asp:LinkButton></td>
		<td align="right">
			<a href="faq.aspx">FAQ</a> <strong>::</strong> <a href="license.aspx">Лицензия</a>
			<strong>::</strong> <a href="about.aspx">Контакты</a>
		</td>
	</tr>
</table>
