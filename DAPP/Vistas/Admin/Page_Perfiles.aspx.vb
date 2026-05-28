
Public Class Page_PErfiles
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            Hdd_id_usuario.Value = User.Identity.Name
        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarPerfil.aspx?id_perfil=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridUsuarios.Behaviors.Selection IsNot Nothing AndAlso GridUsuarios.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridUsuarios.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_us As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarPerfil.aspx?id_perfil=" & id_us)
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
                Dim x As Integer
                x = (From pf In db.Perfil_Menu
                     Where pf.id_perfil = id_us
                     Select pf).Count
                If x > 0 Then
                    Utilidades.Mensaje("Permisos Asociados al perfil, por favor revisar.", Me)
                Else
                    x = (From pf In db.Usuarios
                         Where pf.id_perfil = id_us
                         Select pf).Count
                    If x > 0 Then
                        Utilidades.Mensaje("Usuarios Asociados al perfil, por favor revisar.", Me)
                    Else
                        Dim usuario_eliminar = (From us In db.AdminPerfiles
                                                Where us.ID = id_us
                                                Select us).FirstOrDefault
                        If usuario_eliminar IsNot Nothing Then
                            Dim ids As Long
                            If Session("id_sucursal") IsNot Nothing Then
                                ids = Session("id_sucursal")
                            Else
                                ids = 0
                            End If
                            Utilidades.RegistrarAuditoria("Perfiles", id_us, usuario_eliminar.nombre, "Eliminación Registro", User.Identity.Name, ids)
                            db.AdminPerfiles.Remove(usuario_eliminar)
                            db.SaveChanges()
                            GridUsuarios.ClearDataSource()
                            GridUsuarios.DataSource = Src_Usuarios
                            GridUsuarios.DataBind()
                        End If
                    End If
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Usuario", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Perfiles" & Today.Date.ToString
        ExpGrid.Export(GridUsuarios)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Perfiles" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridUsuarios)
        'Exppdf.Export(GridTipos)
    End Sub
End Class