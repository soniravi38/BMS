<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="InternalLRReportViewer.aspx.cs" Inherits="Consignment.ConsignmentPanel.InternalLRReportViewer" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>Print Internal LR Documents</h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListInternalLR.aspx">Internal LR List</a></li>
                        <li class="breadcrumb-item active">Print</li>
                    </ol>
                </div>
            </div>
        </div>
    </section>
    <section class="content">
        <div class="container-fluid">
            <div class="box box-default">
                <div class="box-body">
                    <div style="margin-bottom: 10px; font-weight: bold">
                        <asp:Label ID="lblInfo" runat="server"></asp:Label>
                    </div>
                    <asp:Button ID="btnPrintAll" runat="server" OnClick="btnPrintAll_Click" CssClass="btn btn-primary" Text="Generate & Print All (Crossing Note + Voucher + Bill)" />
                    <asp:Button ID="btnPrintCN" runat="server" OnClick="btnPrintCN_Click" CssClass="btn btn-secondary" Text="Crossing Note" />
                    <asp:Button ID="btnPrintVoucher" runat="server" OnClick="btnPrintVoucher_Click" CssClass="btn btn-secondary" Text="Voucher(s)" />
                    <asp:Button ID="btnPrintBill" runat="server" OnClick="btnPrintBill_Click" CssClass="btn btn-secondary" Text="Bill" />
                    <a id="lnkEdit" runat="server" class="btn btn-default">Edit / Update Bill</a>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
