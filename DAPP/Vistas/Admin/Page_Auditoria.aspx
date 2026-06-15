<%@ Page Title="Auditoria" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="Page_Auditoria.aspx.vb" Inherits="DAPP.Page_Auditoria" %>

<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics45.Web.v15.2, Version=15.2.20152.2273, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">    
    <div class="titular-into-form">
        <span class="texto-titulo-form">AUDITORIA DE TRANSACCIONES</span>
        <ig:WebExcelExporter ID="ExpGrid" runat="server">
        </ig:WebExcelExporter>
        <ig:WebDocumentExporter ID="Exppdf" runat="server">
        </ig:WebDocumentExporter>
    </div>
    <br />
    <br />
    <div>
        <table style="width:100%">
            <tr>
                <td style="text-align: left;">
                    <b class="control-label">
                        <asp:Label ID="Label2" runat="server" Text="Fecha Inicio:*" Width="180px" />
                    </b>
                </td>
                <td>
                    <ig:WebDatePicker ID="txtFechaInicio" runat="server" DisplayModeFormat="d" Nullable="False" CssClass="input-xlarge"  Width="180px" SpinOnlyOneField="True" DropDownCalendarID="WMInicio">
                    </ig:WebDatePicker> 
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaInicio" ErrorMessage=" * El campo 'Fecha Inicio' es obligatorio." Width="400px" /> 
                    <ig:WebMonthCalendar ID="WMInicio" runat="server">
                    </ig:WebMonthCalendar>
                </td>
                <td>
                    <b class="control-label">
                        <asp:Label ID="Label6" runat="server" Text="Fecha Fin:*" Width="180px" />
                    </b>
                </td>
                <td>
                    <ig:WebDatePicker ID="TxtFechafin" runat="server" DisplayModeFormat="d" Nullable="False" CssClass="input-xlarge"  Width="180px" DropDownCalendarID="WMFin">
                    </ig:WebDatePicker>                                                
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="Errores" CssClass="help-block" ControlToValidate="txtFechaFin" ErrorMessage=" * El campo 'Fecha Fin' es obligatorio." Width="400px" />  
                    <ig:WebMonthCalendar ID="WMFin" runat="server">
                    </ig:WebMonthCalendar>
                </td>
                <td>
                    <asp:Button ID="btOk" runat="server" Text="Consultar" OnClick="btOk_Click" CssClass="btn btn-primary" ValidationGroup="Errores" />  
                </td>
            </tr>
            
        </table>
                                    
                                                                                                   
                                    
    </div> 
    <div class="DivFormulario">
        <table style="margin: auto; width: 100%;">
            <tr>
                <td style="text-align: center; width: 40px;">&nbsp;</td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttExcel" ToolTip="Exportar a Excel" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoExcel.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td style="text-align: center; width: 40px;">&nbsp;
                    <asp:ImageButton ID="bttPdf" ToolTip="Exportar a Pdf" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/pdf.png" BackColor="Transparent" Width="26" Height="26" />
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center;" colspan="8">
                    <ig:WebDataGrid ID="GridTipos" runat="server" Width="100%" AutoGenerateColumns="False" CellSpacing="2" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" EnableDataViewState="True" DataSourceID="Src_Tipos">
                        <Columns>
                            <ig:BoundDataField DataFieldName="Tabla" Key="Tabla">
                                <Header Text="Funcionalidad">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Operacion" Key="Operacion">
                                <Header Text="Operacion">
                                </Header>
                            </ig:BoundDataField>                                                                      
                            
                            <ig:BoundDataField DataFieldName="id_tabla" Key="id_tabla">
                                <Header Text="Identificador">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Observaciones" Key="Observaciones">
                                <Header Text="Referencia">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="usuario" Key="usuario">
                                <Header Text="Usuario">
                                </Header>
                            </ig:BoundDataField>
                            <ig:BoundDataField DataFieldName="Fecha" Key="Fecha" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}">
                                <Header Text="Fecha">
                                </Header>
                            </ig:BoundDataField>
                        </Columns>
                        <Behaviors>
                            <ig:Selection CellClickAction="Row" SelectedCellCssClass="SelectedCellClass" RowSelectType="Single">
                            </ig:Selection>
                            <ig:Sorting SortingMode="Single" Enabled="true">
                            </ig:Sorting>
                            <ig:Filtering>
                            </ig:Filtering>
                        </Behaviors>
                    </ig:WebDataGrid>
                    <asp:SqlDataSource ID="Src_Tipos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="SELECT [id], [Operacion], [Tabla], [id_tabla], [Observaciones], [usuario], [Fecha] FROM [Auditoria] WHERE (([id_sucursal] = @id_sucursal) AND ([Fecha] &gt;= @Fecha) AND ([Fecha] &lt;= @Fecha2 + 1))">
                        <SelectParameters>
                            <asp:SessionParameter DefaultValue="0" Name="id_sucursal" SessionField="id_sucursal" Type="Int64" />
                            <asp:ControlParameter ControlID="txtFechaInicio" DefaultValue="01/01/2016" Name="Fecha" PropertyName="Value" Type="DateTime" />
                            <asp:ControlParameter ControlID="TxtFechafin" DefaultValue="01/02/2016" Name="Fecha2" PropertyName="Value" Type="DateTime" />
                        </SelectParameters>
                    </asp:SqlDataSource>
                </td>
            </tr>
        </table>
    </div>

</asp:Content>



