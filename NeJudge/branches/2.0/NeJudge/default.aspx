<%@ Page Language="C#" MasterPageFile="~/design.master" AutoEventWireup="true" 
CodeFile="default.aspx.cs" Inherits="DefaultPage" Title="√лавна€ страница" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<div align="center">
		<h1>
			ƒобро пожаловать в провер€ющую систему NeJudge!
		</h1>
	</div>
	<asp:Literal runat="server" ID="anonymousBanner">
		ƒл€ того, чтобы воспользоватьс€ NeJudge, пожалуйста, <a href="login.aspx">войдите в систему</a>.<br />
		≈сли вы не зарегистрированы -
		<a href="edituser.aspx">сделайте это</a>, иначе вам будут доступны только просмотр результатов соревнований и задач.
	</asp:Literal>
	<asp:Literal runat="server" ID="registeredBanner">
		ѕросмотрите список <a href="contests.aspx">доступных соревнований</a>, выберите
		задачу, и отправьте исходный текст вашего решени€ <a href="submit.aspx">на проверку.</a><br/>
		¬ любой момент вы можете:
		<ul>
			<li><a href="monitor.aspx">просмотреть монитор любого соревновани€,</a></li>
			<li><a href="status.aspx">просмотреть список посланных вами решений</a></li>
			<li><a href="ask.aspx">задать вопрос</a> по задаче.</li>
		</ul>
	</asp:Literal>
</asp:Content>
