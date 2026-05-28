Public Class EditarArea
    Inherits System.Web.UI.Page


    Private AreaEditada As Areas
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_area As Long
        Get
            If Me.ClientQueryString.Contains("id_area") Then
                Return CLng(Request.QueryString.Get("id_area"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not Page.IsPostBack Then
            Hdd_id_area.Value = 0
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_area <> -1 Then

                AreaEditada = (From ar In db.Areas
                               Where ar.id = id_area
                               Select ar).FirstOrDefault

                If AreaEditada IsNot Nothing Then
                    BoundArea(AreaEditada)
                End If
            Else
                txtcolor.Text = "#ffffff"
            End If
        End If
        If Hdd_id_area.Value = 0 Then
            txtcolor.Enabled = True
        Else
            Dim AreaAsociada As Areas = (From ar In db.Areas
                                         Where ar.id = Hdd_id_area.Value
                                         Select ar).FirstOrDefault
            If AreaAsociada IsNot Nothing Then
                txtArea.Text = AreaAsociada.nombre
                Hdd_id_area.Value = AreaAsociada.id
            End If
            txtcolor.Enabled = False
            txtcolor.Text = AreaAsociada.Color
        End If
    End Sub

    Private Sub BoundArea(AreaEditada As Areas)

        If AreaEditada.id_area Is Nothing Then
            txtArea.Text = ""
            Hdd_id_area.Value = 0
            txtcolor.Enabled = True
        Else
            Dim AreaAsociada As Areas = (From ar In db.Areas
                                         Where ar.id = AreaEditada.id_area
                                         Select ar).FirstOrDefault
            If AreaAsociada IsNot Nothing Then
                txtArea.Text = AreaAsociada.nombre
                Hdd_id_area.Value = AreaAsociada.id
            End If
            txtcolor.Enabled = False
            txtcolor.Text = AreaAsociada.Color
        End If
        txtprefijo.Text = AreaEditada.Prefijo
        txtDescripcion.Text = AreaEditada.descripcion
        txtNombre.Text = AreaEditada.nombre
        Chkactivo.Checked = AreaEditada.Activo
        If AreaEditada.Color IsNot Nothing Then
            txtcolor.Text = AreaEditada.Color
        End If
        If AreaEditada.Codigo IsNot Nothing Then
            txtCodigo.Text = AreaEditada.Codigo
        End If

    End Sub



    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim ax As Integer
        Dim ids As Long
        If id_area = -1 Then
            AreaEditada = New Areas
            AreaEditada.id_cliente = Session("id_cliente")
            AreaEditada.id_sucursal = CLng(Session("id_sucursal"))
            ids = CLng(Session("id_sucursal"))
            db.Areas.Add(AreaEditada)
        Else
            AreaEditada = (From ar In db.Areas
                           Where ar.id = id_area
                           Select ar).FirstOrDefault
        End If

        If AreaEditada IsNot Nothing Then
            If id_area = -1 Then
                ax = (From ar In db.Areas
                      Where ar.nombre = txtNombre.Text And ar.id_sucursal = ids
                      Select ar).Count
            Else
                ax = 0
            End If
            If ax = 0 Then
                If Hdd_id_area.Value = 0 Then
                    AreaEditada.id_area = Nothing
                Else
                    AreaEditada.id_area = Hdd_id_area.Value
                End If
                AreaEditada.Prefijo = txtprefijo.Text
                AreaEditada.descripcion = txtDescripcion.Text
                AreaEditada.nombre = txtNombre.Text
                AreaEditada.Activo = Chkactivo.Checked
                If txtcolor.Text <> "" Then
                    AreaEditada.Color = txtcolor.Text

                End If

                AreaEditada.Codigo = txtCodigo.Text
                db.SaveChanges()
                If txtcolor.Text <> "" Then
                    Utilidades.Callcolor(txtcolor.Text, AreaEditada.id)
                End If

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_area = -1 Then
                    Utilidades.RegistrarAuditoria("Areas", AreaEditada.id, AreaEditada.nombre, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Areas", AreaEditada.id, AreaEditada.nombre, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Area Guardada", Me)
                Response.Redirect("../Admin/Page_Areas.aspx")
            Else
                Utilidades.Mensaje("Area con el mismo nombre Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Areas.aspx")
    End Sub

    Protected Sub bttBuscarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarArea.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Area.aspx','SeleccionarArea','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttQuitarSeleccion_Click(sender As Object, e As ImageClickEventArgs) Handles bttQuitarSeleccion.Click
        txtArea.Text = ""
        Hdd_id_area.Value = 0
        txtcolor.Enabled = True
    End Sub


End Class