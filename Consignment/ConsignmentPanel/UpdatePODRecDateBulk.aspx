<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master"
    AutoEventWireup="true"
    CodeBehind="UpdatePODRecDateBulk.aspx.cs"
    Inherits="Consignment.ConsignmentPanel.UpdatePODRecDateBulk" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

<section class="content-header">
    <div class="container-fluid">
        <h1>Bulk Update POD Received Date</h1>
    </div>
</section>

<section class="content">
    <div class="container-fluid">
        <div class="box box-default">
            <div class="box-body">

                <div class="row">
                    <div class="col-md-6">

                        <div class="form-group">
                            <label>Upload CSV File</label>

                            <asp:FileUpload ID="fuUpload"
                                runat="server"
                                CssClass="form-control" />
                        </div>

                    </div>
                </div>

                <asp:Button ID="btnSubmit"
                    runat="server"
                    Text="Upload"
                    CssClass="btn btn-success"
                    OnClick="btnSubmit_Click" />

            </div>
        </div>
    </div>
</section>

<br />

<div style="padding:10px; background:#f2dede; border-radius:5px; width:75%;">
    <h6>CSV Format:</h6>

    LRNo,RecDateHO<br />
    LR001,12-05-2026<br />
    LR002,13-05-2026<br />

</div>

</asp:Content>