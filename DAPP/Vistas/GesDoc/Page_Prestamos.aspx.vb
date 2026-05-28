
Imports Infragistics.Web.UI.GridControls

Public Class Page_Prestamos
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




    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../GesDoc/EditarPrestamo.aspx?id_solicitud=" & id_cl)

            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Solicitud", Me)
        End If
    End Sub



    Protected Sub bttAbrir_Click(sender As Object, e As ImageClickEventArgs) Handles bttAbrir.Click
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim aliasdoc As String
                aliasdoc = rw.Items.Item(5).Value
                Dim doc = (From cl In db.Documentos_Solicitud.Include("Documentos")
                           Where cl.Documentos.alias = aliasdoc And cl.id_solicitud = id_cl
                           Select cl).FirstOrDefault
                If doc IsNot Nothing Then
                    Dim lnk As String
                    lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/" & doc.Documentos.nombre
                    Utilidades.RegistrarScript("window.open('" & lnk & "','Documento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
                Else
                    Utilidades.Mensaje("No Se puede visualizar todos los documentos de una carpeta al tiempo", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono No selecciono ninguna Solicitud", Me)
        End If
    End Sub


    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Solicitudes" & Today.Date.ToString
        ExpGrid.Export(GridDocs)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Solicitudes" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridDocs)
        'Exppdf.Export(GridTipos)
    End Sub

End Class