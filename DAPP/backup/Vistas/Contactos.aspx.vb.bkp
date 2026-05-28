Imports Infragistics.Web.UI.NavigationControls

Public Class Contactos
    Inherits Page

    Private db As New DAPP_BDEntities

    Private ReadOnly Property id_tercero As Long
        Get
            If Me.ClientQueryString.Contains("id_tercero") Then
                Return CLng(Request.QueryString.Get("id_tercero"))
            Else
                Return -1
            End If
        End Get
    End Property
    Private ReadOnly Property id_suc As Long
        Get
            If Me.ClientQueryString.Contains("id_suc") Then
                Return CLng(Request.QueryString.Get("id_suc"))
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
    Private ReadOnly Property id_sox As Long
        Get
            If Me.ClientQueryString.Contains("id_sox") Then
                Return CLng(Request.QueryString.Get("id_sox"))
            Else
                Return -1
            End If
        End Get
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load


        If Not Me.Page.IsPostBack Then
            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Response.Redirect("../SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If
            If id_tercero = -1 And id_suc = -1 And id_cliente = -1 And id_sox = -1 Then
                Utilidades.Mensaje("Falta Asociar Info", Me)
                Utilidades.RegistrarScript(String.Format("window.opener.location.href = window.opener.location.href;window.close();"), Me)
            Else
                consulta()
            End If
        End If
    End Sub
    Private Sub consulta()
        Dim cadena As String
        cadena = "SELECT [AdminContacto].[id]"
        cadena = cadena & ",[AdminContacto].[Nombre]"
        cadena = cadena & ",[AdminContacto].[Telefono]"
        cadena = cadena & ",[AdminContacto].[Cargo]"
        cadena = cadena & ",[AdminContacto].[Direccion]"
        cadena = cadena & ",[AdminContacto].[Celular]"
        cadena = cadena & ",[AdminContacto].[Correo]"
        cadena = cadena & ",[AdminContacto].[id_tercero]"
        cadena = cadena & ",[AdminContacto].[id_sucursal]"

        If id_tercero <> -1 Then
            cadena = cadena & " FROM [AdminContacto] inner join terceros"
            cadena = cadena & " on AdminContacto.id_tercero=terceros.id"
            cadena = cadena & " where AdminContacto.id_tercero=" & id_tercero
            cadena = cadena & " order  by [AdminContacto].[Nombre]"
            Dim tit = (From t In db.Terceros
                       Where t.id = id_tercero
                       Select t).FirstOrDefault
            If tit IsNot Nothing Then
                lbTitulo.Text = "Contactos Tercero - " & tit.nombre
            End If
        ElseIf id_suc <> -1 Then
            cadena = cadena & " FROM [AdminContacto] inner join sucursalterceros"
            cadena = cadena & " on AdminContacto.id_sucursal=sucursalterceros.id"
            cadena = cadena & " where AdminContacto.id_sucursal=" & id_suc
            cadena = cadena & " order  by [AdminContacto].[Nombre]"
            Dim tit = (From t In db.SucursalTerceros
                       Where t.id = id_suc
                       Select t).FirstOrDefault
            If tit IsNot Nothing Then
                lbTitulo.Text = "Contactos Sucursal - " & tit.nombre
            End If
        ElseIf id_cliente <> -1 Then
            cadena = cadena & " FROM [AdminContacto] inner join clientes"
            cadena = cadena & " on AdminContacto.id_cliente=clientes.id"
            cadena = cadena & " where AdminContacto.id_cliente=" & id_cliente
            cadena = cadena & " order  by [AdminContacto].[Nombre]"
            Dim tit = (From t In db.Clientes
                       Where t.id = id_cliente
                       Select t).FirstOrDefault
            If tit IsNot Nothing Then
                lbTitulo.Text = "Contactos Cliente - " & tit.nombre
            End If
        Else
            cadena = cadena & " FROM [AdminContacto] inner join AdminSucursal"
            cadena = cadena & " on AdminContacto.id_succliente=AdminSucursal.id"
            cadena = cadena & " where AdminContacto.id_succliente=" & id_sox
            cadena = cadena & " order  by [AdminContacto].[Nombre]"
            Dim tit = (From t In db.AdminSucursal
                       Where t.Id = id_sox
                       Select t).FirstOrDefault
            If tit IsNot Nothing Then
                lbTitulo.Text = "Contactos Sucursal - " & tit.Sucursal
            End If
        End If
        Src_Contactos.SelectCommand = cadena
        Src_Contactos.DataBind()
        GridContactos.DataBind()
    End Sub
    Protected Sub bttNuevo_Click(sender As Object, e As ImageClickEventArgs) Handles bttNuevo.Click
        If id_tercero <> -1 Then
            Response.Redirect("../Vistas/EditarContacto.aspx?id_tercero=" & id_tercero)
        ElseIf id_suc <> -1 Then
            Response.Redirect("../Vistas/EditarContacto.aspx?id_suc=" & id_suc)
        ElseIf id_cliente <> -1 Then
            Response.Redirect("../Vistas/EditarContacto.aspx?id_cliente=" & id_cliente)
        Else
            Response.Redirect("../Vistas/EditarContacto.aspx?id_sox=" & id_sox)
        End If
    End Sub

    Protected Sub bttEditar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEditar.Click
        If GridContactos.Behaviors.Selection IsNot Nothing AndAlso GridContactos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridContactos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_c As Long = rw.DataKey(0)
                If id_tercero <> -1 Then
                    Response.Redirect("../Vistas/EditarContacto.aspx?id_tercero=" & id_tercero & "&id_contacto=" & id_c)
                ElseIf id_suc <> -1 Then
                    Response.Redirect("../Vistas/EditarContacto.aspx?id_suc=" & id_suc & "&id_contacto=" & id_c)
                ElseIf id_cliente <> -1 Then
                    Response.Redirect("../Vistas/EditarContacto.aspx?id_cliente=" & id_cliente & "&id_contacto=" & id_c)
                Else
                    Response.Redirect("../Vistas/EditarContacto.aspx?id_sox=" & id_sox & "&id_contacto=" & id_c)
                End If
            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Contacto", Me)
        End If
    End Sub

    Protected Sub bttEliminar_Click(sender As Object, e As ImageClickEventArgs) Handles bttEliminar.Click
        If GridContactos.Behaviors.Selection IsNot Nothing AndAlso GridContactos.Behaviors.Selection.SelectedRows.Count > 0 Then
            Dim rw As Infragistics.Web.UI.GridControls.ControlDataRecord = GridContactos.Behaviors.Selection.SelectedRows(0)
            If Not rw Is Nothing Then
                Dim id_c As Long = rw.DataKey(0)
                Dim db As New DAPP_BDEntities
                Dim x As Integer
                'x = (From tc In db.Documentos_Terceros
                '     Where tc.id_tercero = id_tc
                '     Select tc).Count
                x = 0
                If x = 0 Then
                    Dim tercero_eliminar = (From tc In db.AdminContacto
                                            Where tc.id = id_c
                                            Select tc).FirstOrDefault

                    If tercero_eliminar IsNot Nothing Then
                        Dim ids As Long
                        If Session("id_sucursal") IsNot Nothing Then
                            ids = Session("id_sucursal")
                        Else
                            ids = 0
                        End If
                        Utilidades.RegistrarAuditoria("Contactos", id_c, tercero_eliminar.Nombre, "Eliminación Registro", User.Identity.Name, ids)
                        db.AdminContacto.Remove(tercero_eliminar)
                        db.SaveChanges()
                        consulta()
                    End If
                Else
                    Utilidades.Mensaje("Existen Datos Asociados al Contacto, Por favor Revisar.", Me)
                End If

            End If
        Else
            Utilidades.Mensaje("No selecciono ningún Contacto", Me)
        End If
    End Sub
    Protected Sub bttExcel_Click(sender As Object, e As ImageClickEventArgs) Handles bttExcel.Click
        ExpGrid.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        ExpGrid.DownloadName = "Contactos" & Today.Date.ToString
        ExpGrid.Export(GridContactos)
    End Sub

    Protected Sub bttPdf_Click(sender As Object, e As ImageClickEventArgs) Handles bttPdf.Click
        Exppdf.DataExportMode = Infragistics.Web.UI.GridControls.DataExportMode.DataInGridOnly
        Exppdf.DownloadName = "Contactos" & Today.Date.ToString
        Exppdf.Format = Infragistics.Web.UI.GridControls.FileFormat.PDF
        Exppdf.Export(GridContactos)
        'Exppdf.Export(GridTipos)
    End Sub
End Class