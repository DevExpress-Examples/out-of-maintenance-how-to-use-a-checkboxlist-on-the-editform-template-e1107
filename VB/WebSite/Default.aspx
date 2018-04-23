<%-- BeginRegion Page setup --%>
<%@ Page Language="vb" AutoEventWireup="true"  CodeFile="Default.aspx.vb" Inherits="_Default" %>
<%@ Register assembly="DevExpress.Web.v13.1" namespace="DevExpress.Web.ASPxGridView" tagprefix="dxwgv" %>
<%-- EndRegion --%>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
	<title>How to add ListControl (CheckBoxList) in EditItemTemplate</title>
</head>
<body>
	<form id="form1" runat="server">
		<dxwgv:ASPxGridView ID="Grid" runat="server" KeyFieldName="ID" AutoGenerateColumns="false"
			OnRowUpdating="Grid_RowUpdating" >
			<SettingsEditing Mode="Inline" />
			<Columns>
				<dxwgv:GridViewCommandColumn VisibleIndex="0">
					<EditButton Visible="True" />                
				</dxwgv:GridViewCommandColumn>
				<dxwgv:GridViewDataTextColumn FieldName="Name" VisibleIndex="1" />
				<dxwgv:GridViewDataTextColumn FieldName="CategoriesString" Caption="Categories" VisibleIndex="2" >
				<%-- BeginRegion EditItemTemplate --%>
					<EditItemTemplate>
						<asp:CheckBoxList ID="List" runat="server" DataValueField="Name" DataSource="<%#Categories%>" OnDataBound="List_DataBound" />
					</EditItemTemplate>
				<%-- EndRegion --%>
				</dxwgv:GridViewDataTextColumn>
			</Columns>
		</dxwgv:ASPxGridView>
	</form>
</body>
</html>
