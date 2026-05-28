Imports Infragistics.Web.UI.NavigationControls

Public Class Page_Ubicaciones
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            HCliente.Value = Session("id_cliente")
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarUbicacion.aspx?id_ubicacion=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridUbicaciones.Behaviors.Selection IsNot Nothing AndAlso GridUbicaciones.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridUbicaciones.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_ar As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarUbicacion.aspx?id_ubicacion=" & id_ar)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Ubicación", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridUbicaciones.Behaviors.Selection IsNot Nothing AndAlso GridUbicaciones.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridUbicaciones.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_ub As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From ub In db.Ubicaciones
                     Where ub.id_ubicacion = id_ub
                     Select ub).Count
                If x = 0 Then
                    x = (From ub In db.Documentos
                         Where ub.id_ubicacion = id_ub
                         Select ub).Count
                    If x = 0 Then
                        Dim ubicacion_eliminar = (From ub In db.Ubicaciones
                                                  Where ub.id = id_ub
                                                  Select ub).FirstOrDefault

                        If ubicacion_eliminar IsNot Nothing Then
                            Dim ids As Long
                            If Session("id_sucursal") IsNot Nothing Then
                                ids = Session("id_sucursal")
                            Else
                                ids = 0
                            End If
                            Utilidades.RegistrarAuditoria("Ubicación", id_ub, ubicacion_eliminar.ubicacion, "Eliminación Registro", User.Identity.Name, ids)
                            db.Ubicaciones.Remove(ubicacion_eliminar)
                            db.SaveChanges()
                            GridUbicaciones.ClearDataSource()
                            GridUbicaciones.DataSource = Src_Ubicaciones
                            GridUbicaciones.DataBind()
                        End If
                    Else
                        Utilidades.Mensaje("Ubicación con documentos Asociados, por favor revisar.", Me)
                    End If
                Else
                    Utilidades.Mensaje("Ubicación con otras ubicaciones Asociadas, por favor revisar.", Me)
                End If
            Else
                Utilidades.Mensaje("No selecciono ninguna Ubicación", Me)
            End If
        End If
    End Sub


    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Ubicaciones" & Today.Date.ToString
        ExpGrid.Export(GridUbicaciones)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Ubicaciones" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridUbicaciones)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub BttUbicaciones_Click(sender As Object, e As EventArgs) Handles BttUbicaciones.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Ubicacion.aspx','SeleccionarUbicacion','width=800,height=800,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 800)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttSticker_Click(sender As Object, e As ImageClickEventArgs) Handles bttSticker.Click
        If GridUbicaciones.Behaviors.Selection IsNot Nothing AndAlso GridUbicaciones.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridUbicaciones.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_ar As Long = rw.DataKey(0)
                Utilidades.RegistrarScript("window.open('../Admin/GenerarStickerU.aspx?id_ubicacion=" & id_ar & "','Generar Sticker','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Ubicación", Me)
        End If
    End Sub
End Class