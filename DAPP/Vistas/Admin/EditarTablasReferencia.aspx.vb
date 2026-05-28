Public Class EditarTablasReferencia
    Inherits System.Web.UI.Page


    Private TipoDocRet As TipoDoc_TablaRetencion
    Private db As New DAPP_BDEntities
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_tablaret As Long
        Get
            If Me.ClientQueryString.Contains("id_tablaret") Then
                Return CLng(Request.QueryString.Get("id_tablaret"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_tipodoc As Long
        Get
            If Me.ClientQueryString.Contains("id_tipodoc") Then
                Return CLng(Request.QueryString.Get("id_tipodoc"))
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
            If id_tablaret = -1 Then
                Utilidades.Mensaje("Asegurar Tabla de Retención", Me)
                Response.Redirect("../Admin/Page_TablasReferencia.aspx")
            End If
            If id_tipodoc <> -1 Then

                TipoDocRet = (From tr In db.TipoDoc_TablaRetencion
                              Where tr.id = id_tipodoc
                              Select tr).FirstOrDefault

                If TipoDocRet IsNot Nothing Then
                    BoundTipoDocRet(TipoDocRet)
                End If
            End If
        End If
    End Sub

    Private Sub BoundTipoDocRet(TipoDocRet As TipoDoc_TablaRetencion)
        txtCodigo.Text = TipoDocRet.Codigo
        txtProcedimiento.Text = TipoDocRet.procedimientos
        txtTipo.SelectedValue = TipoDocRet.id_tipo
        txtcentral.Value = TipoDocRet.central
        txthistorico.Value = TipoDocRet.historico
        txtgestion.Value = TipoDocRet.gestion
        txtTime.SelectedValue = TipoDocRet.tgestion
        txttime2.SelectedValue = TipoDocRet.tcentral
        txttime3.SelectedValue = TipoDocRet.thistorico
        ChkCT.Checked = TipoDocRet.CT
        ChkE.Checked = TipoDocRet.E
        ChkM.Checked = TipoDocRet.M
        ChkS.Checked = TipoDocRet.S
        ChkD.Checked = TipoDocRet.D
        ChkOriginal.Checked = TipoDocRet.Original
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click

        If ChkCT.Checked = False And
            ChkE.Checked = False And
            ChkM.Checked = False And
            ChkD.Checked = False And
            ChkS.Checked = False Then
            Utilidades.Mensaje("Tiene que elegir una opción de dipsosición final", Me)
        Else
            If id_tipodoc = -1 Then
                TipoDocRet = New TipoDoc_TablaRetencion
                TipoDocRet.id_cliente = Session("id_cliente")
                TipoDocRet.id_tablaret = id_tablaret
                db.TipoDoc_TablaRetencion.Add(TipoDocRet)
            Else
                TipoDocRet = (From tr In db.TipoDoc_TablaRetencion
                              Where tr.id_tablaret = id_tablaret
                              Select tr).FirstOrDefault
            End If

            If TipoDocRet IsNot Nothing Then
                TipoDocRet.Codigo = txtCodigo.Text
                TipoDocRet.Procedimientos = txtProcedimiento.Text
                TipoDocRet.TipoDocumento = txtTipo.Text
                TipoDocRet.id_tipo = txtTipo.SelectedValue
                TipoDocRet.gestion = CInt(txtgestion.Value)
                TipoDocRet.historico = CInt(txthistorico.Value)
                TipoDocRet.central = CInt(txtcentral.Value)
                TipoDocRet.tgestion = txtTime.SelectedValue
                TipoDocRet.tcentral = txttime2.SelectedValue
                TipoDocRet.thistorico = txttime3.SelectedValue
                TipoDocRet.CT = ChkCT.Checked
                TipoDocRet.E = ChkE.Checked
                TipoDocRet.M = ChkM.Checked
                TipoDocRet.S = ChkS.Checked
                TipoDocRet.D = ChkD.Checked
                TipoDocRet.Original = ChkOriginal.Checked
            End If

            db.SaveChanges()
            Dim ids As Long
            If Session("id_sucursal") IsNot Nothing Then
                ids = Session("id_sucursal")
            Else
                ids = 0
            End If
            If id_tipodoc = -1 Then
                Utilidades.RegistrarAuditoria("Serie Documenta", TipoDocRet.id, TipoDocRet.TipoDocumento, "Nuevo Registro", User.Identity.Name, ids)
            Else
                Utilidades.RegistrarAuditoria("Serie Documental", TipoDocRet.id, TipoDocRet.TipoDocumento, "Actualización Registro", User.Identity.Name, ids)
            End If
            Utilidades.Mensaje("Registro Tabla de Retención Guardado", Me)
            Response.Redirect("../Admin/Page_TablasReferencia.aspx")
        End If


    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../Admin/Page_TablasReferencia.aspx")
    End Sub


End Class