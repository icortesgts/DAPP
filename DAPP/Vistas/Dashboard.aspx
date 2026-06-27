<%@ Page Title="Dashboard" Language="VB" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Dashboard.aspx.vb" Inherits="DAPP.Dashboard" ResponseEncoding="UTF-8" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">

    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/Chart.js/3.9.1/chart.min.js"></script>

    <style type="text/css">
        /* ── KPIs ── */
        .dash-kpi-row           { margin-bottom: 20px; }
        .kpi-card               { background: #fff; border: 1px solid #ddd; border-radius: 4px;
                                   padding: 14px 16px; box-shadow: 0 1px 2px rgba(0,0,0,0.06);
                                   border-left: 4px solid #337ab7; min-height: 90px; position: relative; }
        .kpi-card.kpi-info      { border-left-color: #4a90d9; }
        .kpi-card.kpi-danger    { border-left-color: #d9534f; }
        .kpi-card.kpi-warning   { border-left-color: #f0ad4e; }
        .kpi-card.kpi-success   { border-left-color: #5cb85c; }
        .kpi-icono              { position: absolute; right: 14px; top: 14px; font-size: 28px; color: #eaeaea; }
        .kpi-valor              { font-size: 26px; font-weight: bold; color: #333; line-height: 1; }
        .kpi-label              { font-size: 11px; color: #888; text-transform: uppercase;
                                   margin-top: 6px; letter-spacing: .3px; }

        /* ── Paneles de gráfico ── */
        .panel-dashboard        { background: #fff; border: 1px solid #ddd; border-radius: 3px; margin-bottom: 20px; }
        .panel-dashboard .panel-header {
                                   background: #4a90d9; color: #fff; padding: 9px 14px;
                                   border-radius: 3px 3px 0 0; font-weight: bold; font-size: 13px;
                                   display: -webkit-box; display: -ms-flexbox; display: flex;
                                   -webkit-box-align: center; -ms-flex-align: center; align-items: center;
                                   -webkit-box-pack: justify; -ms-flex-pack: justify; justify-content: space-between; }
        .panel-dashboard .panel-header a       { color: #fff; font-size: 11px; font-weight: normal; }
        .panel-dashboard .panel-header a:hover { text-decoration: underline; }
        .panel-body-dash        { padding: 14px; }

        /* ── Área del canvas ── */
        .chart-wrap             { position: relative; height: 240px; overflow: hidden; }
        .chart-wrap canvas      { position: absolute; top: 0; left: 0; width: 100% !important; height: 100% !important; }

        /* ── Leyenda personalizada debajo del gráfico ── */
        .chart-legend           { margin-top: 8px; padding: 0; list-style: none;
                                   display: -webkit-box; display: -ms-flexbox; display: flex;
                                   -ms-flex-wrap: wrap; flex-wrap: wrap; gap: 6px 14px;
                                   font-size: 11px; color: #555; }
        .chart-legend li        { display: -webkit-box; display: -ms-flexbox; display: flex;
                                   -webkit-box-align: center; -ms-flex-align: center; align-items: center; gap: 5px; }
        .chart-legend .leg-dot  { width: 10px; height: 10px; border-radius: 50%;
                                   display: inline-block; -ms-flex-negative: 0; flex-shrink: 0; }
    </style>

    <div class="container mrgnBotMd">

        <%-- ── Cabecera ── --%>
        <div class="row noPadding">
            <div class="col-sm-8 noPadding colHeight">
                <h3>DASHBOARD GESTIÓN DOCUMENTAL</h3>
            </div>
            <div class="col-sm-4" style="text-align:right; padding-top:16px;">
                <asp:Label ID="lblFechaActualizacion" runat="server" style="font-size:12px;color:#888;margin-right:10px;" />
                <asp:Button ID="btnActualizar" runat="server" Text="Actualizar" CssClass="btn btn-default btn-sm" ValidationGroup="Ninguno" />
            </div>
        </div>

        <%-- ── KPIs ── --%>
        <div class="row dash-kpi-row">
            <div class="col-sm-3">
                <div class="kpi-card kpi-info">
                    <i class="fa fa-book kpi-icono"></i>
                    <div class="kpi-valor"><asp:Literal ID="litKpiPrestamosVigentes" runat="server" /></div>
                    <div class="kpi-label">Préstamos Vigentes</div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="kpi-card kpi-danger">
                    <i class="fa fa-exclamation-triangle kpi-icono"></i>
                    <div class="kpi-valor"><asp:Literal ID="litKpiPrestamosVencidos" runat="server" /></div>
                    <div class="kpi-label">Préstamos Vencidos</div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="kpi-card kpi-warning">
                    <i class="fa fa-archive kpi-icono"></i>
                    <div class="kpi-valor"><asp:Literal ID="litKpiArchivosPendientes" runat="server" /></div>
                    <div class="kpi-label">Doc. Pend. Transferencia</div>
                </div>
            </div>
            <div class="col-sm-3">
                <div class="kpi-card kpi-success">
                    <i class="fa fa-list-alt kpi-icono"></i>
                    <div class="kpi-valor"><asp:Literal ID="litKpiChequeosPendientes" runat="server" /></div>
                    <div class="kpi-label">Items Lista Chequeo Pend.</div>
                </div>
            </div>
        </div>

        <%-- ════════════════════ FILA 1: PRÉSTAMOS ════════════════════ --%>
        <div class="row">
            <%-- Gráfico 1: Doughnut – Estado de Préstamos --%>
            <div class="col-sm-5">
                <div class="panel-dashboard">
                    <div class="panel-header">
                        <span><i class="fa fa-book"></i>&nbsp; Estado de Préstamos</span>
                        <a href="Page_Prestamos.aspx">Ver módulo <i class="fa fa-angle-right"></i></a>
                    </div>
                    <div class="panel-body-dash">
                        <div class="chart-wrap"><canvas id="chartEstadoPrestamos"></canvas></div>
                        <ul class="chart-legend" id="legendEstadoPrestamos"></ul>
                    </div>
                </div>
            </div>
            <%-- Gráfico 2: Barras – Top retrasos --%>
            <div class="col-sm-7">
                <div class="panel-dashboard">
                    <div class="panel-header">
                        <span><i class="fa fa-clock-o"></i>&nbsp; Préstamos con Mayor Días de Retraso</span>
                    </div>
                    <div class="panel-body-dash">
                        <div class="chart-wrap"><canvas id="chartTopRetraso"></canvas></div>
                        <ul class="chart-legend">
                            <li><span class="leg-dot" style="background:#d9534f;"></span>Días de retraso acumulados</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <%-- ════════════════════ FILA 2: ARCHIVOS ════════════════════ --%>
        <div class="row">
            <%-- Gráfico 3: Barras – Pendientes por etapa --%>
            <div class="col-sm-5">
                <div class="panel-dashboard">
                    <div class="panel-header">
                        <span><i class="fa fa-archive"></i>&nbsp; Doc. Pendientes por Etapa de Archivo</span>
                        <a href="Page_EstadoArchivo.aspx">Ver módulo <i class="fa fa-angle-right"></i></a>
                    </div>
                    <div class="panel-body-dash">
                        <div class="chart-wrap"><canvas id="chartPendientesEtapa"></canvas></div>
                        <ul class="chart-legend" id="legendPendientesEtapa"></ul>
                    </div>
                </div>
            </div>
            <%-- Gráfico 4: Barras horizontales – Top permanencia --%>
            <div class="col-sm-7">
                <div class="panel-dashboard">
                    <div class="panel-header">
                        <span><i class="fa fa-hourglass-half"></i>&nbsp; Mayor Permanencia Adicional (meses)</span>
                    </div>
                    <div class="panel-body-dash">
                        <div class="chart-wrap"><canvas id="chartTopPermanencia"></canvas></div>
                        <ul class="chart-legend">
                            <li><span class="leg-dot" style="background:#f0ad4e;"></span>Meses adicionales sobre el límite permitido</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

        <%-- ════════════════════ FILA 3: LISTAS DE CHEQUEO ════════════════════ --%>
        <div class="row">
            <%-- Gráfico 5: Doughnut – Estado verificación --%>
            <div class="col-sm-5">
                <div class="panel-dashboard">
                    <div class="panel-header">
                        <span><i class="fa fa-list-alt"></i>&nbsp; Estado de Verificación (Chequeos)</span>
                        <a href="Page_EstadoListas.aspx">Ver módulo <i class="fa fa-angle-right"></i></a>
                    </div>
                    <div class="panel-body-dash">
                        <div class="chart-wrap"><canvas id="chartEstadoChequeos"></canvas></div>
                        <ul class="chart-legend" id="legendEstadoChequeos"></ul>
                    </div>
                </div>
            </div>
            <%-- Gráfico 6: Barras – Faltantes por proceso --%>
            <div class="col-sm-7">
                <div class="panel-dashboard">
                    <div class="panel-header">
                        <span><i class="fa fa-files-o"></i>&nbsp; Documentos Faltantes por Proceso</span>
                    </div>
                    <div class="panel-body-dash">
                        <div class="chart-wrap"><canvas id="chartFaltantesProceso"></canvas></div>
                        <ul class="chart-legend">
                            <li><span class="leg-dot" style="background:#337ab7;"></span>Total de documentos faltantes por proceso</li>
                        </ul>
                    </div>
                </div>
            </div>
        </div>

    </div><%-- /container --%>

    <%-- Literales ocultos que inyectan JSON generado en el code-behind --%>
    <asp:Literal ID="litJsonEstadoPrestamos"  runat="server" />
    <asp:Literal ID="litJsonTopRetraso"       runat="server" />
    <asp:Literal ID="litJsonPendientesEtapa"  runat="server" />
    <asp:Literal ID="litJsonTopPermanencia"   runat="server" />
    <asp:Literal ID="litJsonEstadoChequeos"   runat="server" />
    <asp:Literal ID="litJsonFaltantesProceso" runat="server" />

    <script type="text/javascript">

        // ── Datos para los gráficos (inyectados por el code-behind) ──
        var datosEstadoPrestamos  = __json_EstadoPrestamos;
        var datosTopRetraso       = __json_TopRetraso;
        var datosPendientesEtapa  = __json_PendientesEtapa;
        var datosTopPermanencia   = __json_TopPermanencia;
        var datosEstadoChequeos   = __json_EstadoChequeos;
        var datosFaltantesProceso = __json_FaltantesProceso;

        // ── Helper: genera leyenda HTML a partir de labels+colores del dataset ──
        function buildLegend(containerId, labels, colores) {
            var ul = document.getElementById(containerId);
            if (!ul) return;
            ul.innerHTML = '';
            for (var i = 0; i < labels.length; i++) {
                var color = Array.isArray(colores) && colores.length > 1 ? colores[i] : colores[0];
                var li = document.createElement('li');
                li.innerHTML = '<span class="leg-dot" style="background:' + color + ';"></span>' + labels[i];
                ul.appendChild(li);
            }
        }

        function initCharts() {

            // ── 1. Doughnut: Estado de Préstamos ──
            new Chart(document.getElementById('chartEstadoPrestamos'), {
                type: 'doughnut',
                data: {
                    labels: datosEstadoPrestamos.Labels,
                    datasets: [{ data: datosEstadoPrestamos.Data, backgroundColor: datosEstadoPrestamos.Colores }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: { legend: { display: false } },
                    cutout: '60%'
                }
            });
            buildLegend('legendEstadoPrestamos', datosEstadoPrestamos.Labels, datosEstadoPrestamos.Colores);

            // ── 2. Barras: Top retrasos ──
            new Chart(document.getElementById('chartTopRetraso'), {
                type: 'bar',
                data: {
                    labels: datosTopRetraso.Labels,
                    datasets: [{ label: 'Días de Retraso', data: datosTopRetraso.Data, backgroundColor: '#d9534f' }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: { legend: { display: false } },
                    scales: {
                        y: { beginAtZero: true, ticks: { stepSize: 1, precision: 0 } },
                        x: { ticks: { font: { size: 10 } } }
                    }
                }
            });

            // ── 3. Barras: Pendientes por etapa ──
            new Chart(document.getElementById('chartPendientesEtapa'), {
                type: 'bar',
                data: {
                    labels: datosPendientesEtapa.Labels,
                    datasets: [{ label: 'Documentos', data: datosPendientesEtapa.Data, backgroundColor: datosPendientesEtapa.Colores }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: { legend: { display: false } },
                    scales: { y: { beginAtZero: true, ticks: { stepSize: 1, precision: 0 } } }
                }
            });
            buildLegend('legendPendientesEtapa', datosPendientesEtapa.Labels, datosPendientesEtapa.Colores);

            // ── 4. Barras horizontales: Top permanencia adicional ──
            new Chart(document.getElementById('chartTopPermanencia'), {
                type: 'bar',
                data: {
                    labels: datosTopPermanencia.Labels,
                    datasets: [{ label: 'Meses adicionales', data: datosTopPermanencia.Data, backgroundColor: '#f0ad4e' }]
                },
                options: {
                    maintainAspectRatio: false,
                    indexAxis: 'y',
                    plugins: { legend: { display: false } },
                    scales: { x: { beginAtZero: true, ticks: { stepSize: 1, precision: 0 } } }
                }
            });

            // ── 5. Doughnut: Estado de Chequeos ──
            new Chart(document.getElementById('chartEstadoChequeos'), {
                type: 'doughnut',
                data: {
                    labels: datosEstadoChequeos.Labels,
                    datasets: [{ data: datosEstadoChequeos.Data, backgroundColor: datosEstadoChequeos.Colores }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: { legend: { display: false } },
                    cutout: '60%'
                }
            });
            buildLegend('legendEstadoChequeos', datosEstadoChequeos.Labels, datosEstadoChequeos.Colores);

            // ── 6. Barras: Faltantes por proceso ──
            new Chart(document.getElementById('chartFaltantesProceso'), {
                type: 'bar',
                data: {
                    labels: datosFaltantesProceso.Labels,
                    datasets: [{ label: 'Documentos Faltantes', data: datosFaltantesProceso.Data, backgroundColor: '#337ab7' }]
                },
                options: {
                    maintainAspectRatio: false,
                    plugins: { legend: { display: false } },
                    scales: { y: { beginAtZero: true, ticks: { stepSize: 1, precision: 0 } } }
                }
            });
        }

        window.onload = function () { initCharts(); };

    </script>

</asp:Content>
