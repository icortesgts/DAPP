<%@ Page Title="Flujo de Proceso" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="FlujoProceso.aspx.vb" Inherits="DAPP.FlujoProceso" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
     
    <style type="text/css">
        /* ── Layout general ── */
        .panel-izq   { padding-right: 6px; }
        .panel-der   { padding-left: 6px; }

        /* ── Árbol de tipos ── */
        .arbol-box   { border: 1px solid #ccc; border-radius: 3px; background: #fff;
                       min-height: 420px; padding: 0; }
        .arbol-header{ background: #4a90d9; color: #fff; padding: 7px 12px;
                       font-weight: bold; font-size: 13px; border-radius: 3px 3px 0 0; }
        .arbol-body  { padding: 8px; }
        .arbol-nodo  { padding: 6px 10px; cursor: pointer; border-radius: 3px;
                       font-size: 13px; display: flex; align-items: center;
                       border: 1px solid transparent; margin-bottom: 3px; }
        .arbol-nodo:hover   { background: #eaf3fb; border-color: #b8d9f5; }
        .arbol-nodo.activo  { background: #337ab7; color: #fff; border-color: #2e6da4; }
        .arbol-nodo .ico-tipo { margin-right: 8px; width: 18px; text-align: center; }

        /* ── Tabs ── */
        .nav-tabs > li > a  { font-size: 13px; }
        .tab-content        { border: 1px solid #ddd; border-top: none;
                              padding: 14px; background: #fff; border-radius: 0 0 3px 3px; }

        /* ── Árbol de eventos ── */
        .evento-item { border: 1px solid #d6e4f0; border-radius: 4px;
                       margin-bottom: 8px; background: #f8fbff; }
        .evento-header { display: flex; align-items: center; padding: 8px 12px;
                         cursor: pointer; user-select: none; }
        .evento-header:hover { background: #eaf3fb; border-radius: 4px 4px 0 0; }
        .evento-num  { width: 26px; height: 26px; border-radius: 50%;
                       background: #337ab7; color: #fff; font-size: 12px; font-weight: bold;
                       display: flex; align-items: center; justify-content: center;
                       margin-right: 10px; flex-shrink: 0; }
        .evento-num.obligatorio { background: #d9534f; }
        .evento-titulo { flex: 1; font-weight: bold; font-size: 13px; color: #333; }
        .evento-badge  { font-size: 11px; padding: 2px 8px; border-radius: 10px;
                         background: #ddd; color: #666; margin-right: 8px; }
        .evento-badge.configurado { background: #5cb85c; color: #fff; }
        .evento-toggle { color: #999; font-size: 12px; }
        .evento-body   { padding: 10px 14px 14px 48px; border-top: 1px solid #d6e4f0;
                         display: none; }

        /* ── Responsables dentro de evento ── */
        .seccion-resp  { margin-bottom: 10px; }
        .seccion-resp label { font-weight: bold; font-size: 12px;
                              color: #555; display: block; margin-bottom: 4px; }
        .chips-box     { min-height: 34px; border: 1px solid #aaa; border-radius: 3px;
                         padding: 4px 6px; background: #fff; display: flex;
                         flex-wrap: wrap; gap: 4px; align-items: center; cursor: text; }
        .chip          { display: inline-flex; align-items: center; background: #337ab7;
                         color: #fff; border-radius: 12px; padding: 2px 10px 2px 8px;
                         font-size: 12px; gap: 5px; }
        .chip.chip-rol { background: #8e44ad; }
        .chip .rm      { cursor: pointer; font-size: 14px; line-height: 1;
                         opacity: 0.8; font-weight: bold; }
        .chip .rm:hover { opacity: 1; }
        .dias-plazo    { display: flex; align-items: center; gap: 8px; margin-top: 8px; }
        .dias-plazo label { font-weight: bold; font-size: 12px; color: #555; margin: 0; }
        .dias-plazo input { width: 80px; height: 28px; border: 1px solid #aaa;
                            border-radius: 3px; padding: 2px 6px; font-size: 13px; }
        .btn-add-resp  { font-size: 12px; padding: 3px 10px; margin-top: 4px; }

        /* ── Select inline para agregar ── */
        .add-row       { display: flex; gap: 6px; margin-top: 6px; align-items: center; }
        .add-row select { height: 28px; font-size: 12px; border: 1px solid #aaa;
                          border-radius: 3px; padding: 2px 6px; flex: 1; }
        .add-row .btn  { font-size: 12px; padding: 3px 10px; height: 28px; }

        /* ── Tab Generación ── */
        .editor-plantilla { border: 1px solid #aaa; border-radius: 3px;
                            min-height: 200px; padding: 10px; background: #fff;
                            font-size: 13px; line-height: 1.6; }
        .toolbar-editor   { border: 1px solid #aaa; border-top: none; border-bottom: none;
                            padding: 4px 6px; background: #f5f5f5;
                            display: flex; gap: 4px; flex-wrap: wrap; }
        .toolbar-editor button { font-size: 13px; padding: 2px 8px; background: #fff;
                                 border: 1px solid #ccc; border-radius: 2px; cursor: pointer; }
        .toolbar-editor button:hover { background: #e8e8e8; }
        .tags-disponibles { margin-top: 8px; }
        .tags-disponibles span { font-size: 12px; color: #555; }
        .tag-btn { display: inline-block; background: #e8f4fd; border: 1px solid #b8d9f5;
                   color: #2c6fad; padding: 2px 8px; border-radius: 3px; font-size: 12px;
                   cursor: pointer; margin: 2px; }
        .tag-btn:hover { background: #cce5ff; }

        /* ── Sin selección ── */
        .sin-seleccion { text-align: center; color: #aaa; padding: 60px 20px;
                         font-size: 13px; }
        .sin-seleccion i { font-size: 40px; display: block; margin-bottom: 10px; }

        /* ── Estado guardado ── */
        #msgGuardado { display: none; }
    </style>

    <script type="text/javascript">

        // ── Datos dummy de tipos de documento ──────────────────
        var tiposDoc = [
            { id: 280, nombre: "PQRS" },
            { id: 281, nombre: "Documento Fisico / Escaneado" },
            { id: 282, nombre: "Archivo Digital (Audio / Video)" },
            { id: 5,   nombre: "ACTAS ACADEMICAS" },
            { id: 11,  nombre: "Comunicaciones Externas" },
            { id: 23,  nombre: "Contrato Terceros" },
            { id: 31,  nombre: "Informe Escolar" }
        ];

        // Eventos fijos del sistema
        var eventosBase = [
            { id: 1, nombre: "Radicacion",                              obligatorio: true },
            { id: 2, nombre: "Verificacion de condiciones",             obligatorio: false },
            { id: 3, nombre: "Validar area responsable y asignacion",   obligatorio: false },
            { id: 4, nombre: "Validar respuesta",                       obligatorio: false },
            { id: 5, nombre: "Registro de respuesta",                   obligatorio: false }
        ];

        // Roles y usuarios dummy
        var rolesDisp   = ["Administrador", "Coordinador", "Secretaria", "Jefe de Area", "Auditor"];
        var usuariosDisp = ["jperez", "mgarcia", "lrodriguez", "alopez", "cmartinez"];

        // Estado en memoria por tipo de documento
        // flujoData[id_tipo] = [ { eventoId, activo, roles:[], usuarios:[], dias:0 }, ... ]
        var flujoData = {};
        var tipoActual = null;

        // ── Al cargar ────────────────────────────────────────────
        window.onload = function () {
            renderArbol();
        };

        // ── Árbol izquierdo ──────────────────────────────────────
        function renderArbol() {
            var cont = document.getElementById("listaArbol");
            cont.innerHTML = "";
            tiposDoc.forEach(function (t) {
                var div = document.createElement("div");
                div.className = "arbol-nodo";
                div.id = "nodo_" + t.id;
                div.innerHTML = '<span class="ico-tipo"><i class="fa fa-file-text-o"></i></span>' + t.nombre;
                div.onclick = function () { seleccionarTipo(t.id, t.nombre); };
                cont.appendChild(div);
            });
        }

        function seleccionarTipo(id, nombre) {
            tipoActual = id;
            // resaltar nodo
            document.querySelectorAll(".arbol-nodo").forEach(function (n) {
                n.classList.remove("activo");
            });
            document.getElementById("nodo_" + id).classList.add("activo");
            document.getElementById("tituloTipo").innerHTML =
                '<i class="fa fa-file-text-o"></i> ' + nombre;
            document.getElementById("panelSinSeleccion").style.display = "none";
            document.getElementById("panelTabs").style.display = "block";

            inicializarFlujo(id);
            renderEventos(id);
        }

        // ── Tab Flujo ────────────────────────────────────────────
        function inicializarFlujo(idTipo) {
            if (!flujoData[idTipo]) {
                flujoData[idTipo] = eventosBase.map(function (e) {
                    return {
                        eventoId:  e.id,
                        nombre:    e.nombre,
                        obligatorio: e.obligatorio,
                        activo:    e.obligatorio, // radicacion siempre activo
                        roles:     [],
                        usuarios:  [],
                        dias:      0
                    };
                });
            }
        }

        function renderEventos(idTipo) {
            var cont = document.getElementById("contenedorEventos");
            cont.innerHTML = "";
            var eventos = flujoData[idTipo];
            eventos.forEach(function (ev, idx) {
                var configurado = (ev.roles.length > 0 || ev.usuarios.length > 0) && ev.dias > 0;
                cont.innerHTML += buildEventoHTML(ev, idx, configurado);
            });
        }

        function buildEventoHTML(ev, idx, configurado) {
            var numCls  = ev.obligatorio ? "evento-num obligatorio" : "evento-num";
            var badgeTxt = configurado ? "Configurado" : (ev.obligatorio ? "Obligatorio" : "Opcional");
            var badgeCls = configurado ? "evento-badge configurado" : "evento-badge";
            var chkCheck = ev.activo ? "checked" : "";
            var chkDis   = ev.obligatorio ? "disabled" : "";

            var rolesChips = ev.roles.map(function(r) {
                return '<span class="chip chip-rol"><i class="fa fa-users"></i>' + r +
                       '<span class="rm" onclick="quitarResponsable(' + idx + ',\'rol\',\'' + r + '\')">x</span></span>';
            }).join("");

            var usrChips = ev.usuarios.map(function(u) {
                return '<span class="chip"><i class="fa fa-user"></i>' + u +
                       '<span class="rm" onclick="quitarResponsable(' + idx + ',\'usuario\',\'' + u + '\')">x</span></span>';
            }).join("");

            return '<div class="evento-item" id="ev_' + idx + '">' +
                '<div class="evento-header" onclick="toggleEvento(' + idx + ')">' +
                    '<div class="' + numCls + '">' + (idx + 1) + '</div>' +
                    '<div class="evento-titulo">' + ev.nombre + '</div>' +
                    '<span class="' + badgeCls + '">' + badgeTxt + '</span>' +
                    '<input type="checkbox" ' + chkCheck + ' ' + chkDis +
                        ' onclick="event.stopPropagation(); toggleActivo(' + idx + ', this)"' +
                        ' title="Activar/Desactivar evento" style="margin-right:8px">' +
                    '<span class="evento-toggle"><i class="fa fa-chevron-down"></i></span>' +
                '</div>' +
                '<div class="evento-body" id="body_' + idx + '">' +

                    '<div class="seccion-resp">' +
                        '<label><i class="fa fa-users"></i> Roles responsables:</label>' +
                        '<div class="chips-box" id="chips_rol_' + idx + '">' + rolesChips + '</div>' +
                        '<div class="add-row">' +
                            '<select id="selRol_' + idx + '">' +
                                rolesDisp.map(function(r){ return '<option>' + r + '</option>'; }).join("") +
                            '</select>' +
                            '<button class="btn btn-default btn-add-resp" onclick="agregarResponsable(' + idx + ',\'rol\')">' +
                                '<i class="fa fa-plus"></i> Agregar</button>' +
                        '</div>' +
                    '</div>' +

                    '<div class="seccion-resp">' +
                        '<label><i class="fa fa-user"></i> Usuarios especificos:</label>' +
                        '<div class="chips-box" id="chips_usr_' + idx + '">' + usrChips + '</div>' +
                        '<div class="add-row">' +
                            '<select id="selUsr_' + idx + '">' +
                                usuariosDisp.map(function(u){ return '<option>' + u + '</option>'; }).join("") +
                            '</select>' +
                            '<button class="btn btn-default btn-add-resp" onclick="agregarResponsable(' + idx + ',\'usuario\')">' +
                                '<i class="fa fa-plus"></i> Agregar</button>' +
                        '</div>' +
                    '</div>' +

                    '<div class="dias-plazo">' +
                        '<label><i class="fa fa-calendar"></i> Dias de plazo:</label>' +
                        '<input type="number" min="0" value="' + ev.dias + '" id="dias_' + idx + '"' +
                            ' onchange="guardarDias(' + idx + ', this.value)">' +
                        '<span style="font-size:12px;color:#888">dias habiles</span>' +
                    '</div>' +
                '</div>' +
            '</div>';
        }

        function toggleEvento(idx) {
            var body = document.getElementById("body_" + idx);
            body.style.display = body.style.display === "block" ? "none" : "block";
        }

        function toggleActivo(idx, chk) {
            if (tipoActual === null) return;
            flujoData[tipoActual][idx].activo = chk.checked;
        }

        function agregarResponsable(idx, tipo) {
            if (tipoActual === null) return;
            var selId  = tipo === "rol" ? "selRol_" + idx : "selUsr_" + idx;
            var chipsId = tipo === "rol" ? "chips_rol_" + idx : "chips_usr_" + idx;
            var val    = document.getElementById(selId).value;
            var lista  = tipo === "rol" ? flujoData[tipoActual][idx].roles
                                        : flujoData[tipoActual][idx].usuarios;
            if (lista.indexOf(val) !== -1) return; // no duplicar
            lista.push(val);
            var chips = document.getElementById(chipsId);
            var chipCls = tipo === "rol" ? "chip chip-rol" : "chip";
            var ico     = tipo === "rol" ? "fa-users" : "fa-user";
            var span    = document.createElement("span");
            span.className = chipCls;
            span.innerHTML = '<i class="fa ' + ico + '"></i> ' + val +
                '<span class="rm" onclick="quitarResponsable(' + idx + ',\'' + tipo + '\',\'' + val + '\')">x</span>';
            chips.appendChild(span);
        }

        function quitarResponsable(idx, tipo, val) {
            if (tipoActual === null) return;
            var lista = tipo === "rol" ? flujoData[tipoActual][idx].roles
                                       : flujoData[tipoActual][idx].usuarios;
            var i = lista.indexOf(val);
            if (i !== -1) lista.splice(i, 1);
            renderEventos(tipoActual);
        }

        function guardarDias(idx, val) {
            if (tipoActual === null) return;
            flujoData[tipoActual][idx].dias = parseInt(val) || 0;
        }

        // ── Guardar flujo (llama al servidor) ────────────────────
        function guardarFlujo() {
            if (tipoActual === null) return;
            // Serializar el estado actual al campo oculto
            var datos = flujoData[tipoActual].map(function (ev, idx) {
                // leer dias del DOM por si no disparó onchange
                var diasEl = document.getElementById("dias_" + idx);
                if (diasEl) ev.dias = parseInt(diasEl.value) || 0;
                return ev;
            });
            document.getElementById("<%= hddFlujoDatos.ClientID %>").value = JSON.stringify(datos);
            document.getElementById("<%= hddTipoId.ClientID %>").value = tipoActual;
            document.getElementById("<%= btGuardarFlujo.ClientID %>").click();
        }

        // ── Tab Generación ───────────────────────────────────────
        var tags = ["{{usuario}}", "{{area}}", "{{documento}}",
                    "{{fecha_radicacion}}", "{{fecha_respuesta}}", "{{estado}}", "{{ubicacion}}"];

        function insertarTag(tag) {
            var ed = document.getElementById("editorPlantilla");
            ed.focus();
            var sel = window.getSelection();
            if (sel.rangeCount) {
                var range = sel.getRangeAt(0);
                range.deleteContents();
                range.insertNode(document.createTextNode(tag + " "));
            } else {
                ed.innerHTML += tag + " ";
            }
        }

        function cmdEditor(cmd, val) {
            document.execCommand(cmd, false, val || null);
            document.getElementById("editorPlantilla").focus();
        }

        function generarDocumentos() {
            var contenido = document.getElementById("editorPlantilla").innerHTML;
            document.getElementById("<%= hddPlantilla.ClientID %>").value = contenido;
            document.getElementById("<%= hddTipoIdGen.ClientID %>").value = tipoActual || 0;
            document.getElementById("<%= btGenerarDocs.ClientID %>").click();
        }

    </script>

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>FLUJO DE PROCESO</h3>
            </div>
        </div>

        <div class="row">
            <%-- ── Columna izquierda: árbol de tipos ── --%>
            <div class="col-sm-3 panel-izq">
                <div class="arbol-box">
                    <div class="arbol-header">
                        <i class="fa fa-sitemap"></i> Tipos de Documento
                    </div>
                    <div class="arbol-body" id="listaArbol">
                    </div>
                </div>
            </div>

            <%-- ── Columna derecha: tabs ── --%>
            <div class="col-sm-9 panel-der">

                <%-- Título tipo seleccionado --%>
                <h4 id="tituloTipo" style="margin-top:0; color:#337ab7; font-size:14px; font-weight:bold; min-height:22px;"></h4>

                <%-- Sin selección --%>
                <div id="panelSinSeleccion" class="sin-seleccion">
                    <i class="fa fa-hand-o-left"></i>
                    Seleccione un tipo de documento para configurar su flujo.
                </div>

                <%-- Panel con tabs (oculto hasta seleccionar) --%>
                <div id="panelTabs" style="display:none">
                    <ul class="nav nav-tabs" role="tablist">
                        <li role="presentation" class="active">
                            <a href="#tabFlujo" role="tab" data-toggle="tab">
                                <i class="fa fa-code-fork"></i> Flujo de Proceso
                            </a>
                        </li>
                        <li role="presentation">
                            <a href="#tabGeneracion" role="tab" data-toggle="tab">
                                <i class="fa fa-file-word-o"></i> Generacion Masiva
                            </a>
                        </li>
                    </ul>

                    <div class="tab-content">

                        <%-- ── TAB FLUJO ── --%>
                        <div role="tabpanel" class="tab-pane active" id="tabFlujo">
                            <div id="contenedorEventos"></div>
                            <br />
                            <asp:Label ID="lblMsgFlujo" runat="server" CssClass="help-block" Text="" />
                            <div id="msgGuardado" class="alert alert-success" style="font-size:13px">
                                <i class="fa fa-check-circle"></i> Flujo guardado correctamente.
                            </div>
                            <br />
                            <button type="button" class="btn btn-primary" onclick="guardarFlujo()">
                                <i class="fa fa-save"></i> Guardar Flujo
                            </button>
                            <asp:Button ID="btGuardarFlujo" runat="server" Text="__guardar__"
                                CssClass="hidden" OnClick="btGuardarFlujo_Click" />
                        </div>

                        <%-- ── TAB GENERACION ── --%>
                        <div role="tabpanel" class="tab-pane" id="tabGeneracion">

                            <div class="form-horizontal fom-border" style="padding:14px">
                                <div style="margin-bottom:10px">
                                    <b style="font-size:13px">Plantilla del documento:</b>
                                    <span style="font-size:11px; color:#888; margin-left:8px">
                                        Use los tags de abajo para combinar datos al generar.
                                    </span>
                                </div>

                                <%-- Toolbar editor --%>
                                <div class="toolbar-editor">
                                    <button onclick="cmdEditor('bold')"     title="Negrita">  <b>N</b></button>
                                    <button onclick="cmdEditor('italic')"   title="Cursiva">  <i>K</i></button>
                                    <button onclick="cmdEditor('underline')"title="Subrayado"><u>S</u></button>
                                    <button onclick="cmdEditor('justifyLeft')"   title="Izquierda"><i class="fa fa-align-left"></i></button>
                                    <button onclick="cmdEditor('justifyCenter')" title="Centro">  <i class="fa fa-align-center"></i></button>
                                    <button onclick="cmdEditor('justifyRight')"  title="Derecha"> <i class="fa fa-align-right"></i></button>
                                    <button onclick="cmdEditor('insertUnorderedList')" title="Lista"><i class="fa fa-list-ul"></i></button>
                                    <button onclick="cmdEditor('fontSize', '3')" title="Normal">A</button>
                                    <button onclick="cmdEditor('fontSize', '5')" title="Grande"><b>A+</b></button>
                                </div>

                                <%-- Editor contenteditable --%>
                                <div id="editorPlantilla" class="editor-plantilla" contenteditable="true"
                                     style="border-top:none; border-radius:0 0 3px 3px;">
                                    Estimado {{usuario}},<br><br>
                                    Por medio de la presente, le informamos que el documento <b>{{documento}}</b>
                                    ha sido radicado con fecha {{fecha_radicacion}}.<br><br>
                                    Area responsable: {{area}}<br>
                                    Estado actual: {{estado}}<br>
                                    Fecha limite de respuesta: {{fecha_respuesta}}<br>
                                    Ubicacion: {{ubicacion}}<br><br>
                                    Cordialmente,<br>
                                    Gestion Documental
                                </div>

                                <%-- Tags disponibles --%>
                                <div class="tags-disponibles" style="margin-top:10px">
                                    <span><i class="fa fa-tags"></i> <b>Tags disponibles</b> (clic para insertar):</span><br>
                                    <span class="tag-btn" onclick="insertarTag('{{usuario}}')">{{usuario}}</span>
                                    <span class="tag-btn" onclick="insertarTag('{{area}}')">{{area}}</span>
                                    <span class="tag-btn" onclick="insertarTag('{{documento}}')">{{documento}}</span>
                                    <span class="tag-btn" onclick="insertarTag('{{fecha_radicacion}}')">{{fecha_radicacion}}</span>
                                    <span class="tag-btn" onclick="insertarTag('{{fecha_respuesta}}')">{{fecha_respuesta}}</span>
                                    <span class="tag-btn" onclick="insertarTag('{{estado}}')">{{estado}}</span>
                                    <span class="tag-btn" onclick="insertarTag('{{ubicacion}}')">{{ubicacion}}</span>
                                </div>

                                <br />
                                <div style="border-top:1px solid #eee; padding-top:12px; margin-top:4px">
                                    <b style="font-size:13px">Distribucion:</b>
                                    <div style="margin-top:8px; font-size:13px; color:#555">
                                        <i class="fa fa-envelope-o"></i>
                                        Los documentos generados se enviaran por correo via
                                        <b>SendGrid</b> a los responsables del flujo configurado.
                                    </div>
                                </div>

                                <br />
                                <asp:Label ID="lblMsgGen" runat="server" CssClass="help-block" Text="" />
                                <button type="button" class="btn btn-primary" onclick="generarDocumentos()">
                                    <i class="fa fa-cogs"></i> Generar y Distribuir
                                </button>
                                <asp:Button ID="btGenerarDocs" runat="server" Text="__generar__"
                                    CssClass="hidden" OnClick="btGenerarDocs_Click" />
                            </div>
                        </div>

                    </div><%-- /tab-content --%>
                </div><%-- /panelTabs --%>

            </div><%-- /col derecha --%>
        </div><%-- /row --%>

        <%-- Campos ocultos para pasar datos al servidor --%>
        <asp:HiddenField ID="hddFlujoDatos" runat="server" Value="" />
        <asp:HiddenField ID="hddTipoId"     runat="server" Value="" />
        <asp:HiddenField ID="hddPlantilla"  runat="server" Value="" />
        <asp:HiddenField ID="hddTipoIdGen"  runat="server" Value="" />

    </div><%-- /container --%>

    <%-- Bootstrap tabs requiere jQuery + Bootstrap JS (ya incluido por Site.Master) --%>

</asp:Content>
