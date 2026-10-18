<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="TripsheetLog.aspx.cs" Inherits="Consignment.ConsignmentPanel.TripsheetLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <%-- <asp:UpdatePanel ID="updatepnl" runat="server">
        <ContentTemplate>--%>
    <style>
        .modal.in .modal-dialog {
            -webkit-transform: translate(0,0);
            -ms-transform: translate(0,0);
            -o-transform: translate(0,0);
            transform: translate(0,0);
            width: 180vh !important;
        }
    </style>
    <script>
        $(document).ready(function () {
            //$(".DivLoadType").hide();
            //if ($(".ddlVehicleType").val() == "REEFER") {
            //    $(".DivLoadType").show();
            //} else {
            //    $(".DivLoadType").hide();
            //}

            //$(document).on("change", ".ddlVehicleType", function (e) {
            //    if ($(this).val() == "REEFER") {
            //    $(".DivLoadType").show();
            //} else {
            //    $(".DivLoadType").hide();
            //}
            //});
            //window.open("http://www.stackoverflow.com");
            <%--var session = '<%=Session["TripsheetReportPath"] != null%>';
            if (session == 'True') {
                var value = '<%=Session["TripsheetReportPath"] %>';
                window.open(value, '_blank'); 
                alert(value);
            }--%>
            
            <%--if('<%= Session["TripsheetReportPath"].ToString() %>' != null)
            {
                var someSession = '<%= Session["TripsheetReportPath"].ToString() %>';
                alert(someSession);
            }--%>

            $(".DateFormat").change(function () {
                var fromdate = $(".StartDate").val();
                var todate = $(".EndDate").val();
                if ((fromdate == "") || (todate == "")) {
                    $("#result").html("Please enter two dates");
                    return false
                }
                var dt1 = new Date(fromdate.split('/')[2],fromdate.split('/')[1]-1,fromdate.split('/')[0]);
                var dt2 = new Date(todate.split('/')[2],todate.split('/')[1]-1,todate.split('/')[0]);
                var time_difference = dt2.getTime() - dt1.getTime();
                var result = time_difference / (1000 * 60 * 60 * 24);
                $(".NoOfDays").val(result);
            });
        });
    </script>

    <section class="content-header">
        <h1>
            <asp:Label ID="lblHeading" runat="server" Style="text-transform: uppercase" Text=""></asp:Label>
        </h1>
        <ol class="breadcrumb">
        </ol>
    </section>
    <asp:HiddenField ID="hdnTripsheetNo" runat="server" />
    <section class="content">
        <div class="box box-default">
           
            <div class="box-body">
                <div class="row">
                    
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Start Date</label>
                           <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" autocomplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>End Date</label>
                           <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" autocomplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>No. of Days</label>
                            <asp:TextBox ID="txtNoOfDays" runat="server" autocomplete="off" CssClass="form-control NoOfDays NotBlank" Text="0"></asp:TextBox>
                            
                          
                        </div>
                    </div>
                      <div class="col-md-3">
                        <div class="form-group">
                            <label>Vehicle No.</label>
                            <asp:DropDownList ID="ddlVehicleNo"  runat="server" CssClass="form-control searchable"></asp:DropDownList>
                         
                        </div>
                    </div>
                </div>
                <div class="row">
                  
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Vehicle Size</label>
                            <asp:TextBox ID="txtVehicleSize" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator ControlToValidate="txtVehicleSize" ID="RequiredFieldValidator4" runat="server"
                                Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Vehicle Size
                            </div>
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Driver</label>
                            <asp:DropDownList ID="ddlDriver" ClientIDMode="Static" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                        
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Vehicle Type</label>
                            <asp:DropDownList ID="ddlVehicleType"  ClientIDMode="Static" runat="server" CssClass="form-control ddlVehicleType">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="LINE">LINE</asp:ListItem>
                                <asp:ListItem Value="KOLKATA LOCAL">KOLKATA LOCAL</asp:ListItem>
                                <asp:ListItem Value="SOUTH LOCAL">SOUTH LOCAL</asp:ListItem>
                                <asp:ListItem Value="REEFER">REEFER</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlVehicleType" ID="RequiredFieldValidator6" runat="server" InitialValue="0"
                                Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Vehicle Type
                            </div>
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>

                   
                </div>
                <div class="row">
                    <div class="col-md-3 DivLoadType" runat="server" id="DivLoadType" style="display: none">
                        <div class="form-group">
                            <label>Load Type</label>
                            <asp:DropDownList ID="ddlLoadType" ClientIDMode="Static" runat="server" CssClass="form-control">
                                <asp:ListItem Value="">--Select--</asp:ListItem>
                                <asp:ListItem Value="REFFER LOAD">REEFER LOAD</asp:ListItem>
                                <asp:ListItem Value="DRY LOAD">DRY LOAD</asp:ListItem>
                                <asp:ListItem Value="EMPTY">EMPTY</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div class="col-md-3" style="display: none">
                        <div class="form-group">
                            <label>Guarantor</label>
                            <asp:TextBox ID="txtGuarantor" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                   
                     
                </div>

                 <div class="row">
                    <div style="overflow-x: scroll; margin-top: 1%; width: 100%" id="DvLoadingUnloadingHamali" runat="server">
                        <table class="ListPageArea">
                            <tr>
                                <td valign="top" colspan="2">
                                    <asp:GridView ID="gvReport" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                                        AllowPaging="True"  DataKeyNames="ID" Width="100%"
                                        AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3"
                                        ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                                <ItemTemplate>
                                                    <%#Container.DataItemIndex+1 %>
                                                </ItemTemplate>

                                                <HeaderStyle Width="35px"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField Visible="false" HeaderText="ID" DataField="ID" SortExpression="ID" />
                                            <asp:TemplateField HeaderText="Date" ItemStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <div style="text-align: left; padding-left: 10px">
                                                        Start Date :
                                                    <asp:Label ID="Labela" runat="server" Text='<%# Eval("StartDate")%>'></asp:Label><br />
                                                        Arrival Date :
                                                    <asp:Label ID="Labelb" runat="server" Text='<%# Eval("EndDate")%>'></asp:Label><br />
                                                        Unloading Date :
                                                    <asp:Label ID="Labelc" runat="server" Text='<%# Eval("UnloadingDate")%>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <%--<asp:BoundField HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" />
                                            <asp:BoundField HeaderText="Arrival Date" DataField="EndDate" SortExpression="EndDate" />
                                            <asp:BoundField HeaderText="Unloading Date" DataField="UnloadingDate" SortExpression="UnloadingDate" />--%>
                                            <asp:BoundField HeaderText="From" DataField="Origin" SortExpression="Origin" />
                                            <asp:BoundField HeaderText="To" DataField="Destination" SortExpression="Destination" />
                                            <asp:TemplateField HeaderText="LR No" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("LrNo1")%>'></asp:Label><br />
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("LrNo2")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="LR Date" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("LrDate1")%>'></asp:Label><br />
                                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("LrDate2")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Party" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("PartyId1")%>'></asp:Label><br />
                                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("PartyId2")%>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Loading Hamali" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label566" runat="server" Text='<%# Eval("LoadingHamali")%>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Unloading Hamali" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label544" runat="server" Text='<%# Eval("UnloadingHamali")%>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Trip Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label533" runat="server" Text='<%# Eval("TripType")%>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Load Type" HeaderStyle-HorizontalAlign="Left">
                                                <ItemTemplate>
                                                    <asp:Label ID="Label5344" runat="server" Text='<%# Eval("LoadType")%>'></asp:Label><br />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../App_Content/icon/icn_trash.png"
                                                        AlternateText="Delete" ToolTip="Delete this record" CommandName="IsDelete" CommandArgument='<%#Eval("ID") %>'
                                                        OnClientClick='return confirm("Are You Sure To Delete This Record?")' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="25px"></HeaderStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderStyle-Width="25px">
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="../App_Content/icon/edit_16x16.png"
                                                        AlternateText="Edit" ToolTip="Edit this record" CommandName="isEdit" CommandArgument='<%#Eval("ID") %>'
                                                        OnClientClick='return confirm("Are You Sure To Edit This Record?")' />
                                                </ItemTemplate>
                                                <HeaderStyle Width="25px"></HeaderStyle>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EmptyDataTemplate>
                                            <div class="EmptyDataTemplate">
                                                No Record Found !
                                            </div>
                                        </EmptyDataTemplate>
                                        <RowStyle CssClass="RowStyle" />
                                        <PagerStyle CssClass="PagerStyle" ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle CssClass="HeaderStyle" BackColor="#333" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#CCCCCC" />
                                        <PagerSettings Position="Bottom" />
                                        <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                <section class="content">




        <div class="box box-default">
          
            <div class="box-body">
               
                <hr />

                <div class="row">
                    <div class="col-md-3">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>EXPENSES</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group" style="background-color: #808080; color: white; text-align: center">
                                    <label>Description</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group" style="background-color: #808080; color: white; text-align: center">
                                    <label>Amount</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Total Diesel (Ltr)</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTotalDiesel" runat="server" autocomplete="off" Text="0" CssClass="form-control NotBlank"></asp:TextBox>

                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtDieselAmount" runat="server" Text="0" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Extra Diesel</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtExtraDiesel" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Reefer Diesel 1</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtReeferDiesel1" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Reefer Diesel 2</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtReeferDiesel2" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Toll</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtToll" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Escort/Naka</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtEscortNaka" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Border RTO</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBorderRTO" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Check Post</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCheckPost" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Mamul</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtMamul" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Weightment Slip</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtWeightmentSlip" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Tyre Repair</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTyreRepair" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Mech Repair</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtMechRepair" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Wire Cutting</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtWireCutting" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Parking</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtParking" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Union Receipt</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtUnionReceipt" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Police Expenses</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtPoliceExp" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Accident Expenses</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtAccidentExp" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Guide 1</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtGuide1" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Guide 2</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtGuide2" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Loading Hamali</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtLoadingHamali" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Unloading Hamali</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtUnloadingHamali" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Medical Expenses</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtMedicalExp" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <div class="form-group">
                                    <label>Batta/Travelling</label>
                                </div>
                            </div>
                            <div class="col-md-8">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBattaTravelling" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <%--  <div class="row" style="display:none">
                                    <div class="col-md-4">
                                    </div>
                                    <div class="col-md-8">
                                        <button type="button" id="btnLoadingUnloading" class="form-control btn btn-danger btnLoadingUnloading">Loading & Unloading Hamali</button>
                                    </div>
                                </div>--%>
                    </div>

                    <div class="col-md-9">
                        <div class="row">
                            <div class="col-md-12">
                                <div class="form-group">
                                    <label>RECEIPTS</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <div class="form-group" style="background-color: #808080; color: white; text-align: center">
                                    <label>Cash Advance From Branch</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" style="background-color: #808080; color: white; text-align: center">
                                    <label>Date</label>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group" style="background-color: #808080; color: white; text-align: center">
                                    <label>Amount</label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch1" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate1" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt1" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch2" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate2" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt2" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch3" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate3" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt3" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    </div> 
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch4" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate4" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt4" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch5" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate5" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt5" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch6" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate6" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt6" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch7" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate7" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt7" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch8" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate8" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt8" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch9" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate9" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt9" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlBranch10" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchDate10" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBranchAmt10" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cash Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCash1" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashDate1" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashAmt1" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cash Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCash2" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashDate2" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashAmt2" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cash Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCash3" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashDate3" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashAmt3" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cash Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCash4" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashDate4" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashAmt4" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Cash Advance From</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:DropDownList ID="ddlCash5" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashDate5" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtCashAmt5" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total To-Pay Freight Amount</label>
                                </div>
                            </div>
                            <div class="col-md-7">
                            </div>

                            <div class="col-md-3">
                                <div class="form-group">
                                    <asp:TextBox ID="txtToPayAmount" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                    
                                </div>
                            </div>
                        </div>
                        <br />
                        <br />
                        <br />
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total Receipts</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTotalReceipts" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank txtTotalReceipts"></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>Total Expenses</label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtTotalExp" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank txtTotalExp"></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-2">
                                <div class="form-group">
                                    <label>
                                        <asp:Label ID="balance" CssClass="balance" runat="server" Text=""></asp:Label></label>
                                </div>
                            </div>
                            <div class="col-md-4">
                                <div class="form-group">
                                    <asp:TextBox ID="txtBalance" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank txtBalance"></asp:TextBox>
                                    
                                </div>
                            </div>
                            <div class="col-md-6">
                            </div>
                        </div>
                    </div>
                </div>



                <div runat="server" visible="false">
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>KA RTO</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtKaRTO" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>TN RTO</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtTnRTO" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>AP RTO</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtApRTO" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>MH RTO</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtMhRTO" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>KL RTO</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtKlRTO" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Loading Hamali</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtELoadingHamali" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To-Pay Fright</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranchFrieght1" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchFrieghtDate1" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchFrieghtAmt1" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Unloading Hamali</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtEUnloadingHamali" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>
                        <div class="col-md-5">
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtFrieghtLrNo1" placeholder="LR No" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtFrieghtParty1" placeholder="Party" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-2">
                            <div class="form-group">
                                <label>To-Pay Fright</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranchFrieght2" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchFrieghtDate2" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchFrieghtAmt2" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtFrieghtLrNo2" placeholder="LR No" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtFrieghtParty2" placeholder="Party" runat="server" autocomplete="off" CssClass="form-control"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Claim For Delay</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranchClaimForDelay" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchClaimForDelayDate" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchClaimForDelayAmt" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Claim For Damage</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranchClaimForDamage" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchClaimForDamageDate" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchClaimForDamageAmt" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                    <div class="row">

                        <div class="col-md-2">
                            <div class="form-group">
                                <label>Diesel Claim</label>
                            </div>
                        </div>
                        <div class="col-md-3">
                            <div class="form-group">
                                <asp:DropDownList ID="ddlBranchDieselClaim" runat="server" CssClass="form-control Select2Box"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchDieselClaimDate" placeholder="dd/mm/yyyy" runat="server" autocomplete="off" CssClass="form-control Datet"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtBranchDieselClaimAmt" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumRec"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>



                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>Overload Diesel</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtOverloadDiesel" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>

                    </div>

                    <div class="row">
                        <div class="col-md-1">
                            <div class="form-group">
                                <label>A.C. Diesel</label>
                            </div>
                        </div>
                        <div class="col-md-2">
                            <div class="form-group">
                                <asp:TextBox ID="txtACDiesel" Text="0" runat="server" autocomplete="off" CssClass="form-control NotBlank SumExp"></asp:TextBox>
                                
                            </div>
                        </div>
                    </div>
                </div>
                <%--<div class="row">
                            <div class="col-md-12">
                                <div style="overflow-x: scroll; margin-top: 1%; width: 100%" id="DvLoadingUnloadingHamali">
                                    <table class="ListPageArea">
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <asp:GridView ID="gvReport" runat="server" CssClass="GridViewStyle" AutoGenerateColumns="False"
                                                    AllowPaging="True" DataKeyNames="ID" Width="100%" OnRowCommand="gvReport_RowCommand"
                                                    AllowSorting="True" RowStyle-CssClass="rowHover" ShowHeaderWhenEmpty="True" CellPadding="3"
                                                    ForeColor="Black" GridLines="Both" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="35px">
                                                            <ItemTemplate>
                                                                <%#Container.DataItemIndex+1 %>
                                                            </ItemTemplate>

                                                            <HeaderStyle Width="35px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                        <asp:BoundField Visible="false" HeaderText="ID" DataField="ID" SortExpression="ID" />
                                                        <asp:BoundField HeaderText="Start Date" DataField="StartDate" SortExpression="StartDate" />
                                                        <asp:BoundField HeaderText="Arrival Date" DataField="EndDate" SortExpression="EndDate" />
                                                        <asp:BoundField HeaderText="Unloading Date" DataField="UnloadingDate" SortExpression="UnloadingDate" />
                                                        <asp:BoundField HeaderText="From" DataField="Origin" SortExpression="Origin" />
                                                        <asp:BoundField HeaderText="To" DataField="Destination" SortExpression="Destination" />
                                                        <asp:TemplateField HeaderText="LR No" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("LrNo1")%>'></asp:Label><br />
                                                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("LrNo2")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="LR Date" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("LrDate1")%>'></asp:Label><br />
                                                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("LrDate2")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Party" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label5" runat="server" Text='<%# Eval("PartyId1")%>'></asp:Label><br />
                                                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("PartyId2")%>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Loading Hamali" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtLoadingHamli" runat="server" Text='<%# Eval("LoadingHamali")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Unloading Hamali" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtunloadingHamli" runat="server" Text='<%# Eval("UnloadingHamali")%>'></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Trip Type" HeaderStyle-HorizontalAlign="Left">
                                                            <ItemTemplate>
                                                                <asp:Label ID="Label544" runat="server" Text='<%# Eval("TripType")%>'></asp:Label><br />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderStyle-Width="25px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="../App_Content/icon/verify.png"
                                                                    AlternateText="Save" ToolTip="Save this record" CommandName="Save" CommandArgument='<%#Eval("ID") %>' />
                                                            </ItemTemplate>
                                                            <HeaderStyle Width="25px"></HeaderStyle>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <div class="EmptyDataTemplate">
                                                            No Record Found !
                                                        </div>
                                                    </EmptyDataTemplate>
                                                    <RowStyle CssClass="RowStyle" />
                                                    <PagerStyle CssClass="PagerStyle" ForeColor="Black" HorizontalAlign="Center" BackColor="#999999" />
                                                    <FooterStyle BackColor="#CCCCCC" />
                                                    <HeaderStyle CssClass="HeaderStyle" BackColor="#333" Font-Bold="True" ForeColor="White" />
                                                    <AlternatingRowStyle CssClass="AltRowStyle" BackColor="#CCCCCC" />
                                                    <PagerSettings Position="Bottom" />
                                                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                        </div>--%>


                <br />
              

            </div>
    </section>
                <div>
                    <asp:Button ID="btnSubmitHead" ValidationGroup="a" CssClass="btn btn-custon-four btn-success" runat="server" Text="Save" />
                  
                </div>
            </div>
        </div>
    </section>
    <%--        </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
