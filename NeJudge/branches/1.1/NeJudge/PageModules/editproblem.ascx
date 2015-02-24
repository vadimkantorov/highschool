<%@ Control Language="c#" AutoEventWireup="false" Codebehind="editproblem.ascx.cs" Inherits="Ne.Judge.editproblem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
function f(x)
{
	if(x == true)
	{	document.getElementById("Table2").rows[0].cells[0].className = "ne_first";
		document.getElementById("Table2").rows[0].cells[1].className = "ne_second";
		document.getElementById("Table2").rows[1].style.display="";
		document.getElementById("Table2").rows[2].style.display="none";
	}
	else
	{
		document.getElementById("Table2").rows[0].cells[0].className = "ne_second";
		document.getElementById("Table2").rows[0].cells[1].className = "ne_first";
		document.getElementById("Table2").rows[1].style.display="none";
		document.getElementById("Table2").rows[2].style.display="";
	}
}
</script>
<div language="C#" id="outerror" runat="server"></div>
<table id="Table2" border=1>
	<tr>
		<td class="ne_first"><a href="javascript:f(true)">Смысл</a></td>
		<td class="ne_second"><a href="javascript:f(false)">Ерунда</a></td>
	</tr>
	<tr>
		<td colspan="2">
			<TABLE id="Table1" cellpadding="2" runat="server" border="0" width="100%">
				<TR>
					<td vAlign="top" align="right">Название задачи:</td>
					<td><asp:textbox id="problemNameTextBox" runat="server" Columns="60"></asp:textbox></td>
				</TR>
				<TR>
					<td vAlign="top" align="right">Текст задачи:</td>
					<td>
						<asp:textbox id="problemTextTextBox" TextMode="MultiLine" Columns="50" Rows="15" runat="server"></asp:textbox></td>
				</TR>
				<TR>
					<TD vAlign="top" align="right">Формат ввода:</TD>
					<TD><asp:textbox id="inputFormatTextBox" Columns="50" Rows="15" runat="server" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right">Формат вывода:</TD>
					<TD><asp:textbox id="outputFormatTextBox" Columns="50" Rows="15" runat="server" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right">Пример ввода:</TD>
					<TD><asp:textbox id="inputSampleTextbox" Columns="50" Rows="15" runat="server" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right">Пример вывода:</TD>
					<TD><asp:textbox id="outputSampleTextbox" Columns="50" Rows="15" runat="server" TextMode="MultiLine"></asp:textbox></TD>
				</TR>
				<TR>
					<TD vAlign="top" align="right">Автор:</TD>
					<TD><asp:textbox id="authorTextBox" Columns="60" runat="server"></asp:textbox></TD>
				</TR>
				<tr>
					<td align="center" colspan="2">
						<asp:button id="finishButton" runat="server" Text="Закончить"></asp:button>
					</td>
				</tr>
			</TABLE>
		</td>
	</tr>
	<tr style="display:none">
	<td colspan=2>Всякая ерунда!</td>
	</tr>
</table>
