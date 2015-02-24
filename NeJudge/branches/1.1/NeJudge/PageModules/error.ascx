<%@ Control Language="c#" AutoEventWireup="false" Codebehind="error.ascx.cs" Inherits="Ne.Judge.errorpage" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False" %>
<div style="TEXT-ALIGN:center">
	<asp:Literal ID="mesLiteral" Runat="server" Text='<H2 style="FONT-SIZE: 14pt; COLOR: red">На сервере произошла неисправимая ошибка.</H2>Сведения о ней были записаны и переданы разработчикам. Пожалуйста, попробуйте зайти на эту же страницу позднее.'></asp:Literal>
	<asp:panel id="panelDetailedError" runat="Server">
		<H1>Дополнительная информация</H1>
		<STRONG>Дата и время исключения:</STRONG>
		<asp:literal id="litErrorDate" runat="Server"></asp:literal>
		<BR>
		<STRONG>Сообщение исключения:</STRONG>
		<asp:literal id="litMessage" runat="Server"></asp:literal>
		<BR>
		<STRONG>Источник исключения:</STRONG>
		<asp:literal id="litSource" runat="Server"></asp:literal>
		<BR>
		<STRONG>Тип исключения:</STRONG>
		<asp:literal id="litErrorType" runat="server"></asp:literal>
		<BR>
		<BR>
		<STRONG>Stack Trace:</STRONG><BR>
		<DIV align="left">
			<asp:literal id="litStackTrace" runat="Server"></asp:literal></DIV>
	</asp:panel>
</div>
