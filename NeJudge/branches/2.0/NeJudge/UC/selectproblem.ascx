<%@ Control Language="c#" Inherits="Ne.Judge.SelectProblem" CodeFile="selectproblem.ascx.cs" %>
<table cellspacing="0" cellpadding="5" border="0" id="maintable">
	<tr>
		<td>Соревнования:</td>
		<td><asp:DropDownList ID="contestsDDL" runat="server" DataTextField="Name" DataValueField="ID" /></td>
	</tr>
	<tr>
		<td><span id="prspan">Задача:</span></td>
		<td><asp:DropDownList ID="problemsDDL" runat="server" DataTextField="Name" DataValueField="ID" /></td>
	</tr>
</table>
<script type="text/javascript">
var tohideArray = new Array(document.getElementById("tohide"),document.getElementById("maintable").rows[0]);

function HideControl(control)
{
	if(control)
		control.style.display = "";
}

function UnHideControl(control)
{
	if(control)
		control.style.display = "none";
}

function PrrintMessage(str)
{
	var prs = document.getElementById("<%=problemsDDL.ClientID%>").style.display = "none";
	document.getElementById("prspan").innerHTML = str;
	if(tohideArray[0])
	{
		tohideArray[0].style.display = "none"
		control.style.display = "none";
	}
}

function ClearMessage()
{
	var prs = document.getElementById("<%=problemsDDL.ClientID%>").style.display = "";
	document.getElementById("prspan").innerHTML = "Задача:";
	if(tohideArray[0])
		tohideArray[0].style.display = "";
}
function ProcessCallbackResult(result, context)
{
	//debugger;
	var prs = document.getElementById("<%=problemsDDL.ClientID%>");
	var prob = <%=prob%>;			
	prs.length = 0;
	if (!result)
		PrintMessage("Нет задач для отображения");
	else
	{
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
				if(option.value == prob)
					option.selected = true;
				prs.appendChild(option);
				if(option.value == prob)
					prs.selectedIndex = i;
			}
		}
		ClearMessage();
	}
}

function CheckContestLack()
{
	if(document.getElementById("<%=contestsDDL.ClientID%>").length == 0)
	{
		PrintMessage("Нет соревнований для отображения");
		for(var i = 0; i < tohideArray.length; i++)
			HideControl(tohideArray[i]);
	}
}
</script>
