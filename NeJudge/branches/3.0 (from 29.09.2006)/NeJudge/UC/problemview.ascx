<%@ Control Language="C#" CodeFile="problemview.ascx.cs" Inherits="Ne.Judge.ProblemView" 
	EnableViewState="false"%>
<div style="text-align:center; width:100%">
	<asp:Literal id="nameLiteral" runat="server" /><br/>
	<asp:Literal id="tlLiteral" runat="server" Text="<strong>����������� �������:</strong> " /><br/>
	<asp:Literal id="mlLiteral" runat="server" Text="<strong>����������� ������:</strong> " /><br/>
	<asp:Literal id="olLiteral" runat="server" Text="<strong>����������� �� ������ ��������� �����:</strong> " /><br/><br/>
	<asp:Literal id="ifLiteral" runat="server" Text="<strong>������� ����:</strong> " /><br/>
	<asp:Literal id="ofLiteral" runat="server" Text="<strong>�������� ����:</strong> " />
</div>
<asp:Xml ID="problemXml" runat="server" TransformSource="~/Styles/transforms/problem.xsl" />