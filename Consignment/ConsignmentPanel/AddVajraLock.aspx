<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddVajraLock.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddVajraLock" %>
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
                        <li class="breadcrumb-item"><a href="ListVajraLock.aspx">Vajra Lock List</a></li>
                        <li class="breadcrumb-item active">Add-Update Vajra Lock</li>
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
                                <label>Vehicle No</label><span class="Star">*</span>
                                <asp:TextBox ID="txtVehicleNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtVehicleNo" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle No
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Company</label><span class="Star">*</span>
                                 <asp:DropDownList ID="ddlCompany" runat="server" CssClass="searchable form-control">
                                    <asp:ListItem Value="">--Select Company--</asp:ListItem>
                                    <asp:ListItem Value="VRC">VRC</asp:ListItem>
                                    <asp:ListItem Value="URL">URL</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Company
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Vehicle Size</label>
                                <asp:DropDownList ID="ddlVehicleSize" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlVehicleSize" ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Vehicle Size
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Line/Local</label><span class="Star">*</span>
                                <asp:TextBox ID="txtLineLocal" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLineLocal" ID="RequiredFieldValidator6" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Line/Local
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Door Size</label><span class="Star">*</span>
                              <asp:TextBox ID="txtDoorSize" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDoorSize" ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Door Size
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                          <div class="col-md-6">
                            <div class="form-group">
                                <label>Lock Key No</label><span class="Star">*</span>
                              <asp:TextBox ID="txtLockKeyNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLockKeyNo" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Lock Key No
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>DriverName</label><span class="Star">*</span>
                              <asp:TextBox ID="txtDriverName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDriverName" ID="RequiredFieldValidator7" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Driver Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                          <div class="col-md-6">
                            <div class="form-group">
                                <label>Mobile No</label><span class="Star">*</span>
                              <asp:TextBox ID="txtMobileNo" MaxLength="10" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtMobileNo" ID="RequiredFieldValidator8" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Mobile No
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Date Of Installation</label><span class="Star">*</span>
                              <asp:TextBox ID="txtInstallationDate" TextMode="Date" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtInstallationDate" ID="RequiredFieldValidator9" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Installation Date
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Place</label><span class="Star">*</span>
                              <asp:TextBox ID="txtPlace" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPlace" ID="RequiredFieldValidator10" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Place
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>

                     <div class="row">
                        
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Plate Door Weld</label><span class="Star">*</span>
                              <asp:DropDownList ID="ddlPlateDoorWeld" runat="server" CssClass="searchable form-control">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="YES">YES</asp:ListItem>
                                    <asp:ListItem Value="NO">NO</asp:ListItem>
                                </asp:DropDownList>
                                  <asp:RequiredFieldValidator ControlToValidate="ddlPlateDoorWeld" ID="RequiredFieldValidator11" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Plate Door Weld
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





