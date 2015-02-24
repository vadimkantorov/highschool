<%@ Page Language="C#" MasterPageFile="~/design.master" AutoEventWireup="true" CodeFile="quickstart.aspx.cs" Inherits="quickstart" Title="Untitled Page" %>

<%@ Register Src="UC/helpmessage.ascx" TagName="helpmessage" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="Content" Runat="Server">
<table>
<tr><td>Введите путь к провайдеру данных:</td><td>
	<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td></tr>
<tr><td>Введите имя сервера БД:</td><td>
	<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td></tr>
<tr><td>Введите имя пользователя БД:</td><td>
	<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox></td></tr>
<tr><td>Введите пароль пользователя БД:</td><td>
	<asp:TextBox ID="TextBox4" runat="server" TextMode="Password"></asp:TextBox></td></tr>
</table>
	<uc1:helpmessage ID="Helpmessage1" runat="server" Message="Help!!!" />
</asp:Content>

