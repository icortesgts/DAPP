
Imports Infragistics.Web.UI.GridControls

Public Class Page_EstadoArchivo
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
        End If
    End Sub



    Private Sub GridDocs_InitializeRow(sender As Object, e As RowEventArgs) Handles GridDocs.InitializeRow
        Dim i As Long

        For i = 0 To e.Row.Items.Count



            If e.Row.Items.Item(11).Value < 0 Then
                e.Row.Items.Item(11).CssClass = "redBg"
            ElseIf e.Row.Items.Item(11).Value = 0 Then
                e.Row.Items.Item(11).CssClass = "yellowBg"
            Else
                e.Row.Items.Item(11).CssClass = "greenBg"
            End If

            If e.Row.Items.Item(12).Value < 0 Then
                e.Row.Items.Item(12).CssClass = "redBg"
            ElseIf e.Row.Items.Item(12).Value = 0 Then
                e.Row.Items.Item(12).CssClass = "yellowBg"
            Else
                e.Row.Items.Item(12).CssClass = "greenBg"
            End If

            If e.Row.Items.Item(13).Value < 0 Then
                e.Row.Items.Item(13).CssClass = "redBg"
            ElseIf e.Row.Items.Item(13).Value = 0 Then
                e.Row.Items.Item(13).CssClass = "yellowBg"
            Else
                e.Row.Items.Item(13).CssClass = "greenBg"
            End If


        Next
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Documentos" & Today.Date.ToString
        ExpGrid.Export(GridDocs)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Documentos" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridDocs)
        'Exppdf.Export(GridTipos)
    End Sub

End Class