<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master"
    AutoEventWireup="true" CodeBehind="AddPOD.aspx.cs"
    Inherits="Consignment.ConsignmentPanel.AddPOD" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <h1>
                <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
        </div>
    </section>

    <section class="content">
        <div class="container-fluid">
            <div class="box box-default">
                <div class="box-body">

                    <div class="row">

    <div class="col-md-4">
        <div class="form-group">
            <label>LR Date</label>
            <asp:TextBox ID="txtLRDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Service Month</label>
            <asp:TextBox ID="txtServiceMonth" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>LR No</label>
            <asp:TextBox ID="txtLRNo" runat="server" CssClass="form-control" />
        </div>
    </div>

</div>

<div class="row">

    <div class="col-md-4">
        <div class="form-group">
            <label>Consignor Name</label>
            <asp:TextBox ID="txtConsignorName" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Origin</label>
            <asp:TextBox ID="txtOrigin" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Destination</label>
            <asp:TextBox ID="txtDestination" runat="server" CssClass="form-control" />
        </div>
    </div>

</div>

<div class="row">

    <div class="col-md-4">
        <div class="form-group">
            <label>Vehicle No</label>
            <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Package Count</label>
            <asp:TextBox ID="txtPackageCount" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Vehicle Owner</label>
            <asp:TextBox ID="txtVehicleOwner" runat="server" CssClass="form-control" />
        </div>
    </div>

</div>

<div class="row">

    <div class="col-md-4">
        <div class="form-group">
            <label>Vehicle Type</label>
            <asp:TextBox ID="txtVehicleType" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Exceptional</label>
            <asp:TextBox ID="txtExceptionalFlag" runat="server" CssClass="form-control" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Concerned Branch</label>
            <asp:TextBox ID="txtConcernedBranch" runat="server" CssClass="form-control" />
        </div>
    </div>

</div>

<hr />

<div class="row">

    <div class="col-md-4">
        <div class="form-group">
            <label>Ack Status</label>
            <asp:DropDownList ID="ddlAckStatus" runat="server" CssClass="form-control">
                <asp:ListItem Value="0">Pending</asp:ListItem>
                <asp:ListItem Value="1">Received</asp:ListItem>
            </asp:DropDownList>
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Sent Date</label>
            <asp:TextBox ID="txtSentByBranchDate" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Sent Through</label>
            <asp:TextBox ID="txtSentThrough" runat="server" CssClass="form-control" />
        </div>
    </div>

</div>

<div class="row">

    <div class="col-md-4">
        <div class="form-group">
            <label>Rec Date HO</label>
            <asp:TextBox ID="txtRecDateHO" runat="server" CssClass="form-control" TextMode="Date" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Remarks Branch</label>
            <asp:TextBox ID="txtRemarksBranch" runat="server" CssClass="form-control" TextMode="MultiLine" />
        </div>
    </div>

    <div class="col-md-4">
        <div class="form-group">
            <label>Remarks HO</label>
            <asp:TextBox ID="txtRemarksHO" runat="server" CssClass="form-control" TextMode="MultiLine" />
        </div>
    </div>

</div>

                    <br />

                    <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success"
                        Text="Save" OnClick="btnSubmit_Click" />

                    <asp:Button ID="btnClear" runat="server" CssClass="btn btn-default"
                        Text="Clear" OnClick="btnClear_Click" />

                </div>
            </div>
        </div>
    </section>

</asp:Content>
