
Public Class Page_Sucursal
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
        Response.Redirect("../Admin/EditarSucursal.aspx?id_sucursal=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarSucursal.aspx?id_sucursal=" & id_cl)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                x = (From cl In db.AdminClaseDocumento
                     Where cl.id_sucursal = id_cl
                     Select cl).Count
                If x = 0 Then
                    x = (From cl In db.AdminTipoDocumento
                         Where cl.id_sucursal = id_cl
                         Select cl).Count
                    If x = 0 Then
                        x = (From cl In db.Areas
                             Where cl.id_sucursal = id_cl
                             Select cl).Count
                        If x = 0 Then
                            Dim cliente_eliminar = (From cl In db.AdminSucursal
                                                    Where cl.Id = id_cl
                                                    Select cl).FirstOrDefault

                            If cliente_eliminar IsNot Nothing Then
                                Dim ids As Long
                                If Session("id_sucursal") IsNot Nothing Then
                                    ids = Session("id_sucursal")
                                Else
                                    ids = 0
                                End If
                                Utilidades.RegistrarAuditoria("Sucursal", id_cl, cliente_eliminar.Sucursal, "Eliminación Registro", User.Identity.Name, ids)
                                db.AdminSucursal.Remove(cliente_eliminar)
                                db.SaveChanges()
                                GridClientes.ClearDataSource()
                                GridClientes.DataSource = Src_Clientes
                                GridClientes.DataBind()
                            End If
                        Else
                            Utilidades.Mensaje("Areas Asociadas a la Sucursal, por favor revise", Me)
                        End If

                    Else
                        Utilidades.Mensaje("Tipos de Documento Asociados a la Sucursal, por favor revise", Me)
                    End If

                Else
                    Utilidades.Mensaje("Clases de Documento Asociadas a la Sucursal, por favor revise", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Sucursales" & Today.Date.ToString
        ExpGrid.Export(GridClientes)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Sucursales" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridClientes)
        'Exppdf.Export(GridTipos)
    End Sub

    Protected Sub bttContacto_Click(sender As Object, e As ImageClickEventArgs) Handles bttContacto.Click
        If GridClientes.Behaviors.Selection IsNot Nothing AndAlso GridClientes.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridClientes.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_cl As Long = rw.DataKey(0)
                Utilidades.RegistrarScript("window.open('../Contactos.aspx?id_sox=" & id_cl & "','Contactos Sucursal','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        Else
            Utilidades.Mensaje("No selecciono ninguna Sucursal", Me)
        End If
    End Sub
End Class