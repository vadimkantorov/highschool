<%@ Page Language="c#" Inherits="Ne.Judge.AskPage" CodeFile="ask.aspx.cs" MasterPageFile="~/design.master"
	Title="������ ������" %>
<%@ Register TagPrefix="nejudge" TagName="selectproblem" Src="UC/selectproblem.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<nejudge:selectproblem ID="selprob" runat="server" Current="true" Forthcoming="false" />
	<table id="tohide" cellspacing="0" align="center" cellpadding="5" width="100%">
		<tr>
			<td valign="top" width="10%">������:</td>
			<td align="center">
				<asp:TextBox ID="messageTB" runat="server" Rows="15" Columns="50" TextMode="MultiLine" /><br />
				<asp:Button ID="sendMessageB" runat="server" OnClick="sendMessageB_Click" Text="�������" />
			</td>
			<td valign="top">
				<asp:CustomValidator ID="messageV" runat="server" ErrorMessage="������� �����, ������ �� ������ 10 ��������"
					ClientValidationFunction="validate" OnServerValidate="messageV_ServerValidate" />
			</td>
		</tr>
	</table>
	<script language="javascript" type="text/javascript">
	function validate(sender, args)
	{
		var len = document.getElementById("<%=messageTB.ClientID%>").value.length;
		if(len < 10)
			args.IsValid = false;
		else if(len > 60)
		{
			sender.innerText = "������� �����, ������ �� ������ 60 ��������";
			args.IsValid = false;
		}
	}
	</script>
</asp:Content>
