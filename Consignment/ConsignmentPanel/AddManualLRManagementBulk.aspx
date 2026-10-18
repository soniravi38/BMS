<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="AddManualLRManagementBulk.aspx.cs" Inherits="Consignment.ConsignmentPanel.AddManualLRManagementBulk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>
                        <a href="../App_Content/SampleCSV/ManualLR.csv"><img id="imgBtnExcel" title="Required CSV Sample Format" Width="30" src="https://static-00.iconduck.com/assets.00/csv-icon-448x512-rkoi7crs.png" /></a>
                    </h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListManualLRManagement.aspx">Manual LR Management List</a></li>
                        <li class="breadcrumb-item active">Add Manual LR in Bulk</li>
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
                                <label>File Upload</label><span class="Star">*</span>
                                <asp:FileUpload ID="fuUpload" runat="server" CssClass="form-control" />
                                <asp:RegularExpressionValidator ID="revfuUpload"  
                     runat="server" ControlToValidate="fuUpload"   
                     ErrorMessage="Only CSV format is allowed."  Display="Dynamic" 
                     ValidationExpression="^.*\.(csv)$"  
                     ValidationGroup="a" SetFocusOnError="true">
                                    <div class="alert alert-danger alert-mg-b" role="alert">
                                Only CSV format is allowed.
                            </div>
                                </asp:RegularExpressionValidator>  
                                   <asp:RequiredFieldValidator ControlToValidate="fuUpload" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Upload CSV File
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

   <br />
    <div class="row mb-2" style="padding: 10px; background: #f2dede; border-radius: 5px; font-size: 9pt;width:74%;margin-left: 1.5%;">
        <h6>Points to be noted for uploading :</h6>
        1. Only CSV File is Valid.<br />
        2. There should not be any comma (,) or apostrophe (') anywhere in the excel file.<br />
        3. You can see the format in which you want to upload CSV by downloading it from the above CSV icon.<br />
        4. Wherever is a date column in the file, that should come in DD-MM-YYYY format only otherwise file will not upload.<br />
        5. If the data of any row is already saved, then that will be ignored in the uploading process and will not be updated.<br />
        6. If you want to update the already existing data, then either follow the edit process one by one OR delete the required data and then upload the file again.<br />
    </div>
</asp:Content>







