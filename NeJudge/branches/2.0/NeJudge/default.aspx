<%@ Page Language="C#" MasterPageFile="~/design.master" AutoEventWireup="true" 
CodeFile="default.aspx.cs" Inherits="DefaultPage" Title="������� ��������" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<div align="center">
		<h1>
			����� ���������� � ����������� ������� NeJudge!
		</h1>
	</div>
	<asp:Literal runat="server" ID="anonymousBanner">
		��� ����, ����� ��������������� NeJudge, ����������, <a href="login.aspx">������� � �������</a>.<br />
		���� �� �� ���������������� -
		<a href="edituser.aspx">�������� ���</a>, ����� ��� ����� �������� ������ �������� ����������� ������������ � �����.
	</asp:Literal>
	<asp:Literal runat="server" ID="registeredBanner">
		����������� ������ <a href="contests.aspx">��������� ������������</a>, ��������
		������, � ��������� �������� ����� ������ ������� <a href="submit.aspx">�� ��������.</a><br/>
		� ����� ������ �� ������:
		<ul>
			<li><a href="monitor.aspx">����������� ������� ������ ������������,</a></li>
			<li><a href="status.aspx">����������� ������ ��������� ���� �������</a></li>
			<li><a href="ask.aspx">������ ������</a> �� ������.</li>
		</ul>
	</asp:Literal>
</asp:Content>
