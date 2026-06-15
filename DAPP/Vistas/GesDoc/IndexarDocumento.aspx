<%@ Page Title="Indexar Documentos" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="IndexarDocumento.aspx.vb" Inherits="DAPP.IndexarDocumento" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <style type="text/css">
        /* ── Panel simulación OCR / Audio-Video ── */
        #panelProceso   { display:none; margin:10px 0; }
        .proceso-box    { border:1px solid #d6e4f0; border-radius:4px; background:#f0f7ff; padding:14px 18px; }
        .proceso-titulo { font-size:14px; font-weight:bold; color:#2c6fad; margin-bottom:12px; }
        .paso           { display:flex; align-items:center; margin-bottom:8px; font-size:13px; color:#555; }
        .paso .ico      { width:30px; height:30px; border-radius:50%; display:flex; align-items:center;
                          justify-content:center; font-size:14px; margin-right:10px; flex-shrink:0; }
        .ico.espera     { background:#ddd; color:#999; }
        .ico.activo     { background:#337ab7; color:#fff; }
        .ico.ok         { background:#5cb85c; color:#fff; }
        .badge-p        { font-size:11px; padding:2px 8px; border-radius:10px; margin-left:8px; }
        .bp-espera      { background:#ddd; color:#666; }
        .bp-activo      { background:#337ab7; color:#fff; }
        .bp-ok          { background:#5cb85c; color:#fff; }
        .prog-paso      { display:none; height:8px; border-radius:4px; background:#e0e0e0;
                          margin:3px 0 4px 40px; overflow:hidden; }
        .prog-paso .bar { height:100%; width:0%; background:#337ab7;
                          transition:width 0.2s ease; border-radius:4px; }
        /* Resultado final */
        #resultadoOCR, #resultadoAV {
            display:none; margin-top:10px; border-radius:4px; padding:12px 16px;
        }
        #resultadoOCR   { border:1px solid #d6e9c6; background:#dff0d8; }
        #resultadoAV    { border:1px solid #f0ad4e; background:#fcf8e3; }
        #resultadoOCR h4 { color:#3c763d; margin:0 0 8px 0; font-size:14px; }
        #resultadoAV  h4 { color:#8a6d3b; margin:0 0 8px 0; font-size:14px; }
        .tags-res span  { display:inline-block; padding:2px 8px; border-radius:3px;
                          font-size:11px; color:#fff; margin-right:6px; margin-bottom:6px; }
        .tag-ocr  { background:#337ab7; }
        .tag-pag  { background:#27ae60; }
        .tag-ia   { background:#9b59b6; }
        .tag-av   { background:#e67e22; }
        .caja-ia  { background:#fff; border-left:4px solid #337ab7; padding:8px 12px;
                    font-size:12px; color:#444; border-radius:2px; margin-top:8px; line-height:1.6; }
        .caja-av  { background:#fff; border-left:4px solid #e67e22; padding:8px 12px;
                    font-size:12px; color:#444; border-radius:2px; margin-top:8px; line-height:1.6; }
        .hint-archivo { font-size:11px; color:#888; margin-top:3px; margin-left:184px; }
    </style>

    <script type="text/javascript" src="Resources/dynamsoft.webtwain.initiate.js"> </script>
    <script type="text/javascript" src="Resources/dynamsoft.webtwain.config.js"> </script>

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        // IDs de los tipos especiales (según los registros insertados en AdminTipoDocumento)
        var ID_FISICO = "281";
        var ID_AV = "282";

        // Se dispara con el onchange del DropDownList (además del AutoPostBack del server)
        function onTipoDocChange(sel) {
            ocultarPaneles();
            var val = sel.value;
            var hint = document.getElementById("hintArchivo");
            if (val === ID_FISICO) {
                hint.innerHTML = '<i class="fa fa-info-circle"></i> Adjunte la imagen escaneada (JPG, PNG, TIFF, PDF)';
            } else if (val === ID_AV) {
                hint.innerHTML = '<i class="fa fa-info-circle"></i> Adjunte el archivo de audio o video (MP3, MP4, WAV, AVI...)';
            } else {
                hint.innerHTML = "";
            }
        }

        // Intercepta el botón OK del lado cliente antes del postback
        function interceptarOK() {
            var sel = document.getElementById("<%= txtTipoDocumento.ClientID %>");
            if (!sel) return true; // si no encuentra el control, deja continuar normal
            var val = sel.value;
            if (val === ID_FISICO) {
                iniciarProcesoOCR();
                return false; // evita postback hasta terminar la animación
            } else if (val === ID_AV) {
                iniciarProcesoAV();
                return false;
            }
            return true; // otros tipos -> postback normal
        }

        function ocultarPaneles() {
            setDisplay("panelProceso", "none");
            setDisplay("resultadoOCR", "none");
            setDisplay("resultadoAV", "none");
            resetPasos();
        }

        // ── FLUJO OCR ──────────────────────────────────────
        function iniciarProcesoOCR() {
            ocultarPaneles();
            setDisplay("panelProceso", "block");
            setDisplay("pasosOCR", "block");
            setDisplay("pasosAV", "none");
            setText("procesoTitulo", '<i class="fa fa-cogs fa-spin"></i> Procesando documento físico escaneado...');

            animarPaso("ico1", "b1", "pg1", "bar1", 900, function () {
                animarPaso("ico2", "b2", "pg2", "bar2", 1500, function () {
                    animarPaso("ico3", "b3", "pg3", "bar3", 1000, function () {
                        animarPaso("ico4", "b4", "pg4", "bar4", 1800, function () {
                            mostrarResultadoOCR();
                        });
                    });
                });
            });
        }

        function mostrarResultadoOCR() {
            setText("procesoTitulo", '<i class="fa fa-check-circle" style="color:#5cb85c"></i> Procesamiento completado');
            var alias = valById("<%= txtAlias.ClientID %>") || "documento";
            setText("nombrePdf", alias + "_ocr.pdf");
            setText("nroPaginas", valById("<%= TxtFolios.ClientID %>") || "1");
            var resumenes = [
                "El documento contiene una comunicación formal en la que se solicita revisión de procedimientos internos. Se identifican tres puntos clave: actualización normativa, seguimiento a compromisos previos y solicitud de respuesta en un plazo máximo de 15 días hábiles.",
                "Registro de actas del proceso evaluativo del período reportado. Se detallan participantes, resultados y observaciones del comité evaluador. Se recomienda archivar con carácter confidencial.",
                "Documento contractual que establece condiciones de prestación de servicios entre las partes, incluyendo cláusulas de vigencia, obligaciones, garantías y causales de terminación. Cuenta con firmas y sellos en todas sus páginas."
            ];
            setText("textoResumen", resumenes[Math.floor(Math.random() * resumenes.length)]);
            setDisplay("resultadoOCR", "block");
        }

        // ── FLUJO AUDIO / VIDEO ────────────────────────────
        function iniciarProcesoAV() {
            ocultarPaneles();
            setDisplay("panelProceso", "block");
            setDisplay("pasosAV",  "block");
            setDisplay("pasosOCR", "none");
            setText("procesoTitulo", '<i class="fa fa-cogs fa-spin"></i> Procesando archivo de audio / video...');

            animarPaso("icoav1","bav1","pgav1","barav1", 800, function() {
            animarPaso("icoav2","bav2","pgav2","barav2", 1200, function() {
            animarPaso("icoav3","bav3","pgav3","barav3", 2200, function() {
                mostrarResultadoAV();
            }); }); });
        }

        function mostrarResultadoAV() {
            setText("procesoTitulo", '<i class="fa fa-check-circle" style="color:#5cb85c"></i> Procesamiento completado');
            var inp = document.getElementById("<%= flDocumentos.ClientID %>");
            var nombre = (inp && inp.value) ? inp.value.split("\\").pop() : "archivo_digital";
            setText("nombreAV", nombre);
            var trans = [
                "Buenos días. El motivo de esta comunicación es informar los avances del proyecto en el último trimestre. Se completó la fase de análisis con un cumplimiento del 95% y se registran avances en la integración del módulo de reportes. Se agenda próxima reunión para el día quince a las diez de la mañana.",
                "La presente grabación corresponde a la sesión ordinaria del comité de calidad. Se trató la actualización del manual de procedimientos y el estado de no conformidades de la última auditoría. Se asignaron responsables para el plan de mejora.",
                "Le envío este audio para confirmar la recepción de los documentos del día de ayer. Por favor confirmar si requieren algún ajuste o si podemos proceder con la radicación formal. Quedo atento a su respuesta."
            ];
            setText("textoTranscripcion", trans[Math.floor(Math.random() * trans.length)]);
            setDisplay("resultadoAV", "block");
        }

        // ── Animación genérica de paso ─────────────────────
        function animarPaso(icoId, badgeId, progId, barId, durMs, cb) {
            var ico  = document.getElementById(icoId);
            var badge = document.getElementById(badgeId);
            var prog = document.getElementById(progId);
            var bar  = document.getElementById(barId);
            ico.className   = "ico activo";
            badge.className = "badge-p bp-activo";
            badge.innerHTML = "Procesando...";
            prog.style.display = "block";
            var pct = 0;
            var steps = 20;
            var interval = durMs / steps;
            var iv = setInterval(function() {
                pct = Math.min(pct + (100 / steps) + Math.random() * 3, 100);
                bar.style.width = pct + "%";
                if (pct >= 100) {
                    clearInterval(iv);
                    ico.className   = "ico ok";
                    badge.className = "badge-p bp-ok";
                    badge.innerHTML = "Listo";
                    setTimeout(cb, 250);
                }
            }, interval);
        }

        function resetPasos() {
            ["1","2","3","4"].forEach(function(n) {
                setIco("ico"+n, "espera"); setBadge("b"+n, "Pendiente");
                resetProg("pg"+n, "bar"+n);
            });
            ["1","2","3"].forEach(function(n) {
                setIco("icoav"+n, "espera"); setBadge("bav"+n, "Pendiente");
                resetProg("pgav"+n, "barav"+n);
            });
        }
        function setIco(id, cls)   { var e=document.getElementById(id); if(e) e.className="ico "+cls; }
        function setBadge(id, txt) { var e=document.getElementById(id); if(e){ e.className="badge-p bp-espera"; e.innerHTML=txt; } }
        function resetProg(pgId, barId) {
            var pg=document.getElementById(pgId); if(pg) pg.style.display="none";
            var b=document.getElementById(barId);  if(b)  b.style.width="0%";
        }
        function setDisplay(id, v) { var e=document.getElementById(id); if(e) e.style.display=v; }
        function setText(id, v)    { var e=document.getElementById(id); if(e) e.innerHTML=v; }
        function valById(id)       { var e=document.getElementById(id); return e ? e.value : ""; }

        // Al cargar (o recargar por postback) restaura el hint según lo que quedó seleccionado
        window.onload = function() {
            var sel = document.getElementById("<%= txtTipoDocumento.ClientID %>");
            if (sel) onTipoDocChange(sel);
        };

    </script>

   

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>DOCUMENTO</h3>
                <div class="form-horizontal fom-border">
                    <br />
                    <br />
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipoDoc" DataTextField="nombre" DataValueField="id" AutoPostBack="True" onchange="onTipoDocChange(this)">
                        </asp:DropDownList>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNumDocumento" runat="server" Text="Nombre Documento:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtAlias" CssClass="input-xlarge" Width="320px"  /> 
                        
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNombre" runat="server" Text="Descripcion:/" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDescripcion" CssClass="input-xlarge" Width="320px" Height="66px" TextMode="MultiLine" />
                        <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDescripcion" ErrorMessage=" * El campo 'Descripción' es obligatorio." Width="400px" />
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label4" runat="server" Text="Fecha Documento: *" Width="180px"  />
                        </b>
                        <asp:TextBox runat="server" ID="txtFechaInicio" CssClass="input-xlarge" Width="180px" textmode="Date" AutoPostBack="True"  /> 
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaInicio" ErrorMessage=" * El campo 'Fecha Documento' es obligatorio." Width="400px" />  
                                                                                        
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label2" runat="server" Text="Folios:*" Width="180px"  />
                        </b>
                        <asp:TextBox runat="server" ID="TxtFolios" CssClass="input-xlarge" Width="320px" TextMode="Number" >1</asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="TxtFolios" ErrorMessage=" * El campo 'Folios' es obligatorio." Width="400px" />                      
                    </div>
                    <div>
                         <b class="control-label">
                            <asp:Label ID="lbArchivo" runat="server" Text="Archivo:*" Width="180px" />
                        </b>
                        <div style="margin-left:180px">
                            <asp:FileUpload ID="flDocumentos" runat="server" CssClass="input-xlarge" Width="320px" />                          
                        </div>
                        <div class="hint-archivo" id="hintArchivo"></div>
                    </div>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTelefono" runat="server" Text="Versión:*" Width="180px" />
                        </b>
                        <ig:WebNumericEditor ID="txtVersion" Width="180px" runat="server" CssClass="input-xlarge" NullValue="1" NullText="1" Nullable="False" BorderStyle="None" DataMode="Int" MinValue="1" ToolTip="Versión">
                        </ig:WebNumericEditor>
                        <asp:RequiredFieldValidator ID="RQ_Version" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtVersion" ErrorMessage=" * El campo 'Versión' es obligatorio." Width="400px" />
                    </div>

                    <br />
                    <br />

                    <%-- ── PANEL SIMULACIÓN OCR / AUDIO-VIDEO (solo visual JS) ── --%>
                    <div id="panelProceso">
                        <div class="proceso-box">
                            <div class="proceso-titulo" id="procesoTitulo"></div>

                            <%-- Pasos Documento Físico / OCR --%>
                            <div id="pasosOCR" style="display:none">
                                <div class="paso" id="p1"><div class="ico espera" id="ico1"><i class="fa fa-file-image-o"></i></div><div>Recepción del documento escaneado <span class="badge-p bp-espera" id="b1">Pendiente</span></div></div>
                                <div class="prog-paso" id="pg1"><div class="bar" id="bar1"></div></div>
                                <div class="paso" id="p2"><div class="ico espera" id="ico2"><i class="fa fa-font"></i></div><div>Reconocimiento óptico de caracteres (OCR) <span class="badge-p bp-espera" id="b2">Pendiente</span></div></div>
                                <div class="prog-paso" id="pg2"><div class="bar" id="bar2"></div></div>
                                <div class="paso" id="p3"><div class="ico espera" id="ico3"><i class="fa fa-file-pdf-o"></i></div><div>Generación de PDF paginado <span class="badge-p bp-espera" id="b3">Pendiente</span></div></div>
                                <div class="prog-paso" id="pg3"><div class="bar" id="bar3"></div></div>
                                <div class="paso" id="p4"><div class="ico espera" id="ico4"><i class="fa fa-magic"></i></div><div>Generación de resumen con IA <span class="badge-p bp-espera" id="b4">Pendiente</span></div></div>
                                <div class="prog-paso" id="pg4"><div class="bar" id="bar4"></div></div>
                            </div>

                            <%-- Pasos Audio / Video --%>
                            <div id="pasosAV" style="display:none">
                                <div class="paso"><div class="ico espera" id="icoav1"><i class="fa fa-upload"></i></div><div>Recepción del archivo digital <span class="badge-p bp-espera" id="bav1">Pendiente</span></div></div>
                                <div class="prog-paso" id="pgav1"><div class="bar" id="barav1"></div></div>
                                <div class="paso"><div class="ico espera" id="icoav2"><i class="fa fa-microphone"></i></div><div>Análisis de contenido multimedia <span class="badge-p bp-espera" id="bav2">Pendiente</span></div></div>
                                <div class="prog-paso" id="pgav2"><div class="bar" id="barav2"></div></div>
                                <div class="paso"><div class="ico espera" id="icoav3"><i class="fa fa-align-left"></i></div><div>Transcripción automática con IA <span class="badge-p bp-espera" id="bav3">Pendiente</span></div></div>
                                <div class="prog-paso" id="pgav3"><div class="bar" id="barav3"></div></div>
                            </div>
                        </div>
                    </div>

                    <%-- Resultado OCR --%>
                    <div id="resultadoOCR">
                        <h4><i class="fa fa-check-circle"></i> Documento procesado exitosamente</h4>
                        <div class="tags-res">
                            <span class="tag-ocr"><i class="fa fa-font"></i> OCR</span>
                            <span class="tag-pag"><i class="fa fa-file-pdf-o"></i> PDF Paginado</span>
                            <span class="tag-ia"><i class="fa fa-magic"></i> Resumen IA</span>
                            &nbsp; <i class="fa fa-file-pdf-o"></i> <b id="nombrePdf"></b>
                            &nbsp; <i class="fa fa-files-o"></i> Páginas: <b id="nroPaginas"></b>
                        </div>
                        <div class="caja-ia"><b><i class="fa fa-magic"></i> Resumen generado por IA:</b><br><span id="textoResumen"></span></div>
                    </div>

                    <%-- Resultado Audio/Video --%>
                    <div id="resultadoAV">
                        <h4><i class="fa fa-check-circle"></i> Archivo procesado exitosamente</h4>
                        <div class="tags-res">
                            <span class="tag-av"><i class="fa fa-align-left"></i> Transcripción IA</span>
                            &nbsp; <i class="fa fa-file-o"></i> <b id="nombreAV"></b>
                        </div>
                        <div class="caja-av"><b><i class="fa fa-align-left"></i> Transcripción generada por IA:</b><br><span id="textoTranscripcion"></span></div>
                    </div>
                    <%-- ── FIN PANEL SIMULACIÓN ── --%>
                                               
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" OnClientClick="return interceptarOK();" />
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <br />
                    <br />
                    <div class="cabezote-form">
                        <span>CAMPOS ADICIONALES</span>
                    </div>
                    <div>
                       <asp:table ID="TblDatos" runat="server" CssClass="Office2007Blue" Width="100%" ViewStateMode="Enabled">

                        </asp:table>
                    </div>
                    <br />
                    <br />
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    <asp:TextBox runat="server" ID="txtnombre" CssClass="input-xlarge" Width="320px" ReadOnly="false" Visible="false"  />
                </div>
            </div>
        </div>
        <asp:TextBox runat="server" ID="txtcuantos" Height="70" TextMode="MultiLine" Rows="5" Width="280px" Visible="false"  />
       
    
    </div>
    <asp:SqlDataSource ID="SqlTipoDoc" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [nombre] FROM [AdminTipoDocumento] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [nombre]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlUbicacion" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [ubicacion] FROM [ubicaciones] WHERE ([id_sucursal] = @id_sucursal) ORDER BY [ubicacion]">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
    <asp:HiddenField ID="Hdd_id_tercero" runat="server" Value="0" />
</asp:Content>


