<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="ListInternalLR.aspx.cs" Inherits="Consignment.ConsignmentPanel.ListInternalLR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>
                        <asp:ImageButton ID="imgBtnExcel" OnClick="imgBtnExcel_Click" ToolTip="Excel" Width="20" ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Microsoft_Office_Excel_%282019%E2%80%93present%29.svg/826px-Microsoft_Office_Excel_%282019%E2%80%93present%29.svg.png" runat="server" />
                        |
                        <asp:ImageButton ID="imgBtnPdf" OnClick="imgBtnPdf_Click" ToolTip="PDF" Width="20" ImageUrl="https://cdn4.iconfinder.com/data/icons/file-extensions-1/64/pdfs-512.png" runat="server" />
                    </h1>

                    Total Record's :
                    <asp:Literal ID="litrecordcount" runat="server" Text="0" />
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Internal LR / Settlement List</li>
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
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>CN Date From</label>
                                <asp:TextBox ID="txtDateFrom" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>CN Date To</label>
                                <asp:TextBox ID="txtDateTo" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Original LR No</label>
                                <asp:TextBox ID="txtLRNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Crossing Note No</label>
                                <asp:TextBox ID="txtCNNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Vehicle No</label>
                                <asp:TextBox ID="txtVehicleNo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Bill Status</label>
                                <asp:DropDownList ID="ddlBillStatus" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="">--All--</asp:ListItem>
                                    <asp:ListItem Value="D">Draft</asp:ListItem>
                                    <asp:ListItem Value="F">Final</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-danger" Style="margin-top: 13%" Text="Search" runat="server" />
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
                    <div class="row">
                        <div class="col-md-4">
                            <a href="AddInternalLR.aspx" id="btnAdd" runat="server" class="btn btn-custon-four btn-success" style="color: white; margin-top: 0%">
                                <asp:Label ID="lblInsert1" runat="server" Text=""></asp:Label>
                                <span class="fa fa-plus-circle"></span></a>
                        </div>
                        <div class="col-md-3">
                        </div>
                        <div class="col-md-1">
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                            <asp:DropDownList ID="ddlPageSize" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                <asp:ListItem Value="10">10 Record(s)</asp:ListItem>
                                <asp:ListItem Value="25">25 Record(s)</asp:ListItem>
                                <asp:ListItem Value="50">50 Record(s)</asp:ListItem>
                                <asp:ListItem Value="100">100 Record(s)</asp:ListItem>
                                <asp:ListItem Value="200">200 Record(s)</asp:ListItem>
                                <asp:ListItem Value="500">500 Record(s)</asp:ListItem>
                                <asp:ListItem Value="1000">1000 Record(s)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="container-fluid" style="overflow-x: scroll; margin-top: 1%">
                        <asp:GridView ID="gvReport" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="Id" OnPageIndexChanging="gvReport_PageIndexChanging" Width="100%" OnRowCommand="gvReport_RowCommand" OnSorting="gvReport_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" HeaderText="Id" DataField="Id" SortExpression="Id" />
                                <asp:BoundField HeaderText="Crossing Note No" DataField="CNNo" SortExpression="CNNo" />
                                <asp:BoundField HeaderText="CN Date" DataField="CNDate" SortExpression="CNDate" />
                                <asp:BoundField HeaderText="LR Company" DataField="PartnerCompany" SortExpression="PartnerCompany" />
                                <asp:BoundField HeaderText="Original LR No" DataField="PartnerLRNo" SortExpression="PartnerLRNo" />
                                <asp:BoundField HeaderText="LR Date" DataField="PartnerLRDate" SortExpression="PartnerLRDate" />
                                <asp:BoundField HeaderText="Vehicle No" DataField="VehicleNo" SortExpression="VehicleNo" />
                                <asp:BoundField HeaderText="From" DataField="FromStation" SortExpression="FromStation" />
                                <asp:BoundField HeaderText="To" DataField="ToStation" SortExpression="ToStation" />
                                <asp:BoundField HeaderText="Consignee" DataField="LRConsigneeName" SortExpression="LRConsigneeName" />
                                <asp:BoundField HeaderText="Voucher(s)" DataField="VoucherCount" SortExpression="VoucherCount" />
                                <asp:BoundField HeaderText="Voucher Total" DataField="VoucherTotal" SortExpression="VoucherTotal" />
                                <asp:BoundField HeaderText="Bill No" DataField="BillNo" SortExpression="BillNo" />
                                <asp:BoundField HeaderText="Bill Amount" DataField="BillAmount" SortExpression="BillAmount" />
                                <asp:BoundField HeaderText="Bill Status" DataField="BillStatus" SortExpression="BillStatus" />
                                <asp:BoundField HeaderText="Created By" DataField="CreatedName" SortExpression="CreatedName" />
                                <asp:BoundField HeaderText="Created Date" DataField="Created_Date" SortExpression="Created_Date" />
                                <asp:TemplateField HeaderStyle-Width="27px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnEdit" runat="server" Width="22" ImageUrl="../App_Content/icon/edit.png"
                                            AlternateText="Edit" ToolTip="Edit / Update Bill" CommandName="EditRecord" CommandArgument='<%#Eval("Id") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="27px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="27px">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnDelete" runat="server" Width="22" ImageUrl="../App_Content/icon/del.png"
                                            AlternateText="Delete" ToolTip="Delete this record" CommandName="isDelete" CommandArgument='<%#Eval("Id") %>'
                                            OnClientClick='return confirm("Are You Sure To Delete This Internal LR, Voucher(s) and Bill?")' />
                                    </ItemTemplate>
                                    <HeaderStyle Width="27px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderStyle-Width="27px">
                                    <ItemTemplate>
                                        <a href="InternalLRReportViewer.aspx?Id=<%#Eval("Id") %>" title="Generate & Print All">
                                            <i class="fa fa-print" style="font-size: 20px; color: #212529"></i>
                                        </a>
                                    </ItemTemplate>
                                    <HeaderStyle Width="27px"></HeaderStyle>
                                </asp:TemplateField>
                            </Columns>
                            <EmptyDataTemplate>
                                <div class="EmptyDataTemplate">
                                    No Record Found !
                                </div>
                            </EmptyDataTemplate>
                            <RowStyle CssClass="RowStyle" HorizontalAlign="Center" />
                            <PagerStyle CssClass="PagerStyle" ForeColor="white" HorizontalAlign="Center" Font-Bold="true" BackColor="#212529" />
                            <FooterStyle BackColor="#CCCCCC" />
                            <HeaderStyle CssClass="HeaderStyle" HorizontalAlign="Center" BackColor="#212529" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle CssClass="RowStyle" BackColor="#CCCCCC" />
                            <PagerSettings Position="Bottom" />
                            <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                            <SortedAscendingHeaderStyle BackColor="#808080" />
                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                            <SortedDescendingHeaderStyle BackColor="#383838" />
                        </asp:GridView>
                    </div>
                    <div class="text-left">
                        <a href="AddInternalLR.aspx" id="btnAdd1" runat="server" class="btn btn-custon-four btn-success" style="color: white">
                            <asp:Label ID="lblInsert2" runat="server" Text=""></asp:Label>
                            <span class="fa fa-plus-circle"></span></a>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
