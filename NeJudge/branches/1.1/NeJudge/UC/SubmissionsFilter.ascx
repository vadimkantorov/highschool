<%@ Control Language="c#" AutoEventWireup="false" Codebehind="SubmissionsFilter.ascx.cs" Inherits="Ne.Judge.SubmissionsFilter" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"
EnableViewState="True" %>
<%@ Register TagPrefix="uc1" TagName="helpmsg" Src="../UC/HelpMessage.ascx" %>
<table>
	<tr>
		<td>
			������������:
			<asp:DropDownList EnableViewState="true" ID="contestDropDownList" Runat="server"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td id="userTd" runat="server">
			������������:
			<asp:DropDownList EnableViewState="true" ID="userDropDownList" Runat="server"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td>
			������:
			<asp:DropDownList EnableViewState="true" ID="snameDropDownList" Runat="server"></asp:DropDownList>
			<asp:Literal id="noSuchProb" runat="server" Text="<font color='red'>����� ������ ��� � ������� ������������</font>"
				Visible="False"></asp:Literal>
		</td>
	</tr>
	<tr>
		<td>
			���������:
			<asp:DropDownList EnableViewState="true" ID="resultDropDownList" Runat="server"></asp:DropDownList>
		</td>
	</tr>
	<tr>
		<td><asp:Button EnableViewState="True" ID="filterButton" Runat="server" Text="������!"></asp:Button></td>
	</tr>
</table>
