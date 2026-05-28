Imports System.Reflection
Imports System.Runtime.Serialization

Public Class UtilityDB_Common
    ''' <summary>
    ''' Retorna el valor de una propiedad nombrada en un objeto genérico
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function ObjectGetProperty(ByVal obj As Object, ByVal propertyName As String) As Object
        Dim propertyValue As Object = Nothing
        Dim propertyInfo = (From prop In obj.GetType().GetProperties Where prop.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()
        If Not propertyInfo Is Nothing Then
            Return propertyInfo.GetValue(obj, Nothing)
        End If
        Dim fieldInfo = (From field In obj.GetType().GetFields Where field.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()
        If Not fieldInfo Is Nothing Then
            Return fieldInfo.GetValue(obj)
        End If
        Throw New ArgumentException("Nombre de atributo """ & propertyName & """ no definido para el tipo (" & obj.GetType.FullName & ")")
    End Function

    ''' <summary>
    ''' Asigna el valor de una propiedad nombrada en un objeto genérico
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub ObjectSetProperty(ByVal obj As Object, ByVal propertyName As String, ByVal propertyValue As Object)
        Dim propertyInfo = (From prop In obj.GetType().GetProperties Where prop.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()
        If Not propertyInfo Is Nothing Then
            propertyInfo.SetValue(obj, propertyValue, Nothing)
            Return
        End If
        Dim fieldInfo = (From field In obj.GetType().GetFields Where field.Name.Equals(propertyName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault()
        If Not fieldInfo Is Nothing Then
            fieldInfo.SetValue(obj, propertyValue)
            Return
        End If
        Throw New ArgumentException("Nombre de atributo """ & propertyName & """ no definido para el tipo (" & obj.GetType.FullName & ")")
    End Sub

    ''' <summary>
    ''' Asigna la cadena «NullString» a las propiedades vacías o nulas de tipo String en el objeto «Entidad»
    ''' </summary>
    Public Shared Sub NotNullEmptyStrings(ByVal Entidad As Object)
        NotNullEmptyStrings(Entidad, String.Empty)
    End Sub

    ''' <summary>
    ''' Asigna la cadena «NullString» a las propiedades vacías o nulas de tipo String en el objeto «Entidad»
    ''' </summary>
    Public Shared Sub NotNullEmptyStrings(ByVal Entidad As Object, ByVal NullString As String)
        Dim originValue As Object = Nothing
        For Each Prop In Entidad.GetType.GetProperties
            If Prop.PropertyType.Equals(GetType(String)) Then
                originValue = Prop.GetValue(Entidad, Nothing)
                If (originValue Is Nothing) OrElse (CStr(originValue).Trim = String.Empty) Then
                    Prop.SetValue(Entidad, NullString, Nothing)
                End If
            End If
        Next
        For Each Field In Entidad.GetType.GetFields
            If Field.FieldType.Equals(GetType(String)) Then
                originValue = Field.GetValue(Entidad)
                If (originValue Is Nothing) OrElse (CStr(originValue).Trim = String.Empty) Then
                    Field.SetValue(Entidad, NullString)
                End If
            End If
        Next
    End Sub

    ''' <summary>
    ''' «destObject» := «originObject» ... copia las propiedades del objeto «originObject» sobre el objeto «destObject» 
    ''' </summary>
    Public Shared Sub CopyObject(ByVal destObject As Object, ByVal originObject As Object, Optional ByVal excludeFields As String() = Nothing)
        Dim destProperties = destObject.GetType.GetProperties
        Dim originProperties = originObject.GetType.GetProperties
        Dim destFields = destObject.GetType.GetFields
        Dim originFields = originObject.GetType.GetFields
        Dim originProp As PropertyInfo
        Dim originField As FieldInfo
        Dim PropName, TypeName As String
        Dim originValue As Object = Nothing
        Dim excludeList As New List(Of String)

        If Not excludeFields Is Nothing Then
            excludeList.AddRange(From field In excludeFields Select field.ToLower)
        End If
        For Each destProp In destProperties
            PropName = destProp.Name
            TypeName = destProp.PropertyType.Name
            originField = originFields.Where(Function(prop) prop.Name = PropName).FirstOrDefault()
            originProp = originProperties.Where(Function(prop) prop.Name = PropName).FirstOrDefault()
            If (destProp.CanWrite) And
                (Not (TypeName.Contains("EntityKey") Or TypeName.Contains("EntityReference") Or TypeName.Contains("EntityCollection"))) And
                (Not (originField Is Nothing And originProp Is Nothing)) And
                (Not excludeList.Contains(PropName.ToLower)) Then
                ' copy Property
                If Not originField Is Nothing Then
                    originValue = originField.GetValue(originObject)
                ElseIf Not originProp Is Nothing Then
                    originValue = originProp.GetValue(originObject, Nothing)
                End If
                destProp.SetValue(destObject, originValue, Nothing)
            End If
        Next
        For Each destField In destFields
            PropName = destField.Name
            TypeName = destField.FieldType.Name
            originField = originFields.Where(Function(prop) prop.Name = PropName).FirstOrDefault()
            originProp = originProperties.Where(Function(prop) prop.Name = PropName).FirstOrDefault()
            If (Not (TypeName.Contains("EntityKey") Or TypeName.Contains("EntityReference") Or TypeName.Contains("EntityCollection"))) And
                (Not (originField Is Nothing And originProp Is Nothing)) And
                (Not excludeList.Contains(PropName.ToLower)) Then
                ' copy field
                If Not originField Is Nothing Then
                    originValue = originField.GetValue(originObject)
                ElseIf Not originProp Is Nothing Then
                    originValue = originProp.GetValue(originObject, Nothing)
                End If
                destField.SetValue(destObject, originValue)
            End If
        Next
    End Sub

    ''' <summary>
    ''' Retorna una copia idéntica del objeto transferido.
    ''' </summary>
    ''' <typeparam name="T">Tipo del objeto transferido</typeparam>
    ''' <param name="obj">Objeto a duplicar.</param>
    Public Shared Function ClonarEntidad(Of T)(obj As T) As T
        Dim dcSer As New DataContractSerializer(obj.[GetType]())
        Dim memoryStream As New IO.MemoryStream()

        dcSer.WriteObject(memoryStream, obj)
        memoryStream.Position = 0

        Dim newObject As T = DirectCast(dcSer.ReadObject(memoryStream), T)
        Return newObject
    End Function

    Public Shared Function ReadFullStream(ByVal stream As IO.Stream) As Byte()
        Dim buffer(32768) As Byte
        Using ms As New IO.MemoryStream()
            While (True)
                Dim read As Integer = stream.Read(buffer, 0, buffer.Length)
                If (read <= 0) Then
                    Return ms.ToArray()
                End If
                ms.Write(buffer, 0, read)
            End While
        End Using
        Return (New Byte() {})
    End Function

    ''' <summary>
    ''' Retorna True si el argumento es una direccion de correo válida
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Function IsValidEmail(strIn As String) As Boolean
        Try
            Return Text.RegularExpressions.Regex.IsMatch(strIn,
                   "^(?("")(""[^""]+?""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                   "(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9]{2,17}))$",
                   Text.RegularExpressions.RegexOptions.IgnoreCase)
        Catch e As Exception
            Return False
        End Try
    End Function

    Public Shared Function ConvertDictionary(Dictionary1 As IOrderedDictionary) As Dictionary(Of String, Object)
        Dim NewDictionary As New Dictionary(Of String, Object)(StringComparer.InvariantCultureIgnoreCase)
        For Each entry As DictionaryEntry In Dictionary1
            NewDictionary.Add(entry.Key, entry.Value)
        Next
        Return NewDictionary
    End Function
End Class
