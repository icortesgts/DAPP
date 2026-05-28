Public Class EditarTipoUbicacion
    Inherits System.Web.UI.Page


    Private TipoUbicacionEditado As AdminTipoUbicacion
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tipoubicacion As Long
        Get
            If Me.ClientQueryString.Contains("id_tipoubicacion") Then
                Return CLng(Request.QueryString.Get("id_tipoubicacion"))
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

            If id_tipoubicacion <> -1 Then
                TipoUbicacionEditado = (From tr In db.AdminTipoUbicacion
                                        Where tr.id = id_tipoubicacion
                                        Select tr).FirstOrDefault

                If TipoUbicacionEditado IsNot Nothing Then
                    BoundTipoUbicacion(TipoUbicacionEditado)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoubicacion(TipoUbicacionEditado As AdminTipoUbicacion)
        txtTipoDocumento.Text = TipoUbicacionEditado.nombre
        txtClase.SelectedValue = TipoUbicacionEditado.Clase
        txtAlto.Value = TipoUbicacionEditado.Alto
        txtAncho.Value = TipoUbicacionEditado.Ancho
        txtlargo.Value = TipoUbicacionEditado.Largo
        txtcapacidad.Value = TipoUbicacionEditado.Capacidad
        Chkactivo.Checked = TipoUbicacionEditado.Activo
        If TipoUbicacionEditado.id_tipoCapacidad IsNot Nothing Then
            txtcontiene.SelectedValue = TipoUbicacionEditado.id_tipoCapacidad
        Else
            txtcontiene.SelectedValue = 0
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim sx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_tipoubicacion = -1 Then
            TipoUbicacionEditado = New AdminTipoUbicacion
            TipoUbicacionEditado.id_cliente = Session("id_cliente")
            TipoUbicacionEditado.id_sucursal = CInt(Session("id_sucursal"))
            db.AdminTipoUbicacion.Add(TipoUbicacionEditado)
        Else
            TipoUbicacionEditado = (From tr In db.AdminTipoUbicacion
                                    Where tr.id = id_tipoubicacion
                                    Select tr).FirstOrDefault
        End If


        If TipoUbicacionEditado IsNot Nothing Then
            If id_tipoubicacion = -1 Then
                sx = (From tr In db.AdminTipoUbicacion
                      Where tr.nombre = txtTipoDocumento.Text And tr.id_sucursal = ids
                      Select tr).Count
            Else
                sx = 0
            End If
            If sx = 0 Then
                TipoUbicacionEditado.nombre = txtTipoDocumento.Text
                TipoUbicacionEditado.Clase = txtClase.SelectedValue
                TipoUbicacionEditado.Alto = CInt(txtAlto.Value)
                TipoUbicacionEditado.Ancho = CInt(txtAncho.Value)
                TipoUbicacionEditado.Largo = CInt(txtlargo.Value)
                TipoUbicacionEditado.Capacidad = CInt(txtcapacidad.Value)
                TipoUbicacionEditado.Activo = Chkactivo.Checked
                If txtcontiene.SelectedValue <> 0 Then
                    TipoUbicacionEditado.id_tipoCapacidad = txtcontiene.SelectedValue
                End If
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_tipoubicacion = -1 Then
                    Utilidades.RegistrarAuditoria("Tipo Ubicacion", TipoUbicacionEditado.id, TipoUbicacionEditado.nombre, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Tipo Ubicacion", TipoUbicacionEditado.id, TipoUbicacionEditado.nombre, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Tipo Ubicación Guardado", Me)
                Response.Redirect("../Admin/Page_TiposUbicacion.aspx")
            Else
                Utilidades.Mensaje("Tipo Ubicación ya Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_TiposUbicacion.aspx")
    End Sub

    Protected Sub BttCapacidad_Click(sender As Object, e As EventArgs) Handles BttCapacidad.Click
        Dim x As Integer
        If txtmetodo.Text = "Profundidad" Then
            If txtcontiene.SelectedValue <> 0 Then
                Dim tpx = (From tp In db.AdminTipoUbicacion
                           Where tp.id = txtcontiene.SelectedValue
                           Select tp).FirstOrDefault
                If tpx IsNot Nothing Then
                    If tpx.Ancho <= txtAncho.Value And tpx.Alto <= txtAlto.Value Then
                        x = CInt(txtlargo.Value / tpx.Largo)
                        Lblcalculo.Text = "Cálculo Ok"
                    Else
                        x = 0
                        Lblcalculo.Text = "Revise Alto y/o Ancho."
                    End If
                Else
                    x = 0
                End If
            Else
                If txtlargo.Value = 0 Then
                    x = 0
                Else
                    x = CInt(txtlargo.Value * 62.5)
                    Lblcalculo.Text = "Cálculo Ok, basado en Documentos"
                End If

            End If
        ElseIf txtmetodo.Text = "Ancho" Then
            If txtcontiene.SelectedValue <> 0 Then
                Dim tpx = (From tp In db.AdminTipoUbicacion
                           Where tp.id = txtcontiene.SelectedValue
                           Select tp).FirstOrDefault
                If tpx IsNot Nothing Then
                    If tpx.Largo <= txtlargo.Value And tpx.Alto <= txtAlto.Value Then
                        x = CInt(txtAncho.Value / tpx.Ancho)
                        Lblcalculo.Text = "Cálculo Ok"
                    Else
                        x = 0
                        Lblcalculo.Text = "Revise Alto y/o Profundidad."
                    End If
                Else
                    x = 0
                End If
            Else
                If txtAncho.Value = 0 Then
                    x = 0
                Else
                    x = CInt(txtAncho.Value * 62.5)
                    Lblcalculo.Text = "Cálculo Ok, basado en Documentos"
                End If

            End If
        Else
            If txtcontiene.SelectedValue <> 0 Then
                Dim tpx = (From tp In db.AdminTipoUbicacion
                           Where tp.id = txtcontiene.SelectedValue
                           Select tp).FirstOrDefault
                If tpx IsNot Nothing Then
                    If tpx.Largo <= txtlargo.Value And tpx.Ancho <= txtAncho.Value Then
                        x = CInt(txtAlto.Value / tpx.Alto)
                        Lblcalculo.Text = "Cálculo Ok"
                    Else
                        x = 0
                        Lblcalculo.Text = "Revise Ancho y/o Profundidad."
                    End If
                Else
                    x = 0
                End If
            Else
                If txtAlto.Value = 0 Then
                    x = 0
                Else
                    x = CInt(txtAlto.Value * 62.5)
                    Lblcalculo.Text = "Cálculo Ok, basado en Documentos"
                End If

            End If
        End If
        txtcapacidad.Value = x
    End Sub
End Class