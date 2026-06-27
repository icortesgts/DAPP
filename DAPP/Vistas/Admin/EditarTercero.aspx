<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="EditarTercero.aspx.vb" Inherits="DAPP.EditarTercero" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.5.0/css/all.min.css" />

    <style>
        /* ============================================================
           FIRMA ELECTRONICA / DIGITAL - Estilos visuales demo cliente
           Bootstrap 3 compatible, .NET Framework 4.8
        ============================================================ */
        .firma-section-wrapper {
            border: 1px solid #ddd;
            border-radius: 4px;
            padding: 12px 14px;
            margin-top: 6px;
            background-color: #fafafa;
        }
        .firma-tipo-header {
            font-weight: bold;
            color: #555;
            margin-bottom: 8px;
            font-size: 13px;
            border-bottom: 1px solid #e0e0e0;
            padding-bottom: 5px;
        }
        .firma-tipo-header i { margin-right: 6px; color: #337ab7; }
        .firma-row {
            display: -webkit-box;
            display: -ms-flexbox;
            display: flex;
            -webkit-box-align: center;
            -ms-flex-align: center;
            align-items: center;
            margin-bottom: 6px;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
            gap: 6px;
        }
        .firma-estado-badge {
            display: inline-block;
            padding: 2px 10px;
            border-radius: 10px;
            font-size: 11px;
            font-weight: bold;
            margin-left: 8px;
        }
        .badge-configurada  { background-color: #5cb85c; color: #fff; }
        .badge-no-configurada { background-color: #d9534f; color: #fff; }

        .btn-firma-accion {
            font-size: 12px;
            padding: 3px 10px;
            margin-left: 4px;
            border-radius: 3px;
            cursor: pointer;
        }
        .btn-firma-accion i { margin-right: 4px; }

        .firma-info-resumen {
            font-size: 11px;
            color: #666;
            font-style: italic;
        }
        .firma-info-resumen i { margin-right: 3px; }

        .firma-separador { height: 1px; background: #eee; margin: 10px 0; }

        .demo-badge {
            font-size: 10px;
            background: #e67e22;
            color: #fff;
            padding: 1px 6px;
            border-radius: 3px;
            margin-left: 6px;
            vertical-align: middle;
            font-weight: normal;
        }

        /* Fila de contenido firma alineada con el resto del form */
        .firma-content-col {
            display: inline-block;
            vertical-align: top;
            width: 620px;
        }

        /* ---- MODALES ---- */
        .firma-modal-overlay {
            display: none;
            position: fixed;
            top: 0; left: 0;
            width: 100%; height: 100%;
            background: rgba(0,0,0,0.52);
            z-index: 9999;
        }
        .firma-modal-overlay.activo { display: block; }

        .firma-modal-box {
            background: #fff;
            border-radius: 5px;
            width: 480px;
            max-width: 96%;
            box-shadow: 0 5px 24px rgba(0,0,0,0.28);
            position: absolute;
            top: 50%;
            left: 50%;
            -webkit-transform: translate(-50%, -50%);
            -ms-transform: translate(-50%, -50%);
            transform: translate(-50%, -50%);
        }
        .firma-modal-header {
            background-color: #337ab7;
            color: #fff;
            padding: 11px 16px;
            border-radius: 5px 5px 0 0;
            overflow: hidden;
        }
        .firma-modal-header h4 {
            margin: 0;
            font-size: 15px;
            display: inline-block;
        }
        .firma-modal-header .btn-cerrar-modal {
            float: right;
            background: none;
            border: none;
            color: #fff;
            font-size: 19px;
            cursor: pointer;
            line-height: 1;
            padding: 0;
        }
        .firma-modal-body { padding: 16px 18px; }
        .firma-modal-body .fmg { margin-bottom: 11px; }
        .firma-modal-body .fmg label {
            display: block;
            font-weight: bold;
            font-size: 12px;
            margin-bottom: 3px;
            color: #444;
        }
        .firma-modal-body .fmg input[type="text"],
        .firma-modal-body .fmg input[type="password"],
        .firma-modal-body .fmg input[type="email"],
        .firma-modal-body .fmg select {
            width: 100%;
            padding: 5px 8px;
            border: 1px solid #ccc;
            border-radius: 3px;
            font-size: 13px;
            -webkit-box-sizing: border-box;
            box-sizing: border-box;
            height: 30px;
        }
        .firma-modal-body .fmg input[type="file"] {
            font-size: 12px;
            width: 100%;
        }
        .firma-preview-cert {
            border: 2px dashed #c8e0f5;
            border-radius: 4px;
            padding: 12px;
            background: #f0f7ff;
            font-size: 12px;
            color: #444;
            min-height: 72px;
            text-align: center;
        }
        .firma-preview-cert i { font-size: 22px; color: #5cb85c; display: block; margin-bottom: 5px; }
        .firma-modal-footer {
            padding: 10px 16px;
            border-top: 1px solid #eee;
            text-align: right;
            border-radius: 0 0 5px 5px;
        }
        .firma-modal-footer .btn { margin-left: 6px; font-size: 12px; }

        /* Alertas dentro y fuera de modales */
        .f-alert {
            padding: 7px 11px;
            border-radius: 3px;
            font-size: 12px;
            margin-top: 8px;
            display: none;
        }
        .f-alert.f-success { background:#dff0d8; color:#3c763d; border:1px solid #d6e9c6; display:block; }
        .f-alert.f-error   { background:#f2dede; color:#a94442; border:1px solid #ebccd1; display:block; }
        .f-alert.f-info    { background:#d9edf7; color:#31708f; border:1px solid #bce8f1; display:block; }

        /* Panel firma oculto por defecto */
        #divSeccionFirmas { display: none; }

        /* Fila de dos columnas para tipo firma */
        .firma-fila-row { margin-top: 4px; }
    </style>

    <script id="clientEventHandlersJS" lang="javascript" type="text/javascript">

        /* -------------------------------------------------------
           Funciones originales del sistema — SIN CAMBIOS
        ------------------------------------------------------- */
        function ActualizarSeleccionArea(id_area, nombre) {
            document.getElementById("<%=Hdd_id_area.ClientID%>").value = id_area;
            document.getElementById("<%=txtArea.ClientID%>").value = nombre;
            document.getElementById("<%=txtArea.ClientID%>").Text = nombre;
        }

        function ActualizarSeleccionTercero(id_tercero, documento, nombre) {
            document.getElementById("<%=Hdd_id_tercero.ClientID%>").value = id_tercero;
            document.getElementById("<%=txtTercero.ClientID%>").value = nombre;
            document.getElementById("<%=txtTercero.ClientID%>").Text = nombre;
        }

        /* -------------------------------------------------------
           Estado dummy de firmas (solo cliente, sin BD)
        ------------------------------------------------------- */
        var firmas = {
            electronica: { ok: false, nombre: '', email: '', cargo: '' },
            digital:     { ok: false, titular: '', entidad: '', vence: '' }
        };

        /* -------------------------------------------------------
           Al cargar la página: sincronizar visibilidad del panel
           con el estado actual del checkbox (respeta postback)
        ------------------------------------------------------- */
        window.addEventListener('load', function () {
            var chk = document.getElementById('<%=ChkFirma.ClientID%>');
            if (chk) {
                syncPanelFirma(chk.checked);
            }
        });

        /* -------------------------------------------------------
           Toggle panel firma — llamado por onclick del checkbox.
           Retorna TRUE para que el AutoPostBack del servidor
           siga funcionando normalmente (no se cancela).
        ------------------------------------------------------- */
        function onChkFirmaClick(chk) {
            syncPanelFirma(chk.checked);
            return true; // deja que continúe el AutoPostBack
        }

        function syncPanelFirma(visible) {
            var div = document.getElementById('divSeccionFirmas');
            if (div) div.style.display = visible ? 'block' : 'none';
            if (visible) actualizarBadges();
        }

        /* -------------------------------------------------------
           Actualizar badges y texto resumen
        ------------------------------------------------------- */
        function actualizarBadges() {
            // Electrónica
            var bE = document.getElementById('badge_e');
            var iE = document.getElementById('info_e');
            if (firmas.electronica.ok) {
                bE.className = 'firma-estado-badge badge-configurada';
                bE.innerHTML = '<i class="fa fa-check"></i> Configurada';
                iE.innerHTML = '<i class="fa fa-user"></i> ' + firmas.electronica.nombre +
                               ' &nbsp;&bull;&nbsp; <i class="fa fa-envelope"></i> ' + firmas.electronica.email;
            } else {
                bE.className = 'firma-estado-badge badge-no-configurada';
                bE.innerHTML = '<i class="fa fa-xmark"></i> No configurada';
                iE.innerHTML = 'Sin datos — haga clic en <strong>Configurar</strong> para asignar.';
            }

            // Digital
            var bD = document.getElementById('badge_d');
            var iD = document.getElementById('info_d');
            if (firmas.digital.ok) {
                bD.className = 'firma-estado-badge badge-configurada';
                bD.innerHTML = '<i class="fa fa-check"></i> Configurada';
                iD.innerHTML = '<i class="fa fa-certificate"></i> ' + firmas.digital.titular +
                               ' &nbsp;&bull;&nbsp; ' + firmas.digital.entidad +
                               ' &nbsp;&bull;&nbsp; Vence: ' + firmas.digital.vence;
            } else {
                bD.className = 'firma-estado-badge badge-no-configurada';
                bD.innerHTML = '<i class="fa fa-xmark"></i> No configurada';
                iD.innerHTML = 'Sin certificado — haga clic en <strong>Cargar Certificado</strong> para registrar.';
            }
        }

        /* -------------------------------------------------------
           MODAL FIRMA ELECTRÓNICA
        ------------------------------------------------------- */
        function abrirModalFirmaE() {
            // Precargar datos dummy / guardados
            document.getElementById('fe_nombre').value = firmas.electronica.nombre || 'Juan Carlos Pérez López';
            document.getElementById('fe_email').value  = firmas.electronica.email  || 'jcperez@entidad.gov.co';
            document.getElementById('fe_cargo').value  = firmas.electronica.cargo  || 'Jefe de División';
            document.getElementById('fe_pin').value    = '';
            document.getElementById('fe_pinc').value   = '';
            setAlerta('alerta_e', '', '');
            document.getElementById('modalFirmaE').className = 'firma-modal-overlay activo';
        }
        function cerrarModalFirmaE() {
            document.getElementById('modalFirmaE').className = 'firma-modal-overlay';
        }
        function guardarFirmaE() {
            var n = trim(document.getElementById('fe_nombre').value);
            var e = trim(document.getElementById('fe_email').value);
            var c = trim(document.getElementById('fe_cargo').value);
            var p = document.getElementById('fe_pin').value;
            var pc= document.getElementById('fe_pinc').value;

            if (!n || !e || !c) {
                return setAlerta('alerta_e', 'f-error',
                    '<i class="fa fa-triangle-exclamation"></i> Complete todos los campos obligatorios.');
            }
            if (p.length < 4) {
                return setAlerta('alerta_e', 'f-error',
                    '<i class="fa fa-triangle-exclamation"></i> El PIN debe tener mínimo 4 caracteres.');
            }
            if (p !== pc) {
                return setAlerta('alerta_e', 'f-error',
                    '<i class="fa fa-triangle-exclamation"></i> Los PINs no coinciden. Verifique.');
            }

            setAlerta('alerta_e', 'f-info',
                '<i class="fa fa-spinner fa-spin"></i> Registrando firma electrónica...');

            setTimeout(function () {
                firmas.electronica.ok     = true;
                firmas.electronica.nombre = n;
                firmas.electronica.email  = e;
                firmas.electronica.cargo  = c;
                actualizarBadges();
                setAlerta('alerta_e', 'f-success',
                    '<i class="fa fa-circle-check"></i> Firma electrónica configurada correctamente.');
                setTimeout(cerrarModalFirmaE, 1300);
            }, 1100);
        }

        /* -------------------------------------------------------
           MODAL FIRMA DIGITAL
        ------------------------------------------------------- */
        function abrirModalFirmaD() {
            document.getElementById('fd_titular').value  = firmas.digital.titular  || 'JUAN CARLOS PEREZ LOPEZ - CC 80123456';
            document.getElementById('fd_entidad').value  = firmas.digital.entidad  || 'Certicámara S.A.';
            document.getElementById('fd_vence').value    = firmas.digital.vence    || '2026-12-31';
            document.getElementById('fd_pin').value      = '';
            setAlerta('alerta_d', '', '');
            refrescarPreview();
            document.getElementById('modalFirmaD').className = 'firma-modal-overlay activo';
        }
        function cerrarModalFirmaD() {
            document.getElementById('modalFirmaD').className = 'firma-modal-overlay';
        }
        function refrescarPreview() {
            var t = document.getElementById('fd_titular').value  || '—';
            var e = document.getElementById('fd_entidad').value  || '—';
            var v = document.getElementById('fd_vence').value    || '—';
            document.getElementById('cert_preview').innerHTML =
                '<i class="fa fa-shield-halved"></i>' +
                '<strong>' + t + '</strong><br>' +
                'Emitido por: ' + e + '<br>' +
                '<span style="color:#337ab7">Vencimiento: ' + v + '</span>';
        }
        function guardarFirmaD() {
            var t = trim(document.getElementById('fd_titular').value);
            var e = trim(document.getElementById('fd_entidad').value);
            var v = trim(document.getElementById('fd_vence').value);
            var p = document.getElementById('fd_pin').value;

            if (!t || !e || !v) {
                return setAlerta('alerta_d', 'f-error',
                    '<i class="fa fa-triangle-exclamation"></i> Complete todos los datos del certificado.');
            }
            if (p.length < 4) {
                return setAlerta('alerta_d', 'f-error',
                    '<i class="fa fa-triangle-exclamation"></i> Ingrese el PIN del certificado (mín. 4 caracteres).');
            }

            setAlerta('alerta_d', 'f-info',
                '<i class="fa fa-spinner fa-spin"></i> Validando certificado digital con la entidad...');

            setTimeout(function () {
                setAlerta('alerta_d', 'f-info',
                    '<i class="fa fa-spinner fa-spin"></i> Registrando en el sistema...');
                setTimeout(function () {
                    firmas.digital.ok      = true;
                    firmas.digital.titular = t;
                    firmas.digital.entidad = e;
                    firmas.digital.vence   = v;
                    actualizarBadges();
                    setAlerta('alerta_d', 'f-success',
                        '<i class="fa fa-circle-check"></i> Certificado digital registrado exitosamente.');
                    setTimeout(cerrarModalFirmaD, 1300);
                }, 900);
            }, 1100);
        }

        /* -------------------------------------------------------
           MODAL VER FIRMA (botón original "Ver Firma")
        ------------------------------------------------------- */
        function abrirModalVerFirma() {
            var pwd = document.getElementById('<%=txtvalida.ClientID%>');
            if (!pwd || trim(pwd.value) === '') {
                alert('Ingrese la Contraseña de Firma para continuar.');
                return false;
            }
            setAlerta('alerta_ver', '', '');
            document.getElementById('modalVerFirma').className = 'firma-modal-overlay activo';
            return false; // previene postback del Button
        }
        function cerrarModalVerFirma() {
            document.getElementById('modalVerFirma').className = 'firma-modal-overlay';
        }
        function confirmarVerFirma() {
            setAlerta('alerta_ver', 'f-info',
                '<i class="fa fa-spinner fa-spin"></i> Verificando contraseña de firma...');
            setTimeout(function () {
                setAlerta('alerta_ver', 'f-success',
                    '<i class="fa fa-circle-check"></i> Contraseña correcta. ' +
                    '<br><strong>Titular:</strong> Juan Carlos Pérez López &nbsp;|&nbsp; ' +
                    '<strong>Tipo:</strong> Electrónica &nbsp;|&nbsp; ' +
                    '<strong>Válida hasta:</strong> 31/12/2026');
            }, 1000);
        }

        /* -------------------------------------------------------
           Eliminar firma (con confirmación)
        ------------------------------------------------------- */
        function eliminarFirma(tipo) {
            var lbl = tipo === 'electronica' ? 'Firma Electrónica' : 'Firma Digital';
            if (!confirm('¿Está seguro de eliminar la configuración de ' + lbl + '?\n\nEsta acción no se puede deshacer.')) return;
            firmas[tipo] = tipo === 'electronica'
                ? { ok: false, nombre: '', email: '', cargo: '' }
                : { ok: false, titular: '', entidad: '', vence: '' };
            actualizarBadges();
            alert(lbl + ' eliminada correctamente.');
        }

        /* -------------------------------------------------------
           Utilidades
        ------------------------------------------------------- */
        function setAlerta(id, cls, msg) {
            var el = document.getElementById(id);
            if (!el) return;
            el.className = 'f-alert' + (cls ? ' ' + cls : '');
            el.innerHTML = msg;
        }
        function trim(s) { return s ? s.replace(/^\s+|\s+$/g, '') : ''; }

        // Cerrar modal al hacer clic en el fondo oscuro
        document.addEventListener('click', function (ev) {
            if (ev.target && ev.target.className === 'firma-modal-overlay activo') {
                ev.target.className = 'firma-modal-overlay';
            }
        });

    </script>

    <div class="container mrgnBotMd">
        <div class="row noPadding">
            <div class="col-sm-12 noPadding colHeight">
                <h3>TERCERO</h3>
                <div class="form-horizontal fom-border">
                    <br />
                    <br />

                    <%-- Tipo Documento --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbTipoDocumento" runat="server" Text="Tipo Documento:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtTipoDocumento" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD">
                            <asp:ListItem>CC</asp:ListItem>
                            <asp:ListItem>NIT</asp:ListItem>
                            <asp:ListItem>CE</asp:ListItem>
                            <asp:ListItem>Pasaporte</asp:ListItem>
                            <asp:ListItem>NUIP</asp:ListItem>
                            <asp:ListItem>Otro</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%-- Número Documento --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNumDocumento" runat="server" Text="Número Documento:*" Width="180px" />
                        </b>
                        <ig:WebNumericEditor ID="txtNumDocumento" Width="180px" runat="server" CssClass="input-xlarge" NullValue="0" NullText="0" Nullable="False"></ig:WebNumericEditor>
                        <asp:RequiredFieldValidator ID="RQ_NumDocumento" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNumDocumento" ErrorMessage=" * El campo 'Número Documento' es obligatorio." Width="400px" />
                    </div>

                    <%-- Nombre --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbNombre" runat="server" Text="Nombre:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtNombre" CssClass="input-xlarge" Width="320px" />
                        <asp:RequiredFieldValidator ID="RQ_Nombre" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtNombre" ErrorMessage=" * El campo 'Nombre' es obligatorio." Width="400px" />
                    </div>

                    <%-- Sector --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label1" runat="server" Text="Sector:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtSector" runat="server" Font-Overline="False" Height="20px" Width="320px" CssClass="newsInputD">
                            <asp:ListItem>Privado</asp:ListItem>
                            <asp:ListItem>Público</asp:ListItem>
                            <asp:ListItem>Mixto</asp:ListItem>
                        </asp:DropDownList>
                    </div>

                    <%-- Tipo --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label5" runat="server" Text="Tipo:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txttipo" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD" DataSourceID="SqlTipo" DataTextField="tipo" DataValueField="id" AutoPostBack="True">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlTipo" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [tipo] FROM [AdminTipoTercero] WHERE (activo=1 and[id_cliente] = @id_cliente) ORDER BY [tipo]">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" Type="Int64" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                    <%-- Actividad --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="Label3" runat="server" Text="Actividad:*" Width="180px" />
                        </b>
                        <asp:DropDownList ID="txtActividad" runat="server" Font-Overline="False" Height="22px" Width="320px" CssClass="newsInputD" DataSourceID="SqlActividad" DataTextField="actividad" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="SqlActividad" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [actividad] FROM [AdminActividad] WHERE (activo=1 and[id_cliente] = @id_cliente) ORDER BY [actividad]">
                            <SelectParameters>
                                <asp:SessionParameter DefaultValue="0" Name="id_cliente" SessionField="id_cliente" Type="Int64" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </div>

                    <%-- Dirección --%>
                    <div>
                        <b class="control-label">
                            <asp:Label ID="lbDireccion" runat="server" Text="Dirección:*" Width="180px" />
                        </b>
                        <asp:TextBox runat="server" ID="txtDireccion" CssClass="input-xlarge" Width="320px" />
                        <asp:RequiredFieldValidator ID="RQ_Direccion" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtDireccion" ErrorMessage=" * El campo 'Dirección' es obligatorio." Width="400px" />
                    </div>

                    <%-- País / Dpto / Ciudad --%>
                    <div>
                        <table style="width:100%;">
                            <tr>
                                <td style="width:33%;">
                                    <div>
                                        <b class="control-label"><asp:Label ID="Label6" runat="server" Text="Pais:*" Width="120px" /></b>
                                        <asp:DropDownList ID="txtpais" runat="server" Width="200px" DataSourceID="SqlPais" DataTextField="Pais" DataValueField="id" AutoPostBack="True"></asp:DropDownList>
                                        <asp:SqlDataSource ID="SqlPais" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [Pais] FROM [AdminPais] ORDER BY [Pais]"></asp:SqlDataSource>
                                    </div>
                                </td>
                                <td style="width:33%;">
                                    <b class="control-label"><asp:Label ID="Label7" runat="server" Text="Estado:*" Width="120px" /></b>
                                    <asp:DropDownList ID="txtDpto" runat="server" Width="200px" DataSourceID="SqlDpto" DataTextField="departamento" DataValueField="departamento" AutoPostBack="True"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlDpto" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT DISTINCT [departamento] FROM [Admin_Ciudades] WHERE ([id_pais] = @id_pais)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtpais" DefaultValue="0" Name="id_pais" PropertyName="SelectedValue" Type="Int64" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                                <td style="width:34%;">
                                    <b class="control-label"><asp:Label ID="Label8" runat="server" Text="Ciudad:*" Width="120px" /></b>
                                    <asp:DropDownList ID="txtCiudad" runat="server" Width="200px" DataSourceID="SqlCiudad" DataTextField="ciudad" DataValueField="codigo"></asp:DropDownList>
                                    <asp:SqlDataSource ID="SqlCiudad" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [codigo], [ciudad] FROM [Admin_Ciudades] WHERE ([departamento] = @departamento) ORDER BY [ciudad]">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="txtDpto" DefaultValue="" Name="departamento" PropertyName="SelectedValue" Type="String" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%-- Teléfono --%>
                    <div>
                        <b class="control-label"><asp:Label ID="lbTelefono" runat="server" Text="Teléfono:*" Width="180px" /></b>
                        <asp:TextBox runat="server" ID="txtTelefono" CssClass="input-xlarge" Width="180px" />
                        <asp:RequiredFieldValidator ID="RQ_Telefono" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtTelefono" ErrorMessage=" * El campo 'Telefono' es obligatorio." Width="400px" />
                    </div>

                    <%-- Celular --%>
                    <div>
                        <b class="control-label"><asp:Label ID="lbCelular" runat="server" Text="Celular:*" Width="180px" /></b>
                        <asp:TextBox runat="server" ID="txtCelular" CssClass="input-xlarge" Width="180px" />
                        <asp:RequiredFieldValidator ID="RQ_Celular" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCelular" ErrorMessage=" * El campo 'Celular' es obligatorio." Width="400px" />
                    </div>

                    <%-- Correo --%>
                    <div>
                        <b class="control-label"><asp:Label ID="lbCorreo" runat="server" Text="Correo:*" Width="180px" /></b>
                        <asp:TextBox runat="server" ID="txtCorreo" CssClass="input-xlarge" Width="320px" />
                        <asp:RequiredFieldValidator ID="RQ_Correo" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtCorreo" ErrorMessage=" * El campo 'Correo' es obligatorio." Width="400px" />
                    </div>

                    <%-- Activo --%>
                    <div>
                        <b class="control-label"><asp:Label ID="Label2" runat="server" Text="Activo:*" Width="180px" /></b>
                        <asp:CheckBox ID="Chkactivo" runat="server" CssClass="control-label" Text="    " TextAlign="Left" />
                    </div>

                    <%-- ==========================================================
                         MANEJA FIRMA — checkbox original + campos originales
                         MODIFICACIÓN: onclick llama a onChkFirmaClick (solo JS)
                         El AutoPostBack del servidor sigue funcionando igual.
                    ========================================================== --%>
                    <div>
                        <table>
                            <tr>
                                <td>
                                    <b class="control-label">
                                        <asp:Label ID="Label4" runat="server" Text=" Maneja Firma:*" Width="111px" />
                                    </b>
                                </td>
                                <td style="width:10%">
                                    <%-- onclick se ejecuta ANTES del AutoPostBack; retorna true para no cancelarlo --%>
                                    <asp:CheckBox ID="ChkFirma" runat="server" CssClass="control-label" Text="    " TextAlign="Left"
                                        AutoPostBack="True" onclick="return onChkFirmaClick(this);" />
                                </td>
                                <td style="width:80%">
                                    <b class="control-label">
                                        <asp:Label ID="Lblvalida" runat="server" Text="Contraseña Firma:*" Width="180px" Visible="false" />
                                    </b>
                                    <asp:TextBox runat="server" ID="txtvalida" CssClass="input-xlarge" Width="320px" Visible="false" TextMode="Password" />
                                    <%-- OnClientClick retorna false para PREVENIR el postback del Button
                                         y en su lugar abre el modal JS --%>
                                    <asp:Button ID="BttFirma" runat="server" Text="Ver Firma" CssClass="btn btn-primary"
                                        ValidationGroup="Errores"
                                        OnClientClick="return abrirModalVerFirma();" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <%-- Archivo Firma (original, sin cambios) --%>
                    <div>
                        <table>
                            <tr>
                                <td style="width:10%">
                                    <b class="control-label">
                                        <asp:Label ID="Lblfirma" runat="server" Text="Archivo Firma:" Width="117px" Height="18px" Visible="false" />
                                    </b>
                                </td>
                                <td style="width:90%;align-items:flex-start">
                                    <asp:FileUpload ID="flDocumentos" runat="server" CssClass="input-xlarge" Width="320px" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>

                    <div>
                        <b class="control-label">
                            <asp:Label ID="Lblpwd" runat="server" Text="Contraseña Firma:*" Width="180px" Visible="false" />
                        </b>
                        <asp:TextBox runat="server" ID="Txtpwd" CssClass="input-xlarge" Width="320px" Visible="false" TextMode="Password" />
                    </div>

                    <div>
                        <b class="control-label">
                            <asp:Label ID="lblmensajeria" runat="server" Text="Mensajeria:" Width="180px" Visible="false" />
                        </b>
                        <asp:CheckBox ID="Chkmensajeria" runat="server" CssClass="control-label" Text="    " TextAlign="Left" Visible="false" />
                    </div>

                    <%-- ==========================================================
                         NUEVO: Panel Firma Electrónica + Digital
                         Visible solo cuando ChkFirma está marcado.
                         100% JS / visual — sin postback ni BD.
                    ========================================================== --%>
                    <div id="divSeccionFirmas">
                        <br />
                        <div>
                            <b class="control-label" style="display:inline-block; width:180px; vertical-align:top; padding-top:6px;">
                                Tipo de Firma:
                            </b>
                            <div class="firma-content-col">

                                <%-- ---- FIRMA ELECTRÓNICA ---- --%>
                                <div class="firma-section-wrapper">
                                    <div class="firma-tipo-header">
                                        <i class="fa fa-pen-to-square"></i>
                                        Firma Electrónica
                                        <span id="badge_e" class="firma-estado-badge badge-no-configurada">
                                            <i class="fa fa-xmark"></i> No configurada
                                        </span>
                                    </div>
                                    <div class="firma-row">
                                        <span class="firma-info-resumen" id="info_e">
                                            Sin datos — haga clic en <strong>Configurar</strong> para asignar.
                                        </span>
                                    </div>
                                    <div class="firma-row firma-fila-row">
                                        <button type="button" class="btn btn-primary btn-firma-accion"
                                            onclick="abrirModalFirmaE()">
                                            <i class="fa fa-gear"></i> Configurar
                                        </button>
                                        <button type="button" class="btn btn-default btn-firma-accion"
                                            onclick="eliminarFirma('electronica')">
                                            <i class="fa fa-trash"></i> Eliminar
                                        </button>
                                        <span class="firma-info-resumen">
                                            <i class="fa fa-circle-info"></i>
                                            Autoriza documentos mediante PIN personal (no requiere certificado)
                                        </span>
                                    </div>
                                </div>

                                <div class="firma-separador"></div>

                                <%-- ---- FIRMA DIGITAL ---- --%>
                                <div class="firma-section-wrapper">
                                    <div class="firma-tipo-header">
                                        <i class="fa fa-certificate"></i>
                                        Firma Digital
                                        <span id="badge_d" class="firma-estado-badge badge-no-configurada">
                                            <i class="fa fa-xmark"></i> No configurada
                                        </span>
                                    </div>
                                    <div class="firma-row">
                                        <span class="firma-info-resumen" id="info_d">
                                            Sin certificado — haga clic en <strong>Cargar Certificado</strong> para registrar.
                                        </span>
                                    </div>
                                    <div class="firma-row firma-fila-row">
                                        <button type="button" class="btn btn-primary btn-firma-accion"
                                            onclick="abrirModalFirmaD()">
                                            <i class="fa fa-upload"></i> Cargar Certificado
                                        </button>
                                        <button type="button" class="btn btn-default btn-firma-accion"
                                            onclick="eliminarFirma('digital')">
                                            <i class="fa fa-trash"></i> Eliminar
                                        </button>
                                        <span class="firma-info-resumen">
                                            <i class="fa fa-circle-info"></i>
                                            Requiere certificado .p12/.pfx emitido por entidad certificadora (ej. Certicámara)
                                        </span>
                                    </div>
                                </div>

                            </div><%-- fin firma-content-col --%>
                        </div>
                        <br />
                    </div><%-- fin divSeccionFirmas --%>

                    <%-- Area / Jefe / Cargo (originales) --%>
                    <div>
                        <b class="control-label"><asp:Label ID="lbArea" runat="server" Text="Area:" Width="180px" /></b>
                        <asp:TextBox runat="server" ID="txtArea" Width="320px" CssClass="input-xlarge" ViewStateMode="Enabled"></asp:TextBox>
                        <asp:Image ID="R1" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttBuscarArea" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" Visible="False" />
                    </div>
                    <div>
                        <b class="control-label"><asp:Label ID="lbDoc" runat="server" Text="Jefe:" Width="180px" /></b>
                        <asp:TextBox runat="server" ID="txtTercero" CssClass="input-xlarge" Width="320px" ViewStateMode="Enabled" />
                        <asp:Image ID="R5" runat="server" ImageUrl="~/Images/Rec.png" />
                        <asp:ImageButton ID="bttBuscarTercero" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" style="width: 16px" Visible="False" />
                    </div>
                    <div>
                        <b class="control-label"><asp:Label ID="LblCargo" runat="server" Text="Cargo:" Width="180px" Visible="false" /></b>
                        <asp:TextBox runat="server" ID="txtcargo" CssClass="input-xlarge" Width="320px" ViewStateMode="Enabled" Visible="false" />
                    </div>

                    <br />
                    <asp:Button ID="btOk" runat="server" Text="OK" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />
                    <asp:Button ID="btCancel" ValidationGroup='Ninguno' runat="server" Text="Cancel" OnClick="btCancel_Click" CssClass="btn btn-primary-right" />
                    <br /><br />
                    <asp:CustomValidator ID="cvPagina" runat="server" Display="Dynamic" ErrorMessage="" ValidationGroup="Errores" CssClass="help-block"></asp:CustomValidator>
                    <asp:HiddenField ID="Hdd_id_area" runat="server" Value="0" />
                    <asp:HiddenField ID="Hdd_id_tercero" runat="server" Value="0" />
                    <asp:TextBox runat="server" ID="txtcuantos" Height="70" TextMode="MultiLine" Rows="5" Width="280px" Visible="false" />

                </div>
            </div>
        </div>
    </div>


    <%-- ================================================================
         MODALES — puros HTML/JS, sin runat="server", sin postback
    ================================================================ --%>

    <%-- MODAL: Configurar Firma Electrónica --%>
    <div id="modalFirmaE" class="firma-modal-overlay">
        <div class="firma-modal-box">
            <div class="firma-modal-header">
                <h4><i class="fa fa-pen-to-square"></i>&nbsp; Configurar Firma Electrónica</h4>
                <button type="button" class="btn-cerrar-modal" onclick="cerrarModalFirmaE()">
                    <i class="fa fa-xmark"></i>
                </button>
            </div>
            <div class="firma-modal-body">

                <div class="f-alert f-info" style="display:block; margin-bottom:10px;">
                    <i class="fa fa-circle-info"></i>
                    La firma electrónica permite al firmante autorizar documentos mediante un PIN personal.
                    &nbsp;<strong>[Datos almacenan en BD]</strong>
                </div>

                <div class="fmg">
                    <label><i class="fa fa-user"></i> Nombre completo del firmante *</label>
                    <input type="text" id="fe_nombre" placeholder="Ej: Juan Carlos Pérez López" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-envelope"></i> Correo electrónico *</label>
                    <input type="email" id="fe_email" placeholder="correo@entidad.gov.co" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-briefcase"></i> Cargo *</label>
                    <input type="text" id="fe_cargo" placeholder="Ej: Jefe de División" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-lock"></i> PIN de firma * &nbsp;<small style="color:#888;font-weight:normal;">(mín. 4 caracteres)</small></label>
                    <input type="password" id="fe_pin" placeholder="••••••" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-lock"></i> Confirmar PIN *</label>
                    <input type="password" id="fe_pinc" placeholder="••••••" />
                </div>

                <div id="alerta_e" class="f-alert"></div>
            </div>
            <div class="firma-modal-footer">
                <button type="button" class="btn btn-default" onclick="cerrarModalFirmaE()">
                    <i class="fa fa-ban"></i> Cancelar
                </button>
                <button type="button" class="btn btn-primary" onclick="guardarFirmaE()">
                    <i class="fa fa-floppy-disk"></i> Guardar
                </button>
            </div>
        </div>
    </div>

    <%-- MODAL: Configurar Firma Digital --%>
    <div id="modalFirmaD" class="firma-modal-overlay">
        <div class="firma-modal-box">
            <div class="firma-modal-header">
                <h4><i class="fa fa-certificate"></i>&nbsp; Configurar Firma Digital</h4>
                <button type="button" class="btn-cerrar-modal" onclick="cerrarModalFirmaD()">
                    <i class="fa fa-xmark"></i>
                </button>
            </div>
            <div class="firma-modal-body">

                <div class="f-alert f-info" style="display:block; margin-bottom:10px;">
                    <i class="fa fa-circle-info"></i>
                    La firma digital requiere un certificado emitido por entidad certificadora acreditada (ej. Certicámara).
                    &nbsp;<strong>[Datos demo — no se almacenan en BD]</strong>
                </div>

                <div class="fmg">
                    <label><i class="fa fa-file-arrow-up"></i> Cargar archivo de certificado (.p12 / .pfx)</label>
                    <input type="file" id="fd_archivo" accept=".p12,.pfx,.cer" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-id-card"></i> Titular del certificado *</label>
                    <input type="text" id="fd_titular"
                        placeholder="Ej: JUAN CARLOS PEREZ LOPEZ - CC 80123456"
                        oninput="refrescarPreview()" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-building"></i> Entidad certificadora *</label>
                    <select id="fd_entidad" onchange="refrescarPreview()">
                        <option value="">-- Seleccione --</option>
                        <option value="Certicámara S.A.">Certicámara S.A.</option>
                        <option value="Firma Digital Colombia">Firma Digital Colombia</option>
                        <option value="GSE Group">GSE Group</option>
                        <option value="Andes SCD">Andes SCD</option>
                    </select>
                </div>
                <div class="fmg">
                    <label><i class="fa fa-calendar-days"></i> Fecha de vencimiento del certificado *</label>
                    <input type="text" id="fd_vence" placeholder="AAAA-MM-DD"
                        oninput="refrescarPreview()" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-lock"></i> PIN del certificado * &nbsp;<small style="color:#888;font-weight:normal;">(mín. 4 caracteres)</small></label>
                    <input type="password" id="fd_pin" placeholder="••••••" />
                </div>
                <div class="fmg">
                    <label><i class="fa fa-eye"></i> Vista previa del certificado</label>
                    <div class="firma-preview-cert" id="cert_preview">
                        <i class="fa fa-certificate"></i>
                        Complete los datos para ver la vista previa
                    </div>
                </div>

                <div id="alerta_d" class="f-alert"></div>
            </div>
            <div class="firma-modal-footer">
                <button type="button" class="btn btn-default" onclick="cerrarModalFirmaD()">
                    <i class="fa fa-ban"></i> Cancelar
                </button>
                <button type="button" class="btn btn-primary" onclick="guardarFirmaD()">
                    <i class="fa fa-floppy-disk"></i> Registrar Certificado
                </button>
            </div>
        </div>
    </div>

    <%-- MODAL: Ver / Verificar Firma (flujo del botón original "Ver Firma") --%>
    <div id="modalVerFirma" class="firma-modal-overlay">
        <div class="firma-modal-box">
            <div class="firma-modal-header">
                <h4><i class="fa fa-eye"></i>&nbsp; Verificar Firma</h4>
                <button type="button" class="btn-cerrar-modal" onclick="cerrarModalVerFirma()">
                    <i class="fa fa-xmark"></i>
                </button>
            </div>
            <div class="firma-modal-body">

                <div class="f-alert f-info" style="display:block; margin-bottom:10px;">
                    <i class="fa fa-circle-info"></i>
                    Se verificará la contraseña ingresada y se mostrará la información de la firma registrada.
                    &nbsp;<strong>[DEMO]</strong>
                </div>

                <div class="firma-preview-cert" style="margin-bottom:12px;">
                    <i class="fa fa-pen-to-square" style="font-size:22px; color:#337ab7;"></i>
                    <strong>Firma Electrónica</strong><br />
                    Juan Carlos Pérez López<br />
                    <span style="color:#5cb85c;"><i class="fa fa-circle-check"></i> Activa</span>
                </div>

                <div id="alerta_ver" class="f-alert"></div>
            </div>
            <div class="firma-modal-footer">
                <button type="button" class="btn btn-default" onclick="cerrarModalVerFirma()">
                    <i class="fa fa-ban"></i> Cerrar
                </button>
                <button type="button" class="btn btn-primary" onclick="confirmarVerFirma()">
                    <i class="fa fa-magnifying-glass"></i> Verificar
                </button>
            </div>
        </div>
    </div>

</asp:Content>
