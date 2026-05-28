Imports System.Data.SqlClient
Imports Microsoft.AspNet.Identity


Public Class SiteMaster

    Inherits MasterPage

    Private db As New DAPP_BDEntities
    Private Const AntiXsrfTokenKey As String = "__AntiXsrfToken"
    Private Const AntiXsrfUserNameKey As String = "__AntiXsrfUserName"
    Private _antiXsrfTokenValue As String
    Public ReadOnly Property cliente As Long
        Get
            If txtCliente.Items.Count > 0 Then
                Return CLng(txtCliente.SelectedValue)
            Else
                Return -1
            End If

        End Get
    End Property
    Public ReadOnly Property sucursal As Long
        Get
            If txtsucursal.Items.Count > 0 Then
                Return CLng(txtsucursal.SelectedValue)
            Else
                Return -1
            End If

        End Get
    End Property
    Protected Sub Page_Init(sender As Object, e As EventArgs)
        ' The code below helps to protect against XSRF attacks
        Dim requestCookie = Request.Cookies(AntiXsrfTokenKey)
        Dim requestCookieGuidValue As Guid
        If requestCookie IsNot Nothing AndAlso Guid.TryParse(requestCookie.Value, requestCookieGuidValue) Then
            ' Use the Anti-XSRF token from the cookie
            _antiXsrfTokenValue = requestCookie.Value
            Page.ViewStateUserKey = _antiXsrfTokenValue
        Else
            ' Generate a new Anti-XSRF token and save to the cookie
            _antiXsrfTokenValue = Guid.NewGuid().ToString("N")
            Page.ViewStateUserKey = _antiXsrfTokenValue

            Dim responseCookie = New HttpCookie(AntiXsrfTokenKey) With {
                 .HttpOnly = True,
                 .Value = _antiXsrfTokenValue
            }
            If FormsAuthentication.RequireSSL AndAlso Request.IsSecureConnection Then
                responseCookie.Secure = True
            End If
            Response.Cookies.[Set](responseCookie)
        End If

        AddHandler Page.PreLoad, AddressOf master_Page_PreLoad
    End Sub

    Protected Sub master_Page_PreLoad(sender As Object, e As EventArgs)
        If Not IsPostBack Then
            ' Set Anti-XSRF token
            ViewState(AntiXsrfTokenKey) = Page.ViewStateUserKey
            ViewState(AntiXsrfUserNameKey) = If(Context.User.Identity.Name, [String].Empty)

        Else
            ' Validate the Anti-XSRF token
            If DirectCast(ViewState(AntiXsrfTokenKey), String) <> _antiXsrfTokenValue OrElse DirectCast(ViewState(AntiXsrfUserNameKey), String) <> (If(Context.User.Identity.Name, [String].Empty)) Then
                Throw New InvalidOperationException("Validation of Anti-XSRF token failed.")
            End If
        End If
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim lnk As String
        lnk = ConfigurationManager.AppSettings("URLBase").ToString

        If Not IsPostBack Then
            If Session("Usuario") Is Nothing Or Session("Usuario") = "" Then
                Session("Usuario") = HttpContext.Current.User.Identity.Name
            End If
            If Session("id_sucursal") Is Nothing Then
                txtCliente.DataBind()
                txtCliente.SelectedIndex = 0
                Session("id_cliente") = txtCliente.SelectedValue
                DfGen.NavigateUrl = lnk & "Default.aspx?" & txtCliente.SelectedValue
                txtsucursal.DataBind()
                If txtsucursal.Items.Count > 0 Then
                    txtsucursal.SelectedIndex = 0
                    Session("id_sucursal") = txtsucursal.SelectedValue
                End If
            Else
                If Session("id_cliente") Is Nothing Then
                    txtCliente.DataBind()
                    txtCliente.SelectedIndex = 0
                    Session("id_cliente") = txtCliente.SelectedValue
                    DfGen.NavigateUrl = lnk & "Default.aspx?" & txtCliente.SelectedValue
                End If
                txtCliente.DataBind()
                txtCliente.SelectedValue = Session("id_cliente")
                DfGen.NavigateUrl = lnk & "Default.aspx?" & txtCliente.SelectedValue
                txtsucursal.DataBind()
                txtsucursal.SelectedValue = Session("id_sucursal")
            End If
            cargar_menu()
        End If
        If Session("id_cliente") IsNot Nothing Then
            Dim ids As Long = CLng(Session("id_cliente"))
            Dim cl = (From cx In db.Clientes
                      Where cx.id = ids
                      Select cx).FirstOrDefault
            If cl IsNot Nothing Then

                lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                imglogo.ImageUrl = lnk & cl.Logo
            End If
            DfGen.NavigateUrl = lnk & "Default.aspx?" & Session("id_cliente")
            cargar_menu()
        End If

        Dim idp As Long
        Dim usr As String
        usr = HttpContext.Current.User.Identity.Name
        idp = Utilidades.ConsultarUsuario(usr).id_perfil

        Dim perf = (From px In db.AdminPerfiles.Include("Perfil_menu")
                    Where px.ID = idp
                    Select px).FirstOrDefault
        If perf IsNot Nothing Then
            For Each mn In perf.Perfil_Menu
                If mn.id_menu = 30 Then
                    Dim arx = (From ax In db.Alerta_Archivo(0)).FirstOrDefault
                    If txtsucursal.SelectedValue IsNot Nothing And txtsucursal.SelectedValue <> "" Then
                        arx = (From ax In db.Alerta_Archivo(txtsucursal.SelectedValue)).FirstOrDefault
                    End If


                    If arx IsNot Nothing Then
                        LblArchivo.Text = "Archivos (" & arx.Value & ")"
                    Else
                        LblArchivo.Text = "Archivos (0)"
                    End If
                End If
                If mn.id_menu = 31 Then
                    Dim arx = (From ax In db.Alerta_Listas(0)).FirstOrDefault
                    If txtCliente.SelectedValue IsNot Nothing And txtCliente.SelectedValue <> "" Then
                        arx = (From ax In db.Alerta_Listas(txtCliente.SelectedValue)).FirstOrDefault
                    End If

                    If arx IsNot Nothing Then
                        lblListas.Text = "Listas de Chequeo (" & arx.Value & ")"
                    Else
                        lblListas.Text = "Listas de Chequeo (0)"
                    End If
                End If
                If mn.id_menu = 33 Then
                    Dim arx = (From ax In db.Alerta_prestamo(0)).FirstOrDefault
                    If txtCliente.SelectedValue IsNot Nothing And txtCliente.SelectedValue <> "" Then
                        arx = (From ax In db.Alerta_prestamo(txtCliente.SelectedValue)).FirstOrDefault
                    End If

                    If arx IsNot Nothing Then
                        lblPrestamo.Text = "Prestamos (" & arx.Value & ")"
                    Else
                        lblPrestamo.Text = "Prestamos (0)"
                    End If
                End If
                If mn.id_menu = 26 Then
                    Dim trx As Long
                    If Utilidades.ConsultarUsuario(usr).correspondencia IsNot Nothing Then
                        If Utilidades.ConsultarUsuario(usr).correspondencia Then
                            If Utilidades.ConsultarUsuario(usr).id_tercero IsNot Nothing Then
                                trx = Utilidades.ConsultarUsuario(usr).id_tercero
                            Else
                                If Utilidades.ConsultarUsuario(usr).id_perfil = 1 Then
                                    trx = 0
                                Else
                                    trx = -1
                                End If
                            End If
                        Else
                            If Utilidades.ConsultarUsuario(usr).id_tercero IsNot Nothing Then
                                trx = Utilidades.ConsultarUsuario(usr).id_tercero
                            Else
                                trx = -1
                            End If
                        End If
                    Else
                        If Utilidades.ConsultarUsuario(usr).id_tercero IsNot Nothing Then
                            trx = Utilidades.ConsultarUsuario(usr).id_tercero
                        Else
                            trx = -1
                        End If
                    End If

                    Dim arx = (From ax In db.Alerta_correspondencia(trx)).FirstOrDefault
                    If arx IsNot Nothing Then
                        LblCorrespondencia.Text = "Correspondencia (" & arx.Value & ")"
                    Else
                        LblCorrespondencia.Text = "Correspondencia (0)"
                    End If
                End If
            Next
        Else
            LblArchivo.Text = "Archivos (0)"
            LblCorrespondencia.Text = "Correspondencia (0)"
            lblListas.Text = "Listas de Chequeo (0)"
            lblPrestamo.Text = "Prestamos (0)"
        End If

        CargarTareasWorkflow()

    End Sub

    Private Sub CargarTareasWorkflow()

        Dim cnn As New SqlConnection(ConfigurationManager.ConnectionStrings("DefaultConnectionWF").ToString())
        cnn.Open()

        Dim adp As New SqlDataAdapter("", cnn)
        adp.SelectCommand.CommandText = "SELECT ISNULL(COUNT(*), 0 ) as Cuentos FROM [Solicitudes] WHERE responsable_ = @usuario AND [esCerrado_] = 'FALSE' "
        adp.SelectCommand.CommandType = CommandType.Text

        'Asignacion de parámetros
        adp.SelectCommand.Parameters.Add("@usuario", SqlDbType.VarChar).Value = HttpContext.Current.User.Identity.Name

        ' Execute
        Dim cuantos As Long = adp.SelectCommand.ExecuteScalar

        cnn.Close()

        lbTareas.Text = lbTareas.Text & " (" & cuantos.ToString & ")"

    End Sub

    Protected Sub Unnamed_LoggingOut(sender As Object, e As LoginCancelEventArgs)
        Context.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie)
    End Sub



    Protected Sub txtCliente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtCliente.SelectedIndexChanged
        Session("id_cliente") = txtCliente.SelectedValue

        txtsucursal.DataBind()

        If txtsucursal.Items.Count > 0 Then
            txtsucursal.SelectedIndex = 0
            Session("id_sucursal") = txtsucursal.SelectedValue
        End If
        If Session("id_cliente") IsNot Nothing Then
            Dim ids As Long = CLng(Session("id_cliente"))
            Dim cl = (From cx In db.Clientes
                      Where cx.id = ids
                      Select cx).FirstOrDefault
            If cl IsNot Nothing Then
                Dim lnk As String
                lnk = ConfigurationManager.AppSettings("URLBase").ToString
                DfGen.NavigateUrl = lnk & "Default.aspx?" & txtCliente.SelectedValue
                lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                imglogo.ImageUrl = lnk & cl.Logo
            End If
        End If
    End Sub

    Protected Sub txtsucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtsucursal.SelectedIndexChanged
        Session("id_sucursal") = txtsucursal.SelectedValue
        If Session("id_cliente") IsNot Nothing Then
            Dim ids As Long = CLng(Session("id_cliente"))
            Dim cl = (From cx In db.Clientes
                      Where cx.id = ids
                      Select cx).FirstOrDefault
            If cl IsNot Nothing Then
                Dim lnk As String
                lnk = ConfigurationManager.AppSettings("URLBase").ToString
                DfGen.NavigateUrl = lnk & "Default.aspx?" & txtCliente.SelectedValue
                lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                imglogo.ImageUrl = lnk & cl.Logo
            End If
        End If
    End Sub
    Protected Sub cargar_menu()
        'Cargar Menu
        Dim lnk As String
        Dim idc As Long
        idc = Session("id_cliente")
        lnk = ConfigurationManager.AppSettings("URLBase").ToString
        Dim pm As List(Of Perfil_Menu)
        Dim pz As List(Of Perfil_Menu)
        Dim id_perfil As Long
        id_perfil = Utilidades.ConsultarUsuario(HttpContext.Current.User.Identity.Name).id_perfil
        Dim aliasm As Admin_AliasMenu
        Dim strMenu As New StringBuilder
        Dim cadena As String
        pm = (From px In db.Perfil_Menu.Include("Admin_Menu")
              Where px.id_perfil = id_perfil And px.Admin_Menu.nivel = 0
              Select px).ToList
        For Each px As Perfil_Menu In pm
            '<li class="treeview">
            '<a href="#"><i class="fa fa-dashboard"></i> <span>Administracion</span> <i class="fa fa-angle-left pull-right"></i></a>
            cadena = "<li class="
            cadena = cadena & Chr(34)
            cadena = cadena & "treeview"
            cadena = cadena & Chr(34)
            cadena = cadena & ">"
            strMenu.Append(cadena)
            cadena = "<a href='"
            If px.id_menu = 1 Then
                cadena = cadena & px.Admin_Menu.link & "'><i class="
                cadena = cadena & Chr(34)
                cadena = cadena & "fa fa-dashboard"
                cadena = cadena & Chr(34)
                cadena = cadena & "></i> <span>"
                strMenu.Append(cadena)
            ElseIf px.id_menu = 2 Then
                cadena = cadena & px.Admin_Menu.link & "'><i class="
                cadena = cadena & Chr(34)
                cadena = cadena & "fa fa-book"
                cadena = cadena & Chr(34)
                cadena = cadena & "></i> <span>"
                strMenu.Append(cadena)

            Else
                cadena = cadena & px.Admin_Menu.link & "'><i class="
                cadena = cadena & Chr(34)
                cadena = cadena & "fa fa-envelope"
                cadena = cadena & Chr(34)
                cadena = cadena & "></i> <span>"
                strMenu.Append(cadena)

            End If
            aliasm = (From am In db.Admin_AliasMenu
                      Where am.id_menu = px.id_menu And am.id_cliente = idc
                      Select am).FirstOrDefault
            If aliasm IsNot Nothing Then
                strMenu.Append(aliasm.Alias)
            End If
            cadena = "</span> <i class="
            cadena = cadena & Chr(34)
            cadena = cadena & "fa fa-angle-left pull-right"
            cadena = cadena & Chr(34)
            cadena = cadena & "></i>"
            cadena = cadena & "</a>"
            strMenu.Append(cadena)

            ' <ul class="treeview-menu">
            cadena = "<ul class="
            cadena = cadena & Chr(34)
            cadena = cadena & "treeview-menu"
            cadena = cadena & Chr(34)
            cadena = cadena & ">"
            strMenu.Append(cadena)

            pz = (From py In db.Perfil_Menu.Include("Admin_Menu")
                  Where py.id_perfil = id_perfil And py.Admin_Menu.nivel > 0 And py.Admin_Menu.id_menu = px.id_menu
                  Select py).ToList
            For Each py As Perfil_Menu In pz
                '<li><a href="<%=ResolveClientUrl("~/Vistas/Admin/Page_Areas")%>">Areas</a></li>     
                strMenu.Append("<li><a href='")
                If py.Admin_Menu.link <> "#" Then
                    strMenu.Append(lnk & py.Admin_Menu.link)
                Else
                    strMenu.Append(py.Admin_Menu.link)
                End If
                aliasm = (From am In db.Admin_AliasMenu
                          Where am.id_menu = py.id_menu And am.id_cliente = idc
                          Select am).FirstOrDefault
                strMenu.Append("' title='")
                strMenu.Append(aliasm.Alias)
                strMenu.Append("'>")
                strMenu.Append(aliasm.Alias)
                strMenu.Append("</a></li>")
            Next
            cadena = "</ul>"
            strMenu.Append(cadena)
            cadena = "</li>"
            strMenu.Append(cadena)
        Next


        MenuBBC.InnerHtml = strMenu.ToString
    End Sub
End Class