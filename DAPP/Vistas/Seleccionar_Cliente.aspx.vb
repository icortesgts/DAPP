Public Class Seleccionar_Cliente
    Inherits Page

    Private db As New DAPP_BDEntities

    Private ReadOnly Property cedula As Long
        Get
            If Me.ClientQueryString.Contains("cedula") Then
                Return CLng(Request.QueryString.Get("cedula"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        If Not Me.Page.IsPostBack Then
            If cedula <> -1 Then
                txtFiltro.Text = cedula
                GridDatos.DataBind()
            End If
        End If        
    End Sub

    Protected Sub bttBuscarCliente_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarCliente.Click
        GridDatos.DataBind()
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("Admin/EditarCliente.aspx?id_cliente=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDatos.Behaviors.Selection IsNot Nothing AndAlso GridDatos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDatos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim idCliente As Long = rw.DataKey(0)
                Response.Redirect("Admin/EditarCliente.aspx?id_cliente=" & idCliente)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun cliente", Me)
        End If        
    End Sub

    Protected Sub bttSeleccionar_Click(sender As Object, e As EventArgs) Handles bttSeleccionar.Click
        If GridDatos.Behaviors.Selection IsNot Nothing AndAlso GridDatos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDatos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim idCliente As Long = rw.DataKey(0)
                Dim ClienteSeleccionado = (From cl In db.Clientes
                                           Where cl.id = idCliente
                                           Select cl).FirstOrDefault
                If ClienteSeleccionado IsNot Nothing Then
                    Dim CallbackFunction As String = "ActualizarSeleccionCliente"
                    Utilidades.RegistrarScript(String.Format("parent.window.opener.{0}({1},{2},'{3}'); window.close();", CallbackFunction, ClienteSeleccionado.id, ClienteSeleccionado.numero_documento, ClienteSeleccionado.nombre), Me)
                Else
                    Utilidades.Mensaje("No es posible seleccionar cliente", Me)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun cliente", Me)
        End If
    End Sub

End Class