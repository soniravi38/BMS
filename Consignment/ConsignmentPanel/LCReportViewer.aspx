<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="LCReportViewer.aspx.cs" Inherits="Consignment.ConsignmentPanel.LCReportViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        <asp:Button ID="btnPrint" runat="server" OnClick="btnPrint_Click" CssClass="btn btn-primary" Text="Print" />
    </div>
</asp:Content>
