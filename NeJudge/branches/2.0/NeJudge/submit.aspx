<%@ Page Language="c#" Inherits="Ne.Judge.SubmitPage" CodeFile="submit.aspx.cs" MasterPageFile="~/design.master"
	Title="������� �� ��������"  ValidateRequest="false" %>
<%@ Register TagPrefix="nejudge" TagName="SelectProblem" Src="~/UC/SelectProblem.ascx" %>
<%@ Register TagPrefix="nejudge" TagName="ErrorMessage" Src="~/UC/ErrorMessage.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<nejudge:SelectProblem ID="selprob" Current="true" runat="server" />
	<table id="tohide" width="100%" border="0">
		<tr>
			<td align="center" colspan="2">
				<asp:CustomValidator ID="sourceV" runat="server" ErrorMessage='�������� ���� � ����� ��� ��������� ���� "�������� ���" (��� - �����������)'
					ClientValidationFunction="validate" OnServerValidate="sourceV_ServerValidate"></asp:CustomValidator>&nbsp;
				</td>
		</tr>
		<tr>
			<td align="center">����:</td>
			<td><asp:DropDownList ID="languageDropDownList" runat="server" DataTextField="Name" DataValueField="ID" /></td>
		</tr>
		<tr>
			<td valign="top" align="center">�������� ���:</td>
			<td><asp:TextBox ID="sourceTextBox" runat="server" Rows="15" TextMode="MultiLine" Columns="50"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="center"><i><b>���</b></i> ���� � �����:</td>
			<td><asp:FileUpload ID="file" runat="server" Width="100%" /></td>
		</tr>
		<tr>
			<td align="center" colspan="2">
				<asp:Button ID="Button1" runat="server" Text="�������" OnClick="Button1_Click"></asp:Button>
			</td>
		</tr>
	</table>

	<script type="text/javascript">
	function validate(sender,args)
	{
		args.IsValid = (document.getElementById("<%=sourceTextBox.ClientID%>").value == "" ^ 
		document.getElementById("<%=file.ClientID%>").value == "");
	}
	</script>

</asp:Content>
