<%@ Page Language="c#" Inherits="Ne.Judge.LoginPage" EnableViewState="False" CodeFile="login.aspx.cs"
	MasterPageFile="~/design.master" Title="Вход в систему" %>
<%@ Register TagPrefix="nejudge" TagName="ErrorMessage" Src="~/UC/ErrorMessage.ascx" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<table id="Table1" cellspacing="5" cellpadding="1" border="0">
		<tr>
			<td colspan="3">
				<h2 align="center">
					Вход в систему NeJudge
				</h2>
			</td>
		</tr>
		<tr>
			<td align="right">
				Логин:</td>
			<td>
				<asp:TextBox ID="usernameTextBox" TabIndex="1" runat="server" EnableViewState="False"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="right">
				Пароль:</td>
			<td>
				<asp:TextBox ID="passwordTextBox" TabIndex="2" runat="server" TextMode="Password"
					EnableViewState="False"></asp:TextBox></td>
		</tr>
		<tr>
			<td align="right">
				<asp:CheckBox ID="persistCheckBox" TabIndex="3" runat="server" Text="Запомнить" EnableViewState="False">
				</asp:CheckBox>
			</td>
			<td>
				<asp:Button ID="loginButton" TabIndex="4" runat="server" Text="Войти!" EnableViewState="False"
					OnClick="loginButton_Click"></asp:Button>
			</td>
		</tr>
		<tr>
		<td colspan="3"><nejudge:ErrorMessage ID="ErrorMessage" runat="server"></nejudge:ErrorMessage></td>
		</tr>
	</table>
</asp:Content>
