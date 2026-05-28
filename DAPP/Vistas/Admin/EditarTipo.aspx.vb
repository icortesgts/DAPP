Public Class EditarTipo
    Inherits System.Web.UI.Page


    Private TipoEditado As AdminTipoTercero
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tipo As Long
        Get
            If Me.ClientQueryString.Contains("id_tipo") Then
                Return CLng(Request.QueryString.Get("id_tipo"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_tipo <> -1 Then
                TipoEditado = (From tr In db.AdminTipoTercero
                               Where tr.Id = id_tipo
                               Select tr).FirstOrDefault

                If TipoEditado IsNot Nothing Then
                    BoundTipoDoc(TipoEditado)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(TipoEditado As AdminTipoTercero)
        txtTipoDocumento.Text = TipoEditado.Tipo
        txtDescipcion.Text = TipoEditado.Descripcion
        Chkactivo.Checked = TipoEditado.Activo
        If TipoEditado.interno IsNot Nothing Then
            chkinterno.Checked = TipoEditado.interno
        End If
        If TipoEditado.proveedor IsNot Nothing Then
            chkProveedor.Checked = TipoEditado.proveedor
        End If
        Dim lt = (From lst In db.Listas_Tipo.Include("Procesos")
                  Where lst.id_tipo = TipoEditado.Id
                  Select lst).ToList
        lstlista.Items.Clear()
        For Each ls In lt
            lstlista.Items.Add(New ListItem With {.Text = ls.Procesos.proceso, .Value = ls.id_proceso})
        Next
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_tipo = -1 Then
            TipoEditado = New AdminTipoTercero
            TipoEditado.id_sucursal = CLng(Session("id_sucursal"))
            TipoEditado.id_cliente = CLng(Session("id_cliente"))
            db.AdminTipoTercero.Add(TipoEditado)
        Else
            TipoEditado = (From tr In db.AdminTipoTercero
                           Where tr.Id = id_tipo
                           Select tr).FirstOrDefault
        End If


        If TipoEditado IsNot Nothing Then
            If id_tipo = -1 Then
                sx = (From tr In db.AdminTipoTercero
                      Where tr.Tipo = txtTipoDocumento.Text And tr.id_sucursal = ids
                      Select tr).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                TipoEditado.Tipo = txtTipoDocumento.Text
                TipoEditado.Descripcion = txtDescipcion.Text
                TipoEditado.Activo = Chkactivo.Checked
                TipoEditado.interno = chkinterno.Checked
                TipoEditado.proveedor = chkProveedor.Checked
                db.SaveChanges()
                For Each it As ListItem In lstlista.Items
                    Dim listas = (From dsc In db.Listas_Tipo
                                  Where dsc.id_tipo = TipoEditado.Id And dsc.id_proceso = it.Value
                                  Select dsc).FirstOrDefault
                    If listas Is Nothing Then
                        Dim ar As New Listas_Tipo
                        ar.id_proceso = it.Value
                        ar.id_tipo = TipoEditado.Id
                        db.Listas_Tipo.Add(ar)
                        db.SaveChanges()
                    End If
                Next
                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_tipo = -1 Then
                    Utilidades.RegistrarAuditoria("Tipo Terceros", TipoEditado.Id, TipoEditado.Tipo, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Tipo Terceros", TipoEditado.Id, TipoEditado.Tipo, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Tipo de Tercero Guardado", Me)
                Response.Redirect("../Admin/Page_TipoTerceros.aspx")
            Else
                Utilidades.Mensaje("Tipo de Tercero Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_TipoTerceros.aspx")
    End Sub



    Protected Sub BttDocumento_Click(sender As Object, e As ImageClickEventArgs) Handles BttDocumento.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Listo.aspx','SeleccionarLista','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttAgregarDoc_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarDoc.Click
        Dim doc As Procesos = (From dc In db.Procesos
                               Where dc.id = Hdd_id_lista.Value
                               Select dc).FirstOrDefault

        If doc IsNot Nothing Then
            lstlista.Items.Add(New ListItem With {.Text = doc.proceso, .Value = doc.id})
        End If
        TxtDocumento.Text = ""
        Hdd_id_lista.Value = 0
    End Sub

    Protected Sub bttEliminarDoc_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarDoc.Click
        Dim it As ListItem = lstlista.SelectedItem
        TipoEditado = (From dc In db.AdminTipoTercero.Include("Listas_tipo")
                       Where dc.Id = id_tipo
                       Select dc).FirstOrDefault

        If id_tipo <> -1 Then
            If it IsNot Nothing Then
                If TipoEditado IsNot Nothing Then
                    Dim listatipo As Listas_Tipo = (From dsc In TipoEditado.Listas_Tipo
                                                    Where dsc.id_proceso = it.Value
                                                    Select dsc).FirstOrDefault
                    If listatipo IsNot Nothing Then
                        db.Listas_Tipo.Remove(listatipo)
                        db.SaveChanges()
                        BoundTipoDoc(TipoEditado)
                    Else
                        lstlista.Items.Remove(it)
                    End If
                Else
                    lstlista.Items.Remove(it)
                End If
            End If
        Else
            If it IsNot Nothing Then
                lstlista.Items.Remove(it)
            End If
        End If
    End Sub
End Class