
Imports Infragistics.Web.UI.GridControls

Public Class Page_BuscarDocumentos
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim idt As Long
        bttfirma.Visible = False
        If Utilidades.ConsultarUsuario(User.Identity.Name).id_tercero IsNot Nothing Then
            idt = Utilidades.ConsultarUsuario(User.Identity.Name).id_tercero
            Dim trx = (From tx In db.Terceros
                       Where tx.id = idt
                       Select tx).FirstOrDefault
            If trx IsNot Nothing Then
                If trx.firmas Then
                    bttfirma.Visible = True
                Else
                    bttfirma.Visible = False
                End If
            End If
        End If
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

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
        cadena = cadena & ",Documentos.estado"
        cadena = cadena & ",isnull(ubicaciones.ubicacion,'Pendiente') as ubicacion"
        cadena = cadena & ",isnull(ubicaciones.archivo,'') as archivou"
        cadena = cadena & ",AdminTipoDocumento.nombre as tipodoc"
        cadena = cadena & ",isnull(Areas.nombre,'Pendiente') Area"
        cadena = cadena & ",isnull(Areas.color,'') Areaid"
        cadena = cadena & ",isnull(Terceros.nombre,'Pendiente') Tercero"
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

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        connsulta()


    End Sub




    Private Sub GridDocs_RowSelectionChanged(sender As Object, e As SelectedRowEventArgs) Handles GridDocs.RowSelectionChanged
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
        End If
    End Sub
    Private Sub GridDocs_InitializeRow(sender As Object, e As RowEventArgs) Handles GridDocs.InitializeRow

        Dim i As Long
        Dim txtc As TextBox
        For i = 0 To e.Row.Items.Count
            If e.Row.Items.Item(16).Value IsNot Nothing Then
                If e.Row.Items.Item(16).Value <> "" Then
                    txtc = e.Row.Items.Item(15).FindControl("TextBox1")
                    If txtc IsNot Nothing Then
                        txtc.Text = ""
                        txtc.TextMode = TextBoxMode.Color
                        txtc.Text = e.Row.Items.Item(16).Value
                        txtc.Enabled = False
                    End If

                End If
            End If
        Next

    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Docs" & Today.Date.ToString
        ExpGrid.Export(GridDocs)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Docs" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridDocs)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../GesDoc/EditarDocumento.aspx?buscardoc=1&id_documento=" & id_cl)

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Documento", Me)
        End If
    End Sub

    Protected Sub bttfirma_Click(sender As Object, e As ImageClickEventArgs) Handles bttfirma.Click
        Dim idt As Long
        idt = Utilidades.ConsultarUsuario(User.Identity.Name).ID
        Utilidades.RegistrarScript("window.open('../Admin/GenerarFirma.aspx?id_usuario=" & idt & "','Documento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub
End Class