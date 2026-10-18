<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddVehicleSize.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddVehicleSize" %>
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
                        <li class="breadcrumb-item"><a href="ListVehicleSize.aspx">Vehicle Size List</a></li>
                        <li class="breadcrumb-item active">Add-Update Vehicle Size</li>
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
                                <label>Lenght</label><span class="Star">*</span>
                               <asp:TextBox ID="txtLenght" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLenght" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Lenght
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-md-6">
                            <div class="form-group">
                                <label>Height</label><span class="Star">*</span>
                               <asp:TextBox ID="txtHeight" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtHeight" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Height
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                      <div class="col-md-6">
                            <div class="form-group">
                                <label>Width</label><span class="Star">*</span>
                               <asp:TextBox ID="txtWidth" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtWidth" ID="RequiredFieldValidator3" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Width
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                 <div class="row">
                       <div class="col-md-6">
                            <div class="form-group">
                                <label>OwnMarket</label><span class="Star">*</span>
                               <asp:TextBox ID="txtOwnMarket" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtOwnMarket" ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter OwnMarket
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                      <div class="col-md-6">
                            <div class="form-group">
                                <label>CFT</label><span class="Star">*</span>
                               <asp:TextBox ID="txtCFT" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtCFT" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter CFT
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-md-6">
                            <div class="form-group">
                                <label>Code</label><span class="Star">*</span>
                               <asp:TextBox ID="txtCode" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtCode" ID="RequiredFieldValidator6" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Code
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                      <div class="col-md-6">
                            <div class="form-group">
                                <label>LastUpdated</label><span class="Star">*</span>
                               <asp:TextBox ID="txtLastUpdated" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLastUpdated" ID="RequiredFieldValidator7" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter LastUpdated
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       <div class="col-md-6">
                            <div class="form-group">
                                <label>Size</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlVehicleSize" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlVehicleSize" ID="RequiredFieldValidator8" runat="server" InitialValue="0"
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
                                <label>Remark</label>
                                  <asp:TextBox ID="txtRemark" runat="server" TextMode="MultiLine" autocomplete="off" CssClass="form-control"></asp:TextBox>
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





