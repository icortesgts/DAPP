
Public Class Page_BackUp
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            Hdd_id_usuario.Value = User.Identity.Name
            GridClientes.DataBind()
        Else
            Hdd_id_usuario.Value = User.Identity.Name
        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarBackup.aspx?id_backup=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarBackup.aspx?id_backup=" & id_cl)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities

                Dim cliente_eliminar = (From cl In db.Backups
                                        Where cl.id = id_cl
                                        Select cl).FirstOrDefault

                If cliente_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Solicitud de Backup", id_cl, cliente_eliminar.Fecha_solicitud, "Eliminación Registro", User.Identity.Name, ids)
                    db.Backups.Remove(cliente_eliminar)
                    db.SaveChanges()
                    GridClientes.ClearDataSource()
                    GridClientes.DataSource = Src_Clientes
                    GridClientes.DataBind()
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Backups" & Today.Date.ToString
        ExpGrid.Export(GridClientes)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Backups" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridClientes)
        'Exppdf.Export(GridTipos)
    End Sub
End Class