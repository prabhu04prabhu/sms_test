<%@ Page Title="Exchange" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmExchange.aspx.cs" Inherits="frmExchange" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Exchange
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
                                                <th>Exchange #</th>
                                                <th class="hidden-xs">Exchange Date</th>
                                                <th class="hidden-xs">Customer</th>
                                                <th class="hidden-xs">Mobile No</th>
                                                <th class="hidden-xs">Branch</th>
                                                <th class="hidden-xs">Exchange Amount</th>
                                                <%--<th class="hidden-xs">Status</th>--%>
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
                                                    <th>S.No</th>
                                                    <th>Exchange #</th>
                                                    <th class="hidden-xs">Exchange Date</th>
                                                    <th class="hidden-xs">Customer</th>
                                                    <th class="hidden-xs">Mobile No</th>
                                                    <th class="hidden-xs">Branch</th>
                                                    <th class="hidden-xs">Exchange Amount</th>
                                                    <%--<th class="hidden-xs">Status</th>--%>
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
            <div class="box box-primary" id="divExchange">
                <div class="box-header with-border">
                    <div class="box-title">Exchange Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1" id="divExchangeNo">
                            <label>
                                Exchange No</label>

                            <input type="text" class="form-control" id="txtExchangeNo" placeholder="Exchange No"
                                maxlength="15" tabindex="1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divExchangeDate">
                            <label>
                                Exchange Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtExchangeDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtExchangeDate" readonly="true" disabled="disabled"  />
                            </div>
                        </div>

                        <div class="form-group col-md-2" id="divInvoiceNo">
                            <label>
                                Invoice No</label><span class="text-danger">*</span>

                            <input type="text" class="form-control" id="txtInvoiceNo" placeholder=""
                                maxlength="15" tabindex="3" />
                        </div>
                        <div class="form-group col-md-3" id="divCustomer">
                            <label>
                                Customer</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtName" placeholder="Customer Name"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>

                        <div class="form-group col-md-2" id="divPhone">
                            <label>
                                Phone</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtPhone" placeholder="Phone"
                                maxlength="15" tabindex="-1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divAddress">
                            <label>
                                Address</label><span class="text-danger">*</span>
                            <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="-1" rows="3" readonly="true"></textarea>
                        </div>
                    </div>

                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">

                                <div class="form-group col-md-2" id="divCategory">
                                    <label>
                                        Category</label><span class="text-danger">*</span>

                                    <select id="ddlCategory" class="form-control select2" data-placeholder="Select Category" tabindex="5">
                                    </select>

                                </div>
                                <div class="form-group col-md-2" id="divProduct">
                                    <label>
                                        Product</label><span class="text-danger">*</span>
                                    <select id="ddlProduct" class="form-control select2" data-placeholder="Select Product" tabindex="6">
                                    </select>
                                </div>
                                <div class="form-group col-md-1" id="divNetWeight">
                                    <label>
                                        Net Wt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtNetWeight" placeholder="Net Weight"
                                        maxlength="50" tabindex="7" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1" id="divStoneWeight">
                                    <label>
                                        Stone Wt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtStoneWeight" placeholder="Stone Weight"
                                        maxlength="50" tabindex="8" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1" id="divMeltingWeight">
                                    <label>
                                        Melting Wt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtMeltingWeight" placeholder="Melting Weight"
                                        maxlength="50" tabindex="9" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1" id="divGrossWeight">
                                    <label>
                                        Gross Wt</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtGrossWeight" placeholder="Gross Weight"
                                        maxlength="50" tabindex="-1" readonly="true" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1" id="divKarat">
                                    <label>
                                        Karat</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtKarat" placeholder="Karat"
                                        maxlength="50" tabindex="10" />
                                </div>
                                <div class="form-group col-md-2" id="divCurrentRate">
                                    <label>
                                        Rate</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtCurrentRate" placeholder="Current Rate"
                                        maxlength="50" tabindex="11" onkeypress="return IsNumeric(event)" />
                                </div>
                                <div class="form-group col-md-1" id="divTotal">
                                    <label>
                                        Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtTotal" placeholder="Total"
                                        maxlength="50" tabindex="-1" readonly="true" onkeypress="return IsNumeric(event)" />
                                </div>

                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="13">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="14">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12 pull-right">

                                    <div class="table-responsive">
                                        <div id="divExchangeList">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12"></div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-12"></div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Subtotal</label>
                        </div>
                        <div class="form-group col-md-2" id="divSubtotal">

                            <input type="text" class="form-control" id="txtSubtotal" placeholder="Subtotal"
                                maxlength="15" tabindex="15" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-3"></div>
                        <div class="form-group col-md-1">
                            <label>
                                Tax</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-1">
                            <label id="txtTaxPercent">0</label><span>%</span>
                        </div>
                        <div class="form-group col-md-2" id="divtax">
                            <select id="ddlTax" class="form-control select2" data-placeholder="Select Tax" tabindex="16">
                            </select>
                        </div>
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Tax Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divTaxAmount">
                            <input type="text" class="form-control" id="txtTaxAmount" placeholder="Tax Amount"
                                maxlength="15" tabindex="17" value="0" onkeypress="return IsNumeric(event)" readonly="true" />
                        </div>
                    </div>
                    <div class="row" hidden="hidden">
                        <div class="form-group col-md-2">
                            <label>
                                CGST Amount</label>
                        </div>
                        <label id="txtCGSTPercent">0</label><span>%</span>
                        <div class="form-group col-md-2" id="divCGSTAmount">
                            <input type="text" class="form-control" id="txtCGSTAmount" placeholder="CGST Amount"
                                maxlength="15" tabindex="18" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                SGST Amount</label>
                        </div>
                        <label id="txtSGSTPercent">0</label><span>%</span>
                        <div class="form-group col-md-2" id="divSGSTAmount">
                            <input type="text" class="form-control" id="txtSGSTAmount" placeholder="SGST Amount"
                                maxlength="15" tabindex="19" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                IGST Amount</label>
                        </div>
                        <label id="txtIGSTPercent">0</label><span>%</span>
                        <div class="form-group col-md-2" id="divIGSTAmount">
                            <input type="text" class="form-control" id="txtIGSTAmount" placeholder="IGST Amount"
                                maxlength="15" tabindex="20" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Total Amount</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divTotalAmount">

                            <input type="text" class="form-control" id="txtTotalAmount" placeholder="Total Amount"
                                maxlength="21" tabindex="12" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>

                    </div>

                    <div class="modal-footer clearfix">
                        <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="16">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        <button id="btnSave" type="button" class="btn btn-info margin pull-right" tabindex="14">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save Changes</button>
                        <%--<button id="btnPrint" type="button" class="btn btn-info margin pull-right" tabindex="14">
                            <i class="fa fa-print"></i>&nbsp;&nbsp;Print</button>--%>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-right" tabindex="15">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update Changes</button>
                    </div>
                </div>
            </div>
            <div class="modal fade" id="compose-modal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group" id="divReason">
                                <label>
                                    Reason</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtReason" placeholder="Please enter Reason"
                                    maxlength="150" tabindex="20" />
                            </div>
                            <div class="form-group" id="divID">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="20" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnCancel" tabindex="22">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnOK" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnExchangeID" />
    <input type="hidden" id="hdnExchangeTransID" />
    <input type="hidden" id="hdnCustomerID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnSalesID" />
    <script src="UserDefined_Js/JExchange.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JExchange.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        $("#hdnSalesID").val('<%=Session["SalesID"]%>');

        $(document).ready(function () {
            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });
        });
    </script>
</asp:Content>




