<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="ListActivityLog.aspx.cs" Inherits="Consignment.ConsignmentPanel.ListActivityLog" %>
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
                        <li class="breadcrumb-item active">Activity Log</li>
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
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>User</label>
                                <asp:DropDownList ID="ddlUser" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>&nbsp;</label>
                                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-danger" Style="margin-top: 19%" Text="Search" runat="server" />
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
                            AllowPaging="True" DataKeyNames="ID" OnPageIndexChanging="gvReport_PageIndexChanging" Width="100%" OnSorting="gvReport_Sorting"
                            AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>

                                    <HeaderStyle Width="35px"></HeaderStyle>
                                </asp:TemplateField>
                                <asp:BoundField Visible="false" HeaderText="ID" DataField="ID" SortExpression="ID" />
                                <asp:BoundField HeaderText="Activity Log" DataField="Log" SortExpression="Log" />
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






