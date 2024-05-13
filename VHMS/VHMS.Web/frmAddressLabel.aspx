<%@ Page Title="AddressLabel" Language="C#" MasterPageFile="~/VHMSReportPage.master"
    AutoEventWireup="true" CodeFile="frmAddressLabel.aspx.cs" Inherits="frmAddressLabel" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <%--  <link href="css/print.css" rel="stylesheet" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>--%>
    <style type="text/css">
        #VHMSWebContent_CRDischargeSummaryReport__UI_mb, #VHMSWebContent_CRDischargeSummaryReport__UI_bc
        {
            height: inherit !important;
            top: 0px !important;
            left: 0px !important;
        }
        .BigCheckBox input
        {
            width: 25px;
            height: 25px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <asp:HiddenField ID="hdnPatientID" runat="server" />
    <div class="container-wrapper hidden">
        <div class="content-header">
            <h1>
                Address Label
            </h1>
            <ol class="breadcrumb">
                <li><a href="#"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Address Label</li>
            </ol>
            <br />
            <br />
            <div class="input-group-append">
                <div class="form-group col-md-2 ">
                    <label>
                        Search Customer Mobile No</label>
                </div>
                <div class="form-group col-md-2 ">
                    <asp:TextBox ID="txtAddressLabel" CssClass="form-control" runat="server" ReadOnly></asp:TextBox>
                </div>
                <div class="checkbox col-md-4" style="margin-top: -38px; margin-left: 440px; font-size: 23px;">
                    <asp:CheckBox ID="chkPartyCode" runat="server" AutoPostBack="true" Text="Phone No" OnCheckedChanged="chkPartyCode_CheckedChanged"
                        CssClass="BigCheckBox" />
                    <div class="form-group col-md-1" style="display: none;">
                        <asp:Button ID="btnGenrate" CssClass="btn btn-outline-primary" runat="server" Text="Generate"
                            OnClick="btnGenrate_Click" />
                    </div>
                    <div class="form-group col-md-1">
                        <asp:Button ID="btnPrint" CssClass="btn btn-outline-primary" style="margin-left: 170px;" runat="server" Text="Print"
                            OnClick="btnPrint_Click" />
                    </div>
                </div>
            </div>
            <br />
            <br />
        </div>
        <div class="content" id="imageOutput" style="margin-left: 200px">
            <div class="row" style="display: none;">
                <div class="form-group col-md-2">
                    <label style="font-size: 16px; font-family: arial;">
                        To</label>
                    <asp:Panel ID="PlaceHolder1" runat="server">
                    </asp:Panel>
                    <asp:Label ID="lblName" runat="server" Text="" Style="margin-left: 10%; font-size: 14px;
                        font-family: arial;"></asp:Label>
                    <br />
                    <asp:Label ID="lblAddressLabel" runat="server" Text="" Style="margin-left: 10%; font-size: 12px;
                        font-family: arial;"></asp:Label>
                </div>
            </div>
            <div class="content" id="imageOutputs" style="display: none;">
                <div class="row" style="padding-top: 30px;">
                    <div class="form-group col-md-2">
                        <label style="font-size: 16px; font-family: arial;">
                            From</label>
                        <asp:Panel ID="PlaceHolder2" runat="server">
                        </asp:Panel>
                        <asp:Label ID="lblCompanyName" runat="server" Text="" Style="margin-left: 10%; font-size: 14px;
                            font-family: arial;"></asp:Label>
                        <br />
                        <asp:Label ID="lblCompanyAddress" runat="server" Text="" Style="margin-left: 10%;
                            font-size: 11px; font-family: arial;"></asp:Label>
                    </div>
                </div>
                <%--<div class="form-group col-md-1" style="margin-left: 50%; margin-top: -60px; font-size:14px; font-family:arial;">
                     <asp:Label ID="lblGrossWt" style="font-weight: bold;" runat="server" Text="GW:"></asp:Label><br/>
                    <asp:Label ID="lblStoneWt" style="font-weight: bold;" runat="server" Text="SW:"></asp:Label><br/>   
                    <asp:Label ID="lblNetWt" style="font-weight: bold;" runat="server" Text="NW:"></asp:Label>
                </div>--%>
            </div>
            <CR:CrystalReportViewer ID="CustomerAddress" ToolPanelView="None" runat="server"
                Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
                HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
            <CR:CrystalReportViewer ID="SupplierAddress" ToolPanelView="None" runat="server"
                Width="100%" HasCrystalLogo="False" HasToggleGroupTreeButton="False" HasExportButton="False"
                HasPrintButton="True" HasDrillUpButton="False" HasDrilldownTabs="False" HasRefreshButton="False" />
        </div>
    </div>
    <script type="text/javascript">
        $(document).ready(function () {
            pLoadingSetup(false);

            pLoadingSetup(true);
        });

        function PrintPanel() {
           <%-- var panel = document.getElementById("<%=PlaceHolder1.ClientID %>");
            var printWindow = window.open('', '', 'height=100,width=400');
            printWindow.document.write('<html><head><title>DIV Contents</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(panel.innerHTML);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            setTimeout(function () {
                printWindow.print();
            }, 500);
            return false;--%>

            var disp_setting = "toolbar=yes,location=no,directories=yes,menubar=yes,";
            disp_setting += "scrollbars=yes,width=700, height=400, left=100, top=25";
            var content_vlue = document.getElementById("imageOutput").innerHTML;

            var docprint = window.open("", "", disp_setting);
            docprint.document.open();
            docprint.document.write('</head><body onLoad="self.print()" style="width: 400px; margin-left:70px; font-size:12px; font-family:arial; font-weight:normal;">');
            docprint.document.write(content_vlue);
            docprint.document.close();
            docprint.focus();
        }
    </script>
</asp:Content>
