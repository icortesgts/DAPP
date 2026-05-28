Public Class EditarCamposAdicionales
    Inherits System.Web.UI.Page


    Private CampoEditado As AdminCamposAdicionales
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class
    Private ReadOnly Property id_tipo As Long
        Get
            If Me.ClientQueryString.Contains("id_tipo") Then
                Return CLng(Request.QueryString.Get("id_tipo"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_campo As Long
        Get
            If Me.ClientQueryString.Contains("id_campo") Then
                Return CLng(Request.QueryString.Get("id_campo"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim tipo = (From tpx In db.AdminTipoDocumento
                    Where tpx.id = id_tipo
                    Select tpx).FirstOrDefault
        If tipo IsNot Nothing Then
            LblTipo.Text = "Tipo Documento:" & tipo.nombre
        End If
        If Not Page.IsPostBack Then

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            If id_tipo = -1 Then
                Response.Redirect("../Admin/Page_TiposDocumento.aspx")
            Else
                If id_campo <> -1 Then
                    CampoEditado = (From tr In db.AdminCamposAdicionales
                                    Where tr.id = id_campo
                                    Select tr).FirstOrDefault

                    If CampoEditado IsNot Nothing Then
                        BoundTipoDoc(CampoEditado)
                    End If
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDoc(CampoEditado As AdminCamposAdicionales)
        txtTipoDocumento.Text = CampoEditado.Campo
        txtDescipcion.Text = CampoEditado.Descripcion

    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim cx As Integer
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        If id_campo = -1 Then
            CampoEditado = New AdminCamposAdicionales
            CampoEditado.id_tipo = id_tipo
            db.AdminCamposAdicionales.Add(CampoEditado)
        Else
            CampoEditado = (From tr In db.AdminCamposAdicionales
                            Where tr.id = id_campo
                            Select tr).FirstOrDefault
        End If


        If CampoEditado IsNot Nothing Then
            If id_campo = -1 Then
                cx = (From tr In db.AdminCamposAdicionales
                      Where tr.Campo = txtTipoDocumento.Text And tr.id_tipo = id_tipo
                      Select tr).Count
            Else
                cx = 0
            End If
            If cx = 0 Then
                CampoEditado.Campo = txtTipoDocumento.Text
                CampoEditado.Descripcion = txtDescipcion.Text

                db.SaveChanges()

                If Session("id_sucursal") IsNot Nothing Then
                    ids = Session("id_sucursal")
                Else
                    ids = 0
                End If
                If id_campo = -1 Then
                    Utilidades.RegistrarAuditoria("Campo Adicional", CampoEditado.id, CampoEditado.Campo, "Nuevo Registro", User.Identity.Name, ids)
                Else
                    Utilidades.RegistrarAuditoria("Campo Adicional", CampoEditado.id, CampoEditado.Campo, "Actualización Registro", User.Identity.Name, ids)
                End If
                Utilidades.Mensaje("Campo Adicional Guardado", Me)
                Response.Redirect("../Admin/EditarTipoDoc.aspx?id_tipodoc=" & id_tipo)
            Else
                Utilidades.Mensaje("Campo Adicional ya Existente", Me)
            End If

        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/EditarTipoDoc.aspx?id_tipodoc=" & id_tipo)
    End Sub

End Class