Public Class EditarUbicacion
    Inherits System.Web.UI.Page


    Private UbicacionEditada As Ubicaciones
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_ubicacion As Long
        Get
            If Me.ClientQueryString.Contains("id_ubicacion") Then
                Return CLng(Request.QueryString.Get("id_ubicacion"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            txttipo.DataBind()
            If txttipo.Items.Count = 0 Then
                Response.Redirect("../Admin/Page_Ubicaciones.aspx")
            Else
                Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
                If Usuario Is Nothing Then
                    Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
                End If

                If id_ubicacion <> -1 Then

                    UbicacionEditada = (From ub In db.Ubicaciones
                                        Where ub.id = id_ubicacion
                                        Select ub).FirstOrDefault

                    If UbicacionEditada IsNot Nothing Then
                        BoundUbicacion(UbicacionEditada)
                    End If
                End If
            End If

        End If
    End Sub

    Private Sub BoundUbicacion(UbicacionEditada As Ubicaciones)

        If UbicacionEditada.id_ubicacion Is Nothing Then
            txtUbicacion.Text = ""
            Hdd_id_ubicacion.Value = 0
        Else
            Dim UbicacionAsociada As Ubicaciones = (From ub In db.Ubicaciones
                                                    Where ub.id = UbicacionEditada.id_ubicacion
                                                    Select ub).FirstOrDefault
            If UbicacionAsociada IsNot Nothing Then
                txtUbicacion.Text = UbicacionAsociada.ubicacion
                Hdd_id_ubicacion.Value = UbicacionAsociada.id
            End If
        End If
        txtArchivo.SelectedValue = UbicacionEditada.Archivo
        txtDescripcion.Text = UbicacionEditada.descripcion
        txtNombre.Text = UbicacionEditada.ubicacion
        txtcapacidad.Value = UbicacionEditada.Capacidad
        txttipo.SelectedValue = UbicacionEditada.id_tipo
    End Sub



    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_ubicacion = -1 Then
            UbicacionEditada = New Ubicaciones
            UbicacionEditada.id_sucursal = Session("id_sucursal")
            db.Ubicaciones.Add(UbicacionEditada)
        Else
            UbicacionEditada = (From ub In db.Ubicaciones
                                Where ub.id = id_ubicacion
                                Select ub).FirstOrDefault
        End If

        If UbicacionEditada IsNot Nothing Then
            If id_ubicacion = -1 Then
                sx = (From ub In db.Ubicaciones
                      Where ub.ubicacion = txtNombre.Text And ub.id_sucursal = ids
                      Select ub).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                If Hdd_id_ubicacion.Value = 0 Then
                    UbicacionEditada.id_ubicacion = Nothing
                Else
                    UbicacionEditada.id_ubicacion = Hdd_id_ubicacion.Value
                End If
                UbicacionEditada.descripcion = txtDescripcion.Text
                UbicacionEditada.ubicacion = txtNombre.Text
                UbicacionEditada.Capacidad = CInt(txtcapacidad.Value)
                UbicacionEditada.id_tipo = txttipo.SelectedValue
                UbicacionEditada.Archivo = txtArchivo.SelectedValue
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_ubicacion = -1 Then
                    Utilidades.RegistrarAuditoria("Ubicación", UbicacionEditada.id, UbicacionEditada.ubicacion, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Ubicación", UbicacionEditada.id, UbicacionEditada.ubicacion, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Ubicación Guardada", Me)
                Response.Redirect("../Admin/Page_Ubicaciones.aspx")
            Else
                Utilidades.Mensaje("Ubicación ya Existente", Me)
            End If


        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Ubicaciones.aspx")
    End Sub

    Protected Sub bttBuscarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarUbicacion.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Ubicacion.aspx','SeleccionarUbicacion','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttQuitarSeleccion_Click(sender As Object, e As ImageClickEventArgs) Handles bttQuitarSeleccion.Click
        txtUbicacion.Text = ""
        Hdd_id_ubicacion.Value = 0
    End Sub

    Protected Sub txttipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txttipo.SelectedIndexChanged
        Dim tu = (From tpu In db.AdminTipoUbicacion
                  Where tpu.id = txttipo.SelectedValue
                  Select tpu).FirstOrDefault
        If tu IsNot Nothing Then
            txtcapacidad.Text = tu.Capacidad
        End If
    End Sub
End Class