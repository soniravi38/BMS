<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="BNGExport.aspx.cs" Inherits="Consignment.ConsignmentPanel.BNGExport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

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
                        <li class="breadcrumb-item active">BNG Export</li>
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
                                <label>Date From</label>
                                <asp:TextBox ID="txtDateFrom" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Date To</label>
                                <asp:TextBox ID="txtDateTo" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>Consignment Entry Mode</label>
                                <asp:DropDownList ID="ddlMode" runat="server" CssClass="searchable form-control">
                                    <asp:ListItem Text="All" Value=""></asp:ListItem>
                                    <asp:ListItem Text="EConsignment" Value="E"></asp:ListItem>
                                    <asp:ListItem Text="Manual" Value="M"></asp:ListItem>
                                    <asp:ListItem Text="Multi-Consignment" Value="B"></asp:ListItem>
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
                                <asp:ListItem Value="2000">2000 Record(s)</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="container-fluid" style="overflow-x: scroll; margin-top: 1%">
                        <asp:GridView ID="gvReport" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="Con No" OnPageIndexChanging="gvReport_PageIndexChanging" Width="100%" OnSorting="gvReport_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                            
                                <asp:BoundField HeaderText="Con No" DataField="Con No" SortExpression="Con No" />
                                <asp:BoundField HeaderText="Con Date" DataField="Con Date" SortExpression="Con Date" />
                                <asp:BoundField HeaderText="Ref No" DataField="Ref No" SortExpression="Ref No" />
                                <asp:BoundField HeaderText="Invoice No" DataField="Invoice No" SortExpression="Invoice No" />
                                <asp:BoundField HeaderText="Invoice Date" DataField="Invoice Date" SortExpression="Invoice Date" />
                                <asp:BoundField HeaderText="Invoice Value" DataField="Invoice Value" SortExpression="Invoice Value" />
                                <asp:BoundField HeaderText="Ship No" DataField="Ship No" SortExpression="Ship No" />
                                <asp:BoundField HeaderText="Doc No" DataField="Doc No" SortExpression="Doc No" />
                                <asp:BoundField HeaderText="Do No" DataField="Do No" SortExpression="Do No" />
                                <asp:BoundField HeaderText="Source" DataField="Source" SortExpression="Source" />
                                <asp:BoundField HeaderText="Destination" DataField="Destination" SortExpression="Destination" />
                                <asp:BoundField HeaderText="Consignor" DataField="Consignor" SortExpression="Consignor" />
                                <asp:BoundField HeaderText="Consignee" DataField="Consignee" SortExpression="Consignee" />
                                <asp:BoundField HeaderText="Billing Party" DataField="Billing Party" SortExpression="Billing Party" />
                                <asp:BoundField HeaderText="Vehicle No" DataField="Vehicle No" SortExpression="Vehicle No" />
                                <asp:BoundField HeaderText="Load Type" DataField="Load Type" SortExpression="Load Type" />
                                <asp:BoundField HeaderText="Commodity" DataField="Commodity" SortExpression="Commodity" />
                                <asp:BoundField HeaderText="PKG" DataField="PKG" SortExpression="PKG" />
                                <asp:BoundField HeaderText="Unit" DataField="Unit" SortExpression="Unit" />
                                <asp:BoundField HeaderText="Gross Weight" DataField="Gross Weight" SortExpression="Gross Weight" />
                                <asp:BoundField HeaderText="Tare" DataField="Tare" SortExpression="Tare" />
                                <asp:BoundField HeaderText="Net Weight" DataField="Net Weight" SortExpression="Net Weight" />
                                <asp:BoundField HeaderText="Rate" DataField="Rate" SortExpression="Rate" />
                                <asp:BoundField HeaderText="Freight Amount" DataField="Freight Amount" SortExpression="Freight Amount" />
                                <asp:BoundField HeaderText="Type" DataField="Type" SortExpression="Type" />
                                <asp:BoundField HeaderText="At" DataField="At" SortExpression="At" />
                                <asp:BoundField HeaderText="GST No" DataField="GST No" SortExpression="GST No" />
                                <asp:BoundField HeaderText="Eway Bill No" DataField="Eway Bill No" SortExpression="Eway Bill No" />
                                <asp:BoundField HeaderText="Eway Bill Date" DataField="Eway Bill Date" SortExpression="Eway Bill Date" />
                                <asp:BoundField HeaderText="Eway Bill Exp Date" DataField="Eway Bill Exp Date" SortExpression="Eway Bill Exp Date" />
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
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>


