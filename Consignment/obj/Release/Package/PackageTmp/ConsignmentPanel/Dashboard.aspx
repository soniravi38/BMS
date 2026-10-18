<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="Consignment.ConsignmentPanel.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content">
        <div class="container-fluid">
            <!-- Small boxes (Stat box) -->
            <div class="row">
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-info">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblTotalLR" runat="server" Text="10"></asp:Label></h3>
                            <p>Total LR</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-card"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-6">
                    <!-- small box -->
                    <div class="small-box bg-success">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblTodayLR" runat="server" Text="10"></asp:Label></h3>
                            <p>Today LR</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-cash"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-6" id="dvConsignor" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-warning">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblConsignor" runat="server" Text="10"></asp:Label></h3>
                            <p>Consignor</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-paper-airplane"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <!-- ./col -->
                <div class="col-lg-3 col-6" id="dvConsignee" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-danger">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblConsignee" runat="server" Text="10"></asp:Label></h3>
                            <p>Consignee</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-share"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>


                <div class="col-lg-3 col-6" id="dvStation" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-blue">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblStation" runat="server" Text="10"></asp:Label></h3>
                            <p>Station</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-chevron-left"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvVehicle" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-body">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblVehicle" runat="server" Text="10"></asp:Label></h3>
                            <p>Vehicle</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-pie-graph"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvBranch" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-cyan">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblBranch" runat="server" Text="10"></asp:Label></h3>
                            <p>Branch</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-pie-graph"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvUnit" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-fuchsia">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblUnit" runat="server" Text="10"></asp:Label></h3>
                            <p>Unit</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-pie-graph"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvSize" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-gradient-cyan">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblSize" runat="server" Text="10"></asp:Label></h3>
                            <p>Size</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-search"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvDiesel" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-gradient-indigo">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblDiesel" runat="server" Text="10"></asp:Label></h3>
                            <p>Total Diesel Entry</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-gear-a"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvCashEntry" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-gradient-purple">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblCash" runat="server" Text="10"></asp:Label></h3>
                            <p>Total Cash Entry</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-quote"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                </div>
                <div class="col-lg-3 col-6" id="dvVehicleSize" runat="server">
                    <!-- small box -->
                    <div class="small-box bg-green">
                        <div class="inner">
                            <h3>
                                <asp:Label ID="lblVehicleSize" runat="server" Text="10"></asp:Label></h3>
                            <p>Total Vehicle Size Entry</p>
                        </div>
                        <div class="icon">
                            <i class="ion ion-bag"></i>
                        </div>
                        <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                    </div>
                    </div>
                   
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box bg-danger">
                            <div class="inner">
                                <h3>
                                    <asp:Label ID="lblCurrentIP" runat="server" Text="10"></asp:Label></h3>
                                <p>Current Login IP</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-pie-graph"></i>
                            </div>
                            <a href="#" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>

                    </div>
                </div>
            </div>
            <!-- ./col -->
        </div>
        <!-- /.row -->
        <!-- Main row -->

        <!-- /.row (main row) -->
        </div>
        <!-- /.container-fluid -->
    </section>
</asp:Content>
