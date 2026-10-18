<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master"
    AutoEventWireup="true" CodeBehind="AddVehicleDoc.aspx.cs"
    Inherits="Consignment.ConsignmentPanel.AddVehicleDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">

                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>

                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="VehicleDocManagement.aspx">Document List</a></li>
                        <li class="breadcrumb-item active">Add-Update Document</li>
                    </ol>
                </div>

            </div>
        </div>
    </section>

    <section class="content">

        <div class="container-fluid">

            <div class="box box-default">

                <div class="box-body">



                    <!-- Vehicle -->
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Vehicle</label><span class="Star">*</span>

                                <asp:DropDownList ID="ddlVehicle"
                                    runat="server"
                                    CssClass="form-control searchable">
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator
                                    ControlToValidate="ddlVehicle"
                                    InitialValue="0"
                                    ID="rfvVehicle"
                                    runat="server"
                                    ValidationGroup="a"
                                    Display="Dynamic"
                                    SetFocusOnError="True">

<div class="alert alert-danger">
Please Select Vehicle
</div>

                                </asp:RequiredFieldValidator>

                            </div>
                        </div>

                        <div class="col-md-6">

                            <div class="form-group">

                                <label>Registration Date</label><span class="Star">*</span>

                                <asp:TextBox ID="txtRegistrationDate"
                                    runat="server"
                                    TextMode="Date"
                                    CssClass="form-control">
                                </asp:TextBox>

                                <asp:RequiredFieldValidator
                                    ControlToValidate="txtRegistrationDate"
                                    ID="RequiredFieldValidator1"
                                    runat="server"
                                    ValidationGroup="a"
                                    Display="Dynamic"
                                    SetFocusOnError="True">

<div class="alert alert-danger">
Please Select txtRegistration Date
</div>

                                </asp:RequiredFieldValidator>

                            </div>

                        </div>
                    </div>
                    <!-- Document Type -->
                    <div class="row">
                        <div class="col-md-6">

                            <div class="form-group">

                                <label>Document Type</label><span class="Star">*</span>

                                <asp:DropDownList ID="ddlDocumentType"
                                    runat="server"
                                    CssClass="form-control">

                                    <asp:ListItem Text="--Select Document--" Value=""></asp:ListItem>
                                    <asp:ListItem Value="INSURANCE">INSURANCE</asp:ListItem>
                                    <asp:ListItem Value="MV TAX">MV TAX</asp:ListItem>
                                    <asp:ListItem Value="FITNESS">FITNESS</asp:ListItem>
                                    <asp:ListItem Value="GREEN TAX">GREEN TAX</asp:ListItem>
                                    <asp:ListItem Value="NP">NP</asp:ListItem>
                                    <asp:ListItem Value="NPGDC">NPGDC</asp:ListItem>
                                    <asp:ListItem Value="PGDC">PGDC</asp:ListItem>
                                    <asp:ListItem Value="KA CS PGDC">KA CS PGDC</asp:ListItem>
                                    <asp:ListItem Value="KA CS TAX">KA CS TAX</asp:ListItem>
                                    <asp:ListItem Value="TS CS PGDC">TS CS PGDC</asp:ListItem>
                                    <asp:ListItem Value="TS CS TAX">TS CS TAX</asp:ListItem>
                                    <asp:ListItem Value="MH CS PGDC">MH CS PGDC</asp:ListItem>
                                    <asp:ListItem Value="MH CS TAX">MH CS TAX</asp:ListItem>
                                    <asp:ListItem Value="AP CS PGDC">AP CS PGDC</asp:ListItem>
                                    <asp:ListItem Value="AP CS TAX">AP CS TAX</asp:ListItem>
                                    <asp:ListItem Value="TN CS PGDC">TN CS PGDC</asp:ListItem>
                                    <asp:ListItem Value="TN CS TAX">TN CS TAX</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator
                                    ControlToValidate="ddlDocumentType"
                                    InitialValue=""
                                    ID="rfvDocType"
                                    runat="server"
                                    ValidationGroup="a"
                                    Display="Dynamic"
                                    SetFocusOnError="True">

<div class="alert alert-danger">
Please Select Document Type
</div>

                                </asp:RequiredFieldValidator>

                            </div>

                        </div>






                        <!-- Expiry Date -->

                        <div class="col-md-6">

                            <div class="form-group">

                                <label>Expiry Date</label><span class="Star">*</span>

                                <asp:TextBox ID="txtExpiryDate"
                                    runat="server"
                                    TextMode="Date"
                                    CssClass="form-control">
                                </asp:TextBox>

                                <asp:RequiredFieldValidator
                                    ControlToValidate="txtExpiryDate"
                                    ID="rfvExpiry"
                                    runat="server"
                                    ValidationGroup="a"
                                    Display="Dynamic"
                                    SetFocusOnError="True">

<div class="alert alert-danger">
Please Select Expiry Date
</div>

                                </asp:RequiredFieldValidator>

                            </div>

                        </div>

                    </div>
                    <!-- Renewal Date -->
                    <div class="row">
                        <div class="col-md-6">

                            <div class="form-group">

                                <label>Renewal Date</label><span class="Star">*</span>

                                <asp:TextBox ID="txtRenewalDate"
                                    runat="server"
                                    TextMode="Date"
                                    CssClass="form-control">
                                </asp:TextBox>

                                <asp:RequiredFieldValidator
                                    ControlToValidate="txtRenewalDate"
                                    ID="rfvRenewal"
                                    runat="server"
                                    ValidationGroup="a"
                                    Display="Dynamic"
                                    SetFocusOnError="True">

<div class="alert alert-danger">
Please Select Renewal Date
</div>

                                </asp:RequiredFieldValidator>

                            </div>

                        </div>

                        <div class="col-md-6">

                            <div class="form-group">

                                <label>Insurance Policy No</label>

                                <asp:TextBox ID="txtPolicyNo"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>



                            </div>

                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">

                            <div class="form-group">

                                <label>Insurance Company No</label>

                                <asp:TextBox ID="txtCompany"
                                    runat="server"
                                    CssClass="form-control">
                                </asp:TextBox>



                            </div>
                        </div>
                    </div>
                    <!-- Buttons -->

                    <div>

                        <asp:Button
                            ID="btnSubmit"
                            runat="server"
                            Text="Add"
                            CssClass="btn btn-custon-four btn-success"
                            ValidationGroup="a"
                            OnClick="btnSubmit_Click" />

                        <asp:Button
                            ID="btnClear"
                            runat="server"
                            Text="Clear"
                            CssClass="btn btn-custon-four btn-default"
                            OnClick="btnClear_Click" />

                    </div>


                </div>

            </div>

        </div>

    </section>

</asp:Content>
