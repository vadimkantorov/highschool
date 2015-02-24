<%@ Page Language="c#" Inherits="Ne.Judge.EditProblemPage" CodeFile="editproblem.aspx.cs"
	MasterPageFile="~/design.master" Title="Управление задачами" %>

<%@ Register Src="UC/selectproblem.ascx" TagName="selectproblem" TagPrefix="nejudge" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<table>
		<tr>
			<td>
				<nejudge:selectproblem EveryContestTime="Forthcoming" ID="selprob" runat="server" Past="true" Current="true" Forthcoming="true" SpecialProblemName="--Создать--" />
			</td>
			<td valign="bottom">
				<asp:Button ID="goButton" runat="server" Text="Вперед!" OnClick="goButton_Click" /></td>
		</tr>
	</table>
	&nbsp;
	<asp:Menu ID="menu" runat="server" Orientation="Horizontal" OnMenuItemClick="menu_MenuItemClick">
		<Items>
			<asp:MenuItem Text="Содержание" Selected="True" Value="0" />
			<asp:MenuItem Text="Тесты" Value="1" />
		</Items>
	</asp:Menu>
	<asp:MultiView ID="mv" runat="server">
		<asp:View runat="server" ID="contentsParams" OnActivate="contentsParams_Activate" OnDeactivate="contentsParams_Deactivate">
			<table id="Table1" cellpadding="2" runat="server" border="0" width="100%">
				<tr>
					<td valign="top" align="right">Название задачи:</td>
					<td><asp:TextBox ID="nameTB" runat="server" Columns="60" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Текст задачи:</td>
					<td><asp:TextBox ID="textTB" TextMode="MultiLine" Columns="50" Rows="15" runat="server" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Формат ввода:</td>
					<td><asp:TextBox ID="inputFormatTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Формат вывода:</td>
					<td><asp:TextBox ID="outputFormatTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Пример ввода:</td>
					<td><asp:TextBox ID="inputSampleTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Пример вывода:</td>
					<td><asp:TextBox ID="outputSampleTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Автор:</td>
					<td><asp:TextBox ID="authorTB" Columns="60" runat="server" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">Подсказки:</td>
					<td>
						<asp:GridView ID="hintsGV" runat="server" AutoGenerateColumns="False" EmptyDataText="Не было добавлено ни одной подсказки"
									OnRowCancelingEdit="hintsGV_RowCancelingEdit" OnRowDeleting="hintsGV_RowDeleting"
									OnRowEditing="hintsGV_RowEditing" OnRowUpdating="hintsGV_RowUpdating">
							<Columns>
								<asp:TemplateField HeaderText="Подсказка">
									<ItemTemplate><%#Container.DataItem %></ItemTemplate>
								</asp:TemplateField>
								<asp:CommandField CancelText="Отменить" DeleteText="Удалить" EditText="Редактировать"
									HeaderText="Редактирование" InsertText="" NewText="" SelectText="" ShowDeleteButton="True"
									ShowEditButton="True" UpdateText="Обновить" />
								
							</Columns>
						</asp:GridView>
					</td>
					<td>
						<asp:TextBox ID="newHintTB" runat="server"></asp:TextBox>
						<asp:Button ID="addHintB" runat="server" Text="Добавить подсказку" OnClick="addHintB_Click" />
					</td>
				</tr>
			</table>
		</asp:View>
		<asp:View runat="server" ID="testsParams" OnActivate="testsParams_Activate" OnDeactivate="testsParams_Deactivate">
			XXX</asp:View>
	</asp:MultiView>
	<div style="text-align:center">
		<asp:Button ID="saveB" runat="server" Text="Сохранить" OnClick="saveB_Click" />
	</div>
</asp:Content>
