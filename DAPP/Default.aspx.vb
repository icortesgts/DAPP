''Imports Vintasoft.Twain
'Imports Vintasoft.Shared
'Imports Vintasoft.Data
'Imports Vintasoft.WpfTwain
Public Class _Default
    Inherits Page
    Private db As New DAPP_BDEntities
    Private ReadOnly Property id_cliente As Long
        Get
            If Me.ClientQueryString.Contains("id_cliente") Then
                Return CLng(Request.QueryString.Get("id_cliente"))
            Else
                Return -1
            End If
        End Get
    End Property
    'Public Sub New()
    '    Dim user As String
    '    Dim url As String
    '    Dim regcode As String
    '    user = "SISTAV TECNOLOGIA"
    '    url = "greentechsupply.co"
    '    regcode = "WYAe6r2B0IXIJ2w + axoE / dINpUSqCOVrjvus5EAwYRYhMteCes88GrhMa / xn3tDZMmm2xmvL6zA1EEtsoi5 / Ngu88cQDaXhELwut5vWiIrVXCdQleXubYQ6bBnVLIy4p5wVVEcPKGxB / hnzxI / HLySqw0VI35FqIQRg1QYENzbl4"
    '    'InitializeComponent()

    '    Dim twainGlobalSettings1 As TwainGlobalSettings = New TwainGlobalSettings()

    '    'twainGlobalSettings1.Register(user, url, regcode)

    'End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles Me.Load
        Dim mp As SiteMaster
        mp = Me.Page.Master

        If mp.cliente <> -1 Then
            Dim ids As Long
            ids = mp.cliente
            Session("id_cliente") = mp.cliente
            If mp.sucursal <> -1 Then
                Session("id_sucursal") = mp.sucursal
            End If
            Dim cl = (From cx In db.Clientes
                      Where cx.id = ids
                      Select cx).FirstOrDefault
            If cl IsNot Nothing Then
                Dim lnk As String
                lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                imginicio.ImageUrl = lnk & cl.Inicio
                Dim script As String
                script = "inicio('" & lnk & cl.Inicio & "');"
                Utilidades.RegistrarScript(script, Me)
            End If
        ElseIf Session("id_cliente") IsNot Nothing Then
            Dim ids As Long
            ids = CLng(Session("id_cliente"))
            Dim cl = (From cx In db.Clientes
                      Where cx.id = ids
                      Select cx).FirstOrDefault
            If cl IsNot Nothing Then
                Dim lnk As String
                lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                imginicio.ImageUrl = lnk & cl.Inicio
                Dim script As String
                script = "inicio('" & lnk & cl.Inicio & "');"
                Utilidades.RegistrarScript(script, Me)
            End If
        Else
            Dim idu As Long
            Dim clx As Boolean
            Dim lnk As String
            Dim script As String
            idu = Utilidades.ConsultarUsuario(User.Identity.Name).ID
            clx = Utilidades.ConsultarUsuario(User.Identity.Name).Todos
            If clx Then
                Dim cly = (From cl In db.Clientes
                           Select cl Order By cl.nombre).FirstOrDefault
                If cly IsNot Nothing Then
                    lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                    imginicio.ImageUrl = lnk & cly.Inicio
                    script = "inicio('" & lnk & cly.Inicio & "');"
                    Utilidades.RegistrarScript(script, Me)
                Else
                    lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Images/"
                    imginicio.ImageUrl = lnk & "procesos cng.jpg"

                    script = "inicio('" & lnk & "procesos cng.jpg" & "');"
                    Utilidades.RegistrarScript(script, Me)
                End If
            Else
                Dim cly = (From cl In db.Usuario_Cliente.Include("Clientes")
                           Where cl.id_usuario = idu
                           Select cl Order By cl.Clientes.nombre).FirstOrDefault
                If cly IsNot Nothing Then
                    lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Archivos/"
                    imginicio.ImageUrl = lnk & cly.Clientes.Inicio
                    script = "inicio('" & lnk & cly.Clientes.Inicio & "');"
                    Utilidades.RegistrarScript(script, Me)
                Else
                    lnk = ConfigurationManager.AppSettings("URLBase").ToString & "Images/"
                    imginicio.ImageUrl = lnk & "procesos cng.jpg"

                    script = "inicio('" & lnk & "procesos cng.jpg" & "');"
                    Utilidades.RegistrarScript(script, Me)
                End If
            End If


        End If

    End Sub


End Class