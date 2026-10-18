<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master"
AutoEventWireup="true" CodeBehind="VehicleDocManagement.aspx.cs"
Inherits="Consignment.ConsignmentPanel.VehicleDocManagement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<section class="content-header">
<div class="container-fluid">
<h1>Vehicle Document Management Dashboard
      <asp:ImageButton ID="imgBtnExcel" OnClick="imgBtnExcel_Click" ToolTip="Excel" Width="20" ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Microsoft_Office_Excel_%282019%E2%80%93present%29.svg/826px-Microsoft_Office_Excel_%282019%E2%80%93present%29.svg.png" runat="server" />
  |
  <asp:ImageButton ID="imgBtnPdf" OnClick="imgBtnPdf_Click" ToolTip="PDF" Width="20" ImageUrl="https://cdn4.iconfinder.com/data/icons/file-extensions-1/64/pdfs-512.png" runat="server" />

</h1>
</div>
</section>

<section class="content">
<div class="container-fluid">
   

    <div class="row mt-3 text-center">

<div class="col-md-2">
<div class="small-box bg-primary">
<div class="inner">
<h4><asp:Label ID="lblTotalVehicles" runat="server"></asp:Label></h4>
<p>Total Vehicles</p>
</div>
</div>
</div>

<div class="col-md-2">
<div class="small-box bg-secondary">
<div class="inner">
<h4><asp:Label ID="lblPendingApproval" runat="server"></asp:Label></h4>
<p>Pending Approvals</p>
</div>
</div>
</div>

<div class="col-md-2">
<div class="small-box bg-danger">
<div class="inner">
<h4><asp:Label ID="lblExpiredDocs" runat="server"></asp:Label></h4>
<p>Expired</p>
</div>
</div>
</div>

<div class="col-md-2">
<div class="small-box bg-warning">
<div class="inner">
<h4><asp:Label ID="lblExpire7" runat="server"></asp:Label></h4>
<p>Expire in 7 Days</p>
</div>
</div>
</div>

<div class="col-md-2">
<div class="small-box bg-info">
<div class="inner">
<h4><asp:Label ID="lblExpire15" runat="server"></asp:Label></h4>
<p>Expire in 15 Days</p>
</div>
</div>
</div>

<div class="col-md-2">
<div class="small-box bg-success">
<div class="inner">
<h4><asp:Label ID="lblExpire30" runat="server"></asp:Label></h4>
<p>Expire in 30 Days</p>
</div>
</div>
</div>

</div>
<!-- FILTERS -->

<div class="card">
<div class="card-body">

<div class="row">

<div class="col-md-3">
<label>Vehicle</label>
<asp:DropDownList ID="ddlVehicle" runat="server" CssClass="form-control searchable"></asp:DropDownList>
</div>

<div class="col-md-3">
<label>Document Type</label>
<asp:DropDownList ID="ddlDocumentType" runat="server" CssClass="form-control">
<asp:ListItem Text="--Select Document--" Value=""></asp:ListItem>
<asp:ListItem>INSURANCE</asp:ListItem>
<asp:ListItem>MV TAX</asp:ListItem>
<asp:ListItem>FITNESS</asp:ListItem>
<asp:ListItem>GREEN TAX</asp:ListItem>
<asp:ListItem>NP</asp:ListItem>
<asp:ListItem>NPGDC</asp:ListItem>
<asp:ListItem>PGDC</asp:ListItem>
<asp:ListItem>KA CS PGDC</asp:ListItem>
<asp:ListItem>KA CS TAX</asp:ListItem>
<asp:ListItem>TS CS PGDC</asp:ListItem>
<asp:ListItem>TS CS TAX</asp:ListItem>
<asp:ListItem>MH CS PGDC</asp:ListItem>
<asp:ListItem>MH CS TAX</asp:ListItem>
<asp:ListItem>AP CS PGDC</asp:ListItem>
<asp:ListItem>AP CS TAX</asp:ListItem>
<asp:ListItem>TN CS PGDC</asp:ListItem>
<asp:ListItem>TN CS TAX</asp:ListItem>
</asp:DropDownList>
</div>

    <div class="col-md-3">
<label>Approval Status</label>
<asp:DropDownList ID="ddlApprovalStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="--Select--" Value="4"></asp:ListItem>
<asp:ListItem Value="0">PENDING</asp:ListItem>
<asp:ListItem Value="1">APPROVED</asp:ListItem>
<asp:ListItem Value="2">REJECTED</asp:ListItem>
</asp:DropDownList>
</div>

    
    <div class="col-md-3">
