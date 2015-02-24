<%@ Page Language="c#" Inherits="Ne.Judge.ErrorPage" EnableViewState="False" CodeFile="error.aspx.cs"
	MasterPageFile="~/design.master" Title="Ошибка сервера" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<div style="text-align: center">
		<asp:Literal ID="mesLiteral" runat="server" Text='<H2 style="FONT-SIZE: 14pt; COLOR: red">На сервере произошла неисправимая ошибка.</H2>Сведения о ней были записаны и переданы разработчикам. Пожалуйста, попробуйте зайти на эту же страницу позднее.'></asp:Literal>
		<asp:Panel ID="panelDetailedError" runat="Server">
			<h1>Дополнительная информация</h1>
			<strong>Дата и время исключения:</strong><asp:Literal ID="litErrorDate" runat="Server" /><br/>
			<strong>Сообщение исключения:</strong><asp:Literal ID="litMessage" runat="Server" /><br/>
			<strong>Источник исключения:</strong><asp:Literal ID="litSource" runat="Server" /><br/>
			<strong>Тип исключения:</strong><asp:Literal ID="litErrorType" runat="server" /><br/><br/>
			<strong>Stack Trace:</strong><br/><div align="left"><asp:Literal ID="litStackTrace" runat="Server" /></div>
		</asp:Panel>
	</div>
</asp:Content>