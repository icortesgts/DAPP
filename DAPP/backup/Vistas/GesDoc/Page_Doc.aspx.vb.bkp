
Imports Infragistics.Web.UI.GridControls

Public Class Page_Doc
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../GesDoc/IndexarDocumento.aspx?id_documento=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../GesDoc/IndexarDocumento.aspx?id_documento=" & id_cl)

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Documento", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridDocs.Behaviors.Selection IsNot Nothing AndAlso GridDocs.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDocs.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim cdoc = (From cd In db.Campos_Documentos
                            Where cd.id_doc = id_cl
                            Select cd).ToList
                For Each cdx In cdoc
                    db.Campos_Documentos.Remove(cdx)
                    db.SaveChanges()
                Next
                Dim cliente_eliminar = (From cl In db.Documentos
                                        Where cl.id = id_cl
                                        Select cl).FirstOrDefault

                If cliente_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Documentos", id_cl, cliente_eliminar.nombre & "- " & cliente_eliminar.alias, "Eliminación Registro", User.Identity.Name, ids)
                    db.Documentos.Remove(cliente_eliminar)
                    db.SaveChanges()
                    GridDocs.ClearDataSource()
                    GridDocs.DataSource = Src_Clientes
                    GridDocs.DataBind()
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Documento", Me)
        End If
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
        Dim uno As String
        Dim dos As String
        Dim ida As Long
        For i = 0 To e.Row.Items.Count



            If e.Row.Items.Item(14).Value <> 0 Then
                uno = ""
                dos = ""
                ida = CLng(e.Row.Items.Item(14).Value)
                Dim ar = (From cl In db.Areas
                          Where cl.id = ida
                          Select cl).FirstOrDefault
                If ar IsNot Nothing Then
                    uno = ar.nombre
                    If ar.id_area IsNot Nothing Then
                        Dim arx = (From cl In db.Areas
                                   Where cl.id = ar.id_area
                                   Select cl).FirstOrDefault
                        If arx IsNot Nothing Then
                            dos = arx.nombre
                        End If
                    End If
                End If
                If uno = "GESTION DIRECTIVA" Or dos = "GESTION DIRECTIVA" Then
                    e.Row.Items.Item(14).CssClass = "blueBg"
                End If

                If uno = "GESTION ACADEMICA" Or dos = "GESTION ACADEMICA" Then
                    e.Row.Items.Item(14).CssClass = "pinkBg"
                End If

                If uno = "GESTION DE LA COMUNIDAD" Or dos = "GESTION DE LA COMUNIDAD" Then
                    e.Row.Items.Item(14).CssClass = "greenBg"
                End If

                If uno = "GESTION ADMINISTRATIVA FINANCIERA" Or dos = "GESTION ADMINISTRATIVA FINANCIERA" Then
                    e.Row.Items.Item(14).CssClass = "yellowBg"
                End If
                If uno = "Seguimiento Academico" Or dos = "Seguimiento Academico" Then
                    e.Row.Items.Item(14).CssClass = "pinkBg"
                End If

                If uno = "Financiero y Contable" Or dos = "Financiero y Contable" Then
                    e.Row.Items.Item(14).CssClass = "yellowBg"
                End If
            End If



        Next
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Documentos" & Today.Date.ToString
        ExpGrid.Export(GridDocs)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Documentos" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridDocs)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub BttScan_Click(sender As Object, e As ImageClickEventArgs) Handles BttScan.Click
        Response.Redirect("../GesDoc/ScannearDocumento.aspx?id_documento=-1")
    End Sub
End Class