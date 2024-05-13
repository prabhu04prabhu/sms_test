<%@ Page Title="Stock" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmStock.aspx.cs" Inherits="frmStock" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Stock
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Stock</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="divTab">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">Search Result</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="table-responsive">
                                    <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>SNo
                                                </th>
                                                <th>Code
                                                </th>
                                                <th>Category
                                                </th>
                                                <th class="hidden-xs">Product
                                                </th>
                                                <th class="hidden-xs">Branch
                                                </th>
                                                <th class="hidden-xs">Net Weight
                                                </th>
                                                <th class="hidden-xs">Wastage
                                                </th>
                                                <th class="hidden-xs">Total Weight
                                                </th>
                                                <th class="hidden-xs">Status
                                                </th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                                <th></th>
                                            </tr>
                                        </thead>
                                        <tbody id="tblRecord_tbody">
                                        </tbody>
                                    </table>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="SearchResult">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="form-group col-md-4" id="divSearchaname">
                                    <label>
                                        Search records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter search details"
                                        maxlength="150" />
                                </div>
                                <div class="form-group col-md-8"></div>
                                <div class="form-group col-md-12">
                                    <div class="table-responsive">
                                        <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>SNo
                                                    </th>
                                                    <th>Code
                                                    </th>
                                                    <th>Category
                                                    </th>
                                                    <th class="hidden-xs">Product
                                                    </th>
                                                    <th class="hidden-xs">Branch
                                                    </th>
                                                    <th class="hidden-xs">Net Weight
                                                    </th>
                                                    <th class="hidden-xs">Wastage
                                                    </th>
                                                    <th class="hidden-xs">Total Weight
                                                    </th>
                                                    <th class="hidden-xs">Status
                                                    </th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                    <th></th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblSearchResult_tbody">
                                            </tbody>
                                        </table>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-3" id="divDOB">
                                    <label>
                                        Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="1" id="txtDate" readonly="true" />
                                    </div>
                                </div>

                                <div class="form-group col-md-3" id="divCategory">
                                    <label>
                                        Category</label><span class="text-danger">*</span>
                                    <select id="ddlCategory" class="form-control" tabindex="2">
                                        <option selected="selected" value="0">--Select Category--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divProduct">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <select id="ddlProduct" class="form-control" tabindex="3">
                                        <option selected="selected" value="0">--Select Product--</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Barcode</label>
                                    <input type="text" class="form-control" id="txtCode" maxlength="50" readonly="true" tabindex="4" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divNetWeight">
                                    <label>
                                        Net Weight</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtNetWeight" placeholder="Net Weight in gm"
                                        maxlength="15" tabindex="5" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />

                                </div>
                                <div class="form-group col-md-3" id="divWastePercent">
                                    <label>
                                        Waste %</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtWastePercent" placeholder="Waste Percent"
                                        maxlength="15" tabindex="6" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divWaste">
                                    <label>
                                        Wastage</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtWaste" placeholder="Wastage in gm"
                                        maxlength="15" tabindex="7" value="0" onkeypress="return IsNumeric(event)" readonly="true" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-3" id="divTotalWeight">
                                    <label>
                                        Total Weight</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtTotalWeight" placeholder="Total Weight  in gm"
                                        maxlength="15" tabindex="8" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row" hidden="hidden">
                                <div class="form-group col-md-3" id="divRatti">
                                    <label>
                                        Ratti</label>
                                    <input type="text" class="form-control" id="txtRatti" placeholder="Ratti"
                                        maxlength="15" tabindex="9" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divPureWeight">
                                    <label>
                                        Pure Weight</label>
                                    <input type="text" class="form-control" id="txtPureWeight" placeholder="Pure Weight"
                                        maxlength="15" tabindex="10" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-3" id="divLacquer">
                                    <label>
                                        Lacquer</label>
                                    <input type="text" class="form-control" id="txtLacquer" placeholder="Lacquer"
                                        maxlength="15" tabindex="11" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divSellingPrice">
                                    <label>
                                        Selling Price</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSellingPrice" placeholder="Selling Price"
                                        maxlength="15" tabindex="20" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label>
                                        Making</label>
                                    <input type="text" class="form-control" id="txtMaking" placeholder="Making per gm"
                                        maxlength="50" tabindex="12" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Design</label>
                                    <input type="text" class="form-control" id="txtDesign" placeholder="Design"
                                        maxlength="50" tabindex="13" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-3">
                                    <label>
                                        Karat</label><span class="text-danger">*</span>
                                    <select id="ddlKarat" class="form-control" tabindex="3">
                                        <option selected="selected" value="0">--Select Karat--</option>
                                    </select>
                                    <%-- <input type="text" class="form-control" id="txtKarat" placeholder="Karat"
                                        maxlength="10" tabindex="14" />--%>
                                </div>
                                <div class="form-group col-md-3" id="divQuantity">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtQuantity" placeholder="Quantity"
                                        maxlength="10" tabindex="15" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label>
                                        Pieces</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtPieces" placeholder="Pieces"
                                        maxlength="12" tabindex="16" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Size</label>
                                    <input type="text" class="form-control" id="txtSize" placeholder="Size"
                                        maxlength="10" tabindex="17" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Description</label>
                                    <input type="text" class="form-control" id="txtDescription" placeholder="Description"
                                        maxlength="10" tabindex="18" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divPurchasePrice">
                                    <label>
                                        Purchase Price</label>
                                    <input type="text" class="form-control" id="txtPurchasePrice" placeholder="Purchase Price"
                                        maxlength="15" tabindex="19" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>

                            </div>
                        </div>
                    </div>
                    <div class="modal-content">
                        <div class="modal-header">
                            <%--  <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>  --%>
                            <h4>Stone Details</h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label>
                                        Stone Name</label>
                                    <input type="text" class="form-control" id="txtStoneName" placeholder="Stone Name"
                                        maxlength="50" tabindex="21" autocomplete="off" />
                                </div>

                                <div class="form-group col-md-3" id="divStoneWeight">
                                    <label>
                                        Stone Weight</label>
                                    <input type="text" class="form-control" id="txtStoneWeight" placeholder="Stone Weight"
                                        maxlength="15" tabindex="22" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />

                                </div>
                                <div class="form-group col-md-3" id="divStoneQuantity">
                                    <label>
                                        Stone Quantity</label>
                                    <input type="text" class="form-control" id="txtStoneQuantity" placeholder="Stone Quantity"
                                        maxlength="10" tabindex="23" onkeypress="return isNumberKey(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divStoneRate">
                                    <label>
                                        Stone Rate</label>
                                    <input type="text" class="form-control" id="txtStoneRate" placeholder="Stone Rate"
                                        maxlength="15" tabindex="24" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divStonePrice">
                                    <label>
                                        Stone Price</label>
                                    <input type="text" class="form-control" id="txtStonePrice" placeholder="Stone Price"
                                        maxlength="15" tabindex="25" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Stone Color</label>
                                    <input type="text" class="form-control" id="txtStoneColor" placeholder="StoneColor"
                                        maxlength="50" tabindex="26" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Stone Cut</label>
                                    <input type="text" class="form-control" id="txtStoneCut" placeholder="Stone Cut"
                                        maxlength="10" tabindex="27" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3">
                                    <label>
                                        Stone Clarity</label>
                                    <input type="text" class="form-control" id="txtStoneClarity" placeholder="Stone Clarity"
                                        maxlength="10" tabindex="28" autocomplete="off" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="31">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="29">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="30">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                        </div>
                    </div>


                </div>
            </div>
            <div class="modal fade" id="Stockmodal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group  col-md-1"></div>
                                <div class="form-group  col-md-3">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                </div>
                                <div class="form-group  col-md-5">
                                    <input type="text" class="form-control" id="txtProduct" placeholder=""
                                        maxlength="150" tabindex="-1" readonly="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group  col-md-1"></div>
                                <div class="form-group  col-md-3">
                                    <label>
                                        Barcode</label><span class="text-danger">*</span>
                                </div>
                                <div class="form-group  col-md-5">
                                    <input type="text" class="form-control" id="txtBarcode" placeholder=""
                                        maxlength="150" tabindex="-1" readonly="true" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group  col-md-1"></div>
                                <div class="form-group  col-md-3">
                                    <label>
                                        Weight</label><span class="text-danger">*</span>
                                </div>
                                <div class="form-group col-md-3" id="divSplitWeight">

                                    <input type="text" class="form-control" id="txtSplitWeight" placeholder="Weight in gm"
                                        maxlength="15" tabindex="34" value="0" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnCancel" tabindex="36">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnOk" tabindex="35">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
     <input type="hidden" id="hdnNetWt" />
    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            $("#txtDate").attr("data-link-format", "dd/MM/yyyy");

            $("#txtDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            pLoadingSetup(false);
            pLoadingSetup(true);
            GetCategoryList();
            // GetProductList();
            GetRecord();
        });

        //$('#ddlKarat').change(function () {
        //    $("#ddlKarat").empty();

        //})

        $("#ddlCategory").change(function () {
            GetProduct("ddlProduct");
            $("#ddlKarat").empty();
            if ($("#ddlCategory option:selected").text() == "GOLD") {
                $("#ddlKarat").append('<option value=22 Karat>22 Karat</option>');
                $("#ddlKarat").append('<option value=24 Karat>24 Karat</option>');
            }
            else if ($("#ddlCategory option:selected").text() == "DIAMOND") {
                $("#ddlKarat").append('<option value=1 Cent>1 Cent</option>');
                $("#ddlKarat").append('<option value=1 Carat>1 Carat</option>');
            }
            else
                $("#ddlKarat").append('<option value=1 Gram>1 Gram</option>');
        });
        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#ddlProduct").prop("disabled", false);
            $("#ddlCategory").prop("disabled", false);
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Stock");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtDate").focus();
            $("#ddlCategory").focus();
            return false;
        });
        function GetCategoryList() {
            $("#ddlCategory").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCategory",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ CountryID: 0 }),
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    //$("#ddlCategory").append('<option value="' + '0' + '">' + '--Select State--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                            $("#ddlCategory").append('<option value=' + obj[index].CategoryID + ' >' + obj[index].CategoryName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#ddlCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetProduct(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            var CatID = $("#ddlCategory").val();
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetProductID",
                data: JSON.stringify({ CategoryID: CatID }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $(sControlName).append("<option value='" + 0 + "'> --Select-- </option>");
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].ProductID + "'>" + obj[index].ProductName + "</option>");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                                dProgress(false);
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                            dProgress(false);
                        }
                    }
                    else {
                        $(sControlName).append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }
        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#ddlCategory").val() == "0" || $("#ddlCategory").val() == undefined || $("#ddlCategory").val() == null) {
                $.jGrowl("Please select Category", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCategory").addClass('has-error'); $("#ddlCategory").focus(); return false;
            } else { $("#divCategory").removeClass('has-error'); }

            if ($("#ddlProduct").val() == "0" || $("#ddlProduct").val() == undefined || $("#ddlProduct").val() == null) {
                $.jGrowl("Please select Product", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divProduct").addClass('has-error'); $("#ddlProduct").focus(); return false;
            } else { $("#divProduct").removeClass('has-error'); }

            if ($("#ddlKarat").val() == "0" || $("#ddlKarat").val() == undefined || $("#ddlKarat").val() == null) {
                $.jGrowl("Please select Karat", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divKarat").addClass('has-error'); $("#ddlKarat").focus(); return false;
            } else { $("#divKarat").removeClass('has-error'); }

            if ($("#txtDate").val().trim() == "" || $("#txtDate").val().trim() == undefined) {
                $.jGrowl("Please select Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divDate").addClass('has-error'); $("#txtDate").focus(); return false;
            }
            else { $("#divDate").removeClass('has-error'); }
            //if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
            //    $.jGrowl("Please enter Stock", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            //} else { $("#divName").removeClass('has-error'); }

            //if ($("#txtMobileNo").val().trim() == "" || $("#txtMobileNo").val().trim() == undefined) {
            //    $.jGrowl("Please enter Stock", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divMobileNo").addClass('has-error'); $("#txtMobileNo").focus(); return false;
            //} else { $("#divMobileNo").removeClass('has-error'); }

            var Obj = new Object();
            Obj.StockID = 0;
            Obj.sStockDate = $("#txtDate").val();

            var ObjCategory = new Object();
            ObjCategory.CategoryID = $("#ddlCategory").val();
            Obj.Category = ObjCategory;

            var ObjProduct = new Object();
            ObjProduct.ProductID = $("#ddlProduct").val();
            Obj.Product = ObjProduct;
            Obj.NetWeight = $("#txtNetWeight").val();
            Obj.WastagePercent = $("#txtWastePercent").val();
            Obj.Wastage = $("#txtWaste").val();
            Obj.TotalWeight = $("#txtTotalWeight").val();
            Obj.Ratti = $("#txtRatti").val();
            Obj.Lacquer = $("#txtLacquer").val();
            Obj.PureWeight = $("#txtPureWeight").val();
            Obj.Making = $("#txtMaking").val();
            Obj.Design = $("#txtDesign").val();
            Obj.Karat = $("#ddlKarat option:selected").text();
            Obj.Quantity = $("#txtQuantity").val();
            Obj.Pieces = $("#txtPieces").val();
            Obj.Size = $("#txtSize").val();
            Obj.Description = $("#txtDescription").val();
            Obj.PurchasePrice = $("#txtPurchasePrice").val();
            Obj.SellingPrice = $("#txtSellingPrice").val();
            Obj.StoneName = $("#txtStoneName").val();
            Obj.StoneWeight = $("#txtStoneWeight").val();
            Obj.StoneQuantity = $("#txtStoneQuantity").val();
            Obj.StoneRate = $("#txtStoneRate").val();
            Obj.StonePrice = $("#txtStonePrice").val();
            Obj.StoneColor = $("#txtStoneColor").val();
            Obj.StoneCut = $("#txtStoneCut").val();
            Obj.StoneClarity = $("#txtStoneClarity").val();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.StockID = $("#hdnID").val();
                sMethodName = "UpdateStock";
            }
            else { sMethodName = "AddStock"; }

            SaveandUpdateStock(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        $("#txtNetWeight,#txtWastePercent,#txtWaste").change(function () {
            var netWt = 0;
            var WastePercent = 0;
            var WasteAmt = 0;
            var TotAmt = 0;
            if ($("#txtNetWeight").val() > 0)
                netWt = $("#txtNetWeight").val();
            if ($("#txtWastePercent").val() > 0)
                WastePercent = $("#txtWastePercent").val();
            WasteAmt = parseFloat(netWt) * parseFloat(WastePercent) / 100;
            TotAmt = parseFloat(WasteAmt) + parseFloat(netWt);
            $("#txtWaste").val(WasteAmt.toFixed(2));
            $("#txtTotalWeight").val(TotAmt.toFixed(2));

        });
        function ClearFields() {
            $("#txtNetWeight").val(0);
            $("#txtSplitWeight").val(0);
            $("#txtWastePercent").val(0);
            $("#txtWaste").val(0);
            $("#txtTotalWeight").val(0);
            $("#txtRatti").val(0);
            $("#txtLacquer").val(0);
            $("#txtPureWeight").val(0);
            $("#txtMaking").val("0");
            $("#txtDesign").val("");
            //$("#txtKarat").val("");
            $("#txtQuantity").val(1);
            $("#txtPieces").val(1);
            $("#txtSize").val("");
            $("#txtDescription").val("");
            $("#txtPurchasePrice").val(0);
            $("#txtSellingPrice").val(0);
            $("#txtStoneName").val("");
            $("#txtStoneWeight").val(0);
            $("#txtStoneQuantity").val(0);
            $("#txtStoneRate").val(0);
            $("#txtStonePrice").val(0);
            $("#txtStoneColor").val("");
            $("#txtStoneCut").val("");
            $("#txtStoneClarity").val("");
            $("#ddlProduct").val("0");
            $("#ddlCategory").val("0");

            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtDate").val(d + "/" + m + "/" + y);

            $("#divQuantity").removeClass('has-error');
            return false;
        }

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetTopStock",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblRecord_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].StockID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Barcode + "</td>";
                                        table += "<td>" + obj[index].Category.CategoryName + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].StockCode + "</td>";
                                        table += "<td>" + obj[index].Product.ProductName + "</td>";
                                        table += "<td>" + obj[index].Branch.BranchName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].NetWeight + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WastagePercent + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalWeight + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1" && obj[index].Status == "IN")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }
                                        if (ActionAdd == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " Barcode=" + obj[index].Barcode + " class='Print' title='Click here to Print Barcode'><i class='fa fa-print text-green'/></a></td>"; }
                                        else { table += "<td></td>"; }
                                        if (ActionAdd == "1" && obj[index].Status =="IN")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='Split' title='Click here to Split'><i class='fa fa-lg fa-balance-scale text-blue'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }
                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Stock");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Print").click(function () {
                                        var iStockID = parseInt($(this).parent().parent()[0].id);
                                        var iBarcode = $(this).attr('Barcode');
                                        SetSessionValue("Barcode", iBarcode);
                                        var myWindow = window.open("frmBarcode.aspx", "MsgWindow");
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        { EditRecord($(this).parent().parent()[0].id); }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Split").click(function () {

                                        SplitBarcode($(this).parent().parent()[0].id);
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblRecord_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblRecord").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                  { "sWidth": "5%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "5%" },
                                  { "sWidth": "3%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" }
                                ]
                            });
                            $("#tblRecord_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblRecord_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function GetSearchRecord(iDetails) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/SearchStock",
                data: JSON.stringify({ ID: iDetails }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblSearchResult").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblSearchResult_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive == "1")
                                        { TypeStatus = "<span class='label label-success'>Active</span>"; }
                                        else
                                        { TypeStatus = "<span class='label label-warning'>Inactive</span>"; }

                                        var table = "<tr id='" + obj[index].StockID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].Barcode + "</td>";
                                        table += "<td>" + obj[index].Category.CategoryName + "</td>";
                                        //table += "<td class='hidden-xs'>" + obj[index].StockCode + "</td>";
                                        table += "<td>" + obj[index].Product.ProductName + "</td>";
                                        table += "<td>" + obj[index].Branch.BranchName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].NetWeight + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].WastagePercent + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].TotalWeight + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                        if (ActionView == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1" && obj[index].Status == "IN")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        if (ActionDelete == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }
                                        if (ActionAdd == "1")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " Barcode=" + obj[index].Barcode + " class='Print' title='Click here to Print Barcode'><i class='fa fa-print text-green'/></a></td>"; }
                                        else { table += "<td></td>"; }
                                        if (ActionAdd == "1" && obj[index].Status == "IN")
                                        { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].StockID + " class='Split' title='Click here to Split'><i class='fa fa-lg fa-balance-scale text-blue'/></a></td>"; }
                                        else
                                        { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Stock");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Print").click(function () {
                                        var iStockID = parseInt($(this).parent().parent()[0].id);
                                        var iBarcode = $(this).attr('Barcode');
                                        SetSessionValue("Barcode", iBarcode);
                                        var myWindow = window.open("frmBarcode.aspx", "MsgWindow");
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1")
                                        {
                                           
                                            EditRecord($(this).parent().parent()[0].id);
                                            
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?'))
                                            { DeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Split").click(function () {

                                        SplitBarcode($(this).parent().parent()[0].id);
                                    });
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblSearchResult_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblSearchResult").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                  { "sWidth": "5%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "0%" },
                                  { "sWidth": "5%" },
                                  { "sWidth": "3%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" },
                                  { "sWidth": "1%" }
                                ]
                            });
                            $("#tblSearchResult_filter").addClass('pull-right');
                            $(".pagination").addClass('pull-right');
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location("frmLogin.aspx");
                            }
                            else if (objResponse.Value == "Error") {
                                window.location = "frmErrorPage.aspx";
                            }
                        }
                    }
                    else {
                        $("#tblSearchResult_tbody").empty();
                        dProgress(false);
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
        }

        function SaveandUpdateStock(Obj, sMethodName) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/" + sMethodName,
                data: JSON.stringify({ Objdata: Obj }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {

                                GetRecord();

                                if (sMethodName == "AddStock") {
                                    $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                    $("#hdnID").val(objResponse.Value);
                                }
                                else if (sMethodName == "UpdateStock")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                GetBarcode($("#hdnID").val());
                                $('#compose-modal').modal('hide');

                                ClearFields();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Stock_A_01" || objResponse.Value == "Stock_U_01") {
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
        function SplitBarcode(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetStockByID",
                data: JSON.stringify({ ID: id }),
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

                                    $("#hdnID").val(obj.StockID);
                                    $("#txtBarcode").val(obj.Barcode);
                                    $("#txtProduct").val(obj.Product.ProductName);                                    
                                    $("#hdnNetWt").val(obj.NetWeight);
                                    $('#Stockmodal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Stock Split");
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
        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetStockByID",
                data: JSON.stringify({ ID: id }),
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
                                    $("#btnSave").hide();
                                    $("#btnUpdate").show();
                                  
                                   // $("#ddlProduct").disable;
                                    $("#hdnID").val(obj.StockID);

                                    $("#txtDate").val(obj.sStockDate);
                                    $("#txtNetWeight").val(obj.NetWeight);
                                    $("#txtWastePercent").val(obj.WastagePercent);
                                    $("#txtWaste").val(obj.Wastage);
                                    $("#txtTotalWeight").val(obj.TotalWeight);
                                    $("#txtRatti").val(obj.Ratti);
                                    $("#txtLacquer").val(obj.Lacquer);
                                    $("#txtPureWeight").val(obj.PureWeight);
                                    $("#txtCode").val(obj.Barcode);
                                    $("#txtMaking").val(obj.Making);
                                    $("#txtDesign").val(obj.Design);
                                    $("#ddlKarat").val(obj.Karat);
                                    $("#txtQuantity").val(obj.Quantity);
                                    $("#txtPieces").val(obj.Pieces);
                                    $("#txtSize").val(obj.Size);
                                    $("#txtDescription").val(obj.Description);
                                    $("#txtPurchasePrice").val(obj.PurchasePrice);
                                    $("#txtSellingPrice").val(obj.SellingPrice);
                                    $("#txtStoneName").val(obj.StoneName);
                                    $("#txtStoneWeight").val(obj.StoneWeight);
                                    $("#txtStoneQuantity").val(obj.StoneQuantity);
                                    $("#txtStoneRate").val(obj.StoneRate);
                                    $("#txtStonePrice").val(obj.StonePrice);
                                    $("#txtStoneColor").val(obj.StoneColor);
                                    $("#txtStoneCut").val(obj.StoneCut);
                                    $("#txtStoneClarity").val(obj.StoneClarity);
                                    $("#ddlCategory").val(obj.Category.CategoryID).change();
                                    $("#ddlProduct").val(obj.Product.ProductID).change();
                                    $("#ddlProduct").prop("disabled", true);
                                    $("#ddlCategory").prop("disabled", true);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Stock");
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

        function DeleteRecord(id) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/DeleteStock",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                ClearFields();
                                GetRecord();
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Stock_R_01" || objResponse.Value == "Stock_D_01") {
                                $.jGrowl(_CMDeleteError, { sticky: false, theme: 'danger', life: jGrowlLife });
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


        function GetBarcode(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetStockByID",
                data: JSON.stringify({ ID: id }),
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
                                    SetSessionValue("Barcode", obj.Barcode);
                                    var myWindow = window.open("frmBarcode.aspx", "MsgWindow");
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

        $("#txtSearchName").change(function () {

            if ($("#txtSearchName").val().trim() != "" || $("#txtSearchName").val().trim() != undefined) {
                var iDetails = $("#txtSearchName").val();
                GetSearchRecord(iDetails);
            }
            return false;
        });

        $("#aGeneral").click(function () {
            $("#SearchResult").hide();
            GetRecord();
        });

        $("#aSearchResult").click(function () {
            $("#SearchResult").show();
        });

        $("#btnOk").click(function () {

            if ($("#txtSplitWeight").val() <= 0 || $("#txtSplitWeight").val() == "" || $("#txtSplitWeight").val() == undefined || $("#txtSplitWeight").val() == null) {
                $.jGrowl("Please enter Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSplitWeight").addClass('has-error'); $("#txtSplitWeight").focus(); return false;
            } else { $("#divSplitWeight").removeClass('has-error'); }

            if ($("#txtSplitWeight").val() >= $("#hdnNetWt").val()) {
                $.jGrowl("Please enter Weight less than Net Weight", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSplitWeight").addClass('has-error'); $("#txtSplitWeight").focus(); return false;
            } else { $("#divSplitWeight").removeClass('has-error'); }

            UpdateSplitWeight($("#hdnID").val(), $("#txtSplitWeight").val());

            return false;
        });

        function UpdateSplitWeight(id, SplitWeight) {
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/UpdateSplitWeight",
                data: JSON.stringify({ ID: id, iWeight:SplitWeight }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                GetRecord();                                
                                $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                               // GetBarcode($("#hdnID").val());
                               
                                $('#Stockmodal').modal('hide');

                                ClearFields();
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Stock_A_01" || objResponse.Value == "Stock_U_01") {
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

        $("#btnCancel").click(function () {
            $('#Stockmodal').modal('hide');
            return false;
        });
    </script>
</asp:Content>



