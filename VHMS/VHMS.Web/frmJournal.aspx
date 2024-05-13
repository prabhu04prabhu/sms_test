<%@ Page Title="Other Expense" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmJournal.aspx.cs" Inherits="frmJournal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Other Expense
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
                                                <th>Expense #</th>
                                                <th>Date</th>
                                                 <th>Bill No</th>
                                                 <th>Bill Date</th>
                                                 <th>Net Amount</th>
                                                <th>Narration</th>
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
                                        Search Expense records</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtSearchName" placeholder="Please enter Details"
                                        maxlength="150" />
                                </div>

                                <div class="table-responsive">
                                    <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                        <thead>
                                            <tr>
                                                <th>S.No</th>
                                                <th>Expense #</th>
                                               <th>Date</th>
                                                 <th>Bill No</th>
                                                 <th>Bill Date</th>
                                                 <th>Net Amount</th>
                                                <th>Narration</th>
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
                    <div class="box-title">Expense Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-1 fa-align-right">
                            <label>
                                PO. No</label>
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
                         <div class="form-group col-md-1 fa-align-right">
                            <label>
                                Bill No</label><span class="text-danger">*</span>
                        </div>
                         <div class="form-group col-md-2" id="divExpenseNo">
                            <input type="text" class="form-control" id="txtExpenseNo" placeholder="Bill No"
                                maxlength="30" tabindex="4" autocomplete="off" />
                        </div>
                         <div class="form-group col-md-1 fa-align-right">
                            <label>
                                Bill Date</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divBDate">
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtBDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtBDate" readonly="true" />
                            </div>
                        </div>

                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2" id="divType" style="display:none;">
                                    <label>
                                        Type</label><span class="text-danger">*</span>
                                    <select id="ddlType" class="form-control" tabindex="5">
                                        <option value="Cr" selected="selected">Cr</option>
                                        <%--<option value="Dr">Dr</option>--%>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divLedgerName">
                                    <label>
                                        Ledger</label><span class="text-danger">*</span>
                                    <div id="divSelectLedgerName">
                                        <select id="ddlLedgerName" class="form-control select2" data-placeholder="Select Ledger Name" tabindex="6"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divAmount">
                                    <label>
                                        Amount</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtAmount" placeholder="Amount"
                                        maxlength="12" tabindex="7" onkeypress="return IsNumeric(event)" autocomplete="off"/>
                                </div>
                                 <div class="form-group col-md-4" id="divNotes">
                                    <label>
                                        Notes</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtNotes" placeholder="Notes"
                                        maxlength="500" tabindex="7"  autocomplete="off"/>
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
                          <div class="table-responsive"  style="min-height:100px !Important;">
                        <div id="divOPBillingList">
                        </div>
                    </div>
                    </div>
                  
                    <div class="row">
                         <div class="form-group col-md-1">
                            <label>
                                Narration</label>
                        </div>
                        <div class="form-group col-md-4" id="divNarration">

                            <input type="text" class="form-control" id="txtNarration" placeholder="Narration"
                                maxlength="200" tabindex="12" autocomplete="off" style="width:130%" />
                        </div>
                        <div class="form-group col-md-4"></div>
                         <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Round off</label>
                        </div>
                        <div class="form-group col-md-2" id="divRoundoff">
                            <input type="text" class="form-control" id="txtRoundoff" placeholder="Roundoff"
                                maxlength="15" tabindex="2" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                        <div class="form-group col-md-2" style="display:none;">
                            <label>
                                Credit Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divCredit" style="display:none;">
                            <input type="text" class="form-control" id="txtCredit"style="font-weight: bold; font-size: 18px;" placeholder="Debit Amount"
                                maxlength="15" tabindex="12" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row" style="display:none;">
                        <div class="form-group col-md-8"></div>
                        <div class="form-group col-md-2">
                            <label>
                                Debit Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divDebit">

                            <input type="text" class="form-control" id="txtDebit"style="font-weight: bold; font-size: 18px;" placeholder="Debit Amount"
                                maxlength="15" tabindex="12" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                       
                    </div>
                    <div class="row">
                        <div class="form-group col-md-9"></div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Net Amount</label>
                        </div>
                        <div class="form-group col-md-2" id="divNetAmount">

                            <input type="text" class="form-control" id="txtNetAmount" placeholder="Net Amount" style="font-weight: bold; font-size: 25px;"
                                maxlength="15" tabindex="-1" readonly="true" value="0" onkeypress="return IsNumeric(event)" />
                        </div>
                    </div>

                    <div class="row">
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Pay Mode</label><span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divReceiptMode">
                            <select id="ddlReceiptMode" class="form-control" tabindex="12">
                                <option value="0" selected="selected">--Select--</option>
                                <option value="1">Cash</option>
                                <option value="2">Cheque</option>
                                <option value="3">NEFT/RTGS</option>
                                <option value="4">Others</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1" style="text-align: right">
                            <label>
                                Select A/c</label>
                            <span class="text-danger">*</span>
                        </div>
                        <div class="form-group col-md-2" id="divBank">
                            <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="11">
                            </select>
                        </div>
                        <div id="divChequeDetails">
                            <div class="form-group col-md-1" style="text-align: right">
                                <label>
                                    ChequeNo #</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divChequeNo">
                                <input type="text" class="form-control" id="txtChequeNo" placeholder="Cheque/DD No."
                                    maxlength="150" tabindex="13" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-1" style="text-align: right">
                                <label>
                                    Issued Date</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divIssueDate">
                                <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtIssueDate" data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar"></i>
                                    </div>
                                    <input class="form-control pull-right" tabindex="14" id="txtIssueDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                </div>
                            </div>

                            <div class="form-group col-md-1" style="text-align: right">
                                <label>
                                    Status</label><span class="text-danger">*</span>
                            </div>
                            <div class="form-group col-md-2" id="divStatus">
                                <select id="ddlPaymentStatus" class="form-control" tabindex="15">
                                    <option value="Cleared">Cleared</option>
                                    <option value="Pending">Pending</option>
                                    <option value="Bounced">Bounced</option>
                                </select>
                            </div>
                        </div>
                    </div>

                    <div class="modal-footer clearfix">
                         <button id="btnSave" type="button" class="btn btn-info margin pull-left" tabindex="14">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save Changes</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-left" tabindex="15">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update Changes</button>
                        <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="16">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
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
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="22">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnJournalID" />
    <input type="hidden" id="hdnJournalTransID" />
    <input type="hidden" id="hdnPatientID" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/jJournal.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/jJournal.js") %>" type="text/javascript"></script>
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

