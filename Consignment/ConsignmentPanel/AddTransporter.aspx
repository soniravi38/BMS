<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddTransporter.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddTransporter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1><asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListDriver.aspx">Transporter List</a></li>
                        <li class="breadcrumb-item active">Add-Update Transporter</li>
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
                                <label>Transporter Name</label><span class="Star">*</span>
                                <asp:TextBox ID="txtTransporterrName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtTransporterrName" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Transporter Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="col-md-6">
                            <div class="form-group">
                                <label>PAN</label><span class="Star">*</span>
                                <asp:TextBox ID="txtPAN"  MaxLength="10" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPAN" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter PAN
                            </div>
                                </asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ControlToValidate="txtPAN" ID="revPAN" runat="server" ValidationExpression="[A-Z]{5}\d{4}[A-Z]{1}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid PAN
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address </label><span class="Star">*</span>
                                <asp:TextBox ID="txtAddress" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Address
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                       <div class="col-md-6">
                            <div class="form-group">
                                <label>Mobile</label><span class="Star">*</span>
                                <asp:TextBox ID="txtMobile" runat="server" MaxLength="10" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtMobile" ID="RequiredFieldValidator3" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Mobile
                            </div>
                                </asp:RequiredFieldValidator>
                                 <asp:RegularExpressionValidator ControlToValidate="txtMobile" ID="revMobile1" runat="server" ValidationExpression="[0-9]{10}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Mobile Number
                            </div>
                                </asp:RegularExpressionValidator>
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
</asp:Content>
