<%@ Control Language="c#" AutoEventWireup="false" Codebehind="maintemplate.ascx.cs" Inherits="Ne.Judge.maintemplate" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="dfyz" TagName="footer" Src="UC\footer.ascx" %>
<%@ Register TagPrefix="dfyz" TagName="header" Src="UC\header.ascx" %>
<%@ Register TagPrefix="dfyz" TagName="nav" Src="UC\navigation.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>
			<%= PageName %>
		</title>
		<meta http-equiv="Content-Type" content="text/html; charset=windows-1251">
		<meta http-equiv="description" content="Архив задач по программированию">
		<meta http-equiv="keywords" content="ACM, Online Judge, NeJudge, SESC USU">
		<link href="Styles/Default.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="100%">
				<tr>
					<td colspan="2">
						<dfyz:header runat="server" id="Header1" />
					</td>
				</tr>
				<tr>
					<td class="nav" style="VERTICAL-ALIGN:top;WIDTH:20%">
						<dfyz:nav runat="server" id="Nav1" />
					</td>
					<td class="content" style="VERTICAL-ALIGN:top">
						<asp:placeholder id="Content" runat="server"></asp:placeholder>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<dfyz:footer runat="server" id="Footer1" />
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
