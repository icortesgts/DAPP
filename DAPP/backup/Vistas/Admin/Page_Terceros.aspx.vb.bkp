
Imports Infragistics.Web.UI.GridControls

Public Class Page_Terceros
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
        Response.Redirect("../Admin/EditarTercero.aspx?id_tercero=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarTercero.aspx?id_tercero=" & id_tc)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tercero", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From tc In db.Documentos_Terceros
                     Where tc.id_tercero = id_tc
                     Select tc).Count
                If x = 0 Then
                    Dim tercero_eliminar = (From tc In db.Terceros
                                            Where tc.id = id_tc
                                            Select tc).FirstOrDefault

                    If tercero_eliminar IsNot Nothing Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Terceros", id_tc, tercero_eliminar.nombre, "Eliminación Registro", User.Identity.Name, ids)
                        db.Terceros.Remove(tercero_eliminar)
                        db.SaveChanges()
                        GridTerceros.ClearDataSource()
                        GridTerceros.DataSource = Src_Terceros
                        GridTerceros.DataBind()
                    End If
                Else
                    Utilidades.Mensaje("Existen Documentos Asociados al Tercero, Por favor Revisar.", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tercero", Me)
        End If
    End Sub

    Private Sub GridTerceros_RowSelectionChanged(sender As Object, e As SelectedRowEventArgs) Handles GridTerceros.RowSelectionChanged
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                HiddenField1.Value = id_tc
            End If

        End If
    End Sub

    Protected Sub BttNew_Click(sender As Object, e As ImageClickEventArgs) Handles BttNew.Click

        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarSuc.aspx?id_tercero=" & id_tc)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tercero", Me)
        End If
    End Sub

    Protected Sub BttEdit_Click(sender As Object, e As ImageClickEventArgs) Handles BttEdit.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                If GridSucursales.Behaviors.Selection IsNot Nothing AndAlso GridSucursales.Behaviors.Selection.SelectedRows.Count > 0 Then
                    Dim rwx As Infragistics.Web.UI.GridControls.ControlDataRecord = GridSucursales.Behaviors.Selection.SelectedRows(0)
                    If Not rwx Is Nothing Then
                        Dim id_sc As Long = rwx.DataKey(0)
                        Response.Redirect("../Admin/EditarSuc.aspx?id_tercero=" & id_tc & "&id_suc=" & id_sc)
                    Else
                        Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
                    End If
                End If
            Else
                Utilidades.Mensaje("No selecciono ningún Tercero", Me)
            End If
        End If
    End Sub

    Protected Sub BttDelete_Click(sender As Object, e As ImageClickEventArgs) Handles BttDelete.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                If GridSucursales.Behaviors.Selection IsNot Nothing AndAlso GridSucursales.Behaviors.Selection.SelectedRows.Count > 0 Then
                    Dim rwx As Infragistics.Web.UI.GridControls.ControlDataRecord = GridSucursales.Behaviors.Selection.SelectedRows(0)
                    If Not rwx Is Nothing Then
                        Dim id_sc As Long = rwx.DataKey(0)
                        Dim db As New DAPP_BDEntities
                        Dim x As Integer
                        'x = (From tc In db.Documentos_Terceros
                        '     Where tc.id_tercero = id_tc
                        '     Select tc).Count
                        x = 0
                        If x = 0 Then
                            Dim tercero_eliminar = (From tc In db.SucursalTerceros
                                                    Where tc.id = id_sc
                                                    Select tc).FirstOrDefault

                            If tercero_eliminar IsNot Nothing Then
                                Dim ids As Long
                                If Session("id_sucursal") IsNot Nothing Then
                                    ids = Session("id_sucursal")
                                Else
                                    ids = 0
                                End If
                                Utilidades.RegistrarAuditoria("Sucursal Terceros", id_sc, tercero_eliminar.nombre, "Eliminación Registro", User.Identity.Name, ids)
                                db.SucursalTerceros.Remove(tercero_eliminar)
                                db.SaveChanges()
                                GridSucursales.ClearDataSource()
                                GridSucursales.DataSource = Src_Sucursales
                                GridSucursales.DataBind()
                            End If
                        Else
                            Utilidades.Mensaje("Existen Documentos Asociados a la Sucursal del Tercero, Por favor Revisar.", Me)
                        End If
                    Else
                        Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
                    End If
                End If
            Else
                Utilidades.Mensaje("No selecciono ningún Tercero", Me)
            End If
        End If
    End Sub

    Protected Sub bttContacto_Click(sender As Object, e As ImageClickEventArgs) Handles bttContacto.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                Utilidades.RegistrarScript("window.open('../Contactos.aspx?id_tercero=" & id_tc & "','Contactos Tercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tercero", Me)
        End If

    End Sub

    Protected Sub BttContact_Click(sender As Object, e As ImageClickEventArgs) Handles BttContact.Click
        If GridTerceros.Behaviors.Selection IsNot Nothing AndAlso GridTerceros.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTerceros.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tc As Long = rw.DataKey(0)
                If GridSucursales.Behaviors.Selection IsNot Nothing AndAlso GridSucursales.Behaviors.Selection.SelectedRows.Count > 0 Then
                    Dim rwx As Infragistics.Web.UI.GridControls.ControlDataRecord = GridSucursales.Behaviors.Selection.SelectedRows(0)
                    If Not rwx Is Nothing Then
                        Dim id_sc As Long = rwx.DataKey(0)
                        Utilidades.RegistrarScript("window.open('../Contactos.aspx?id_suc=" & id_sc & "','Contactos Sucursal','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
                    Else
                        Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
                    End If
                End If
            Else
                Utilidades.Mensaje("No selecciono ningún Tercero", Me)
            End If
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Terceros" & Today.Date.ToString
        ExpGrid.Export(GridTerceros)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Terceros" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTerceros)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub BttExc1_Click(sender As Object, e As ImageClickEventArgs) Handles BttExc1.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "SucTerceros" & Today.Date.ToString
        ExpGrid.Export(GridSucursales)
    End Sub

    Protected Sub Bttpd1_Click(sender As Object, e As ImageClickEventArgs) Handles Bttpd1.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "SucTerceros" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridSucursales)
    End Sub
End Class