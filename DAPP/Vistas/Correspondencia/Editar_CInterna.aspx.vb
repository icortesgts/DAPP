Imports System.IO
Public Class Editar_CInterna
    Inherits System.Web.UI.Page


    Private CorrespondenciaEditada As Correspondencia
    Private db As New DAPP_BDEntities
    Private arch As Byte()
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_correspondencia As Long
        Get
            If Me.ClientQueryString.Contains("id_correspondencia") Then
                Return CLng(Request.QueryString.Get("id_correspondencia"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property Mi As Long
        Get
            If Me.ClientQueryString.Contains("Mi") Then
                Return CLng(Request.QueryString.Get("Mi"))
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
            Else
                If id_correspondencia <> -1 Then

                    CorrespondenciaEditada = (From dc In db.Correspondencia
                                              Where dc.id = id_correspondencia
                                              Select dc).FirstOrDefault

                    If CorrespondenciaEditada IsNot Nothing Then
                        BoundDocumento(CorrespondenciaEditada)
                    End If
                Else
                    If Session("id_cliente") IsNot Nothing Then
                        Dim idc As Long
                        idc = Session("id_cliente")
                        Dim pref = (From sc In db.Clientes
                                    Where sc.id = idc
                                    Select sc).FirstOrDefault
                        If pref IsNot Nothing Then
                            TxtRadicado.Text = pref.Prefijo
                        End If
                    End If
                    Dim idt As Long

                    If Usuario.id_tercero IsNot Nothing Then
                        If Usuario.correspondencia = False Then
                            Hdd_id_area.Value = 0
                            Txtarea.Text = ""
                            Hdd_id_responsable.Value = 0
                            txtResponsable.Text = ""
                        Else
                            idt = Usuario.id_tercero
                            Dim arx = (From ax In db.Terceros.Include("Areas")
                                       Where ax.id = idt
                                       Select ax).FirstOrDefault
                            If arx IsNot Nothing Then
                                If arx.id_area IsNot Nothing Then
                                    TxtRadicado.Text = TxtRadicado.Text & arx.Areas.Prefijo
                                    Hdd_id_area.Value = arx.id_area
                                    Txtarea.Text = arx.Areas.nombre
                                Else
                                    TxtRadicado.Text = ""
                                    Hdd_id_area.Value = 0
                                    Txtarea.Text = ""
                                End If
                                lstRemitentes.Items.Add(New ListItem With {.Text = arx.nombre, .Value = arx.id})
                                Hdd_id_responsable.Value = arx.id
                                txtResponsable.Text = arx.nombre
                            End If
                        End If

                    End If

                    TxtfechaC.Text = Format(Now.Date(), "yyyy-MM-dd")
                    Hdd_id_documento.Value = 0
                    Hdd_id_destinatario.Value = 0
                    Hdd_id_mensajeria.Value = 0
                    Hdd_id_remitente.Value = 0

                End If
            End If


        Else
            TabName.Value = Request.Form(TabName.UniqueID)
            If Txtarea.Text = "" Then
                If Hdd_id_responsable.Value <> 0 Then
                    Dim arx = (From ax In db.Terceros.Include("Areas")
                               Where ax.id = Hdd_id_responsable.Value
                               Select ax).FirstOrDefault
                    If arx IsNot Nothing Then
                        If arx.id_area IsNot Nothing Then
                            Txtarea.Text = arx.Areas.nombre
                            If id_correspondencia = -1 Then
                                TxtRadicado.Text = arx.Areas.Prefijo
                            End If
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BoundDocumento(CorrespondenciaEditada As Correspondencia)

        txtTipoDocumento.DataBind()
        txtTipoDocumento.SelectedValue = CorrespondenciaEditada.id_tipodoc
        TxtRadicado.Text = CorrespondenciaEditada.numero_radicado
        txttipoc.SelectedValue = CorrespondenciaEditada.tipo_correspondencia
        TxtAsunto.Text = CorrespondenciaEditada.asunto
        txtDescripcion.Text = CorrespondenciaEditada.observaciones
        txtestado.SelectedValue = CorrespondenciaEditada.Estado

        If txttipoc.SelectedValue = "Interna Enviada" Then
            lblRem.Text = "Remitente"
        Else
            lblRem.Text = "Destinatario"
        End If
        If CorrespondenciaEditada.remitente IsNot Nothing Then
            txtRemitente.Text = CorrespondenciaEditada.remitente
        End If
        If CorrespondenciaEditada.dependencia_remitente IsNot Nothing Then
            txtDest.Text = CorrespondenciaEditada.dependencia_remitente
        End If
        If CorrespondenciaEditada.guia IsNot Nothing Then
            txtGuia.Text = CorrespondenciaEditada.guia
        End If
        txtTipoEnvio.SelectedValue = CorrespondenciaEditada.tipo_envio
        TxtfechaC.Text = Format(CorrespondenciaEditada.fecha_documento, "yyyy-MM-dd")
        If CorrespondenciaEditada.Fecha_respuesta IsNot Nothing Then
            TxtFecRespuesta.Text = Format(CorrespondenciaEditada.Fecha_respuesta, "yyyy-MM-dd")
        End If
        If CorrespondenciaEditada.id_original IsNot Nothing Then
            txtCorrespondencia.DataBind()
            If txtCorrespondencia.Items.Count > 1 Then
                txtCorrespondencia.SelectedValue = CorrespondenciaEditada.id_original
            End If
        End If
        If CorrespondenciaEditada.Respuesta IsNot Nothing Then
            TxtRespuesta.Text = CorrespondenciaEditada.Respuesta
        End If
        If CorrespondenciaEditada.folios IsNot Nothing Then
            TxtFolios.Text = CorrespondenciaEditada.folios
        End If
        If CorrespondenciaEditada.ciudad_remitente IsNot Nothing Then
            TxtRemitenteAdd.Text = CorrespondenciaEditada.ciudad_remitente
        End If
        If CorrespondenciaEditada.paquetes IsNot Nothing Then
            txtPaquetes.Text = CorrespondenciaEditada.paquetes
        End If
        Dim CDocumento = (From dsc In db.Documentos_Correspondencia.Include("Documentos")
                          Where dsc.id_correspondencia = CorrespondenciaEditada.id
                          Select dsc).ToList
        lstDocumentos.Items.Clear()
        For Each ds In CDocumento
            lstDocumentos.Items.Add(New ListItem With {.Text = ds.Documentos.alias, .Value = ds.id_documento})
        Next
        Dim trx As Terceros
        trx = (From tx In db.Terceros
               Where tx.id = CorrespondenciaEditada.id_responsable
               Select tx).FirstOrDefault
        If trx IsNot Nothing Then
            txtResponsable.Text = trx.nombre
            Hdd_id_responsable.Value = CorrespondenciaEditada.id_responsable
        End If
        If CorrespondenciaEditada.id_mensajeria IsNot Nothing Then
            trx = (From tx In db.Terceros
                   Where tx.id = CorrespondenciaEditada.id_mensajeria
                   Select tx).FirstOrDefault
            If trx IsNot Nothing Then
                TxtMensajeria.Text = trx.nombre
                Hdd_id_mensajeria.Value = CorrespondenciaEditada.id_mensajeria
            End If
        End If
        lstDestinatario.Items.Clear()
        lstRemitentes.Items.Clear()
        Dim Tcorrespondencia = (From trc In db.Terceros_Correspondencia.Include("Terceros")
                                Where trc.id_correspondencia = CorrespondenciaEditada.id
                                Select trc).ToList
        For Each tc In Tcorrespondencia
            If tc.Tipo = "Remitente" Then
                lstRemitentes.Items.Add(New ListItem With {.Text = tc.Terceros.nombre, .Value = tc.id_tercero})
            Else
                lstDestinatario.Items.Add(New ListItem With {.Text = tc.Terceros.nombre, .Value = tc.id_tercero})
            End If
        Next
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click


        Guardar("OK")



    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        If Mi = 1 Then
            Response.Redirect("../Correspondencia/Page_MiCorrespondencia.aspx")
        Else
            Response.Redirect("../Correspondencia/Page_CInterna.aspx")
        End If


    End Sub







    Protected Sub bttBuscarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarTercero.Click
        If txtDocumentoTercero.Text IsNot Nothing AndAlso txtDocumentoTercero.Text <> String.Empty Then

            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?interno=1&cedula=" & txtDocumentoTercero.Text & "','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)

        Else

            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?interno=1','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)

        End If

    End Sub

    Protected Sub bttAgregarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarTercero.Click
        Dim tr As Terceros = (From tc In db.Terceros
                              Where tc.id = Hdd_id_remitente.Value
                              Select tc).FirstOrDefault

        If tr IsNot Nothing Then
            lstRemitentes.Items.Add(New ListItem With {.Text = tr.nombre, .Value = tr.id})
        End If
        txtDocumentoTercero.Text = ""
        txtTercero.Text = ""
        Hdd_id_remitente.Value = 0
    End Sub

    Protected Sub bttEliminarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarTercero.Click
        Dim it As ListItem = lstRemitentes.SelectedItem

        CorrespondenciaEditada = (From dc In db.Correspondencia.Include("Terceros_Correspondencia").Include("Terceros_Correspondencia.Terceros")
                                  Where dc.id = id_correspondencia
                                  Select dc).FirstOrDefault
        If it IsNot Nothing Then
            If CorrespondenciaEditada IsNot Nothing Then
                Dim TerceroDocumento As Terceros_Correspondencia = (From dtr In CorrespondenciaEditada.Terceros_Correspondencia
                                                                    Where dtr.id_tercero = it.Value And dtr.Tipo = "Remitente"
                                                                    Select dtr).FirstOrDefault
                If TerceroDocumento IsNot Nothing Then
                    db.Terceros_Correspondencia.Remove(TerceroDocumento)
                    db.SaveChanges()
                    BoundDocumento(CorrespondenciaEditada)
                Else
                    lstRemitentes.Items.Remove(it)
                End If
            Else
                lstRemitentes.Items.Remove(it)
            End If
        End If
    End Sub

    Protected Sub BttDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles BttDocumento.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Documento.aspx','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
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
        CorrespondenciaEditada = (From dc In db.Correspondencia.Include("Documentos_Correpondencia")
                                  Where dc.id = id_correspondencia
                                  Select dc).FirstOrDefault

        If id_correspondencia <> -1 Then
            If it IsNot Nothing Then
                If CorrespondenciaEditada IsNot Nothing Then
                    Dim SolicitudDocumento As Documentos_Correspondencia = (From dsc In CorrespondenciaEditada.Documentos_Correspondencia
                                                                            Where dsc.id_documento = it.Value
                                                                            Select dsc).FirstOrDefault
                    If SolicitudDocumento IsNot Nothing Then
                        db.Documentos_Correspondencia.Remove(SolicitudDocumento)
                        db.SaveChanges()
                        BoundDocumento(CorrespondenciaEditada)
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

    Protected Sub BttResponsable_Click(sender As Object, e As ImageClickEventArgs) Handles BttResponsable.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?interno=1&id_responsable=1','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub ImageButton4_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton4.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?correspondencia=1&id_mensajeria=1','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub ImageButton1_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton1.Click
        If TxtDocDestinatario.Text IsNot Nothing AndAlso TxtDocDestinatario.Text <> String.Empty Then

            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?interno=1&id_destinatario=1&cedula=" & txtDocumentoTercero.Text & "','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)


        Else

            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?interno=1&id_destinatario=1','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)


        End If
    End Sub

    Protected Sub ImageButton2_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton2.Click
        Dim tr As Terceros = (From tc In db.Terceros
                              Where tc.id = Hdd_id_destinatario.Value
                              Select tc).FirstOrDefault

        If tr IsNot Nothing Then
            lstDestinatario.Items.Add(New ListItem With {.Text = tr.nombre, .Value = tr.id})
        End If
        TxtDocDestinatario.Text = ""
        TxtDestinatario.Text = ""
        Hdd_id_destinatario.Value = 0
    End Sub

    Protected Sub ImageButton3_Click(sender As Object, e As ImageClickEventArgs) Handles ImageButton3.Click
        Dim it As ListItem = lstDestinatario.SelectedItem

        CorrespondenciaEditada = (From dc In db.Correspondencia.Include("Terceros_Correspondencia").Include("Terceros_Correspondencia.Terceros")
                                  Where dc.id = id_correspondencia
                                  Select dc).FirstOrDefault
        If it IsNot Nothing Then
            If CorrespondenciaEditada IsNot Nothing Then
                Dim TerceroDocumento As Terceros_Correspondencia = (From dtr In CorrespondenciaEditada.Terceros_Correspondencia
                                                                    Where dtr.id_tercero = it.Value And dtr.Tipo = "Destinatario"
                                                                    Select dtr).FirstOrDefault
                If TerceroDocumento IsNot Nothing Then
                    db.Terceros_Correspondencia.Remove(TerceroDocumento)
                    db.SaveChanges()
                    BoundDocumento(CorrespondenciaEditada)
                Else
                    lstDestinatario.Items.Remove(it)
                End If
            Else
                lstDestinatario.Items.Remove(it)
            End If
        End If
    End Sub

    Private Sub Enviar_Correo()
        Dim correoDesde As String = ""
        Dim correoHasta As String = ""
        Dim subject As String
        Dim i As Integer
        Dim archivo As String
        Dim aliasx As String
        Dim lnk As String
        lnk = ConfigurationManager.AppSettings("URLBase").ToString
        correoDesde = "info@sistavtecnologia.com"
        Dim trx = (From tx In db.Terceros
                   Where tx.id = Hdd_id_responsable.Value
                   Select tx).FirstOrDefault
        If trx IsNot Nothing Then
            correoHasta = trx.correo_electronico
            Dim bodyPlain As String
            Dim bodyHtml As String
            If id_correspondencia <> -1 Then
                subject = "Actualización Correspondencia - " & txtResponsable.Text
                bodyPlain = subject & Environment.NewLine & Environment.NewLine &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                    "Se generó una actualización  de la correspondencia Nro de radicado:" & TxtRadicado.Text & Environment.NewLine &
                    "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Cordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                    "Soporte Archigex" & Environment.NewLine & "info@sistavtecnologia.com" & Environment.NewLine

                i = 1
                For Each it As ListItem In lstDocumentos.Items
                    Dim dcx = (From tx In db.Documentos
                               Where tx.id = it.Value
                               Select tx).FirstOrDefault
                    archivo = ""
                    aliasx = ""
                    If dcx IsNot Nothing Then
                        archivo = dcx.nombre
                        aliasx = dcx.alias

                    End If
                    bodyPlain = bodyPlain & "Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a>" & Environment.NewLine
                Next
                bodyHtml = " < html <> body <> p > " & subject & "</p><br /><br /><p>" &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                    "<p>Se generó una actualización  de la correspondencia Nro de radicado" & TxtRadicado.Text & "<br />" &
                    "Agradecemos su atención.<br /><br />Cordial saludo.<br />ARCHIGEX<br /><br />" &
                            "<b><font color=#336699><a href='http://www.greentechsupply.co/DAPP/'>ARCHIGEX</a></font></b><br />" &
                            "<a href='http://www.greentechsupply.co/DAPP/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/dapp/dist/img/logo_dapp.png'" &
                            " /></a>" &
                            "<img src='http://www.greentechsupply.co/DAPP/logo_dapp_thumb'>"

                i = 1
                For Each it As ListItem In lstDocumentos.Items
                    Dim dcx = (From tx In db.Documentos
                               Where tx.id = it.Value
                               Select tx).FirstOrDefault
                    archivo = ""
                    aliasx = ""
                    If dcx IsNot Nothing Then
                        archivo = dcx.nombre
                        aliasx = dcx.alias

                    End If
                    bodyHtml = bodyHtml & "<p>Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a><p>"

                Next
            Else
                subject = "Nueva Correspondencia - " & txtResponsable.Text
                bodyPlain = subject & Environment.NewLine & Environment.NewLine &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                    "Se generó una nueva correspondencia, lo invitamos a que consulte el sistema para realizar el seguimiento respectivo " & Environment.NewLine &
                    "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Coordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                    "Soporte Archigex" & Environment.NewLine & "info@sistavtecnologia.com" & Environment.NewLine

                i = 1
                For Each it As ListItem In lstDocumentos.Items
                    Dim dcx = (From tx In db.Documentos
                               Where tx.id = it.Value
                               Select tx).FirstOrDefault
                    archivo = ""
                    aliasx = ""
                    If dcx IsNot Nothing Then
                        archivo = dcx.nombre
                        aliasx = dcx.alias

                    End If
                    bodyPlain = bodyPlain & "Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a>" & Environment.NewLine
                Next
                bodyHtml = "<html><body><p>" & subject & "</p><br /><br /><p>" &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                    "<p>Se generó una nueva correspondencia, lo invitamos a que consulte el sistema para realizar el seguimiento respectivo <br />" &
                    "Agradecemos su atención.<br /><br />Coordial saludo.<br />ARCHIGEX<br /><br />" &
                            "<b><font color=#336699><a href='http://www.greentechsupply.co/DAPP/'>ARCHIGEX</a></font></b><br />" &
                            "<a href='http://www.greentechsupply.co/DAPP/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/dapp/dist/img/logo_dapp.png'" &
                            " /></a>" &
                            "<img src='http://www.greentechsupply.co/DAPP/logo_dapp_thumb'>"

                i = 1
                For Each it As ListItem In lstDocumentos.Items
                    Dim dcx = (From tx In db.Documentos
                               Where tx.id = it.Value
                               Select tx).FirstOrDefault
                    archivo = ""
                    aliasx = ""
                    If dcx IsNot Nothing Then
                        archivo = dcx.nombre
                        aliasx = dcx.alias

                    End If
                    bodyHtml = bodyHtml & "<p>Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a><p>"

                Next
                bodyHtml = bodyHtml & "</body></html>"
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
    Private Sub Enviar_Correodest(id_corresp As Long)
        Dim correoDesde As String = ""
        Dim correoHasta As String = ""
        Dim subject As String
        Dim i As Integer
        Dim archivo As String
        Dim aliasx As String
        Dim lnk As String
        lnk = ConfigurationManager.AppSettings("URLBase").ToString
        correoDesde = "info@sistavtecnologia.com"
        Dim trx = (From tx In db.Terceros.Include("Terceros_Correspondencia")
                   Where tx.id = Hdd_id_responsable.Value
                   Select tx).FirstOrDefault

        If trx IsNot Nothing Then
            If trx.correo_electronico IsNot Nothing And trx.correo_electronico <> "" Then
                correoDesde = trx.correo_electronico
            Else
                correoDesde = "info@sistavtecnologia.com"
            End If

            Dim trz = (From tx In db.Terceros_Correspondencia
                       Where tx.id_correspondencia = id_corresp And tx.Tipo = "Destinatario"
                       Select tx).ToList

            For Each tyy In trz
                Dim destx = (From tx In db.Terceros
                             Where tx.id = tyy.id_tercero
                             Select tx).FirstOrDefault
                If destx IsNot Nothing Then
                    correoHasta = destx.correo_electronico
                    Dim bodyPlain As String
                    Dim bodyHtml As String
                    If id_correspondencia <> -1 Then
                        subject = "Actualización Correspondencia - " & txtResponsable.Text
                        bodyPlain = subject & Environment.NewLine & Environment.NewLine &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                    "Se generó una actualización  de la correspondencia Nro de radicado:" & TxtRadicado.Text & Environment.NewLine &
                    "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Coordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                    "Soporte Archigex" & Environment.NewLine & "info@sistavtecnologia.com" & Environment.NewLine
                        i = 1
                        For Each it As ListItem In lstDocumentos.Items
                            Dim dcx = (From tx In db.Documentos
                                       Where tx.id = it.Value
                                       Select tx).FirstOrDefault
                            archivo = ""
                            aliasx = ""
                            If dcx IsNot Nothing Then
                                archivo = dcx.nombre
                                aliasx = dcx.alias

                            End If
                            bodyPlain = bodyPlain & "Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a>" & Environment.NewLine
                        Next
                        bodyHtml = " < html <> body <> p > " & subject & "</p><br /><br /><p>" &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                    "<p>Se generó una actualización  de la correspondencia Nro de radicado" & TxtRadicado.Text & "<br />" &
                    "Agradecemos su atención.<br /><br />Cordial saludo.<br />ARCHIGEX<br /><br />" &
                            "<b><font color=#336699><a href='http://www.greentechsupply.co/DAPP/'>ARCHIGEX</a></font></b><br />" &
                            "<a href='http://www.greentechsupply.co/DAPP/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/dapp/dist/img/logo_dapp.png'" &
                            " /></a>" &
                            "<img src='http://www.greentechsupply.co/DAPP/logo_dapp_thumb'>"

                        i = 1
                        For Each it As ListItem In lstDocumentos.Items
                            Dim dcx = (From tx In db.Documentos
                                       Where tx.id = it.Value
                                       Select tx).FirstOrDefault
                            archivo = ""
                            aliasx = ""
                            If dcx IsNot Nothing Then
                                archivo = dcx.nombre
                                aliasx = dcx.alias

                            End If
                            bodyHtml = bodyHtml & "<p>Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a><p>"

                        Next
                        bodyHtml = bodyHtml & "</body></html>"
                    Else
                        subject = "Nueva Correspondencia - " & txtResponsable.Text
                        bodyPlain = subject & Environment.NewLine & Environment.NewLine &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:" & Environment.NewLine &
                    "Se generó una nueva correspondencia, lo invitamos a que consulte el sistema para realizar el seguimiento respectivo " & Environment.NewLine &
                    "Agradecemos su atención." & Environment.NewLine & Environment.NewLine & "Coordial saludo." & Environment.NewLine & "WS." & Environment.NewLine &
                    "Soporte Archigex" & Environment.NewLine & "info@sistavtecnologia.com" & Environment.NewLine

                        i = 1
                        For Each it As ListItem In lstDocumentos.Items
                            Dim dcx = (From tx In db.Documentos
                                       Where tx.id = it.Value
                                       Select tx).FirstOrDefault
                            archivo = ""
                            aliasx = ""
                            If dcx IsNot Nothing Then
                                archivo = dcx.nombre
                                aliasx = dcx.alias

                            End If
                            bodyPlain = bodyPlain & "Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a>" & Environment.NewLine
                        Next
                        bodyHtml = "<html><body><p>" & subject & "</p><br /><br /><p>" &
                    "Estimado Usuario, Hemos registrado una transacción de acuerdo al Asunto de este correo:</p><br />" &
                    "<p>Se generó una nueva correspondencia, lo invitamos a que consulte el sistema para realizar el seguimiento respectivo <br />" &
                    "Agradecemos su atención.<br /><br />Coordial saludo.<br />ARCHIGEX<br /><br />" &
                            "<b><font color=#336699><a href='http://www.greentechsupply.co/DAPP/'>ARCHIGEX</a></font></b><br />" &
                            "<a href='http://www.greentechsupply.co/DAPP/' target='_blank'><img style='border:none;' src='http://www.greentechsupply.co/dapp/dist/img/logo_dapp.png'" &
                            " /></a>" &
                            "<img src='http://www.greentechsupply.co/DAPP/logo_dapp_thumb'>"

                        i = 1
                        For Each it As ListItem In lstDocumentos.Items
                            Dim dcx = (From tx In db.Documentos
                                       Where tx.id = it.Value
                                       Select tx).FirstOrDefault
                            archivo = ""
                            aliasx = ""
                            If dcx IsNot Nothing Then
                                archivo = dcx.nombre
                                aliasx = dcx.alias

                            End If
                            bodyHtml = bodyHtml & "<p>Se referencio El Siguiente Documento " & "<a href='" & lnk & "Archivos/" & archivo & "'>" & aliasx & "</a><p>"

                        Next
                        bodyHtml = bodyHtml & "</body></html>"
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

            Next
        End If



    End Sub
    Protected Sub BttDetalle_Click(sender As Object, e As EventArgs) Handles BttDetalle.Click
        If lstDocumentos.Items.Count > 0 Then
            If id_correspondencia <> -1 Then
                Utilidades.RegistrarScript("window.open('../Detalle_Documento.aspx?id_correspondencia=" & id_correspondencia & "','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            Else
                Utilidades.Mensaje("Tiene que grabar para ver los documentos", Me)
            End If

        Else
            Utilidades.Mensaje("No hay documentos disponibles", Me)
        End If
    End Sub

    Protected Sub BtComentarios_Click(sender As Object, e As EventArgs) Handles BtComentarios.Click
        If id_correspondencia <> -1 Then
            Utilidades.RegistrarScript("window.open('../Comentarios.aspx?id_correspondencia=" & id_correspondencia & "','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
        Else
            Utilidades.Mensaje("Tiene que grabar para ver los comentarios", Me)
        End If
    End Sub

    Protected Sub BttStiker_Click(sender As Object, e As EventArgs) Handles BttStiker.Click
        If id_correspondencia <> -1 Then
            If txtSticker.SelectedValue = "Pequeño" Then
                Utilidades.RegistrarScript("window.open('../Correspondencia/ReporteSticker.aspx?id_correspondencia=" & id_correspondencia & "','Sticker','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            Else
                Utilidades.RegistrarScript("window.open('../Correspondencia/ReporteStickerBG.aspx?id_correspondencia=" & id_correspondencia & "','Sticker','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If


        Else
            Utilidades.Mensaje("Se procede a guardar primero la correspondencia", Me)
            Guardar("Sticker")
        End If
    End Sub
    Protected Sub Guardar(orig As String)
        Dim ids As Long
        Dim id_cliente As Long

        ids = CLng(Session("id_sucursal"))
        id_cliente = CLng(Session("id_cliente"))
        If Hdd_id_responsable.Value = 0 Then
            Utilidades.Mensaje("Debe Asociar un Responsable", Me)
        ElseIf txtTipoEnvio.SelectedValue = "Courrier Mensajería" And Hdd_id_mensajeria.Value = 0 Then
            Utilidades.Mensaje("Debe Asociar una empresa de Mensajeria", Me)
        ElseIf txtTipoEnvio.SelectedValue = "Courrier Mensajería" And txtGuia.Text = "" Then
            Utilidades.Mensaje("Debe Asociar una guía de la empresa de Mensajeria", Me)
        Else
            If id_correspondencia = -1 Then
                CorrespondenciaEditada = New Correspondencia
                CorrespondenciaEditada.usuario = User.Identity.Name
                CorrespondenciaEditada.id_sucursal = ids
                CorrespondenciaEditada.fecha_sistema = Now()
                Prefijo()
                db.Correspondencia.Add(CorrespondenciaEditada)
            Else
                CorrespondenciaEditada = (From dc In db.Correspondencia
                                          Where dc.id = id_correspondencia
                                          Select dc).FirstOrDefault
            End If

            If CorrespondenciaEditada IsNot Nothing Then
                CorrespondenciaEditada.id_tipodoc = txtTipoDocumento.SelectedValue
                CorrespondenciaEditada.tipo_correspondencia = txttipoc.SelectedValue
                CorrespondenciaEditada.fecha_documento = TxtfechaC.Text
                CorrespondenciaEditada.asunto = TxtAsunto.Text
                CorrespondenciaEditada.observaciones = txtDescripcion.Text
                CorrespondenciaEditada.Estado = txtestado.SelectedValue
                CorrespondenciaEditada.remitente = txtRemitente.Text
                CorrespondenciaEditada.dependencia_remitente = txtDest.Text
                CorrespondenciaEditada.guia = txtGuia.Text
                CorrespondenciaEditada.tipo_envio = txtTipoEnvio.SelectedValue
                CorrespondenciaEditada.id_responsable = Hdd_id_responsable.Value
                CorrespondenciaEditada.folios = TxtFolios.Text
                CorrespondenciaEditada.paquetes = txtPaquetes.Text
                CorrespondenciaEditada.ciudad_remitente = TxtRemitenteAdd.Text
                If Hdd_id_mensajeria.Value <> 0 Then
                    CorrespondenciaEditada.id_mensajeria = Hdd_id_mensajeria.Value
                End If
                If TxtFecRespuesta.Text <> "" Then
                    CorrespondenciaEditada.Estado = "Recibida"
                    CorrespondenciaEditada.Fecha_respuesta = TxtFecRespuesta.Text
                End If
                CorrespondenciaEditada.Respuesta = TxtRespuesta.Text
                If TxtRespuesta.Text <> "" Then
                    CorrespondenciaEditada.Estado = "Atendida"
                End If
                If txtCorrespondencia.SelectedValue <> 0 Then
                    CorrespondenciaEditada.id_original = txtCorrespondencia.SelectedValue
                End If
                If id_correspondencia = -1 Then
                    Dim consx As AdminConsecutivo = (From dcx In db.AdminConsecutivo
                                                     Where dcx.Anio = Year(Now) And dcx.id_cliente = id_cliente And dcx.Tipo = "INTERNO"
                                                     Select dcx).FirstOrDefault
                    If consx IsNot Nothing Then
                        CorrespondenciaEditada.numero_radicado = TxtRadicado.Text & "-" & Year(Now) & "-" & consx.Consecutivo
                        TxtRadicado.Text = TxtRadicado.Text & " - " & Year(Now) & "-" & consx.Consecutivo
                        consx.Consecutivo = consx.Consecutivo + 1
                        db.SaveChanges()
                    Else
                        TxtRadicado.Text = TxtRadicado.Text & "-" & Year(Now) & "-" & CorrespondenciaEditada.id
                        CorrespondenciaEditada.numero_radicado = TxtRadicado.Text & "-" & Year(Now) & "-" & CorrespondenciaEditada.id
                        db.SaveChanges()
                    End If
                End If
            End If
            db.SaveChanges()

            For Each it As ListItem In lstDocumentos.Items
                Dim SolicitudDocumento = (From dsc In db.Documentos_Correspondencia.Include("Documentos")
                                          Where dsc.id_correspondencia = CorrespondenciaEditada.id And dsc.id_documento = it.Value
                                          Select dsc).FirstOrDefault
                If SolicitudDocumento Is Nothing Then
                    Dim ar As New Documentos_Correspondencia
                    ar.id_documento = it.Value
                    ar.id_correspondencia = CorrespondenciaEditada.id
                    db.Documentos_Correspondencia.Add(ar)
                    db.SaveChanges()
                End If
            Next
            For Each it As ListItem In lstDestinatario.Items
                Dim Tdest = (From trc In db.Terceros_Correspondencia
                             Where trc.id_correspondencia = CorrespondenciaEditada.id And trc.id_tercero = it.Value And trc.Tipo = "Destinatario"
                             Select trc).FirstOrDefault
                If Tdest Is Nothing Then
                    Dim ar As New Terceros_Correspondencia
                    ar.id_tercero = it.Value
                    ar.id_correspondencia = CorrespondenciaEditada.id
                    ar.Tipo = "Destinatario"
                    db.Terceros_Correspondencia.Add(ar)
                    db.SaveChanges()
                End If
            Next
            For Each it As ListItem In lstRemitentes.Items
                Dim Tdest = (From trc In db.Terceros_Correspondencia
                             Where trc.id_correspondencia = CorrespondenciaEditada.id And trc.id_tercero = it.Value And trc.Tipo = "Remitente"
                             Select trc).FirstOrDefault
                If Tdest Is Nothing Then
                    Dim ar As New Terceros_Correspondencia
                    ar.id_tercero = it.Value
                    ar.id_correspondencia = CorrespondenciaEditada.id
                    ar.Tipo = "Remitente"
                    db.Terceros_Correspondencia.Add(ar)
                    db.SaveChanges()
                End If
            Next

            If Session("id_sucursal") IsNot Nothing Then
                ids = Session("id_sucursal")
            Else
                ids = 0
            End If
            If id_correspondencia = -1 Then
                Utilidades.RegistrarAuditoria("Correspondencia", CorrespondenciaEditada.id, CorrespondenciaEditada.numero_radicado & " - " & Format(CorrespondenciaEditada.fecha_documento, "dd/MM/yyyy"), "Nuevo Registro", User.Identity.Name, ids)
            Else
                Utilidades.RegistrarAuditoria("Correspondencia", CorrespondenciaEditada.id, CorrespondenciaEditada.numero_radicado & " - " & Format(CorrespondenciaEditada.fecha_documento, "dd/MM/yyyy"), "Actualización Registro", User.Identity.Name, ids)
            End If
            Utilidades.Mensaje("Correspondencia Guardada", Me)

            Enviar_Correo()
            Enviar_Correodest(CorrespondenciaEditada.id)
            If orig = "OK" Then
                If Mi = 1 Then
                    Response.Redirect("../Correspondencia/Page_MiCorrespondencia.aspx")
                Else
                    Response.Redirect("../Correspondencia/Page_CInterna.aspx")
                End If
            Else
                Response.Redirect("../Correspondencia/Editar_CInterna.aspx?id_correspondencia=" & CorrespondenciaEditada.id)
            End If

        End If
    End Sub

    Protected Sub txttipoc_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txttipoc.SelectedIndexChanged
        Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
        Dim idt As Long
        If txttipoc.SelectedValue = "Interna Enviada" Then


            lblRem.Text = "Remitente"
            If id_correspondencia = -1 Then

                If Session("id_cliente") IsNot Nothing Then
                    Dim idc As Long
                    idc = Session("id_cliente")
                    Dim pref = (From sc In db.Clientes
                                Where sc.id = idc
                                Select sc).FirstOrDefault
                    If pref IsNot Nothing Then
                        TxtRadicado.Text = pref.Prefijo
                    End If
                End If
                If Usuario.id_tercero IsNot Nothing Then
                    If Usuario.correspondencia = False Then
                        Hdd_id_area.Value = 0
                        Txtarea.Text = ""
                        Hdd_id_responsable.Value = 0
                        txtResponsable.Text = ""
                        lstDestinatario.Items.Clear()
                    Else

                        lstDestinatario.Items.Clear()
                        idt = Usuario.id_tercero
                        Dim arx = (From ax In db.Terceros.Include("Areas")
                                   Where ax.id = idt
                                   Select ax).FirstOrDefault
                        If arx IsNot Nothing Then
                            If arx.id_area IsNot Nothing Then
                                TxtRadicado.Text = TxtRadicado.Text & arx.Areas.Prefijo
                                Hdd_id_area.Value = arx.id_area
                                Txtarea.Text = arx.Areas.nombre
                            Else
                                TxtRadicado.Text = TxtRadicado.Text
                                Hdd_id_area.Value = 0
                                Txtarea.Text = ""
                            End If
                            lstDestinatario.Items.Add(New ListItem With {.Text = arx.nombre, .Value = arx.id})
                            Hdd_id_responsable.Value = arx.id
                            txtResponsable.Text = arx.nombre
                        End If
                    End If
                End If
            End If
        Else
            lblRem.Text = "Destinatario"
            If id_correspondencia = -1 Then


                If Usuario.id_tercero IsNot Nothing Then
                    If Usuario.correspondencia = False Then
                        Hdd_id_area.Value = 0
                        Txtarea.Text = ""
                        Hdd_id_responsable.Value = 0
                        txtResponsable.Text = ""
                        lstRemitentes.Items.Clear()
                    Else

                        lstRemitentes.Items.Clear()
                        idt = Usuario.id_tercero
                        Dim arx = (From ax In db.Terceros.Include("Areas")
                                   Where ax.id = idt
                                   Select ax).FirstOrDefault
                        If arx IsNot Nothing Then
                            If arx.id_area IsNot Nothing Then
                                TxtRadicado.Text = TxtRadicado.Text & arx.Areas.Prefijo
                                Hdd_id_area.Value = arx.id_area
                                Txtarea.Text = arx.Areas.nombre
                            Else
                                TxtRadicado.Text = TxtRadicado.Text
                                Hdd_id_area.Value = 0
                                Txtarea.Text = ""
                            End If
                            lstRemitentes.Items.Add(New ListItem With {.Text = arx.nombre, .Value = arx.id})
                            Hdd_id_responsable.Value = arx.id
                            txtResponsable.Text = arx.nombre
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub txtDocumentoTercero_TextChanged(sender As Object, e As EventArgs) Handles txtDocumentoTercero.TextChanged
        Dim idc As Long
        idc = CLng(Session("id_cliente"))
        If txtDocumentoTercero.Text <> "" Then
            Dim Tercero As Terceros = (From tr In db.Terceros
                                       Where tr.numero_documento = CLng(txtDocumentoTercero.Text) And tr.id_cliente = idc
                                       Select tr).FirstOrDefault
            If Tercero IsNot Nothing Then
                txtTercero.Text = Tercero.nombre
                Hdd_id_remitente.Value = Tercero.id

            Else
                Utilidades.Mensaje("Documento No encontrado", Me)
            End If
        End If
    End Sub

    Protected Sub txtTercero_TextChanged(sender As Object, e As EventArgs) Handles txtTercero.TextChanged
        Dim idc As Long
        idc = CLng(Session("id_cliente"))
        If txtTercero.Text <> "" Then
            Dim Tercero As Terceros = (From tr In db.Terceros
                                       Where tr.nombre = txtTercero.Text And tr.id_cliente = idc
                                       Select tr).FirstOrDefault
            If Tercero IsNot Nothing Then
                txtDocumentoTercero.Text = Tercero.numero_documento
                Hdd_id_remitente.Value = Tercero.id
            Else
                Utilidades.Mensaje("Nombre No encontrado", Me)
            End If
        End If
    End Sub

    Protected Sub TxtDocDestinatario_TextChanged(sender As Object, e As EventArgs) Handles TxtDocDestinatario.TextChanged
        Dim idc As Long
        idc = CLng(Session("id_cliente"))
        If txtDocumentoTercero.Text <> "" Then
            Dim Tercero As Terceros = (From tr In db.Terceros
                                       Where tr.numero_documento = CLng(TxtDocDestinatario.Text) And tr.id_cliente = idc
                                       Select tr).FirstOrDefault
            If Tercero IsNot Nothing Then
                TxtDestinatario.Text = Tercero.nombre
                Hdd_id_destinatario.Value = Tercero.id

            Else
                Utilidades.Mensaje("Documento No encontrado", Me)
            End If
        End If
    End Sub

    Protected Sub TxtDestinatario_TextChanged(sender As Object, e As EventArgs) Handles TxtDestinatario.TextChanged
        Dim idc As Long
        idc = CLng(Session("id_cliente"))
        If txtTercero.Text <> "" Then
            Dim Tercero As Terceros = (From tr In db.Terceros
                                       Where tr.nombre = TxtDestinatario.Text And tr.id_cliente = idc
                                       Select tr).FirstOrDefault
            If Tercero IsNot Nothing Then
                TxtDocDestinatario.Text = Tercero.numero_documento
                Hdd_id_destinatario.Value = Tercero.id
            Else
                Utilidades.Mensaje("Nombre No encontrado", Me)
            End If
        End If
    End Sub
    Protected Sub Prefijo()
        Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
        Dim idt As Long
        If txttipoc.SelectedValue = "Interna Enviada" Then


            lblRem.Text = "Remitente"
            If id_correspondencia = -1 Then

                If Session("id_cliente") IsNot Nothing Then
                    Dim idc As Long
                    idc = Session("id_cliente")
                    Dim pref = (From sc In db.Clientes
                                Where sc.id = idc
                                Select sc).FirstOrDefault
                    If pref IsNot Nothing Then
                        TxtRadicado.Text = pref.Prefijo
                    End If
                End If
                If Usuario.id_tercero IsNot Nothing Then
                    If Usuario.correspondencia = False Then
                        Hdd_id_area.Value = 0
                        Txtarea.Text = ""
                        Hdd_id_responsable.Value = 0
                        txtResponsable.Text = ""
                        lstDestinatario.Items.Clear()
                    Else

                        lstDestinatario.Items.Clear()
                        idt = Usuario.id_tercero
                        Dim arx = (From ax In db.Terceros.Include("Areas")
                                   Where ax.id = idt
                                   Select ax).FirstOrDefault
                        If arx IsNot Nothing Then
                            If arx.id_area IsNot Nothing Then
                                TxtRadicado.Text = TxtRadicado.Text & arx.Areas.Prefijo
                                Hdd_id_area.Value = arx.id_area
                                Txtarea.Text = arx.Areas.nombre
                            Else
                                TxtRadicado.Text = TxtRadicado.Text
                                Hdd_id_area.Value = 0
                                Txtarea.Text = ""
                            End If
                            lstDestinatario.Items.Add(New ListItem With {.Text = arx.nombre, .Value = arx.id})
                            Hdd_id_responsable.Value = arx.id
                            txtResponsable.Text = arx.nombre
                        End If
                    End If
                End If
            End If
        Else
            lblRem.Text = "Destinatario"
            If id_correspondencia = -1 Then


                If Usuario.id_tercero IsNot Nothing Then
                    If Usuario.correspondencia = False Then
                        Hdd_id_area.Value = 0
                        Txtarea.Text = ""
                        Hdd_id_responsable.Value = 0
                        txtResponsable.Text = ""
                        lstRemitentes.Items.Clear()
                    Else

                        lstRemitentes.Items.Clear()
                        idt = Usuario.id_tercero
                        Dim arx = (From ax In db.Terceros.Include("Areas")
                                   Where ax.id = idt
                                   Select ax).FirstOrDefault
                        If arx IsNot Nothing Then
                            If arx.id_area IsNot Nothing Then
                                TxtRadicado.Text = TxtRadicado.Text & arx.Areas.Prefijo
                                Hdd_id_area.Value = arx.id_area
                                Txtarea.Text = arx.Areas.nombre
                            Else
                                TxtRadicado.Text = TxtRadicado.Text
                                Hdd_id_area.Value = 0
                                Txtarea.Text = ""
                            End If
                            lstRemitentes.Items.Add(New ListItem With {.Text = arx.nombre, .Value = arx.id})
                            Hdd_id_responsable.Value = arx.id
                            txtResponsable.Text = arx.nombre
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub BttScan_Click(sender As Object, e As ImageClickEventArgs) Handles BttScan.Click
        Utilidades.RegistrarScript("window.open('../GesDoc/ScannearDocumento.aspx?id_documento=-1','EscanearDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub BttArea_Click(sender As Object, e As ImageClickEventArgs) Handles BttArea.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Area.aspx','SeleccionarArea','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub TxtFecRespuesta_TextChanged(sender As Object, e As EventArgs) Handles TxtFecRespuesta.TextChanged
        If CDate(TxtFecRespuesta.Text) < CDate(TxtfechaC.Text) Then
            Utilidades.Mensaje("Fecha de Respuesta, no puede ser menor a la fecha de Correspondencia", Me)
        End If
    End Sub
End Class