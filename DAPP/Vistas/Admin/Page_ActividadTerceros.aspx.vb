

Public Class Page_ActividadTerceros
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
        Response.Redirect("../Admin/EditarActividad.aspx?id_actividad=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarActividad.aspx?id_actividad=" & id_tp)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Actividad", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From td In db.Terceros
                     Where td.id_actividad = id_tp
                     Select td).Count
                If x = 0 Then
                    Dim tipo_eliminar = (From tp In db.AdminActividad
                                         Where tp.id = id_tp
                                         Select tp).FirstOrDefault


                    If tipo_eliminar IsNot Nothing Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Actividad Terceros", id_tp, tipo_eliminar.Actividad, "Eliminación Registro", User.Identity.Name, ids)
                        db.AdminActividad.Remove(tipo_eliminar)
                        db.SaveChanges()
                        GridTipos.ClearDataSource()
                        GridTipos.DataSource = Src_Tipos
                        GridTipos.DataBind()

                    End If
                Else
                    Utilidades.Mensaje("Tiene terceros asociados a esta Actividad por favor revisar.", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Actividad", Me)
        End If
    End Sub

    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "ActividadT" & Today.Date.ToString
        ExpGrid.Export(GridTipos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "ActividadT" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTipos)
        'Exppdf.Export(GridTipos)
    End Sub
End Class