Public Class EditarSuc
    Inherits System.Web.UI.Page


    Private SucursalEditada As SucursalTerceros
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tercero As Long
        Get
            If Me.ClientQueryString.Contains("id_tercero") Then
                Return CLng(Request.QueryString.Get("id_tercero"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_suc As Long
        Get
            If Me.ClientQueryString.Contains("id_suc") Then
                Return CLng(Request.QueryString.Get("id_suc"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If id_tercero = -1 Then
                Utilidades.Mensaje("Tercero No Asociado", Me)
                Response.Redirect("../Admin/Page_Terceros.aspx")
            Else
                Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
                If Usuario Is Nothing Then
                    Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
                End If

                If id_suc <> -1 Then

                    SucursalEditada = (From tr In db.SucursalTerceros
                                       Where tr.id = id_suc
                                       Select tr).FirstOrDefault

                    If SucursalEditada IsNot Nothing Then
                        BoundTercero(SucursalEditada)
                    End If
                Else
                    txtpais.DataBind()
                    txtpais.SelectedValue = 539
                    txtDpto.DataBind()
                    txtCiudad.DataBind()
                End If
            End If
        End If
    End Sub

    Private Sub BoundTercero(SucursalEditada As SucursalTerceros)

        txtCorreo.Text = SucursalEditada.correo_electronico
        txtDireccion.Text = SucursalEditada.direccion
        txtNombre.Text = SucursalEditada.nombre
        txtTelefono.Text = SucursalEditada.telefono
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


        If id_suc = -1 Then
            SucursalEditada = New SucursalTerceros
            SucursalEditada.id_tercero = id_tercero
            db.SucursalTerceros.Add(SucursalEditada)
        Else
            SucursalEditada = (From tr In db.SucursalTerceros
                               Where tr.id = id_suc
                               Select tr).FirstOrDefault
        End If

        If Not UtilityDB_Common.IsValidEmail(txtCorreo.Text) Then
            cvPagina.ErrorMessage = "El correo ingresado es invalido."
            cvPagina.IsValid = False
            Return
        End If



        If SucursalEditada IsNot Nothing Then
            If id_suc = -1 Then
                sx = (From tr In db.SucursalTerceros
                      Where tr.nombre = txtNombre.Text And tr.id_tercero = id_tercero
                      Select tr).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                SucursalEditada.correo_electronico = txtCorreo.Text
                SucursalEditada.direccion = txtDireccion.Text
                SucursalEditada.nombre = txtNombre.Text
                SucursalEditada.telefono = txtTelefono.Text
                SucursalEditada.Activo = Chkactivo.Checked
                SucursalEditada.id = txtCiudad.SelectedValue
                db.SaveChanges()
                Dim ids As Long
                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_suc = -1 Then
                    Utilidades.RegistrarAuditoria("Sucursal Terceros", SucursalEditada.id, SucursalEditada.nombre, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Sucursal Terceros", SucursalEditada.id, SucursalEditada.nombre, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Sucursal de Tercero Guardada", Me)

                Response.Redirect("../Admin/Page_Terceros.aspx")
            Else
                Utilidades.Mensaje("Sucursal de Tercero Existente", Me)
            End If


        End If






    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Terceros.aspx")
    End Sub

    Protected Sub txtpais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtpais.SelectedIndexChanged
        txtDpto.DataBind()
    End Sub

    Protected Sub txtDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtDpto.SelectedIndexChanged
        txtCiudad.DataBind()
    End Sub
End Class