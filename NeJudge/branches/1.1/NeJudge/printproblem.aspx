<%@ Register TagPrefix="uc1" TagName="printproblem" Src="PageModules/printproblem.ascx" %>
<%@ Page language="c#" Codebehind="printproblem.aspx.cs" AutoEventWireup="false" Inherits="Ne.Judge.printproblem1" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Просмотр задачи - Версия для печати</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<link href="Styles/Default.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:printproblem id="Print" runat="server"></uc1:printproblem>
		</form>
	</body>
</HTML>
