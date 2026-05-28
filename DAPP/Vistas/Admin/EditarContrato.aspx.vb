
Imports System.Text
Imports System.Security.Cryptography
Public Class EditarContrato
    Inherits System.Web.UI.Page


    Private ClienteEditado As Contratos_Cliente
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
    Private ReadOnly Property id_contrato As Long
        Get
            If Me.ClientQueryString.Contains("id_contrato") Then
                Return CLng(Request.QueryString.Get("id_contrato"))
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
                If id_contrato <> -1 Then
                    ClienteEditado = (From cl In db.Contratos_Cliente
                                      Where cl.id = id_contrato
                                      Select cl).FirstOrDefault

                    If ClienteEditado IsNot Nothing Then
                        BoundCliente(ClienteEditado)
                    End If
                Else
                    TxtFecha.Text = Format(Now(), "yyyy-MM-dd")
                    TxtFechaFin.Text = Format(Now().AddYears(1), "yyyy-MM-dd")
                    TxtLicencia.Text = "Pendiente"
                End If

            Else
                Response.Redirect("../Admin/Page_Clientes.aspx")
            End If
        End If
    End Sub

    Private Sub BoundCliente(ClienteEditado As Contratos_Cliente)
        txtNro.Text = ClienteEditado.Nro_Contrato
        TxtFecha.Text = Format(ClienteEditado.Fecha_inicio, "yyyy-MM-dd")
        TxtFechaFin.Text = Format(ClienteEditado.Fecha_fin, "yyyy-MM-dd")
        txtEstado.SelectedValue = ClienteEditado.Estado
        TxtTipo.SelectedValue = ClienteEditado.Tipo_Pago
        TxtLicencia.Text = ClienteEditado.Licencia
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim cadena As String
        If id_contrato = -1 Then
            ClienteEditado = New Contratos_Cliente
            ClienteEditado.id_cliente = id_cliente
            db.Contratos_Cliente.Add(ClienteEditado)
        Else
            ClienteEditado = (From cl In db.Contratos_Cliente.Include("Clientes")
                              Where cl.id = id_contrato
                              Select cl).FirstOrDefault
        End If


        If id_contrato = -1 Then
            Dim Cuantos As Integer = (From cl In db.Contratos_Cliente
                                      Where cl.Nro_Contrato = txtNro.Text And cl.id_cliente = id_cliente
                                      Select cl).Count
            If Cuantos > 0 Then
                cvPagina.ErrorMessage = "Este Nro De Contrato ya se encuentra registrado en el sistema."
                cvPagina.IsValid = False
                Return
            End If
        End If

        If ClienteEditado IsNot Nothing Then

            ClienteEditado.Nro_Contrato = txtNro.Text
            ClienteEditado.Fecha_inicio = TxtFecha.Text
            ClienteEditado.Fecha_fin = TxtFechaFin.Text
            ClienteEditado.Estado = txtEstado.SelectedValue
            ClienteEditado.Tipo_Pago = TxtTipo.Text
            ClienteEditado.Licencia = TxtLicencia.Text
        End If

        db.SaveChanges()
        If id_contrato = -1 Then
            cadena = txtNro.Text & " - " & TxtFecha.Text
            TxtLicencia.Text = generarClaveSHA1(cadena)
            ClienteEditado.Licencia = TxtLicencia.Text
        End If
        db.SaveChanges()
        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_contrato = -1 Then
            Utilidades.RegistrarAuditoria("Contrato Clientes", ClienteEditado.id, ClienteEditado.Nro_Contrato & " - " & ClienteEditado.Fecha_inicio, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Contrato Clientes", ClienteEditado.id, ClienteEditado.Nro_Contrato & " - " & ClienteEditado.Fecha_inicio, "Actualización Registro", User.Identity.Name, ids)
        End If
        Utilidades.Mensaje("Cliente Guardado", Me)
        Response.Redirect("../Admin/Page_Clientes.aspx")
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Clientes.aspx")

    End Sub
    Function generarClaveSHA1(ByVal cadena As String) As String

        Dim enc As New UTF8Encoding
        Dim data() As Byte = enc.GetBytes(cadena)
        Dim result() As Byte

        Dim sha As New SHA1CryptoServiceProvider

        result = sha.ComputeHash(data)

        Dim sb As New StringBuilder
        Dim max As Int32 = result.Length



        For i As Integer = 0 To max - 1


            'Convertimos los valores en hexadecimal
            'cuando tiene una cifra hay que rellenarlo con cero
            'para que siempre ocupen dos dígitos.
            If (result(i) < 16) Then
                sb.Append("0")
            End If

            sb.Append(result(i).ToString("x"))


        Next


        'Devolvemos la cadena con el hash en mayúsculas para que quede más chuli :)
        generarClaveSHA1 = sb.ToString().ToUpper()


    End Function

End Class