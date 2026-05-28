
Imports System.Collections.ObjectModel

Public Class AppGlobal_Class

    Public Shared Property PasswordMinLength As Integer = ConfigurationManager.AppSettings("PasswordMinLength")
    Public Shared Property PasswordMaxLength As Integer = ConfigurationManager.AppSettings("PasswordMaxLength")
    Public Shared Property PasswordPoliciesString = "La nueva contraseña debe tener entre " & PasswordMinLength & " y " & PasswordMaxLength & " caracteres, contener una mayúscula y un carácter especial (p.e. @$^&)."
    Public Shared Property CamposCubosWorkflow As String() = {"empresa", "solicitante", "responsable", "fecha_creacion", "fecha_actualizacion", "urgencia", "proceso", "estado", "descripcion", "esCerrado", "fecha_estimada_estado", "fecha_estimada", "notificacion", "idEmpresa"}

    Public Const CryptographyKey As String = "CASSINI@2004"

    Public ReadOnly Property DefaultConnection() As String
        Get
            Return ConfigurationManager.ConnectionStrings("DefaultConnection").ConnectionString
        End Get
    End Property

    ''' <summary>
    ''' Determines if a password is sufficiently complex.
    ''' </summary>
    ''' <param name="pwd">Password to validate</param>
    ''' <returns>True if the password is sufficiently complex.</returns> 
    Public Function ValidatePasswordPolicies(ByVal pwd As String)
        Return ValidatePassword_local(pwd, PasswordMinLength, PasswordMaxLength, numUpper:=1, numLower:=1, numNumbers:=0, numSpecial:=1)
    End Function

    ''' <summary>Determines if a password is sufficiently complex.</summary> 
    ''' <param name="pwd">Password to validate</param> 
    ''' <param name="minLength">Minimum number of password characters.</param> 
    ''' <param name="numUpper">Minimum number of uppercase characters.</param> 
    ''' <param name="numLower">Minimum number of lowercase characters.</param> 
    ''' <param name="numNumbers">Minimum number of numeric characters.</param> 
    ''' <param name="numSpecial">Minimum number of special characters.</param> 
    ''' <returns>True if the password is sufficiently complex.</returns> 
    Private Shared Function ValidatePassword_local(
        ByVal pwd As String,
        ByVal minLength As Integer,
        ByVal maxLength As Integer,
        ByVal numUpper As Integer,
        ByVal numLower As Integer,
        ByVal numNumbers As Integer,
        ByVal numSpecial As Integer) As Boolean

        ' Replace [A-Z] with \p{Lu}, to allow for Unicode uppercase letters. 
        Dim upper As New System.Text.RegularExpressions.Regex("[A-Z]")
        Dim lower As New System.Text.RegularExpressions.Regex("[a-z]")
        Dim number As New System.Text.RegularExpressions.Regex("[0-9]")
        ' Special is "none of the above". 
        Dim special As New System.Text.RegularExpressions.Regex("[^a-zA-Z0-9]")

        ' Check the length. 
        If Len(pwd) < minLength Then Return False
        If Len(pwd) > maxLength Then Return False
        ' Check for minimum number of occurrences. 
        If upper.Matches(pwd).Count < numUpper Then Return False
        If lower.Matches(pwd).Count < numLower Then Return False
        If number.Matches(pwd).Count < numNumbers Then Return False
        If special.Matches(pwd).Count < numSpecial Then Return False

        ' Passed all checks. 
        Return True
    End Function


    Public Shared Sub MessageBox(ByVal msg As String, ByRef parent As Page)
        Dim lbl As New Label
        lbl.Text = "<script language='javascript'>" & Environment.NewLine &
               "window.alert('" + msg + "')</script>"
        parent.Controls.Add(lbl)
    End Sub
End Class
