
Public Class Page_TiposUbicacion
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
        Response.Redirect("../Admin/EditarTipoUbicacion.aspx?id_tipoubicacion=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarTipoUbicacion.aspx?id_tipoubicacion=" & id_tp)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tipo de Ubicación", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From tb In db.AdminTipoUbicacion
                     Where tb.id_tipoCapacidad = id_tp
                     Select tb).Count
                If x = 0 Then
                    x = (From tb In db.Ubicaciones
                         Where tb.id_tipo = id_tp
                         Select tb).Count
                    If x = 0 Then
                        Dim tipo_eliminar = (From tb In db.AdminTipoUbicacion
                                             Where tb.id = id_tp
                                             Select tb).FirstOrDefault

                        If tipo_eliminar IsNot Nothing Then
                            Dim ids As Long
                            If Session("id_sucursal") IsNot Nothing Then
                                ids = Session("id_sucursal")
                            Else
                                ids = 0
                            End If
                            Utilidades.RegistrarAuditoria("Tipo Ubicación", id_tp, tipo_eliminar.nombre, "Eliminación Registro", User.Identity.Name, ids)
                            db.AdminTipoUbicacion.Remove(tipo_eliminar)
                            db.SaveChanges()
                            GridTipos.ClearDataSource()
                            GridTipos.DataSource = Src_Tipos
                            GridTipos.DataBind()
                        End If
                    Else
                        Utilidades.Mensaje("Ubicaciones asociadas a este tipo de ubicación ,por favor Revisar.", Me)
                    End If

                Else
                    Utilidades.Mensaje("Tipo de Ubicación contenido en otros tipos de ubicación ,por favor Revisar.", Me)
                End If


            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tipo de Ubicación", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "TiposU" & Today.Date.ToString
        ExpGrid.Export(GridTipos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "TiposU" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTipos)
        'Exppdf.Export(GridTipos)
    End Sub
End Class