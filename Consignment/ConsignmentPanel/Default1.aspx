<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Consignment.ConsignmentPanel.Default" %>

<!doctype html>
<html lang="en">
<head>


    <meta name="viewport" content="width=device-width, initial-scale=1, shrink-to-fit=no">
    <link href="https://fonts.googleapis.com/css?family=Roboto:300,400&display=swap" rel="stylesheet">
    <link rel="stylesheet" href="../Content/Login/fonts/icomoon/style.css">
    <link rel="stylesheet" href="../Content/Login/css/owl.carousel.min.css">
    <!-- Bootstrap CSS -->
    <link rel="stylesheet" href="../Content/Login/css/bootstrap.min.css">
    <!-- Style -->
    <link rel="stylesheet" href="../Content/Login/css/style.css">
    <link rel="stylesheet" href="../Content/Common/Login.css">
    <title runat="server" id="title">Login #2</title>
    <link rel="shortcut icon" type="image/x-icon" id="fevicon" href="https://cdn-icons-png.flaticon.com/512/295/295128.png" runat="server" />

    <meta name="theme-color" content="#4A90E2" />
    <link href="../App_Content/SplashSreen/SplashLogo_180x180.png" rel="apple-touch-icon" />
    <link href="../manifest.json" rel="manifest" />
    <script>
        //service worker supported by browser
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('../sw.js');
        };
    </script>

    <style>
        @media only screen and (max-width: 600px) {
            .bgInfo {
                display: none;
            }

            #imgCompanyLogo {
                display: none;
            }
        }
    </style>
</head>
<body>
    <form runat="server">
        <asp:Label ID="lblOTP" runat="server" Style="color: transparent; font-size: 3pt; position: absolute; left: 0px; top: 0px"></asp:Label>
        <asp:Image ID="imgCompanyLogo" runat="server" />
        <div class="d-lg-flex half">
            <div class="bg order-1 order-md-2 bgInfo" style="background-image: url('../App_Content/Icon/LoginBG.jpg');"></div>
            <div class="contents order-2 order-md-1">

                <asp:HiddenField ID="hfUnderMaintenance" runat="server" />
                <div class="container">
                    <div class="row align-items-center justify-content-center" id="dvLogin" runat="server" >
                        <div class="col-md-7">
                            <h3>Login to <strong>Consignment Panel</strong></h3>
                            <div class="form-group first">
                                <label for="username">Company</label>
                                <asp:DropDownList ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="0">--Select--</asp:ListItem>
                                    <asp:ListItem Value="Vayudoot_">VRC</asp:ListItem>
                                    <asp:ListItem Value="Universal_">URL</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlCompany" ID="rfvProduct" runat="server" InitialValue="0"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Select Company to Login
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>

                            <div class="form-group first">
                                <label for="username">Username</label>
                                <asp:TextBox ID="txtUsername" runat="server" CssClass="nospace form-control" placeholder="Enter your Username" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtUsername" ID="rfvCompanyName" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Username
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group last mb-3">
                                <label for="password">Password</label>
                                <asp:TextBox ID="txtPassword" TextMode="Password" runat="server" CssClass="nospace form-control" placeholder="Enter your Password" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtPassword" ID="RequiredFieldValidator1" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="a">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter Password
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>

                            <asp:CheckBox ID="chkRemember" CssClass="form-control" runat="server" Text=" &nbsp; Remember me" />

                            <asp:Button ID="btnSend" OnClick="btnSend_Click" ValidationGroup="a" runat="server" CssClass="btn btn-block btn-primary" Text="Send OTP" />
                        </div>
                    </div>


                    <div class="row align-items-center justify-content-center" id="dvOTP" runat="server">
                        <div class="col-md-7">
                            <h3>OTP sent to your Registered Mobile. <strong>Please Enter OTP</strong></h3>
                            <div class="form-group first">
                                <label for="OTP">OTP</label>
                                <asp:TextBox ID="txtOTP" runat="server" CssClass="digit form-control" MaxLength="5" placeholder="Enter OTP" autocomplete="off"></asp:TextBox>
                                <asp:RequiredFieldValidator ControlToValidate="txtOTP" ID="RequiredFieldValidator3" runat="server"
                                    Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                    ValidationGroup="b">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter OTP
                            </div>
                                </asp:RequiredFieldValidator>
                            </div>

                            <asp:Button ID="btnOTP" OnClick="btnOTP_Click" ValidationGroup="b" runat="server" CssClass="btn btn-block btn-primary" Text="Login" />
                            <asp:Button ID="btnResend" OnClick="btnResend_Click" runat="server" CssClass="btn btn-block btn-danger" Text="Resend OTP" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </form>
    <script src="../Content/Login/js/jquery-3.3.1.min.js"></script>
    <script src="../Content/Login/js/popper.min.js"></script>
    <script src="../Content/Login/js/bootstrap.min.js"></script>
    <script src="../Content/Login/js/main.js"></script>
    <script src="../Content/Login/js/main.js"></script>
    <script src="../Content/Common/Admin.js"></script>
</body>
</html>
