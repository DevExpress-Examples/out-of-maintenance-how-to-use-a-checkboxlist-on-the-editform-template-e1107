Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Web.UI.HtmlControls
Imports System.Collections.Generic
Imports DevExpress.Web.Data
Imports System.Collections
Imports DevExpress.Web.ASPxGridView

Partial Public Class _Default
	Inherits System.Web.UI.Page

	Protected ReadOnly Property Persons() As List(Of Person)
		Get
			Const key As String = "DX1"
			If Session(key) Is Nothing Then
				Session(key) = CreatePersons()
			End If
			Return CType(Session(key), List(Of Person))
		End Get
	End Property
	Protected ReadOnly Property Categories() As List(Of Category)
		Get
			Const key As String = "DX2"
			If Session(key) Is Nothing Then
				Session(key) = CreateCategories()
			End If
			Return CType(Session(key), List(Of Category))
		End Get
	End Property

	Protected Overrides Sub OnInit(ByVal e As EventArgs)
		MyBase.OnInit(e)
		Grid.DataSource = Persons
		Grid.DataBind()
	End Sub

	Protected Sub Grid_RowUpdating(ByVal sender As Object, ByVal e As ASPxDataUpdatingEventArgs)
		Dim person As Person = FindPersonById(CInt(Fix(e.Keys(0))))
		person.Name = e.NewValues("Name").ToString()

		Dim list As CheckBoxList = CType(Grid.FindEditRowCellTemplateControl(CType(Grid.Columns(2), GridViewDataColumn), "List"), CheckBoxList)
		If Grid.IsCallback Then
			LoadListControlPostDataOnCallback(list)
		End If

		person.Categories.Clear()
		For Each item As ListItem In list.Items
			If item.Selected Then
				person.Categories.Add(FindCategoryByName(item.Value))
			End If
		Next item

		e.Cancel = True
		Grid.CancelEdit()
	End Sub

	Protected Function FindCategoryByName(ByVal categoryName As String) As Category
		For Each item As Category In Categories
			If item.Name = categoryName Then
				Return item
			End If
		Next item
		Return Nothing
	End Function
	Protected Function FindPersonById(ByVal id As Integer) As Person
		For Each item As Person In Persons
			If item.ID = id Then
				Return item
			End If
		Next item
		Return Nothing
	End Function

	Protected Function CreatePersons() As List(Of Person)
		Dim persons As List(Of Person) = New List(Of Person)()
		persons.Add(New Person(1, "Alex", Categories(1), Categories(2)))
		persons.Add(New Person(2, "Bill", Categories(0)))
		persons.Add(New Person(3, "Kate", Categories(2)))
		Return persons
	End Function
	Protected Function CreateCategories() As List(Of Category)
		Dim categories As List(Of Category) = New List(Of Category)()
		categories.Add(New Category("Family"))
		categories.Add(New Category("Friends"))
		categories.Add(New Category("Business"))
		Return categories
	End Function

	Protected Sub List_DataBound(ByVal sender As Object, ByVal e As EventArgs)
		Dim list As CheckBoxList = CType(sender, CheckBoxList)
		Dim container As GridViewEditItemTemplateContainer = CType(list.Parent, GridViewEditItemTemplateContainer)
		Dim hash As IDictionary = CreatePersonCategoriesHash(container.Grid.GetRowValues(container.VisibleIndex, "CategoriesString").ToString())
		For Each item As ListItem In list.Items
			item.Selected = hash.Contains(item.Value)
		Next item
	End Sub
	Private Function CreatePersonCategoriesHash(ByVal catString As String) As IDictionary
		Dim table As New Hashtable()
		Dim names() As String = catString.Split(New String() { Person.CategorySeparator }, StringSplitOptions.None)
		For Each name As String In names
			table.Add(name, Nothing)
		Next name
		Return table
	End Function

	' workaround for std ListControl LoadPostData
	Private Sub LoadListControlPostDataOnCallback(ByVal control As ListControl)
		If (Not Grid.IsEditing) Then
			Return
		End If
		For Each item As ListItem In control.Items
			item.Selected = False
		Next item
		For Each key As String In Request.Params.AllKeys
			Dim dataHandler As IPostBackDataHandler = TryCast(control, IPostBackDataHandler)
			If key.StartsWith(control.UniqueID) Then
				dataHandler.LoadPostData(key, Request.Params)
			End If
		Next key
	End Sub
End Class
