Public Class EditarBackup
    Inherits System.Web.UI.Page


    Private SucursalEditada As Backups
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_backup As Long
        Get
            If Me.ClientQueryString.Contains("id_backup") Then
                Return CLng(Request.QueryString.Get("id_backup"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load


        Hdd_id_usuario.Value = Utilidades.ConsultarUsuario(User.Identity.Name).ID

        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_backup <> -1 Then

                SucursalEditada = (From cl In db.Backups
                                   Where cl.id = id_backup
                                   Select cl).FirstOrDefault

                If SucursalEditada IsNot Nothing Then
                    BoundCliente(SucursalEditada)
                End If
            Else
                txtFecha.Text = Format(Now(), "yyyy-MM-yy hh:mm:ss tt")
                TxtFecResp.Text = Format(Now(), "yyyy-MM-yy hh:mm:ss tt")
                txtUsuario.Text = Utilidades.ConsultarUsuario(User.Identity.Name).nombre
                txtCliente.DataBind()
                If Session("id_cliente") IsNot Nothing Then
                    txtCliente.SelectedValue = Session("id_cliente")
                End If
                If Session("id_sucursal") IsNot Nothing Then
                    txtSucursal.DataBind()
                    txtSucursal.SelectedValue = Session("id_sucursal")
                End If
                TxtRuta.Text = "Pendiente"
                TxtRuta.NavigateUrl = "#"
            End If
        End If
    End Sub

    Private Sub BoundCliente(SucursalEditada As Backups)
        txtFecha.Text = Format(SucursalEditada.Fecha_solicitud, "yyyy-MM-yy hh:mm:ss tt")
        TxtFecResp.Text = Format(SucursalEditada.fecha_respuesta, "yyyy-MM-yy hh:mm:ss tt")
        Dim idusr = SucursalEditada.id_usuario
        Dim usrx = (From ul In db.Usuarios
                    Where ul.ID = idusr
                    Select ul).FirstOrDefault
        If usrx IsNot Nothing Then
            txtUsuario.Text = usrx.nombre
        End If

        txtCliente.DataBind()
        txtCliente.SelectedValue = SucursalEditada.id_usuario
        If SucursalEditada.id_sucursal IsNot Nothing Then
            txtSucursal.DataBind()
            txtSucursal.SelectedValue = SucursalEditada.id_sucursal
        End If
        TxtEstado.SelectedValue = SucursalEditada.Estado
        If TxtEstado.SelectedValue = "Disponible" Then
            TxtRuta.Text = SucursalEditada.Ruta
            TxtRuta.NavigateUrl = SucursalEditada.Ruta
        Else
            TxtRuta.Text = SucursalEditada.Ruta
            TxtRuta.NavigateUrl = "#"
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        Dim ids As Long
        ids = CLng(Session("id_cliente"))
        If id_backup = -1 Then
            SucursalEditada = New Backups
            SucursalEditada.id_usuario = Hdd_id_usuario.Value
            db.Backups.Add(SucursalEditada)
        Else
            SucursalEditada = (From cl In db.Backups
                               Where cl.id = id_backup
                               Select cl).FirstOrDefault
        End If




        If SucursalEditada IsNot Nothing Then
            SucursalEditada.Fecha_solicitud = txtFecha.Text
            SucursalEditada.fecha_respuesta = TxtFecResp.Text
            SucursalEditada.Estado = TxtEstado.SelectedValue
            SucursalEditada.id_cliente = txtCliente.SelectedValue
            SucursalEditada.Ruta = TxtRuta.Text
            db.SaveChanges()
            If id_backup = -1 Then
                SucursalEditada.Estado = "En Proceso"
                db.SaveChanges()
            End If
            Enviar_Correo()
            If Session("id_backup") IsNot Nothing Then
                ids = Session("id_backup")
            Else
                ids = 0
            End If
            If id_backup = -1 Then
                Utilidades.RegistrarAuditoria("Backups", SucursalEditada.id, Utilidades.ConsultarUsuario(User.Identity.Name).nombre & " - " & SucursalEditada.Fecha_solicitud, "Nuevo Registro", User.Identity.Name, ids)
            Else
                Utilidades.RegistrarAuditoria("Backups", SucursalEditada.id, Utilidades.ConsultarUsuario(User.Identity.Name).nombre & " - " & SucursalEditada.Fecha_solicitud, "Actualización Registro", User.Identity.Name, ids)
            End If
            Utilidades.Mensaje("Solicitud Recibida", Me)
            Response.Redirect("../Admin/Page_Backup.aspx")

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Backup.aspx")
    End Sub


    Private Sub Enviar_Correo()
        Dim correoDesde As String = ""
        Dim correoHasta As String = ""
        Dim subject As String
        correoDesde = "soporteSIRL@greentechsupply.co"
        Dim trx = (From tx In db.Usuarios
                   Where tx.ID = Hdd_id_usuario.Value
                   Select tx).FirstOrDefault
        If trx IsNot Nothing Then
            correoHasta = trx.email
            Dim bodyPlain As String
            Dim bodyHtml As String
            If id_backup <> -1 Then
                subject = "Actualización Solicitud Copia de Seguridad - " & txtUsuario.Text
                bodyPlain = subject & Environment.NewLine & Environment.NewLine &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                    "Se generó una actualización  de su solicitud Nro." & id_backup & Environment.NewLine &
                    "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Coordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                    "Soporte SIRL" & Environment.NewLine & "soporteSIRL@greentechsupply.co"
                bodyHtml = "<html><body><p>" & subject & "</p><br /><br /><p>" &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                    "<p>Se generó una actualización  de su solicitud Nro." & id_backup & "<br />" &
                    "Agradecemos su atención.<br /><br />Coordial saludo.<br />Green Tech Supply - SIRL<br /><br />" &
                    "<b><font color=#336699><a href='http://www.greentechsupply.co/sirl/'>SIRL</a></font></b><br />" &
                    "<a href='http://www.greentechsupply.co/sirl/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/sirl/" &
                    "Images/SIRL.gif' /></a>" &
                    "<img src='http://www.greentechsupply.co/sirl/Images/logo.gif'></body></html>"
            Else
                subject = "Nueva Solicitud de Copia de Seguridad - " & txtUsuario.Text
                bodyPlain = subject & Environment.NewLine & Environment.NewLine &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                    "Se generó una nueva solicitud, lo invitamos a que consulte el sistema para realizar el seguimiento respectivo " & Environment.NewLine &
                    "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Coordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                    "Soporte SIRL" & Environment.NewLine & "soporteSIRL@greentechsupply.co"
                bodyHtml = "<html><body><p>" & subject & "</p><br /><br /><p>" &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                    "<p>Se generó una nueva solicitud, lo invitamos a que consulte el sistema para realizar el seguimiento respectivo <br />" &
                    "Agradecemos su atención.<br /><br />Coordial saludo.<br />Green Tech Supply - SIRL<br /><br />" &
                    "<b><font color=#336699><a href='http://www.greentechsupply.co/sirl/'>SIRL</a></font></b><br />" &
                    "<a href='http://www.greentechsupply.co/sirl/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/sirl/" &
                    "Images/SIRL.gif' /></a>" &
                    "<img src='http://www.greentechsupply.co/sirl/Images/logo.gif'></body></html>"
            End If

            Dim mailFrom As New Net.Mail.MailAddress(IIf(String.IsNullOrWhiteSpace(correoDesde), "info@greentechsupply.co", correoDesde))
            Dim mailTo As New Net.Mail.MailAddressCollection
            mailTo.Add(New Net.Mail.MailAddress(correoHasta))
            Dim mailCc As New Net.Mail.MailAddressCollection
            For Each u In Utilidades.CorreosNotificacion(User.Identity.Name)
                If Not String.IsNullOrWhiteSpace(u) Then
                    mailCc.Add(New Net.Mail.MailAddress(u))
                End If
            Next
            Utilidades.sendMail("Mensaje - " & subject, bodyHtml, bodyPlain, Net.Mail.MailPriority.High, mailFrom, mailTo, mailCc, Nothing, Encoding.Default, Me)
        End If



    End Sub

    Protected Sub txtCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtCliente.SelectedIndexChanged
        txtSucursal.DataBind()
    End Sub
End Class