<label>Document Status</label>
<asp:DropDownList ID="ddlDocumentStatus" runat="server" CssClass="form-control">
<asp:ListItem Text="--Select--" Value="4"></asp:ListItem>
<asp:ListItem Value="0">N/A</asp:ListItem>
<asp:ListItem Value="1">ACTIVE</asp:ListItem>
<asp:ListItem Value="2">EXPIRING SOON</asp:ListItem>
<asp:ListItem Value="3">EXPIRED</asp:ListItem>
</asp:DropDownList>
</div>
<div class="col-md-3">
<label>Expiry Date</label>
<asp:TextBox ID="txtExpiryDate" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
</div>

<div class="col-md-3" style="margin-top:30px;">
<asp:Button ID="btnFilter" runat="server" Text="Apply Filters"
CssClass="btn btn-primary" OnClick="btnFilter_Click" />

<asp:Button ID="btnClear" runat="server" Text="Clear"
CssClass="btn btn-secondary" OnClick="btnClear_Click" />
</div>

</div>

</div>
</div>

        <asp:Button ID="btnAdd"
runat="server"
Text="+ Add Document"
CssClass="btn btn-success btn-sm"
OnClick="btnAdd_Click" /> <br />

<div class="card">
<div class="card-body table-responsive">
  
<asp:GridView ID="gvVehicles" runat="server"
AutoGenerateColumns="false"
CssClass="table table-bordered table-striped"
AllowPaging="true"
PageSize="10"
OnPageIndexChanging="gvVehicles_PageIndexChanging"
OnRowCommand="gvVehicles_RowCommand">

<Columns>

<asp:BoundField DataField="VehicleNo" HeaderText="Vehicle No" />
    <asp:BoundField DataField="RegistrationDate" HeaderText="Registration Date" />
<asp:BoundField DataField="DocumentType" HeaderText="Document Type" />
<asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date" />




<asp:TemplateField HeaderText="Status">
<ItemTemplate>

<span style='<%# 
Eval("Status").ToString()=="ACTIVE" ? "color:green;font-weight:bold;" :
Eval("Status").ToString()=="EXPIRING SOON" ? "color:orange;font-weight:bold;" :
Eval("Status").ToString()=="EXPIRED" ? "color:red;font-weight:bold;" :
"color:black;font-weight:bold;" %>'>

<%# Eval("Status") %>

</span>

</ItemTemplate>
</asp:TemplateField>



<asp:TemplateField HeaderText="Approval Status">
<ItemTemplate>

<span style='<%# 
Eval("ApprovalStatus").ToString()=="PENDING" ? "color:orange;font-weight:bold;" :
Eval("ApprovalStatus").ToString()=="APPROVED" ? "color:green;font-weight:bold;" :
Eval("ApprovalStatus").ToString()=="REJECTED" ? "color:red;font-weight:bold;" :
"" %>'>

<%# Eval("ApprovalStatus") %>

</span>

</ItemTemplate>
</asp:TemplateField>

<%--<asp:BoundField DataField="CreatedDate" HeaderText="Created Date" />
<asp:BoundField DataField="UpdatedDate" HeaderText="Modified Date" />
<asp:BoundField DataField="CreatedBy" HeaderText="Created By" />
<asp:BoundField DataField="UpdatedBy" HeaderText="Modified By" />--%>



<asp:TemplateField HeaderText="Action">
<ItemTemplate>

<asp:Button ID="btnUpdate"
runat="server"
Text="Update"
CssClass="btn btn-primary btn-sm"
CommandName="UpdateDoc"
CommandArgument='<%# Eval("Id") %>' />

</ItemTemplate>
</asp:TemplateField>



<asp:TemplateField HeaderText="Approval">
<ItemTemplate>

<asp:Button ID="btnApprove" runat="server"
Text="Approve"
CssClass="btn btn-success btn-sm"
CommandName="Approve"
CommandArgument='<%# Eval("Id") %>'
Enabled='<%# Eval("ApprovalStatus").ToString()=="PENDING" %>' />

<asp:Button ID="btnReject" runat="server"
Text="Reject"
CssClass="btn btn-danger btn-sm"
CommandName="Reject"
CommandArgument='<%# Eval("Id") %>'
Enabled='<%# Eval("ApprovalStatus").ToString()=="PENDING" %>' />

</ItemTemplate>
</asp:TemplateField>



<asp:TemplateField HeaderText="Delete">
<ItemTemplate>

<asp:Button ID="btnDelete" runat="server"
Text="Delete"
CssClass="btn btn-dark btn-sm"
CommandName="DeleteDoc"
CommandArgument='<%# Eval("Id") %>'
OnClientClick="return confirm('Delete this record?');" />

</ItemTemplate>
</asp:TemplateField>

</Columns>
</asp:GridView>
    
</div>
</div>

</div>
</section>

</asp:Content>