Imports System.IO
Public Class EditarCorrespondencia
    Inherits System.Web.UI.Page


    Private CorrespondenciaEditada As Correspondencia
    Private db As New DAPP_BDEntities
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

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_correspondencia <> -1 Then

                CorrespondenciaEditada = (From crr In db.Correspondencia.Include("Documentos_Correspondencia").Include("Terceros_Correspondencia")
                                          Where crr.id = id_correspondencia
                                          Select crr).FirstOrDefault

                If CorrespondenciaEditada IsNot Nothing Then
                    BoundCorrespondencia(CorrespondenciaEditada)
                End If
            End If
        End If
    End Sub

    Private Sub BoundCorrespondencia(CorrespondenciaEditada As Correspondencia)

        txtAsunto.Text = CorrespondenciaEditada.asunto

        Dim ciudad As Admin_Ciudades = (From cd In db.Admin_Ciudades
                                        Where cd.ciudad = CorrespondenciaEditada.ciudad_remitente
                                        Select cd).FirstOrDefault
        If ciudad IsNot Nothing Then
            txtDepartamento.DataBind()
            txtDepartamento.SelectedValue = ciudad.departamento
            txtCiudad.DataBind()
            txtCiudad.SelectedValue = ciudad.ciudad
        End If

        txtDependenciaRemitente.Text = CorrespondenciaEditada.dependencia_remitente
        txtFechaDocumento.Value = CorrespondenciaEditada.fecha_documento
        txtGuia.Text = CorrespondenciaEditada.guia
        txtNumeroRadicado.Text = CorrespondenciaEditada.numero_radicado
        txtObservaciones.Text = CorrespondenciaEditada.observaciones
        txtRemitente.Text = CorrespondenciaEditada.remitente
        txtSticker.Text = CorrespondenciaEditada.sticker
        txtTipoCorrespondencia.SelectedValue = CorrespondenciaEditada.tipo_correspondencia
        txtTipoDocumento.SelectedValue = CorrespondenciaEditada.tipo_envio
        txtTipoIngreso.SelectedValue = CorrespondenciaEditada.tipo_ingreso

        lstTercerosDocumento.Items.Clear()
        For Each tr_cr In CorrespondenciaEditada.Terceros_Correspondencia
            lstTercerosDocumento.Items.Add(New ListItem With {.Text = tr_cr.Terceros.nombre, .Value = tr_cr.id_tercero})
        Next


        lstDocumentos.Items.Clear()
        For Each dc_cr In CorrespondenciaEditada.Documentos_Correspondencia
            lstDocumentos.Items.Add(New ListItem With {.Text = dc_cr.Documentos.nombre, .Value = dc_cr.id_documento})
        Next

    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        Dim id_sucursal As Long = CLng(Session("id_sucursal"))
        Dim id_cliente As Long = CLng(Session("id_cliente"))
        If id_correspondencia = -1 Then
            CorrespondenciaEditada = New Correspondencia
            CorrespondenciaEditada.id_sucursal = Session("id_sucursal")
            db.Correspondencia.Add(CorrespondenciaEditada)
        Else
            CorrespondenciaEditada = (From crr In db.Correspondencia.Include("Documentos_Correspondencia").Include("Terceros_Correspondencia")
                                      Where crr.id = id_correspondencia
                                      Select crr).FirstOrDefault
        End If


        If CorrespondenciaEditada IsNot Nothing Then

            CorrespondenciaEditada.fecha_sistema = Date.Now
            CorrespondenciaEditada.asunto = txtAsunto.Text
            CorrespondenciaEditada.ciudad_remitente = txtCiudad.SelectedValue
            CorrespondenciaEditada.dependencia_remitente = txtDependenciaRemitente.Text
            CorrespondenciaEditada.fecha_documento = txtFechaDocumento.Value
            CorrespondenciaEditada.guia = txtGuia.Text
            CorrespondenciaEditada.observaciones = txtObservaciones.Text
            CorrespondenciaEditada.remitente = txtRemitente.Text
            CorrespondenciaEditada.sticker = txtSticker.Text
            CorrespondenciaEditada.tipo_correspondencia = txtTipoCorrespondencia.SelectedValue
            CorrespondenciaEditada.tipo_envio = txtTipoDocumento.SelectedValue
            CorrespondenciaEditada.tipo_ingreso = txtTipoIngreso.SelectedValue

            For Each it As ListItem In lstTercerosDocumento.Items
                Dim TerceroCorrespondencia As Terceros_Correspondencia = (From ctr In db.Terceros_Correspondencia
                                                                          Where ctr.id_tercero = it.Value And ctr.id_correspondencia = CorrespondenciaEditada.id
                                                                          Select ctr).FirstOrDefault
                If TerceroCorrespondencia Is Nothing Then
                    Dim tr As New Terceros_Correspondencia
                    tr.id_tercero = it.Value
                    tr.Correspondencia = CorrespondenciaEditada
                    tr.id_correspondencia = CorrespondenciaEditada.id
                    db.Terceros_Correspondencia.Add(tr)
                End If
            Next

            For Each it As ListItem In lstDocumentos.Items
                Dim DocumentoCorrespondencia As Documentos_Correspondencia = (From dc In db.Documentos_Correspondencia
                                                                              Where dc.id_correspondencia = it.Value And dc.id_correspondencia = CorrespondenciaEditada.id
                                                                              Select dc).FirstOrDefault
                If DocumentoCorrespondencia Is Nothing Then
                    Dim dcr As New Documentos_Correspondencia
                    dcr.id_documento = it.Value
                    dcr.Correspondencia = CorrespondenciaEditada
                    dcr.id_correspondencia = CorrespondenciaEditada.id
                    db.Documentos_Correspondencia.Add(dcr)
                End If
            Next

            db.SaveChanges()
            Dim consx As AdminConsecutivo = (From dcx In db.AdminConsecutivo
                                             Where dcx.Anio = Year(Now) And dcx.id_cliente = id_cliente
                                             Select dcx).FirstOrDefault
            If consx IsNot Nothing Then
                CorrespondenciaEditada.numero_radicado = CorrespondenciaEditada.tipo_correspondencia.Substring(0, 2) & " - " & Year(Now) & " - " & consx.Consecutivo
                consx.Consecutivo = consx.Consecutivo + 1
                db.SaveChanges()
            Else
                CorrespondenciaEditada.numero_radicado = CorrespondenciaEditada.tipo_correspondencia.Substring(0, 2) & " - " & Year(Now) & " - " & CorrespondenciaEditada.id
                db.SaveChanges()
            End If

            Utilidades.Mensaje("Correspondencia Guardada", Me)
            Response.Redirect("../Correspondencia/Page_Correspondencia.aspx")

        End If

    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Correspondencia/Page_Correspondencia.aspx")
    End Sub

    Protected Sub bttBuscarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarTercero.Click
        If txtDocumentoTercero.Text IsNot Nothing AndAlso txtDocumentoTercero.Text <> String.Empty Then
            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?cedula=" & txtDocumentoTercero.Text & "','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
        Else
            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
        End If

    End Sub

    Protected Sub bttAgregarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarTercero.Click
        Dim tr As Terceros = (From tc In db.Terceros
                              Where tc.id = Hdd_id_tercero.Value
                              Select tc).FirstOrDefault

        If tr IsNot Nothing Then
            lstTercerosDocumento.Items.Add(New ListItem With {.Text = tr.nombre, .Value = tr.id})
        End If
    End Sub

    Protected Sub bttEliminarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarTercero.Click
        Dim it As ListItem = lstTercerosDocumento.SelectedItem

        CorrespondenciaEditada = (From crr In db.Correspondencia.Include("Documentos_Correspondencia").Include("Terceros_Correspondencia")
                                  Where crr.id = id_correspondencia
                                  Select crr).FirstOrDefault

        If it IsNot Nothing Then
            If CorrespondenciaEditada IsNot Nothing Then
                Dim TerceroCorrespondencia As Terceros_Correspondencia = (From trc In CorrespondenciaEditada.Terceros_Correspondencia
                                                                          Where trc.id_tercero = it.Value
                                                                          Select trc).FirstOrDefault
                If TerceroCorrespondencia IsNot Nothing Then
                    db.Terceros_Correspondencia.Remove(TerceroCorrespondencia)
                    db.SaveChanges()
                    BoundCorrespondencia(CorrespondenciaEditada)
                Else
                    lstTercerosDocumento.Items.Remove(it)
                End If
            Else
                lstTercerosDocumento.Items.Remove(it)
            End If
        End If
    End Sub

    Protected Sub bttBuscarDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarDocumento.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Documento.aspx','SeleccionarDocumento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttAgregarDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarDocumento.Click

        Dim doc As Documentos = (From dc In db.Documentos
                                 Where dc.id = Hdd_id_documento.Value
                                 Select dc).FirstOrDefault

        If doc IsNot Nothing Then
            lstDocumentos.Items.Add(New ListItem With {.Text = doc.nombre, .Value = doc.id})
        End If
    End Sub

    Protected Sub bttEliminarDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarDocumento.Click
        Dim it As ListItem = lstDocumentos.SelectedItem

        CorrespondenciaEditada = (From crr In db.Correspondencia.Include("Documentos_Correspondencia").Include("Terceros_Correspondencia")
                                  Where crr.id = id_correspondencia
                                  Select crr).FirstOrDefault

        If it IsNot Nothing Then
            If CorrespondenciaEditada IsNot Nothing Then
                Dim DocumentoCorrespondencia As Documentos_Correspondencia = (From dcr In CorrespondenciaEditada.Documentos_Correspondencia
                                                                              Where dcr.id_documento = it.Value
                                                                              Select dcr).FirstOrDefault
                If DocumentoCorrespondencia IsNot Nothing Then
                    db.Documentos_Correspondencia.Remove(DocumentoCorrespondencia)
                    db.SaveChanges()
                    BoundCorrespondencia(CorrespondenciaEditada)
                Else
                    lstDocumentos.Items.Remove(it)
                End If
            Else
                lstDocumentos.Items.Remove(it)
            End If
        End If
    End Sub
End Class