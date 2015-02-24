<%@ Page Language="c#" Inherits="Ne.Judge.EditProblemPage" CodeFile="editproblem.aspx.cs"
	MasterPageFile="~/design.master" Title="���������� ��������" %>

<%@ Register Src="UC/selectproblem.ascx" TagName="selectproblem" TagPrefix="nejudge" %>

<asp:Content ContentPlaceHolderID="Content" runat="server">
	<table>
		<tr>
			<td>
				<nejudge:selectproblem EveryContestTime="Forthcoming" ID="selprob" runat="server" Past="true" Current="true" Forthcoming="true" SpecialProblemName="--�������--" />
			</td>
			<td valign="bottom">
				<asp:Button ID="goButton" runat="server" Text="������!" OnClick="goButton_Click" /></td>
		</tr>
	</table>
	&nbsp;
	<asp:Menu ID="menu" runat="server" Orientation="Horizontal" OnMenuItemClick="menu_MenuItemClick">
		<Items>
			<asp:MenuItem Text="����������" Selected="True" Value="0" />
			<asp:MenuItem Text="�����" Value="1" />
		</Items>
	</asp:Menu>
	<asp:MultiView ID="mv" runat="server">
		<asp:View runat="server" ID="contentsParams" OnActivate="contentsParams_Activate" OnDeactivate="contentsParams_Deactivate">
			<table id="Table1" cellpadding="2" runat="server" border="0" width="100%">
				<tr>
					<td valign="top" align="right">�������� ������:</td>
					<td><asp:TextBox ID="nameTB" runat="server" Columns="60" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">����� ������:</td>
					<td><asp:TextBox ID="textTB" TextMode="MultiLine" Columns="50" Rows="15" runat="server" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">������ �����:</td>
					<td><asp:TextBox ID="inputFormatTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">������ ������:</td>
					<td><asp:TextBox ID="outputFormatTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">������ �����:</td>
					<td><asp:TextBox ID="inputSampleTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">������ ������:</td>
					<td><asp:TextBox ID="outputSampleTB" Columns="50" Rows="15" runat="server" TextMode="MultiLine" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">�����:</td>
					<td><asp:TextBox ID="authorTB" Columns="60" runat="server" /></td>
				</tr>
				<tr>
					<td valign="top" align="right">���������:</td>
					<td>
						<asp:GridView ID="hintsGV" runat="server" AutoGenerateColumns="False" EmptyDataText="�� ���� ��������� �� ����� ���������"
									OnRowCancelingEdit="hintsGV_RowCancelingEdit" OnRowDeleting="hintsGV_RowDeleting"
									OnRowEditing="hintsGV_RowEditing" OnRowUpdating="hintsGV_RowUpdating">
							<Columns>
								<asp:TemplateField HeaderText="���������">
									<ItemTemplate><%#Container.DataItem %></ItemTemplate>
								</asp:TemplateField>
								<asp:CommandField CancelText="��������" DeleteText="�������" EditText="�������������"
									HeaderText="��������������" InsertText="" NewText="" SelectText="" ShowDeleteButton="True"
									ShowEditButton="True" UpdateText="��������" />
								
							</Columns>
						</asp:GridView>
					</td>
					<td>
						<asp:TextBox ID="newHintTB" runat="server"></asp:TextBox>
						<asp:Button ID="addHintB" runat="server" Text="�������� ���������" OnClick="addHintB_Click" />
					</td>
				</tr>
			</table>
		</asp:View>
		<asp:View runat="server" ID="testsParams" OnActivate="testsParams_Activate" OnDeactivate="testsParams_Deactivate">
			XXX</asp:View>
	</asp:MultiView>
	<div style="text-align:center">
		<asp:Button ID="saveB" runat="server" Text="���������" OnClick="saveB_Click" />
	</div>
</asp:Content>
