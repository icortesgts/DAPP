Public Class Editarinfo
    Inherits System.Web.UI.Page


    Private ContactoEditado As AdminContacto
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_usuario As Long
        Get
            If Me.ClientQueryString.Contains("id_usuario") Then
                Return CLng(Request.QueryString.Get("id_usuario"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_cliente As Long
        Get
            If Me.ClientQueryString.Contains("id_cliente") Then
                Return CLng(Request.QueryString.Get("id_cliente"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property info As String
        Get
            If Me.ClientQueryString.Contains("info") Then
                Return Request.QueryString.Get("info")
            Else
                Return ""
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim cadena As String
        If Not Page.IsPostBack Then
            If info = "" Then
                Utilidades.Mensaje("Información no  Asociada", Me)
                Response.Redirect("../Admin/Page_Usuarios.aspx")
            Else
                If info = "Clientes" Then
                    SqlNombre.DataBind()
                    txtNombre.DataBind()
                    Lbltodos.Visible = True
                    Chktodos.Visible = True
                    If id_cliente <> -1 Then
                        Dim tx = (From uc In db.Usuario_Cliente
                                  Where uc.id = id_cliente
                                  Select uc).FirstOrDefault
                        If tx IsNot Nothing Then
                            cadena = "select id,nombre from clientes order by nombre"
                            SqlNombre.SelectCommand = cadena
                            SqlNombre.DataBind()
                            txtNombre.DataBind()
                            txtNombre.SelectedValue = tx.id_cliente
                            txtNombre.Enabled = False
                            Chktodos.Checked = tx.todas
                        End If
                    End If
                ElseIf info = "Sucursales" Then
                    cadena = "select id,sucursal as nombre from adminsucursal"
                    cadena = cadena & " where id_cliente=" & id_cliente & " and id not in (select id_sucursal from usuario_sucursal where id_usuario=" & id_usuario & ") order by sucursal"
                    SqlNombre.SelectCommand = cadena
                    SqlNombre.DataBind()
                    txtNombre.DataBind()
                ElseIf info = "Terceros" Then
                    cadena = "select id,nombre from terceros"
                    cadena = cadena & " where id not in (select id_tercero from usuarios_terceros where id_usuario=" & id_usuario & ") and id_sucursal=" & Session("id_sucursal") & " order by nombre"
                    SqlNombre.SelectCommand = cadena
                    SqlNombre.DataBind()
                    txtNombre.DataBind()
                Else
                    cadena = "select id,nombre from areas"
                    cadena = cadena & " where id not in (select id_area from usuarios_areas where id_usuario=" & id_usuario & ") and id_sucursal=" & Session("id_sucursal") & " order by nombre"
                    SqlNombre.SelectCommand = cadena
                    SqlNombre.DataBind()
                    txtNombre.DataBind()
                End If
            End If
        End If
    End Sub


    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        If info = "Clientes" Then

            If id_cliente <> -1 Then
                Dim tx = (From uc In db.Usuario_Cliente
                          Where uc.id = id_cliente
                          Select uc).FirstOrDefault
                If tx IsNot Nothing Then
                    tx.todas = Chktodos.Checked
                    db.SaveChanges()
                End If
            Else
                Dim data = New Usuario_Cliente
                data.id_usuario = id_usuario
                data.id_cliente = txtNombre.SelectedValue
                data.todas = Chktodos.Checked()
                db.Usuario_Cliente.Add(data)
                db.SaveChanges()
            End If
        ElseIf info = "Sucursales" Then
            Dim data = New Usuario_Sucursal
            data.id_usuario = id_usuario
            data.id_sucursal = txtNombre.SelectedValue
            db.Usuario_Sucursal.Add(data)
            db.SaveChanges()
        ElseIf info = "Terceros" Then
            Dim data = New Usuarios_Terceros
            data.id_usuario = id_usuario
            data.Id_tercero = txtNombre.SelectedValue
            db.Usuarios_Terceros.Add(data)
            db.SaveChanges()
        Else
            Dim data = New Usuarios_Areas
            data.id_usuario = id_usuario
            data.id_area = txtNombre.SelectedValue
            db.Usuarios_Areas.Add(data)
            db.SaveChanges()
        End If
        Utilidades.RegistrarScript(String.Format("window.opener.location.href = window.opener.location.href;window.close();"), Me)


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click

        Utilidades.RegistrarScript(String.Format("window.opener.location.href = window.opener.location.href;window.close();"), Me)

    End Sub

End Class