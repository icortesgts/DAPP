<%@ Page Title="" Language="VB" AutoEventWireup="true" CodeBehind="Seleccionar_Tercero.aspx.vb" Inherits="DAPP.Seleccionar_Tercero" %>

<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.GridControls" TagPrefix="ig" %>
<%@ Register Assembly="Infragistics4.Web.v14.2, Version=14.2.20142.2590, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb" Namespace="Infragistics.Web.UI.EditorControls" TagPrefix="ig" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <meta charset="utf-8">
    <title>SELECCIONAR TERCERO</title>
    <link rel="stylesheet" type="text/css" href="../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../Styles/styles_form.css">    
    <style type="text/css">
        .altura {
            height: 45px;
        }       
        
    </style>
</head>
<body>
    <form id="form1" runat="server" class="formulario">
        <asp:ScriptManager ID="MGR" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="AJAX_Panel" runat="server">
            <ContentTemplate>

                <div class="container mrgnBotMd" style="width: 100%;">
                    <div class="row noPadding">
                        <div class="col-sm-12 noPadding colHeight">
                            <h3>SELECCIONAR TERCERO</h3>
                            <div class="form-horizontal fom-border">

                                <table style="width: 100%;">
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>                                                                        
                                    <tr>
                                        <td class="altura">
                                            <asp:Label ID="lbFiltro" runat="server" Text="Identificacion / Nombre" class="control-label"></asp:Label>
                                        </td>
                                        <td class="altura">
                                            <asp:TextBox ID="txtFiltro" runat="server" CssClass="input-xlarge" Width="200px"></asp:TextBox>
                                            <asp:Image ID="Rec01" runat="server" ImageUrl="~/Images/Rec.png" />
                                            <asp:ImageButton ID="bttBuscarTercero" runat="server" ImageUrl="~/Images/find.png" ImageAlign="Middle" />
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td colspan="2">

                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 50px">
                                                        <asp:ImageButton ID="bttNuevo" ToolTip="Nuevo Tercero" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoNew.png" BackColor="Transparent" Width="20" Height="20" />
                                                    </td>
                                                    <td style="width: 50px">
                                                        <asp:ImageButton ID="bttEditar" ToolTip="Editar Tercero" ValidationGroup="Ninguno" runat="server" ImageUrl="~/Images/icoEdit.png" BackColor="Transparent" Width="20" Height="20" />
                                                    </td>
                                                    <td style="width: 50px">&nbsp;</td>
                                                    <td style="width: 50px">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="text-align: center">
                                            <ig:WebDataGrid ID="GridDatos" runat="server" Width="100%" Height="250px" AutoGenerateColumns="False" HeaderCaptionCssClass="HeaderCaptionClass" StyleSetName="Office2007Blue" DataKeyFields="id" EnableDataViewState="True" DataSourceID="SRC_Datos">
                                                <columns>                                    
                                                    <ig:BoundDataField DataFieldName="tipo_documento" DataType="System.String" Key="tipo_documento">
                                                        <Header Text="Tipo Documento">
                                                        </Header>
                                                    </ig:BoundDataField>
                                                    <ig:BoundDataField DataFieldName="numero_documento" DataType="System.Int64" Key="numero_documento">
                                                        <Header Text="Documento">
                                                        </Header>
                                                    </ig:BoundDataField>
                                                    <ig:BoundDataField DataFieldName="nombre" DataType="System.String" Key="nombre">
                                                        <Header Text="Nombre">
                                                        </Header>
                                                    </ig:BoundDataField>                                                  
                                                </columns>
                                                <behaviors>                                                              
                                                    <ig:Selection CellClickAction="Row" RowSelectType="Single">
                                                    </ig:Selection>
                                                    <ig:Sorting SortingMode="Multi" Enabled="true" >                                    
                                                    </ig:Sorting>
                                                </behaviors>
                                            </ig:WebDataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="bttSeleccionar" runat="server" Text="Seleccionar Tercero" class="btn btn-primary" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:SqlDataSource ID="SRC_Datos" runat="server" ConnectionString="<%$ ConnectionStrings:DefaultConnection %>" SelectCommand="BuscarTercero" SelectCommandType="StoredProcedure">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="txtFiltro" DefaultValue="0" Name="Filtro" PropertyName="Text" Type="String" />
                                                    <asp:SessionParameter DefaultValue="0" Name="Sucursal" SessionField="id_cliente" Type="Int64" />
                                                    <asp:QueryStringParameter DefaultValue="-1" Name="interno" QueryStringField="interno" Type="Int32" />
                                                    <asp:QueryStringParameter DefaultValue="-1" Name="correspondencia" QueryStringField="correspondencia" Type="Int32" />
                                                </SelectParameters>
                                            </asp:SqlDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
