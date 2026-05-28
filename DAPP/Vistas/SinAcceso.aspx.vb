Public Class SinAcceso
    Inherits System.Web.UI.Page

    Private ReadOnly Property Mensaje As String
        Get
            If Me.ClientQueryString.Contains("mensaje") Then
                Return Request.QueryString.Get("mensaje")
            Else
                Return ""
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbMensaje.Text = Mensaje
    End Sub

    Protected Sub bttHome_Click(sender As Object, e As EventArgs) Handles bttHome.Click
        Server.Transfer("~/Default.aspx")
    End Sub
End Class