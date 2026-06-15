Imports System.Web.Script.Serialization

Public Class FlujoProceso
    Inherits System.Web.UI.Page

    ' ── Entidades dummy (reemplazar con las del Entity Framework cuando se integre BD) ──

    Public Class EventoFlujoDTO
        Public Property eventoId As Integer
        Public Property nombre As String
        Public Property obligatorio As Boolean
        Public Property activo As Boolean
        Public Property roles As List(Of String)
        Public Property usuarios As List(Of String)
        Public Property dias As Integer
    End Class

    Public Class ResultadoOperacion
        Public Property Exito As Boolean
        Public Property Mensaje As String
    End Class

    ' ── Page Load ────────────────────────────────────────────────────────────────────

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            ' Verificar acceso (mismo patrón que IndexarDocumento)
            ' Dim Usuario As Usuarios = Utilidades.ConsultarUsuario(User.Identity.Name)
            ' If Usuario Is Nothing Then
            '     Server.Transfer("../Vistas/SinAcceso.aspx?mensaje=El usuario no tiene acceso a esta operacion")
            ' End If

            ' Limpia mensajes
            lblMsgFlujo.Text = ""
            lblMsgGen.Text = ""
        End If
    End Sub

    ' ── Controlador: Guardar Flujo ────────────────────────────────────────────────────
    '
    '   Recibe desde el cliente (via HiddenFields):
    '     hddTipoId    → id del tipo de documento seleccionado
    '     hddFlujoDatos → JSON con los eventos configurados
    '
    '   Lo que haría con BD (comentado, para activar cuando se integre):
    '     1. Borrar configuracion previa del tipo en FlujoProceso_Eventos
    '     2. Insertar los eventos activos con sus responsables y dias
    '     3. Registrar auditoría

    Protected Sub btGuardarFlujo_Click(sender As Object, e As EventArgs) Handles btGuardarFlujo.Click
        Dim resultado As New ResultadoOperacion

        Try
            Dim idTipo As Integer = 0
            If Not Integer.TryParse(hddTipoId.Value, idTipo) OrElse idTipo = 0 Then
                resultado.Exito = False
                resultado.Mensaje = "Debe seleccionar un tipo de documento."
                MostrarMensajeFlujo(resultado)
                Return
            End If

            ' Deserializar JSON enviado desde el cliente
            Dim json As String = hddFlujoDatos.Value
            If String.IsNullOrEmpty(json) Then
                resultado.Exito = False
                resultado.Mensaje = "No hay datos de flujo para guardar."
                MostrarMensajeFlujo(resultado)
                Return
            End If

            Dim serializer As New JavaScriptSerializer()
            Dim eventos As List(Of EventoFlujoDTO) = serializer.Deserialize(Of List(Of EventoFlujoDTO))(json)

            ' ── Validaciones de negocio ──────────────────────────
            resultado = ValidarFlujo(eventos)
            If Not resultado.Exito Then
                MostrarMensajeFlujo(resultado)
                Return
            End If

            ' ── Aquí iría la persistencia en BD ──────────────────
            ' Using db As New DAPP_BDEntities
            '     ' 1. Eliminar configuracion previa
            '     Dim previos = db.FlujoProceso_Eventos.Where(Function(f) f.id_tipo = idTipo).ToList()
            '     db.FlujoProceso_Eventos.RemoveRange(previos)
            '
            '     ' 2. Insertar eventos activos
            '     For Each ev In eventos.Where(Function(x) x.activo)
            '         Dim nuevo As New FlujoProceso_Eventos
            '         nuevo.id_tipo     = idTipo
            '         nuevo.id_evento   = ev.eventoId
            '         nuevo.dias_plazo  = ev.dias
            '         nuevo.fecha_mod   = Today
            '         nuevo.usuario_mod = User.Identity.Name
            '         db.FlujoProceso_Eventos.Add(nuevo)
            '         db.SaveChanges()
            '
            '         ' Responsables
            '         For Each rol In ev.roles
            '             db.FlujoProceso_Roles.Add(New FlujoProceso_Roles With {
            '                 .id_evento_flujo = nuevo.id, .rol = rol })
            '         Next
            '         For Each usr In ev.usuarios
            '             db.FlujoProceso_Usuarios.Add(New FlujoProceso_Usuarios With {
            '                 .id_evento_flujo = nuevo.id, .usuario = usr })
            '         Next
            '     Next
            '     db.SaveChanges()
            '
            '     ' 3. Auditoria
            '     Utilidades.RegistrarAuditoria("FlujoProceso", idTipo, "Tipo " & idTipo, "Configuracion flujo", User.Identity.Name, Session("id_sucursal"))
            ' End Using

            ' ── Dummy: simular guardado exitoso ──────────────────
            resultado.Exito = True
            resultado.Mensaje = "Flujo guardado correctamente para el tipo de documento " & idTipo & "."
            MostrarMensajeFlujo(resultado)

            ' Registrar auditoria (cuando exista el metodo)
            ' Utilidades.RegistrarAuditoria("FlujoProceso", idTipo, "Flujo Proceso", "Guardado", User.Identity.Name, Session("id_sucursal"))

        Catch ex As Exception
            resultado.Exito = False
            resultado.Mensaje = "Error al guardar el flujo: " & ex.Message
            MostrarMensajeFlujo(resultado)
        End Try
    End Sub

    ' ── Controlador: Generar Documentos ──────────────────────────────────────────────
    '
    '   Recibe desde el cliente:
    '     hddTipoIdGen  → id del tipo de documento
    '     hddPlantilla  → HTML de la plantilla con tags {{...}}
    '
    '   Lo que haría con BD/servicios (comentado):
    '     1. Obtener documentos del tipo con su estado/metadatos
    '     2. Reemplazar tags en la plantilla por cada documento
    '     3. Generar archivos en carpeta del servidor
    '     4. Enviar por correo via SendGrid

    Protected Sub btGenerarDocs_Click(sender As Object, e As EventArgs) Handles btGenerarDocs.Click
        Dim resultado As New ResultadoOperacion

        Try
            Dim idTipo As Integer = 0
            Integer.TryParse(hddTipoIdGen.Value, idTipo)

            Dim plantilla As String = hddPlantilla.Value
            If String.IsNullOrEmpty(plantilla) Then
                resultado.Exito = False
                resultado.Mensaje = "La plantilla no puede estar vacia."
                MostrarMensajeGen(resultado)
                Return
            End If

            ' ── Aquí iría la generación real ─────────────────────
            ' Dim documentos = (From d In db.Documentos
            '                   Where d.id_tipo = idTipo And d.Activo = True
            '                   Select d).ToList()
            '
            ' Dim carpetaSalida As String = Server.MapPath("~/Generados/")
            ' If Not Directory.Exists(carpetaSalida) Then Directory.CreateDirectory(carpetaSalida)
            '
            ' For Each doc In documentos
            '     Dim contenido As String = ReemplazarTags(plantilla, doc)
            '     Dim rutaArchivo As String = carpetaSalida & "Doc_" & doc.id & ".html"
            '     File.WriteAllText(rutaArchivo, contenido, Encoding.UTF8)
            '     EnviarPorSendGrid(doc.correo_responsable, "Documento " & doc.alias, contenido)
            ' Next

            ' ── Dummy: simular generación exitosa ─────────────────
            Dim cantidadSimulada As Integer = 5 ' simulado
            resultado.Exito = True
            resultado.Mensaje = cantidadSimulada & " documento(s) generados y enviados por correo via SendGrid."
            MostrarMensajeGen(resultado)

        Catch ex As Exception
            resultado.Exito = False
            resultado.Mensaje = "Error al generar documentos: " & ex.Message
            MostrarMensajeGen(resultado)
        End Try
    End Sub

    ' ── Validaciones de negocio ───────────────────────────────────────────────────────

    Private Function ValidarFlujo(eventos As List(Of EventoFlujoDTO)) As ResultadoOperacion
        Dim r As New ResultadoOperacion With { .Exito = True, .Mensaje = "" }

        ' Regla 1: Radicacion siempre debe estar activa (eventoId = 1)
        Dim radicacion = eventos.FirstOrDefault(Function(e) e.eventoId = 1)
        If radicacion Is Nothing OrElse Not radicacion.activo Then
            r.Exito = False
            r.Mensaje = "El evento 'Radicacion' es obligatorio y no puede desactivarse."
            Return r
        End If

        ' Regla 2: Radicacion debe tener al menos un responsable
        If radicacion.roles.Count = 0 AndAlso radicacion.usuarios.Count = 0 Then
            r.Exito = False
            r.Mensaje = "El evento 'Radicacion' debe tener al menos un rol o usuario responsable."
            Return r
        End If

        ' Regla 3: Radicacion debe tener dias de plazo > 0
        If radicacion.dias <= 0 Then
            r.Exito = False
            r.Mensaje = "El evento 'Radicacion' debe tener un plazo mayor a 0 dias."
            Return r
        End If

        ' Regla 4: Eventos activos deben tener responsable y dias
        For Each ev In eventos.Where(Function(x) x.activo AndAlso x.eventoId <> 1)
            If ev.roles.Count = 0 AndAlso ev.usuarios.Count = 0 Then
                r.Exito = False
                r.Mensaje = "El evento '" & ev.nombre & "' esta activo pero no tiene responsables asignados."
                Return r
            End If
            If ev.dias <= 0 Then
                r.Exito = False
                r.Mensaje = "El evento '" & ev.nombre & "' esta activo pero no tiene dias de plazo definidos."
                Return r
            End If
        Next

        Return r
    End Function

    ' ── Helper: Reemplazar tags en plantilla ─────────────────────────────────────────
    '   (Se usará cuando se integre BD)

    Private Function ReemplazarTags(plantilla As String, doc As Object) As String
        ' Dim resultado As String = plantilla
        ' resultado = resultado.Replace("{{usuario}}",         doc.usuario)
        ' resultado = resultado.Replace("{{area}}",            doc.area)
        ' resultado = resultado.Replace("{{documento}}",       doc.alias)
        ' resultado = resultado.Replace("{{fecha_radicacion}}",Format(doc.fecha_creacion, "dd/MM/yyyy"))
        ' resultado = resultado.Replace("{{fecha_respuesta}}", Format(doc.fecha_respuesta, "dd/MM/yyyy"))
        ' resultado = resultado.Replace("{{estado}}",          doc.Estado)
        ' resultado = resultado.Replace("{{ubicacion}}",       doc.ubicacion)
        ' Return resultado
        Return plantilla ' dummy
    End Function

    ' ── Helper: Enviar por SendGrid ───────────────────────────────────────────────────
    '   (Activar cuando se integre el paquete SendGrid NuGet)

    Private Sub EnviarPorSendGrid(destinatario As String, asunto As String, cuerpo As String)
        ' Imports SendGrid
        ' Imports SendGrid.Helpers.Mail
        '
        ' Dim apiKey As String = ConfigurationManager.AppSettings("SendGridApiKey")
        ' Dim client As New SendGridClient(apiKey)
        ' Dim from   As New EmailAddress("noreply@tudominio.com", "Gestion Documental")
        ' Dim to     As New EmailAddress(destinatario)
        ' Dim msg    As SendGridMessage = MailHelper.CreateSingleEmail(from, to, asunto, "", cuerpo)
        ' Dim resp   = Await client.SendEmailAsync(msg)
    End Sub

    ' ── Helpers de UI ────────────────────────────────────────────────────────────────

    Private Sub MostrarMensajeFlujo(r As ResultadoOperacion)
        If r.Exito Then
            lblMsgFlujo.CssClass = "text-success"
            lblMsgFlujo.Text = "<i class='fa fa-check-circle'></i> " & r.Mensaje
        Else
            lblMsgFlujo.CssClass = "help-block"
            lblMsgFlujo.Text = "<i class='fa fa-exclamation-circle'></i> " & r.Mensaje
        End If
    End Sub

    Private Sub MostrarMensajeGen(r As ResultadoOperacion)
        If r.Exito Then
            lblMsgGen.CssClass = "text-success"
            lblMsgGen.Text = "<i class='fa fa-check-circle'></i> " & r.Mensaje
        Else
            lblMsgGen.CssClass = "help-block"
            lblMsgGen.Text = "<i class='fa fa-exclamation-circle'></i> " & r.Mensaje
        End If
    End Sub

End Class
