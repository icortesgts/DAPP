Imports System.IO
Public Class EditarPrsetamo
    Inherits System.Web.UI.Page


    Private SolicitudEditada As Solicitudes
    Private db As New DAPP_BDEntities
    Private arch As Byte()
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_solicitud As Long
        Get
            If Me.ClientQueryString.Contains("id_solicitud") Then
                Return CLng(Request.QueryString.Get("id_solicitud"))
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

            If id_solicitud <> -1 Then

                SolicitudEditada = (From dc In db.Solicitudes.Include("Documentos_Solicitud").Include("Ubicaciones")
                                    Where dc.id = id_solicitud
                                    Select dc).FirstOrDefault

                If SolicitudEditada IsNot Nothing Then
                    BoundDocumento(SolicitudEditada)
                End If
            Else
                txtFechaInicio.Text = Format(Now.Date(), "yyyy-MM-dd")
                txtFdevolucion.Text = Format(Now.Date(), "yyyy-MM-dd")
                txtFEdevolucion.Text = Format(Now.Date(), "yyyy-MM-dd")
                txtFPrestamo.Text = Format(Now.Date(), "yyyy-MM-dd")
                Hdd_id_documento.Value = 0
                Hdd_id_responsable.Value = 0
                Hdd_id_solicitante.Value = 0
                Hdd_id_ubicacion.Value = 0
            End If
        End If
    End Sub

    Private Sub BoundDocumento(SolicitudEditada As Solicitudes)


        If SolicitudEditada.id_ubicacion IsNot Nothing Then
            TxtUbicacion.Text = SolicitudEditada.Ubicaciones.ubicacion
            Hdd_id_ubicacion.Value = SolicitudEditada.id_ubicacion
        Else
            TxtUbicacion.Text = ""
            Hdd_id_ubicacion.Value = 0
        End If

        txtFechaInicio.Text = Format(SolicitudEditada.Fecha, "yyyy-MM-dd")
        If SolicitudEditada.Fecha_prestamo IsNot Nothing Then
            txtFPrestamo.Text = Format(SolicitudEditada.Fecha_prestamo, "yyyy-MM-dd")
        End If
        If SolicitudEditada.Fecha_estimada IsNot Nothing Then
            txtFEdevolucion.Text = Format(SolicitudEditada.Fecha_estimada, "yyyy-MM-dd")
        End If
        If SolicitudEditada.fecha_devolucion IsNot Nothing Then
            txtFdevolucion.Text = Format(SolicitudEditada.fecha_devolucion, "yyyy-MM-dd")
        End If
        Dim SolicitudDocumento = (From dsc In db.Documentos_Solicitud.Include("Documentos")
                                  Where dsc.id_solicitud = SolicitudEditada.id
                                  Select dsc).ToList
        lstDocumentos.Items.Clear()
        For Each ds In SolicitudDocumento
            lstDocumentos.Items.Add(New ListItem With {.Text = ds.Documentos.alias, .Value = ds.id_documento})
        Next
        Dim trx As Terceros
        trx = (From tx In db.Terceros
               Where tx.id = SolicitudEditada.id_responsable
               Select tx).FirstOrDefault
        If trx IsNot Nothing Then
            txtResponsable.Text = trx.nombre
            Hdd_id_responsable.Value = SolicitudEditada.id_responsable
        End If
        trx = (From tx In db.Terceros
               Where tx.id = SolicitudEditada.id_solicitante
               Select tx).FirstOrDefault
        If trx IsNot Nothing Then
            txtSolicitante.Text = trx.nombre
            Hdd_id_solicitante.Value = SolicitudEditada.id_solicitante
        End If
        TxtDuracion.Text = SolicitudEditada.duracion
        txtEstado.SelectedValue = SolicitudEditada.Estado
        If SolicitudEditada.motivo IsNot Nothing Then
            Txtmotivo.Text = SolicitudEditada.motivo
        End If
        If SolicitudEditada.observaciones IsNot Nothing Then
            TxtObservaciones.Text = SolicitudEditada.observaciones
        End If
        If txtEstado.SelectedValue = "Negado" Or txtEstado.SelectedValue = "Devuelto" Then
            txtEstado.Enabled = False
            txtFechaInicio.Enabled = False
            txtFdevolucion.Enabled = False
            txtFEdevolucion.Enabled = False
            txtFPrestamo.Enabled = False
            TxtObservaciones.Enabled = False
            BttResponsable.Enabled = False
        Else
            txtEstado.Enabled = True
            txtFechaInicio.Enabled = True
            txtFdevolucion.Enabled = True
            txtFEdevolucion.Enabled = True
            txtFPrestamo.Enabled = True
            TxtObservaciones.Enabled = True
            BttResponsable.Enabled = True
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        Dim ids As Long
        Dim prest As Boolean


        ids = CLng(Session("id_sucursal"))

        If TxtDocumento.Text = "" Then
            Hdd_id_documento.Value = 0
        End If
        If TxtUbicacion.Text = "" Then
            Hdd_id_ubicacion.Value = 0
        End If


        prest = False
        If lstDocumentos.Items.Count = 0 And Hdd_id_ubicacion.Value = 0 Then
            Utilidades.Mensaje("Tiene que Escoger un Documento o una Carpeta", Me)
        Else
            If lstDocumentos.Items.Count > 0 And Hdd_id_ubicacion.Value <> 0 Then
                Utilidades.Mensaje("Tiene que Escoger solo un Documento o una Carpeta", Me)
            End If
            If Hdd_id_responsable.Value = 0 Then
                Utilidades.Mensaje("Tiene que Escoger un Responsable", Me)
            ElseIf Hdd_id_solicitante.Value = 0 Then
                Utilidades.Mensaje("Tiene que Escoger un Solicitante", Me)
            ElseIf CDate(txtFPrestamo.Text) < CDate(txtFechaInicio.Text) Then
                Utilidades.Mensaje("La fecha de prestamo no puede ser menor a la fecha de la solicitud.", Me)
            ElseIf CDate(txtFEdevolucion.Text) < CDate(txtFPrestamo.Text) Then
                Utilidades.Mensaje("La fecha Estimada dedevolución no puede ser menor a La fecha de prestamo.", Me)
            ElseIf CDate(txtFdevolucion.Text) < CDate(txtFPrestamo.Text) Then
                Utilidades.Mensaje("La fecha de devolución no puede ser menor a La fecha de prestamo.", Me)
            Else
                If id_solicitud = -1 Then
                    SolicitudEditada = New Solicitudes
                    SolicitudEditada.id_sucursal = ids
                    db.Solicitudes.Add(SolicitudEditada)
                Else
                    SolicitudEditada = (From dc In db.Solicitudes
                                        Where dc.id = id_solicitud
                                        Select dc).FirstOrDefault
                End If

                If SolicitudEditada IsNot Nothing Then
                    If SolicitudEditada.Estado <> "Solicitado" And txtEstado.SelectedValue = "Solicitado" Then
                        Utilidades.Mensaje("Ese Cambio de Estado no es válido", Me)


                    Else
                        If txtEstado.SelectedValue = "Prestado" Then
                            For Each it As ListItem In lstDocumentos.Items
                                Dim docs = (From dsc In db.Documentos
                                            Where dsc.id = it.Value
                                            Select dsc).FirstOrDefault
                                If docs IsNot Nothing Then
                                    If docs.Estado IsNot Nothing Then
                                        If docs.Estado = "Prestado" Then
                                            prest = True
                                        End If
                                    End If
                                End If
                            Next
                            If Hdd_id_ubicacion.Value <> 0 Then
                                Dim docs = (From dsc In db.Documentos
                                            Where dsc.id_ubicacion = Hdd_id_ubicacion.Value
                                            Select dsc).FirstOrDefault
                                If docs IsNot Nothing Then
                                    If docs.Estado IsNot Nothing Then
                                        If docs.Estado = "Prestado" Then
                                            prest = True
                                        End If
                                    End If
                                End If
                            End If
                            If prest Then
                                Utilidades.Mensaje("Al Menos uno de los documentos solicitados se encuentra prestado, por favor revisar", Me)
                            Else
                                If Hdd_id_ubicacion.Value <> 0 Then
                                    SolicitudEditada.id_ubicacion = Hdd_id_ubicacion.Value
                                Else
                                    SolicitudEditada.id_ubicacion = Nothing
                                End If

                                SolicitudEditada.id_solicitante = Hdd_id_solicitante.Value
                                SolicitudEditada.id_responsable = Hdd_id_responsable.Value
                                SolicitudEditada.duracion = TxtDuracion.Text
                                SolicitudEditada.Estado = txtEstado.SelectedValue
                                SolicitudEditada.Fecha = txtFechaInicio.Text
                                SolicitudEditada.Fecha_prestamo = txtFPrestamo.Text
                                SolicitudEditada.Fecha_estimada = txtFEdevolucion.Text
                                SolicitudEditada.fecha_devolucion = txtFdevolucion.Text
                                SolicitudEditada.motivo = Txtmotivo.Text
                                SolicitudEditada.observaciones = TxtObservaciones.Text
                                db.SaveChanges()

                                If Session("id_sucursal") IsNot Nothing Then
                                    ids = Session("id_sucursal")
                                Else
                                    ids = 0
                                End If
                                If id_solicitud = -1 Then
                                    Utilidades.RegistrarAuditoria("Solicitudes", SolicitudEditada.id, SolicitudEditada.id, "Nuevo Registro", User.Identity.Name, ids)
                                Else
                                    Utilidades.RegistrarAuditoria("Solicitudes", SolicitudEditada.id, SolicitudEditada.id, "Actualización Registro", User.Identity.Name, ids)
                                End If
                                Utilidades.Mensaje("Solicitud Guardada", Me)
                                If Hdd_id_ubicacion.Value <> 0 Then
                                    If txtEstado.SelectedValue = "Prestado" Then
                                        Utilidades.ActualizaEstadoUbicacion(Hdd_id_ubicacion.Value, txtEstado.SelectedValue)
                                    Else
                                        Utilidades.ActualizaEstadoUbicacion(Hdd_id_ubicacion.Value, "Archivado")
                                    End If
                                Else
                                    If txtEstado.SelectedValue = "Prestado" Then
                                        Utilidades.ActualizaEstadoSolicitud(id_solicitud, txtEstado.SelectedValue)
                                    Else
                                        Utilidades.ActualizaEstadoSolicitud(id_solicitud, "Archivado")
                                    End If

                                End If
                                For Each it As ListItem In lstDocumentos.Items
                                    Dim SolicitudDocumento = (From dsc In db.Documentos_Solicitud.Include("Documentos")
                                                              Where dsc.id_solicitud = SolicitudEditada.id And dsc.id_documento = it.Value
                                                              Select dsc).FirstOrDefault
                                    If SolicitudDocumento Is Nothing Then
                                        Dim ar As New Documentos_Solicitud
                                        ar.id_documento = it.Value
                                        ar.id_solicitud = SolicitudEditada.id
                                        db.Documentos_Solicitud.Add(ar)
                                        db.SaveChanges()
                                    End If
                                Next
                                Response.Redirect("../GesDoc/Page_Prestamos.aspx")
                                Enviar_Correo()
                            End If
                        Else
                            If Hdd_id_ubicacion.Value <> 0 Then
                                SolicitudEditada.id_ubicacion = Hdd_id_ubicacion.Value
                            Else
                                SolicitudEditada.id_ubicacion = Nothing
                            End If

                            SolicitudEditada.id_solicitante = Hdd_id_solicitante.Value
                            SolicitudEditada.id_responsable = Hdd_id_responsable.Value
                            SolicitudEditada.duracion = TxtDuracion.Text
                            SolicitudEditada.Estado = txtEstado.SelectedValue
                            SolicitudEditada.Fecha = txtFechaInicio.Text
                            SolicitudEditada.Fecha_prestamo = txtFPrestamo.Text
                            SolicitudEditada.Fecha_estimada = txtFEdevolucion.Text
                            SolicitudEditada.fecha_devolucion = txtFdevolucion.Text
                            SolicitudEditada.motivo = Txtmotivo.Text
                            SolicitudEditada.observaciones = TxtObservaciones.Text
                            db.SaveChanges()

                            If Session("id_sucursal") IsNot Nothing Then
                                ids = Session("id_sucursal")
                            Else
                                ids = 0
                            End If
                            If id_solicitud = -1 Then
                                Utilidades.RegistrarAuditoria("Solicitudes", SolicitudEditada.id, SolicitudEditada.id, "Nuevo Registro", User.Identity.Name, ids)
                            Else
                                Utilidades.RegistrarAuditoria("Solicitudes", SolicitudEditada.id, SolicitudEditada.id, "Actualización Registro", User.Identity.Name, ids)
                            End If
                            Utilidades.Mensaje("Solicitud Guardada", Me)
                            If Hdd_id_ubicacion.Value <> 0 Then
                                If txtEstado.SelectedValue = "Prestado" Then
                                    Utilidades.ActualizaEstadoUbicacion(Hdd_id_ubicacion.Value, txtEstado.SelectedValue)
                                Else
                                    Utilidades.ActualizaEstadoUbicacion(Hdd_id_ubicacion.Value, "Archivado")
                                End If
                            Else
                                Dim doc = (From cl In db.Documentos
                                           Where cl.id = Hdd_id_documento.Value
                                           Select cl).FirstOrDefault
                                If doc IsNot Nothing Then
                                    If txtEstado.SelectedValue = "Prestado" Then
                                        doc.Estado = txtEstado.SelectedValue
                                    Else
                                        doc.Estado = "Archivado"
                                    End If
                                    db.SaveChanges()
                                End If

                            End If
                            For Each it As ListItem In lstDocumentos.Items
                                Dim SolicitudDocumento = (From dsc In db.Documentos_Solicitud.Include("Documentos")
                                                          Where dsc.id_solicitud = SolicitudEditada.id And dsc.id_documento = it.Value
                                                          Select dsc).FirstOrDefault
                                If SolicitudDocumento Is Nothing Then
                                    Dim ar As New Documentos_Solicitud
                                    ar.id_documento = it.Value
                                    ar.id_solicitud = SolicitudEditada.id
                                    db.Documentos_Solicitud.Add(ar)
                                    db.SaveChanges()
                                End If
                            Next
                            Enviar_Correo()
                            Response.Redirect("../GesDoc/Page_Prestamos.aspx")
                        End If



                    End If

                End If

            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../GesDoc/Page_Prestamos.aspx")
    End Sub



    Protected Sub BttSolicitante_Click(sender As Object, e As ImageClickEventArgs) Handles BttSolicitante.Click

        Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)

    End Sub

    Protected Sub BttDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles BttDocumento.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Documento.aspx','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub BttUbicacion_Click(sender As Object, e As ImageClickEventArgs) Handles BttUbicacion.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Ubicacion.aspx','SeleccionarUbicacion','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub BttResponsable_Click(sender As Object, e As ImageClickEventArgs) Handles BttResponsable.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?id_responsable=1','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttAgregarDoc_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarDoc.Click

        Dim doc As Documentos = (From dc In db.Documentos
                                 Where dc.id = Hdd_id_documento.Value
                                 Select dc).FirstOrDefault

        If doc IsNot Nothing Then
            lstDocumentos.Items.Add(New ListItem With {.Text = doc.alias, .Value = doc.id})
        End If
        TxtDocumento.Text = ""
        Hdd_id_documento.Value = 0
    End Sub

    Protected Sub bttEliminarDoc_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarDoc.Click
        Dim it As ListItem = lstDocumentos.SelectedItem

        SolicitudEditada = (From sc In db.Solicitudes.Include("Documentos_Solicitud").Include("Ubicaciones")
                            Where sc.id = id_solicitud
                            Select sc).FirstOrDefault
        If id_solicitud <> -1 Then
            If it IsNot Nothing Then
                If SolicitudEditada IsNot Nothing Then
                    Dim SolicitudDocumento As Documentos_Solicitud = (From dsc In SolicitudEditada.Documentos_Solicitud
                                                                      Where dsc.id_documento = it.Value
                                                                      Select dsc).FirstOrDefault
                    If SolicitudDocumento IsNot Nothing Then
                        db.Documentos_Solicitud.Remove(SolicitudDocumento)
                        db.SaveChanges()
                        BoundDocumento(SolicitudEditada)
                    Else
                        lstDocumentos.Items.Remove(it)
                    End If
                Else
                    lstDocumentos.Items.Remove(it)
                End If
            End If
        Else
            If it IsNot Nothing Then
                lstDocumentos.Items.Remove(it)
            End If
        End If

    End Sub

    Private Sub Enviar_Correo()
        Dim correoDesde As String = ""
        Dim correoHasta As String = ""
        Dim subject As String
        correoDesde = "soporteSIRL@greentechsupply.co"
        Dim trx = (From tx In db.Terceros
                   Where tx.id = Hdd_id_solicitante.Value
                   Select tx).FirstOrDefault
        If trx IsNot Nothing Then
            correoHasta = trx.correo_electronico

            subject = "Solicitud de Prestamo de Documentos - " & txtSolicitante.Text
            Dim bodyPlain As String = subject & Environment.NewLine & Environment.NewLine &
                "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                "Se generó una actualización  de su solicitud Nro." & id_solicitud & Environment.NewLine &
                "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Coordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                "Soporte SIRL" & Environment.NewLine & "soporteSIRL@greentechsupply.co"
            Dim bodyHtml As String = "<html><body><p>" & subject & "</p><br /><br /><p>" &
                "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                "<p>Se generó una actualización  de su solicitud Nro." & id_solicitud & "<br />" &
                "Agradecemos su atención.<br /><br />Coordial saludo.<br />Green Tech Supply - SIRL<br /><br />" &
                "<b><font color=#336699><a href='http://www.greentechsupply.co/sirl/'>SIRL</a></font></b><br />" &
                "<a href='http://www.greentechsupply.co/sirl/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/sirl/" &
                "Images/SIRL.gif' /></a>" &
                "<img src='http://www.greentechsupply.co/sirl/Images/logo.gif'></body></html>"
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

    Protected Sub txtFPrestamo_TextChanged(sender As Object, e As EventArgs) Handles txtFPrestamo.TextChanged
        Dim fec As Date
        If txtFEdevolucion.Text = "" Then
            fec = CDate(txtFPrestamo.Text)
            fec = fec.AddDays(CInt(TxtDuracion.Text))
            txtFEdevolucion.Text = Format(fec.ToString, "yyyy/MM/dd")
            txtFdevolucion.Text = txtFEdevolucion.Text
        End If
    End Sub

    Protected Sub txtEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtEstado.SelectedIndexChanged

    End Sub

    Protected Sub BttDetalle_Click(sender As Object, e As EventArgs) Handles BttDetalle.Click
        If lstDocumentos.Items.Count > 0 Then
            If id_solicitud <> -1 Then
                Utilidades.RegistrarScript("window.open('../Detalle_Documento.aspx?id_solicitud=" & id_solicitud & "','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            Else
                Utilidades.Mensaje("Tiene que grabar para ver los documentos", Me)
            End If

        Else
            Utilidades.Mensaje("No hay documentos disponibles", Me)
        End If
    End Sub
End Class