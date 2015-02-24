<%@ Control Language="C#" CodeFile="problemview.ascx.cs" Inherits="Ne.Judge.ProblemView" 
	EnableViewState="false"%>
<div style="text-align:center; width:100%">
	<asp:Literal id="nameLiteral" runat="server" /><br/>
	<asp:Literal id="tlLiteral" runat="server" Text="<strong>Ограничение времени:</strong> " /><br/>
	<asp:Literal id="mlLiteral" runat="server" Text="<strong>Ограничение памяти:</strong> " /><br/>
	<asp:Literal id="olLiteral" runat="server" Text="<strong>Ограничение на размер выходного файла:</strong> " /><br/><br/>
	<asp:Literal id="ifLiteral" runat="server" Text="<strong>Входной файл:</strong> " /><br/>
	<asp:Literal id="ofLiteral" runat="server" Text="<strong>Выходной файл:</strong> " />
</div>
<asp:Xml ID="problemXml" runat="server" TransformSource="~/Styles/transforms/problem.xsl" />