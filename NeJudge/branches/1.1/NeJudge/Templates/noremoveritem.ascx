<%@ Control %>
<tr>
	<td><asp:Literal runat="server" ID="PID" Text='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem, "PID")%>' /></td>
	<td><%#DataBinder.Eval(((RepeaterItem)Container).DataItem, "ShortName")%></td>
	<td>
		<asp:HyperLink ID="Hyperlink2" runat="server" NavigateUrl='<%#DataBinder.Eval(((RepeaterItem)Container).DataItem, "Url")%>'>
			<%#DataBinder.Eval(((RepeaterItem)Container).DataItem, "Name")%>
		</asp:HyperLink>
	</td>
	<td><%#DataBinder.Eval(((RepeaterItem)Container).DataItem, "Text")%></td>
</tr>