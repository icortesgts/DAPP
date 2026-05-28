
Public Class Page_Usuarios
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            Hdd_id_usuario.Value = Utilidades.ConsultarUsuario(User.Identity.Name).ID
        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarUsuario.aspx?id_usuario=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridUsuarios.Behaviors.Selection IsNot Nothing AndAlso GridUsuarios.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridUsuarios.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_us As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarUsuario.aspx?id_usuario=" & id_us)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Usuario", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridUsuarios.Behaviors.Selection IsNot Nothing AndAlso GridUsuarios.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridUsuarios.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_us As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim usuario_eliminar = (From us In db.Usuarios
                                        Where us.ID = id_us
                                        Select us).FirstOrDefault

                If usuario_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Usuarios", id_us, usuario_eliminar.usuario, "Eliminación Registro", User.Identity.Name, ids)
                    db.Usuarios.Remove(usuario_eliminar)
                    db.SaveChanges()
                    GridUsuarios.ClearDataSource()
                    GridUsuarios.DataSource = Src_Usuarios
                    GridUsuarios.DataBind()
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Usuario", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Usuarios" & Today.Date.ToString
        ExpGrid.Export(GridUsuarios)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Usuarios" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridUsuarios)
        'Exppdf.Export(GridTipos)
    End Sub
End Class