<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="VehicleStatus.aspx.cs" Inherits="Consignment.ConsignmentPanel.VehicleStatus" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label></h1>
                </div>
                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="ListDieselReconciliation.aspx">Diesel Data List</a></li>
                        <li class="breadcrumb-item active">Vehicle Status</li>
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
                                <label>Vehicle No.</label><span class="Star">*</span>
                                <asp:DropDownList ID="ddlVehicle" AutoPostBack="true" OnSelectedIndexChanged="ddlVehicle_SelectedIndexChanged" runat="server" CssClass="searchable form-control">
                                </asp:DropDownList>
                            </div>
                        </div>

                    </div>
                    <hr />

                    <div class="row">
                        <div class="container-fluid" style="overflow-x: scroll; margin-top: 1%">
                            <asp:GridView ID="gvReport" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                                AllowPaging="True" DataKeyNames="ID" Width="100%"
                                AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3" ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                <Columns>
                                    <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                        <ItemTemplate>
                                            <%#Container.DataItemIndex+1 %>
                                        </ItemTemplate>

                                        <HeaderStyle Width="35px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="Vehicle No" DataField="VehicleNo" SortExpression="VehicleNo" />
                                    <asp:BoundField HeaderText="Size" DataField="Size" SortExpression="Size" />
                                    <asp:BoundField HeaderText="Mode" DataField="Mode" SortExpression="Mode" />
                                    <asp:BoundField HeaderText="Owner" DataField="OwnerName" SortExpression="OwnerName" />
                                    <asp:BoundField HeaderText="Last Lr No" DataField="LastLrNo" SortExpression="LastLrNo" />
                                    <asp:BoundField HeaderText="Last Lr Date" DataField="LastLrDate" SortExpression="LastLrDate" />




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
        </div>
    </section>
</asp:Content>






