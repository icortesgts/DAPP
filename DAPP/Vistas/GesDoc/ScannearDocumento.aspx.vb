Imports System.IO
Public Class ScannearDocumento
    Inherits System.Web.UI.Page


    Private DocumentoEditado As Documentos
    Private db As New DAPP_BDEntities
    Private arch As Byte()
    Private ReadOnly Property AppGlobal As New AppGlobal_Class

    Private ReadOnly Property id_Documento As Long
        Get
            If Me.ClientQueryString.Contains("id_documento") Then
                Return CLng(Request.QueryString.Get("id_documento"))
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

            If id_Documento <> -1 Then

                DocumentoEditado = (From dc In db.Documentos.Include("Documentos_Area").Include("Documentos_Area.Areas").Include("Documentos_Terceros").Include("Documentos_Terceros.Terceros")
                                    Where dc.id = id_Documento
                                    Select dc).FirstOrDefault

                If DocumentoEditado IsNot Nothing Then
                    BoundDocumento(DocumentoEditado)
                End If
            Else
                txtFechaInicio.Value = Now.Date()
            End If

        End If
    End Sub

    Private Sub BoundDocumento(DocumentoEditado As Documentos)
        txtAlias.Text = DocumentoEditado.alias
        txtTipoDocumento.DataBind()
        txtTipoDocumento.SelectedValue = DocumentoEditado.id_tipo

        txtnombre.Text = DocumentoEditado.nombre
        txtDescripcion.Text = DocumentoEditado.descripcion
        txtVersion.ValueLong = DocumentoEditado.version
        TxtFolios.Text = DocumentoEditado.folios
        arch = DocumentoEditado.archivo

        txtFechaInicio.Value = DocumentoEditado.fechadoc


    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim cadena As String
        Dim extension As String
        Dim ids As Long
        Dim aux As Integer
        ids = CLng(Session("id_sucursal"))
        If Not (flDocumentos.HasFile) And id_Documento = -1 Then
            Utilidades.Mensaje("Debe seleccionar un archivo", Me)
            Return
        End If

        If id_Documento = -1 Then
            DocumentoEditado = New Documentos
            DocumentoEditado.id_cliente = Session("id_cliente")
            DocumentoEditado.id_sucursal = ids
            DocumentoEditado.fecha_creacion = Today
            db.Documentos.Add(DocumentoEditado)
        Else
            DocumentoEditado = (From dc In db.Documentos.Include("Documentos_Area").Include("Documentos_Area.Areas").Include("Documentos_Terceros").Include("Documentos_Terceros.Terceros")
                                Where dc.id = id_Documento
                                Select dc).FirstOrDefault
        End If

        If DocumentoEditado IsNot Nothing Then
            DocumentoEditado.id_tipo = txtTipoDocumento.SelectedValue
            DocumentoEditado.descripcion = txtDescripcion.Text
            DocumentoEditado.version = txtVersion.ValueLong
            DocumentoEditado.fecha_modificacion = Today
            DocumentoEditado.usuario = User.Identity.Name
            DocumentoEditado.alias = txtAlias.Text
            DocumentoEditado.folios = CLng(TxtFolios.Text)
            DocumentoEditado.nombre = txtnombre.Text
            DocumentoEditado.fechadoc = txtFechaInicio.Value
        End If
        db.SaveChanges()

        If id_Documento = -1 Then

            If flDocumentos.HasFile Then
                extension = flDocumentos.FileName
                aux = InStrRev(extension, ".")
                extension = Mid(extension, aux + 1)
                cadena = "~/Archivos/Arch" & DocumentoEditado.id & "." & extension
                Dim filePath As String =
                    Server.MapPath(cadena)
                flDocumentos.SaveAs(filePath)
                DocumentoEditado.nombre = "Arch" & DocumentoEditado.id & "." & extension
                DocumentoEditado.extension = extension
                db.SaveChanges()
            End If
        End If


        db.SaveChanges()
        If Session("id_sucursal") IsNot Nothing Then
            ids = Session("id_sucursal")
        Else
            ids = 0
        End If
        If id_Documento = -1 Then
            Utilidades.RegistrarAuditoria("Documentos", DocumentoEditado.id, DocumentoEditado.nombre & " - " & DocumentoEditado.alias, "Nuevo Registro", User.Identity.Name, ids)
        Else
            Utilidades.RegistrarAuditoria("Documentos", DocumentoEditado.id, DocumentoEditado.nombre & " - " & DocumentoEditado.alias, "Actualización Registro", User.Identity.Name, ids)
        End If
        Utilidades.Mensaje("Documento Guardado", Me)

        Response.Redirect("../GesDoc/Page_Documentos.aspx")
    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        Response.Redirect("../GesDoc/Page_Documentos.aspx")
    End Sub


End Class