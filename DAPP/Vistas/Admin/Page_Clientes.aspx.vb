
Imports Infragistics.Web.UI.GridControls

Public Class Page_Clientes
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
        Response.Redirect("../Admin/EditarCliente.aspx?id_cliente=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarCliente.aspx?id_cliente=" & id_cl)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From cx In db.Contratos_Cliente
                     Where cx.id_cliente = id_cl
                     Select cx).Count
                If x = 0 Then
                    Utilidades.Mensaje("Existen Contratos Asocicados, por favor revisar", Me)
                Else
                    x = (From cx In db.AdminSucursal
                         Where cx.id_cliente = id_cl
                         Select cx).Count
                    If x = 0 Then
                        Utilidades.Mensaje("Existen Sucursales Asocicadas, por favor revisar", Me)
                    Else
                        x = (From cx In db.Usuario_Cliente
                             Where cx.id_cliente = id_cl
                             Select cx).Count
                        If x = 0 Then
                            Utilidades.Mensaje("Existen Usuarios Asocicados, por favor revisar", Me)
                        Else
                            x = (From cx In db.AdminPerfiles
                                 Where cx.id_cliente = id_cl
                                 Select cx).Count
                            If x = 0 Then
                                Utilidades.Mensaje("Existen Perfiles Asocicados, por favor revisar", Me)
                            Else
                                Dim cliente_eliminar = (From cl In db.Clientes
                                                        Where cl.id = id_cl
                                                        Select cl).FirstOrDefault

                                If cliente_eliminar IsNot Nothing Then
                                    Dim ids As Long
                                    If Session("id_sucursal") IsNot Nothing Then
                                        ids = Session("id_sucursal")
                                    Else
                                        ids = 0
                                    End If
                                    Utilidades.RegistrarAuditoria("Clientes", id_cl, cliente_eliminar.nombre, "Eliminación Registro", User.Identity.Name, ids)
                                    db.Clientes.Remove(cliente_eliminar)
                                    db.SaveChanges()
                                    GridClientes.ClearDataSource()
                                    GridClientes.DataSource = Src_Clientes
                                    GridClientes.DataBind()
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Clientes" & Today.Date.ToString
        ExpGrid.Export(GridClientes)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Clientes" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridClientes)
        'Exppdf.Export(GridTipos)
    End Sub

    Private Sub GridClientes_RowSelectionChanged(sender As Object, e As SelectedRowEventArgs) Handles GridClientes.RowSelectionChanged
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Hdd_id_cliente.Value = id_cl

            End If
        End If
    End Sub

    Protected Sub BttNew_Click(sender As Object, e As ImageClickEventArgs) Handles BttNew.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarContrato.aspx?id_cliente=" & id_cl)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If

    End Sub

    Protected Sub BttEdit_Click(sender As Object, e As ImageClickEventArgs) Handles BttEdit.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                If GridContratos.Behaviors.Selection IsNot Nothing AndAlso GridContratos.Behaviors.Selection.SelectedRows.Count > 0 Then
                    Dim rwx As Infragistics.Web.UI.GridControls.ControlDataRecord = GridContratos.Behaviors.Selection.SelectedRows(0)
                    If Not rwx Is Nothing Then
                        Dim id_ct As Long = rwx.DataKey(0)
                        Response.Redirect("../Admin/EditarContrato.aspx?id_cliente=" & id_cl & "&id_contrato=" & id_ct)
                    End If
                Else
                    Utilidades.Mensaje("No selecciono ningún Contrato", Me)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If
    End Sub

    Protected Sub BttDelete_Click(sender As Object, e As ImageClickEventArgs) Handles BttDelete.Click
        If GridContratos.Behaviors.Selection IsNot Nothing AndAlso GridContratos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridContratos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)

                Dim cliente_eliminar = (From cl In db.Contratos_Cliente
                                        Where cl.id = id_cl
                                        Select cl).FirstOrDefault

                If cliente_eliminar IsNot Nothing Then
                    Dim ids As Long
                    If Session("id_sucursal") IsNot Nothing Then
                        ids = Session("id_sucursal")
                    Else
                        ids = 0
                    End If
                    Utilidades.RegistrarAuditoria("Contrato Clientes", id_cl, cliente_eliminar.Nro_Contrato, "Eliminación Registro", User.Identity.Name, ids)
                    db.Contratos_Cliente.Remove(cliente_eliminar)
                    db.SaveChanges()
                    GridContratos.ClearDataSource()
                    GridContratos.DataSource = SqlContratos
                    GridContratos.DataBind()

                End If
            Else
                Utilidades.Mensaje("No selecciono ningún Contrato", Me)
            End If
        End If
    End Sub

    Protected Sub BttXls_Click(sender As Object, e As ImageClickEventArgs) Handles BttXls.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Contrato Cliente" & Today.Date.ToString
        ExpGrid.Export(GridContratos)
    End Sub

    Protected Sub BttRep_Click(sender As Object, e As ImageClickEventArgs) Handles BttRep.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Contratos Clientes" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridContratos)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub bttContacto_Click(sender As Object, e As ImageClickEventArgs) Handles bttContacto.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Utilidades.RegistrarScript("window.open('../Contactos.aspx?id_cliente=" & id_cl & "','Contactos Cliente','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Cliente", Me)
        End If
    End Sub
End Class