Public Class EditarTercero
    Inherits System.Web.UI.Page


    Private TerceroEditado As Terceros
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tercero As Long
        Get
            If Me.ClientQueryString.Contains("id_tercero") Then
                Return CLng(Request.QueryString.Get("id_tercero"))
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

            If id_tercero <> -1 Then

                TerceroEditado = (From tr In db.Terceros
                                  Where tr.id = id_tercero
                                  Select tr).FirstOrDefault

                If TerceroEditado IsNot Nothing Then
                    BoundTercero(TerceroEditado)
                End If
            Else
                Hdd_id_area.Value = 0
                Hdd_id_tercero.Value = 0
                txttipo.DataBind()
                txttipo_SelectedIndexChanged(sender, e)
                txtpais.DataBind()
                txtpais.SelectedValue = 539
                txtDpto.DataBind()
                txtCiudad.DataBind()
            End If
        End If
    End Sub

    Private Sub BoundTercero(TerceroEditado As Terceros)
        txtCelular.Text = TerceroEditado.celular
        txtCorreo.Text = TerceroEditado.correo_electronico
        txtDireccion.Text = TerceroEditado.direccion
        txtNombre.Text = TerceroEditado.nombre
        txtNumDocumento.ValueLong = TerceroEditado.numero_documento
        txtTelefono.Text = TerceroEditado.telefono
        txtTipoDocumento.SelectedValue = TerceroEditado.tipo_documento
        txtSector.SelectedValue = TerceroEditado.sector
        txtActividad.SelectedValue = TerceroEditado.id_actividad
        txttipo.SelectedValue = TerceroEditado.id_tipo


        Dim city = (From cx In db.Admin_Ciudades.Include("AdminPais")
                    Where cx.codigo = TerceroEditado.id_ciudad
                    Select cx).FirstOrDefault
        If city IsNot Nothing Then
            txtpais.DataBind()
            txtpais.SelectedValue = city.id_pais
            txtDpto.DataBind()
            txtDpto.SelectedValue = city.departamento
            txtCiudad.DataBind()
            txtCiudad.SelectedValue = TerceroEditado.id_ciudad
        End If

        Chkactivo.Checked = TerceroEditado.Activo
        ChkFirma.Checked = TerceroEditado.firmas
        If TerceroEditado.archfirma IsNot Nothing Then
            txtcuantos.Text = TerceroEditado.archfirma
        Else

            txtcuantos.Text = ""
        End If
        If ChkFirma.Checked And txtcuantos.Text = "" Then
            Lblfirma.Visible = True
            flDocumentos.Visible = True
            Txtpwd.Visible = True
            Lblpwd.Visible = True
            BttFirma.Visible = False
            Lblvalida.Visible = False
            txtvalida.Visible = False
        ElseIf ChkFirma.Checked And txtcuantos.Text <> "" Then
            Lblfirma.Visible = False
            flDocumentos.Visible = False
            Txtpwd.Visible = False
            Lblpwd.Visible = False
            BttFirma.Visible = True
            Lblvalida.Visible = True
            txtvalida.Visible = True
        ElseIf ChkFirma.Checked = False Then
            Lblfirma.Visible = False
            flDocumentos.Visible = False
            Txtpwd.Visible = False
            Lblpwd.Visible = False
            BttFirma.Visible = False
            Lblvalida.Visible = False
            txtvalida.Visible = False
        End If

        Dim tx = (From tpx In db.AdminTipoTercero
                  Where tpx.Id = TerceroEditado.id_tipo
                  Select tpx).FirstOrDefault
        If tx IsNot Nothing Then
            If tx.interno IsNot Nothing And tx.interno Then
                txtArea.Visible = True
                lbArea.Visible = True
                txtTercero.Visible = True
                lbDoc.Visible = True
                bttBuscarArea.Visible = True
                bttBuscarTercero.Visible = True
                txtcargo.Visible = True
                LblCargo.Visible = True
                If TerceroEditado.id_area IsNot Nothing Then
                    Dim arx = (From ar In db.Areas
                               Where ar.id = TerceroEditado.id_area
                               Select ar).FirstOrDefault
                    If arx IsNot Nothing Then
                        Hdd_id_area.Value = TerceroEditado.id_area
                        txtArea.Text = arx.nombre
                    End If

                End If
                If TerceroEditado.id_jefe IsNot Nothing Then
                    Dim trx = (From tr In db.Terceros
                               Where tr.id = TerceroEditado.id_jefe
                               Select tr).FirstOrDefault
                    If trx IsNot Nothing Then
                        Hdd_id_tercero.Value = TerceroEditado.id_jefe
                        txtTercero.Text = trx.nombre
                    End If
                End If
                If TerceroEditado.cargo IsNot Nothing Then
                    txtcargo.Text = TerceroEditado.cargo
                End If
            Else
                txtArea.Visible = False
                lbArea.Visible = False
                txtTercero.Visible = False
                lbDoc.Visible = False
                bttBuscarArea.Visible = False
                bttBuscarTercero.Visible = False
                txtcargo.Visible = False
                LblCargo.Visible = False
            End If
            If tx.proveedor IsNot Nothing And tx.proveedor Then
                lblmensajeria.Visible = True
                Chkmensajeria.Visible = True
                If TerceroEditado.mensajeria IsNot Nothing Then
                    Chkmensajeria.Checked = TerceroEditado.mensajeria
                End If
            Else
                lblmensajeria.Visible = False
                Chkmensajeria.Visible = False
            End If
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim Cuantos As Integer
        Dim ids As Long
        Dim cadena As String
        Dim extension As String

        Dim aux As Integer

        ids = CLng(Session("id_sucursal"))
        If id_tercero = -1 Then
            TerceroEditado = New Terceros
            TerceroEditado.id_cliente = CLng(Session("id_cliente"))
            TerceroEditado.id_sucursal = CLng(Session("id_sucursal"))
            TerceroEditado.fecha_creacion = Today
            db.Terceros.Add(TerceroEditado)
        Else
            TerceroEditado = (From tr In db.Terceros
                              Where tr.id = id_tercero
                              Select tr).FirstOrDefault
        End If

        If Not UtilityDB_Common.IsValidEmail(txtCorreo.Text) Then
            cvPagina.ErrorMessage = "El correo ingresado es invalido."
            cvPagina.IsValid = False
            Return
        End If

        If id_tercero = -1 Then
            Cuantos = (From tr In db.Terceros
                       Where tr.numero_documento = txtNumDocumento.Text And tr.id_sucursal = ids
                       Select tr).Count
            If Cuantos > 0 Then
                cvPagina.ErrorMessage = "Este tercero ya se encuentra registrado en el sistema."
                cvPagina.IsValid = False
                Return
            End If
        End If

        If flDocumentos.HasFile Then
            If Txtpwd.Text = "" Then
                cvPagina.ErrorMessage = "Tiene que definir una contraseña para el archivo."
                cvPagina.IsValid = False
                Return
            End If
        End If

        If TerceroEditado IsNot Nothing Then
            If txtNumDocumento.Text <> TerceroEditado.numero_documento Then
                Cuantos = (From tr In db.Terceros
                           Where tr.numero_documento = txtNumDocumento.Text And tr.id_sucursal = ids
                           Select tr).Count
            End If
            If Cuantos = 0 Then
                TerceroEditado.celular = txtCelular.Text
                TerceroEditado.correo_electronico = txtCorreo.Text
                TerceroEditado.direccion = txtDireccion.Text
                TerceroEditado.nombre = txtNombre.Text
                TerceroEditado.numero_documento = txtNumDocumento.ValueLong
                TerceroEditado.telefono = txtTelefono.Text
                TerceroEditado.tipo_documento = txtTipoDocumento.SelectedValue
                TerceroEditado.usuario = User.Identity.Name
                TerceroEditado.Activo = Chkactivo.Checked
                TerceroEditado.id_actividad = CLng(txtActividad.SelectedValue)
                TerceroEditado.id_tipo = CLng(txttipo.SelectedValue)
                TerceroEditado.sector = txtSector.SelectedValue
                TerceroEditado.id_ciudad = txtCiudad.SelectedValue
                TerceroEditado.mensajeria = Chkmensajeria.Checked
                TerceroEditado.firmas = ChkFirma.Checked
                TerceroEditado.cargo = txtcargo.Text
                If ChkFirma.Checked = False Then
                    TerceroEditado.archfirma = ""
                    TerceroEditado.pwdfirma = ""
                    Txtpwd.Text = ""
                End If
                TerceroEditado.pwdfirma = Txtpwd.Text
                If Hdd_id_area.Value = 0 Or txtArea.Text = "" Then
                    TerceroEditado.id_area = Nothing
                Else
                    TerceroEditado.id_area = Hdd_id_area.Value
                End If

                If Hdd_id_tercero.Value = 0 Or txtTercero.Text = "" Then
                    TerceroEditado.id_jefe = Nothing
                Else
                    TerceroEditado.id_jefe = Hdd_id_tercero.Value
                End If
            Else
                Utilidades.Mensaje("Este tercero ya se encuentra registrado en el sistema, no puede modificar el Nro de Documento", Me)
            End If

        End If

        db.SaveChanges()

        If flDocumentos.HasFile Then
            extension = flDocumentos.FileName
            aux = InStrRev(extension, ".")
            extension = Mid(extension, aux + 1)
            cadena = "~/Archivos/ArchFirma" & TerceroEditado.id & "." & extension
            Dim filePath As String =
                    Server.MapPath(cadena)
            flDocumentos.SaveAs(filePath)
            TerceroEditado.archfirma = "ArchFirma" & TerceroEditado.id & "." & extension
            db.SaveChanges()
        End If

        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_tercero = -1 Then
            Utilidades.RegistrarAuditoria("Terceros", TerceroEditado.id, TerceroEditado.nombre, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Terceros", TerceroEditado.id, TerceroEditado.nombre, "Actualización Registro", User.Identity.Name, ids)
        End If
        Utilidades.Mensaje("Tercero Guardado", Me)

        Response.Redirect("../Admin/Page_Terceros.aspx")



    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Terceros.aspx")
    End Sub

    Protected Sub txttipo_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txttipo.SelectedIndexChanged
        If txttipo.Items.Count > 0 Then
            Dim tx = (From tpx In db.AdminTipoTercero
                      Where tpx.Id = txttipo.SelectedValue
                      Select tpx).FirstOrDefault
            If tx IsNot Nothing Then
                If tx.interno IsNot Nothing And tx.interno Then
                    txtArea.Visible = True
                    lbArea.Visible = True
                    txtTercero.Visible = True
                    lbDoc.Visible = True
                Else
                    txtArea.Visible = False
                    lbArea.Visible = False
                    txtTercero.Visible = False
                    lbDoc.Visible = False
                End If
                If tx.proveedor IsNot Nothing And tx.proveedor Then
                    lblmensajeria.Visible = True
                    Chkmensajeria.Visible = True
                Else
                    lblmensajeria.Visible = False
                    Chkmensajeria.Visible = False
                End If
            End If
        End If

    End Sub

    Protected Sub bttBuscarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarArea.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Area.aspx','SeleccionarArea','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttBuscarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarTercero.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub ChkFirma_CheckedChanged(sender As Object, e As EventArgs) Handles ChkFirma.CheckedChanged
        If ChkFirma.Checked And txtcuantos.Text = "" Then
            Lblfirma.Visible = True
            flDocumentos.Visible = True
            Txtpwd.Visible = True
            Lblpwd.Visible = True
            BttFirma.Visible = False
            Lblvalida.Visible = False
            txtvalida.Visible = False
        ElseIf ChkFirma.Checked And txtcuantos.Text <> "" Then
            Lblfirma.Visible = False
            flDocumentos.Visible = False
            Txtpwd.Visible = False
            Lblpwd.Visible = False
            BttFirma.Visible = True
            Lblvalida.Visible = True
            txtvalida.Visible = True
        ElseIf ChkFirma.Checked = False Then
            Lblfirma.Visible = False
            flDocumentos.Visible = False
            Txtpwd.Visible = False
            Lblpwd.Visible = False
            BttFirma.Visible = False
            Lblvalida.Visible = False
            txtvalida.Visible = False
        End If

    End Sub

    Protected Sub BttFirma_Click(sender As Object, e As EventArgs) Handles BttFirma.Click
        TerceroEditado = (From tr In db.Terceros
                          Where tr.id = id_tercero
                          Select tr).FirstOrDefault

        If TerceroEditado IsNot Nothing Then
            If txtvalida.Text = "" Then
                Utilidades.Mensaje("Tiene que introducir una contraseña", Me)
            ElseIf txtvalida.Text <> TerceroEditado.pwdfirma Then
                Utilidades.Mensaje("Contraseña Invalida", Me)
            Else
                Dim lnk As String
                lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/" & txtcuantos.Text
                Utilidades.RegistrarScript("window.open('" & lnk & "','Documento','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
            End If
        End If


    End Sub

    Protected Sub txtpais_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtpais.SelectedIndexChanged
        txtDpto.DataBind()
    End Sub

    Protected Sub txtDpto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtDpto.SelectedIndexChanged
        txtCiudad.DataBind()
    End Sub
End Class