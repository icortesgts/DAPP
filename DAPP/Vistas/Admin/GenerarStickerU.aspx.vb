Public Class GenerarStickerU
    Inherits System.Web.UI.Page


    Private UbicacionEditada As Ubicaciones
    Private db As New DAPP_BDEntities

    Private ReadOnly Property id_ubicacion As Long
        Get
            If Me.ClientQueryString.Contains("id_ubicacion") Then
                Return CLng(Request.QueryString.Get("id_ubicacion"))
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
            Dim contenido As String
            If id_ubicacion <> -1 Then

                UbicacionEditada = (From crr In db.Ubicaciones.Include("Ubicaciones2").Include("AdminSucursal").Include("AdminSucursal.Clientes")
                                    Where crr.id = id_ubicacion
                                    Select crr).FirstOrDefault

                If UbicacionEditada IsNot Nothing Then

                    If UbicacionEditada.Ubicaciones2 IsNot Nothing Then
                        LblPadre.Text = UbicacionEditada.Ubicaciones2.ubicacion
                    Else
                        LblPadre.Text = ""
                    End If

                    LblUbicacion.Text = UbicacionEditada.ubicacion
                    LblCliente.Text = UbicacionEditada.AdminSucursal.Clientes.nombre & " - " & UbicacionEditada.AdminSucursal.Sucursal
                    If (From fx In db.Folios_ubicacion(id_ubicacion)).FirstOrDefault IsNot Nothing Then
                        Dim flx = (From fx In db.Folios_ubicacion(id_ubicacion)).FirstOrDefault
                        If flx IsNot Nothing Then
                            lbFolios.Text = flx.Value
                        Else
                            lbFolios.Text = ""
                        End If
                    End If

                    Dim cadena As String
                    Dim tipos As String
                    Dim clases As String
                    Dim tpu As String
                    Dim tpx = (From tp In db.TiposDoc_ubicacion(id_ubicacion)).ToList
                    tipos = ""
                    For Each tx In tpx
                        If tipos = "" Then
                            tipos = tipos & tx.ToString
                        Else
                            tipos = tipos & ", " & tx.ToString
                        End If
                    Next
                    clases = ""
                    Dim cdx = (From tp In db.ClaseDoc_ubicacion(id_ubicacion)).ToList
                    For Each cx In cdx
                        If clases = "" Then
                            clases = clases & cx.ToString
                        Else
                            clases = clases & ", " & cx.ToString
                        End If
                    Next
                    cadena = ""
                    tpu = ""
                    CargarArbol(id_ubicacion, cadena, tpu)
                    If tpu = "" Then
                        tpu = "Documentos"
                    Else
                        tpu = tpu & ", Documentos"
                    End If
                    contenido = ""
                    lstcontenido.Text = ""
                    If tipos <> "" Then
                        lstcontenido.Text = "Tipos de Documento: " & tipos
                    End If
                    If clases <> "" Then
                        If lstcontenido.Text = "" Then
                            lstcontenido.Text = "Clases de Documento: " & clases
                        Else
                            lstcontenido.Text = lstcontenido.Text & Chr(13) & "Clases de Documento: " & clases
                        End If

                    End If
                    If tpu <> "" Then
                        If lstcontenido.Text = "" Then
                            lstcontenido.Text = "Tipo Contenido:" & tpu
                        Else
                            lstcontenido.Text = lstcontenido.Text & Chr(13) & "Tipo Contenido:" & tpu
                        End If
                    End If
                    If cadena <> "" Then
                        If lstcontenido.Text = "" Then
                            lstcontenido.Text = "Contiene:" & cadena
                        Else
                            lstcontenido.Text = lstcontenido.Text & Chr(13) & "Contiene:" & cadena
                        End If
                    End If

                End If
            End If
        End If
    End Sub
    Private Sub CargarArbol(id As Long, ByRef cadena As String, ByRef tipo As String)
        Dim ids As Long
        ids = CLng(Session("id_sucursal"))
        Dim enc As Integer
        Dim Raices = From rz In db.Ubicaciones
                     Where rz.id_ubicacion = id And rz.id_sucursal = ids
                     Select rz

        For Each Raiz In Raices
            If cadena = "" Then
                cadena = Raiz.ubicacion
            Else
                cadena = cadena & "," & Raiz.ubicacion
            End If
            Dim tp = (From rz In db.AdminTipoUbicacion
                      Where rz.id = Raiz.id_tipo
                      Select rz).FirstOrDefault
            enc = InStr(1, tipo, tp.nombre)
            If enc <= 0 Then
                If tipo = "" Then
                    tipo = tp.nombre
                Else
                    tipo = tipo & "," & tp.nombre
                End If
            End If
            AdjuntarHijos(Raiz.id, cadena, tipo)
        Next

    End Sub

    Private Sub AdjuntarHijos(id As Long, ByRef cadena As String, ByRef tipo As String)

        Dim Hijos = (From hj In db.Ubicaciones
                     Where hj.id_ubicacion = id
                     Select hj)
        Dim enc As Integer

        For Each Hijo In Hijos
            If cadena = "" Then
                cadena = Hijo.ubicacion
            Else
                cadena = cadena & "," & Hijo.ubicacion
            End If
            Dim tp = (From rz In db.AdminTipoUbicacion
                      Where rz.id = Hijo.id_tipo
                      Select rz).FirstOrDefault
            enc = InStr(1, tipo, tp.nombre)
            If enc <= 0 Then
                If tipo = "" Then
                    tipo = tp.nombre
                Else
                    tipo = tipo & "," & tp.nombre
                End If
            End If
            AdjuntarHijos(Hijo.id, cadena, tipo)
        Next

    End Sub

End Class