<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="Tripsheet.aspx.cs" Inherits="Consignment.ConsignmentPanel.Tripsheet" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script src="../Scripts/jquery-3.4.1.min.js"></script>
    <script>
        $(document).ready(function () {


            $(".DateFormat").change(function () {
                var fromdate = $(".StartDate").val();
                var todate = $(".EndDate").val();
                if ((fromdate == "") || (todate == "")) {
                    $("#result").html("Please enter two dates");
                    return false
                }
                var dt1 = new Date(fromdate.split('-')[0], fromdate.split('-')[1] - 1, fromdate.split('-')[2]);
               
                var dt2 = new Date(todate.split('-')[0], todate.split('-')[1] - 1, todate.split('-')[2]);
                
                var time_difference = dt2.getTime() - dt1.getTime();
                var result = time_difference / (1000 * 60 * 60 * 24);
                $(".txtNoOfDays").val(result);
            });
        });
    </script>
    <section class="content">
        <div class="box box-default">
            <div class="box-body">
                <div class="row">
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Start Date</label>
                            <asp:TextBox ID="txtStartDate" runat="server" TextMode="Date" autocomplete="off" CssClass="form-control DateFormat StartDate"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>End Date</label>
                            <asp:TextBox ID="txtEndDate" runat="server" TextMode="Date" autocomplete="off" CssClass="form-control DateFormat EndDate"></asp:TextBox>
                        </div>

                    </div>
                      <div class="col-md-3">
                        <div class="form-group">
                            <label>No Of Days</label>
                            <asp:TextBox ID="txtNoOfDays" runat="server" Text="0"  autocomplete="off" CssClass="form-control digit txtNoOfDays"></asp:TextBox>
                        </div>

                    </div>
                    <div class="col-md-3">
                        <div class="form-group">
                            <label>Vehicle No.</label>
                            <asp:DropDownList ID="ddlVehicleNo"   runat="server" CssClass="form-control searchable"></asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlVehicleNo" ID="rfvDriver" runat="server" InitialValue="0"
                                Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Vehicle Number
                            </div>
                            </asp:RequiredFieldValidator>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <asp:Button ID="btnSubmit" CssClass="btn btn-custon-four btn-success" OnClick="btnSubmit_Click" runat="server" Text="Next" />
                </div>
            </div>
        </div>
    </section>
</asp:Content>
