<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Site.Master" CodeBehind="SinAcceso.aspx.vb" Inherits="DAPP.SinAcceso" %>

<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <link rel="stylesheet" type="text/css" href="../../Styles/Grids.css">
    <link rel="stylesheet" type="text/css" href="../../Styles/styles.css">
    <div style="vertical-align: middle; text-align: center">
    <asp:Label ID="lbMensaje" runat="server" Font-Bold="True" Font-Names="Verdana" Font-Size="Large" ForeColor="#FF4646"></asp:Label>
        <br />
        <br />
        <asp:Button ID="bttHome" CssClass="boton-form-home-a" runat="server" Text="Inicio" />
    </div>
</asp:Content>

