<%@ Control Language="c#" AutoEventWireup="false" Codebehind="editcontest.ascx.cs" Inherits="Ne.Judge.editcontest" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<DIV id="outerror" runat="server"></DIV>
<TABLE id="Table3" cellSpacing="1" cellPadding="1" border="0" runat="server">
	<TR>
		<TD>Название соревнования</TD>
		<TD><asp:textbox id="nameTextbox" runat="server"></asp:textbox></TD>
		<td>
			<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" EnableViewState="False" ErrorMessage='Введите что-нибудь в поле "Название"'
				ControlToValidate="nameTextbox"></asp:RequiredFieldValidator></td>
	</TR>
	<TR>
		<TD>Дата начала соревнования</TD>
		<TD><asp:textbox id="beginningTextBox" runat="server"></asp:textbox></TD>
		<td>
			<asp:CustomValidator id="beginningCustomValidator" runat="server" ErrorMessage="Введите корректную дату"
				EnableViewState="False" ClientValidationFunction="beginningCustomValidator_ClientValidate"></asp:CustomValidator></td>
	</TR>
	<TR>
		<TD>Дата окончания соревнования</TD>
		<TD><asp:textbox id="endingTextBox" runat="server"></asp:textbox></TD>
		<td>
			<asp:CustomValidator id="endingCustomValidator" runat="server" ErrorMessage="Введите корректную дату"
				EnableViewState="False" ClientValidationFunction="endingCustomValidator_ClientValidate"></asp:CustomValidator></td>
	</TR>
</TABLE>
<TABLE id="Table4" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
	<TBODY>
		<tr>
			<TD colspan="3"><asp:repeater id="Repeater1" runat="server">
					<HeaderTemplate>
						<TABLE id="Table2" cellSpacing="0" cellPadding="0" border="1" width="100%">
							<THEAD>
								<TR>
									<TH>
										PID</TH>
									<th>
										Кор. название</th>
									<TH>
										Редактировать</TH>
									<TH>
										Неполный текст</TH>
									<TH>
										Удалить</TH>
								</TR>
							</THEAD>
					</HeaderTemplate>
					<ItemTemplate>
						<tr>
							<td><asp:Literal Runat=server ID="PID" Text='<%#DataBinder.Eval(Container.DataItem, "PID")%>' /></td>
							<td><%#DataBinder.Eval(Container.DataItem, "ShortName")%></td>
							<td>
								<asp:HyperLink ID="Hyperlink2" runat="server" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "Url")%>'>
									<%#DataBinder.Eval(Container.DataItem, "Name")%>
								</asp:HyperLink>
							</td>
							<td><%#DataBinder.Eval(Container.DataItem, "Text")%></td>
							<td><asp:checkbox id="Remove" runat="server" /></td>
						</tr>
					</ItemTemplate>
					<FooterTemplate>
</TABLE>
</FooterTemplate> </asp:repeater></TD></TR>
<tr>
	<td align="right"><asp:button id="addButton" runat="server" Text="Добавить задачу" CausesValidation="False"></asp:button></td>
	<td align="center"><asp:button id="finishButton" runat="server" Text="Закончить"></asp:button></td>
	<td align="left"><asp:button id="deleteButton" runat="server" Text="Удалить задачи" CausesValidation="False"></asp:button></td>
</tr>
</TBODY></TABLE>
<script>
	function isvalid(str)
	{
		var pattern = "^(?:(0[1-9]|[12]\\d|3[01])\\.(01|03|05|07|08|10|12)\\.(\\d{4})|(0[1-9]|[12]\\d|30)\\.(04|06|09|11)\\.(\\d{4})|(0[1-9]|1\\d|2[0-8])\\.(02)\\.(\\d{4})|(29)\\.(02)\\.((?:[02468][048]|[13579][26])00|\\d{2}(?:[02468][48]|[2468][048]|[13579][26])))\\040(00|1?\\d|2[0-3]):([0-5]\\d)$";
		return new RegExp(pattern).exec(str);
	}
	function parsedate(str)
	{
		var m = isvalid(str);
		var dt;
		
		if( m!= null)
		{
			var day,month,year,hour,minute;
			
			if( m[1] != "" )
			{
				day = m[1];
				month = m[2];
				year = m[3];
			}
			else if( m[4] != "" )
			{
				day = m[4];
				month = m[5];
				year = m[6];
			}
			else if( m[7] != "" )
			{
				day = m[7];
				month = m[8];
				year = m[9];
			}
			else if( m[10] != "" )
			{
				day = m[10];
				month = m[11];
				year = m[12];
			}
			
			month--;
			hour = m[13];
			minute = m[14];
			
			dt = new Date(year,month,day,hour,minute);
		}
		return dt;
	}
	function beginningCustomValidator_ClientValidate(sender, args)
	{
		var dt = parsedate(document.getElementById("<%=beginningTextBox.ClientID%>").value);
		
		if(dt == null)
		{
			args.IsValid = false;
		}				
		else if( dt <= new Date() )
		{
			sender.innerText = "Дата начала не должна быть прошедшей";
			args.IsValid = false;
		}
	}
	
	function endingCustomValidator_ClientValidate(sender, args)
	{
		if(Page_IsValid)
		{
			var dt1 = parsedate(document.getElementById("<%=beginningTextBox.ClientID%>").value);
			var dt2 = parsedate(document.getElementById("<%=endingTextBox.ClientID%>").value);
			
			if(dt2 == null)
			{
				args.IsValid = false;
			}
			else if(dt2 <= dt1)
			{
				sender.innerText = "Дата окончания должна быть больше даты начала";
				args.IsValid = false;
			}
		}
	}
</script>
