Public Class EditarSucursal
    Inherits System.Web.UI.Page


    Private SucursalEditada As AdminSucursal
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_sucursal As Long
        Get
            If Me.ClientQueryString.Contains("id_sucursal") Then
                Return CLng(Request.QueryString.Get("id_sucursal"))
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

            If id_sucursal <> -1 Then

                SucursalEditada = (From cl In db.AdminSucursal
                                   Where cl.Id = id_sucursal
                                   Select cl).FirstOrDefault

                If SucursalEditada IsNot Nothing Then
                    BoundCliente(SucursalEditada)
                End If
            Else
                txtpais.DataBind()
                txtpais.SelectedValue = 539
                txtDpto.DataBind()
                txtCiudad.DataBind()
            End If
        End If
    End Sub

    Private Sub BoundCliente(SucursalEditada As AdminSucursal)

        txtDireccion.Text = SucursalEditada.Direccion
        txtNombre.Text = SucursalEditada.Sucursal
        txtTelefono.Text = SucursalEditada.Telefono
        Chkactivo.Checked = SucursalEditada.Activo
        Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                    Where cx.codigo = SucursalEditada.id_ciudad
                    Select cx).FirstOrDefault
        If city IsNot Nothing Then
            txtpais.DataBind()
            txtpais.SelectedValue = city.id_pais
            txtDpto.DataBind()
            txtDpto.SelectedValue = city.departamento
            txtCiudad.DataBind()
            txtCiudad.SelectedValue = SucursalEditada.id_ciudad
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_cliente"))
        If id_sucursal = -1 Then
            SucursalEditada = New AdminSucursal
            SucursalEditada.id_cliente = Session("id_cliente")
            db.AdminSucursal.Add(SucursalEditada)
        Else
            SucursalEditada = (From cl In db.AdminSucursal
                               Where cl.Id = id_sucursal
                               Select cl).FirstOrDefault
        End If




        If SucursalEditada IsNot Nothing Then
            If id_sucursal = -1 Then
                sx = (From cl In db.AdminSucursal
                      Where cl.Sucursal = txtNombre.Text And cl.id_cliente = ids
                      Select cl).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                SucursalEditada.Direccion = txtDireccion.Text
                SucursalEditada.Sucursal = txtNombre.Text
                SucursalEditada.Telefono = txtTelefono.Text
                SucursalEditada.Ciudad = txtCiudad.SelectedValue
                SucursalEditada.id_ciudad = txtCiudad.SelectedValue
                SucursalEditada.Departamento = txtDpto.SelectedValue
                SucursalEditada.Activo = Chkactivo.Checked
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_sucursal = -1 Then
                    Utilidades.RegistrarAuditoria("Terceros", SucursalEditada.Id, SucursalEditada.Sucursal, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Terceros", SucursalEditada.Id, SucursalEditada.Sucursal, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Sucursal Guardada", Me)
                Response.Redirect("../Admin/Page_Sucursal.aspx")
            Else
                Utilidades.Mensaje("Sucursal ya Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Sucursal.aspx")
    End Sub



    Protected Sub txtpais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtpais.SelectedIndexChanged
        txtDpto.DataBind()
    End Sub

    Protected Sub txtDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtDpto.SelectedIndexChanged
        txtCiudad.DataBind()
    End Sub
End Class