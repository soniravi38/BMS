<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="BranchwiseParty.aspx.cs" Inherits="Consignment.ConsignmentPanel.BranchwiseParty" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <style>
        .styled-table {
            border-collapse: collapse;
            margin: 10px 0;
            font-size: 0.7em;
            /*font-family: sans-serif;*/
            width: 100% !important;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.15);
        }

            .styled-table thead tr {
                background-color: #212529;
                color: #ffffff;
                text-align: left;
            }

            .styled-table th,
            .styled-table td {
                padding: 5px 6px;
            }

            .styled-table tbody tr {
                border-bottom: 1px solid #dddddd;
                vertical-align: top;
                white-space: nowrap !important;
                overflow: hidden !important;
                text-overflow: ellipsis !important;
            }

                .styled-table tbody tr:nth-of-type(even) {
                    background-color: #f3f3f3;
                }

                .styled-table tbody tr:last-of-type {
                    border-bottom: 2px solid #009879;
                }

                .styled-table tbody tr.active-row {
                    font-weight: bold;
                    color: #009879;
                }
    </style>
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>
                    </h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Branchwise Party</li>
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
                                <label>Party Name</label>
                                <asp:TextBox ID="txtPartyName" placeholder="Enter any Keyword here to Search..." CssClass="form-control PartyName" runat="server"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <br />
    <section class="content">
        <div class="container-fluid">
            <div class="box box-default">
                <div class="box-body">
                    <table border="1" class="styled-table">
                        <thead>
                            <tr>
                                <th colspan="3">
                                    <asp:DropDownList ID="ddlBranch" AutoPostBack="true" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" runat="server" CssClass="form-control searchable">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ControlToValidate="ddlBranch" ID="RequiredFieldValidator2" runat="server" InitialValue="0"
                                        Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                        ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Branch
                            </div>
                                    </asp:RequiredFieldValidator>
                                </th>
                            </tr>
                            <tr>
                                <th>
                                     <input type="checkbox" class="ConsignorAll" id="ConsignorAll" />CONSIGNOR
                                   </th>
                                <th>
                                    <input type="checkbox" class="ConsigneeAll" id="ConsigneeAll" />CONSIGNEE </th>
                                <th>
                                    <input type="checkbox" class="BillingPartyAll" id="BillingPartyAll" />BILLING PARTY </th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:CheckBoxList ID="chkConsignor" CssClass="checkBoxConsignor" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="chkConsignee" CssClass="checkboxConsignee" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                                <td>
                                    <asp:CheckBoxList ID="chkBillingParty" CssClass="checkboxBillingParty" runat="server">
                                    </asp:CheckBoxList>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:Button ID="btnSubmit" ValidationGroup="a" Style="position: fixed; right: 5px; bottom: 62px" OnClick="btnSubmit_Click" runat="server" CssClass="btn btn-success" Text="SUBMIT" />
                </div>
            </div>
        </div>
    </section>

    <script>
        $(document).ready(function () {
            $(".PartyName").on("keyup", function () {
                var value = $(this).val().toLowerCase();
                $(".styled-table tbody tr").filter(function () {
                    $(this).toggle($(this).text().toLowerCase().indexOf(value) > -1)
                });
            });

            $(".ConsignorAll").change(function (e) {
                var table = $(e.target).closest('table');
                $('.checkBoxConsignor input:checkbox', table).prop('checked', $(this).is(':checked'));
            });

            $(".ConsigneeAll").change(function (e) {
                var table = $(e.target).closest('table');
                $('.checkboxConsignee input:checkbox', table).prop('checked', $(this).is(':checked'));
            });

            $(".BillingPartyAll").change(function (e) {
                var table = $(e.target).closest('table');
                $('.checkboxBillingParty input:checkbox', table).prop('checked', $(this).is(':checked'));
            });

        });
    </script>
</asp:Content>







