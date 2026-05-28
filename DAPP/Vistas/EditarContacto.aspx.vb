Public Class EditarContacto
    Inherits System.Web.UI.Page


    Private ContactoEditado As AdminContacto
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
    Private ReadOnly Property id_contacto As Long
        Get
            If Me.ClientQueryString.Contains("id_contacto") Then
                Return CLng(Request.QueryString.Get("id_contacto"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_cliente As Long
        Get
            If Me.ClientQueryString.Contains("id_cliente") Then
                Return CLng(Request.QueryString.Get("id_cliente"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_sox As Long
        Get
            If Me.ClientQueryString.Contains("id_sox") Then
                Return CLng(Request.QueryString.Get("id_sox"))
            Else
                Return -1
            End If
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            If id_tercero = -1 And id_suc = -1 And id_cliente = -1 And id_sox = -1 Then
                Utilidades.Mensaje("Información no Asociada", Me)

            Else
                Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
                If Usuario Is Nothing Then
                    Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
                End If

                If id_contacto <> -1 Then

                    ContactoEditado = (From tr In db.AdminContacto
                                       Where tr.id = id_contacto
                                       Select tr).FirstOrDefault

                    If ContactoEditado IsNot Nothing Then
                        BoundTercero(ContactoEditado)
                    End If
                Else
                    txtpais.DataBind()
                    txtpais.SelectedValue = 539
                    txtDpto.DataBind()
                    txtCiudad.DataBind()
                    If id_tercero <> -1 Then
                        Dim trx = (From tr In db.Terceros
                                   Where tr.id = id_tercero
                                   Select tr).FirstOrDefault
                        If trx IsNot Nothing Then
                            txtdir.Text = trx.direccion
                            TxtCelular.Text = trx.celular
                            Txtcorreo.Text = trx.correo_electronico
                            txtTelefono.Text = trx.telefono
                            Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                                        Where cx.codigo = trx.id_ciudad
                                        Select cx).FirstOrDefault
                            If city IsNot Nothing Then
                                txtpais.DataBind()
                                txtpais.SelectedValue = city.id_pais
                                txtDpto.DataBind()
                                txtDpto.SelectedValue = city.departamento
                                txtCiudad.DataBind()
                                txtCiudad.SelectedValue = trx.id_ciudad
                            End If
                        End If
                    End If
                    If id_suc <> -1 Then
                        Dim trx = (From tr In db.SucursalTerceros
                                   Where tr.id = id_suc
                                   Select tr).FirstOrDefault
                        If trx IsNot Nothing Then
                            txtdir.Text = trx.direccion
                            TxtCelular.Text = trx.telefono
                            Txtcorreo.Text = trx.correo_electronico
                            txtTelefono.Text = trx.telefono
                            Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                                        Where cx.codigo = trx.id_ciudad
                                        Select cx).FirstOrDefault
                            If city IsNot Nothing Then
                                txtpais.DataBind()
                                txtpais.SelectedValue = city.id_pais
                                txtDpto.DataBind()
                                txtDpto.SelectedValue = city.departamento
                                txtCiudad.DataBind()
                                txtCiudad.SelectedValue = trx.id_ciudad
                            End If
                        End If
                    End If
                    If id_cliente <> -1 Then
                        Dim trx = (From tr In db.Clientes
                                   Where tr.id = id_cliente
                                   Select tr).FirstOrDefault
                        If trx IsNot Nothing Then
                            txtdir.Text = trx.direccion
                            TxtCelular.Text = trx.telefono
                            Txtcorreo.Text = trx.correo_electronico
                            txtTelefono.Text = trx.telefono
                            Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                                        Where cx.codigo = trx.id_ciudad
                                        Select cx).FirstOrDefault
                            If city IsNot Nothing Then
                                txtpais.DataBind()
                                txtpais.SelectedValue = city.id_pais
                                txtDpto.DataBind()
                                txtDpto.SelectedValue = city.departamento
                                txtCiudad.DataBind()
                                txtCiudad.SelectedValue = trx.id_ciudad
                            End If
                        End If
                    End If
                    If id_sox <> -1 Then
                        Dim trx = (From tr In db.AdminSucursal
                                   Where tr.Id = id_sox
                                   Select tr).FirstOrDefault
                        If trx IsNot Nothing Then
                            txtdir.Text = trx.direccion
                            TxtCelular.Text = trx.Telefono
                            Txtcorreo.Text = ""
                            txtTelefono.Text = trx.telefono
                            Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                                        Where cx.codigo = trx.id_ciudad
                                        Select cx).FirstOrDefault
                            If city IsNot Nothing Then
                                txtpais.DataBind()
                                txtpais.SelectedValue = city.id_pais
                                txtDpto.DataBind()
                                txtDpto.SelectedValue = city.departamento
                                txtCiudad.DataBind()
                                txtCiudad.SelectedValue = trx.id_ciudad
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BoundTercero(ContactoEditado As AdminContacto)


        txtDireccion.Text = ContactoEditado.Cargo
        txtNombre.Text = ContactoEditado.Nombre
        txtTelefono.Text = ContactoEditado.Telefono
        txtdir.Text = ContactoEditado.Direccion
        TxtCelular.Text = ContactoEditado.Celular
        Txtcorreo.Text = ContactoEditado.Correo
        Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                    Where cx.codigo = ContactoEditado.id_ciudad
                    Select cx).FirstOrDefault
        If city IsNot Nothing Then
            txtpais.DataBind()
            txtpais.SelectedValue = city.id_pais
            txtDpto.DataBind()
            txtDpto.SelectedValue = city.departamento
            txtCiudad.DataBind()
            txtCiudad.SelectedValue = ContactoEditado.id_ciudad
        End If


    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click


        If id_contacto = -1 Then
            ContactoEditado = New AdminContacto
            If id_tercero <> -1 Then
                ContactoEditado.id_tercero = id_tercero
            ElseIf id_suc <> -1 Then
                ContactoEditado.id_sucursal = id_suc
            ElseIf id_cliente <> -1 Then
                ContactoEditado.id_cliente = id_cliente
            Else
                ContactoEditado.id_succliente = id_sox
            End If

            db.AdminContacto.Add(ContactoEditado)
        Else
            ContactoEditado = (From tr In db.AdminContacto
                               Where tr.id = id_contacto
                               Select tr).FirstOrDefault
        End If





        If ContactoEditado IsNot Nothing Then


            ContactoEditado.Cargo = txtDireccion.Text
            ContactoEditado.Nombre = txtNombre.Text
            ContactoEditado.Telefono = txtTelefono.Text
            ContactoEditado.Direccion = txtdir.Text
            ContactoEditado.Celular = TxtCelular.Text
            ContactoEditado.Correo = Txtcorreo.Text
            ContactoEditado.id_ciudad = txtCiudad.SelectedValue
        End If

        db.SaveChanges()
        Utilidades.Mensaje("Contacto Guardado", Me)
        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_contacto = -1 Then
            Utilidades.RegistrarAuditoria("Contactos", ContactoEditado.id, ContactoEditado.Nombre, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Contactos", ContactoEditado.id, ContactoEditado.Nombre, "Actualización Registro", User.Identity.Name, ids)
        End If
        If id_tercero <> -1 Then
            Response.Redirect("../Vistas/Contactos.aspx?id_tercero=" & id_tercero)
        ElseIf id_suc <> -1 Then
            Response.Redirect("../Vistas/Contactos.aspx?id_suc=" & id_suc)
        ElseIf id_cliente <> -1 Then
            Response.Redirect("../Vistas/Contactos.aspx?id_cliente=" & id_cliente)
        Else
            Response.Redirect("../Vistas/Contactos.aspx?id_sox=" & id_sox)
        End If





    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        If id_tercero <> -1 Then
            Response.Redirect("../Vistas/Contactos.aspx?id_tercero=" & id_tercero)
        ElseIf id_suc <> -1 Then
            Response.Redirect("../Vistas/Contactos.aspx?id_suc=" & id_suc)
        ElseIf id_cliente <> -1 Then
            Response.Redirect("../Vistas/Contactos.aspx?id_clliente=" & id_cliente)
        Else
            Response.Redirect("../Vistas/Contactos.aspx?id_sox=" & id_sox)
        End If
    End Sub

    Protected Sub txtpais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtpais.SelectedIndexChanged
        txtDpto.DataBind()
    End Sub

    Protected Sub txtDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtDpto.SelectedIndexChanged
        txtCiudad.DataBind()
    End Sub
End Class