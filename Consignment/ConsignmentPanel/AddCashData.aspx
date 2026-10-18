<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddCashData.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddCashData" %>
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
                        <li class="breadcrumb-item"><a href="ListCashData.aspx">Cash Data List</a></li>
                        <li class="breadcrumb-item active">Add-Update Cash Data</li>
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
                                <label>Date</label><span class="Star">*</span>
                                <asp:TextBox ID="txtDate" runat="server" TextMode="Date" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDate" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Date
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Vehicle No</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlVehicle" runat="server" CssClass="form-control searchable"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ControlToValidate="ddlVehicle" ID="RequiredFieldValidator1" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Vehicle No.
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Driver</label>
                                 <asp:TextBox ID="txtDriver" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtDriver" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Driver
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>From</label><span class="Star">*</span>
                                  <asp:DropDownList ID="ddlFrom" runat="server" CssClass="form-control searchable"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ControlToValidate="ddlFrom" ID="RequiredFieldValidator3" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select From
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>To</label><span class="Star">*</span>
                             <asp:DropDownList ID="ddlTo" runat="server" CssClass="form-control searchable"></asp:DropDownList>
                                  <asp:RequiredFieldValidator ControlToValidate="ddlTo" ID="RequiredFieldValidator4" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select To
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                          <div class="col-md-6">
                            <div class="form-group">
                                <label>Amount</label><span class="Star">*</span>
                              <asp:TextBox ID="txtAmount" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAmount" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Amount
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                       
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Purpose</label><span class="Star">*</span>
                              <asp:TextBox ID="txtPurpose" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPurpose" ID="RequiredFieldValidator7" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Purpose
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







