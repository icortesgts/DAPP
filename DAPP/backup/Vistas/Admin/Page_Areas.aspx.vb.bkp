Imports Infragistics.Web.UI.GridControls
Imports Infragistics.Web.UI.NavigationControls
Public Class Page_Areas
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            HCliente.Value = Session("id_cliente")
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarArea.aspx?id_area=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridAreas.Behaviors.Selection IsNot Nothing AndAlso GridAreas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridAreas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_ar As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarArea.aspx?id_area=" & id_ar)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Area", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridAreas.Behaviors.Selection IsNot Nothing AndAlso GridAreas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridAreas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_ar As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From ar In db.Areas
                     Where ar.id_area = id_ar
                     Select ar).Count
                If x = 0 Then
                    x = (From ar In db.Documentos_Area
                         Where ar.id_area = id_ar
                         Select ar).Count
                    If x = 0 Then
                        Dim area_eliminar = (From ar In db.Areas
                                             Where ar.id = id_ar
                                             Select ar).FirstOrDefault

                        If area_eliminar IsNot Nothing Then
                            Dim ids As Long
                            If Session("id_sucursal") IsNot Nothing Then
                                ids = Session("id_sucursal")
                            Else
                                ids = 0
                            End If
                            Utilidades.RegistrarAuditoria("Areas", id_ar, area_eliminar.nombre, "Eliminación Registro", User.Identity.Name, ids)
                            db.Areas.Remove(area_eliminar)
                            db.SaveChanges()
                            GridAreas.ClearDataSource()
                            GridAreas.DataSource = Src_Areas
                            GridAreas.DataBind()
                        End If
                    Else
                        Utilidades.Mensaje("Existen Documentos asociadas a está Área, por favor Revisar.", Me)
                    End If
                Else
                    Utilidades.Mensaje("Existen Otras áreas asociadas a está Área, por favor Revisar.", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Area", Me)
        End If
    End Sub



    Private Sub GridAreas_InitializeRow(sender As Object, e As RowEventArgs) Handles GridAreas.InitializeRow
        Dim i As Long
        Dim txtc As TextBox
        For i = 0 To e.Row.Items.Count
            If e.Row.Items.Item(6).Value IsNot Nothing And e.Row.Items.Item(6).Value IsNot DBNull.Value Then
                If e.Row.Items.Item(6).Value <> "" Then
                    txtc = e.Row.Items.Item(3).FindControl("TextBox1")
                    If txtc IsNot Nothing Then
                        txtc.Text = ""
                        txtc.TextMode = TextBoxMode.Color
                        txtc.Text = e.Row.Items.Item(6).Value
                        txtc.ReadOnly = True
                        txtc.Enabled = False
                    End If

                End If
            End If
        Next
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Areas" & Today.Date.ToString
        ExpGrid.Export(GridAreas)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Areas" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridAreas)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub bttSeleccionar_Click(sender As Object, e As EventArgs) Handles bttSeleccionar.Click
        Utilidades.RegistrarScript("window.open('../Areas_Configuradas.aspx','SeleccionarArea','width=800,height=800,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 800)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub
End Class