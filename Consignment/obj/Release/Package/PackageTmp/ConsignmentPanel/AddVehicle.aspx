<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddVehicle.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddVehicle" %>
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
                        <li class="breadcrumb-item"><a href="ListVehicle.aspx">Vehicle List</a></li>
                        <li class="breadcrumb-item active">Add-Update Vehicle</li>
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
                                <label>Name</label><span class="Star">*</span>
                                <asp:TextBox ID="txtName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtName" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Vehicle No.</label><span class="Star">*</span>
                                <asp:TextBox ID="txtVehicleNo" runat="server" autocomplete="off" CssClass="nospace uppercasewithdigit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtVehicleNo" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle No
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                               <label>Mode</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="searchable form-control">
                                   
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ControlToValidate="ddlMode" ID="RequiredFieldValidator2" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Mode
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Size</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlSize" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlSize" ID="rfvProduct" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Size
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Owner Name</label><span class="Star">*</span>
                              <asp:TextBox ID="txtOwnerName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtOwnerName" ID="RequiredFieldValidator3" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Owner Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Chasis No</label>
                              <asp:TextBox ID="txtChasisNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                             <%--   <asp:RequiredFieldValidator ControlToValidate="txtChasisNo" ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Chasis No
                            </div>
                                </asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Engine No</label>
                              <asp:TextBox ID="txtEngineNo" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                             <%--   <asp:RequiredFieldValidator ControlToValidate="txtEngineNo" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Engine No
                            </div>
                                </asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Owner Address</label><span class="Star">*</span>
                              <asp:TextBox ID="txtOwnerAddress" TextMode="MultiLine" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtOwnerAddress" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Owner Address
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                         <div class="col-md-6">
                            <div class="form-group">
                                <label>Transporter</label>
                               <asp:DropDownList ID="ddlTransporter" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                           
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




