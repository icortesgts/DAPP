Public Class EditarPerfil
    Inherits System.Web.UI.Page


    Private PerfilEditado As AdminPerfiles
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_perfil As Long
        Get
            If Me.ClientQueryString.Contains("id_perfil") Then
                Return CLng(Request.QueryString.Get("id_perfil"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_perfil <> -1 Then
                PerfilEditado = (From tr In db.AdminPerfiles
                                 Where tr.ID = id_perfil
                                 Select tr).FirstOrDefault

                If PerfilEditado IsNot Nothing Then
                    BoundTipoDoc(PerfilEditado)
                End If
                bttNuevo.Enabled = True
            Else
                bttNuevo.Enabled = False
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(PerfilEditado As AdminPerfiles)
        txtTipoDocumento.Text = PerfilEditado.nombre
        txtDescipcion.Text = PerfilEditado.Descripcion
        Chkactivo.Checked = PerfilEditado.Activo
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim cx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        Dim idc = CLng(Session("id_cliente"))
        If id_perfil = -1 Then
            PerfilEditado = New AdminPerfiles
            PerfilEditado.id_cliente = CLng(Session("id_cliente"))
            db.AdminPerfiles.Add(PerfilEditado)
        Else
            PerfilEditado = (From tr In db.AdminPerfiles
                             Where tr.ID = id_perfil
                             Select tr).FirstOrDefault
        End If


        If PerfilEditado IsNot Nothing Then
            If id_perfil = -1 Then
                cx = (From tr In db.AdminPerfiles
                      Where tr.nombre = txtTipoDocumento.Text And tr.id_cliente = idc
                      Select tr).Count
            Else
                cx = 0
            End If
            If cx = 0 Then
                PerfilEditado.nombre = txtTipoDocumento.Text
                PerfilEditado.Descripcion = txtDescipcion.Text
                PerfilEditado.Activo = Chkactivo.Checked
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_perfil = -1 Then
                    Utilidades.RegistrarAuditoria("Perfil", PerfilEditado.ID, PerfilEditado.nombre, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Perfil", PerfilEditado.ID, PerfilEditado.nombre, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Perfil Guardado", Me)
                Response.Redirect("../Admin/Page_Perfiles.aspx")
            Else
                Utilidades.Mensaje("Perfil ya Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_perfiles.aspx")
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarPermiso.aspx?id_permiso=-1&id_perfil=" & id_perfil)
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_us As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarPermiso.aspx?id_permiso=" & id_us & "&id_perfil=" & id_perfil)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Permiso", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_us As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer

                Dim usuario_eliminar = (From us In db.Perfil_Menu
                                        Where us.id = id_us
                                        Select us).FirstOrDefault
                If usuario_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Permiso Perfil", id_us, txtTipoDocumento.Text, "Eliminación Registro", User.Identity.Name, ids)
                    db.Perfil_Menu.Remove(usuario_eliminar)
                    db.SaveChanges()
                    GridTipos.ClearDataSource()
                    GridTipos.DataSource = Src_Tipos
                    GridTipos.DataBind()
                End If

            Else
                Utilidades.Mensaje("No selecciono ningún permiso", Me)
            End If
        End If
    End Sub

    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Permisos" & Today.Date.ToString
        ExpGrid.Export(GridTipos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Permisos" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTipos)
    End Sub
End Class