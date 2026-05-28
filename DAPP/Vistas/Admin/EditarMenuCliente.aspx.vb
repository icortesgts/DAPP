Public Class EditarMenuCliente
    Inherits System.Web.UI.Page


    Private MenuEditado As Admin_AliasMenu
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_menu As Long
        Get
            If Me.ClientQueryString.Contains("id_menu") Then
                Return CLng(Request.QueryString.Get("id_menu"))
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

            If id_menu <> -1 Then

                MenuEditado = (From ar In db.Admin_AliasMenu.Include("Admin_menu")
                               Where ar.id = id_menu
                               Select ar).FirstOrDefault

                If MenuEditado IsNot Nothing Then
                    BoundArea(MenuEditado)
                End If
            Else
                Utilidades.Mensaje("Opción no encontrada", Me)
                Response.Redirect("../Admin/Page_MenuCliente.aspx")
            End If
        End If
    End Sub

    Private Sub BoundArea(MenuEditado As Admin_AliasMenu)


        txtAlias.Text = MenuEditado.Alias
        TxtLabel.Text = MenuEditado.Admin_Menu.label


    End Sub



    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim ax As Integer
        Dim ids As Long
        Dim idc As Long
        idc = Session("id_cliente")
        MenuEditado = (From ar In db.Admin_AliasMenu
                       Where ar.id = id_menu
                       Select ar).FirstOrDefault

        If MenuEditado IsNot Nothing Then
            If id_menu = -1 Then
                ax = (From ar In db.Admin_AliasMenu
                      Where ar.Alias = txtAlias.Text And ar.id_cliente = idc
                      Select ar).Count
            Else
                ax = 0
            End If
            If ax = 0 Then
                MenuEditado.Alias = txtAlias.Text

                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_menu = -1 Then
                    Utilidades.RegistrarAuditoria("Admin_AliasMenu", MenuEditado.id, MenuEditado.Alias, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Admin_AliasMenu", MenuEditado.id, MenuEditado.Alias, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Menú Cliente Guardado", Me)
                Response.Redirect("../Admin/Page_MenuCliente.aspx")
            Else
                Utilidades.Mensaje("Alías Menú con el mismo nombre Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_MenuCliente.aspx")
    End Sub



End Class