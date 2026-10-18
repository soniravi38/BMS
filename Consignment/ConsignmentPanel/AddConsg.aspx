<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddConsg.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddConsg" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListConsg.aspx">Party List</a></li>
                        <li class="breadcrumb-item active">Add-Update Party</li>
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
                                <label>Consigor/Consignee Name</label><span class="Star">*</span>
                                <asp:TextBox ID="txtConsgName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtConsgName" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Consigor/Consignee Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address</label><span class="Star">*</span>
                                <asp:TextBox ID="txtAddress" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAddress" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Address
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>GSTIN</label><span class="Star">*</span>
                                <asp:TextBox ID="txtGSTIN" MaxLength="15" runat="server" autocomplete="off" CssClass="NoSpace uppercasewithdigit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtGSTIN" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter GSTIN
                            </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtGSTIN" ID="revGSTIN" runat="server" ValidationExpression="[0-9]{2}[(a-z)(A-Z)]{5}\d{4}[(a-z)(A-Z)]{1}\d{1}Z[(0-9)(a-z)(A-Z)]{1}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid GSTIN
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Mode</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="searchable form-control">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="A">Consignor</asp:ListItem>
                                    <asp:ListItem Value="S">Consignee</asp:ListItem>
                                    <asp:ListItem Value="B">Billing Party</asp:ListItem>
                                    <asp:ListItem Value="All">All of these</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlMode" ID="rfvProduct" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Mode
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>State</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlState" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlState" ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select State
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Source</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlSource" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlSource" ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Source
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Consignment Type</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlType" runat="server" CssClass="searchable form-control">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Billed">Billed</asp:ListItem>
                                    <asp:ListItem Value="Topay">Topay</asp:ListItem>
                                    <asp:ListItem Value="Paid">Paid</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlType" ID="RequiredFieldValidator5" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Consignment Type
                            </div>
                                </asp:RequiredFieldValidator>
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



