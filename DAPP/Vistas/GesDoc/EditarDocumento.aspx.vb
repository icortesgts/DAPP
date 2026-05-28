Imports System.IO
Public Class EditarDocumento
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
    Private ReadOnly Property buscardoc As Long
        Get
            If Me.ClientQueryString.Contains("buscardoc") Then
                Return CLng(Request.QueryString.Get("buscardoc"))
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
            txtTipoDocumento.DataBind()
            If txtTipoDocumento.Items.Count > 0 Then
                If id_Documento <> -1 Then

                    DocumentoEditado = (From dc In db.Documentos.Include("Documentos_Area").Include("Documentos_Area.Areas").Include("Documentos_Terceros").Include("Documentos_Terceros.Terceros")
                                        Where dc.id = id_Documento
                                        Select dc).FirstOrDefault

                    If DocumentoEditado IsNot Nothing Then
                        BoundDocumento(DocumentoEditado)
                    End If
                Else
                    txtFechaInicio.Text = Format(Now.Date(), "yyyy-MM-dd")
                    FGestion.Text = Format(Now.Date(), "yyyy-MM-dd")
                    FHistorico.Text = Format(Now.Date(), "yyyy-MM-dd")
                    FCentral.Text = Format(Now.Date(), "yyyy-MM-dd")
                    txtTipoDocumento.SelectedIndex = 0
                    Actualiza_Tabla()
                End If
            Else
                Response.Redirect("../GesDoc/Page_Documentos.aspx")
            End If

        Else
            TabName.Value = Request.Form(TabName.UniqueID)
            Pintar_Tabla()
        End If
    End Sub

    Private Sub BoundDocumento(DocumentoEditado As Documentos)
        txtAlias.Text = DocumentoEditado.alias
        txtTipoDocumento.DataBind()
        txtTipoDocumento.SelectedValue = DocumentoEditado.id_tipo
        Actualiza_Tabla()
        If DocumentoEditado.id_ubicacion IsNot Nothing Then
            TxtUbicacion.DataBind()
            TxtUbicacion.SelectedValue = DocumentoEditado.id_ubicacion
        End If
        txtnombre.Text = DocumentoEditado.nombre
        txtDescripcion.Text = DocumentoEditado.descripcion
        txtVersion.ValueLong = DocumentoEditado.version
        TxtFolios.Text = DocumentoEditado.folios
        arch = DocumentoEditado.archivo
        txtFechaInicio.Text = Format(DocumentoEditado.fechadoc, "yyyy-MM-dd")
        If DocumentoEditado.FGestion IsNot Nothing Then
            FGestion.Text = Format(DocumentoEditado.FGestion, "yyyy-MM-dd")
        Else
            FGestion.Text = Format(Now.Date(), "yyyy-MM-dd")
        End If
        If DocumentoEditado.FCentral IsNot Nothing Then
            FCentral.Text = Format(DocumentoEditado.FCentral, "yyyy-MM-dd")
        Else
            FCentral.Text = Format(Now.Date(), "yyyy-MM-dd")
        End If
        If DocumentoEditado.FHistorico IsNot Nothing Then
            FHistorico.Text = Format(DocumentoEditado.FHistorico, "yyyy-MM-dd")
        Else
            FHistorico.Text = Format(Now.Date(), "yyyy-MM-dd")
        End If

        lstAreasDocumento.Items.Clear()
        For Each dc_ar In DocumentoEditado.Documentos_Area
            lstAreasDocumento.Items.Add(New ListItem With {.Text = dc_ar.Areas.nombre, .Value = dc_ar.id_area})
        Next

        lstTercerosDocumento.Items.Clear()
        For Each dc_tr In DocumentoEditado.Documentos_Terceros
            lstTercerosDocumento.Items.Add(New ListItem With {.Text = dc_tr.Terceros.nombre, .Value = dc_tr.id_tercero})
        Next


    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        Dim cadena As String
        Dim extension As String
        Dim ids As Long
        Dim aux As Integer
        Dim ubc As Boolean
        Dim areasx As Boolean
        Dim tercx As Boolean
        ubc = False
        areasx = False
        tercx = False
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
            DocumentoEditado = (From dc In db.Documentos.Include("Documentos_Area").Include("Documentos_Area.Areas").Include("Documentos_Terceros").Include("Documentos_Terceros.Terceros")
                                Where dc.id = id_Documento
                                Select dc).FirstOrDefault
        End If



        If DocumentoEditado IsNot Nothing Then
            If id_Documento = -1 Then
                Dim tu = (From tpu In db.Ubicaciones
                          Where tpu.id = TxtUbicacion.SelectedValue
                          Select tpu).FirstOrDefault
                If tu IsNot Nothing Then
                    tu.Capacidad = tu.Capacidad - CLng(TxtFolios.Text)
                End If
            Else
                If DocumentoEditado.id_ubicacion <> TxtUbicacion.SelectedValue Then
                    Dim tu = (From tpu In db.Ubicaciones
                              Where tpu.id = TxtUbicacion.SelectedValue
                              Select tpu).FirstOrDefault
                    If tu IsNot Nothing Then
                        tu.Capacidad = tu.Capacidad - CLng(TxtFolios.Text)
                    End If
                    tu = (From tpu In db.Ubicaciones
                          Where tpu.id = DocumentoEditado.id_ubicacion
                          Select tpu).FirstOrDefault
                    If tu IsNot Nothing Then
                        tu.Capacidad = tu.Capacidad + CLng(TxtFolios.Text)
                    End If
                End If
            End If
            DocumentoEditado.id_tipo = txtTipoDocumento.SelectedValue
            DocumentoEditado.id_ubicacion = TxtUbicacion.SelectedValue
            ubc = True
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
            DocumentoEditado.FGestion = FGestion.Text
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

        For Each it As ListItem In lstAreasDocumento.Items
            Dim AreaDocumento As Documentos_Area = (From dar In db.Documentos_Area
                                                    Where dar.id_area = it.Value And dar.id_documento = DocumentoEditado.id
                                                    Select dar).FirstOrDefault
            If AreaDocumento Is Nothing Then
                Dim ar As New Documentos_Area
                ar.id_area = it.Value
                ar.Documentos = DocumentoEditado
                ar.id_documento = DocumentoEditado.id
                db.Documentos_Area.Add(ar)
                areasx = True
            Else
                areasx = True
            End If
        Next


        For Each it As ListItem In lstTercerosDocumento.Items
            Dim TerceroDocumento As Documentos_Terceros = (From dtr In db.Documentos_Terceros
                                                           Where dtr.id_tercero = it.Value And dtr.id_documento = DocumentoEditado.id
                                                           Select dtr).FirstOrDefault
            If TerceroDocumento Is Nothing Then
                Dim tr As New Documentos_Terceros
                tr.id_tercero = it.Value
                tr.Documentos = DocumentoEditado
                tr.id_documento = DocumentoEditado.id
                db.Documentos_Terceros.Add(tr)
                tercx = True
            Else
                tercx = True
            End If
        Next
        If ubc And areasx And tercx Then
            DocumentoEditado.Estado = "Archivado"
        Else

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

        If buscardoc = 1 Then
            Response.Redirect("../GesDoc/Page_BuscarDocumentos.aspx")
        Else
            Response.Redirect("../GesDoc/Page_Documentos.aspx")
        End If

    End Sub

    Protected Sub btCancel_Click(sender As Object, e As EventArgs) Handles btCancel.Click
        If buscardoc = 1 Then
            Response.Redirect("../GesDoc/Page_BuscarDocumentos.aspx")
        Else
            Response.Redirect("../GesDoc/Page_Documentos.aspx")
        End If
    End Sub

    Protected Sub bttBuscarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarArea.Click
        Utilidades.RegistrarScript("window.open('../Seleccionar_Area.aspx','SeleccionarArea','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
    End Sub

    Protected Sub bttAgregarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarArea.Click
        Dim area As Areas = (From ar In db.Areas
                             Where ar.id = Hdd_id_area.Value
                             Select ar).FirstOrDefault

        If area IsNot Nothing Then
            lstAreasDocumento.Items.Add(New ListItem With {.Text = area.nombre, .Value = area.id})
        End If
    End Sub

    Protected Sub bttEliminarArea_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarArea.Click
        Dim it As ListItem = lstAreasDocumento.SelectedItem

        DocumentoEditado = (From dc In db.Documentos.Include("Documentos_Area").Include("Documentos_Area.Areas").Include("Documentos_Terceros").Include("Documentos_Terceros.Terceros")
                            Where dc.id = id_Documento
                            Select dc).FirstOrDefault

        If it IsNot Nothing Then
            If DocumentoEditado IsNot Nothing Then
                Dim AreaDocumento As Documentos_Area = (From dar In DocumentoEditado.Documentos_Area
                                                        Where dar.id_area = it.Value
                                                        Select dar).FirstOrDefault
                If AreaDocumento IsNot Nothing Then
                    db.Documentos_Area.Remove(AreaDocumento)
                    db.SaveChanges()
                    BoundDocumento(DocumentoEditado)
                Else
                    lstTercerosDocumento.Items.Remove(it)
                End If
            Else
                lstTercerosDocumento.Items.Remove(it)
            End If
        End If
    End Sub

    Protected Sub bttBuscarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttBuscarTercero.Click
        If txtDocumentoTercero.Text IsNot Nothing AndAlso txtDocumentoTercero.Text <> String.Empty Then
            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx?cedula=" & txtDocumentoTercero.Text & "','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
        Else
            Utilidades.RegistrarScript("window.open('../Seleccionar_Tercero.aspx','SeleccionarTercero','width=800,height=600,left=' + (screen.width - 800)/2 + ',top=' + (screen.height - 600)/2  + ',scrollbars=yes,,resizable=yes,status=no,menubar=no');", Me)
        End If

    End Sub

    Protected Sub bttAgregarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttAgregarTercero.Click
        Dim tr As Terceros = (From tc In db.Terceros
                              Where tc.id = Hdd_id_tercero.Value
                              Select tc).FirstOrDefault

        If tr IsNot Nothing Then
            lstTercerosDocumento.Items.Add(New ListItem With {.Text = tr.nombre, .Value = tr.id})
        End If
    End Sub

    Protected Sub bttEliminarTercero_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminarTercero.Click
        Dim it As ListItem = lstTercerosDocumento.SelectedItem

        DocumentoEditado = (From dc In db.Documentos.Include("Documentos_Area").Include("Documentos_Area.Areas").Include("Documentos_Terceros").Include("Documentos_Terceros.Terceros")
                            Where dc.id = id_Documento
                            Select dc).FirstOrDefault
        If it IsNot Nothing Then
            If DocumentoEditado IsNot Nothing Then
                Dim TerceroDocumento As Documentos_Terceros = (From dtr In DocumentoEditado.Documentos_Terceros
                                                               Where dtr.id_tercero = it.Value
                                                               Select dtr).FirstOrDefault
                If TerceroDocumento IsNot Nothing Then
                    db.Documentos_Terceros.Remove(TerceroDocumento)
                    db.SaveChanges()
                    BoundDocumento(DocumentoEditado)
                Else
                    lstTercerosDocumento.Items.Remove(it)
                End If
            Else
                lstTercerosDocumento.Items.Remove(it)
            End If
        End If
    End Sub

    Protected Sub txtFechaInicio_TextChanged(sender As Object, e As EventArgs) Handles txtFechaInicio.TextChanged
        FGestion.Text = txtFechaInicio.Text
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
        Dim tpx As Long
        tpx = CLng(txtTipoDocumento.SelectedValue)
        Dim lstcampos = (From ct In db.AdminCamposAdicionales
                         Where ct.id_tipo = tpx
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
        Dim tpx As Long
        tpx = CLng(txtTipoDocumento.SelectedValue)
        Dim lstcampos = (From ct In db.AdminCamposAdicionales
                         Where ct.id_tipo = tpx
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

    Protected Sub txtDocumentoTercero_TextChanged(sender As Object, e As EventArgs) Handles txtDocumentoTercero.TextChanged
        Dim idc As Long
        idc = CLng(Session("id_cliente"))
        If txtDocumentoTercero.Text <> "" Then
            Dim Tercero As Terceros = (From tr In db.Terceros
                                       Where tr.numero_documento = CLng(txtDocumentoTercero.Text) And tr.id_cliente = idc
                                       Select tr).FirstOrDefault
            If Tercero IsNot Nothing Then
                txtTercero.Text = Tercero.nombre
                Hdd_id_tercero.Value = Tercero.id
            Else
                Utilidades.Mensaje("Documento No encontrado", Me)
            End If
        End If
    End Sub

    Protected Sub txtTercero_TextChanged(sender As Object, e As EventArgs) Handles txtTercero.TextChanged
        Dim idc As Long
        idc = CLng(Session("id_cliente"))
        If txtTercero.Text <> "" Then
            Dim Tercero As Terceros = (From tr In db.Terceros
                                       Where tr.nombre = txtTercero.Text And tr.id_cliente = idc
                                       Select tr).FirstOrDefault
            If Tercero IsNot Nothing Then
                txtDocumentoTercero.Text = Tercero.numero_documento
                Hdd_id_tercero.Value = Tercero.id
            Else
                Utilidades.Mensaje("Nombre No encontrado", Me)
            End If
        End If
    End Sub
End Class