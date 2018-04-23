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
Imports System.Text


Public Class Person
	Public Const CategorySeparator As String = ", "

	Private id_Renamed As Integer
	Private name_Renamed As String
	Private categories_Renamed As List(Of Category)

	Public Sub New(ByVal id As Integer, ByVal name As String, ParamArray ByVal categories() As Category)
		Me.id_Renamed = id
		Me.name_Renamed = name
		Me.categories_Renamed = New List(Of Category)(categories)
	End Sub

	Public ReadOnly Property ID() As Integer
		Get
			Return id_Renamed
		End Get
	End Property
	Public Property Name() As String
		Get
			Return name_Renamed
		End Get
		Set(ByVal value As String)
			name_Renamed = value
		End Set
	End Property
	Public ReadOnly Property Categories() As List(Of Category)
		Get
			Return categories_Renamed
		End Get
	End Property
	Public ReadOnly Property CategoriesString() As String
		Get
			Dim builder As New StringBuilder()
			For Each item As Category In Categories
				If builder.Length > 0 Then
					builder.Append(CategorySeparator)
				End If
				builder.Append(item.Name)
			Next item
			Return builder.ToString()
		End Get
	End Property
End Class
