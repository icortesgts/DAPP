Imports System.Data.SqlClient
Imports Infragistics.Web.UI.GridControls

Public Class EditarUsuario

    Inherits System.Web.UI.Page
    Private UsuarioEditado As Usuarios
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_usuario As Long
        Get
            If Me.ClientQueryString.Contains("id_usuario") Then
                Return CLng(Request.QueryString.Get("id_usuario"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Hdd_id_usuario.Value = User.Identity.Name
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_usuario <> -1 Then

                UsuarioEditado = (From us In db.Usuarios
                                  Where us.ID = id_usuario
                                  Select us).FirstOrDefault

                If UsuarioEditado IsNot Nothing Then
                    BoundUsuario(UsuarioEditado)
                End If
                bttNuevo.Enabled = True
                BttN1.Enabled = True
                BttN2.Enabled = True
                BttN3.Enabled = True
            Else
                bttNuevo.Enabled = False
                BttN1.Enabled = False
                BttN2.Enabled = False
                BttN3.Enabled = False
                Chkareas.Checked = True
                Chkclientes.Checked = True
                Chkterceros.Checked = True

            End If
        End If
    End Sub

    Private Sub BoundUsuario(UsuarioEditado As Usuarios)
        txtUsuario.ReadOnly = True
        txtEstado.Checked = UsuarioEditado.bloqueado
        txtNombre.Text = UsuarioEditado.nombre
        txtPerfil.SelectedValue = UsuarioEditado.id_perfil
        txtUsuario.Text = UsuarioEditado.usuario
        Txtemail.Text = UsuarioEditado.email
        txtPassword.Attributes.Add("Value", UsuarioEditado.password)
        txtConfirmarPassword.Attributes.Add("Value", UsuarioEditado.password)
        GridClientes.DataBind()
        GridSucursal.DataBind()
        GridTerceros.DataBind()
        GridAreas.DataBind()
        Chkareas.Checked = UsuarioEditado.Areas
        Chkterceros.Checked = UsuarioEditado.Terceros
        Chkclientes.Checked = UsuarioEditado.Todos
        If UsuarioEditado.correspondencia IsNot Nothing Then
            ChkCorrespondencia.Checked = UsuarioEditado.correspondencia
        Else
            ChkCorrespondencia.Checked = False
        End If
        If Not Chkclientes.Checked Then
            PnClientes.Visible = True
        End If
        If Not Chkareas.Checked Then
            PnAreas.Visible = True
        End If
        If Not Chkterceros.Checked Then
            PnTerceros.Visible = True
        End If
        If UsuarioEditado.id_perfil = 1 Then
            txttercero.SelectedValue = 0
            txttercero.Enabled = False
        End If
        If UsuarioEditado.id_tercero IsNot Nothing Then
            txttercero.DataBind()
            txttercero.SelectedValue = UsuarioEditado.id_tercero
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        If id_usuario = -1 Then
            UsuarioEditado = New Usuarios
            db.Usuarios.Add(UsuarioEditado)
        Else
            UsuarioEditado = (From us In db.Usuarios
                              Where us.ID = id_usuario
                              Select us).FirstOrDefault
        End If


        If txtPassword.Text <> txtConfirmarPassword.Text Then
            cvPagina.ErrorMessage = "Los Password no coinciden."
            cvPagina.IsValid = False
            Return
        End If

        If Not UtilityDB_Common.IsValidEmail(Txtemail.Text) Then
            cvPagina.ErrorMessage = "El correo ingresado es invalido."
            cvPagina.IsValid = False
            Return
        End If

        If id_usuario = -1 Then
            Dim Cuantos As Integer = (From us In db.Usuarios
                                      Where us.usuario = txtUsuario.Text
                                      Select us).Count
            If Cuantos > 0 Then
                cvPagina.ErrorMessage = "Este usuario ya se encuentra registrado en el sistema."
                cvPagina.IsValid = False
                Return
            End If
        End If

        If UsuarioEditado.password <> txtPassword.Text Then
            If Not AppGlobal.ValidatePasswordPolicies(txtPassword.Text) Then
                cvPagina.IsValid = False
                cvPagina.ErrorMessage = AppGlobal_Class.PasswordPoliciesString
                Return
            End If
        End If

        If UsuarioEditado IsNot Nothing Then
            If UsuarioEditado.password <> txtPassword.Text Then
                UsuarioEditado.password = PasswordHash.CreateHash(txtPassword.Text)
            End If
            UsuarioEditado.bloqueado = txtEstado.Checked
            UsuarioEditado.nombre = txtNombre.Text
            UsuarioEditado.id_perfil = txtPerfil.SelectedValue
            UsuarioEditado.usuario = txtUsuario.Text
            UsuarioEditado.fecha_password = Now
            UsuarioEditado.email = Txtemail.Text
            UsuarioEditado.Areas = Chkareas.Checked
            UsuarioEditado.Todos = Chkclientes.Checked
            UsuarioEditado.Terceros = Chkterceros.Checked
            UsuarioEditado.correspondencia = ChkCorrespondencia.Checked
            If txttercero.SelectedValue = 0 Then
                UsuarioEditado.id_tercero = Nothing
            Else
                UsuarioEditado.id_tercero = txttercero.SelectedValue
            End If
        End If

        db.SaveChanges()
        If id_usuario = -1 Then
            Dim usrclx As New Usuario_Cliente
            db.Usuario_Cliente.Add(usrclx)
            Dim id_cliente As Long
            id_cliente = CLng(Session("id_cliente"))
            usrclx.id_cliente = id_cliente
            usrclx.id_usuario = UsuarioEditado.ID
            usrclx.todas = True
            db.SaveChanges()
        End If

        'WF
        CargarEnWF(UsuarioEditado.usuario, UsuarioEditado.password, UsuarioEditado.nombre, UsuarioEditado.email, UsuarioEditado.bloqueado, UsuarioEditado.fecha_password, txtRol.SelectedValue)
        Utilidades.SincronizarSegmentos(UsuarioEditado.ID)

        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_usuario = -1 Then
            Utilidades.RegistrarAuditoria("Usuarios", UsuarioEditado.ID, UsuarioEditado.usuario, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Usuarios", UsuarioEditado.ID, UsuarioEditado.usuario, "Actualización Registro", User.Identity.Name, ids)
        End If

        Utilidades.Mensaje("Usuario Guardado", Me)
        Response.Redirect("../Admin/Page_Usuarios.aspx")
    End Sub

    Private Sub CargarEnWF(usuario As String, password As String, nombre As String, email As String, bloqueado As Boolean, fecha_password As Date?, Rol As Long)
        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnectionWF").ToString())
        cnn.Open()

        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "CargarUsuario"
        adp.SelectCommand.CommandType = CommandType.StoredProcedure

        'Asignacion de parámetros
        adp.SelectCommand.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario
        adp.SelectCommand.Parameters.Add("@password", SqlDbType.VarChar).Value = password
        adp.SelectCommand.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre
        adp.SelectCommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email
        adp.SelectCommand.Parameters.Add("@bloqueado", SqlDbType.Bit).Value = bloqueado
        adp.SelectCommand.Parameters.Add("@fecha_password", SqlDbType.DateTime).Value = fecha_password
        adp.SelectCommand.Parameters.Add("@Rol", SqlDbType.BigInt).Value = Rol

        ' Execute
        adp.SelectCommand.ExecuteNonQuery()

        cnn.Close()
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Usuarios.aspx")
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        guardar()
        Utilidades.RegistrarScript("window.open('../EditarInfo.aspx?info=Clientes&id_usuario=" & id_usuario & "','Información Usuarios','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Utilidades.RegistrarScript("window.open('../EditarInfo.aspx?info=Clientes&id_usuario=" & id_usuario & "&id_cliente=" & id_tc & "','Información Usuarios','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If
    End Sub

    Protected Sub ImageButton9_Click(sender As Object, e As ImageClickEventArgs) Handles BttN1.Click
        guardar()
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim cl = (From cx In db.Usuario_Cliente
                          Where cx.id = id_tc
                          Select cx).FirstOrDefault
                If cl IsNot Nothing Then
                    If cl.todas Then
                        Utilidades.Mensaje("El Usuario tiene acceso a todas las sucursales del Cliente Seleccionado, si desea hacerlo selectivo, debe editar los permisos.", Me)
                    Else
                        Utilidades.RegistrarScript("window.open('../EditarInfo.aspx?info=Sucursales&id_usuario=" & id_usuario & "&id_cliente=" & cl.id_cliente & "','Información Usuarios','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
                    End If

                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles BttN2.Click
        guardar()
        Utilidades.RegistrarScript("window.open('../EditarInfo.aspx?info=Terceros&id_usuario=" & id_usuario & "','Información Usuarios','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub ImageButton5_Click(sender As Object, e As ImageClickEventArgs) Handles BttN3.Click
        guardar()
        Utilidades.RegistrarScript("window.open('../EditarInfo.aspx?info=Areas&id_usuario=" & id_usuario & "','Información Usuarios','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Private Sub GridClientes_RowSelectionChanged(sender As Object, e As SelectedRowEventArgs) Handles GridClientes.RowSelectionChanged
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim clx = (From usx In db.Usuario_Cliente
                           Where usx.id = id_tc
                           Select usx).FirstOrDefault
                If clx IsNot Nothing Then
                    Hdd_id_cliente.Value = clx.id_cliente
                    GridSucursal.DataBind()
                End If

            End If
        End If
    End Sub

    Protected Sub Chkclientes_CheckedChanged(sender As Object, e As EventArgs) Handles Chkclientes.CheckedChanged
        If Chkclientes.Checked Then
            PnClientes.Visible = False
            Utilidades.EliminarSucursal(id_usuario)
            Utilidades.EliminarClientes(id_usuario)
        Else
            PnClientes.Visible = True
        End If
    End Sub

    Protected Sub Chkterceros_CheckedChanged(sender As Object, e As EventArgs) Handles Chkterceros.CheckedChanged
        If Chkterceros.Checked Then
            PnTerceros.Visible = False
            Utilidades.EliminarTerceros(id_usuario)
        Else
            PnTerceros.Visible = True
        End If
    End Sub

    Protected Sub Chkareas_CheckedChanged(sender As Object, e As EventArgs) Handles Chkareas.CheckedChanged
        If Chkareas.Checked Then
            PnAreas.Visible = False
            Utilidades.Eliminarareas(id_usuario)
        Else
            PnAreas.Visible = True
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim usuario_eliminar = (From us In db.Usuario_Cliente
                                        Where us.id = id_tc
                                        Select us).FirstOrDefault
                If usuario_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Clientes Usuario", id_tc, txtNombre.Text, "Eliminación Registro", User.Identity.Name, ids)
                    Utilidades.EliminarSucursal(id_usuario, usuario_eliminar.id_cliente)
                    db.Usuario_Cliente.Remove(usuario_eliminar)
                    db.SaveChanges()
                End If
                Hdd_id_cliente.Value = 0
                GridClientes.DataBind()
                GridSucursal.DataBind()
            End If
        End If
    End Sub

    Protected Sub ImageButton10_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton10.Click
        If GridSucursal.Behaviors.Selection IsNot Nothing AndAlso GridSucursal.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridSucursal.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim usuario_eliminar = (From us In db.Usuario_Sucursal
                                        Where us.id = id_tc
                                        Select us).FirstOrDefault
                If usuario_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Sucursales Usuario", id_tc, txtNombre.Text, "Eliminación Registro", User.Identity.Name, ids)
                    db.Usuario_Sucursal.Remove(usuario_eliminar)
                    db.SaveChanges()
                End If
                GridSucursal.DataBind()
            End If
        End If
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim usuario_eliminar = (From us In db.Usuarios_Terceros
                                        Where us.id = id_tc
                                        Select us).FirstOrDefault
                If usuario_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Terceros Usuario", id_tc, txtNombre.Text, "Eliminación Registro", User.Identity.Name, ids)
                    db.Usuarios_Terceros.Remove(usuario_eliminar)
                    db.SaveChanges()
                End If
                GridTerceros.DataBind()
            End If
        End If
    End Sub

    Protected Sub ImageButton6_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton6.Click
        If GridAreas.Behaviors.Selection IsNot Nothing AndAlso GridAreas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridAreas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim usuario_eliminar = (From us In db.Usuarios_Areas
                                        Where us.id = id_tc
                                        Select us).FirstOrDefault
                If usuario_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Areas Usuario", id_tc, txtNombre.Text, "Eliminación Registro", User.Identity.Name, ids)
                    db.Usuarios_Areas.Remove(usuario_eliminar)
                    db.SaveChanges()
                End If
                GridAreas.DataBind()
            End If
        End If
    End Sub

    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "ClientesUsuario" & Today.Date.ToString
        ExpGrid.Export(GridClientes)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "ClientesUsuario" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridClientes)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub ImageButton11_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton11.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "SucursalUsuario" & Today.Date.ToString
        ExpGrid.Export(GridSucursal)
    End Sub

    Protected Sub ImageButton12_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton12.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "SucursalUsuario" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridSucursal)
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "TerceroUsuario" & Today.Date.ToString
        ExpGrid.Export(GridTerceros)
    End Sub

    Protected Sub ImageButton4_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton4.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "TercerosUsuario" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTerceros)
    End Sub

    Protected Sub ImageButton7_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton7.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "AreaUsuario" & Today.Date.ToString
        ExpGrid.Export(GridAreas)
    End Sub

    Protected Sub ImageButton8_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton8.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "AreaUsuario" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridAreas)
    End Sub
    Protected Sub guardar()
        If id_usuario = -1 Then
            UsuarioEditado = New Usuarios
            db.Usuarios.Add(UsuarioEditado)
        Else
            UsuarioEditado = (From us In db.Usuarios
                              Where us.ID = id_usuario
                              Select us).FirstOrDefault
        End If


        If txtPassword.Text <> txtConfirmarPassword.Text Then
            cvPagina.ErrorMessage = "Los Password no coinciden."
            cvPagina.IsValid = False
            Return
        End If

        If Not UtilityDB_Common.IsValidEmail(Txtemail.Text) Then
            cvPagina.ErrorMessage = "El correo ingresado es invalido."
            cvPagina.IsValid = False
            Return
        End If

        If id_usuario = -1 Then
            Dim Cuantos As Integer = (From us In db.Usuarios
                                      Where us.usuario = txtUsuario.Text
                                      Select us).Count
            If Cuantos > 0 Then
                cvPagina.ErrorMessage = "Este usuario ya se encuentra registrado en el sistema."
                cvPagina.IsValid = False
                Return
            End If
        End If

        If UsuarioEditado.password <> txtPassword.Text Then
            If Not AppGlobal.ValidatePasswordPolicies(txtPassword.Text) Then
                cvPagina.IsValid = False
                cvPagina.ErrorMessage = AppGlobal_Class.PasswordPoliciesString
                Return
            End If
        End If

        If UsuarioEditado IsNot Nothing Then
            If UsuarioEditado.password <> txtPassword.Text Then
                UsuarioEditado.password = PasswordHash.CreateHash(txtPassword.Text)
            End If
            UsuarioEditado.bloqueado = txtEstado.Checked
            UsuarioEditado.nombre = txtNombre.Text
            UsuarioEditado.id_perfil = txtPerfil.SelectedValue
            UsuarioEditado.usuario = txtUsuario.Text
            UsuarioEditado.fecha_password = Now
            UsuarioEditado.email = Txtemail.Text
            UsuarioEditado.Areas = Chkareas.Checked
            UsuarioEditado.Todos = Chkclientes.Checked
            UsuarioEditado.Terceros = Chkterceros.Checked
        End If



        db.SaveChanges()
    End Sub

    Protected Sub txttercero_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txttercero.SelectedIndexChanged
        If txttercero.Items.Count > 0 Then
            If txttercero.SelectedValue <> 0 Then
                Dim tx = (From t In db.Terceros
                          Where t.id = txttercero.SelectedValue
                          Select t).FirstOrDefault
                If tx IsNot Nothing Then
                    txtNombre.Text = tx.nombre
                    Txtemail.Text = tx.correo_electronico
                End If
            Else
                If id_usuario = -1 Then

                    txtEstado.Checked = False
                    txtNombre.Text = ""

                    txtUsuario.Text = ""
                    Txtemail.Text = ""

                    GridClientes.DataBind()
                    GridSucursal.DataBind()
                    GridTerceros.DataBind()
                    GridAreas.DataBind()
                    Chkareas.Checked = True
                    Chkterceros.Checked = True
                    Chkclientes.Checked = True
                Else
                    UsuarioEditado = (From us In db.Usuarios.Include("Usuario_Cliente").Include("Usuario_Cliente.Clientes")
                                      Where us.ID = id_usuario
                                      Select us).FirstOrDefault

                    If UsuarioEditado IsNot Nothing Then
                        BoundUsuario(UsuarioEditado)
                    End If
                    bttNuevo.Enabled = True
                    BttN1.Enabled = True
                    BttN2.Enabled = True
                    BttN3.Enabled = True
                End If
            End If


        End If
    End Sub

    Protected Sub txtPerfil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtPerfil.SelectedIndexChanged
        If txtPerfil.SelectedValue = 1 Then
            txttercero.SelectedValue = 0
            txttercero.Enabled = False
        End If
    End Sub
End Class