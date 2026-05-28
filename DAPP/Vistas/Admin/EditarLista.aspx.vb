Public Class EditarLista
    Inherits System.Web.UI.Page


    Private ProcesoEditado As Procesos
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

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

            If id_lista <> -1 Then
                ProcesoEditado = (From tr In db.Procesos
                                  Where tr.id = id_lista
                                  Select tr).FirstOrDefault

                If ProcesoEditado IsNot Nothing Then
                    BoundTipoDoc(ProcesoEditado)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(ProcesoEditado As Procesos)
        txtTipoDocumento.Text = ProcesoEditado.proceso
        txtDescipcion.Text = ProcesoEditado.descripcion

        Chkactivo.Checked = ProcesoEditado.Activo
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_lista = -1 Then
            ProcesoEditado = New Procesos
            ProcesoEditado.id_sucursal = ids
            db.Procesos.Add(ProcesoEditado)
        Else
            ProcesoEditado = (From tr In db.Procesos
                              Where tr.id = id_lista
                              Select tr).FirstOrDefault
        End If


        If ProcesoEditado IsNot Nothing Then
            If id_lista = -1 Then
                sx = (From tr In db.Procesos
                      Where tr.proceso = txtTipoDocumento.Text And tr.id_sucursal = ids
                      Select tr).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                ProcesoEditado.proceso = txtTipoDocumento.Text
                ProcesoEditado.descripcion = txtDescipcion.Text

                ProcesoEditado.Activo = Chkactivo.Checked
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_lista = -1 Then
                    Utilidades.RegistrarAuditoria("Lista de Chequeo", ProcesoEditado.id, ProcesoEditado.proceso, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Lista de Chequeo", ProcesoEditado.id, ProcesoEditado.proceso, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Lista de Chequeo Guardada", Me)
                Response.Redirect("../Admin/Page_ListaChequeo.aspx")
            Else
                Utilidades.Mensaje("Lista de Chequeo Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_TiposDocumento.aspx")
    End Sub

End Class