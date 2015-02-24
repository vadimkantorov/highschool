<%@ Register TagPrefix="uc1" TagName="selectproblem" Src="../UC/selectproblem.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="ask.ascx.cs" Inherits="Ne.Judge.ask" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<uc1:selectproblem id="selprob" runat="server" Old="false" Now="true" Future="false"></uc1:selectproblem>
<TABLE id="hidabletable" cellSpacing="0" align="center" cellPadding="5" width="100%" border="0"
	runat="server">
	<TR>
		<TD vAlign="top" width="10%">Вопрос:</TD>
		<TD align="left" style="WIDTH: 418px"><asp:textbox id="qTextBox" runat="server" Rows="15" Columns="50" TextMode="MultiLine"></asp:textbox></TD>
		<td valign="top">
			<asp:CustomValidator id="CustomValidator1" runat="server" ErrorMessage="Введите текст, длиной не меньше 10 символов"
				ClientValidationFunction="validate"></asp:CustomValidator></td>
	</TR>
	<tr>
		<td><asp:button id="Button1" runat="server" Text="Послать"></asp:button></td>
	</tr>
</TABLE>
<script language="javascript">
function validate(sender, args)
{
	var len = document.getElementById("<%=qTextBox.ClientID%>").value.length;
	if(len < 10)
		args.IsValid = false;
	else if(len > 60)
	{
		sender.innerText = "Введите текст, длиной не больше 60 символов";
		args.IsValid = false;
	}
}
</script>
