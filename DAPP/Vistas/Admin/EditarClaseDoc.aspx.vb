Public Class EditarClaseDoc
    Inherits System.Web.UI.Page


    Private ClaseDocEditado As AdminClaseDocumento
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_clasedoc As Long
        Get
            If Me.ClientQueryString.Contains("id_clasedoc") Then
                Return CLng(Request.QueryString.Get("id_clasedoc"))
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

            If id_clasedoc <> -1 Then
                ClaseDocEditado = (From tr In db.AdminClaseDocumento
                                   Where tr.id = id_clasedoc
                                   Select tr).FirstOrDefault

                If ClaseDocEditado IsNot Nothing Then
                    BoundTipoDoc(ClaseDocEditado)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(ClaseDocEditado As AdminClaseDocumento)
        txtTipoDocumento.Text = ClaseDocEditado.ClaseDoc
        txtDescipcion.Text = ClaseDocEditado.Descripcion
        Chkactivo.Checked = ClaseDocEditado.Activo
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim cx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_clasedoc = -1 Then
            ClaseDocEditado = New AdminClaseDocumento
            ClaseDocEditado.id_sucursal = CLng(Session("id_sucursal"))
            ClaseDocEditado.id_cliente = CLng(Session("id_cliente"))
            db.AdminClaseDocumento.Add(ClaseDocEditado)
        Else
            ClaseDocEditado = (From tr In db.AdminClaseDocumento
                               Where tr.id = id_clasedoc
                               Select tr).FirstOrDefault
        End If


        If ClaseDocEditado IsNot Nothing Then
            If id_clasedoc = -1 Then
                cx = (From tr In db.AdminClaseDocumento
                      Where tr.ClaseDoc = txtTipoDocumento.Text And tr.id_sucursal = ids
                      Select tr).Count
            Else
                cx = 0
            End If
            If cx = 0 Then
                ClaseDocEditado.ClaseDoc = txtTipoDocumento.Text
                ClaseDocEditado.Descripcion = txtDescipcion.Text
                ClaseDocEditado.Activo = Chkactivo.Checked
                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_clasedoc = -1 Then
                    Utilidades.RegistrarAuditoria("Clase Documentos", ClaseDocEditado.id, ClaseDocEditado.ClaseDoc, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Clase Documentos", ClaseDocEditado.id, ClaseDocEditado.ClaseDoc, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Clase Documento Guardado", Me)
                Response.Redirect("../Admin/Page_ClaseDocumento.aspx")
            Else
                Utilidades.Mensaje("Clase Documento ya Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_ClaseDocumento.aspx")
    End Sub

End Class