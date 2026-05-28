Public Class Seleccionar_Documento
    Inherits Page

    Private db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
        End If
    End Sub
    Private Sub cons()
        Dim cadena As String
        Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)

        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        cadena = "SELECT distinct Documentos.nombre"
        cadena = cadena & ",Documentos.descripcion"
        cadena = cadena & ",Documentos.archivo"
        cadena = cadena & ",Documentos.version"
        cadena = cadena & ",Documentos.fechadoc"
        cadena = cadena & ",Documentos.fecha_creacion"
        cadena = cadena & ",Documentos.fecha_modificacion"
        cadena = cadena & ",Documentos.usuario"
        cadena = cadena & ",Documentos.id_tipo"
        cadena = cadena & ",Documentos.id_ubicacion"
        cadena = cadena & ",Documentos.id_cliente"
        cadena = cadena & ",Documentos.id"
        cadena = cadena & ",Documentos.alias"
        cadena = cadena & ",Documentos.folios"
        cadena = cadena & ",isnull(ubicaciones.ubicacion,'Pendiente') as ubicacion"
        cadena = cadena & ",AdminTipoDocumento.nombre as tipodoc"
        cadena = cadena & ",isnull(Areas.nombre,'') Area"
        cadena = cadena & ",isnull(Areas.id,'') Areaid"
        cadena = cadena & ",isnull(Terceros.nombre,'') Tercero"
        cadena = cadena & " From Documentos"
        cadena = cadena & " left join ubicaciones"
        cadena = cadena & " On Documentos.id_ubicacion=ubicaciones.id"
        cadena = cadena & " inner join AdminTipoDocumento"
        cadena = cadena & " On Documentos.id_tipo=AdminTipoDocumento.id"
        cadena = cadena & " left Join documentos_area"
        cadena = cadena & " On documentos.id=documentos_area.id_documento"
        cadena = cadena & " left Join Areas"
        cadena = cadena & " On Documentos_Area.id_area=Areas.id"
        cadena = cadena & " left Join Documentos_Terceros"
        cadena = cadena & " On Documentos.id=Documentos_Terceros.id_documento"
        cadena = cadena & " left Join Terceros"
        cadena = cadena & " On Documentos_Terceros.id_tercero=Terceros.id"
        cadena = cadena & " where Documentos.id_sucursal=" & ids
        If txtTipoDocumento.SelectedValue <> "" Then
            If txtTipoDocumento.SelectedValue IsNot Nothing And txtTipoDocumento.SelectedValue <> 0 Then
                cadena = cadena & " And Documentos.id_tipo=" & txtTipoDocumento.SelectedValue
            End If
        End If
        If TxtUbicacion.SelectedValue <> "" Then
            If TxtUbicacion.SelectedValue IsNot Nothing And TxtUbicacion.SelectedValue <> 0 Then
                cadena = cadena & " And Documentos.id_ubicacion=" & TxtUbicacion.SelectedValue
            End If
        End If
        If txtdescipcion.Text <> "" Then
            cadena = cadena & " And Documentos.descripcion Like '%" & txtdescipcion.Text & "%'"
        End If
        If txtNombre.Text <> "" Then
            cadena = cadena & " And Documentos.alias Like '%" & txtNombre.Text & "%'"
        End If
        If txtArea.Text <> "" Then
            cadena = cadena & " And Areas.nombre Like '%" & txtArea.Text & "%'"
        End If
        If txtTercero.Text <> "" Then
            cadena = cadena & " And Terceros.nombre Like '%" & txtTercero.Text & "%'"
        End If
        If Usuario.id_tercero IsNot Nothing Then
            Dim trx = (From tr In db.Terceros
                       Where tr.id = Usuario.id_tercero
                       Select tr).FirstOrDefault
            If trx IsNot Nothing Then
                cadena = cadena & " And (documentos.usuario='" & Usuario.usuario & "' or documentos.id in (select id_documento from documentos_area where id_documento=documentos.id and id_area =" & trx.id_area & "))"
            End If
        Else
            If Usuario.Areas = False Then
                cadena = cadena & " And (documentos.usuario='" & Usuario.usuario & "' or documentos.id in (select id_documento from documentos_area where id_documento=documentos.id and id_area in (select id_area in usuarios_areas where id_usuario=" & Usuario.ID & ")))"
            End If
        End If
        cadena = cadena & " order by Alias"

        Src_Clientes.SelectCommand = cadena
    End Sub


    Private Sub connsulta()
        Dim cadena As String
        Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)

        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        cadena = "SELECT distinct Documentos.nombre"
        cadena = cadena & ",Documentos.descripcion"
        cadena = cadena & ",Documentos.archivo"
        cadena = cadena & ",Documentos.version"
        cadena = cadena & ",Documentos.fechadoc"
        cadena = cadena & ",Documentos.fecha_creacion"
        cadena = cadena & ",Documentos.fecha_modificacion"
        cadena = cadena & ",Documentos.usuario"
        cadena = cadena & ",Documentos.id_tipo"
        cadena = cadena & ",Documentos.id_ubicacion"
        cadena = cadena & ",Documentos.id_cliente"
        cadena = cadena & ",Documentos.id"
        cadena = cadena & ",Documentos.alias"
        cadena = cadena & ",Documentos.folios"
        cadena = cadena & ",isnull(ubicaciones.ubicacion,'Pendiente') as ubicacion"
        cadena = cadena & ",AdminTipoDocumento.nombre as tipodoc"
        cadena = cadena & ",isnull(Areas.nombre,'') Area"
        cadena = cadena & ",isnull(Areas.id,'') Areaid"
        cadena = cadena & ",isnull(Terceros.nombre,'') Tercero"
        cadena = cadena & " From Documentos"
        cadena = cadena & " left join ubicaciones"
        cadena = cadena & " On Documentos.id_ubicacion=ubicaciones.id"
        cadena = cadena & " inner join AdminTipoDocumento"
        cadena = cadena & " On Documentos.id_tipo=AdminTipoDocumento.id"
        cadena = cadena & " left Join documentos_area"
        cadena = cadena & " On documentos.id=documentos_area.id_documento"
        cadena = cadena & " left Join Areas"
        cadena = cadena & " On Documentos_Area.id_area=Areas.id"
        cadena = cadena & " left Join Documentos_Terceros"
        cadena = cadena & " On Documentos.id=Documentos_Terceros.id_documento"
        cadena = cadena & " left Join Terceros"
        cadena = cadena & " On Documentos_Terceros.id_tercero=Terceros.id"
        cadena = cadena & " where Documentos.id_sucursal=" & ids
        If txtTipoDocumento.SelectedValue <> "" Then
            If txtTipoDocumento.SelectedValue IsNot Nothing And txtTipoDocumento.SelectedValue <> 0 Then
                cadena = cadena & " And Documentos.id_tipo=" & txtTipoDocumento.SelectedValue
            End If
        End If
        If TxtUbicacion.SelectedValue <> "" Then
            If TxtUbicacion.SelectedValue IsNot Nothing And TxtUbicacion.SelectedValue <> 0 Then
                cadena = cadena & " And Documentos.id_ubicacion=" & TxtUbicacion.SelectedValue
            End If
        End If
        If txtdescipcion.Text <> "" Then
            cadena = cadena & " And Documentos.descripcion Like '%" & txtdescipcion.Text & "%'"
        End If
        If txtNombre.Text <> "" Then
            cadena = cadena & " And Documentos.alias Like '%" & txtNombre.Text & "%'"
        End If
        If txtArea.Text <> "" Then
            cadena = cadena & " And Areas.nombre Like '%" & txtArea.Text & "%'"
        End If
        If txtTercero.Text <> "" Then
            cadena = cadena & " And Terceros.nombre Like '%" & txtTercero.Text & "%'"
        End If
        If Usuario.id_tercero IsNot Nothing Then
            Dim trx = (From tr In db.Terceros
                       Where tr.id = Usuario.id_tercero
                       Select tr).FirstOrDefault
            If trx IsNot Nothing Then
                cadena = cadena & " And (documentos.usuario='" & Usuario.usuario & "' or documentos.id in (select id_documento from documentos_area where id_documento=documentos.id and id_area =" & trx.id_area & "))"
            End If
        Else
            If Usuario.Areas = False Then
                cadena = cadena & " And (documentos.usuario='" & Usuario.usuario & "' or documentos.id in (select id_documento from documentos_area where id_documento=documentos.id and id_area in (select id_area in usuarios_areas where id_usuario=" & Usuario.ID & ")))"
            End If
        End If
        cadena = cadena & " order by Alias"

        Src_Clientes.SelectCommand = cadena
        Src_Clientes.DataBind()
        GridDocs.DataBind()
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        connsulta()
    End Sub

    Protected Sub BttSeleccionar_Click(sender As Object, e As EventArgs) Handles BttSeleccionar.Click
        cons()
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)

            If Not rw Is Nothing Then
                Dim iddocumento As Long = rw.DataKey(0)
                Dim DocumentoSeleccionado = (From tr In db.Documentos
                                             Where tr.id = iddocumento
                                             Select tr).FirstOrDefault
                If DocumentoSeleccionado IsNot Nothing Then
                    Dim CallbackFunction As String = "ActualizarSeleccionDocumento"
                    Utilidades.RegistrarScript(String.Format("parent.window.opener.{0}({1},'{2}'); window.close();", CallbackFunction, DocumentoSeleccionado.id, DocumentoSeleccionado.alias), Me)

                Else
                    Utilidades.Mensaje("No es posible seleccionar el documento", Me)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun documento", Me)
        End If
    End Sub


    Protected Sub bttAbrir_Click(sender As Object, e As ImageClickEventArgs) Handles bttAbrir.Click
        cons()
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim doc = (From cl In db.Documentos
                           Where cl.id = id_cl
                           Select cl).FirstOrDefault
                If doc IsNot Nothing Then
                    Dim lnk As String
                    lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/" & doc.nombre
                    Utilidades.RegistrarScript("window.open('" & lnk & "','Documento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Documento", Me)
        End If
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Vistas/GesDoc/IndexarDocumento.aspx?id_documento=-1&Seleccion=1")
    End Sub
End Class