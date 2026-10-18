<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddBookLC.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddBookLC" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <asp:ScriptManager runat="server"></asp:ScriptManager>
    <asp:UpdatePanel ID="Update1" runat="server">
        <ContentTemplate>

            <section class="content-header">
                <div class="container-fluid">
                    <div class="row mb-2">
                        <div class="col-sm-6">
                            <h1>
                                <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                        </div>
                        <div class="col-sm-6">
                            <ol class="breadcrumb float-sm-right">
                                <li class="breadcrumb-item"><a href="ListDriver.aspx">Lorry Challan List</a></li>
                                <li class="breadcrumb-item active">Add-Update Lorry Challan</li>
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
                                        <label>Vehicle Hire No</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtVehicleHirNo" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtVehicleHirNo" ID="RequiredFieldValidator2" runat="server"
                                            Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                            ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle Hire No
                            </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>LC Date</label>
                                        <asp:TextBox ID="txtdate" TextMode="Date" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtdate" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter LC Date
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Source From Location</label>
                                        <asp:DropDownList ID="ddSourceFrom" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="ddSourceFrom" ID="rfvProduct" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Source From Location
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Source To Location</label>
                                        <asp:DropDownList ID="ddSourceTo" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="ddSourceTo" ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Source To Location
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No. Search</label>
                                        <asp:TextBox ID="TextBox1" OnTextChanged="TextBox1_TextChanged" AutoPostBack="true" BackColor="#9dfda4" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle No</label>
                                        <asp:DropDownList ID="ddlVchNo" OnSelectedIndexChanged="ddlVchNo_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>

                                        <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="ddlVchNo" ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Vehicle
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Owner Name</label>
                                        <asp:TextBox ID="txtVehicleOwnerName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--   <asp:RequiredFieldValidator ControlToValidate="txtVehicleOwnerName" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle Owner Name
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Owner Address</label>
                                        <asp:TextBox ID="txtVehicleOwnerAddress" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtVehicleOwnerAddress" ID="RequiredFieldValidator6" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle Owner Address
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Chasis No</label>
                                        <asp:TextBox ID="txtChasisNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtChasisNo" ID="RequiredFieldValidator7" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Chasis No
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Engine No</label>
                                        <asp:TextBox ID="txtEngine" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--   <asp:RequiredFieldValidator ControlToValidate="txtEngine" ID="RequiredFieldValidator8" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Engine No
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Vehicle Size</label>
                                        <asp:TextBox ID="txtVehicleSize" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtVehicleSize" ID="RequiredFieldValidator9" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle Size
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Consignment No</label><span class="Star">*</span>
                                        <asp:TextBox ID="txtConsignmentNo" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtConsignmentNo" ID="RequiredFieldValidator10" runat="server"
                                            Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                            ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Consignment No
                            </div>
                                        </asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Destination</label>
                                        <asp:TextBox ID="txtDestination" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtDestination" ID="RequiredFieldValidator11" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Destination
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Unit</label>
                                        <asp:DropDownList ID="ddlunit" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="ddlunit" ID="RequiredFieldValidator12" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Unit
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Qty</label>
                                        <asp:TextBox ID="txtQty" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtQty" ID="RequiredFieldValidator13" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Quantity
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Weight</label>
                                        <asp:TextBox ID="txtWeight" Text="Fixed" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtWeight" ID="RequiredFieldValidator15" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Weight
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver</label>
                                        <asp:TextBox ID="txtDriver" Text="Fixed" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="ddlDriver" ID="RequiredFieldValidator14" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Driver
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Adhar/License</label>
                                        <asp:TextBox ID="txtDriverLicenseNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtDriverLicenseNo" ID="RequiredFieldValidator16" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter License NO
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Driver Mobile</label>
                                        <asp:TextBox ID="txtDriverMobile" runat="server" autocomplete="off" MaxLength="10" CssClass="form-control digit"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtDriverMobile" ID="RequiredFieldValidator17" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Driver's Mobile
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Transporter</label>
                                        <asp:DropDownList ID="ddlTransporter" runat="server" CssClass="searchable form-control">
                                        </asp:DropDownList>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="ddlTransporter" ID="RequiredFieldValidator18" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Transporter
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Freight</label>
                                        <asp:TextBox ID="txtFreight" runat="server" Text="0" autocomplete="off" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtFreight" ID="RequiredFieldValidator19" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Freight
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Hamali</label>
                                        <asp:TextBox ID="txtHamali" runat="server" Text="0" autocomplete="off" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtHamali" ID="RequiredFieldValidator20" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Hamali
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Detention</label>
                                        <asp:TextBox ID="txtDetention" runat="server" Text="0" autocomplete="off" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%--   <asp:RequiredFieldValidator ControlToValidate="txtDetention" ID="RequiredFieldValidator21" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Detention
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Other Charges</label>
                                        <asp:TextBox ID="txtOtherCharges" runat="server" Text="0" autocomplete="off" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtOtherCharges" ID="RequiredFieldValidator22" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Other Charges
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Delay Charges</label>
                                        <asp:TextBox ID="txtDelayCharges" runat="server" Text="0" autocomplete="off" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtOtherDeduction" ID="RequiredFieldValidator23" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Other Deduction
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Other Deduction</label>
                                        <asp:TextBox ID="txtOtherDeduction" runat="server" Text="0" autocomplete="off" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtOtherDeduction" ID="RequiredFieldValidator23" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Other Deduction
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>TDS</label>
                                        <asp:TextBox ID="txtTDS" runat="server" autocomplete="off" Text="0" CssClass="form-control digit SumCharges"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtTDS" ID="RequiredFieldValidator24" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter TDS
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Total</label>
                                        <asp:TextBox ID="txtTotal" runat="server" autocomplete="off" Text="0" CssClass="form-control txtTotal digit"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtTotal" ID="RequiredFieldValidator25" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Total
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>

                            </div>

                            <div class="row">
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Advance</label>
                                        <asp:TextBox ID="txtAdvance" runat="server" autocomplete="off" Text="0" CssClass="form-control digit LessAdv"></asp:TextBox>
                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtAdvance" ID="RequiredFieldValidator26" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Advance
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                                <div class="col-md-3">
                                    <div class="form-group">
                                        <label>Balance</label>
                                        <asp:TextBox ID="txtBalance" runat="server" Text="0" autocomplete="off" CssClass="form-control txtBalance digit"></asp:TextBox>
                                        <%-- <asp:RequiredFieldValidator ControlToValidate="txtBalance" ID="RequiredFieldValidator27" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Balance
                            </div>
                                </asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" ValidationGroup="a" CssClass="btn btn-custon-four btn-success" runat="server" Text="Add" />
                                <asp:Button ID="btnClear" OnClick="btnClear_Click" CssClass="btn btn-custon-four btn-default" runat="server" Text="Clear" />
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </ContentTemplate>
        <Triggers>

            <asp:PostBackTrigger ControlID="btnClear" />
            <asp:PostBackTrigger ControlID="btnSubmit" />
            <asp:AsyncPostBackTrigger ControlID="ddlVchNo" EventName="SelectedIndexChanged" />
            <asp:AsyncPostBackTrigger ControlID="TextBox1" EventName="TextChanged" />

        </Triggers>
    </asp:UpdatePanel>
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script type="text/javascript">
        $(function () {
            initdropdown();
        })

        function initdropdown() {

            $('.searchable').select2();


        }
    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            // Event for SumCharges field
            $(document).on("keyup", ".SumCharges", function () {
                SumAmount();
            });

            // Event for LessAdv field
            $(document).on("keyup", ".LessAdv", function () {
                SumAmount();
            });

            // Corrected event for digit validation on keypress
            $(document).on("keypress", ".digit", function (e) {
                var regex = new RegExp("^[0-9]+$");
                var str = String.fromCharCode(!e.charCode ? e.which : e.charCode);
                if (!regex.test(str)) {
                    return false;  // Prevent the input if not a digit
                }
            });

            // Function to calculate total and balance
            function SumAmount() {
                var sum = 0;
                var Adv = 0;

                // Sum up the values in SumCharges fields
                $('.SumCharges').each(function () {
                    sum += parseFloat(this.value) || 0;  // Handle NaN cases
                });

                // Sum up the values in LessAdv fields
                $('.LessAdv').each(function () {
                    Adv += parseFloat(this.value) || 0;  // Handle NaN cases
                });

                // Update the total and balance
                $(".txtTotal").val(sum);
                $(".txtBalance").val(sum - Adv);
            }
        });
</script>
</asp:Content>

