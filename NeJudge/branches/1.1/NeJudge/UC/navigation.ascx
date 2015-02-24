<%@ Control Language="c#" AutoEventWireup="false" Codebehind="navigation.ascx.cs" Inherits="Ne.Judge.navigation" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" enableViewState="False"%>
<DIV class="nav_block" ID="anon_div" runat="server">
	<H3 class="nav_title">Аутентификация</H3>
	<UL class="nav_list">
		<asp:Repeater id="anon_repeater" runat="server">
			<ItemTemplate>
				<li>
					<asp:HyperLink runat="server" NavigateUrl=' <%# ((Link)Container.DataItem).Ref %> ' ID="Hyperlink3">
						<%# ((Link)Container.DataItem).Name  %>
					</asp:HyperLink>
				</li>
			</ItemTemplate>
		</asp:Repeater></UL>
</DIV>
<DIV class="nav_block" ID="common_div" runat="server">
	<H3 class="nav_title">NeJudge</H3>
	<UL class="nav_list">
		<asp:Repeater id="common_repeater" runat="server">
			<ItemTemplate>
				<li>
					<asp:HyperLink runat="server" NavigateUrl=' <%# ((Link)Container.DataItem).Ref %> '>
						<%# ((Link)Container.DataItem).Name  %>
					</asp:HyperLink>
				</li>
			</ItemTemplate>
		</asp:Repeater></UL>
</DIV>
<DIV class="nav_block" ID="account_div" runat="server">
	<H3 class="nav_title">Моя учетная запись</H3>
	<UL class="nav_list">
		<asp:Repeater id="account_repeater" runat="server">
			<ItemTemplate>
				<li>
					<asp:HyperLink runat="server" NavigateUrl=' <%# ((Link)Container.DataItem).Ref %> ' ID="Hyperlink1">
						<%# ((Link)Container.DataItem).Name  %>
					</asp:HyperLink>
				</li>
			</ItemTemplate>
		</asp:Repeater></UL>
</DIV>
<DIV class="nav_block" ID="admin_div" runat="server">
	<H3 class="nav_title">Администрирование</H3>
	<UL class="nav_list">
		<asp:Repeater id="admin_repeater" runat="server">
			<ItemTemplate>
				<li>
					<asp:HyperLink runat="server" NavigateUrl=' <%# ((Link)Container.DataItem).Ref %> ' ID="Hyperlink2">
						<%# ((Link)Container.DataItem).Name  %>
					</asp:HyperLink>
				</li>
			</ItemTemplate>
		</asp:Repeater></UL>
</DIV>
