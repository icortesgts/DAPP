Public Class Detalle_Documento
    Inherits Page

    Private db As New DAPP_BDEntities
    Private ReadOnly Property id_solicitud As Long
        Get
            If Me.ClientQueryString.Contains("id_solicitud") Then
                Return CLng(Request.QueryString.Get("id_solicitud"))
            Else
                Return -1
            End If
        End Get
    End Property
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
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            connsulta()
        Else
            connsulta()
        End If
    End Sub



    Private Sub connsulta()
        Dim cadena As String
        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If

        cadena = "SELECT Documentos.nombre"
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
        If id_solicitud <> -1 Then
            cadena = cadena & "and documentos.id in (select id_documento from documentos_solicitud where id_solicitud=" & id_solicitud & ")"
        ElseIf id_correspondencia <> -1 Then
            cadena = cadena & "and documentos.id in (select id_documento from documentos_correspondencia where id_correspondencia=" & id_correspondencia & ")"
        End If
        cadena = cadena & " order by alias"
        Src_Clientes.SelectCommand = cadena
        GridDocs.DataBind()
    End Sub




    Protected Sub bttAbrir_Click(sender As Object, e As ImageClickEventArgs) Handles bttAbrir.Click
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
End Class