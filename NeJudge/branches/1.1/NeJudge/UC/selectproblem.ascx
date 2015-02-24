<%@ Control Language="c#" AutoEventWireup="false" Codebehind="selectproblem.ascx.cs" Inherits="Ne.Judge.SelectProblem" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<script language="javascript">
			function Problem(name,pid,tid)
			{
				this.name = name;
				this.pid = pid;
				this.tid = tid;
			}
			function Hide(str)
			{
				document.getElementById("selProblems").style.display = "none";
				document.getElementById("prspan").innerHTML = str;
				for(i = 0; i < hi.length; i++)
					if(hi[i] != null)
						document.getElementById(hi[i]).style.display = "none";
			}
			function UnHide()
			{
				document.getElementById("selProblems").style.display = "";
				document.getElementById("prspan").innerHTML = "Задача:";
				for(i = 0; i < hi.length; i++)
					if(hi[i] != null)
						document.getElementById(hi[i]).style.display = "";
			}
			function SelectedContestChanged()
			{
				var index = document.getElementById("<%=selContests.ClientID%>").options.selectedIndex;
				var tid = document.getElementById("<%=selContests.ClientID%>").options[index].value;
				document.getElementById("selProblems").options.length = 0;
				for (i = 0; i < arr.length; i++)
				{
					if(arr[i].tid == tid)
					{
						var oCityOption = document.createElement("OPTION");
						oCityOption.text = arr[i].name;
						oCityOption.value = arr[i].pid;
						document.getElementById("selProblems").options.add(oCityOption);
					}
				}
				if(document.getElementById("selProblems").options.length == 0)
				{
					Hide("В этом соревновании нет задач");
				}
				else
				{
					UnHide();
				}
				SelectedProblemChanged();
			}
			function SelectedProblemChanged()
			{
				var index = document.getElementById("selProblems").options.selectedIndex;
				if(index != -1)
					document.getElementById("<%=pidField.ClientID%>").value = document.getElementById("selProblems").options[index].value;
			}
			function Init()
			{
				var pid = document.getElementById("<%=pidField.ClientID%>").value;
				var tid;
				for (i = 0; i < arr.length; i++)
					if(arr[i].pid == pid)
					{
						tid = arr[i].tid;
						break;
					}
				for (i = 0; i < document.getElementById("<%=selContests.ClientID%>").options.length; i++)
					if(document.getElementById("<%=selContests.ClientID%>").options[i].value == tid)
					{
						document.getElementById("<%=selContests.ClientID%>").options.selectedIndex = i;
						break;
					}
				SelectedContestChanged();
				var ar = new Array();
				for (i = 0; i < arr.length; i++)
					if(arr[i].tid == tid)
						ar.push(arr[i]);
				document.getElementById("selProblems").options.selectedIndex = -1;
				for (i = 0; i < ar.length; i++)
					if(ar[i].pid == pid)
					{
						document.getElementById("selProblems").options.selectedIndex = i;
						break;
					}
				if(document.getElementById("selProblems").options.selectedIndex == -1)
					document.getElementById("selProblems").options.selectedIndex = 0;
				SelectedProblemChanged();
			}
</script>
<TABLE id="Table1" cellSpacing="0" cellPadding="5" border="0" runat="server">
	<TR>
		<TD>Соревнования:</TD>
		<TD><SELECT id="selContests" onchange="SelectedContestChanged()" runat="server"></SELECT></TD>
	</TR>
	<TR>
		<TD><span id="prspan">Задача:</span></TD>
		<TD><SELECT id="selProblems" onchange="SelectedProblemChanged()"></SELECT></TD>
	</TR>
</TABLE>
<INPUT id="pidField" type="hidden" runat="server">
