<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddUser.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddUser" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1><asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>
                <div class="col-sm-6" id="dvNavigation" runat="server">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListUser.aspx">User List</a></li>
                        <li class="breadcrumb-item active">Add-Update User</li>
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
                        <div class="col-md-6" id="dvName" runat="server">
                            <div class="form-group">
                                <label>Name</label><span class="Star">*</span>
                                <asp:TextBox ID="txtName" runat="server" autocomplete="off" txtName="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtName" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6" id="dvPicture" runat="server">
                            <div class="form-group">
                                <label>Picture</label>
                                   <asp:FileUpload ID="fuUpload" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-6" id="dvMobile" runat="server">
                            <div class="form-group">
                                <label>Mobile</label><span class="Star">*</span>
                                <asp:TextBox ID="txtMobile" MaxLength="10" runat="server" txtName="off" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtMobile" ID="RequiredFieldValidator1" runat="server"
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
                        <div class="col-md-6" id="dvEmail" runat="server">
                            <div class="form-group">
                                <label>Email</label><span class="Star">*</span>
                                <asp:TextBox ID="txtEmail" runat="server" txtName="off" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Email
                            </div>
                                </asp:RequiredFieldValidator>
                                  <asp:RegularExpressionValidator ControlToValidate="txtEmail" ID="revEmail1" runat="server" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Email
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-6" id="dvUserType" runat="server">
                            <div class="form-group">
                                <label>User Type</label><span class="Star">*</span>
                              <asp:DropDownList ID="ddlType" runat="server" CssClass="searchable form-control">
                                  <asp:ListItem Value="0">--Select--</asp:ListItem>
                                  <asp:ListItem Value="A">ADMIN</asp:ListItem>
                                  <asp:ListItem Value="U">USER</asp:ListItem>
                              </asp:DropDownList>
                                   <asp:RequiredFieldValidator ControlToValidate="ddlType" ID="rfvProduct" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select User Type
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                         <div class="col-md-6" id="dvBranch" runat="server">
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
                                <label>Username</label><span class="Star">*</span>
                                <asp:TextBox ID="txtUsername" runat="server" autocomplete="off" txtName="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtUsername" ID="RequiredFieldValidator3" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Username
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Password</label><span class="Star">*</span>
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" autocomplete="off" txtName="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPassword" ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Password
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



