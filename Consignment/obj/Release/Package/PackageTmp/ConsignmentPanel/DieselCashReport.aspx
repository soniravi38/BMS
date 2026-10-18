<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="DieselCashReport.aspx.cs" Inherits="Consignment.ConsignmentPanel.DieselCashReport" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">Diesel-Cash Report</li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Date From</label>
                        <asp:TextBox ID="txtDateFrom" TextMode="Date" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Date To</label>
                        <asp:TextBox ID="txtDateTo" TextMode="Date" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Vehicle No</label>
                        <asp:TextBox ID="txtVehicleNo" autocomplete="off" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="col-md-3">
                    <div class="form-group">
                        <label>Branch</label>
                         <asp:DropDownList ID="ddlBranch" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="row">
                <div class="col-md-3">
                    <div class="form-group">
                        <label>&nbsp;</label>
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-danger" Style="margin-top: 13%" Text="Search" runat="server" />
                    </div>
                </div>
            </div>
        </div>
    </section>
    <section class="content">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-12">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server" ></asp:Label> | Total Amount : <asp:Label ID="lblDieselAMount"  runat="server">0</asp:Label>
                        <asp:ImageButton ID="imgBtnExcel" OnClick="imgBtnExcel_Click" ToolTip="Excel" Width="20" ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Microsoft_Office_Excel_%282019%E2%80%93present%29.svg/826px-Microsoft_Office_Excel_%282019%E2%80%93present%29.svg.png" runat="server" />
                        |
                        <asp:ImageButton ID="imgBtnPdf" OnClick="imgBtnPdf_Click" ToolTip="PDF" Width="20" ImageUrl="https://cdn4.iconfinder.com/data/icons/file-extensions-1/64/pdfs-512.png" runat="server" />
                    </h1>

                    Total Record's :
                    <asp:Literal ID="litrecordcount" runat="server" Text="0" />
                </div>
                
            </div>
            <div class="box box-default">
                <div class="box-body">

                    <div class="row" style="padding: 10px; background: #dae1fc; border-radius: 5px; font-size: 9pt; width: 100%; margin-left: 0%;">
                        <marquee>
                            <h5 style="color: red; font-weight: bold">
                                <asp:Label ID="lblMsg" runat="server"></asp:Label></h5>
                        </marquee>
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
                        <div class="col-md-6">
                            <a href="AddDieselReconciliation.aspx" id="btnAdd" runat="server" class="btn btn-custon-four btn-success" style="color: white; margin-top: 0%">
                                <asp:Label ID="lblInsert1" runat="server" Text=""></asp:Label>
                                <span class="fa fa-plus-circle"></span></a>
                            <a href="AddDieselReconciliationBulk.aspx" id="btnAddBulk" runat="server" class="btn btn-custon-four btn-dark" style="color: white; margin-top: 0%">
                                <asp:Label ID="Label1" runat="server" Text="Add Diesel Data in Bulk"></asp:Label>
                                <span class="fa fa-plus-circle"></span></a>

                        </div>

                        <div class="col-md-2">
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
                            AllowPaging="True" DataKeyNames="ID" OnPageIndexChanging="gvReport_PageIndexChanging" Width="100%" OnRowCommand="gvReport_RowCommand" OnSorting="gvReport_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" HeaderText="ID" DataField="ID" SortExpression="ID" />
                                <asp:BoundField HeaderText="Date" DataField="LrDate" SortExpression="LrDate" />
                                <asp:BoundField HeaderText="LR No" DataField="LRNo" SortExpression="LRNo" />
                                <asp:BoundField HeaderText="Vehicle No" DataField="VechicleFullNo" SortExpression="VechicleFullNo" />
                                <asp:BoundField HeaderText="Vehicle Mode" DataField="VehicleMode" SortExpression="VehicleMode" />
                                <asp:BoundField HeaderText="FC No" DataField="FCNo" SortExpression="FCNo" />
                                <asp:BoundField HeaderText="Remarks" DataField="Remarks" SortExpression="Remarks" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" />
                                <asp:BoundField HeaderText="Total CMS" DataField="TotalCMS" SortExpression="TotalCMS" />
                                <asp:BoundField HeaderText="Destination" DataField="Destination" SortExpression="Destination" />
                                <asp:BoundField HeaderText="From" DataField="From" SortExpression="From" />
                                <asp:BoundField HeaderText="To" DataField="To" SortExpression="To" />
                                <asp:BoundField HeaderText="Created By" DataField="CreatedBy" SortExpression="CreatedBy" />
                              <asp:BoundField HeaderText="Created Date" DataField="Created_Date" ControlStyle-Width="20" SortExpression="Created_Date" />

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
                
                </div>
            </div>
        </div>
    </section>


    <br />
     <section class="content">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-12">
                    <h1>
                        <asp:Label ID="lblHeading1" runat="server"></asp:Label> | Total Amount : <asp:Label ID="lblCashAmount" runat="server">0</asp:Label>
                        <asp:ImageButton ID="imgBtnExcel1" OnClick="imgBtnExcel1_Click" ToolTip="Excel" Width="20" ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Microsoft_Office_Excel_%282019%E2%80%93present%29.svg/826px-Microsoft_Office_Excel_%282019%E2%80%93present%29.svg.png" runat="server" />
                        |
                        <asp:ImageButton ID="imgBtnPdf1" OnClick="imgBtnPdf1_Click" ToolTip="PDF" Width="20" ImageUrl="https://cdn4.iconfinder.com/data/icons/file-extensions-1/64/pdfs-512.png" runat="server" />
                    </h1>

                    Total Record's :
                    <asp:Literal ID="litrecordcount1" runat="server" Text="0" />
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
                        <div class="col-md-6">
                           <a href="AddCashData.aspx" id="btnAdd1" runat="server" class="btn btn-custon-four btn-success" style="color: white; margin-top: 0%">
                                <asp:Label ID="lblInsertCash" runat="server" Text=""></asp:Label>
                                <span class="fa fa-plus-circle"></span></a>

                        </div>

                        <div class="col-md-2">
                        </div>
                        <div class="col-md-1"></div>
                        <div class="col-md-3">
                          
                        </div>
                    </div>
                    <div class="container-fluid" style="overflow-x: scroll; margin-top: 1%">
                        <asp:GridView ID="gvReport1" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                            AllowPaging="True" DataKeyNames="ID" OnPageIndexChanging="gvReport1_PageIndexChanging" Width="100%" OnRowCommand="gvReport1_RowCommand" OnSorting="gvReport1_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" HeaderText="ID" DataField="ID" SortExpression="ID" />
                                <asp:BoundField HeaderText="Date" DataField="Date" SortExpression="Date" />
                                <asp:BoundField HeaderText="Vehicle No" DataField="VehicleNo" SortExpression="VehicleNo" />
                                <asp:BoundField HeaderText="Driver" DataField="Driver" SortExpression="Driver" />
                                <asp:BoundField HeaderText="From" DataField="From" SortExpression="From" />
                                <asp:BoundField HeaderText="To" DataField="To" SortExpression="To" />
                                <asp:BoundField HeaderText="Amount" DataField="Amount" SortExpression="Amount" />
                                <asp:BoundField HeaderText="Purpose" DataField="Purpose" SortExpression="Purpose" />
                                <asp:BoundField HeaderText="Paying Branch" DataField="BranchName" SortExpression="BranchName" />
                                <asp:BoundField HeaderText="Created By" DataField="CreatedBy" SortExpression="CreatedBy" />
                              <asp:BoundField HeaderText="Created Date" DataField="Created_Date" ControlStyle-Width="20" SortExpression="Created_Date" />

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
                
                </div>
            </div>
        </div>
    </section>
</asp:Content>




