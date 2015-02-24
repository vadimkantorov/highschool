<%@ Register TagPrefix="uc1" TagName="selectcontest" Src="../UC/selectcontest.ascx" %>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="contest.ascx.cs" Inherits="Ne.Judge.contest" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="uc1" TagName="ErrorMessage" Src="../UC/ErrorMessage.ascx" %>
<uc1:selectcontest id="selcon" EnableViewState="true" Address="contest.aspx" runat="server" Old="true"
	Now="true" Future="true"></uc1:selectcontest>
<uc1:ErrorMessage id="ErrorMessage" runat="server" EnableViewState="False"></uc1:ErrorMessage>
<asp:repeater id="Repeater1" runat="server" EnableViewState="False">
	<HeaderTemplate>
		<br>
		<table class="grid" border="0" cellpadding="5" cellspacing="0" width="100%">
			<tr class="grid_head">
				<TH width="5%">
					PID</TH>
				<th width="25%">
					Короткое название</th>
				<TH>
					Название</TH>
			</tr>
	</HeaderTemplate>
	<ItemTemplate>
		<tr class="grid_first">
			<td><%#DataBinder.Eval(Container.DataItem, "PID")%></td>
			<td><%#DataBinder.Eval(Container.DataItem, "ShortName")%></td>
			<td>
				<asp:HyperLink ID="HyperLink1" Runat="server" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "Url")%>'>
					<%#DataBinder.Eval(Container.DataItem, "Name")%>
				</asp:HyperLink></td>
		</tr>
	</ItemTemplate>
	<FooterTemplate>
		</table>
	</FooterTemplate>
</asp:repeater>
<asp:hyperlink id="printHyperLink" runat="server" NavigateUrl="~/printcontest.aspx?tid=" EnableViewState="False">Версия для печати</asp:hyperlink>&nbsp;
<asp:hyperlink id="monHyperLink" runat="server" NavigateUrl="~/monitor.aspx?tid=" EnableViewState="False">Результаты</asp:hyperlink>&nbsp;
<asp:hyperlink id="subHyperLink" runat="server" NavigateUrl="~/status.aspx?tid=" EnableViewState="False">Submissions</asp:hyperlink>&nbsp;
<asp:hyperlink id="quesHyperLink" runat="server" NavigateUrl="~/questions.aspx?tid=" EnableViewState="False">Вопросы</asp:hyperlink>&nbsp;&nbsp;
<asp:hyperlink id="editHyperLink" runat="server" NavigateUrl="~/editcontest.aspx?tid=" EnableViewState="False"
	Visible="False">Редактировать</asp:hyperlink>
