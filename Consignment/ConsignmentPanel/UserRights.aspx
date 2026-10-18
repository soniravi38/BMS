<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="UserRights.aspx.cs" Inherits="Consignment.ConsignmentPanel.UserRights" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Scripts/jquery-3.4.1.min.js"></script>

    <style>
        .center {
            text-align: center;
            display: inherit;
        }

        .header {
            padding: 3px;
            background: #212529;
            border-radius: 10px 10px 0 0;
            color: white;
            text-align: center;
            display: inherit;
            margin-top: 25px
        }

        .content-row {
            border: 2px solid #d4d1d1;
            border-radius: 0 0 10px 10px;
            padding: 10px
        }
    </style>
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server" Text="User Rights Management"></asp:Label></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">User Rights Management</li>
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
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>User</label>
                                <asp:DropDownList ID="ddlUser" OnSelectedIndexChanged="ddlUser_SelectedIndexChanged" AutoPostBack="true" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlUser" ID="RequiredFieldValidator5" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select User
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                    <asp:Repeater ID="rpMenu" runat="server">
                        <ItemTemplate>
                            <div class="Menu">
                                <div class="row header">
                                    <h4><b>
                                        <%--<input type="checkbox" class="CheckAll P<%# Eval("MenuName") %>" id="CheckAll" />--%>
                                        <asp:Label ID="HeadName" runat="server" Text='<%# Eval("MenuName") %>'></asp:Label></b></h4>
                                </div>
                                <div class="row content-row">
                                    <div class="col-md-2">
                                        <div class="row center">
                                            LIST
                                        </div>
                                        <div class="row center">
                                            <asp:CheckBox ID="chkList" Checked='<%#Convert.ToInt32(Eval("LIST"))==1?true:false %>' runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row center">
                                            CREATE
                                        </div>
                                        <div class="row center">
                                            <asp:CheckBox ID="chkCreate" Checked='<%#Convert.ToInt32(Eval("CREATE"))==1?true:false %>' runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row center">
                                            UPDATE
                                        </div>
                                        <div class="row center">
                                            <asp:CheckBox ID="chkUpdate" Checked='<%#Convert.ToInt32(Eval("UPDATE"))==1?true:false %>' runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row center">
                                            DELETE
                                        </div>
                                        <div class="row center">
                                            <asp:CheckBox ID="chkDelete" Checked='<%#Convert.ToInt32(Eval("DELETE"))==1?true:false %>' runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row center">
                                            ACTIVE
                                        </div>
                                        <div class="row center">
                                            <asp:CheckBox ID="chkActive" Checked='<%#Convert.ToInt32(Eval("ACTIVE"))==1?true:false %>' runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="row center">
                                            EXPORT
                                        </div>
                                        <div class="row center">
                                            <asp:CheckBox ID="chkExport" Checked='<%#Convert.ToInt32(Eval("EXPORT"))==1?true:false %>' runat="server" CssClass="form-control" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                    <asp:Button ID="btnSubmit" ValidationGroup="a" Style="position: fixed; right: 5px; bottom: 62px" OnClick="btnSubmit_Click" runat="server" CssClass="btn btn-success" Text="SUBMIT" />
                </div>
            </div>
        </div>
    </section>


    <script>
        $(document).ready(function () {
            $(".CheckAll").change(function () {
                $('#ContentPlaceHolder1_rpMenu_chkList_1').attr('checked', $(this).is(':checked'));
            });
        });
    </script>
</asp:Content>
