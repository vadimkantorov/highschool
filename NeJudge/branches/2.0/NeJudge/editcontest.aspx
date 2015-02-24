<%@ Page Language="c#" Inherits="Ne.Judge.EditContestPage" CodeFile="editcontest.aspx.cs"
	MasterPageFile="~/design.master" Title="Управление соревнованями" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<!--<style>
		.MenuCell
		{
					border-color: Black;
					border-width: 1px;
					border-style:solid;
					background-color: WhiteSmoke;
					color: Black;
					text-align: center;
					height:25px;
					font-size: x-small;
		}

		.MenuCellSelected
		{
					border-top-color:Black;
					border-left-color:Black;
					border-right-color:Black;
					border-bottom-color:Gainsboro;
					border-width:1px;
					background-color:Gainsboro;
					font-weight:bold;
					font-size: x-small;
		}

		.MenuCellHover
		{
					background-color: LightSteelBlue;
					font-size: x-small;
		}

		.Canvas
		{
					background-color: Gainsboro;
					font-size: x-small;
					border-left: 1px solid black;
					border-bottom: 1px solid black;
					border-right: 1px solid black;
		}
	</style>-->
	<asp:Menu ID="menu" runat="server" OnMenuItemClick="menu_MenuItemClick" Orientation="Horizontal">
		<Items>
			<asp:MenuItem Text="Основные параметры" Value="0" />
			<asp:MenuItem Text="Задачи" Value="1" />
			<asp:MenuItem Text="Права и участники" Value="2" />
		</Items>
		<%-- <StaticMenuItemStyle CssClass="MenuCell" ItemSpacing="0px" />
		<StaticHoverStyle CssClass="MenuCellHover" />
		<`StaticSelectedStyle CssClass="MenuCellSelected" ItemSpacing="0px" />--%>
	</asp:Menu>
	<asp:MultiView ID="mv" runat="server">
		<asp:View runat="server" ID="contestParams" OnActivate="contestParams_Activate" OnDeactivate="contestParams_Deactivate">
			<table width="100%">
				<tr>
					<td style="height: 38px">Название:</td>
					<td style="height: 38px"><asp:TextBox ID="nameTB" runat="server" MaxLength="100" ValidationGroup="Name" /></td>
					<td style="height: 38px">
						<asp:RequiredFieldValidator ID="nameRFV" runat="server" ControlToValidate="nameTB"
							ErrorMessage="Это поле обязательно для заполнения" ValidationGroup="Name" />
						<asp:RegularExpressionValidator ID="nameREV" runat="server" ControlToValidate="nameTB"
							ErrorMessage="Длина названия не должна превосходить 100" ValidationGroup="Name"
							ValidationExpression="\w{1,100}" /></td>
				</tr>
				<tr>
					<td>Дата начала:</td>
					<td>
						<asp:DropDownList ID="daysDDL" runat="server" ValidationGroup="Beginning" />
						<asp:DropDownList ID="monthsDDL" runat="server">
							<asp:ListItem Text="января" Value="1" />
							<asp:ListItem Text="февраля" Value="2" />
							<asp:ListItem Text="марта" Value="3" />
							<asp:ListItem Text="апреля" Value="4" />
							<asp:ListItem Text="мая" Value="5" />
							<asp:ListItem Text="июня" Value="6" />
							<asp:ListItem Text="июля" Value="7" />
							<asp:ListItem Text="августа" Value="8" />
							<asp:ListItem Text="сентября" Value="9" />
							<asp:ListItem Text="октября" Value="10" />
							<asp:ListItem Text="ноября" Value="11" />
							<asp:ListItem Text="декабря" Value="12" />
						</asp:DropDownList>
						<asp:DropDownList ID="yearsDDL" runat="server" />&nbsp;
						<asp:DropDownList ID="hoursDDL" runat="server" ValidationGroup="Beginning" />&nbsp;час(а)
						<asp:TextBox ID="minutesTB" runat="server" Columns="2" ValidationGroup="Beginning" />&nbsp;минут(ы)
					</td>
					<td>
						<asp:RequiredFieldValidator ID="beginningRFV" runat="server" ControlToValidate="minutesTB" ValidationGroup="Beginning"
							ErrorMessage="В поле минут необходимо ввести число от 00 до 59" />
						<asp:RegularExpressionValidator	ID="beginningREV" runat="server" ControlToValidate="minutesTB" ValidationGroup="Beginning"
							ErrorMessage="В поле минут необходимо ввести число от 00 до 59" ValidationExpression="[0-5]\d" />
						<asp:CustomValidator ID="beginningCV" runat="server" OnServerValidate="beginningCV_ServerValidate" ValidationGroup="Beginning"
							ErrorMessage="Дата начала должна быть больше времени сервера на момент сохранения" /></td>
				</tr>
				<tr>
					<td><%--<asp:CheckBox ID="durCB" runat="server" />--%>Продолжительность:</td>
					<td>
						<asp:TextBox ID="durHoursTB" runat="server" Columns="2" ValidationGroup="Duration" />&nbsp;час(а)
						<asp:TextBox ID="durMinutesTB" runat="server" Columns="2" ValidationGroup="Duration" />&nbsp;минут(ы)
					</td>
					<td>
						<asp:RequiredFieldValidator ID="durHoursRFV" runat="server" ControlToValidate="durHoursTB"
							ErrorMessage="В поле часов необходимо ввести число от 00 до 23" ValidationGroup="Duration" />
						<asp:RegularExpressionValidator	ID="durHoursREV" runat="server" ControlToValidate="durHoursTB" ValidationGroup="Duration"
							ErrorMessage="В поле часов необходимо ввести число от 00 до 23" ValidationExpression="([01]\d)|(2[0-3])" />
						<asp:RequiredFieldValidator ID="durMintesRFV" runat="server" ControlToValidate="durMinutesTB"
							ErrorMessage="В поле минут необходимо ввести число от 00 до 59" ValidationGroup="Duration" />
						<asp:RegularExpressionValidator	ID="durMinutesREV" runat="server" ControlToValidate="durMinutesTB" ValidationGroup="Duration"
							ErrorMessage="В поле минут необходимо ввести число от 00 до 59" ValidationExpression="[0-5]\d" /></td>
				</tr>
				<tr>
					<td><asp:CheckBox ID="freezeTB" runat="server" />Заморозить монитор за:</td>
					<td>
						<asp:TextBox ID="freezeHoursTB" runat="server" Columns="2" />&nbsp;час(а)
						<asp:TextBox ID="freezeMinutesTB" runat="server" Columns="2" />&nbsp;минут(ы)&nbsp;до&nbsp;конца
					</td>
				</tr>
			</table>
			<input type="hidden" id="dayF" runat="server" /> 
			<script type="text/javascript">
			var yearsDDL = document.getElementById("<%=yearsDDL.ClientID%>");
			var daysDDL = document.getElementById("<%=daysDDL.ClientID%>");
			var monthsDDL = document.getElementById("<%=monthsDDL.ClientID%>");
			var dayF = document.getElementById("<%=dayF.ClientID%>");
			
			function SelectedMonthChanged(item)
			{
				//debugger;
				var n;
				if(item.value == 2)
				{
					var year = yearsDDL.options[yearsDDL.selectedIndex].value;
					n = 28;
					if(year % 4 == 0)
					{
						if (year % 100 == 0)
						{
							if(year % 400 == 0)
								n = 29;
						}
						else
							n = 29;
					}
				}
				else
				{
					var ms = new Array(31,28,31,30,31,30,31,31,30,31,30,31);
					n = ms[monthsDDL.options[monthsDDL.selectedIndex].value-1];
				}
				var day = dayF.value;
				daysDDL.options.length = 0;
				for(var d = 1; d <= n; d++)
				{
					var option = document.createElement("option");
					option.value = d;
					option.innerText = d;
					if(option.value == day)
						option.selected = true;
					daysDDL.appendChild(option);
					if(option.value == day)
						daysDDL.selectedIndex = d-1;
				}
			}
			SelectedMonthChanged(monthsDDL.options[monthsDDL.selectedIndex]);
			</script>
		</asp:View>
		<asp:View runat="server" ID="problemsParams" OnActivate="problemsParams_Activate">
			<asp:GridView ID="problemsGV" runat="server" AutoGenerateColumns="False" EmptyDataText="Не было добавлено ни одной задачи" Width="100%" >
				<Columns>
					<asp:BoundField HeaderText="ID" DataField="ID" />
					<asp:BoundField HeaderText="Короткое название" DataField="ShortName" />
					<asp:HyperLinkField DataNavigateUrlFields="ID" DataNavigateUrlFormatString="editproblem2.aspx?mode=edit&problemID={0}"
						DataTextField="Name" HeaderText="Название" />
					<asp:TemplateField HeaderText="Редактирование" ShowHeader="False">
						<ItemTemplate>
							<asp:LinkButton ID="upLB" runat="server" CausesValidation="False" CommandName="Up" Text="▲" OnClick="upLB_Click" />
							<asp:LinkButton ID="deleteLB" runat="server" CausesValidation="False" CommandName="Delete" Text="X" OnClick="deleteLB_Click" />
							<asp:LinkButton ID="downLB" runat="server" CausesValidation="False" CommandName="Down" Text="▼" OnClick="downLB_Click" />
						</ItemTemplate>
					</asp:TemplateField>
				</Columns>
			</asp:GridView>
			<div style="text-align:center">
				<script type="text/javascript">
				function addProblem()
				{
					document.location.href = "editproblem2.aspx?mode=create&contestID=<%=contest.ID%>";
				}
				</script>
				<button id="addProblemB" runat="server" onclick="addProblem()">Добавить задачу</button>
			</div>
		</asp:View>
		<asp:View runat="server" ID="rightsAndParticipantsParams" OnActivate="rightsAndParticipantsParams_Activate" OnDeactivate="rightsAndParticipantsParams_Deactivate">
		</asp:View>
	</asp:MultiView>
	<div style="text-align:center">
		<asp:Button ID="saveB" runat="server" OnClick="saveB_Click" Text="Сохранить" />
	</div>
	</asp:Content>
