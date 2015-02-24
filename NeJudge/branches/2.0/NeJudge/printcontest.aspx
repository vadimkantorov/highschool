<%@ Page Language="c#" Inherits="Ne.Judge.PrintContestPage" CodeFile="printcontest.aspx.cs" %>

<%@ Register Src="~/UC/selectcontest.ascx" TagName="selectcontest" TagPrefix="nejudge" %>
<%@ Register Src="~/UC/problemview.ascx" TagName="problemview" TagPrefix="nejudge" %>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Печать соревнования</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta http-equiv="description" content="Архив задач по программированию"/>
		<meta http-equiv="keywords" content="ACM, Online Judge, NeJudge, SESC USU"/>
		<link href="Styles/Default.css" type="text/css" rel="stylesheet"/>
	</head>
	<body>
		<form id="MainForm" method="post" runat="server">
			<nejudge:selectcontest ID="selcon" Current="true" Past="true" runat="server" />
			<asp:PlaceHolder ID="problemsPH" runat="server" />
		</form>
	</body>
</html>