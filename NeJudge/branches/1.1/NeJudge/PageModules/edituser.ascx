<%@ Control Language="c#" AutoEventWireup="false" Codebehind="edituser.ascx.cs" Inherits="Ne.Judge.register" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<TABLE id="Table1" cellSpacing="5" cellPadding="1" border="0" runat="server">
	<tr>
		<td colspan="3" align="center">
			<asp:Label EnableViewState="False" id="errLiteral" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" />
		</td>
	</tr>
	<TR>
		<TD colspan="3">
			��������� ��������� ���� (whitespaces � ������ � ����� ����� �� �����������) � 
			������� ������ "���������"</TD>
	</TR>
	<TR>
		<TD>�����:</TD>
		<TD><asp:TextBox id="loginTextBox" runat="server" tabIndex="1"></asp:TextBox></TD>
		<td>
			<asp:CustomValidator id="loginTextBoxValidator" runat="server" ErrorMessage="����� �� ����� ���� ������"
				ControlToValidate="loginTextBox"></asp:CustomValidator></td>
	</TR>
	<TR>
		<TD>������ ������:</TD>
		<TD><asp:TextBox id="oldpassTextBox" runat="server" TextMode="Password" tabIndex="2"></asp:TextBox></TD>
		<td>
			<asp:CustomValidator id="oldpassTextBoxValidator" runat="server" ErrorMessage="�������� ����� �� �������� ������ �������"
				ControlToValidate="oldpassTextBox" EnableClientScript="False"></asp:CustomValidator></td>
	</TR>
	<TR>
		<TD>������:</TD>
		<TD><asp:TextBox id="passTextBox" runat="server" TextMode="Password" tabIndex="2"></asp:TextBox></TD>
		<td>
			<asp:CustomValidator id="passTextBoxValidator" runat="server" ErrorMessage="������ �� ����� ���� ������"
				ClientValidationFunction="passTextBoxValidation"></asp:CustomValidator></td>
	</TR>
	<TR>
		<TD style="HEIGHT: 26px">������������� ������:</TD>
		<TD style="HEIGHT: 26px"><asp:TextBox id="repassTextBox" runat="server" TextMode="Password" tabIndex="2"></asp:TextBox></TD>
		<td style="HEIGHT: 26px">
			<asp:CustomValidator id="repassTextBoxValidator" runat="server" ErrorMessage="�������� ����� �� ��������� � �������"
				ControlToValidate="repassTextBox" ClientValidationFunction="repassTextBoxValidation"></asp:CustomValidator></td>
	</TR>
	<tr>
		<TD>�����, ������� ������������ ��� ����������� � ��������:</TD>
		<td><asp:TextBox id="nameTextBox" runat="server" tabIndex="3"></asp:TextBox></td>
		<td><asp:RequiredFieldValidator id="nameTextBoxValidator" runat="server" ControlToValidate="nameTextBox" ErrorMessage="���� ����� �� ����� ���� ������"></asp:RequiredFieldValidator></td>
	</tr>
	<tr>
		<TD>E-mail:</TD>
		<td><asp:TextBox id="mailTextBox" runat="server" tabIndex="3"></asp:TextBox></td>
		<td>
			<asp:RegularExpressionValidator id="mailTextBoxValidator" runat="server" ErrorMessage="������� ���������� email"
				ControlToValidate="mailTextBox" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
	</tr>
	<tr>
		<td align="center" colspan="3">
			<asp:Button id="finishButton" runat="server" Text="���������" tabIndex="4"></asp:Button></td>
	</tr>
</TABLE>
<script language="javascript">
function repassTextBoxValidation(sender,args)
{
	if( document.getElementById("<%=oldpassTextBox.ClientID %>") == null || document.getElementById("<%=oldpassTextBox.ClientID %>").value != "")
		if( document.getElementById("<%=repassTextBox.ClientID %>").value != document.getElementById("<%=passTextBox.ClientID %>").value )
			args.IsValid = false;
}
function passTextBoxValidation(sender,args)
{
	if( document.getElementById("<%=oldpassTextBox.ClientID %>") == null || document.getElementById("<%=oldpassTextBox.ClientID %>").value != "")
		if( trim(document.getElementById("<%=passTextBox.ClientID %>").value) == "" )
			args.IsValid = false;
}
function trim(s)
{
	while (s.substring(0,1) == ' ')
	{
		s = s.substring(1,s.length);
	}
	while (s.substring(s.length-1,s.length) == ' ')
	{
		s = s.substring(0,s.length-1);
	}
	return s;
}
</script>
