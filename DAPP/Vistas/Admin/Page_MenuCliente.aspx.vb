
Public Class Page_MenuCliente
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



    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "MenusCliente" & Today.Date.ToString
        ExpGrid.Export(GridTipos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "MenusCliente" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTipos)
        'Exppdf.Export(GridTipos)
    End Sub



    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_ar As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarMenuCliente.aspx?id_menu=" & id_ar)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Menú", Me)
        End If
    End Sub
End Class