<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="LcReport.aspx.cs" Inherits="Consignment.ConsignmentPanel.LcReport" %>
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
                        <li class="breadcrumb-item active">LC Report</li>
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
                                <label>Branch</label>
                                <asp:DropDownList ID="ddlBranch" CssClass="searchable form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>User</label>
                                <asp:DropDownList ID="ddlUser" CssClass="searchable form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                         <div class="col-md-3">
                            <div class="form-group">
                                <label>Vehicle</label>
                                <asp:DropDownList ID="ddlVehicle" CssClass="searchable form-control" runat="server"></asp:DropDownList>
                            </div>
                        </div>
                      <div class="col-md-3">
                            <div class="form-group">
                                <label>Date From</label>
                                <asp:TextBox ID="txtDateFrom" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                      
                    </div>
                    <div class="row">
                              <div class="col-md-3">
                            <div class="form-group">
                                <label>Date To</label>
                                <asp:TextBox ID="txtDateTo" TextMode="Date" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <label>LC From</label>
                                <asp:TextBox ID="txtLRFrom" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                  <label>LC To</label>
                                <asp:TextBox ID="txtLRTo" runat="server" CssClass="form-control" autocomplete="off"></asp:TextBox>
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
                            AllowPaging="True" DataKeyNames="id" OnPageIndexChanging="gvReport_PageIndexChanging" Width="100%" OnSorting="gvReport_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Id" DataField="Id" Visible="false" SortExpression="Id" />
                                <asp:BoundField HeaderText="Vehicle" DataField="Vehicle" SortExpression="Vehicle" />
                                <asp:BoundField HeaderText="Transporter" DataField="Transporter" SortExpression="Transporter" />
                                <asp:BoundField HeaderText="Source" DataField="Source" SortExpression="Source" />
                                <asp:BoundField HeaderText="Destination" DataField="Destination" SortExpression="Destination" />
                                <asp:BoundField HeaderText="Hire No" DataField="Hire No" SortExpression="Hire No" />

                                <asp:BoundField HeaderText="Date" DataField="Date" SortExpression="Date" />
                                <asp:BoundField HeaderText="Balance Account" DataField="Balance Account" SortExpression="Balance Account" />
                                <asp:BoundField HeaderText="Advance Account" DataField="Advance Account" SortExpression="Advance Account" />
                                <asp:BoundField HeaderText="MF No" DataField="MF No" SortExpression="MF No" />
                                <asp:BoundField HeaderText="Freight" DataField="Freight" SortExpression="Freight" />
                                <asp:BoundField HeaderText="Loading" DataField="Loading" SortExpression="Loading" />
                                <asp:BoundField HeaderText="Detention" DataField="Detention" SortExpression="Detention" />
                                <asp:BoundField HeaderText="Other Charges" DataField="Other Charges" SortExpression="Other Charges" />
                                <asp:BoundField HeaderText="Broker Comm" DataField="Broker Comm" SortExpression="Broker Comm" />
                                <asp:BoundField HeaderText="Munshiyana" DataField="Munshiyana" SortExpression="Munshiyana" />


                                <asp:BoundField HeaderText="Other Ded" DataField="Other Ded" SortExpression="Other Ded" />
                                <asp:BoundField HeaderText="Advance" DataField="Advance" SortExpression="Advance" />
                                <asp:BoundField HeaderText="Balance" DataField="Balance" SortExpression="Balance" />
                                <asp:BoundField HeaderText="To Pay At" DataField="To Pay At" SortExpression="To Pay At" />
                               
                                
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
