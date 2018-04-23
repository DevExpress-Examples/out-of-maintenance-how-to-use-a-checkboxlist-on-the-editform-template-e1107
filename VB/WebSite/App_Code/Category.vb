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

Public Class Category
	Private name_Renamed As String

	Public Sub New(ByVal name As String)
		Me.name_Renamed = name
	End Sub

	Public ReadOnly Property Name() As String
		Get
			Return name_Renamed
		End Get
	End Property
End Class
