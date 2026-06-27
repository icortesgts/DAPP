Imports System.Web.Script.Serialization

Public Class Dashboard
    Inherits System.Web.UI.Page

    ' ════════════════════════════════════════════════════════════════════════════
    '  Estado en memoria (dummy) de las 3 fuentes del dashboard
    '  TODO: reemplazar por consultas reales a BD cuando se integre EF / ADO
    ' ════════════════════════════════════════════════════════════════════════════

    Private listaPrestamos As List(Of PrestamoDashboardDTO)
    Private listaArchivos As List(Of ArchivoDashboardDTO)
    Private listaChequeos As List(Of ChequeoDashboardDTO)

    ' ── Page Load ────────────────────────────────────────────────────────────────

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs)
        CargarDatosDummy()
        ActualizarKPIs()
        lblFechaActualizacion.Text = "Última actualización: " & Now.ToString("dd/MM/yyyy HH:mm")
    End Sub

    ' ── Botón Actualizar ─────────────────────────────────────────────────────────

    Protected Sub btnActualizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        CargarDatosDummy(randomizar:=True)
        ActualizarKPIs()
        lblFechaActualizacion.Text = "Última actualización: " & Now.ToString("dd/MM/yyyy HH:mm")
    End Sub

    ' ════════════════════════════════════════════════════════════════════════════
    '  Carga de datos dummy
    '  TODO (a futuro, cuando se integre BD):
    '    - Préstamos     → SELECT de Solicitudes + terceros + ubicaciones
    '                       filtrando por @sucursal = Session("id_sucursal")
    '    - Archivos      → SELECT de documentos + tipoDoc_TablaRetencion + ubicaciones
    '    - Lista Chequeo → SELECT de terceros + Documentos_requeridos + procesos
    ' ════════════════════════════════════════════════════════════════════════════

    Private Sub CargarDatosDummy(Optional randomizar As Boolean = False)

        Dim rnd As New Random()

        ' ── Préstamos ────────────────────────────────────────────────────────────
        Dim baseDias As Integer() = {12, 5, 0, -3, 8, -1, 3, -5}
        Dim documentosPrestamo As String() = {
            "CONTRATO_SERVICIOS_0045.pdf", "HOJA_VIDA_0012.pdf", "PODER_NOTARIAL_0078.pdf",
            "ACTA_REUNION_0033.pdf", "CARTA_INSTRUCCIONES_0099.pdf", "CONTRATO_ARRENDAMIENTO_0061.pdf",
            "CERTIFICADO_LABORAL_0027.pdf", "PAGARE_0014.pdf"
        }
        Dim carpetas As String() = {
            "Carpeta Comercial 045", "Carpeta RRHH 012", "Carpeta Legal 078", "Carpeta Actas 033",
            "Carpeta Clientes 099", "Carpeta Inmuebles 061", "Carpeta RRHH 027", "Carpeta Financiera 014"
        }

        listaPrestamos = New List(Of PrestamoDashboardDTO)
        For i As Integer = 0 To baseDias.Length - 1
            Dim dias As Integer = baseDias(i)
            If randomizar Then dias += rnd.Next(-2, 4)
            listaPrestamos.Add(New PrestamoDashboardDTO With {
                .Documento = documentosPrestamo(i),
                .Carpeta = carpetas(i),
                .FechaPrestamo = Date.Today.AddDays(-15 - i),
                .FechaEstimada = Date.Today.AddDays(-dias),
                .DiasRetraso = dias,
                .Estado = ObtenerEstadoPorDias(dias)
            })
        Next

        ' ── Archivos (Permanencia adicional por etapa, en meses) ────────────────
        listaArchivos = New List(Of ArchivoDashboardDTO) From {
            New ArchivoDashboardDTO With {.Documento = "CONTRATO_PRESTACION_0023.pdf", .TipoDocumento = "Contratos", .DifGestion = 3, .DifCentral = -2, .DifHistorico = -12},
            New ArchivoDashboardDTO With {.Documento = "ACTA_ACADEMICA_0089.pdf", .TipoDocumento = "Actas Académicas", .DifGestion = -1, .DifCentral = 5, .DifHistorico = -6},
            New ArchivoDashboardDTO With {.Documento = "COMUNICACION_EXTERNA_0150.pdf", .TipoDocumento = "Comunicaciones Externas", .DifGestion = 0, .DifCentral = 0, .DifHistorico = 8},
            New ArchivoDashboardDTO With {.Documento = "CONTRATO_TERCEROS_0067.pdf", .TipoDocumento = "Contrato Terceros", .DifGestion = 6, .DifCentral = 2, .DifHistorico = -4},
            New ArchivoDashboardDTO With {.Documento = "INFORME_ESCOLAR_0112.pdf", .TipoDocumento = "Informe Escolar", .DifGestion = -3, .DifCentral = -1, .DifHistorico = -9},
            New ArchivoDashboardDTO With {.Documento = "PQRS_0204.pdf", .TipoDocumento = "PQRS", .DifGestion = 2, .DifCentral = 0, .DifHistorico = -7},
            New ArchivoDashboardDTO With {.Documento = "ARCHIVO_AUDIO_0019.mp4", .TipoDocumento = "Archivo Digital (Audio / Video)", .DifGestion = -2, .DifCentral = 4, .DifHistorico = 1},
            New ArchivoDashboardDTO With {.Documento = "ACTA_ACADEMICA_0140.pdf", .TipoDocumento = "Actas Académicas", .DifGestion = 1, .DifCentral = -3, .DifHistorico = -10}
        }
        If randomizar Then
            For Each a In listaArchivos
                a.DifGestion += rnd.Next(-1, 2)
                a.DifCentral += rnd.Next(-1, 2)
                a.DifHistorico += rnd.Next(-1, 2)
            Next
        End If

        ' ── Listas de Chequeo ────────────────────────────────────────────────────
        listaChequeos = New List(Of ChequeoDashboardDTO) From {
            New ChequeoDashboardDTO With {.Tercero = "PEREZ RUIZ JUAN DAVID", .Proceso = "Vinculación Laboral", .TipoDocumentoRequerido = "Hoja de Vida", .Cantidad = 1, .NroActual = 1, .Faltantes = 0, .Estado = "Cumplido"},
            New ChequeoDashboardDTO With {.Tercero = "PEREZ RUIZ JUAN DAVID", .Proceso = "Vinculación Laboral", .TipoDocumentoRequerido = "Certificado Médico", .Cantidad = 1, .NroActual = 0, .Faltantes = 1, .Estado = "Pendiente"},
            New ChequeoDashboardDTO With {.Tercero = "GARCIA MUÑOZ VALERIA", .Proceso = "Vinculación Laboral", .TipoDocumentoRequerido = "Certificado Judicial", .Cantidad = 1, .NroActual = 1, .Faltantes = 0, .Estado = "Cumplido"},
            New ChequeoDashboardDTO With {.Tercero = "GARCIA MUÑOZ VALERIA", .Proceso = "Matrícula Académica", .TipoDocumentoRequerido = "Diploma Bachiller", .Cantidad = 1, .NroActual = 0, .Faltantes = 1, .Estado = "Pendiente"},
            New ChequeoDashboardDTO With {.Tercero = "LOPEZ HERRERA MATEO", .Proceso = "Matrícula Académica", .TipoDocumentoRequerido = "Registro Civil", .Cantidad = 1, .NroActual = 1, .Faltantes = 0, .Estado = "Cumplido"},
            New ChequeoDashboardDTO With {.Tercero = "LOPEZ HERRERA MATEO", .Proceso = "Contratación Proveedores", .TipoDocumentoRequerido = "RUT", .Cantidad = 1, .NroActual = 0, .Faltantes = 1, .Estado = "Pendiente"},
            New ChequeoDashboardDTO With {.Tercero = "TORRES JIMENEZ SOFIA", .Proceso = "Contratación Proveedores", .TipoDocumentoRequerido = "Cámara de Comercio", .Cantidad = 1, .NroActual = 1, .Faltantes = 0, .Estado = "Cumplido"},
            New ChequeoDashboardDTO With {.Tercero = "TORRES JIMENEZ SOFIA", .Proceso = "Matrícula Académica", .TipoDocumentoRequerido = "Foto 3x4", .Cantidad = 2, .NroActual = 0, .Faltantes = 2, .Estado = "Pendiente"},
            New ChequeoDashboardDTO With {.Tercero = "RIVERA CASTRO NICOLAS", .Proceso = "Contratación Proveedores", .TipoDocumentoRequerido = "Certificación Bancaria", .Cantidad = 1, .NroActual = 0, .Faltantes = 1, .Estado = "Pendiente"}
        }
        If randomizar Then
            For Each c In listaChequeos
                If rnd.Next(0, 3) = 0 Then
                    If c.Estado = "Cumplido" Then
                        c.NroActual = c.Cantidad - 1 : c.Faltantes = 1 : c.Estado = "Pendiente"
                    Else
                        c.NroActual = c.Cantidad : c.Faltantes = 0 : c.Estado = "Cumplido"
                    End If
                End If
            Next
        End If

    End Sub

    Private Function ObtenerEstadoPorDias(dias As Integer) As String
        If dias > 0 Then Return "Vencido"
        If dias = 0 Then Return "Por Vencer"
        Return "Vigente"
    End Function

    ' ════════════════════════════════════════════════════════════════════════════
    '  KPIs + JSON para gráficos — se establecen en los Literals del markup
    ' ════════════════════════════════════════════════════════════════════════════

    Private Sub ActualizarKPIs()
        ' ── KPIs ──
        litKpiPrestamosVigentes.Text  = listaPrestamos.Where(Function(p) p.Estado = "Vigente").Count().ToString()
        litKpiPrestamosVencidos.Text  = listaPrestamos.Where(Function(p) p.Estado = "Vencido").Count().ToString()
        litKpiArchivosPendientes.Text = listaArchivos.Where(Function(a) a.DifGestion > 0 OrElse a.DifCentral > 0 OrElse a.DifHistorico > 0).Count().ToString()
        litKpiChequeosPendientes.Text = listaChequeos.Where(Function(c) c.Estado = "Pendiente").Count().ToString()

        ' ── JSON para Chart.js ──
        litJsonEstadoPrestamos.Text  = "<script>var __json_EstadoPrestamos="  & JsonEstadoPrestamos()  & ";</" & "script>"
        litJsonTopRetraso.Text       = "<script>var __json_TopRetraso="       & JsonTopRetrasoPrestamos()  & ";</" & "script>"
        litJsonPendientesEtapa.Text  = "<script>var __json_PendientesEtapa="  & JsonPendientesPorEtapa()  & ";</" & "script>"
        litJsonTopPermanencia.Text   = "<script>var __json_TopPermanencia="   & JsonTopPermanenciaAdicional() & ";</" & "script>"
        litJsonEstadoChequeos.Text   = "<script>var __json_EstadoChequeos="   & JsonEstadoChequeos()   & ";</" & "script>"
        litJsonFaltantesProceso.Text = "<script>var __json_FaltantesProceso=" & JsonFaltantesPorProceso() & ";</" & "script>"
    End Sub

    ' ════════════════════════════════════════════════════════════════════════════
    '  JSON para los gráficos (Chart.js) — Private, sólo usados desde ActualizarKPIs
    ' ════════════════════════════════════════════════════════════════════════════

    Private Function JsonEstadoPrestamos() As String
        Dim vigentes = listaPrestamos.Where(Function(p) p.Estado = "Vigente").Count()
        Dim porVencer = listaPrestamos.Where(Function(p) p.Estado = "Por Vencer").Count()
        Dim vencidos = listaPrestamos.Where(Function(p) p.Estado = "Vencido").Count()
        Dim dto As New DatosGraficoDTO With {
            .Labels = New List(Of String) From {"Vigente", "Por Vencer", "Vencido"},
            .Data = New List(Of Integer) From {vigentes, porVencer, vencidos},
            .Colores = New List(Of String) From {"#5cb85c", "#f0ad4e", "#d9534f"}
        }
        Return New JavaScriptSerializer().Serialize(dto)
    End Function

    Private Function JsonTopRetrasoPrestamos() As String
        Dim top = listaPrestamos.
            Where(Function(p) p.DiasRetraso > 0).
            OrderByDescending(Function(p) p.DiasRetraso).
            Take(5).ToList()
        Dim dto As New DatosGraficoDTO With {
            .Labels = top.Select(Function(p) p.Documento).ToList(),
            .Data = top.Select(Function(p) p.DiasRetraso).ToList(),
            .Colores = New List(Of String) From {"#d9534f"}
        }
        Return New JavaScriptSerializer().Serialize(dto)
    End Function

    Private Function JsonPendientesPorEtapa() As String
        Dim gestion = listaArchivos.Where(Function(a) a.DifGestion > 0).Count()
        Dim central = listaArchivos.Where(Function(a) a.DifCentral > 0).Count()
        Dim historico = listaArchivos.Where(Function(a) a.DifHistorico > 0).Count()
        Dim dto As New DatosGraficoDTO With {
            .Labels = New List(Of String) From {"Arch. Gestión", "Arch. Central", "Arch. Histórico"},
            .Data = New List(Of Integer) From {gestion, central, historico},
            .Colores = New List(Of String) From {"#4a90d9", "#337ab7", "#2c5d8a"}
        }
        Return New JavaScriptSerializer().Serialize(dto)
    End Function

    Private Function JsonTopPermanenciaAdicional() As String
        Dim top = listaArchivos.
            Select(Function(a) New With {
                a.Documento,
                .Total = Math.Max(a.DifGestion, 0) + Math.Max(a.DifCentral, 0) + Math.Max(a.DifHistorico, 0)
            }).
            Where(Function(x) x.Total > 0).
            OrderByDescending(Function(x) x.Total).
            Take(5).ToList()
        Dim dto As New DatosGraficoDTO With {
            .Labels = top.Select(Function(x) x.Documento).ToList(),
            .Data = top.Select(Function(x) x.Total).ToList(),
            .Colores = New List(Of String) From {"#f0ad4e"}
        }
        Return New JavaScriptSerializer().Serialize(dto)
    End Function

    Private Function JsonEstadoChequeos() As String
        Dim cumplidos = listaChequeos.Where(Function(c) c.Estado = "Cumplido").Count()
        Dim pendientes = listaChequeos.Where(Function(c) c.Estado = "Pendiente").Count()
        Dim dto As New DatosGraficoDTO With {
            .Labels = New List(Of String) From {"Cumplido", "Pendiente"},
            .Data = New List(Of Integer) From {cumplidos, pendientes},
            .Colores = New List(Of String) From {"#5cb85c", "#d9534f"}
        }
        Return New JavaScriptSerializer().Serialize(dto)
    End Function

    Private Function JsonFaltantesPorProceso() As String
        Dim grupos = listaChequeos.
            GroupBy(Function(c) c.Proceso).
            Select(Function(g) New With {.Proceso = g.Key, .Total = g.Sum(Function(c) c.Faltantes)}).
            OrderByDescending(Function(x) x.Total).ToList()
        Dim dto As New DatosGraficoDTO With {
            .Labels = grupos.Select(Function(g) g.Proceso).ToList(),
            .Data = grupos.Select(Function(g) g.Total).ToList(),
            .Colores = New List(Of String) From {"#337ab7"}
        }
        Return New JavaScriptSerializer().Serialize(dto)
    End Function

End Class
