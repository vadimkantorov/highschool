<%@ Control Language="c#" Inherits="Ne.Judge.SelectContest" CodeFile="selectcontest.ascx.cs" %>
<%@ Register Src="errormessage.ascx" TagName="errormessage" TagPrefix="nejudge" %>
<table class="selcon">

	<script type="text/javascript">
		function navigateToContest()
		{
			var contestsDDL = document.getElementById('<%=contestsDDL.ClientID%>');
			document.location.href = '<%=Address%>?contestID=' + contestsDDL.options[contestsDDL.selectedIndex].value;
		}
	</script>

	<tr>
		<td>
			<h3>
				Выбрать соревнование:</h3>
		</td>
	</tr>
	<tr id="errmessTR" runat="server" visible="false"><td>Нет соревнований для отображения</td></tr>
	<tr id="ddlTR" runat="server">
		<td>
			<asp:DropDownList ID="contestsDDL" runat="server" DataTextField="Name" DataValueField="ID" />
			<input id="Button1" type="button" value="Вперёд!" onclick="navigateToContest()" /></td>
	</tr>
</table>
