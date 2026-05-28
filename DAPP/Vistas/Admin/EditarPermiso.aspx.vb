Public Class EditarPermiso
    Inherits System.Web.UI.Page


    Private ClaseDocEditado As Perfil_Menu
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_permiso As Long
        Get
            If Me.ClientQueryString.Contains("id_permiso") Then
                Return CLng(Request.QueryString.Get("id_permiso"))
            Else
                Return -1
            End If
        End Get
    End Property

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

            If id_permiso <> -1 Then
                Dim cadena As String
                cadena = "SELECT Admin_menu.id,Admin_AliasMenu.Alias FROM Admin_menu inner join Admin_AliasMenu"
                cadena = cadena & " On Admin_AliasMenu.id_menu=Admin_menu.id and Admin_AliasMenu.id_cliente=@cliente"
                cadena = cadena & " order by Admin_AliasMenu.Alias"
                SqlMenu.SelectCommand = cadena
                SqlMenu.DataBind()
                txtfuncionalidad.DataBind()
                ClaseDocEditado = (From tr In db.Perfil_Menu
                                   Where tr.id = id_permiso
                                   Select tr).FirstOrDefault

                If ClaseDocEditado IsNot Nothing Then
                    BoundTipoDoc(ClaseDocEditado)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(ClaseDocEditado As Perfil_Menu)
        txtfuncionalidad.SelectedValue = ClaseDocEditado.id_menu
        txtfuncionalidad.Enabled = False
        Chkactivo.Checked = ClaseDocEditado.Adicion
        Chkedit.Checked = ClaseDocEditado.Edicion
        Chkdel.Checked = ClaseDocEditado.Consulta
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_permiso = -1 Then
            ClaseDocEditado = New Perfil_Menu
            ClaseDocEditado.id_perfil = id_perfil
            db.Perfil_Menu.Add(ClaseDocEditado)
        Else
            ClaseDocEditado = (From tr In db.Perfil_Menu
                               Where tr.id = id_permiso
                               Select tr).FirstOrDefault
        End If


        If ClaseDocEditado IsNot Nothing Then

            ClaseDocEditado.id_menu = txtfuncionalidad.SelectedValue

            ClaseDocEditado.Adicion = Chkactivo.Checked
            ClaseDocEditado.Edicion = Chkedit.Checked
            ClaseDocEditado.Consulta = Chkdel.Checked
            db.SaveChanges()

            If Session("id_sucursal") IsNot Nothing Then
                ids = Session("id_sucursal")
            Else
                ids = 0
            End If
            If id_permiso = -1 Then
                Utilidades.RegistrarAuditoria("Permisos", ClaseDocEditado.id, txtfuncionalidad.Text, "Nuevo Registro", User.Identity.Name, ids)
            Else
                Utilidades.RegistrarAuditoria("Permisos", ClaseDocEditado.id, txtfuncionalidad.Text, "Actualización Registro", User.Identity.Name, ids)
            End If
            Utilidades.Mensaje("Permiso Guardado", Me)
            Response.Redirect("../Admin/EditarPerfil.aspx?id_perfil=" & id_perfil)


        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/EditarPerfil.aspx?id_perfil=" & id_perfil)
    End Sub

End Class