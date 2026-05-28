Imports Gma.QrCodeNet.Encoding.QrCode
Imports System.IO
Imports System.Drawing
Public Class EditarCliente
    Inherits System.Web.UI.Page


    Private ClienteEditado As Clientes
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_cliente As Long
        Get
            If Me.ClientQueryString.Contains("id_cliente") Then
                Return CLng(Request.QueryString.Get("id_cliente"))
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

            If id_cliente <> -1 Then

                ClienteEditado = (From cl In db.Clientes
                                  Where cl.id = id_cliente
                                  Select cl).FirstOrDefault

                If ClienteEditado IsNot Nothing Then
                    BoundCliente(ClienteEditado)
                End If
            Else
                txtpais.DataBind()
                txtpais.SelectedValue = 539
                txtDpto.DataBind()
                txtCiudad.DataBind()
            End If
        End If
    End Sub

    Private Sub BoundCliente(ClienteEditado As Clientes)
        Dim lnk As String
        lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
        txtCelular.Text = ClienteEditado.celular
        txtCorreo.Text = ClienteEditado.correo_electronico
        txtDireccion.Text = ClienteEditado.direccion
        txtNombre.Text = ClienteEditado.nombre
        txtNumDocumento.ValueLong = ClienteEditado.numero_documento
        txtTelefono.Text = ClienteEditado.telefono
        txtTipoDocumento.SelectedValue = ClienteEditado.tipo_documento
        Txtprefijo.Text = ClienteEditado.Prefijo
        If ClienteEditado.Logo IsNot Nothing Then
            imglogo.ImageUrl = lnk & ClienteEditado.Logo
        End If
        If ClienteEditado.Inicio IsNot Nothing Then
            imginicio.ImageUrl = lnk & ClienteEditado.Inicio
        End If
        Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                    Where cx.codigo = ClienteEditado.id_ciudad
                    Select cx).FirstOrDefault
        If city IsNot Nothing Then
            txtpais.DataBind()
            txtpais.SelectedValue = city.id_pais
            txtDpto.DataBind()
            txtDpto.SelectedValue = city.departamento
            txtCiudad.DataBind()
            txtCiudad.SelectedValue = ClienteEditado.id_ciudad
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim extension As String
        Dim cadena As String
        Dim aux As Integer
        If id_cliente = -1 Then
            ClienteEditado = New Clientes
            ClienteEditado.fecha_creacion = Today
            db.Clientes.Add(ClienteEditado)
        Else
            ClienteEditado = (From cl In db.Clientes
                              Where cl.id = id_cliente
                              Select cl).FirstOrDefault
        End If

        If Not UtilityDB_Common.IsValidEmail(txtCorreo.Text) Then
            cvPagina.ErrorMessage = "El correo ingresado es invalido."
            cvPagina.IsValid = False
            Return
        End If

        If id_cliente = -1 Then
            Dim Cuantos As Integer = (From cl In db.Clientes
                                      Where cl.numero_documento = txtNumDocumento.Text
                                      Select cl).Count
            If Cuantos > 0 Then
                cvPagina.ErrorMessage = "Este cliente ya se encuentra registrado en el sistema."
                cvPagina.IsValid = False
                Return
            End If
        End If

        If ClienteEditado IsNot Nothing Then
            ClienteEditado.celular = txtCelular.Text
            ClienteEditado.correo_electronico = txtCorreo.Text
            ClienteEditado.direccion = txtDireccion.Text
            ClienteEditado.nombre = txtNombre.Text
            ClienteEditado.numero_documento = txtNumDocumento.ValueLong
            ClienteEditado.telefono = txtTelefono.Text
            ClienteEditado.tipo_documento = txtTipoDocumento.SelectedValue
            ClienteEditado.id_ciudad = txtCiudad.SelectedValue
            ClienteEditado.Prefijo = Txtprefijo.Text
        End If
        If fllogo.HasFile Then
            extension = fllogo.FileName
            aux = InStrRev(extension, ".")
            extension = Mid(extension, aux + 1)
            cadena = "~/Archivos/Logo" & ClienteEditado.id & "." & extension
            Dim filePath As String =
                Server.MapPath(cadena)
            fllogo.SaveAs(filePath)
            ClienteEditado.Logo = "Logo" & ClienteEditado.id & "." & extension
        End If
        If flinicio.HasFile Then
            extension = flinicio.FileName
            aux = InStrRev(extension, ".")
            extension = Mid(extension, aux + 1)
            cadena = "~/Archivos/Inicio" & ClienteEditado.id & "." & extension
            Dim filePath As String =
                Server.MapPath(cadena)
            flinicio.SaveAs(filePath)
            ClienteEditado.Inicio = "Inicio" & ClienteEditado.id & "." & extension
        End If
        db.SaveChanges()
        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_cliente = -1 Then
            Utilidades.RegistrarAuditoria("Clientes", ClienteEditado.id, ClienteEditado.nombre, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Clientes", ClienteEditado.id, ClienteEditado.nombre, "Actualización Registro", User.Identity.Name, ids)
        End If
        Utilidades.Mensaje("Cliente Guardado", Me)
        Response.Redirect("../Admin/Page_Clientes.aspx")
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Clientes.aspx")

    End Sub

    Protected Sub txtpais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtpais.SelectedIndexChanged
        txtDpto.DataBind()
    End Sub

    Protected Sub txtDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtDpto.SelectedIndexChanged
        txtCiudad.DataBind()
    End Sub
End Class