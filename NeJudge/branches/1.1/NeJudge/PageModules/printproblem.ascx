<%@ Control Language="c#" AutoEventWireup="false" Codebehind="printproblem.ascx.cs" Inherits="Ne.Judge.printproblem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table cellSpacing="5" cellPadding="5" width="100%">
	<tr>
		<td align=center>
			<asp:literal id="nameLiteral" runat="server" EnableViewState="False"></asp:literal>
			<div id="constraintsDiv" align="center">
				<asp:literal id="tlLiteral" runat="server" EnableViewState="False" Text="Ограничение времени: "></asp:literal><br>
				<asp:literal id="mlLiteral" runat="server" EnableViewState="False" Text="Ограничение памяти: "></asp:literal><br>
				<asp:literal id="olLiteral" runat="server" EnableViewState="False" Text="Ограничение на размер выходного файла: "></asp:literal><br>
			</div>
		</td>
	</tr>
	<tr>
		<td><asp:literal id="textLiteral" runat="server" EnableViewState="False" Text="<h2>Условие задачи:</h2>"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="infoLiteral" runat="server" EnableViewState="False" Text="<h2>Исходные данные:</h2>"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="outfoLiteral" runat="server" EnableViewState="False" Text="<h2>Результат:</h2>"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="inexLiteral" runat="server" EnableViewState="False" Text="<h2>Пример исходных данных:</h2><code>"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="outexLiteral" runat="server" EnableViewState="False" Text="<h2>Пример результата:</h2><code>"></asp:literal></td>
	</tr>
	<tr>
		<td><asp:literal id="authorLiteral" EnableViewState="False" Text="<hr><span style='color:#418ade;font-weight:bolder;'>Автор задачи: </span>"
				Runat="server"></asp:literal></td>
	</tr>
</table>