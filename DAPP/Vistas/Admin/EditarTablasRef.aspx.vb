Public Class EditarTablasRef
    Inherits System.Web.UI.Page


    Private TipoDocRet As AdminTablasReferencia
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tablaret As Long
        Get
            If Me.ClientQueryString.Contains("id_tablaret") Then
                Return CLng(Request.QueryString.Get("id_tablaret"))
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

            If id_tablaret <> -1 Then

                TipoDocRet = (From tr In db.AdminTablasReferencia
                              Where tr.id = id_tablaret
                              Select tr).FirstOrDefault

                If TipoDocRet IsNot Nothing Then
                    BoundTipoDocRet(TipoDocRet)
                End If
            Else
                txtFechaInicio.Text = Format(Now.Date(), "yyyy-MM-dd")
                txtFechaFin.Text = Format(Now.Date(), "yyyy-MM-dd")
            End If
        End If
    End Sub

    Private Sub BoundTipoDocRet(TipoDocRet As AdminTablasReferencia)
        txtCodigo.Text = TipoDocRet.Codigo
        txtOficina.DataBind()
        txtOficina.SelectedValue = TipoDocRet.oficina
        txtArea.Text = TipoDocRet.entidad
        txtResponsable.Text = TipoDocRet.responsable
        txtFechaInicio.Text = Format(TipoDocRet.fechainicio, "yyyy-MM-dd")
        txtFechaFin.Text = Format(TipoDocRet.fechafin, "yyyy-MM-dd")
        Chkactivo.Checked = TipoDocRet.Activo
        If TipoDocRet.id_area IsNot Nothing Then
            Hdd_id_area.Value = TipoDocRet.id_area
        End If

    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        If Hdd_id_area.Value <> 0 Then
            If id_tablaret = -1 Then
                TipoDocRet = New AdminTablasReferencia
                TipoDocRet.id_cliente = Session("id_cliente")
                TipoDocRet.id_sucursal = CLng(Session("id_sucursal"))
                TipoDocRet.usuario = User.Identity.Name
                TipoDocRet.fecha = Now()
                db.AdminTablasReferencia.Add(TipoDocRet)
            Else
                TipoDocRet = (From tr In db.AdminTablasReferencia
                              Where tr.id = id_tablaret
                              Select tr).FirstOrDefault
            End If

            If TipoDocRet IsNot Nothing Then
                TipoDocRet.Codigo = txtCodigo.Text
                TipoDocRet.oficina = txtOficina.SelectedValue
                TipoDocRet.entidad = txtArea.Text
                TipoDocRet.responsable = txtResponsable.Text
                TipoDocRet.Activo = Chkactivo.Checked
                TipoDocRet.fechainicio = txtFechaInicio.Text
                TipoDocRet.fechafin = txtFechaFin.Text
                TipoDocRet.id_area = Hdd_id_area.Value
            End If

            db.SaveChanges()
            Dim ids As Long
            If Session("id_sucursal") IsNot Nothing Then
                ids = Session("id_sucursal")
            Else
                ids = 0
            End If
            If id_tablaret = -1 Then
                Utilidades.RegistrarAuditoria("Tabla de Retención", TipoDocRet.id, TipoDocRet.oficina, "Nuevo Registro", User.Identity.Name, ids)
            Else
                Utilidades.RegistrarAuditoria("Tabla de Retención", TipoDocRet.id, TipoDocRet.oficina, "Actualización Registro", User.Identity.Name, ids)
            End If
            Utilidades.Mensaje("Registro Tabla de Retención Guardado", Me)
            Response.Redirect("../Admin/Page_TablasReferencia.aspx")
        Else
            Utilidades.Mensaje("Tiene que asociar la Oficina, por favor revisar", Me)
        End If



    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_TablasReferencia.aspx")
    End Sub

    Protected Sub bttBuscarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarArea.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Area.aspx','SeleccionarArea','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub
End Class