Public Class GenerarFirma
    Inherits System.Web.UI.Page


    Private usuarioEditado As Usuarios
    Private db As New DAPP_BDEntities

    Private ReadOnly Property id_usuario As Long
        Get
            If Me.ClientQueryString.Contains("id_usuario") Then
                Return CLng(Request.QueryString.Get("id_usuario"))
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
            If id_usuario = -1 Then
                Utilidades.Mensaje("Usuario No Referenciado", Me)
            End If
            Label1.Visible = False
            lblfecha.Visible = False
            Lb3.Visible = False
            imgfirma.Visible = False
        End If
    End Sub

    Protected Sub btOk_Click(sender As Object, e As EventArgs) Handles btOk.Click
        If id_usuario <> -1 Then

            usuarioEditado = (From crr In db.Usuarios
                              Where crr.ID = id_usuario
                              Select crr).FirstOrDefault
            If usuarioEditado IsNot Nothing Then
                If usuarioEditado.id_tercero IsNot Nothing Then
                    Dim trx = (From tx In db.Terceros
                               Where tx.id = usuarioEditado.id_tercero
                               Select tx).FirstOrDefault
                    If trx IsNot Nothing Then
                        If txtPwd.Text = trx.pwdfirma Then
                            Label1.Visible = True
                            lblfecha.Visible = True
                            lblfecha.Text = Format(Now(), "dd-MMM-yyyy hh:mm:ss tt")
                            Dim lnk As String
                            lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/" & trx.archfirma
                            imgfirma.ImageUrl = lnk
                            Lb3.Visible = True
                            imgfirma.Visible = True
                            lbltercero.Text = trx.nombre
                            lb1.Visible = False
                            txtPwd.Visible = False
                            btOk.Visible = False
                        Else
                            Utilidades.Mensaje("Contraseña No corresponde", Me)
                            Label1.Visible = False
                            lblfecha.Visible = False
                            Lb3.Visible = False
                            imgfirma.Visible = False
                            lb1.Visible = True
                            txtPwd.Visible = True
                            btOk.Visible = True
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class