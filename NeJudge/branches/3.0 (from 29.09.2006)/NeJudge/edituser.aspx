<%@ Page Language="c#" Inherits="Ne.Judge.EditUserPage" CodeFile="edituser.aspx.cs" MasterPageFile="~/design.master"
	Title="������ ������ / �����������" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<table id="Table1" cellspacing="5" cellpadding="1" border="0" runat="server">
		<tr>
			<td colspan="3" align="center">
				<asp:Label EnableViewState="False" ID="errLiteral" runat="server" Visible="False"
					Font-Bold="True" ForeColor="Red" />
			</td>
		</tr>
		<tr>
			<td colspan="3">
				��������� ��������� ���� (whitespaces � ������ � ����� ����� �� �����������) � �������
				������ "���������"</td>
		</tr>
		<tr>
			<td>
				�����:</td>
			<td>
				<asp:TextBox ID="loginTextBox" runat="server" TabIndex="1"></asp:TextBox></td>
			<td>
				<asp:CustomValidator ID="loginTextBoxValidator" runat="server" ErrorMessage="����� �� ����� ���� ������"
					ControlToValidate="loginTextBox" OnServerValidate="loginTextBoxValidator_ServerValidate"></asp:CustomValidator></td>
		</tr>
		<tr>
			<td>
				������ ������:</td>
			<td>
				<asp:TextBox ID="oldpassTextBox" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox></td>
			<td>
				<asp:CustomValidator ID="oldpassTextBoxValidator" runat="server" ErrorMessage="�������� ����� �� �������� ������ �������"
					ControlToValidate="oldpassTextBox" EnableClientScript="False" OnServerValidate="oldpassValidator_ServerValidate"></asp:CustomValidator></td>
		</tr>
		<tr>
			<td>
				������:</td>
			<td>
				<asp:TextBox ID="passTextBox" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox></td>
			<td>
				<asp:CustomValidator ID="passTextBoxValidator" runat="server" ErrorMessage="������ �� ����� ���� ������"
					ClientValidationFunction="passTextBoxValidation" OnServerValidate="passTextBoxValidator_ServerValidate"></asp:CustomValidator></td>
		</tr>
		<tr>
			<td style="height: 26px">
				������������� ������:</td>
			<td style="height: 26px">
				<asp:TextBox ID="repassTextBox" runat="server" TextMode="Password" TabIndex="2"></asp:TextBox></td>
			<td style="height: 26px">
				<asp:CustomValidator ID="repassTextBoxValidator" runat="server" ErrorMessage="�������� ����� �� ��������� � �������"
					ControlToValidate="repassTextBox" ClientValidationFunction="repassTextBoxValidation"
					OnServerValidate="repassTextBoxValidator_ServerValidate"></asp:CustomValidator></td>
		</tr>
		<tr>
			<td>
				�����, ������� ������������ ��� ����������� � ��������:</td>
			<td>
				<asp:TextBox ID="nameTextBox" runat="server" TabIndex="3"></asp:TextBox></td>
			<td>
				<asp:RequiredFieldValidator ID="nameTextBoxValidator" runat="server" ControlToValidate="nameTextBox"
					ErrorMessage="���� ����� �� ����� ���� ������"></asp:RequiredFieldValidator></td>
		</tr>
		<tr>
			<td>
				E-mail:</td>
			<td>
				<asp:TextBox ID="mailTextBox" runat="server" TabIndex="3"></asp:TextBox></td>
			<td>
				<asp:RegularExpressionValidator ID="mailTextBoxValidator" runat="server" ErrorMessage="������� ���������� email"
					ControlToValidate="mailTextBox" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator></td>
		</tr>
		<tr>
			<td align="center" colspan="3">
				<asp:Button ID="finishButton" runat="server" Text="���������" TabIndex="4" OnClick="finishButton_Click">
				</asp:Button></td>
		</tr>
	</table>

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

</asp:Content>
