<%@ Page Title="" Language="C#" MasterPageFile="~/ConsignmentPanel/_Consignment.Master" AutoEventWireup="true" CodeBehind="ListPOD.aspx.cs" Inherits="Consignment.ConsignmentPanel.ListPOD" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .GridViewStyle td {
    white-space: nowrap !important;   /* text wrap band */
    padding: 5px !important;
    font-size: 12px;
}

.GridViewStyle th {
    white-space: nowrap !important;
    padding: 6px !important;
}

.RowStyle {
    height: 30px !important;   /* row height control */
}
    </style>
   
    <section class="content-header">
        <div class="container-fluid">
            <div class="row mb-2">
                <div class="col-sm-6">
                    <h1>
                        <asp:Label ID="lblHeading" runat="server"></asp:Label>

                        <asp:ImageButton ID="imgBtnExcel" OnClick="imgBtnExcel_Click"
                            ToolTip="Excel" Width="20"
                            ImageUrl="https://upload.wikimedia.org/wikipedia/commons/thumb/3/34/Microsoft_Office_Excel_%282019%E2%80%93present%29.svg/826px-Microsoft_Office_Excel_%282019%E2%80%93present%29.svg.png"
                            runat="server" />

                        |
                        <asp:ImageButton ID="imgBtnPdf" OnClick="imgBtnPdf_Click"
                            ToolTip="PDF" Width="20"
                            ImageUrl="https://cdn4.iconfinder.com/data/icons/file-extensions-1/64/pdfs-512.png"
                            runat="server" />
                    </h1>

                    Total Record's :
                    <asp:Literal ID="litrecordcount" runat="server" Text="0" />
                </div>

                <div class="col-sm-6">
                    <ol class="breadcrumb float-sm-right">
                        <li class="breadcrumb-item"><a href="Dashboard.aspx">Home</a></li>
                        <li class="breadcrumb-item active">POD List</li>
                    </ol>
                </div>
            </div>
        </div>
    </section>

  
    <section class="content">
        <div class="container-fluid">
            <div class="box box-default">

                <div class="box-body">
                    <div class="row mb-3">

    <div class="col-md-3">
        <label>LR No</label>

        <asp:TextBox ID="txtSearchLRNo"
            runat="server"
            CssClass="form-control"
            placeholder="Enter LR No"></asp:TextBox>
    </div>

    <div class="col-md-3">
        <label>Concerned Branch</label>

       <asp:DropDownList ID="ddlConcernedBranch"
    runat="server"
    CssClass="form-control"
    AppendDataBoundItems="true">

    <asp:ListItem Value="">All</asp:ListItem>

</asp:DropDownList>
    </div>

    <div class="col-md-2">
        <label>&nbsp;</label>

        <asp:Button ID="btnSearch"
            runat="server"
            Text="Search"
            CssClass="btn btn-primary form-control"
            OnClick="btnSearch_Click" />
    </div>

    <div class="col-md-2">
        <label>&nbsp;</label>

        <asp:Button ID="btnReset"
            runat="server"
            Text="Reset"
            CssClass="btn btn-default form-control"
            OnClick="btnReset_Click" />
    </div>

</div>
                 
                   <div class="row">

    <div class="col-md-4">

        <a href="AddPODBulk.aspx" id="btnAdd" runat="server"
            class="btn btn-success"
            style="color:white;">

            Bulk POD Upload
            <span class="fa fa-upload"></span>
        </a>

        <a href="UpdatePODRecDateBulk.aspx" id="btnAdd1" runat="server"
            class="btn btn-primary"
            style="color:white; margin-left:5px;">

            POD Rec Date Update
            <span class="fa fa-calendar"></span>
        </a>

    </div>

    <div class="col-md-5">
    </div>

    <div class="col-md-3">

        <asp:DropDownList ID="ddlPageSize"
            runat="server"
            CssClass="form-control"
            AutoPostBack="True"
            OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">

            <asp:ListItem Value="10">10 Record(s)</asp:ListItem>
            <asp:ListItem Value="25">25 Record(s)</asp:ListItem>
            <asp:ListItem Value="50">50 Record(s)</asp:ListItem>
            <asp:ListItem Value="100">100 Record(s)</asp:ListItem>
            <asp:ListItem Value="200">200 Record(s)</asp:ListItem>

        </asp:DropDownList>

    </div>

</div>

                   
                    <div style="overflow-x:auto; white-space:nowrap;">
                        <asp:GridView ID="gvReport" runat="server"
    CssClass="GridViewStyle"
    AutoGenerateColumns="False"
    AllowPaging="True"
    AllowSorting="True"
    DataKeyNames="Id"
    OnPageIndexChanging="gvReport_PageIndexChanging"
    OnRowCommand="gvReport_RowCommand"
    OnSorting="gvReport_Sorting"
    RowStyle-CssClass="RowStyle"
    AlternatingRowStyle-CssClass="RowStyle"
    HeaderStyle-CssClass="HeaderStyle"
    PagerStyle-CssClass="PagerStyle"
    ShowHeaderWhenEmpty="True"
    CellPadding="3"
    GridLines="Both"
    BorderStyle="Solid"
    BorderWidth="1px">

    <Columns>

        <asp:TemplateField HeaderText="S.No." HeaderStyle-Width="40px">
            <ItemTemplate>
                <%# Container.DataItemIndex + 1 %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:BoundField HeaderText="LR Date" DataField="LRDate" />
        <asp:BoundField HeaderText="Service Month" DataField="ServiceMonth" />
        <asp:BoundField HeaderText="LR No" DataField="LRNo" />
     <asp:TemplateField HeaderText="Consignor Name">
    <ItemTemplate>
        <div style="max-width:150px; overflow:hidden; text-overflow:ellipsis; white-space:nowrap;"
             title='<%# Eval("ConsignorName") %>'>
            <%# Eval("ConsignorName") %>
        </div>
    </ItemTemplate>
</asp:TemplateField>
        <asp:BoundField HeaderText="Origin" DataField="Origin" />
        <asp:BoundField HeaderText="Destination" DataField="Destination" />
        <asp:BoundField HeaderText="Vehicle No" DataField="VehicleNo" />
        <asp:BoundField HeaderText="Pkg." DataField="PackageCount" />
        <asp:BoundField HeaderText="Vehicle Owner" DataField="VehicleOwner" />
        <asp:BoundField HeaderText="Vehicle Type" DataField="VehicleType" />
        <asp:BoundField HeaderText="Exceptional" DataField="ExceptionalFlag" />
        <asp:BoundField HeaderText="Concerned Branch" DataField="ConcernedBranch" />
        <asp:BoundField HeaderText="Created Date" DataField="CreatedDate" />

      
        <asp:TemplateField HeaderStyle-Width="30px">
            <ItemTemplate>
                <a href='AddPOD.aspx?id=<%#Eval("Id") %>'>
                    <img src="../App_Content/icon/edit.png" width="20" />
                </a>
            </ItemTemplate>
        </asp:TemplateField>

      
        <asp:TemplateField HeaderStyle-Width="30px">
            <ItemTemplate>
                <asp:ImageButton ID="btnDelete"
                    runat="server"
                    Width="20"
                    ImageUrl="../App_Content/icon/del.png"
                    CommandName="IsDelete"
                    CommandArgument='<%#Eval("Id") %>'
                    OnClientClick="return confirm('Delete this record?')" />
            </ItemTemplate>
        </asp:TemplateField>

    </Columns>

    <EmptyDataTemplate>
        <div class="EmptyDataTemplate">
            No Record Found !
        </div>
    </EmptyDataTemplate>

</asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </section>

</asp:Content>