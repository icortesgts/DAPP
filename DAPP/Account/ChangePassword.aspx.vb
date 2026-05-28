Public Class ChangePassword
    Inherits System.Web.UI.Page

    Public ReadOnly Property AppGlobal As New AppGlobal_Class

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            UserName.Text = User.Identity.Name
        End If
    End Sub

    Public ReadOnly Property GetPoliciesString()
        Get
            Return AppGlobal_Class.PasswordPoliciesString
        End Get
    End Property

    Protected Sub ButtonOk_Click(sender As Object, e As EventArgs) Handles ButtonOk.Click
        If Not AppGlobal.ValidatePasswordPolicies(NewPassword.Text) Then
            cvLogin.IsValid = False
            cvLogin.ErrorMessage = AppGlobal_Class.PasswordPoliciesString
        End If
        If Page.IsValid() Then
            Try
                DoChangePassword()
            Catch ex As AccessViolationException
                cvLogin.ErrorMessage = "Login failed. Please try again."
                cvLogin.IsValid = False
            End Try
        End If
    End Sub

    Protected Sub ButtonCancel_Click(sender As Object, e As EventArgs) Handles ButtonCancel.Click
        FormsAuthentication.RedirectFromLoginPage(UserName.Text, False)
    End Sub

    Private Sub DoChangePassword()
        Dim cmd As SqlClient.SqlCommand
        Dim ObjPwd As Object = Nothing
        Dim ObjFecha As Object = Nothing
        Dim PasswordDb As String

        Using ActiveConnection As New SqlClient.SqlConnection(AppGlobal.DefaultConnection)
            Try
                ActiveConnection.Open()
            Catch ex As Exception
                Throw New AccessViolationException("La base de datos no está disponible en este momento. Intente más tarde.")
            End Try

            ' validate current PWD
            cmd = New SqlClient.SqlCommand("PasswordGet", ActiveConnection)
            cmd.CommandType = CommandType.StoredProcedure
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = UserName.Text
            Dim cmdReader = cmd.ExecuteReader()
            If cmdReader.HasRows Then
                cmdReader.Read()
                ObjPwd = cmdReader("password")
                ObjFecha = cmdReader("fecha_password")
                If Not IsDBNull(ObjPwd) Then
                    PasswordDb = CStr(ObjPwd)
                    If Not PasswordHash.ValidatePassword(CurrentPassword.Text, PasswordDb) Then
                        Throw New AccessViolationException("La contraseña actual no es correcta")
                    End If
                Else
                    Throw New AccessViolationException("La contraseña actual no es correcta")
                End If
            Else
                Throw New AccessViolationException("User not found")
            End If
            cmdReader.Close()

            ' cambiar pwd en BD
            cmd = New SqlClient.SqlCommand("PasswordChange", ActiveConnection)
            cmd.CommandType = CommandType.StoredProcedure
            Dim ParamReturn = cmd.Parameters.Add("RetVal", SqlDbType.Int)
            ParamReturn.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = UserName.Text
            cmd.Parameters.Add("@old_password", SqlDbType.VarChar).Value = DBNull.Value
            cmd.Parameters.Add("@new_password", SqlDbType.VarChar).Value = PasswordHash.CreateHash(NewPassword.Text)
            cmd.ExecuteNonQuery()
            Select Case ParamReturn.Value
                Case 0
                    GoToApplication()
                Case 1
                    cvLogin.ErrorMessage = "Usuario no registrado en el sistema."
                    cvLogin.IsValid = False
                Case 2
                    cvLogin.ErrorMessage = "La contraseña actual no es correcta."
                    cvLogin.IsValid = False
            End Select
        End Using
    End Sub

    Private Sub GoToApplication()
        FormsAuthentication.RedirectFromLoginPage(UserName.Text, False)
    End Sub

End Class