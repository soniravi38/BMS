<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddManualLRManagement.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddManualLRManagement" %>
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
                        <li class="breadcrumb-item"><a href="ListManualLRManagement.aspx">Manual LR Management List</a></li>
                        <li class="breadcrumb-item active">Add-Update Manual LR Management</li>
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
                                <label>LR No</label><span class="Star">*</span>
                                <asp:TextBox ID="txtLrNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLrNo" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter LR No.
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Branch</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlBranch" runat="server" CssClass="searchable form-control"></asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlBranch" ID="RequiredFieldValidator5" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Branch
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Remark</label><span class="Star">*</span>
                                <asp:TextBox ID="txtRemark" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtRemark" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Remark
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






