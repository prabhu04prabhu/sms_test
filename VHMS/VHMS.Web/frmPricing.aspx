<%@ Page Title="Pricing" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPricing.aspx.cs" Inherits="frmPricing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Pricing
            </h1>
            <ol class="breadcrumb">
                <%--   <div class="form-group col-md-1" style="display: none;">
                    <asp:Button ID="btnExcel" Text="Excel" CausesValidation="false" CssClass="btn btn-info" Style="margin-left: -52px; background: brown !important;" OnClick="btnExcel_Click" runat="server" />
                </div>
                <div class="form-group col-md-1" style="display: none;">
                    <asp:Button ID="Button1" Text="Print" CausesValidation="false" CssClass="btn btn-info" Style="margin-left: -142px; background: brown !important;" OnClick="btnExport_Click" runat="server" />
                </div>--%>
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Billing</a></li>
                <li class="active">Pricing</li>
            </ol>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="nav-tabs-custom" id="tab-modal">
                <%-- <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                </ul>--%>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">

                        <div class="row">
                            <div class="form-group col-md-2" id="divCategory">
                                <label>
                                    Category</label>
                                <select id="ddlCategory" class="form-control select2" data-placeholder="Select Category" tabindex="1">
                                </select>
                            </div>
                            <div class="form-group col-md-2" id="divSubCategory">
                                <label>
                                    Subcategory</label>
                                <select id="ddlSubCategory" class="form-control select2" data-placeholder="Select SubCategory" tabindex="2">
                                </select>
                            </div>
                            <div class="form-group col-md-2" id="divProduct">
                                <label>
                                    Product</label>
                                <select id="ddlProduct" class="form-control select2" data-placeholder="Select Product" tabindex="3">
                                </select>
                            </div>
                            <div class="form-group col-md-2" id="divSupplier">
                                <label>
                                    Supplier</label>
                                <select id="ddlSupplier" class="form-control select2" data-placeholder="Select Product" tabindex="4">
                                </select>
                            </div>
                            <div class="form-group col-md-2" id="divType">
                                <label>
                                    Product Type</label>
                                <select id="ddlType" class="form-control select2" data-placeholder="Select Type" tabindex="4">
                                </select>
                            </div>
                            <div class="form-group col-md-2">
                                <label>
                                    SMS Code / Party Code</label>
                                <input type="text" class="form-control" id="txtCode" placeholder="Please enter Code"
                                    maxlength="10" tabindex="5" autocomplete="off" />
                            </div>
                        </div>

                    </div>
                </div>
            </div>
            <div class="row" id="divRecords" runat="server">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered table-hover bg-info" width="100%">
                                    <%--<table id="" class="table no-margin table-condensed table-hover">--%>
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>SMSCode
                                            </th>
                                            <th>Product 
                                            </th>
                                            <th>Party Code
                                            </th>
                                            <th>Purc. Price
                                            </th>
                                            <th>W. Margin
                                            </th>
                                            <th>Wholesale Price
                                            </th>
                                            <th>Retail Margin
                                            </th>
                                            <th>Retail Price
                                            </th>
                                            <th>MRP
                                            </th>
                                            <th>Min.Disc %
                                            </th>
                                            <th>Max.Disc %
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                                    <tbody id="tblRecord_tbody">
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                    <div class="modal fade" id="composeImage" tabindex="-1" role="dialog" aria-hidden="true">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                        &times;</button>
                                    <h4 class="modal-title"></h4>
                                </div>
                                <div class="modal-body">
                                    <div>
                                        <img src="" id="imgUpload1" alt="" style="visibility: hidden; display: block; margin-right: auto; width: 280px; height: 280px" />
                                    </div>
                                    <div>
                                        <img src="" id="imgUpload5" alt="" style="visibility: hidden; display: block; margin-top: -280px; margin-left: 297px; width: 280px; height: 280px" />
                                    </div>
                                    <div>
                                        <img src="" id="imgUpload6" alt="" style="visibility: hidden; display: block; margin-right: auto; margin-left: 593px; margin-top: -280px; width: 280px; height: 280px" />
                                    </div>
                                </div>
                                <div class="modal-footer clearfix">
                                    <button type="button" class="btn btn-danger pull-left" id="btnImageCancel" tabindex="37">
                                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer clearfix">
                        <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="20">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                        <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="22">
                            <i class="fa fa-times"></i>&nbsp;&nbsp;
                                Close</button>


                    </div>
                </div>
            </div>
            <div class="modal fade" id="composeRetail" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divRetailPriceA">
                                    <label>
                                        Price A</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRetailPriceA" placeholder=""
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divRetailPriceB">
                                    <label>
                                        Price B</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRetailPriceB" placeholder=""
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divRetailPriceC">
                                    <label>
                                        Price C</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtRetailPriceC" placeholder=""
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSaveRetail" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancelRetail" tabindex="22">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composeWholeSale" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divWholeSalePriceA">
                                    <label>
                                        Price A</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceA" placeholder=""
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceB">
                                    <label>
                                        Price B</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceB" placeholder=""
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-4" id="divWholeSalePriceC">
                                    <label>
                                        Price C</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtWholeSalePriceC" placeholder=""
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                        </div>


                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSaveWholeSale" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancelWholeSale" tabindex="22">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="composeProductImage" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body" id="myImg">
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnProductImageCancel" tabindex="37">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnPricingID" />
    <input type="hidden" id="hdnProductID" />
    <script src="UserDefined_Js/Billing/JPricing.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Billing/JPricing.js") %>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });
        });
    </script>
    <script>
        function PrintDiv() {
            var divContents = document.getElementById("printdivcontent").innerHTML;
            var printWindow = window.open('', '', 'height=200,width=400');
            printWindow.document.write('<html><head><title>Print DIV Content</title>');
            printWindow.document.write('</head><body >');
            printWindow.document.write(divContents);
            printWindow.document.write('</body></html>');
            printWindow.document.close();
            printWindow.print();
        }
    </script>
</asp:Content>
