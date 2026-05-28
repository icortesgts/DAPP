Imports DAPP
Imports System.Data
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Security.Principal
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Web.UI

Public Class Utilidades

    Public Shared control As Control
    Public Shared mail As MailMessage

    Private Shared Property db As New DAPP_BDEntities

    ''' <summary>
    ''' Retotna nombre de usuario de aplicacion.
    ''' </summary>
    Public Shared Function UsuarioRed(ByVal Page As System.Web.UI.Page) As String

        If Page.User.Identity.Name.IndexOf("\") > 0 Then
            Return Page.User.Identity.Name.Split("\").GetValue(1)
        Else
            Return Page.User.Identity.Name
        End If
    End Function
    Private Shared ReadOnly Property SmtpServer As SmtpClient
        Get
            Dim SmtpServer_ As SmtpClient = New SmtpClient
            SmtpServer_.Credentials = New Net.NetworkCredential("soporteSIRL@greentechsupply.co", "Gts@5729806")
            SmtpServer_.Host = "smtpout.secureserver.net"
            Return SmtpServer_
        End Get
    End Property
    Public Shared Sub SendCompletedCallback(sender As Object, e As System.ComponentModel.AsyncCompletedEventArgs)
        ' Get the unique identifier for this asynchronous operation. 
        Dim token As String = CStr(e.UserState)
        If e.Cancelled Then
            Mensaje(String.Format("[{0}] Send canceled.", token), control)
        End If
        If e.Error IsNot Nothing Then
            Mensaje(String.Format("[{0}] {1}", token, e.Error.ToString()), control)
        Else
            Mensaje("Correo enviado satisfactoriamente", control)
        End If
        mail.Dispose()
    End Sub
    Public Shared Function ConsultarUsuario(NomUsuario As String) As Usuarios

        Dim Usuario As Usuarios = (From us In db.Usuarios.Include("AdminPerfiles")
                                   Where us.usuario.Equals(NomUsuario)
                                   Select us).FirstOrDefault

        Return Usuario

    End Function

    Public Shared Sub AbrirPagina(url As String, control As Control)
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "window.open('" & url & "');", True)
    End Sub

    Public Shared Sub RegistrarScript(script As String, control As Control)
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", script, True)
    End Sub

    Public Shared Sub Mensaje(mensaje As String, control As Control)
        ScriptManager.RegisterStartupScript(control, control.GetType(), "Open", "alert('" & mensaje & "');", True)
    End Sub

    Public Shared Sub RegistrarAuditoria(tabla As String, id As Long, obs As String, operacion As String, usuario As String, id_sucursal As Long)
        Dim regAudit As New Auditoria
        db.Auditoria.Add(regAudit)
        regAudit.Tabla = tabla
        regAudit.id_tabla = id
        regAudit.Observaciones = obs
        regAudit.Operacion = operacion
        regAudit.Fecha = Now()
        regAudit.usuario = usuario
        If id_sucursal = 0 Then
            regAudit.id_sucursal = Nothing
        Else
            regAudit.id_sucursal = id_sucursal
        End If

        db.SaveChanges()
    End Sub

    Public Shared Sub ActualizaEstadoUbicacion(id_ubicacion As Long, estado As String)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "update documentos set estado='" & estado & "' where id_ubicacion=" & id_ubicacion
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub ActualizaCapacidadUbicacion(id_ubicacion As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "update ubicaciones set capacidad=(select capacidad from admintipoubicacion where id=ubicaciones.id_tipo)-(select sum(folios) from documentos where id_ubicacion=ubicaciones.id) where id=" & id_ubicacion
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub EliminarCampos(id_documento As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "delete campos_documentos where id_doc=" & id_documento
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub EliminarClientes(id_usuario As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "delete usuario_cliente where id_usuario=" & id_usuario
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub EliminarTerceros(id_usuario As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "delete usuarios_terceros where id_usuario=" & id_usuario
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub Eliminarareas(id_usuario As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "delete usuarios_areas where id_usuario=" & id_usuario
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub EliminarSucursal(id_usuario As Long, id_cliente As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "delete usuario_sucursal where id_usuario=" & id_usuario & " and id_sucursal in (select id from admin_sucursal where id_cliente=" & id_cliente & ")"
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub EliminarSucursal(id_usuario As Long)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "delete usuario_sucursal where id_usuario=" & id_usuario
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub ActualizaEstadoSolicitud(id_solicitud As Long, estado As String)

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnection").ToString)
        cnn.Open()
        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "update documentos set estado='" & estado & "' where id in (select id_documento from Documentos_solicitud where id_solicitud=" & id_solicitud & ")"
        adp.SelectCommand.CommandType = CommandType.Text
        adp.SelectCommand.ExecuteNonQuery()
        cnn.Close()

    End Sub
    Public Shared Sub ColorRecursive(color As String, id As Long)

        Dim n = (From arx In db.Areas
                 Where arx.id_area = id
                 Select arx).ToList
        For Each aNode In n
            aNode.Color = color
            ColorRecursive(color, aNode.id)
            db.SaveChanges()
        Next
    End Sub

    ' Call the procedure using the top nodes of the treeview.
    Public Shared Sub Callcolor(color As String, id As Long)
        Dim n = (From arx In db.Areas
                 Where arx.id = id
                 Select arx).FirstOrDefault
        If n IsNot Nothing Then
            ColorRecursive(color, n.id)
        End If

    End Sub
    Public Shared Function CorreoUsuario(ByVal usuario_p As String) As String


        Dim user As Usuarios = (From us In db.Usuarios
                                Where us.Usuario = usuario_p
                                Select us).FirstOrDefault
        If user Is Nothing OrElse user.email Is Nothing Then
            Return ""
        Else
            Return user.email
        End If

    End Function
    Public Shared Function CorreosNotificacion(ByVal usuario As String) As List(Of String)


        Dim lst As New List(Of String)
        Dim user As Usuarios = (From us In db.Usuarios
                                Where us.Usuario = usuario
                                Select us).FirstOrDefault

        If user Is Nothing Then
            Return lst
        Else


            lst.Add(user.email)

            Return lst
        End If

    End Function
    Public Shared Sub sendMail(ByVal mailSubject As String, ByVal mailBodyHtml As String, ByVal mailBodyPlain As String, ByVal mailPriority As MailPriority, ByVal mailFrom As MailAddress, ByVal mailTo As MailAddressCollection, ByVal mailCc As MailAddressCollection, ByVal mailAttachments As AttachmentCollection, ByVal mailEnconding As Encoding, ByVal controlP As Control)
        control = controlP
        'CORREO
        Try
            'FORMATO BÁSICO
            mail = New MailMessage()

            'CORREO PLANO
            Dim plainView As AlternateView = AlternateView.CreateAlternateViewFromString(mailBodyPlain, mailEnconding, "text/plain")
            mail.AlternateViews.Add(plainView)
            'CORREO HTML
            Dim htmlView As AlternateView = AlternateView.CreateAlternateViewFromString(mailBodyHtml, mailEnconding, "text/html")
            ''IMAGENES EMBEBIDAS           
            mail.AlternateViews.Add(htmlView)

            'ENCODING?
            mail.Priority = mailPriority
            mail.BodyEncoding = mailEnconding

            'DATOS CORREO
            'FROM
            mail.From = mailFrom
            'TO
            For Each mt As MailAddress In mailTo
                mail.To.Add(mt)
            Next
            'SUBJECT
            mail.Subject = mailSubject
            'ES HTML?
            mail.IsBodyHtml = True


            'HANDLER
            AddHandler SmtpServer.SendCompleted, AddressOf SendCompletedCallback

            'ENVIAR PRODUCCIÓN
            SmtpServer.Send(mail)
            Mensaje("Correo enviado satisfactoriamente", control)
        Catch ex As Exception
            Mensaje("Error al intentar enviar el correo", control)
        End Try
    End Sub

    Public Shared Sub SincronizarSegmentos(id_usuario As Long)

        Dim dbWF As New WorkprocessEntities
        Dim dbDAPP As New DAPP_BDEntities

        For Each Cliente In dbDAPP.Clientes
            Dim clWF As Segmentos = (From sg In dbWF.Segmentos
                                     Where sg.nombre = Cliente.nombre
                                     Select sg).FirstOrDefault
            If clWF Is Nothing Then
                Dim nCLWF As New Segmentos
                nCLWF.empresa_id = 19
                nCLWF.nombre = Cliente.nombre
                dbWF.Segmentos.Add(nCLWF)
            End If
        Next

        Dim Usuario As Usuarios = (From us_dapp In dbDAPP.Usuarios
                                   Where us_dapp.ID = id_usuario
                                   Select us_dapp).FirstOrDefault

        If Usuario IsNot Nothing Then
            Dim usWF As UsuariosWF = (From us In dbWF.Usuarios
                                      Where us.usuario = Usuario.usuario
                                      Select us).FirstOrDefault
            If usWF Is Nothing Then
                usWF = New UsuariosWF
                usWF.bloqueado = False
                usWF.Cargo = "Administrador"
                usWF.email = Usuario.email
                usWF.empresa_id = 19
                usWF.fecha_password = Date.Now
                usWF.nombre = Usuario.nombre
                usWF.password = Usuario.password
                usWF.perfil_id = 3
                usWF.rol_id = 93

                dbWF.Usuarios.Add(usWF)
            End If

            Dim Us_Clientes = From us_cl In dbDAPP.Usuario_Cliente.Include("Clientes")
                              Where us_cl.id_usuario = Usuario.ID
                              Select us_cl.Clientes

            Dim Us_Segmentos = From us_sg In dbWF.UsuariosSegmento
                               Where us_sg.usuario = usWF.usuario
                               Select us_sg



            ' ELIMINAR ACTUALES '
            For Each us_sg In Us_Segmentos
                dbWF.UsuariosSegmento.Remove(us_sg)
            Next

            If Usuario.Todos Then
                For Each Cl In db.Clientes
                    Dim Sgmt As Segmentos = (From sg In dbWF.Segmentos
                                             Where sg.nombre = Cl.nombre
                                             Select sg).FirstOrDefault
                    Dim nSegmentoUsuario As New UsuariosSegmento
                    nSegmentoUsuario.segmento_id = Sgmt.MSysID
                    nSegmentoUsuario.usuario = usWF.usuario
                    dbWF.UsuariosSegmento.Add(nSegmentoUsuario)
                Next
            Else
                For Each Cl In Us_Clientes
                    Dim Sgmt As Segmentos = (From sg In dbWF.Segmentos
                                             Where sg.nombre = Cl.nombre
                                             Select sg).FirstOrDefault
                    Dim nSegmentoUsuario As New UsuariosSegmento
                    nSegmentoUsuario.segmento_id = Sgmt.MSysID
                    nSegmentoUsuario.usuario = usWF.usuario
                    dbWF.UsuariosSegmento.Add(nSegmentoUsuario)
                Next
            End If
        End If

        dbWF.SaveChanges()

    End Sub


End Class
