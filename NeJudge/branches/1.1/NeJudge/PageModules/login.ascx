<%@ Control Language="c#" AutoEventWireup="false" Codebehind="login.ascx.cs" Inherits="Ne.Judge.WebForm1" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<%@ Register TagPrefix="uc1" TagName="ErrorMessage" Src="../UC/ErrorMessage.ascx" %>
<TABLE id="Table1" cellSpacing="5" cellPadding="1" border="0">
	<TR>
		<TD colspan="3">
			<h2 align="center">���� � ������� NeJudge</h2>
			���� �� ����� ���� ������ ����������� ������ � ����������, � �������� ������ 
			������� "anonymous", � ���� ������ ������ ������� �� ����. ���� �� �� 
			���������������� - <A href="edituser.aspx">�������� ���</A>
		</TD>
	</TR>
	<TR>
		<TD align="right">�����:</TD>
		<TD><asp:textbox id="usernameTextBox" tabIndex="1" runat="server" EnableViewState="False"></asp:textbox></TD>
	</TR>
	<TR>
		<TD align="right">������:</TD>
		<TD><asp:textbox id="passwordTextBox" tabIndex="2" runat="server" TextMode="Password" EnableViewState="False"></asp:textbox></TD>
	</TR>
	<tr>
		<td align="right">
			<asp:checkbox id="persistCheckBox" tabIndex="3" runat="server" Text="���������" EnableViewState="False"></asp:checkbox>
		</td>
		<td><asp:button id="loginButton" tabIndex="4" runat="server" Text="�����!" EnableViewState="False"></asp:button>
		</td>
	</tr>
</TABLE>
<div align="center"><uc1:ErrorMessage id="ErrorMessage" runat="server"></uc1:ErrorMessage></div>
