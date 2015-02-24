<%@ Page Language="c#" Inherits="Ne.Judge.PrintProblemPage" CodeFile="printproblem.aspx.cs" %>

<%@ Register Src="~/UC/selectproblem.ascx" TagName="selectproblem" TagPrefix="nejudge" %>
<%@ Register Src="~/UC/problemview.ascx" TagName="problemview" TagPrefix="nejudge" %>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Печать задачи</title>
		<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
		<meta http-equiv="description" content="Архив задач по программированию"/>
		<meta http-equiv="keywords" content="ACM, Online Judge, NeJudge, SESC USU"/>
		<link href="Styles/Default.css" type="text/css" rel="stylesheet"/>
	</head>
	<body>
		<form id="MainForm" method="post" runat="server">
			<table cellpadding="0" cellspacing="0">
				<tr>
					<td><nejudge:selectproblem ID="selprob" Current="true" Past="true" runat="server" /></td>
					<td valign="bottom" id="tohide"><asp:Button runat="server" ID="goButton" Text="Вперед!" OnClick="goButton_Click" /></td>
				</tr>
			</table>
			<nejudge:problemview ID="problemView" runat="server" />
		</form>
	</body>
</html>
