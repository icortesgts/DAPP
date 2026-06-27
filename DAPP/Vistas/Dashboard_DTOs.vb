' ════════════════════════════════════════════════════════════════════════════
'  DTOs del Dashboard — separados de la clase Page para evitar conflictos
'  con el compilador de ASP.NET Web Forms.
' ════════════════════════════════════════════════════════════════════════════

Public Class PrestamoDashboardDTO
    Public Property Solicitante As String
    Public Property Carpeta As String
    Public Property Documento As String
    Public Property FechaPrestamo As Date
    Public Property FechaEstimada As Date
    Public Property DiasRetraso As Integer
    Public Property Estado As String          ' Vigente / Por Vencer / Vencido
End Class

Public Class ArchivoDashboardDTO
    Public Property Documento As String
    Public Property TipoDocumento As String
    Public Property DifGestion As Integer      ' Permanencia adicional A. Gestión (meses)
    Public Property DifCentral As Integer      ' Permanencia adicional A. Central (meses)
    Public Property DifHistorico As Integer    ' Permanencia adicional A. Histórico (meses)
End Class

Public Class ChequeoDashboardDTO
    Public Property Tercero As String
    Public Property Proceso As String
    Public Property TipoDocumentoRequerido As String
    Public Property Cantidad As Integer
    Public Property NroActual As Integer
    Public Property Faltantes As Integer
    Public Property Estado As String           ' Cumplido / Pendiente
End Class

' DTO genérico para alimentar los gráficos (Chart.js) en formato Labels/Data/Colores
Public Class DatosGraficoDTO
    Public Property Labels As List(Of String)
    Public Property Data As List(Of Integer)
    Public Property Colores As List(Of String)
End Class
