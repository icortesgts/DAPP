Public Class EditarTipoD
    Inherits System.Web.UI.Page


    Private TipoDEditado As Documentos_Requeridos
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tipod As Long
        Get
            If Me.ClientQueryString.Contains("id_tipod") Then
                Return CLng(Request.QueryString.Get("id_tipod"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_lista As Long
        Get
            If Me.ClientQueryString.Contains("id_lista") Then
                Return CLng(Request.QueryString.Get("id_lista"))
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
            If id_lista = -1 Then
                Utilidades.Mensaje("Lista de Chequeo no Referenciada", Me)
                Response.Redirect("../Admin/Page_ListaChequeo.aspx")
            Else
                If id_tipod <> -1 Then
                    TipoDEditado = (From tr In db.Documentos_Requeridos
                                    Where tr.id = id_tipod
                                    Select tr).FirstOrDefault

                    If TipoDEditado IsNot Nothing Then
                        BoundTipoDoc(TipoDEditado)
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub BoundTipoDoc(TipoDEditado As Documentos_Requeridos)
        txtClase.SelectedValue = TipoDEditado.id_tipo_documento
        txtDescipcion.Text = TipoDEditado.descripcion
        txtCantidad.Value = TipoDEditado.cantidad

    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_tipod = -1 Then
            TipoDEditado = New Documentos_Requeridos

            TipoDEditado.id_proceso = id_lista
            db.Documentos_Requeridos.Add(TipoDEditado)
        Else
            TipoDEditado = (From tr In db.Documentos_Requeridos
                            Where tr.id = id_tipod
                            Select tr).FirstOrDefault
        End If


        If TipoDEditado IsNot Nothing Then
            If id_tipod = -1 Then
                sx = (From tr In db.Documentos_Requeridos
                      Where tr.id_tipo_documento = txtClase.SelectedValue And tr.id_proceso = id_lista
                      Select tr).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                TipoDEditado.id_tipo_documento = txtClase.SelectedValue
                TipoDEditado.descripcion = txtDescipcion.Text

                TipoDEditado.cantidad = txtCantidad.ValueInt
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_tipod = -1 Then
                    Utilidades.RegistrarAuditoria("Detalle Lista de Chequeo", TipoDEditado.id, txtClase.Text, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Detalle Lista de Chequeo", TipoDEditado.id, txtClase.Text, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Tipo Documento Asociado", Me)
                Response.Redirect("../Admin/Page_ListaChequeo.aspx")
            Else
                Utilidades.Mensaje("Tipo Documento Asociado", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_ListaChequeo.aspx")
    End Sub

End Class