<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Consignment.ConsignmentPanel.Default" %>

<!DOCTYPE html>
<html lang="en">
<head>
    <title runat="server" id="title">Login #2</title>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <link rel="shortcut icon" type="image/x-icon" id="fevicon" href="https://cdn-icons-png.flaticon.com/512/295/295128.png" runat="server" />
    <!--===============================================================================================-->
    <link rel="icon" type="image/png" href="../Content/LoginNew/images/icons/favicon.ico" />
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/vendor/bootstrap/css/bootstrap.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/fonts/font-awesome-4.7.0/css/font-awesome.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/fonts/iconic/css/material-design-iconic-font.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/vendor/animate/animate.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/vendor/css-hamburgers/hamburgers.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/vendor/animsition/css/animsition.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/vendor/select2/select2.min.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/vendor/daterangepicker/daterangepicker.css">
    <!--===============================================================================================-->
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/css/util.css">
    <link rel="stylesheet" type="text/css" href="../Content/LoginNew/css/main.css">
    <!--===============================================================================================-->



    <meta name="theme-color" content="#4A90E2" />
    <link href="../App_Content/SplashSreen/SplashLogo_180x180.png" rel="apple-touch-icon" />
    <link href="../manifest.json" rel="manifest" />
    <script>
        //service worker supported by browser
        if ('serviceWorker' in navigator) {
            navigator.serviceWorker.register('../sw.js');
        };
    </script>
</head>
<body>
    <form runat="server">
        <asp:HiddenField ID="hfUnderMaintenance" runat="server" />
        <div class="limiter">
            <div class="container-login100" style="background-image: url('../App_Content/Icon/LoginBG.jpg');">
                <div class="wrap-login100">
                    <div class="login100-form validate-form">


                        <span class="login100-form-title p-b-34 p-t-27">BMS LOGIN
                        </span>

                        <div class="container">
                            <div class="row align-items-center justify-content-center" id="dvLogin" runat="server">
                                <div class="col-md-12">
                                   
                                    <div class="form-group first">
                                        <label for="username">Company</label>
                                        <asp:DropDownList ID="ddlCompany" AutoPostBack="true" OnSelectedIndexChanged="ddlCompany_SelectedIndexChanged" CssClass="form-control" runat="server">
                                            <asp:ListItem Value="0">--Select--</asp:ListItem>
                                            <asp:ListItem Value="Vayudoot_">VRC</asp:ListItem>
                                            <asp:ListItem Value="Universal_">URL</asp:ListItem>
                                            <%--<asp:ListItem Value="VRPL_">VRPL</asp:ListItem>--%>
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

                                    <asp:CheckBox ID="chkRemember"  runat="server" Text="Remember me" />

                                   
                                    <asp:Button ID="btnSend" style="cursor:pointer" OnClick="btnSend_Click" ValidationGroup="a" runat="server" CssClass="btn btn-block btn-primary" Text="Send OTP" />
                                </div>
                            </div>


                            <div class="row align-items-center justify-content-center" id="dvOTP" runat="server">
                                <div class="col-md-7">
                                    <%--<h3>OTP sent to your Registered Mobile. <strong>Please Enter OTP</strong></h3>--%>
                                    <div class="form-group first">
                                        
                                        <asp:TextBox ID="txtOTP" runat="server" CssClass="digit form-control" MaxLength="5" placeholder="Enter OTP" autocomplete="off"></asp:TextBox>
                                        <asp:RequiredFieldValidator ControlToValidate="txtOTP" ID="RequiredFieldValidator3" runat="server"
                                            Display="Dynamic" ErrorMessage="Please Enter Name !" SetFocusOnError="True"
                                            ValidationGroup="b">
                        <div class="alert alert-danger alert-mg-b" role="alert">
                                Please Enter OTP
                            </div>
                                        </asp:RequiredFieldValidator>
                                    </div>

                                    <asp:Button ID="btnOTP" style="cursor:pointer" OnClick="btnOTP_Click" ValidationGroup="b" runat="server" CssClass="btn btn-block btn-primary" Text="Login" />
                                    <asp:Button ID="btnResend" style="cursor:pointer" OnClick="btnResend_Click" runat="server" CssClass="btn btn-block btn-danger" Text="Resend OTP" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <asp:Label ID="lblOTP" runat="server" Style="color: transparent; font-size: 3pt; position: absolute; left: 0px; top: 0px;z-index:9999999999999"></asp:Label>
        <div id="dropDownSelect1"></div>

        <!--===============================================================================================-->
        <script src="../Content/LoginNew/vendor/jquery/jquery-3.2.1.min.js"></script>
        <!--===============================================================================================-->
        <script src="../Content/LoginNew/vendor/animsition/js/animsition.min.js"></script>
        <!--===============================================================================================-->
        <script src="../Content/LoginNew/vendor/bootstrap/js/popper.js"></script>
        <script src="../Content/LoginNew/vendor/bootstrap/js/bootstrap.min.js"></script>
        <!--===============================================================================================-->
        <script src="../Content/LoginNew/vendor/select2/select2.min.js"></script>
        <!--===============================================================================================-->
        <script src="../Content/LoginNew/vendor/daterangepicker/moment.min.js"></script>
        <script src="../Content/LoginNew/vendor/daterangepicker/daterangepicker.js"></script>
        <!--===============================================================================================-->
        <script src="../Content/LoginNew/vendor/countdowntime/countdowntime.js"></script>
        <!--===============================================================================================-->
        <script src="../Content/LoginNew/js/main.js"></script>
    </form>
</body>
</html>
