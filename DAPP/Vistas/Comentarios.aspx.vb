Imports Infragistics.Web.UI.NavigationControls

Public Class Comentarios
    Inherits Page

    Private db As New DAPP_BDEntities

    Private ReadOnly Property id_correspondencia As Long
        Get
            If Me.ClientQueryString.Contains("id_correspondencia") Then
                Return CLng(Request.QueryString.Get("id_correspondencia"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        If Not Me.Page.IsPostBack Then
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            If id_correspondencia = -1 Then
                Utilidades.Mensaje("Falta Asociar Info", Me)
                Utilidades.RegistrarScript(String.Format("window.opener.location.href = window.opener.location.href;window.close();"), Me)

            End If
        End If
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click

        Response.Redirect("../Vistas/EditarComentario.aspx?id_comentario=-1&id_correspondencia=" & id_correspondencia)

    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridContactos.Behaviors.Selection IsNot Nothing AndAlso GridContactos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridContactos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_c As Long = rw.DataKey(0)


                Response.Redirect("../Vistas/EditarComentario.aspx?id_comentario=" & id_c & "&id_correspondencia=" & id_correspondencia)

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Contacto", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridContactos.Behaviors.Selection IsNot Nothing AndAlso GridContactos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridContactos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_c As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                'x = (From tc In db.Documentos_Terceros
                '     Where tc.id_tercero = id_tc
                '     Select tc).Count
                x = 0
                If x = 0 Then
                    Dim tercero_eliminar = (From tc In db.Comentarios_Correspondencia
                                            Where tc.id = id_c
                                            Select tc).FirstOrDefault

                    If tercero_eliminar IsNot Nothing Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Comentarios Correspondencia", id_c, tercero_eliminar.Comentario, "Eliminación Registro", User.Identity.Name, ids)
                        db.Comentarios_Correspondencia.Remove(tercero_eliminar)
                        db.SaveChanges()
                        GridContactos.ClearDataSource()
                        GridContactos.DataSource = Src_Contactos
                        GridContactos.DataBind()
                    End If
                Else
                    Utilidades.Mensaje("Existen Datos Asociados al Contacto, Por favor Revisar.", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Comentario", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Comentarios" & Today.Date.ToString
        ExpGrid.Export(GridContactos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Comentarios" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridContactos)
        'Exppdf.Export(GridTipos)
    End Sub
End Class