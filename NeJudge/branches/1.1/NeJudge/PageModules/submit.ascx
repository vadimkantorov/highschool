<%@ Control Language="c#" AutoEventWireup="false" Codebehind="submit.ascx.cs" Inherits="Ne.Judge.submit" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="uc1" TagName="selectproblem" Src="../UC/selectproblem.ascx" %>
<uc1:selectproblem id="selprob" Future="false" Now="true" Old="false" runat="server"></uc1:selectproblem>
<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="80%" align="center" border="0"
	runat="server">
	<tr>
		<td align="center" colSpan="2"><asp:customvalidator id="CustomValidator1" runat="server" ErrorMessage='Выберите файл с кодом ИЛИ заполните поле "Исходник" (ИЛИ - исключающее)'
				ClientValidationFunction="validate"></asp:customvalidator></td>
	</tr>
	<TR>
		<TD align="center">Язык:</TD>
		<TD><asp:dropdownlist id="languageDropDownList" runat="server" Width="96px">
				<asp:ListItem Value="C++">C++</asp:ListItem>
				<asp:ListItem Value="C">C</asp:ListItem>
				<asp:ListItem Value="Pascal">Pascal</asp:ListItem>
			</asp:dropdownlist></TD>
	</TR>
	<tr>
		<td vAlign="top" align="center">Исходник:</td>
		<td><asp:textbox id="sourceTextBox" runat="server" Rows="15" TextMode="MultiLine" Columns="50"></asp:textbox></td>
	</tr>
	<tr>
		<td align="center"><i><b>Или</b></i> файл с кодом:</td>
		<td><INPUT language="c#" id="fileBrowser" type="file" size="50" runat="server"></td>
	</tr>
	<tr>
		<td align="center" colSpan="2"><asp:button id="Button1" runat="server" Text="Послать"></asp:button></td>
	</tr>
</TABLE>
<script>
function validate(sender,args)
{
args.IsValid = (document.getElementById("<%=sourceTextBox.ClientID%>").value == "" ^ 
	document.getElementById("<%=fileBrowser.ClientID%>").value == "");
}
</script>
