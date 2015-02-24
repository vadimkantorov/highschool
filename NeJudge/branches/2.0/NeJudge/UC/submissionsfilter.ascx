<%@ Control Language="c#" Inherits="Ne.Judge.SubmissionsFilterControl"
CodeFile="SubmissionsFilter.ascx.cs" %>
<%@ Register Src="selectproblem.ascx" TagName="selectproblem" TagPrefix="nejudge" %>
<%@ Register TagPrefix="nejudge" TagName="helpmsg" Src="../UC/HelpMessage.ascx" %>
<table id="smaintable">
	<tr>
		<td colspan="2">
			<nejudge:selectproblem EveryContestTime="Current,Forthcoming" Past="true" Current="true"
			 ID="selprob" runat="server" SpecialProblemName="--Любая--" />
		</td>
	</tr>
	<tr>
		<td>Результат:</td>
		<td><asp:DropDownList ID="outcomesDDL" Runat="server" /></td>
	</tr>
	<tr id="userTR" runat="server">
		<td>Пользователь:</td>
		<td><asp:DropDownList ID="usersDDL" Runat="server" /></td>
	</tr>
	<tr>
		<td align="center" colspan="2">
			<asp:Button ID="filterButton" Runat="server" Text="Вперед!" OnClick="filterButton_Click" />
		</td>
	</tr>
</table>
<script type="text/javascript">
var srows = document.getElementById("smaintable").rows;
tohideArray.concat(srows[1],srows[2],srows[3]);

function ProcessOutcomeCallbackResult(result, context)
{
	//debugger;
	var outcomes = document.getElementById("<%=outcomesDDL.ClientID%>");
	var outcome = "<%=filt.Outcome%>";
	outcomes.length = 0;
	var rows = result.split('|'); 
	for (var i = 0; i < rows.length; i++)
	{
		if(rows[i])
		{
			var values = rows[i].split('^');
			var option = document.createElement("option");
			option.innerHTML = values[0];
			option.value = values[1];
			
			//TODO: ??? IE bug?
			if(option.value == outcome)
				option.selected = true;
			outcomes.appendChild(option);
			if(option.value == outcome)
				outcomes.selectedIndex = i;
		}
	}
}

</script>