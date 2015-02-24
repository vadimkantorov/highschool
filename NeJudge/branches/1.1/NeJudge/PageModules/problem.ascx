<%@ Register TagPrefix="uc1" TagName="selectproblem" Src="../UC/selectproblem.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="problem.ascx.cs" Inherits="Ne.Judge.problem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table>
	<tr>
		<td><uc1:selectproblem id="selprob" Future="false" Now="true" Old="true" runat="server"></uc1:selectproblem></td>
		<td valign="bottom"><asp:button id="goButton" runat="server" Text="Вперёд!" EnableViewState="False"></asp:button></td>
	</tr>
</table>
<table id="Table2" cellSpacing="5" cellPadding="5" width="100%" runat="server">
	<tr>
		<td align="center"><asp:literal id="nameLiteral" runat="server" EnableViewState="False"></asp:literal><asp:literal id="tlLiteral" runat="server" Text="Ограничение времени: " EnableViewState="False"></asp:literal><br>
			<asp:literal id="mlLiteral" runat="server" Text="Ограничение памяти: " EnableViewState="False"></asp:literal><br>
			<asp:literal id="olLiteral" runat="server" Text="Ограничение на размер выходного файла: " EnableViewState="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="textLiteral" runat="server" Text="<h2>Условие задачи:</h2>" EnableViewState="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="infoLiteral" runat="server" Text="<h2>Исходные данные:</h2>" EnableViewState="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="outfoLiteral" runat="server" Text="<h2>Результат:</h2>" EnableViewState="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="inexLiteral" runat="server" Text="<h2>Пример исходных данных:</h2>" EnableViewState="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="outexLiteral" runat="server" Text="<h2>Пример результата:</h2>" EnableViewState="False"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="authorLiteral" Text="<hr><span style='color:#418ade;font-weight:bolder;'>Автор задачи: </span>"
				EnableViewState="False" Runat="server"></asp:literal></td>
	</tr>
	<tr>
		<td>
			<hr>
			<asp:hyperlink id="Hyperlink1" runat="server" EnableViewState="False" NavigateUrl="../printproblem.aspx?pid=">
				<FONT COLOR="Red">Версия для печати</FONT>
			</asp:hyperlink>&nbsp;
			<asp:hyperlink id="Hyperlink2" runat="server" EnableViewState="False" NavigateUrl="../submit.aspx?pid=">
				<FONT COLOR="RED">Послать на проверку</FONT>
			</asp:hyperlink>&nbsp;
			<asp:hyperlink id="Hyperlink3" runat="server" EnableViewState="False" NavigateUrl="../questions.aspx?pid=">
				<FONT COLOR="Red">Вопросы</FONT>
			</asp:hyperlink>&nbsp;
			<asp:hyperlink id="Hyperlink4" runat="server" EnableViewState="False" NavigateUrl="../ask.aspx?pid=">
				<FONT COLOR="Red">Задать вопрос</FONT>
			</asp:hyperlink></td>
	</tr>
</table>
