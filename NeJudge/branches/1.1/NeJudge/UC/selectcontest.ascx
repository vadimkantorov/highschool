<%@ Control Language="c#" AutoEventWireup="false" Codebehind="selectcontest.ascx.cs" Inherits="Ne.Judge.SelectContest" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table class="selcon">
	<tr>
		<td>
			<h3>������� ������������:</h3>
		</td>
	</tr>
	<tr>
		<td>
			<asp:dropdownlist id="tidDropDownList" runat="server"></asp:dropdownlist>
			<asp:button id="goButton" EnableViewState="False" Text="�����!" runat="server"></asp:button>
		</td>
	</tr>
</table>
