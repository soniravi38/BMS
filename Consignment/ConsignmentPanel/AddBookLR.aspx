<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddBookLR.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddBookLR" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>
        
            <script type="text/javascript">
                $(function () {
                    initdropdown();
                })

                function initdropdown() {

                    $('.searchable').select2();


                }
            </script>
            <script>
                Sys.Application.add_load(initdropdown);
            </script>
            <asp:HiddenField ID="hfbrid" runat="server" />
            <asp:HiddenField ID="HFid" runat="server" />

            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>
                                <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="Dashboard.aspx">Dashboard</a></li>
                                <li class="breadcrumb-item active">Add Book LR</li>
                            </ol>
                        </div>
                    </div>
                </div>
            </section>
            <section class="content">
                <div class="container-fluid">
                    <div class="box box-default">
                        <div class="box-body">
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Last Used LR No</label><span class="Star">*</span>
                                        <asp:Label ID="lblcongo" BackColor="LightGray" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignment Entry Mode</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddlMode" AutoPostBack="true" OnSelectedIndexChanged="ddlMode_SelectedIndexChanged" runat="server" CssClass="searchable form-control">
                                            <asp:ListItem Text="EConsignment" Value="E"></asp:ListItem>
                                            <asp:ListItem Text="Manual" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Multi-Consignment" Value="B"></asp:ListItem>
                                            <asp:ListItem Text="MB-Transport" Value="T"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignment Type</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddVType" runat="server" CssClass="searchable form-control">
                                            <asp:ListItem Text="CFA Delivery" Value="CFA Delivery"></asp:ListItem>
                                            <asp:ListItem Text="Direct Delivery" Value="Direct Delivery"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>


                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignment No.</label><span class="Star">*</span>
                                        <asp:TextBox ID="TxtLrNo" AutoPostBack="true" OnTextChanged="TxtLrNo_TextChanged" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignment Date</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtdate" TextMode="Date" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Source From Location</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddSourceFrom" Enabled="false" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Source To Location</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddSourceTo" Enabled="false" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Delivery Destination</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtDestination" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignor's Name</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddConsignor" OnSelectedIndexChanged="ddConsignor_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignor's Address</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblConsignorAddress" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignor's GSTIN</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblConsignorGST" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignor's State</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblConsignorState" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignee's Name</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddConsignee" OnSelectedIndexChanged="ddConsignee_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignee's Address</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblConsigneeAddress" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignee's GSTIN</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblConsigneeGst" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignee's State</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblConsigneeState" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No. Search</label><span class="Star">*</span>
                                        <asp:TextBox ID="TextBox1" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true" BackColor="#9dfda4" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddlVchNo" OnSelectedIndexChanged="ddlVchNo_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Size as per VRC</label><span class="Star">*</span>
                                        <asp:TextBox ID="lblVchSize" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Size as per Customer</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddlSize" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Name</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtDriverName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Mobile</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtDriverMobile" MaxLength="10" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Adhaar/License</label>
                                        <asp:TextBox ID="txtDriverAdhar" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Product Description</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtProduct" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>PKGS</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddlunit" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>QTY</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtQty" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Remark</label>
                                        <asp:TextBox ID="txtRemarks1" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Shipment No.</label>
                                        <asp:TextBox ID="txtShipNo" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>Invoice No.</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtinvno" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>Invoice Date</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtinvdate" TextMode="Date" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>Total Invoice Value</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">


                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>Document No.</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtDocNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>E-Way Bill No.</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtEwayBillNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>E-Way Bill Date</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtfromdate" TextMode="Date" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>LR Made By</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtLoading" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3" style="display: none">
                                    <div class="form-group">
                                        <label>Valid Upto Date</label><span class="Star">*</span>
                                        <asp:TextBox ID="txttodate" TextMode="Date" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>

                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Remarks</label>
                                        <asp:TextBox ID="txtRemarks" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>To Be Billed To</label><span class="Star">*</span>
                                        <asp:DropDownList ID="ddlBillto" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Seal No.</label>
                                        <asp:TextBox ID="txtsealno" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>LC No</label>
                                        <asp:TextBox ID="txtLCNo" TextMode="Number"  runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                            <div class="row">


                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:LinkButton runat="server" data-toggle="modal" data-target="#DocumentNo">Document No</asp:LinkButton>
                                        |
                                        <asp:LinkButton runat="server" data-toggle="modal" data-target="#InvoiceNo">Invoice No</asp:LinkButton>
                                        |
                                        <asp:LinkButton runat="server" data-toggle="modal" data-target="#EwayBill">E-Way Bill Details</asp:LinkButton>
                                    </div>
                                </div>
                            </div>
                            <div>


                                <asp:Button ID="btnSaveLocal" Visible="false" OnClick="btnSaveLocal_Click" CssClass="btn btn-custon-four btn-success" runat="server" Text="Save Entry" OnClientClick="return validationLocal();" />
                                <asp:Button ID="btnVerify" OnClick="btnVerify_Click" CssClass="btn btn-custon-four btn-default" runat="server" Text="Send OTP" OnClientClick="return validation();" />

                                <br />
                                <br />
                                <br />
                                <br />


                                <%--<div id="divsave" runat="server" style="position: fixed; top: 30%; left: 48%;z-index:999999999 !important">--%>
                                <div id="divsave" runat="server">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title">Enter OTP</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <asp:TextBox ID="txtCode" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox>

                                            </div>
                                            <div class="modal-footer">
                                                <asp:Button ID="btnAdd" CssClass="btn btn-custon-four btn-default" OnClick="btnAdd_Click" runat="server" Text="Save" OnClientClick="return validation();" />
                                                <asp:Button ID="btnCancel" CssClass="btn btn-custon-four btn btn-danger" runat="server" Text="Cancel" />
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="modal fade" id="DocumentNo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="exampleModalLabel">Document No.</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        1.
                                                    </div>
                                                    <div class="col-md-11">
                                                        <asp:TextBox ID="txtDocumentNo1" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        2.
                                                    </div>
                                                    <div class="col-md-11">
                                                        <asp:TextBox ID="txtDocumentNo2" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        3.
                                                    </div>
                                                    <div class="col-md-11">
                                                        <asp:TextBox ID="txtDocumentNo3" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        4.
                                                    </div>
                                                    <div class="col-md-11">
                                                        <asp:TextBox ID="txtDocumentNo4" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        5.
                                                    </div>
                                                    <div class="col-md-11">
                                                        <asp:TextBox ID="txtDocumentNo5" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Save</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="modal fade" id="EwayBill" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="exampleModalLabel1">Eway Bill Details</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <b>Eway Bill No</b>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <b>Eway Bill Date</b>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <b>Valid Upto Date</b>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        1.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtEwayBillNo1" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEwayBillDate1" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtValidUptoDate1" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        2.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtEwayBillNo2" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEwayBillDate2" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtValidUptoDate2" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        3.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtEwayBillNo3" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEwayBillDate3" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtValidUptoDate3" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        4.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtEwayBillNo4" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEwayBillDate4" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtValidUptoDate4" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        5.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtEwayBillNo5" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtEwayBillDate5" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="txtValidUptoDate5" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Save</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <div class="modal fade" id="InvoiceNo" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                    <div class="modal-dialog" role="document">
                                        <div class="modal-content">
                                            <div class="modal-header">
                                                <h5 class="modal-title" id="exampleModalLabel1">Invoice Details</h5>
                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                    <span aria-hidden="true">&times;</span>
                                                </button>
                                            </div>
                                            <div class="modal-body">
                                                <div class="row">
                                                    <div class="col-md-1">
                                                    </div>
                                                    <div class="col-md-3">
                                                        <b>Invoice No</b>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <b>Invoice Date</b>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <b>Invoice Value</b>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        1.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtInvoiceNo1" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceDate1" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceValue1" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        2.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtInvoiceNo2" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceDate2" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceValue2" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        3.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtInvoiceNo3" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceDate3" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceValue3" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        4.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtInvoiceNo4" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceDate4" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceValue4" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-1">
                                                        5.
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:TextBox ID="txtInvoiceNo5" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceDate5" TextMode="Date" autocomplete="off" CssClass="form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:TextBox ID="InvoiceValue5" autocomplete="off" CssClass="digit form-control" runat="server"></asp:TextBox><br />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="modal-footer">
                                                <button type="button" class="btn btn-secondary" data-dismiss="modal">Save</button>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                </div>
                </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnAdd" />
            <asp:PostBackTrigger ControlID="btnSaveLocal" />
            <asp:PostBackTrigger ControlID="btnVerify" />
            <asp:AsyncPostBackTrigger ControlID="ddConsignor" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddConsignee" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlVchNo" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="TxtLrNo" EventName="TextChanged" />
            <%--<asp:AsyncPostBackTrigger ControlID="txtLCNo" EventName="TextChanged" />--%>
            <asp:AsyncPostBackTrigger ControlID="txtdate" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" />
            <asp:AsyncPostBackTrigger ControlID="ddlMode" EventName="SelectedIndexChanged" />
        </Triggers>
    </asp:UpdatePanel>



    <script type="text/javascript">
        var ddlText, ddlValue, ddl, lblMesg;
        function CacheItems() {
            ddlText = new Array();
            ddlValue = new Array();
            ddl = document.getElementById("<%=ddlVchNo.ClientID %>");
            lblMesg = document.getElementById("<%=lblMessage.ClientID%>");
            for (var i = 0; i < ddl.options.length; i++) {
                ddlText[ddlText.length] = ddl.options[i].text;
                ddlValue[ddlValue.length] = ddl.options[i].value;
            }
        }
        window.onload = CacheItems;

        function FilterItems(value) {
            ddl.options.length = 0;
            for (var i = 0; i < ddlText.length; i++) {
                if (ddlText[i].toLowerCase().indexOf(value) != -1) {
                    AddItem(ddlText[i], ddlValue[i]);
                }
            }
            lblMesg.innerHTML = ddl.options.length + " item(s) found.";
            if (ddl.options.length == 0) {
                AddItem("No item found.", "");
            }
        }

        function AddItem(text, value) {
            var opt = document.createElement("option");
            opt.text = text;
            opt.value = value;
            ddl.options.add(opt);
        }
        $(document).ready(function () {
            $("#calandar").datetimepicker({
                minDate: -1,
                maxDate: +1
            })
        });

    </script>

    <script type="text/javascript">
        function validationLocal() {
            if (document.getElementById("<%=txtdate.ClientID%>").value == "") {
                document.getElementById("<%=txtdate.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtdate.ClientID%>").focus();
                alert("Please Input Date!");
                return false;
            }
            if (document.getElementById("<%=TxtLrNo.ClientID%>").value == "0") {
                document.getElementById("<%=TxtLrNo.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=TxtLrNo.ClientID%>").focus();
                alert("Please Contact To Admin for Consignment No!");
                return false;
            }
            if (document.getElementById("<%=ddSourceFrom.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceFrom.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceFrom.ClientID%>").focus();
                alert("Please Select Source Location!");
                return false;
            }
            if (document.getElementById("<%=ddSourceTo.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceTo.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceTo.ClientID%>").focus();
                alert("Please Select To Location!");
                return false;
            }
            if (document.getElementById("<%=ddConsignor.ClientID%>").value == "0") {
                document.getElementById("<%=ddConsignor.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddConsignor.ClientID%>").focus();
                alert("Please Select Consigner!");
                return false;
            }
            if (document.getElementById("<%=ddConsignee.ClientID%>").value == "0") {
                document.getElementById("<%=ddConsignee.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddConsignee.ClientID%>").focus();
                alert("Please Select Consigneee!");
                return false;
            }

            if (document.getElementById("<%=ddSourceFrom.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceFrom.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceFrom.ClientID%>").focus();
                alert("Please Select Source From!");
                return false;
            }
            if (document.getElementById("<%=ddSourceTo.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceTo.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceTo.ClientID%>").focus();
                alert("Please Select Source To!");
                return false;
            }


            if (document.getElementById("<%=txtDriverMobile.ClientID%>").value == "") {
                document.getElementById("<%=txtDriverMobile.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtDriverMobile.ClientID%>").focus();
                alert("Please Input Driver Mobile No!");
                return false;
            }

            if (document.getElementById("<%=ddlBillto.ClientID%>").value == "0") {
                document.getElementById("<%=ddlBillto.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddlBillto.ClientID%>").focus();
                alert("Please Select Billing Party!");
                return false;
            }

            if (document.getElementById("<%=txtProduct.ClientID%>").value == "") {
                document.getElementById("<%=txtProduct.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtProduct.ClientID%>").focus();
                alert("Please Input Product!");
                return false;
            }
            if (document.getElementById("<%=ddlunit.ClientID%>").value == "0") {
                document.getElementById("<%=ddlunit.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddlunit.ClientID%>").focus();
                alert("Please Select Unit!");
                return false;
            }

            if (document.getElementById("<%=txtQty.ClientID%>").value == "" || document.getElementById("<%=txtQty.ClientID%>").value == "0") {
                document.getElementById("<%=txtQty.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtQty.ClientID%>").focus();
                alert("Please Input Qty.");
                return false;
            }


            if (document.getElementById("<%=txtLoading.ClientID%>").value == "") {
                document.getElementById("<%=txtLoading.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtLoading.ClientID%>").focus();
                alert("Please Input Loading!");
                return false;
            }

            else {
                return true;
            }
        }

        function validation() {
            if (document.getElementById("<%=ddlMode.ClientID%>").value == "B" ||
                document.getElementById("<%=ddlMode.ClientID%>").value == "T") {
                return true;
            }

            if (document.getElementById("<%=TxtLrNo.ClientID%>").value == "0") {
                document.getElementById("<%=TxtLrNo.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=TxtLrNo.ClientID%>").focus();
                alert("Please Contact To Admin for Consignment No!");
                return false;
            }
            if (document.getElementById("<%=ddSourceFrom.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceFrom.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceFrom.ClientID%>").focus();
                alert("Please Select Source Location!");
                return false;
            }
            if (document.getElementById("<%=ddSourceTo.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceTo.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceTo.ClientID%>").focus();
                alert("Please Select To Location!");
                return false;
            }
            if (document.getElementById("<%=ddConsignor.ClientID%>").value == "0") {
                document.getElementById("<%=ddConsignor.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddConsignor.ClientID%>").focus();
                alert("Please Select Consigner!");
                return false;
            }
            if (document.getElementById("<%=ddConsignee.ClientID%>").value == "0") {
                document.getElementById("<%=ddConsignee.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddConsignee.ClientID%>").focus();
                alert("Please Select Consigneee!");
                return false;
            }

            if (document.getElementById("<%=ddSourceFrom.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceFrom.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceFrom.ClientID%>").focus();
                alert("Please Select Source From!");
                return false;
            }

            if (document.getElementById("<%=ddlBillto.ClientID%>").value == "0") {
                document.getElementById("<%=ddlBillto.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddlBillto.ClientID%>").focus();
                alert("Please Select Billing Party!");
                return false;
            }

            if (document.getElementById("<%=ddSourceTo.ClientID%>").value == "0") {
                document.getElementById("<%=ddSourceTo.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddSourceTo.ClientID%>").focus();
                alert("Please Select Source To!");
                return false;
            }


            if (document.getElementById("<%=txtDriverMobile.ClientID%>").value == "") {
                document.getElementById("<%=txtDriverMobile.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtDriverMobile.ClientID%>").focus();
                alert("Please Input Driver Mobile No!");
                return false;
            }

            if (document.getElementById("<%=txtProduct.ClientID%>").value == "") {
                document.getElementById("<%=txtProduct.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtProduct.ClientID%>").focus();
                alert("Please Input Product!");
                return false;
            }
            if (document.getElementById("<%=ddlunit.ClientID%>").value == "0") {
                document.getElementById("<%=ddlunit.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=ddlunit.ClientID%>").focus();
                alert("Please Select Unit!");
                return false;
            }

            if (document.getElementById("<%=txtQty.ClientID%>").value == "" || document.getElementById("<%=txtQty.ClientID%>").value == "0") {
                document.getElementById("<%=txtQty.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtQty.ClientID%>").focus();
                alert("Please Input Qty.");
                return false;
            }


            if (document.getElementById("<%=txtLoading.ClientID%>").value == "") {
                document.getElementById("<%=txtLoading.ClientID%>").style.backgroundColor = "red";
                document.getElementById("<%=txtLoading.ClientID%>").focus();
                alert("Please Input Loading!");
                return false;
            }

            else {
                return true;
            }
        }
    </script>
</asp:Content>





