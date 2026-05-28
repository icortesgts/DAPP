
Imports Infragistics.Web.UI.GridControls

Public Class Page_ListaChequeo
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
        Response.Redirect("../Admin/EditarLista.aspx?id_lista=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarLista.aspx?id_lista=" & id_tp)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Lista", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim tipo_eliminar = (From tp In db.Procesos.Include("Documentos_Requeridos")
                                     Where tp.id = id_tp
                                     Select tp).FirstOrDefault

                If tipo_eliminar IsNot Nothing Then
                    Dim x As Integer
                    x = tipo_eliminar.Documentos_Requeridos.Count
                    If x = 0 Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Listas de Chequeo", id_tp, tipo_eliminar.proceso, "Eliminación Registro", User.Identity.Name, ids)
                        db.Procesos.Remove(tipo_eliminar)
                        db.SaveChanges()
                        GridTipos.ClearDataSource()
                        GridTipos.DataSource = Src_Tipos
                        GridTipos.DataBind()
                    Else
                        Utilidades.Mensaje("Tipos de Documento Asociados a la Lista, por favor Revisar.", Me)
                    End If

                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tipo de Documento", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "ListasCh" & Today.Date.ToString
        ExpGrid.Export(GridTipos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "ListasCh" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTipos)
        'Exppdf.Export(GridTipos)
    End Sub

    Private Sub GridTipos_RowSelectionChanged(sender As Object, e As SelectedRowEventArgs) Handles GridTipos.RowSelectionChanged
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                HiddenField1.Value = id_tp
                'TextBox1.Text = "salto"
            End If
        End If
    End Sub

    Protected Sub BttExc1_Click(sender As Object, e As ImageClickEventArgs) Handles BttExc1.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "TiposAsociados" & Today.Date.ToString
        ExpGrid.Export(GridTiposDoc)
    End Sub

    Protected Sub Bttpd1_Click(sender As Object, e As ImageClickEventArgs) Handles Bttpd1.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "TiposAsociados" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridTiposDoc)
    End Sub

    Protected Sub BttDelete_Click(sender As Object, e As ImageClickEventArgs) Handles BttDelete.Click
        If GridTiposDoc.Behaviors.Selection IsNot Nothing AndAlso GridTiposDoc.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTiposDoc.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim tipo_eliminar = (From tp In db.Documentos_Requeridos.Include("AdminTipoDocumento")
                                     Where tp.id = id_tp
                                     Select tp).FirstOrDefault

                If tipo_eliminar IsNot Nothing Then
                    Dim x As Integer
                    x = 0
                    If x = 0 Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Tipos de Documentos Asociados a Listas de Chequeo", id_tp, tipo_eliminar.AdminTipoDocumento.nombre, "Eliminación Registro", User.Identity.Name, ids)
                        db.Documentos_Requeridos.Remove(tipo_eliminar)
                        db.SaveChanges()
                        GridTiposDoc.ClearDataSource()
                        GridTiposDoc.DataSource = Src_TiposDoc
                        GridTiposDoc.DataBind()
                    Else
                        Utilidades.Mensaje("Información Asociada, por favor Revisar.", Me)
                    End If

                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Tipo de Documento", Me)
        End If
    End Sub

    Protected Sub BttNew_Click(sender As Object, e As ImageClickEventArgs) Handles BttNew.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarTipod.aspx?id_lista=" & id_tp & "&id_tipod=-1")
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Lista", Me)
        End If
    End Sub

    Protected Sub BttEdit_Click(sender As Object, e As ImageClickEventArgs) Handles BttEdit.Click
        If GridTipos.Behaviors.Selection IsNot Nothing AndAlso GridTipos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTipos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tp As Long = rw.DataKey(0)
                If GridTiposDoc.Behaviors.Selection IsNot Nothing AndAlso GridTiposDoc.Behaviors.Selection.SelectedRows.Count > 0 Then
                    Dim rd As Infragistics.Web.UI.GridControls.ControlDataRecord = GridTiposDoc.Behaviors.Selection.SelectedRows(0)
                    If Not rd Is Nothing Then
                        Dim id_td As Long = rd.DataKey(0)
                        Response.Redirect("../Admin/EditarTipod.aspx?id_lista=" & id_tp & "&id_tipod=" & id_td)
                    End If
                Else
                    Utilidades.Mensaje("No selecciono ningún Tipo de Documento Asociado", Me)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Lista", Me)
        End If
    End Sub
End Class