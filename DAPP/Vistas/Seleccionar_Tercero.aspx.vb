Public Class Seleccionar_Tercero
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
    Private ReadOnly Property id_responsable As Long
        Get
            If Me.ClientQueryString.Contains("id_responsable") Then
                Return CLng(Request.QueryString.Get("id_responsable"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property interno As Long
        Get
            If Me.ClientQueryString.Contains("interno") Then
                Return CLng(Request.QueryString.Get("interno"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_destinatario As Long
        Get
            If Me.ClientQueryString.Contains("id_destinatario") Then
                Return CLng(Request.QueryString.Get("id_destinatario"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_mensajeria As Long
        Get
            If Me.ClientQueryString.Contains("id_mensajeria") Then
                Return CLng(Request.QueryString.Get("id_mensajeria"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_solicitante As Long
        Get
            If Me.ClientQueryString.Contains("id_solicitante") Then
                Return CLng(Request.QueryString.Get("id_solicitante"))
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

    Protected Sub bttBuscarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarTercero.Click
        GridDatos.DataBind()
    End Sub

    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        Response.Redirect("Admin/EditarTercero.aspx?id_tercero=-1")
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridDatos.Behaviors.Selection IsNot Nothing AndAlso GridDatos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDatos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim idTercero As Long = rw.DataKey(0)
                Response.Redirect("Admin/EditarTercero.aspx?id_tercero=" & idTercero)
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun tercero", Me)
        End If
    End Sub

    Protected Sub bttSeleccionar_Click(sender As Object, e As EventArgs) Handles bttSeleccionar.Click
        If GridDatos.Behaviors.Selection IsNot Nothing AndAlso GridDatos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridDatos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim idTercero As Long = rw.DataKey(0)
                Dim TerceroSeleccionado = (From tr In db.Terceros
                                           Where tr.id = idTercero
                                           Select tr).FirstOrDefault
                If TerceroSeleccionado IsNot Nothing Then
                    Dim CallbackFunction As String = "ActualizarSeleccionTercero"
                    If id_responsable = 1 Then
                        CallbackFunction = "ActualizarSeleccionResponsable"
                    ElseIf id_destinatario = 1 Then
                        CallbackFunction = "ActualizarSeleccionDestinatario"
                    ElseIf id_mensajeria = 1 Then
                        CallbackFunction = "ActualizarSeleccionMensajeria"
                    Else
                        CallbackFunction = "ActualizarSeleccionTercero"
                    End If
                    Utilidades.RegistrarScript(String.Format("parent.window.opener.{0}({1},{2},'{3}'); window.close();", CallbackFunction, TerceroSeleccionado.id, TerceroSeleccionado.numero_documento, TerceroSeleccionado.nombre), Me)
                Else
                    Utilidades.Mensaje("No es posible seleccionar tercero", Me)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningun tercero", Me)
        End If
    End Sub

End Class