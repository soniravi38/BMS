<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="CompanyMaster.aspx.cs" Inherits="Consignment.ConsignmentPanel.CompanyMaster" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1><asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Company</li>
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
                                <label>Company Name</label><span class="Star">*</span>
                                <asp:TextBox ID="txtCompanyName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtCompanyName" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Company Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Owner Name</label>
                                <asp:TextBox ID="txtOwnerName" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtOwnerName" ID="rfvOwnerName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Owner Name
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Company Logo</label>
                                <asp:FileUpload ID="fuCompanyLogo" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Company Short Logo</label>
                                <asp:FileUpload ID="fuCompanyShortLogo" runat="server" CssClass="form-control" />
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>GSTIN</label><span class="Star">*</span>
                                <asp:TextBox ID="txtGSTIN" MaxLength="15" runat="server" autocomplete="off" CssClass="NoSpace uppercasewithdigit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtGSTIN" ID="rfvGSTIN" runat="server"
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
                            <div class="form-group"><span class="Star">*</span>
                                <label>PAN </label>
                                <asp:TextBox ID="txtPAN" MaxLength="10" runat="server" autocomplete="off" CssClass="NoSpace uppercasewithdigit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPAN" ID="rfvPAN" runat="server"
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
                                <label>CIN</label>
                                <asp:TextBox ID="txtCIN" runat="server" autocomplete="off" CssClass="NoSpace form-control"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator ControlToValidate="txtCIN" ID="RequiredFieldValidator2" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter CIN
                            </div>
                                </asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>FSSAI </label>
                                <asp:TextBox ID="txtFSSAI" runat="server" autocomplete="off" CssClass="NoSpace  form-control"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator ControlToValidate="txtFSSAI" ID="RequiredFieldValidator3" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter FSSAI
                            </div>
                                </asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Phone 1</label><span class="Star">*</span>
                                <asp:TextBox ID="txtPhone1" MaxLength="11" runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPhone1" ID="rfvPhone1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Phone
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Phone 2 </label>
                                <asp:TextBox ID="txtPhone2" MaxLength="11"  runat="server" autocomplete="off" CssClass="form-control digit"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtMobile2" ID="RegularExpressionValidator2" runat="server" ValidationExpression="[0-9]{10}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Mobile Number
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Mobile 1</label><span class="Star">*</span>
                                <asp:TextBox ID="txtMobile1" MaxLength="10" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtMobile1" ID="rfvMobile1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Mobile1
                            </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtMobile1" ID="revMobile1" runat="server" ValidationExpression="[0-9]{10}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Mobile Number
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Mobile 2 </label>
                                <asp:TextBox ID="txtMobile2" MaxLength="10" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtMobile2" ID="revMobile2" runat="server" ValidationExpression="[0-9]{10}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Mobile Number
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>E-mail 1</label><span class="Star">*</span>
                                <asp:TextBox ID="txtEmail1" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtEmail1" ID="rfvEmail1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Email1
                            </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtEmail1" ID="revEmail1" runat="server" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Email
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>E-mail 2 </label>
                                <asp:TextBox ID="txtEmail2" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator ControlToValidate="txtEmail2" ID="revEmail2" runat="server" ValidationExpression="^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Email
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Website</label><span class="Star">*</span>
                                <asp:TextBox ID="txtWebsite" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtWebsite" ID="rfvWebsite" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Website
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address Line 1 </label><span class="Star">*</span>
                                <asp:TextBox ID="txtAddress1" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAddress1" ID="rfvAddress" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Address 1
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Address Line 2 </label><span class="Star">*</span>
                                <asp:TextBox ID="txtAddress2" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtAddress2" ID="RequiredFieldValidator5" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Address 2
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Fax </label><span class="Star">*</span>
                                <asp:TextBox ID="txtFax" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtFax" ID="RequiredFieldValidator6" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Fax
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Google Map API</label><span class="Star">*</span>
                                <asp:TextBox ID="txtGoogleMapAPI" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtGoogleMapAPI" ID="rfvGoogleMapApi" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Google Map API
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Whatsapp Mobile Number </label><span class="Star">*</span>
                                <asp:TextBox ID="txtWhatsappMobile" MaxLength="10" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtWhatsappMobile" ID="rfvWhatsappMobile" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Whatsapp Mobile Number
                            </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtWhatsappMobile" ID="revWhatsappMobile" runat="server" ValidationExpression="[0-9]{10}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Mobile Number
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Mobile Number to Receive SMS</label><span class="Star">*</span>
                                <asp:TextBox ID="txtSMSMobile" MaxLength="10" runat="server" autocomplete="off" CssClass="digit form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtSMSMobile" ID="rfvSMSMobile" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Mobile to Receive SMS
                            </div>
                                </asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator ControlToValidate="txtSMSMobile" ID="revSMSMobile" runat="server" ValidationExpression="[0-9]{10}"
                                    Display="Dynamic" ValidationGroup="a" ErrorMessage="InValid PAN Card" SetFocusOnError="True">
                                <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Valid Mobile Number
                            </div>
                                </asp:RegularExpressionValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>SMS API </label><span class="Star">*</span>
                                <asp:TextBox ID="txtSMSApi" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtSMSApi" ID="rfvSMSApi" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter SMS API
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Facebook</label><span class="Star">*</span>
                                <asp:TextBox ID="txtFacebook" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtFacebook" ID="rfvFacebook" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Facebook
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Instagram </label><span class="Star">*</span>
                                <asp:TextBox ID="txtInstagram" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtInstagram" ID="rfvInstagram" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Instagram
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Twitter</label><span class="Star">*</span>
                                <asp:TextBox ID="txtTwitter" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtTwitter" ID="rfvTwitter" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Twitter
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Google Plus </label><span class="Star">*</span>
                                <asp:TextBox ID="txtGooglePlus" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtGooglePlus" ID="rfvGooglePlus" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Google Plus
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Linkdin</label><span class="Star">*</span>
                                <asp:TextBox ID="txtLinkdin" runat="server" autocomplete="off" CssClass="nospace form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtLinkdin" ID="rfvLinkdin" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Linkdin
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group">
                                <label>Footer Text </label><span class="Star">*</span>
                                <asp:TextBox ID="txtFooter" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtFooter" ID="rfvFooter" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Footer
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="form-group"><span class="Star">*</span>
                                <label>SMS Service Enabled</label>
                                <asp:DropDownList ID="ddlSMSServiceEnabled" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                </asp:DropDownList>
                                 <asp:RequiredFieldValidator ControlToValidate="ddlSMSServiceEnabled" ID="RequiredFieldValidator1" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Service Enabled YES/NO
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <div class="form-group"><span class="Star">*</span>
                                <label>is Site Under Maintenance</label>
                                <asp:DropDownList ID="ddlUnderMaintenance" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">--Select--</asp:ListItem>
                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                    <asp:ListItem Value="1">YES</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlUnderMaintenance" ID="rfvProduct" runat="server" InitialValue=""
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Maintenance YES/NO
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                     <div class="row">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Image String for Report</label><span class="Star">*</span>
                                <asp:TextBox ID="txtImageStr" TextMode="MultiLine" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtImageStr" ID="RequiredFieldValidator4" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Image String
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

