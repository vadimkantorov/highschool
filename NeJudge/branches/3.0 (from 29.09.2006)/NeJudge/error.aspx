<%@ Page Language="c#" Inherits="Ne.Judge.ErrorPage" EnableViewState="False" CodeFile="error.aspx.cs"
	MasterPageFile="~/design.master" Title="������ �������" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<div style="text-align: center">
		<asp:Literal ID="mesLiteral" runat="server" Text='<H2 style="FONT-SIZE: 14pt; COLOR: red">�� ������� ��������� ������������ ������.</H2>�������� � ��� ���� �������� � �������� �������������. ����������, ���������� ����� �� ��� �� �������� �������.'></asp:Literal>
		<asp:Panel ID="panelDetailedError" runat="Server">
			<h1>�������������� ����������</h1>
			<strong>���� � ����� ����������:</strong><asp:Literal ID="litErrorDate" runat="Server" /><br/>
			<strong>��������� ����������:</strong><asp:Literal ID="litMessage" runat="Server" /><br/>
			<strong>�������� ����������:</strong><asp:Literal ID="litSource" runat="Server" /><br/>
			<strong>��� ����������:</strong><asp:Literal ID="litErrorType" runat="server" /><br/><br/>
			<strong>Stack Trace:</strong><br/><div align="left"><asp:Literal ID="litStackTrace" runat="Server" /></div>
		</asp:Panel>
	</div>
</asp:Content>