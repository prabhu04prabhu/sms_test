<%@ Page Title="OPBilling" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmOPBilling.aspx.cs" Inherits="frmOPBilling" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>OPBilling
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
                                                <th>OPBilling #</th>
                                                <th class="hidden-xs">Date</th>
                                                <th class="hidden-xs">OPD No</th>
                                                <th class="hidden-xs">Husband Name</th>
                                                <th class="hidden-xs">Wife Name</th>
                                                <th class="hidden-xs">Status</th>
                                                 <th class="hidden-xs">Reason</th>
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
                                <div class="form-group" id="divSearchaname">
                                    <label>
                                        Search OP Bill records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>OPBilling #</th>
                                                <th class="hidden-xs">Date</th>
                                                <th class="hidden-xs">OPD No</th>
                                                <th class="hidden-xs">Husband Name</th>
                                                <th class="hidden-xs">Wife Name</th>
                                                <th class="hidden-xs">Status</th>
                                                 <th class="hidden-xs">Reason</th>
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
            <div class="box box-primary" id="divOPBilling">
                <div class="box-header with-border">
                    <div class="box-title">OPBilling Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2 fa-align-right">
                            <label>
                                Bill No</label>
                        </div>
                        <div class="form-group col-md-2" id="divBillNo">
                            <input type="text" class="form-control" id="txtBillNo" placeholder="Bill No"
                                maxlength="15" tabindex="1" readonly="true" />
                        </div>
                        <div class="form-group col-md-1 fa-align-right">
                            <label>
                                Date</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divBillDate">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOPBillingDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBillDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-1"></div>
                        <div class="form-group col-md-1 fa-align-right">
                            <label>
                                Doctor</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divDoctor">
                            <select id="ddlDoctor" class="form-control select2" data-placeholder="Select Doctor" tabindex="3">
                            </select>
                        </div>
                        <div class="form-group col-md-2 fa-align-right">
                            <label>
                                OPD No/ Moblie No</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divOPDNo">
                            <input type="text" class="form-control" id="txtOPDNo" placeholder=""
                                maxlength="15" tabindex="4" />
                        </div>
                        <div class="form-group col-md-1 fa-align-right">
                            <label>
                                Patient</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-3" id="divPatient">
                            <input type="text" class="form-control" id="txtName" placeholder="Patient Name"
                                maxlength="15" tabindex="5" readonly="true" />
                        </div>
                        <div class="form-group col-md-1 fa-align-right">
                            <label>
                                Phone</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divPhone">
                            <input type="text" class="form-control" id="txtPhone" placeholder="Phone"
                                maxlength="15" tabindex="6" readonly="true" />
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divDescriptionName">
                                    <label>
                                        Description</label><span class="text-danger">*</span>
                                    <div id="divSelectDescriptionName">
                                        <select id="ddlDescriptionName" class="form-control select2" data-placeholder="Select Description Name" tabindex="6"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divFrequency">
                                    <label>
                                        Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtAmount" placeholder="Amount"
                                        maxlength="12" tabindex="7"  />
                                </div>

                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="8">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="9">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div id="divOPBillingList">
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Subtotal</label>
                        </div>
                        <div class="form-group col-md-2" id="divSubtotal">

                            <input type="text" class="form-control" id="txtSubtotal" placeholder="Subtotal"
                                maxlength="15" tabindex="10" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Discount</label>
                        </div>
                        <div class="form-group col-md-2" id="divDiscount">
                            <input type="text" class="form-control" id="txtDiscount" placeholder="Discount"
                                maxlength="15" tabindex="11" value="0" onkeypress="return IsNumeric(event)" />
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
                                maxlength="15" tabindex="12" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
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
    <input type="hidden" id="hdnOPID" />
    <input type="hidden" id="hdnOPTransID" />
    <input type="hidden" id="hdnPatientID" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/JOPBilling.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JOPBilling.js") %>" type="text/javascript"></script>
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
        });
    </script>
</asp:Content>

