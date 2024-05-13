<%@ Page Title="Stock Check" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmStockCheck.aspx.cs" Inherits="frmStockCheck" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Stock Check
                </h3>
            </div>
            <div class="breadcrumb">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-info">
                    <i class="fa fa-list"></i>&nbsp;&nbsp;List</button>
            </div>
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
                                                <th>S.No</th>
                                                <th class="hidden-xs">StockCheck No</th>
                                                <th class="hidden-xs">Date</th>
                                                <th class="hidden-xs">Status</th>
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
                                                    <th>S.No</th>
                                                    <th class="hidden-xs">StockCheck No</th>
                                                    <th class="hidden-xs">Date</th>
                                                    <th class="hidden-xs">Status</th>
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
            <div class="box box-primary" id="divStockCheck">
                <div class="box-header with-border">
                    <div class="box-title">Stock Check Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1">
                            <label>
                                Check No</label>
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Check No"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divDate">
                            <label>
                                Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="-1" id="txtDate" readonly="true" disabled="disabled" />
                            </div>
                        </div>
                    </div>

                    <div class="box box-primary box-solid" id="divBarcodeDetails">
                        <div class="box-header">
                            Stock Details
                        </div>
                        <div class="box-body">
                            <div class="row">

                                <div class="form-group col-md-2" id="divBarcode" style="display: none;">
                                    <label>
                                        Barcode</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtBarcode" placeholder="Barcode"
                                        maxlength="15" tabindex="1" />
                                </div>
                                <div class="form-group col-md-2" id="divCode">
                                    <label>
                                        SMSCode</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtCode" placeholder="Code"
                                        maxlength="12" tabindex="6" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-2" id="divQuantity">
                                    <label>
                                        Quantity</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Quantity"
                                        maxlength="12" tabindex="t" autocomplete="off" />
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive" id="divBarcodeTable">
                        <div id="divBarcodeList">
                        </div>
                    </div>
                    <div class="row" style="display: none;">
                        <button id="btnVerify" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="4">
                            <i class="fa fa-edit" ></i>&nbsp;&nbsp;Verify</button>
                    </div>
                    <div class="box box-primary box-solid"style="display: none;">
                        <div class="box-header">
                            Missing Stock
                        </div>
                    </div>
                    <div class="table-responsive" >
                        <div id="divMissingBarcodeList">
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-12"></div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12"></div>
                    </div>
                    <%--  <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Subtotal</label>
                        </div>
                        <div class="form-group col-md-2" id="divSubtotal">

                            <input type="text" class="form-control" id="txtSubtotal" placeholder="Subtotal"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>--%>

                    <div class="modal-footer clearfix">
                        <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="33">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        <button id="btnSave" type="button" class="btn btn-info margin pull-right" tabindex="31">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Verify & Save</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-right" tabindex="32">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Verify & Update</button>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnStockCheckID" />
    <input type="hidden" id="hdnStockCheckTransID" />
    <input type="hidden" id="hdnOpeningDate" />
    <script src="UserDefined_Js/JStockCheck.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JStockCheck.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            $("[id$=txtCode]").change(function () {
                //var MobileNo = $("[id$=txtCode]").val().split('|')[0].trim();
                $("[id$=txtCode]").val(($("[id$=txtCode]").val().split('|')[0]).trim());
                // console.log(MobileNo);
            });
            $("[id$=txtCode]").autocomplete({

                source: function (request, response) {
                    $.ajax({
                        url: '<%=ResolveUrl("~/frmBarcode.aspx/GetSMSCode") %>',
                        data: "{ 'prefix': '" + request.term + "','SupplierID':0,'IsAll':'A'}",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return {
                                    label: item,
                                    val: item.split('|')[0]
                                }
                            }))
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                open: function () {
                    $("[id$=txtCode]").autocomplete("widget").css({
                        "width": ("600px"), "backgroundColor": ("#d7dde2")
                    });
                },
                select: function (e, i) {
                },
                minLength: 1
            });
        });
    </script>
</asp:Content>



