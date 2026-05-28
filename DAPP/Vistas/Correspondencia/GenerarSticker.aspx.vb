Public Class GenerarSticker
    Inherits System.Web.UI.Page


    Private CorrespondenciaEditada As Correspondencia
    Private db As New DAPP_BDEntities

    Private ReadOnly Property id_correspondencia As Long
        Get
            If Me.ClientQueryString.Contains("id_correspondencia") Then
                Return CLng(Request.QueryString.Get("id_correspondencia"))
            Else
                Return -1
            End If
        End Get
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim idc As Int16
            idc = CInt(Session("id_cliente"))
            Dim clx As Clientes
            Dim cliente As String
            cliente = ""
            clx = (From cl In db.Clientes
                   Where cl.id = idc
                   Select cl).FirstOrDefault
            If clx IsNot Nothing Then
                cliente = clx.nombre
            End If

            Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            If Usuario Is Nothing Then
                Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operación")
            End If

            If id_correspondencia <> -1 Then
                lblTitulo.Text = cliente
                CorrespondenciaEditada = (From crr In db.Correspondencia.Include("AdminSucursal").Include("AdminSucursal.Admin_ciudades")
                                          Where crr.id = id_correspondencia
                                          Select crr).FirstOrDefault

                If CorrespondenciaEditada IsNot Nothing Then

                    lbCiudad.Text = CorrespondenciaEditada.AdminSucursal.Admin_Ciudades.ciudad
                    Dim dst As String = ""
                    Dim dsx = (From dx In db.Terceros_Correspondencia.Include("Terceros")
                               Where dx.id_correspondencia = id_correspondencia And dx.Tipo = "Destinatario"
                               Select dx).ToList
                    For Each TerceroCr In dsx
                        dst &= TerceroCr.Terceros.nombre & vbCrLf
                    Next
                    lbDestinatario.Text = dst
                    Dim rmt As String = ""
                    dsx = (From dx In db.Terceros_Correspondencia.Include("Terceros")
                           Where dx.id_correspondencia = id_correspondencia And dx.Tipo = "Remitente"
                           Select dx).ToList
                    For Each TerceroCr In dsx
                        rmt &= TerceroCr.Terceros.nombre & vbCrLf
                    Next
                    Dim rsx = (From dx In db.Terceros
                               Where dx.id = CorrespondenciaEditada.id_responsable
                               Select dx).FirstOrDefault
                    If rsx IsNot Nothing Then
                        LblResponsable.Text = rsx.nombre
                    Else
                        LblResponsable.Text = ""
                    End If
                    lbEmpresa.Text = rmt
                    If CorrespondenciaEditada.tipo_correspondencia = "Externa Enviada" Or CorrespondenciaEditada.tipo_correspondencia = "Interna Recibida" Then
                        If lbDestinatario.Text = "" Then
                            lbDestinatario.Text = CorrespondenciaEditada.ciudad_remitente
                        End If
                    Else
                        If lbEmpresa.Text = "" Then
                            lbEmpresa.Text = CorrespondenciaEditada.ciudad_remitente
                        End If
                    End If

                    lbFechaHora.Text = CorrespondenciaEditada.fecha_sistema.ToString("dd/MM/yyyy hh:mm:ss tt")
                    If CorrespondenciaEditada.folios IsNot Nothing Then
                        lbNumeroFolios.Text = CorrespondenciaEditada.folios
                    Else
                        lbNumeroFolios.Text = "0"
                    End If
                    If CorrespondenciaEditada.paquetes IsNot Nothing Then
                        LblPaquete.Text = CorrespondenciaEditada.paquetes
                    Else
                        LblPaquete.Text = "0"
                    End If
                    lbNumeroRadicacion.Text = CorrespondenciaEditada.numero_radicado
                    lbReferencia.Text = CorrespondenciaEditada.numero_radicado
                    lbSucursal.Text = CorrespondenciaEditada.AdminSucursal.Sucursal
                End If
            End If
        End If
    End Sub

End Class