Public Class EditarMenu
    Inherits System.Web.UI.Page


    Private MenuEditado As Admin_Menu
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

                MenuEditado = (From ar In db.Admin_Menu
                               Where ar.id = id_menu
                               Select ar).FirstOrDefault

                If MenuEditado IsNot Nothing Then
                    BoundArea(MenuEditado)
                End If
            Else
                TxtNivel.Text = "0"
            End If
        End If
    End Sub

    Private Sub BoundArea(MenuEditado As Admin_Menu)


        txtItem.Text = MenuEditado.item
        TxtLabel.Text = MenuEditado.label
        TxtLink.Text = MenuEditado.link
        TxtNivel.Text = MenuEditado.nivel
        If MenuEditado.id_menu IsNot Nothing Then
            txtMenu.SelectedValue = MenuEditado.id_menu
        Else
            txtMenu.SelectedValue = 0
        End If


    End Sub



    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim ax As Integer
        Dim ids As Long
        If id_menu = -1 Then
            MenuEditado = New Admin_Menu
            ids = CLng(Session("id_sucursal"))
            db.Admin_Menu.Add(MenuEditado)
        Else
            MenuEditado = (From ar In db.Admin_Menu
                           Where ar.id = id_menu
                           Select ar).FirstOrDefault
        End If

        If MenuEditado IsNot Nothing Then
            If id_menu = -1 Then
                ax = (From ar In db.Admin_Menu
                      Where ar.label = TxtLabel.Text
                      Select ar).Count
            Else
                ax = 0
            End If
            If ax = 0 Then
                MenuEditado.item = txtItem.Text
                MenuEditado.label = TxtLabel.Text
                MenuEditado.link = TxtLink.Text
                MenuEditado.nivel = TxtNivel.Text
                If txtMenu.SelectedValue = 0 Then
                    MenuEditado.id_menu = Nothing
                Else
                    MenuEditado.id_menu = txtMenu.SelectedValue
                End If
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_menu = -1 Then
                    Utilidades.RegistrarAuditoria("Admin_Menu", MenuEditado.id, MenuEditado.label, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Admin_Menu", MenuEditado.id, MenuEditado.label, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Menú Guardado", Me)
                Response.Redirect("../Admin/Page_Menu.aspx")
            Else
                Utilidades.Mensaje("Menú con el mismo nombre Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_Menu.aspx")
    End Sub



    Protected Sub txtMenu_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtMenu.SelectedIndexChanged
        If txtMenu.SelectedValue = 0 Then
            TxtNivel.Text = "0"
        Else
            Dim mn = (From mnx In db.Admin_Menu
                      Where mnx.id = txtMenu.SelectedValue
                      Select mnx).FirstOrDefault
            If mn IsNot Nothing Then
                TxtNivel.Text = mn.nivel + 1
            End If
        End If
    End Sub
End Class