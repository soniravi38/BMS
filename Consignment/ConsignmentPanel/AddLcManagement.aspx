<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddLcManagement.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddLcManagement" %>
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
                        <li class="breadcrumb-item"><a href="ListLrManagement.aspx">LC Management List</a></li>
                        <li class="breadcrumb-item active">Add-Update LC Management</li>
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
                                <label>LC From</label><span class="Star">*</span>
                                <asp:TextBox ID="txtLcFrom" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLcFrom" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter LC From
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>LC To</label><span class="Star">*</span>
                                <asp:TextBox ID="txtLcTo" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLcTo" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter LC To
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>

                    </div>
                    <div class="row">
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
