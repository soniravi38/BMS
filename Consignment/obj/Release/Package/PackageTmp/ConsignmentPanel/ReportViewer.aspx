<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="ReportViewer.aspx.cs" Inherits="Consignment.ConsignmentPanel.ReportViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Button ID="btnConsignor" runat="server" CssClass="btn btn-success" Text="Consignor Copy" OnClick="btnConsignor_Click" />
    <asp:Button ID="btnConsignee" runat="server" CssClass="btn btn-success" Text="Consignee Copy" OnClick="btnConsignee_Click" />
    <asp:Button ID="btnCustomer" runat="server" CssClass="btn btn-success" Text="Account Copy" OnClick="btnCustomer_Click" />
    <asp:Button ID="btnDriver" runat="server" CssClass="btn btn-success" Text="Driver Copy" OnClick="btnDriver_Click" />
    <asp:Button ID="btnAll" runat="server" CssClass="btn btn-success" Text="All Copy" OnClick="btnAll_Click" />
</asp:Content>
