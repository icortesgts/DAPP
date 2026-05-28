Imports System
Imports System.Data
Imports System.Web.Security
Imports System.Configuration
Imports Microsoft.VisualBasic.Information

Partial Public Class Login
    Inherits Page

    Public ReadOnly Property AppGlobal As New AppGlobal_Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Session("Usuario") = Nothing
            Session("id_cliente") = Nothing
            Session("id_sucursal") = Nothing

            Using ActiveConnection As New SqlClient.SqlConnection(AppGlobal.DefaultConnection)
                Dim TestBd = ActiveConnection.State
            End Using

        Catch ex As Exception
            cvLogin.IsValid = False
            cvLogin.ErrorMessage = "La base de datos no está disponible en este momento. Intente más tarde."
            Return
        End Try

    End Sub

    Protected Sub LoginButton_Click(sender As Object, e As EventArgs) Handles LoginButton.Click
        If Page.IsValid() Then
            Try
                DoLogin()
                Session("Usuario") = User.Identity.Name
            Catch ex As AccessViolationException
                cvLogin.ErrorMessage = "Login failed. Please try again. " + ex.Message
                cvLogin.IsValid = False
            End Try
        End If
    End Sub

    Private Sub DoLogin()
        Dim ObjPwd As Object = Nothing
        Dim ObjDateReset As Object = Nothing
        Dim PasswordExpiration As Integer = ConfigurationManager.AppSettings("PasswordExpiration")
        Dim PasswordExpired As Boolean
        Dim PasswordDb As String

        Using ActiveConnection As New SqlClient.SqlConnection(AppGlobal.DefaultConnection)
            Try
                ActiveConnection.Open()
            Catch ex As Exception
                cvLogin.IsValid = False
                cvLogin.ErrorMessage = "La base de datos no está disponible en este momento. Intente más tarde."
                Return
            End Try

            ' recuperar usuario de BD
            Dim cmd As New SqlClient.SqlCommand("PasswordGet", ActiveConnection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = UserName.Text
            Dim cmdReader = cmd.ExecuteReader()
            If cmdReader.HasRows Then
                cmdReader.Read()
                ObjPwd = cmdReader("password")
                ObjDateReset = cmdReader("fecha_password")
            Else
                Throw New AccessViolationException("Usuario no encontrado")
            End If

            ' validar constraseña
            If Not IsDBNull(ObjPwd) Then
                PasswordDb = CStr(ObjPwd)
                If Not PasswordHash.ValidatePassword(Password.Text, PasswordDb) Then
                    Throw New AccessViolationException("Password no coincide")
                End If
            Else
                Throw New AccessViolationException("Password esta vacio")
            End If

            ' validar caducidad de contraseña
            If Not IsDBNull(ObjDateReset) Then
                Dim DateReset As Date = CDate(ObjDateReset)
                PasswordExpired = (PasswordExpiration > 0 AndAlso DateReset.AddDays(PasswordExpiration) <= Today)
            Else
                PasswordExpired = True
            End If

            ' iniciar o solicitar cambio de contraseña
            If Not PasswordExpired Then
                GoToApplication()
            Else
                Response.Redirect(String.Format("ChangePassword.aspx?ReturnUrl={0}&usuario={1}", Request.QueryString("ReturnUrl"), UserName.Text), True)
            End If
        End Using
    End Sub

    Private Sub GoToApplication()
        FormsAuthentication.RedirectFromLoginPage(UserName.Text, CheckRemember.Checked)
    End Sub
End Class
