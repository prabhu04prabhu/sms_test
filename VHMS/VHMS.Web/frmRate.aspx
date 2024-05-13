<%@ Page Title="Rate" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRate.aspx.cs" Inherits="frmRate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <h1>Rate
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Rate</li>
            </ol>
            <div class="pull-right">
                <%-- <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-info">
                    <i class="fa fa-list"></i>&nbsp;&nbsp;List</button>--%>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="box box-solid box-primary" id="divRate">
                <div class="box-header with-border">
                    <div class="box-title">
                        <i class="fa fa-pencil-square"></i>&nbsp;&nbsp;Update Rate
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="col-md-4">
                            <div class="box box-info box-solid">
                                <div class="box-header">
                                    <div class="box-title"><i class="fa fa-diamond"></i>&nbsp;&nbsp;Gold</div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <div class="form-group col-md-4">
                                            <label>
                                            </label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                24 Karat</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                22 Karat</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                Purchase Rate</label>
                                        </div>
                                        <div class="form-group col-md-4" id="divGoldP24">
                                            <input type="text" class="form-control" id="txtGoldP24"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                        <div class="form-group col-md-4" id="divGoldS24">
                                            <input type="text" class="form-control" id="txtGoldS24"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                Selling Rate</label>
                                        </div>
                                        <div class="form-group col-md-4" id="divGoldP22">
                                            <input type="text" class="form-control" id="txtGoldP22"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>

                                        <div class="form-group col-md-4" id="divGoldS22">
                                            <input type="text" class="form-control" id="txtGoldS22"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="box box-info box-solid">
                                <div class="box-header">
                                    <div class="box-title"><i class="fa fa-diamond"></i>&nbsp;&nbsp;Diamond</div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <div class="form-group col-md-4">
                                            <label>
                                            </label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                1 Cent</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                1 Carat</label>
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                Purchase Rate</label>
                                        </div>
                                        <div class="form-group col-md-4" id="divDiamondPCent">

                                            <input type="text" class="form-control" id="txtDiamondPCent"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                        <div class="form-group col-md-4" id="divDiamondSCent">

                                            <input type="text" class="form-control" id="txtDiamondSCent"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                        <div class="form-group col-md-4">
                                            <label>
                                                Selling Rate</label>
                                        </div>
                                        <div class="form-group col-md-4" id="divDiamondPCT">

                                            <input type="text" class="form-control" id="txtDiamondPCT"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                        <div class="form-group col-md-4" id="divDiamondSCT">

                                            <input type="text" class="form-control" id="txtDiamondSCT"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-4">
                            <div class="box box-info box-solid">
                                <div class="box-header">
                                    <div class="box-title"><i class="fa fa-diamond"></i>&nbsp;&nbsp;Silver</div>
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive">
                                        <div class="form-group col-md-6">
                                            <label>
                                            </label>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>
                                                1 Gram</label>
                                        </div>

                                        <div class="form-group col-md-6">
                                            <label>
                                                Purchase Rate</label>
                                        </div>
                                        <div class="form-group col-md-6" id="divSilverP">

                                            <input type="text" class="form-control" id="txtSilverP"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label>
                                                Selling Rate</label>
                                        </div>
                                        <div class="form-group col-md-6" id="divSilverS">

                                            <input type="text" class="form-control" id="txtSilverS"
                                                maxlength="200" tabindex="1" value="0" onkeypress="return IsNumeric(event)" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                </div>
                <div class="modal-footer clearfix">

                    <button type="button" class="btn btn-info pull-right" id="btnUpdate" tabindex="22">
                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                </div>
            </div>
        </section>
    </div>

    <input type="hidden" id="hdnRateID" />
    <%--<script src="UserDefined_Js/JRate.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JRate.js") %>" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {

            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            pLoadingSetup(false);
            pLoadingSetup(true);
            $("#btnUpdate").show();
            $("#divDoctor").show();
            GetRecord();
        });


        $("#btnUpdate").click(function () {
            if (ActionAdd != "1") {
                $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                return false;
            }

            var objDoctor = new Object();
            objDoctor.RateID = 0;
            objDoctor.Gold_22Sales = $("#txtGoldS22").val();
            objDoctor.Gold_24Sales = $("#txtGoldS24").val();
            objDoctor.Gold_22Purchase = $("#txtGoldP22").val();
            objDoctor.Gold_24Purchase = $("#txtGoldP24").val();
            objDoctor.SilverPurchase = $("#txtSilverP").val();
            objDoctor.SilverSales = $("#txtSilverS").val();
            objDoctor.Diamond_1CentPurchase = $("#txtDiamondPCent").val();
            objDoctor.Diamond_1CentSales = $("#txtDiamondSCent").val();
            objDoctor.Diamond_1CTPurchase = $("#txtDiamondPCT").val();
            objDoctor.Diamond_1CTSales = $("#txtDiamondSCT").val();

            SaveandUpdateDoctor(objDoctor, "AddRate");
            return false;
        });
        function SaveandUpdateDoctor(ObjDoctor, sMethodName) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/" + sMethodName,
                data: JSON.stringify({ Objdata: ObjDoctor }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {                                
                                //window.location("frmDefault.aspx");
                                $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Rate_A_01" || objResponse.Value == "Rate_U_01") {
                                $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCurrentRate",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord" && objResponse.Value != "Error") {
                                var obj = jQuery.parseJSON(objResponse.Value);
                                if (obj != null) {
                                    $("#btnUpdate").show();

                                    document.title = "Rate";

                                    $("#hdnRateID").val(obj.RateID);
                                    $("#txtGoldS22").val(obj.Gold_22Sales);
                                    $("#txtGoldS24").val(obj.Gold_24Sales);
                                    $("#txtGoldP22").val(obj.Gold_22Purchase);
                                    $("#txtGoldP24").val(obj.Gold_24Purchase);
                                    $("#txtSilverP").val(obj.SilverPurchase);
                                    $("#txtSilverS").val(obj.SilverSales);
                                    $("#txtDiamondPCent").val(obj.Diamond_1CentPurchase);
                                    $("#txtDiamondSCent").val(obj.Diamond_1CentSales);
                                    $("#txtDiamondPCT").val(obj.Diamond_1CTPurchase);
                                    $("#txtDiamondSCT").val(obj.Diamond_1CTSales);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                                dProgress(false);
                            }
                            else if (objResponse.Value == "Error") {
                                $.jGrowl("Error", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $.jGrowl("No Record", { sticky: false, theme: 'warning', life: jGrowlLife });
                            }
                        }
                    }
                    else {
                        $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }
    </script>
</asp:Content>

