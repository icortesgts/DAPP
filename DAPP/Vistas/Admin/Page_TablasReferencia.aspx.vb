
Imports Infragistics.Web.UI.GridControls

Public Class Page_TablasReferencia
    Inherits System.Web.UI.Page

    Dim db As New DAPP_BDEntities
    Dim tbr As AdminTablasReferencia
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim id_cliente As Long
        id_cliente = Session("id_cliente")
        If Not Page.IsPostBack Then
            HCliente.Value = Session("id_cliente")
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            HiddenField1.Value = 0
        End If
    End Sub


    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        If HiddenField1.Value = "" Then
            Utilidades.Mensaje("Asegure los Datos de la Tabla de Referencia", Me)
        Else
            Response.Redirect("../Admin/EditarTablasReferencia.aspx?id_tablaret=" & HiddenField1.Value)
        End If

    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDetTablas.Behaviors.Selection IsNot Nothing AndAlso GridDetTablas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDetTablas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tb As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarTablasReferencia.aspx?id_tablaret=" & HiddenField1.Value & "&id_tipodoc=" & id_tb)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun Registro", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridDetTablas.Behaviors.Selection IsNot Nothing AndAlso GridDetTablas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDetTablas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tb As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim tabla_eliminar = (From tb In db.TipoDoc_TablaRetencion
                                      Where tb.id = id_tb
                                      Select tb).FirstOrDefault

                If tabla_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Serie documental", id_tb, tabla_eliminar.TipoDocumento, "Eliminación Registro", User.Identity.Name, ids)
                    db.TipoDoc_TablaRetencion.Remove(tabla_eliminar)
                    db.SaveChanges()
                    GridDetTablas.ClearDataSource()
                    GridDetTablas.DataSource = Src_DetTablas
                    GridDetTablas.DataBind()
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun Registro", Me)
        End If
    End Sub

    Private Sub GridTablas_RowSelectionChanged(sender As Object, e As SelectedRowEventArgs) Handles GridTablas.RowSelectionChanged
        If GridTablas.Behaviors.Selection IsNot Nothing AndAlso GridTablas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTablas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tb As Long = rw.DataKey(0)
                HiddenField1.Value = id_tb
                GridDetTablas.DataBind()
            End If
        End If
    End Sub

    Protected Sub bttNew_Click(sender As Object, e As ImageClickEventArgs) Handles bttNew.Click

        Response.Redirect("../Admin/EditarTablasRef.aspx?id_tablaret=-1")


    End Sub

    Protected Sub bttEdit_Click(sender As Object, e As ImageClickEventArgs) Handles bttEdit.Click
        If GridTablas.Behaviors.Selection IsNot Nothing AndAlso GridTablas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTablas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tb As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarTablasRef.aspx?id_tablaret=" & id_tb)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun Registro", Me)
        End If
    End Sub

    Protected Sub bttDelete_Click(sender As Object, e As ImageClickEventArgs) Handles bttDelete.Click
        If GridTablas.Behaviors.Selection IsNot Nothing AndAlso GridTablas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTablas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tb As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From tb In db.TipoDoc_TablaRetencion
                     Where tb.id_tablaret = id_tb
                     Select tb).Count
                If x = 0 Then
                    Dim tabla_eliminar = (From tb In db.AdminTablasReferencia
                                          Where tb.id = id_tb
                                          Select tb).FirstOrDefault

                    If tabla_eliminar IsNot Nothing Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Tablas de Retención", id_tb, tabla_eliminar.oficina, "Eliminación Registro", User.Identity.Name, ids)
                        db.AdminTablasReferencia.Remove(tabla_eliminar)
                        db.SaveChanges()
                        GridTablas.ClearDataSource()
                        GridTablas.DataSource = Src_Tablas
                        GridTablas.DataBind()
                    End If
                Else
                    Utilidades.Mensaje("Tabla de retención con Series y Tipos documentales por favor revisar", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningun Registro", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "TablasR" & Today.Date.ToString
        ExpGrid.Export(GridTablas)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "TablasR" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTablas)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub bttExc1_Click(sender As Object, e As ImageClickEventArgs) Handles bttExc1.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "SeriesD" & Today.Date.ToString
        ExpGrid.Export(GridDetTablas)
    End Sub

    Protected Sub bttpd1_Click(sender As Object, e As ImageClickEventArgs) Handles bttpd1.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "SeriesD" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridDetTablas)
    End Sub
End Class