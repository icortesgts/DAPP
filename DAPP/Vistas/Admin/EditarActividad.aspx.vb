Public Class EditarActividad
    Inherits System.Web.UI.Page


    Private ActividadEditada As AdminActividad
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_actividad As Long
        Get
            If Me.ClientQueryString.Contains("id_actividad") Then
                Return CLng(Request.QueryString.Get("id_actividad"))
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

            If id_actividad <> -1 Then
                ActividadEditada = (From tr In db.AdminActividad
                                    Where tr.id = id_actividad
                                    Select tr).FirstOrDefault

                If ActividadEditada IsNot Nothing Then
                    BoundTipoDoc(ActividadEditada)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(ActividadEditada As AdminActividad)
        txtTipoDocumento.Text = ActividadEditada.Actividad
        txtDescipcion.Text = ActividadEditada.Descripcion
        Chkactivo.Checked = ActividadEditada.Activo
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        If id_actividad = -1 Then
            ActividadEditada = New AdminActividad
            ActividadEditada.id_sucursal = CLng(Session("id_sucursal"))
            ActividadEditada.id_cliente = CLng(Session("id_cliente"))
            db.AdminActividad.Add(ActividadEditada)
        Else
            ActividadEditada = (From tr In db.AdminActividad
                                Where tr.id = id_actividad
                                Select tr).FirstOrDefault
        End If


        If ActividadEditada IsNot Nothing Then
            ActividadEditada.Actividad = txtTipoDocumento.Text
            ActividadEditada.Descripcion = txtDescipcion.Text
            ActividadEditada.Activo = Chkactivo.Checked
        End If

        db.SaveChanges()
        Dim ids As Long
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_actividad = -1 Then
            Utilidades.RegistrarAuditoria("Actividad Terceros", ActividadEditada.id, ActividadEditada.Actividad, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Actividad Terceros", ActividadEditada.id, ActividadEditada.Actividad, "Actualización Registro", User.Identity.Name, ids)
        End If
        Utilidades.Mensaje("Clase Documento Guardado", Me)
        Response.Redirect("../Admin/Page_ActividadTerceros.aspx")
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_ActividadTerceros.aspx")
    End Sub

End Class