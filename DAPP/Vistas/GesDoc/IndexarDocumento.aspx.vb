Imports System.IO
Public Class IndexarDocumento
    Inherits System.Web.UI.Page


    Private DocumentoEditado As Documentos
    Private db As New DAPP_BDEntities
    Private arch As Byte()
    Private ReadOnly Property AppGlobal As New AppGlobal_Class
    Private idd As Long
    Private ReadOnly Property id_Documento As Long
        Get
            If Me.ClientQueryString.Contains("id_documento") Then
                Return CLng(Request.QueryString.Get("id_documento"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property selec As Integer
        Get
            If Me.ClientQueryString.Contains("Seleccion") Then
                Return CLng(Request.QueryString.Get("Seleccion"))
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
                txtFechaInicio.Text = Format(Now.Date(), "yyyy-MM-dd")
                txtTipoDocumento.DataBind()
                txtTipoDocumento.SelectedIndex = 0
                Actualiza_Tabla()
            End If
        Else
            Pintar_Tabla()
        End If
    End Sub

    Private Sub BoundDocumento(DocumentoEditado As Documentos)
        txtAlias.Text = DocumentoEditado.alias
        txtTipoDocumento.DataBind()
        txtTipoDocumento.SelectedValue = DocumentoEditado.id_tipo
        Actualiza_Tabla()
        txtnombre.Text = DocumentoEditado.nombre
        txtDescripcion.Text = DocumentoEditado.descripcion
        txtVersion.ValueLong = DocumentoEditado.version
        TxtFolios.Text = DocumentoEditado.folios
        arch = DocumentoEditado.archivo

        txtFechaInicio.Text = Format(DocumentoEditado.fechadoc, "yyyy-MM-dd")

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
        If Not flDocumentos.HasFile And txtAlias.Text = "" Then
            Utilidades.Mensaje("Debe especificar un nombre de archivo", Me)
            Return
        End If

        If id_Documento = -1 Then
            DocumentoEditado = New Documentos
            DocumentoEditado.id_cliente = Session("id_cliente")
            DocumentoEditado.id_sucursal = ids
            DocumentoEditado.fecha_creacion = Today
            db.Documentos.Add(DocumentoEditado)
        Else
            DocumentoEditado = (From dc In db.Documentos
                                Where dc.id = id_Documento
                                Select dc).FirstOrDefault
        End If

        If DocumentoEditado IsNot Nothing Then
            DocumentoEditado.id_tipo = txtTipoDocumento.SelectedValue
            DocumentoEditado.descripcion = txtDescripcion.Text
            DocumentoEditado.version = txtVersion.ValueLong
            DocumentoEditado.fecha_modificacion = Today
            DocumentoEditado.usuario = User.Identity.Name
            If flDocumentos.HasFile And txtAlias.Text = "" Then
                extension = flDocumentos.FileName
                aux = InStrRev(extension, ".")
                txtAlias.Text = Mid(extension, 1, aux - 1)
            End If
            DocumentoEditado.alias = txtAlias.Text
            DocumentoEditado.folios = CLng(TxtFolios.Text)
            DocumentoEditado.nombre = txtnombre.Text
            DocumentoEditado.fechadoc = txtFechaInicio.Text
            DocumentoEditado.FGestion = txtFechaInicio.Text
            DocumentoEditado.Estado = "Indexado"
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
            idd = DocumentoEditado.id
            Utilidades.RegistrarAuditoria("Documentos", DocumentoEditado.id, DocumentoEditado.nombre & " - " & DocumentoEditado.alias, "Nuevo Registro", User.Identity.Name, ids)
        Else
            idd = id_Documento
            Utilidades.RegistrarAuditoria("Documentos", DocumentoEditado.id, DocumentoEditado.nombre & " - " & DocumentoEditado.alias, "Actualización Registro", User.Identity.Name, ids)
        End If
        Guardar_Tabla()
        Utilidades.Mensaje("Documento Guardado", Me)
        If selec = -1 Then
            Response.Redirect("../GesDoc/Page_Doc.aspx")
        Else
            Response.Redirect("../Seleccionar_Documento.aspx")
        End If

    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        If selec = -1 Then
            Response.Redirect("../GesDoc/Page_Doc.aspx")
        Else
            Response.Redirect("../Seleccionar_Documento.aspx")
        End If
    End Sub

    Protected Sub Actualiza_Tabla()

        Dim objRow As TableRow
        Dim objCell As TableCell
        Dim estilo As String
        Dim i As Integer
        Dim c As Integer
        c = (From cd In db.Campos_Documentos
             Where cd.id_doc = id_Documento
             Select cd).Count

        Dim lstcampos = (From ct In db.AdminCamposAdicionales
                         Where ct.id_tipo = txtTipoDocumento.SelectedValue
                         Select ct).ToList

        TblDatos.Rows.Clear()
        objRow = New TableRow
        estilo = "hcampo1"
        objCell = New TableCell
        objCell.CssClass = "hcampo1"
        objCell.Text = "Campo"
        objCell.HorizontalAlign = HorizontalAlign.Center
        objCell.Width = Unit.Percentage(50)
        objRow.Cells.Add(objCell)
        objCell = New TableCell
        objCell.CssClass = "hcampo1"
        objCell.Text = "Valor"
        objCell.Width = Unit.Percentage(50)
        objCell.HorizontalAlign = HorizontalAlign.Center
        objRow.Cells.Add(objCell)
        objCell = New TableCell
        objCell.CssClass = "hcampo1"
        objCell.Text = "ID Campo"
        objCell.HorizontalAlign = HorizontalAlign.Center
        objCell.Visible = False
        objRow.Cells.Add(objCell)

        TblDatos.Rows.Add(objRow)
        i = 0

        For Each campo As AdminCamposAdicionales In lstcampos
            i = i + 1
            If i Mod 2 = 0 Then
                estilo = "row2"
            Else
                estilo = "row1"
            End If

            objRow = New TableRow
            objRow.EnableViewState = True
            objRow.ViewStateMode = UI.ViewStateMode.Enabled

            objCell = New TableCell
            objCell.CssClass = estilo
            objCell.Text = campo.Campo
            objCell.HorizontalAlign = HorizontalAlign.Center
            objRow.Cells.Add(objCell)

            objCell = New TableCell
            objCell.CssClass = estilo
            Dim txtvalor = New TextBox
            txtvalor.CssClass = estilo
            txtvalor.TextMode = TextBoxMode.SingleLine
            txtvalor.Height = "20"
            txtvalor.Width = Unit.Percentage(100)
            txtvalor.Style("HorizontalAlign") = HorizontalAlign.Left
            txtvalor.ID = "txtvalor" & i
            Dim vcam = (From ca In db.Campos_Documentos
                        Where ca.id_campo = campo.id And ca.id_doc = id_Documento
                        Select ca).FirstOrDefault
            If vcam IsNot Nothing Then
                txtvalor.Text = vcam.valor
            Else
                txtvalor.Text = ""
            End If
            objCell.Controls.Add(txtvalor)
            objRow.Cells.Add(objCell)


            objCell = New TableCell
            objCell.CssClass = estilo
            Dim txtid As New TextBox
            txtid.ID = "txtID" & i
            txtid.Enabled = False
            txtid.Text = campo.id
            txtid.CssClass = estilo
            objCell.Controls.Add(txtid)
            objCell.Visible = False
            objRow.Cells.Add(objCell)

            TblDatos.Rows.Add(objRow)
        Next
        txtcuantos.Text = i

    End Sub
    Protected Sub Guardar_Tabla()
        Dim i As Integer
        Dim j As Integer
        Dim idc As Long
        Dim txt As TextBox
        Dim valor As String
        i = txtcuantos.Text
        For j = 1 To i
            txt = TblDatos.FindControl("txtID" & j)
            If txt IsNot Nothing Then
                idc = CLng(txt.Text)
                txt = TblDatos.FindControl("txtvalor" & j)
                If txt IsNot Nothing Then
                    valor = txt.Text
                    Dim ca = (From cav In db.Campos_Documentos
                              Where cav.id_campo = idc And cav.id_doc = idd
                              Select cav).FirstOrDefault
                    If ca IsNot Nothing Then
                        ca.valor = valor
                    Else
                        ca = New Campos_Documentos
                        ca.id_doc = idd
                        ca.id_campo = idc
                        ca.valor = valor
                        db.Campos_Documentos.Add(ca)
                    End If
                    db.SaveChanges()
                End If
            End If
        Next
    End Sub

    Protected Sub Pintar_Tabla()

        Dim objRow As TableRow
        Dim objCell As TableCell
        Dim estilo As String
        Dim i As Integer
        Dim c As Integer
        c = (From cd In db.Campos_Documentos
             Where cd.id_doc = id_Documento
             Select cd).Count

        Dim lstcampos = (From ct In db.AdminCamposAdicionales
                         Where ct.id_tipo = txtTipoDocumento.SelectedValue
                         Select ct).ToList

        TblDatos.Rows.Clear()
        objRow = New TableRow
        estilo = "hcampo1"
        objCell = New TableCell
        objCell.CssClass = "hcampo1"
        objCell.Text = "Campo"
        objCell.HorizontalAlign = HorizontalAlign.Center
        objCell.Width = Unit.Percentage(50)
        objRow.Cells.Add(objCell)
        objCell = New TableCell
        objCell.CssClass = "hcampo1"
        objCell.Text = "Valor"
        objCell.Width = Unit.Percentage(50)
        objCell.HorizontalAlign = HorizontalAlign.Center
        objRow.Cells.Add(objCell)
        objCell = New TableCell
        objCell.CssClass = "hcampo1"
        objCell.Text = "ID Campo"
        objCell.HorizontalAlign = HorizontalAlign.Center
        objCell.Visible = False
        objRow.Cells.Add(objCell)
        TblDatos.Rows.Add(objRow)
        i = 0

        For Each campo As AdminCamposAdicionales In lstcampos
            i = i + 1
            If i Mod 2 = 0 Then
                estilo = "row2"
            Else
                estilo = "row1"
            End If

            objRow = New TableRow
            objRow.EnableViewState = True
            objRow.ViewStateMode = UI.ViewStateMode.Enabled

            objCell = New TableCell
            objCell.CssClass = estilo
            objCell.Text = campo.Campo
            objCell.HorizontalAlign = HorizontalAlign.Center
            objRow.Cells.Add(objCell)

            objCell = New TableCell
            objCell.CssClass = estilo
            Dim txtvalor = New TextBox
            txtvalor.CssClass = estilo
            txtvalor.TextMode = TextBoxMode.SingleLine
            txtvalor.Height = "20"
            txtvalor.Width = Unit.Percentage(100)
            txtvalor.Style("HorizontalAlign") = HorizontalAlign.Left
            txtvalor.ID = "txtvalor" & i
            objCell.Controls.Add(txtvalor)
            objRow.Cells.Add(objCell)

            objCell = New TableCell
            objCell.CssClass = estilo
            Dim txtid As New TextBox
            txtid.ID = "txtID" & i
            txtid.Enabled = False
            txtid.Text = campo.id
            txtid.CssClass = estilo
            objCell.Controls.Add(txtid)
            objCell.Visible = False
            objRow.Cells.Add(objCell)

            TblDatos.Rows.Add(objRow)
        Next
        txtcuantos.Text = i

    End Sub

    Protected Sub txtTipoDocumento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles txtTipoDocumento.SelectedIndexChanged
        Utilidades.EliminarCampos(id_Documento)
        Actualiza_Tabla()
    End Sub
End Class