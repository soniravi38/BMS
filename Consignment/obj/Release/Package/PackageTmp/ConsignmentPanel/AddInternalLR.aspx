<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddInternalLR.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddInternalLR" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .ilr-section-title { background: #212529; color: #fff; padding: 6px 12px; font-weight: bold; font-size: 15px; margin: 10px 0 12px 0; border-radius: 3px; }
        .ilr-note { font-size: 12px; color: #6c757d; }
        .ilr-total { font-size: 16px; font-weight: bold; color: #c0392b; }
        .ilr-status-D { background: #f39c12; color: #fff; padding: 3px 10px; border-radius: 3px; font-weight: bold; }
        .ilr-status-F { background: #28a745; color: #fff; padding: 3px 10px; border-radius: 3px; font-weight: bold; }
        .ilr-chain { background: #f4f6f9; border: 1px dashed #999; padding: 8px 12px; font-weight: bold; margin-bottom: 10px; }
        .ilr-readonly { background-color: #e9ecef !important; }
    </style>

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListInternalLR.aspx">Internal LR List</a></li>
                        <li class="breadcrumb-item active">Create / Update Internal LR</li>
                    </ol>
                </div>
            </div>
        </div>
    </section>

    <%-- ===================== STEP 1 : PARTNER LR SELECTION (Add mode) ===================== --%>
    <asp:Panel ID="pnlSelectLR" runat="server">
        <section class="content">
            <div class="container-fluid">
                <div class="box box-default">
                    <div class="box-body">
                        <div class="ilr-section-title">Step 1 : Select Original LR of
                            <asp:Label ID="lblPartnerCompany" runat="server"></asp:Label>
                            (vehicle of
                            <asp:Label ID="lblOwnCompany" runat="server"></asp:Label>)
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>LR Date From</label>
                                    <asp:TextBox ID="txtLRDateFrom" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>LR Date To</label>
                                    <asp:TextBox ID="txtLRDateTo" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>LR No</label>
                                    <asp:TextBox ID="txtSearchLRNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Vehicle No</label>
                                    <asp:TextBox ID="txtSearchVehicle" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <div>
                                        <asp:CheckBox ID="chkOwnVehicleOnly" runat="server" Checked="true" Text="&nbsp;Only our OWN vehicles" />
                                    </div>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <div>
                                        <asp:Button ID="btnSearchLR" OnClick="btnSearchLR_Click" CssClass="btn btn-danger" Text="Search LR" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ilr-note">
                            Only LRs for which an Internal LR has not yet been created are shown. "Only our OWN vehicles" shows LRs of vehicles marked as Mode = OWN in the Vehicle Master.
                            Total LR :
                            <asp:Label ID="lblLRCount" runat="server" Text="0"></asp:Label>
                        </div>
                        <div class="container-fluid" style="overflow-x: scroll; margin-top: 1%; max-height: 400px; overflow-y: auto">
                            <asp:GridView ID="gvPartnerLR" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False" DataKeyNames="Bookid"
                                OnRowCommand="gvPartnerLR_RowCommand" Width="100%" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                        <ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="LR No" DataField="BookLrNo" />
                                    <asp:BoundField HeaderText="LR Date" DataField="BookDate" />
                                    <asp:BoundField HeaderText="Vehicle No" DataField="VehicleNo" />
                                    <asp:BoundField HeaderText="Consignor" DataField="ConsignorName" />
                                    <asp:BoundField HeaderText="Consignee" DataField="ConsigneeName" />
                                    <asp:BoundField HeaderText="From" DataField="FromStation" />
                                    <asp:BoundField HeaderText="To" DataField="ToStation" />
                                    <asp:BoundField HeaderText="Pkgs" DataField="BookQty" />
                                    <asp:BoundField HeaderText="LC No" DataField="LCNo" />
                                    <asp:BoundField HeaderText="Branch" DataField="BranchName" />
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lbtnSelectLR" runat="server" CssClass="btn btn-sm btn-success" CommandName="SelectLR"
                                                CommandArgument='<%#Eval("Bookid") %>'>Create Internal LR</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="EmptyDataTemplate">No Pending LR Found !</div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="RowStyle" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" BackColor="#212529" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle CssClass="RowStyle" BackColor="#CCCCCC" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>

    <%-- ===================== STEP 2,3,4 : DETAILS ===================== --%>
    <asp:Panel ID="pnlDetails" runat="server" Visible="false">
        <asp:HiddenField ID="hfPartnerDb" runat="server" />
        <asp:HiddenField ID="hfPartnerCompany" runat="server" />
        <asp:HiddenField ID="hfPartnerLRId" runat="server" />
        <asp:HiddenField ID="hfLRConsignorName" runat="server" />
        <asp:HiddenField ID="hfConsigneeAddress" runat="server" />
        <asp:HiddenField ID="hfDestination" runat="server" />
        <asp:HiddenField ID="hfLRDocNo" runat="server" />
        <asp:HiddenField ID="hfLRShipNo" runat="server" />
        <asp:HiddenField ID="hfPartnerFreight" runat="server" Value="0" />
        <asp:HiddenField ID="hfWeight" runat="server" Value="Fixed" />
        <asp:HiddenField ID="hfBillStatus" runat="server" Value="D" />

        <section class="content">
            <div class="container-fluid">
                <div class="box box-default">
                    <div class="box-body">
                        <div class="ilr-chain">
                            <i class="fa fa-link"></i>&nbsp;
                            <asp:Label ID="lblChain" runat="server"></asp:Label>
                        </div>

                        <%-- ---------- A. Internal LR / Crossing Note ---------- --%>
                        <div class="ilr-section-title">Step 2 : Internal LR / Crossing Note (auto from original LR)</div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Crossing Note No</label>
                                    <asp:TextBox ID="txtCNNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly" Text="AUTO"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Crossing Note Date</label><span class="Star">*</span>
                                    <asp:TextBox ID="txtCNDate" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtCNDate" ID="rfvCNDate" runat="server"
                                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="a">
                                        <div class="alert alert-danger alert-mg-b" role="alert">Please Enter Crossing Note Date</div>
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Original LR No</label>
                                    <asp:TextBox ID="txtPartnerLRNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>LR Date</label>
                                    <asp:TextBox ID="txtPartnerLRDate" TextMode="Date" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Vehicle No</label>
                                    <asp:TextBox ID="txtVehicleNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>From</label>
                                    <asp:TextBox ID="txtFromStation" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>To</label>
                                    <asp:TextBox ID="txtToStation" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Packages</label>
                                    <asp:TextBox ID="txtPackages" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Consignor (on Crossing Note)</label><span class="Star">*</span>
                                    <asp:TextBox ID="txtConsignorName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtConsignorName" ID="rfvConsignorName" runat="server"
                                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="a">
                                        <div class="alert alert-danger alert-mg-b" role="alert">Please Enter Consignor</div>
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Consignee</label>
                                    <asp:TextBox ID="txtConsigneeName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Description (said to contain)</label>
                                    <asp:TextBox ID="txtProdName" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Unit</label>
                                    <asp:TextBox ID="txtUnitName" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>LR Invoice No(s)</label>
                                    <asp:TextBox ID="txtLRInvoiceNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>LR E-Way Bill No(s)</label>
                                    <asp:TextBox ID="txtLREwayBillNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>LC No</label>
                                    <asp:TextBox ID="txtLCNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>LR Billed To (Party)</label>
                                    <asp:TextBox ID="txtLRBillTo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Road Permit No</label>
                                    <asp:TextBox ID="txtRoadPermitNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-9">
                                <div class="form-group">
                                    <label>Crossing Note Remarks</label>
                                    <asp:TextBox ID="txtCNRemarks" runat="server" CssClass="form-control" autocomplete="off" MaxLength="500"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <%-- ---------- B. Vouchers ---------- --%>
                        <div class="ilr-section-title">Step 3 : Voucher(s) - Diesel / Cash Advance / Other (auto from existing data)</div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Trip Date From</label>
                                    <asp:TextBox ID="txtVchDateFrom" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Trip Date To</label>
                                    <asp:TextBox ID="txtVchDateTo" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <div>
                                        <asp:Button ID="btnFetchVoucher" OnClick="btnFetchVoucher_Click" CssClass="btn btn-primary" Text="Fetch Diesel / Cash" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="ilr-note">
                            Diesel: entries for the same vehicle with the same LR No. or within the trip dates. Cash: Cash Driver entries for the same vehicle within the trip dates.
                            Entries already linked to another Internal LR are not shown. Unticked entries are removed when the record is saved.
                        </div>
                        <div class="container-fluid" style="overflow-x: scroll; margin-top: 1%">
                            <asp:GridView ID="gvVoucher" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False" DataKeyNames="RowKey"
                                Width="100%" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both"
                                BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                <Columns>
                                    <asp:TemplateField HeaderText="Include" HeaderStyle-Width="60px">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkSelect" runat="server" Checked='<%#Convert.ToBoolean(Eval("Selected")) %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Voucher No" DataField="VoucherNo" />
                                    <asp:BoundField HeaderText="Type" DataField="VoucherType" />
                                    <asp:BoundField HeaderText="Date" DataField="VoucherDateDisp" />
                                    <asp:BoundField HeaderText="Amount" DataField="Amount" DataFormatString="{0:0.00}" />
                                    <asp:BoundField HeaderText="Through" DataField="Through" />
                                    <asp:BoundField HeaderText="Station" DataField="Station" />
                                    <asp:BoundField HeaderText="Remarks" DataField="Remarks" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <div class="EmptyDataTemplate">No Diesel / Cash entry found. Change the date range and click Fetch, or add an Other voucher.</div>
                                </EmptyDataTemplate>
                                <RowStyle CssClass="RowStyle" HorizontalAlign="Center" />
                                <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" BackColor="#212529" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle CssClass="RowStyle" BackColor="#CCCCCC" />
                            </asp:GridView>
                        </div>
                        <div class="row" style="margin-top: 10px">
                            <div class="col-md-12">
                                Selected Voucher Total : <span class="ilr-total">&#8377;
                                    <asp:Label ID="lblVoucherTotal" runat="server" Text="0.00"></asp:Label></span>
                                &nbsp;&nbsp;(Diesel :
                                <asp:Label ID="lblDieselTotal" runat="server" Text="0.00"></asp:Label>
                                + Cash :
                                <asp:Label ID="lblCashTotal" runat="server" Text="0.00"></asp:Label>
                                + Other :
                                <asp:Label ID="lblOtherTotal" runat="server" Text="0.00"></asp:Label>)
                            </div>
                        </div>
                        <div class="row" style="margin-top: 10px">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Other Expense Date</label>
                                    <asp:TextBox ID="txtOtherDate" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Amount</label>
                                    <asp:TextBox ID="txtOtherAmount" runat="server" CssClass="form-control decimal" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-5">
                                <div class="form-group">
                                    <label>Through / Description</label>
                                    <asp:TextBox ID="txtOtherThrough" runat="server" CssClass="form-control" autocomplete="off" MaxLength="300"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>&nbsp;</label>
                                    <div>
                                        <asp:Button ID="btnAddOther" OnClick="btnAddOther_Click" CssClass="btn btn-secondary" Text="Add Other Voucher" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>

                        <%-- ---------- C. Bill ---------- --%>
                        <div class="ilr-section-title">Step 4 : Bill to
                            <asp:Label ID="lblBillToCompany" runat="server"></asp:Label>
                            &nbsp;&nbsp;<asp:Label ID="lblBillStatus" runat="server"></asp:Label>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bill No</label>
                                    <asp:TextBox ID="txtBillNo" runat="server" ReadOnly="true" CssClass="form-control ilr-readonly" Text="AUTO"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bill Date</label><span class="Star">*</span>
                                    <asp:TextBox ID="txtBillDate" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtBillDate" ID="rfvBillDate" runat="server"
                                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="a">
                                        <div class="alert alert-danger alert-mg-b" role="alert">Please Enter Bill Date</div>
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Freight / Bill Amount</label>
                                    <asp:TextBox ID="txtBillAmount" runat="server" CssClass="form-control decimal" autocomplete="off" Text="0"></asp:TextBox>
                                    <span class="ilr-note">
                                        <asp:Label ID="lblFreightInfo" runat="server"></asp:Label></span>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Weight Kg/Tons</label>
                                    <asp:TextBox ID="txtBillWeight" runat="server" CssClass="form-control" autocomplete="off" Text="Fixed"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Rate Per Kg/Tons</label>
                                    <asp:TextBox ID="txtBillRate" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Invoice No (on Bill)</label>
                                    <asp:TextBox ID="txtBillInvoiceNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Doc No</label>
                                    <asp:TextBox ID="txtBillDocNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>P.O. No</label>
                                    <asp:TextBox ID="txtBillPONo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>eWay Bill No</label>
                                    <asp:TextBox ID="txtBillEwayBillNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bill To (M/s.)</label><span class="Star">*</span>
                                    <asp:TextBox ID="txtBillToName" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ControlToValidate="txtBillToName" ID="rfvBillToName" runat="server"
                                        Display="Dynamic" SetFocusOnError="True" ValidationGroup="a">
                                        <div class="alert alert-danger alert-mg-b" role="alert">Please Enter Bill To Name</div>
                                    </asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Bill To Address</label>
                                    <asp:TextBox ID="txtBillToAddress" TextMode="MultiLine" Rows="3" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Bill To GSTIN</label>
                                    <asp:TextBox ID="txtBillToGSTIN" runat="server" CssClass="form-control" autocomplete="off" MaxLength="20"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <label>Bill Remarks</label>
                                    <asp:TextBox ID="txtBillRemarks" runat="server" CssClass="form-control" autocomplete="off" MaxLength="500"></asp:TextBox>
                                </div>
                            </div>
                        </div>

                        <div style="margin-top: 10px">
                            <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="a" CssClass="btn btn-custon-four btn-success" runat="server" Text="Create Internal LR" />
                            <asp:Button ID="btnSaveAndPrint" OnClick="btnSaveAndPrint_Click" ValidationGroup="a" CssClass="btn btn-custon-four btn-primary" runat="server" Text="Save & Generate & Print All" />
                            <asp:Button ID="btnFinalizeBill" OnClick="btnFinalizeBill_Click" CssClass="btn btn-custon-four btn-warning" runat="server" Text="Generate Final Bill"
                                OnClientClick="return confirm('The Bill amount will be saved and locked after the Final Bill is generated. Do you want to continue?')" />
                            <asp:Button ID="btnReopenBill" OnClick="btnReopenBill_Click" CssClass="btn btn-custon-four btn-dark" runat="server" Text="Reopen Bill (Admin)"
                                OnClientClick="return confirm('Are you sure you want to move this Bill back to Draft?')" />
                            <a id="btnPrintAll" runat="server" class="btn btn-custon-four btn-info" style="color: white"><span class="fa fa-print"></span>&nbsp;Generate &amp; Print All</a>
                            <a href="ListInternalLR.aspx" class="btn btn-custon-four btn-default">Back</a>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </asp:Panel>

    <script type="text/javascript">
        // jQuery is loaded at the end of the master page, so plain JavaScript is used here
        document.addEventListener("keypress", function (e) {
            if (e.target && e.target.classList && e.target.classList.contains("decimal")) {
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (!/^[0-9.]+$/.test(str)) {
                    e.preventDefault();
                }
            }
        });
    </script>
</asp:Content>
