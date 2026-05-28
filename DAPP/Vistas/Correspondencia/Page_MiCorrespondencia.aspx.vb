
Public Class Page_MiCorrespondencia
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim idt As Long
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            Else
                If Usuario.id_tercero IsNot Nothing Then
                    idt = Usuario.id_tercero
                    Hdd_id_responsable.Value = idt
                    Src_Correspondencia.DataBind()
                    GridCorrespondencia.DataBind()
                Else
                    Hdd_id_responsable.Value = -1
                End If
            End If



        End If
    End Sub




    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridCorrespondencia.Behaviors.Selection IsNot Nothing AndAlso GridCorrespondencia.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridCorrespondencia.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim cx = (From crx In db.Correspondencia
                          Where crx.id = id_cl
                          Select crx).FirstOrDefault
                If cx IsNot Nothing Then
                    If cx.tipo_correspondencia = "Interna Enviada" Or cx.tipo_correspondencia = "Interna Recibida" Then
                        Response.Redirect("../Correspondencia/Editar_CInterna.aspx?Mi=1&id_correspondencia=" & id_cl)
                    Else
                        Response.Redirect("../Correspondencia/Editar_Correspondencia.aspx?Mi=1&id_correspondencia=" & id_cl)
                    End If
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Correspondencia", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridCorrespondencia.Behaviors.Selection IsNot Nothing AndAlso GridCorrespondencia.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridCorrespondencia.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cr As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim correspondencia_eliminar = (From cr In db.Correspondencia
                                                Where cr.id = id_cr
                                                Select cr).FirstOrDefault

                If correspondencia_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    db.Correspondencia.Remove(correspondencia_eliminar)
                    db.SaveChanges()
                    GridCorrespondencia.DataBind()
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Correspondencia", Me)
        End If
    End Sub

    Protected Sub BttComentarios_Click(sender As Object, e As ImageClickEventArgs) Handles BttComentarios.Click
        If GridCorrespondencia.Behaviors.Selection IsNot Nothing AndAlso GridCorrespondencia.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridCorrespondencia.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Utilidades.RegistrarScript("window.open('../Comentarios.aspx?id_correspondencia=" & id_cl & "','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Correspondencia", Me)
        End If
    End Sub

    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "MiCorrespondencia" & Today.Date.ToString
        ExpGrid.Export(GridCorrespondencia)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "MiCorrespondencia" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        ExpGrid.Export(GridCorrespondencia)
        'Exppdf.Export(GridTipos)
    End Sub
End Class