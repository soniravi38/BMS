<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddPODBulk.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddPODBulk" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>

                        <a href="../App_Content/SampleCSV/POD.csv">
                            <img width="30" title="Download Sample CSV"
                                 src="https://static-00.iconduck.com/assets.00/csv-icon-448x512-rkoi7crs.png" />
                        </a>
                    </h1>
                </div>

                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListPOD.aspx">POD List</a></li>
                        <li class="breadcrumb-item active">Add POD Bulk</li>
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
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Upload CSV File</label><span class="Star">*</span>

                                <asp:FileUpload ID="fuUpload" runat="server" CssClass="form-control" />

                                <!-- CSV VALIDATION -->
                                <asp:RegularExpressionValidator ID="revFile"
                                    runat="server"
                                    ControlToValidate="fuUpload"
                                    ValidationExpression="^.*\.(csv)$"
                                    ErrorMessage="Only CSV file allowed"
                                    ValidationGroup="a"
                                    Display="Dynamic">

                                    <div class="alert alert-danger">Only CSV file allowed</div>
                                </asp:RegularExpressionValidator>

                                <!-- REQUIRED VALIDATION -->
                                <asp:RequiredFieldValidator ID="rfvFile"
                                    runat="server"
                                    ControlToValidate="fuUpload"
                                    ErrorMessage="Please upload CSV file"
                                    ValidationGroup="a"
                                    Display="Dynamic">

                                    <div class="alert alert-danger">Please upload CSV file</div>
                                </asp:RequiredFieldValidator>

                            </div>
                        </div>
                    </div>

                    <div>
                        <asp:Button ID="btnSubmit"
                            runat="server"
                            Text="Upload POD"
                            CssClass="btn btn-success"
                            ValidationGroup="a"
                            OnClick="btnSubmit_Click" />
                    </div>

                </div>
            </div>
        </div>
    </section>

    <!-- INSTRUCTIONS -->
    <br />
    <div style="padding:10px; background:#f2dede; border-radius:5px; width:75%;">
        <h6>Important Instructions:</h6>

        1. Only CSV file is allowed.<br />
        2. Do not use comma (,) inside data.<br />
        3. Date format must be DD-MM-YYYY.<br />
        4. Follow sample CSV format strictly.<br />
        5. Duplicate LR No will be skipped.<br />
    </div>

</asp:Content>