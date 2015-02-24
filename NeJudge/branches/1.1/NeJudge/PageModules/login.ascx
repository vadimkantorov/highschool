<%@ Control Language="c#" AutoEventWireup="false" Codebehind="login.ascx.cs" Inherits="Ne.Judge.WebForm1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="uc1" TagName="ErrorMessage" Src="../UC/ErrorMessage.ascx" %>
<TABLE id="Table1" cellSpacing="5" cellPadding="1" border="0">
	<TR>
		<TD colspan="3">
			<h2 align="center">Вход в систему NeJudge</h2>
			Если вы всего лишь хотите просмотреть задачи и результаты, в качестве логина 
			введите "anonymous", в поле пароля ничего вводить не надо. Если Вы не 
			зарегистрированы - <A href="edituser.aspx">сделайте это</A>
		</TD>
	</TR>
	<TR>
		<TD align="right">Логин:</TD>
		<TD><asp:textbox id="usernameTextBox" tabIndex="1" runat="server" EnableViewState="False"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="right">Пароль:</TD>
		<TD><asp:textbox id="passwordTextBox" tabIndex="2" runat="server" TextMode="Password" EnableViewState="False"></asp:textbox></TD>
	</TR>
	<tr>
		<td align="right">
			<asp:checkbox id="persistCheckBox" tabIndex="3" runat="server" Text="Запомнить" EnableViewState="False"></asp:checkbox>
		</td>
		<td><asp:button id="loginButton" tabIndex="4" runat="server" Text="Войти!" EnableViewState="False"></asp:button>
		</td>
	</tr>
</TABLE>
<div align="center"><uc1:ErrorMessage id="ErrorMessage" runat="server"></uc1:ErrorMessage></div>
