<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="ConsgReportMBT.aspx.cs" Inherits="Consignment.ConsignmentPanel.ConsgReportMBT" %>
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
                        <li class="breadcrumb-item active">LR List</li>
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
                                <label>LR From</label>
                                <asp:TextBox ID="txtLRFrom" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                  <label>LR To</label>
                                <asp:TextBox ID="txtLRTo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
                            AllowPaging="True" DataKeyNames="Bookid" OnPageIndexChanging="gvReport_PageIndexChanging" Width="100%" OnSorting="gvReport_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" HeaderText="Bookid" DataField="Bookid" SortExpression="Bookid" />
                                <asp:BoundField HeaderText="LR No" DataField="BookLrNo" SortExpression="BookLrNo" />
                                <asp:BoundField HeaderText="Book Date" DataField="BookDate1" SortExpression="BookDate1" />
                                <asp:BoundField HeaderText="Branch" DataField="BranchName" SortExpression="BranchName" />
                                <asp:BoundField HeaderText="Source" DataField="SourceFrom" SortExpression="SourceFrom" />
                                <asp:BoundField HeaderText="Destination" DataField="ToDestination" SortExpression="ToDestination" />
                                <asp:BoundField HeaderText="Created By" DataField="CreatedBy" SortExpression="CreatedBy" />
                                <asp:BoundField HeaderText="Updated By" DataField="UpdatedBy" SortExpression="UpdatedBy" />
                                 <asp:BoundField HeaderText="Created Date" DataField="Created_Date" ControlStyle-Width="20" SortExpression="Created_Date" />
                                <asp:BoundField HeaderText="Modified Date" DataField="Modified_Date" ControlStyle-Width="20" SortExpression="Modified_Date" />
                                <asp:TemplateField HeaderStyle-Width="27px">
                                    <ItemTemplate>
                                        <a href="ReportViewer.aspx?BookId=<%#Eval("BookId") %>" title="Print this record">
                                            <img src="data:image/png;base64,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" style="margin-top: -17% !important" width="22" alt="Print" />
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
                       
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>


