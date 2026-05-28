Public Class EditarTipoDoc
    Inherits System.Web.UI.Page


    Private TipoDocEditado As AdminTipoDocumento
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tipodoc As Long
        Get
            If Me.ClientQueryString.Contains("id_tipodoc") Then
                Return CLng(Request.QueryString.Get("id_tipodoc"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If txtClase.Items.Count > 0 Then

        End If
        If Not Page.IsPostBack Then
            txtClase.DataBind()
            If txtClase.Items.Count = 0 Then
                Response.Redirect("../Admin/Page_TiposDocumento.aspx")
            Else
                Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
                If Usuario Is Nothing Then
                    Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
                End If

                If id_tipodoc <> -1 Then
                    TipoDocEditado = (From tr In db.AdminTipoDocumento
                                      Where tr.id = id_tipodoc
                                      Select tr).FirstOrDefault

                    If TipoDocEditado IsNot Nothing Then
                        BoundTipoDoc(TipoDocEditado)
                    End If
                    bttNuevo.Enabled = True
                Else
                    bttNuevo.Enabled = False
                End If
            End If

        End If
    End Sub

    Private Sub BoundTipoDoc(TipoDocEditado As AdminTipoDocumento)
        txtTipoDocumento.Text = TipoDocEditado.nombre
        txtDescipcion.Text = TipoDocEditado.descripcion
        txtClase.SelectedValue = TipoDocEditado.id_clase
        Chkactivo.Checked = TipoDocEditado.Activo
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_tipodoc = -1 Then
            TipoDocEditado = New AdminTipoDocumento
            TipoDocEditado.id_cliente = Session("id_cliente")
            TipoDocEditado.id_sucursal = CLng(Session("id_sucursal"))
            db.AdminTipoDocumento.Add(TipoDocEditado)
        Else
            TipoDocEditado = (From tr In db.AdminTipoDocumento
                              Where tr.id = id_tipodoc
                              Select tr).FirstOrDefault
        End If


        If TipoDocEditado IsNot Nothing Then
            If id_tipodoc = -1 Then
                sx = (From tr In db.AdminTipoDocumento
                      Where tr.nombre = txtTipoDocumento.Text And tr.id_sucursal = ids
                      Select tr).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                TipoDocEditado.nombre = txtTipoDocumento.Text
                TipoDocEditado.descripcion = txtDescipcion.Text
                TipoDocEditado.id_clase = txtClase.SelectedValue
                TipoDocEditado.Activo = Chkactivo.Checked
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_tipodoc = -1 Then
                    Utilidades.RegistrarAuditoria("Tipo Documento", TipoDocEditado.id, TipoDocEditado.nombre, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Tipo Documento", TipoDocEditado.id, TipoDocEditado.nombre, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Tipo Documento Guardado", Me)
                Response.Redirect("../Admin/Page_TiposDocumento.aspx")
            Else
                Utilidades.Mensaje("Tipo Documento Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_TiposDocumento.aspx")
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("../Admin/EditarCamposAdicionales.aspx?id_tipo=" & id_tipodoc)
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDetTablas.Behaviors.Selection IsNot Nothing AndAlso GridDetTablas.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDetTablas.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_tb As Long = rw.DataKey(0)
                Response.Redirect("../Admin/EditarCamposAdicionales.aspx?id_tipo=" & id_tipodoc & "&id_campo=" & id_tb)
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
                Dim x As Integer
                x = (From cd In db.Campos_Documentos
                     Where cd.id_campo = id_tb
                     Select cd).Count
                If x = 0 Then
                    Dim tabla_eliminar = (From tb In db.AdminCamposAdicionales
                                          Where tb.id = id_tb
                                          Select tb).FirstOrDefault

                    If tabla_eliminar IsNot Nothing Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Campos Adicionales", id_tb, tabla_eliminar.Campo, "Eliminación Registro", User.Identity.Name, ids)
                        db.AdminCamposAdicionales.Remove(tabla_eliminar)
                        db.SaveChanges()
                        GridDetTablas.ClearDataSource()
                        GridDetTablas.DataSource = Src_DetTablas
                        GridDetTablas.DataBind()
                    End If
                Else
                    Utilidades.Mensaje("Documentos con información de Campo Asociada, Por favor Revisar.", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningun Registro", Me)
        End If
    End Sub

    Protected Sub bttExc1_Click(sender As Object, e As ImageClickEventArgs) Handles bttExc1.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "CamposAd" & Today.Date.ToString
        ExpGrid.Export(GridDetTablas)
    End Sub

    Protected Sub bttpd1_Click(sender As Object, e As ImageClickEventArgs) Handles bttpd1.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "CamposAd" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridDetTablas)
    End Sub
End Class