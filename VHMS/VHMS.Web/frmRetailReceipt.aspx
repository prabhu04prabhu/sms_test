<%@ Page Title="Retail Receipt" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRetailReceipt.aspx.cs" Inherits="frmRetailReceipt" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Retail Receipt
            </h1>
            <small></small>
            <div class="breadcrumb">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
            <br />
        </section>
        <section class="content">
            <div class="row">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Ref No</th>
                                            <th>Entry Date</th>
                                            <th class="hidden-xs">Customer</th>
                                            <th class="hidden-xs">Invoice No</th>
                                            <th class="hidden-xs">Payment Date</th>
                                            <th class="hidden-xs">Amount</th>
                                            <th class="hidden-xs">Account</th>
                                            <th class="hidden-xs">Receipt Mode</th>
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
            </div>
            <div class="modal fade" id="compose-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="box box-primary box-solid">
                                <div class="box-header" style="height: 20px; padding: 0px;">
                                    Recent Transaction
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive" style="min-height: 10px !important">
                                        <div id="divOPBillingList">
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <br />
                            <br />
                            <div class="row"></div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divVoucherNo">
                                    <label>
                                        Ref No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtVoucherNo" placeholder="Reference No."
                                        maxlength="150" tabindex="1" readonly="true" />
                                </div>
                                <div class="form-group col-md-3" id="divVoucherDate" style="display: none;">
                                    <label>
                                        Entry Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtVoucherDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="2" id="txtVoucherDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly="true" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divCustomer">
                                    <label>
                                        Customer</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlCustomer" class="form-control select2" data-placeholder="Select Customer" tabindex="3">
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divBank">
                                    <label>
                                        Select A/c</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="4">
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divReceiptMode">
                                    <label>
                                        Receipt Mode</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlReceiptMode" class="form-control" tabindex="5">
                                        <option value="0" selected="selected">--Select--</option>
                                        <option value="3">NEFT/RTGS</option>
                                        <option value="5">IMPS</option>
                                        <option value="1">Cash</option>
                                        <option value="2">Cheque</option>
                                        <option value="4">Others</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">

                                <div class="form-group col-md-3" id="divAmount">
                                    <label>Paid Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtAmount" placeholder="Amount"
                                            maxlength="15" tabindex="6" autocomplete="off" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divDiscountAmount">
                                    <label>Discount Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtDiscountAmount" placeholder="Discount Amount"
                                            maxlength="15" tabindex="7" autocomplete="off" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divTotalAmount">
                                    <label>Total Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" style="font-size: 104%; font-weight: bold;" id="txtTotalAmount" placeholder="Total Amount"
                                            maxlength="15" tabindex="-1" autocomplete="off" readonly="true" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divCollectionDate">
                                    <asp:Label ID="lblVessel" Text="Payment Date" runat="server" Style="font-weight: bold;"></asp:Label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtCollectionDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="8" id="txtCollectionDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly="true" />
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div id="divChequeDetails">
                                    <div class="form-group col-md-3" id="divChequeNo">
                                        <asp:Label ID="Label1" Text="Reference No #" runat="server" Style="font-weight: bold;"></asp:Label>
                                        <span class="text-danger">*</span>
                                        <input type="text" class="form-control" id="txtChequeNo" placeholder="Cheque/DD No."
                                            maxlength="150" tabindex="9" autocomplete="off" />
                                    </div>
                                    <div class="form-group col-md-3" id="divIssueDate">
                                        <label>
                                            Issued Date</label><span class="text-danger">*</span>
                                        <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtIssueDate" data-link-format="dd/MM/yyyy">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input class="form-control pull-right" tabindex="10" id="txtIssueDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly="true" />
                                        </div>
                                    </div>
                                    <div class="form-group col-md-3" id="divIssuedBy" style="display: none;">
                                        <label>
                                            Issued By</label>
                                        <input type="text" class="form-control" id="txtIssuedBy" placeholder="Issued By"
                                            maxlength="150" tabindex="11" autocomplete="off" />
                                    </div>
                                    <div class="form-group col-md-3" id="divCharges">
                                        <label>Charges</label>
                                        <span class="text-danger">*</span>
                                        <div class="input-group">
                                            <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                            <input type="text" class="form-control decimal" id="txtCharges" placeholder="Charges"
                                                maxlength="15" tabindex="12" autocomplete="off" />
                                            <span class="input-group-addon">.00</span>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <div class="row">
                                <div class="form-group col-md-3" id="divBankName" style="display: none;">
                                    <label>
                                        Bank Name</label>
                                    <input type="text" class="form-control" id="txtBankName" placeholder="Bank Name"
                                        maxlength="150" tabindex="13" autocomplete="off" />
                                </div>
                                <div class="form-group col-md-3" id="divStatus">
                                    <label>
                                        Payment Status</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlPaymentStatus" class="form-control" tabindex="14">
                                        <option value="Cleared">Cleared</option>
                                        <option value="Pending">Pending</option>
                                        <option value="Bounced">Bounced</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divBalanceAmount">
                                    <label>Balance Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtBalanceAmount" style="font-size: 104%; font-weight: bold;" placeholder="Amount"
                                            maxlength="15" tabindex="-1" readonly="true" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divDescription">
                                    <label>
                                        Notes</label>
                                    <input id="txtDescription" class="form-control" maxlength="250" tabindex="15" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-3">
                                    <label>
                                        Upload</label>
                                    <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                        runat="server" ID="imgUpload1" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="214px"
                                        OnUploadedComplete="UploadedComplete" OnClientUploadComplete="DocumentuploadComplete" />
                                    <input type="hidden" id="hdnImgupload1" />
                                    <img src="" id="imgUpload1_view" alt="" class="preview_img" style="width: 280px;" />
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <label>
                                        Total Amount in Words :
                                    </label>
                                    <asp:Label ID="Label2" Text="" Style="color: blue; font-size: initial;" runat="server"></asp:Label>
                                </div>
                            </div>
                            <div class="box box-primary box-solid">
                                <div class="box-header">
                                    Pending Bills
                                </div>
                                <div class="box-body">
                                    <div class="table-responsive" style="min-height: 10px !important">
                                        <div id="divPendingBillList">
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="16">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="17">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="18">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
             <div class="modal fade" id="compose-password" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-group" id="divPassword">
                                <label>
                                    Password</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtPassword" placeholder="Please enter Password"
                                    maxlength="150" tabindex="32" autocomplete="off"/>
                            </div>
                            <div class="form-group" id="divID" style="display:none;">
                                <label>
                                    ID</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtID"
                                    maxlength="150" tabindex="33" />
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-left" id="btnOK" tabindex="34">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnCancel" tabindex="35">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="hdRS" />
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmRetailReceipt.aspx") %>';
        $(document).ready(function () {
            pLoadingSetup(false);
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            $('input,select').keydown(function (event) { //event==Keyevent
                if (event.which == 13) {
                    var inputs = $(this).closest('form').find(':input:visible:not(disabled):not([readonly])');
                    inputs.eq(inputs.index(this) + 1).focus();
                    event.preventDefault(); //Disable standard Enterkey action

                }
            });

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            $(".decimal").inputmask("decimal", { digits: 2, radixPoint: "." });

            $("#txtIssueDate,#txtCollectionDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtIssueDate,#txtCollectionDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                format: 'DD/MM/YYYY'
            });

            $("#txtVoucherDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtVoucherDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                maxDate: moment(),
                format: 'DD/MM/YYYY'
            });

            GetBankList();
            GetPassword(0);
            GetCustomerList();

            $("#divChequeDetails").hide();
            pLoadingSetup(true);

            GetRecord();
        });

        function DocumentuploadComplete(sender, args) {
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetProofPath",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#hdnImgupload1").val("./images/Documents/RetailReceipt/" + r.d);
                    $("#imgUpload1_view").val("/images/Documents/RetailReceipt/" + r.d);
                    // $get("imgUpload1").src = "./images/Documents/Payment/" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#divRecentTransaction").hide();
            $("#divPendingBill").hide();
            $("#txtBalanceAmount").val(0);
            $get("imgUpload1_view").src = "";
            gOPBillingList = [];
            DisplayPendingBillList(gOPBillingList);
            DisplayOPBillingList(gOPBillingList);
            $("#btnUpdate").hide();
            $("#ddlCustomer").attr("disabled", false);
            $("#txtAmount").attr("readonly", false);
            $("#txtDiscountAmount").attr("readonly", false);
            $("#ddlPaymentStatus").attr("disabled", false);
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Receipt");
            $('#compose-modal').modal({ show: true, backdrop: true });
            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtVoucherDate").val(d + "/" + m + "/" + y);
            $("#txtVoucherNo").focus();
            return false;
        });
        $("#btnSave,#btnUpdate").click(function () {

            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate") { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtVoucherDate").val().trim() == "" || $("#txtVoucherDate").val().trim() == undefined) {
                $.jGrowl("Please select Voucher Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divVoucherDate").addClass('has-error'); $("#txtVoucherDate").focus(); return false;
            } else { $("#divVoucherDate").removeClass('has-error'); }

            if ($("#ddlCustomer").val() == "0" || $("#ddlCustomer").val() == undefined || $("#ddlCustomer").val() == null) {
                $.jGrowl("Please select Voucher Type", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCustomer").addClass('has-error'); $("#ddlCustomer").focus(); return false;
            } else { $("#divCustomer").removeClass('has-error'); }

            if ($("#ddlBank").val() == "0" || $("#ddlBank").val() == undefined || $("#ddlBank").val() == null) {
                $.jGrowl("Please select Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBank").addClass('has-error'); $("#ddlBank").focus(); return false;
            } else { $("#divBank").removeClass('has-error'); }

            if ($("#ddlReceiptMode").val() == "0" || $("#ddlReceiptMode").val() == undefined || $("#ddlReceiptMode").val() == null) {
                $.jGrowl("Please select Receipt Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divReceiptMode").addClass('has-error'); $("#ddlReceiptMode").focus(); return false;
            } else { $("#divReceiptMode").removeClass('has-error'); }

            if ($("#txtAmount").val().trim() == "" || $("#txtAmount").val().trim() == undefined) {
                $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
            } else { $("#divAmount").removeClass('has-error'); }

            if (!isNaN($("#txtAmount").val())) {
                if ($("#txtAmount").val() == 0) {
                    $.jGrowl("Please enter Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
                }
            }

            if ($("#ddlReceiptMode").val() != 2) {
                if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                    $.jGrowl("Please select Payment Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                } else { $("#divCollectionDate").removeClass('has-error'); }
            }

            if ($("#ddlReceiptMode").val() == 2) {
                if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                    $.jGrowl("Please select Collection Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                } else { $("#divCollectionDate").removeClass('has-error'); }

                if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Cheque No.", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
                } else { $("#divChequeNo").removeClass('has-error'); }

                if ($("#txtIssueDate").val().trim() == "" || $("#txtIssueDate").val().trim() == undefined) {
                    $.jGrowl("Please select Issue Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divIssueDate").addClass('has-error'); $("#txtIssueDate").focus(); return false;
                } else { $("#divIssueDate").removeClass('has-error'); }
            }

            if ($("#ddlReceiptMode").val() == 3 || $("#ddlReceiptMode").val() == 5) {
                if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Reference No.", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
                } else { $("#divChequeNo").removeClass('has-error'); }
                //if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                //    $.jGrowl("Please select Payment Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                //    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                //} else { $("#divCollectionDate").removeClass('has-error'); }
            }

            var Balance = 0; var iCount = 0;
            $.each($("input[name='PurchaseName']:checked"), function () {
                for (var i = 0; i < gvalue.length; i++) {
                    if (gvalue[i].SalesEntryID == $(this).val()) {
                        Balance = parseFloat(Balance) + gvalue[i].BalanceAmount;
                        iCount = 1
                    }
                }
            });

            if (iCount == 1) {
                if (Balance != (parseFloat($("#txtAmount").val()) + parseFloat($("#txtDiscountAmount").val()))) {
                    $.jGrowl("Paid amount must tally with Balance amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
                }

            }

            var Obj = new Object();
            Obj.ReceiptID = 0;
            Obj.VoucherNo = $("#txtVoucherNo").val().trim();
            Obj.sVoucherDate = $("#txtVoucherDate").val();

            var sAgentID = "";
            $.each($("input[name='PurchaseName']:checked"), function () {
                sAgentID = sAgentID + $(this).val() + ',';
            });
            sAgentID = sAgentID.slice(0, -1);

            var objCustomer = new Object();
            objCustomer.CustomerID = $("#ddlCustomer").val();
            Obj.Customer = objCustomer;

            var objBank = new Object();
            objBank.LedgerID = $("#ddlBank").val();
            Obj.Bank = objBank;
            Obj.SalesEntryIDs = sAgentID;
            Obj.DocumentPath = $("#hdnImgupload1").val();
            Obj.ReceiptModeID = $("#ddlReceiptMode").val();
            Obj.Amount = parseFloat($("#txtAmount").val().trim());
            Obj.Status = $("#ddlPaymentStatus").val();
            Obj.DiscountAmount = parseFloat($("#txtDiscountAmount").val().trim());
            Obj.TDSPayment = 0;
            Obj.sCollectionDate = $("#txtCollectionDate").val().trim();
            Obj.IsRetail = 1;
            if ($("#ddlReceiptMode").val() == 2 || $("#ddlReceiptMode").val() == 3 || $("#ddlReceiptMode").val() == 5) {
                Obj.ChequeNo = $("#txtChequeNo").val().trim();
                Obj.sIssueDate = $("#txtIssueDate").val().trim();
                Obj.IssuedBy = $("#txtIssuedBy").val().trim();
                Obj.BankName = $("#txtBankName").val().trim();
                Obj.Charges = parseFloat($("#txtCharges").val().trim());

            }
            Obj.Description = $("#txtDescription").val().trim();

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.ReceiptID = $("#hdnID").val();
                sMethodName = "UpdateReceipt";
            }
            else { sMethodName = "AddReceipt"; }

            SaveandUpdateReceipt(Obj, sMethodName);

            return false;
        });
        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        $("#ddlCustomer").change(function () {
            $("#txtAmount").val("0");
            $("#txtDiscountAmount").val("0");
            $("#txtTotalAmount").val("0");
            if ($("#txtAmount").val() == 0)
                $('#<%= Label2.ClientID %>').text("");
            if ($("#ddlCustomer").val() > 0) {
                GetLastTransaction();
                GetPendingBill();
            }
        });

        $("#txtDiscountAmount,#txtAmount").change(function () {
            var iDiscount = 0; var iAmount = 0;
            iDiscount = parseFloat($("#txtDiscountAmount").val());
            iAmount = parseFloat($("#txtAmount").val());
            if (isNaN(iDiscount)) iDiscount = 0;
            if (isNaN(iAmount)) iAmount = 0;
            $("#txtTotalAmount").val((parseFloat(iDiscount) + parseFloat(iAmount)));
            if ($("#txtAmount").val() > 0)
                $('#<%= Label2.ClientID %>').text(convertNumberToWords($("#txtAmount").val()));
            else
                $('#<%= Label2.ClientID %>').text("");
        });

        function GetLastTransaction() {
            gOPBillingList = [];
            DisplayOPBillingList(gOPBillingList);
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLastReceiptDetails",
                data: JSON.stringify({ ID: $("#ddlCustomer").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    DisplayOPBillingList(obj);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                dProgress(false);
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

        function GetPendingBill() {
            gOPBillingList = [];
            DisplayPendingBillList(gOPBillingList);
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetPendingRetailBills",
                data: JSON.stringify({ PublisherID: $("#ddlCustomer").val() }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblRecord").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    DisplayPendingBillList(obj);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                dProgress(false);
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

        function DisplayOPBillingList(gData) {
            var sTable = "";
            var sCount = 1;
            var sColorCode = "bg-info";

            if (gData.length >= 5) { $("#divOPBillingList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
            else { $("#divOPBillingList").css({ 'height': '', 'min-height': '' }); }

            if (gData.length > 0) {
                $("#divRecentTransaction").show();
                sTable = "<table id='tblOPBillingList' class='table no-margin table-condensed table-hover'>";
                sTable += "<thead><tr><th style='line-height:0.5;' class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Voucher No</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Invoice No</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>A/c</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Payment Mode</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Amount</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Payment Date</th>";
                sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
                sTable += "</tbody></table>";
                var sPaymentMode = "";
                $("#divOPBillingList").html(sTable);
                for (var i = 0; i < gData.length; i++) {

                    if (gData[i].ReceiptModeID == 1)
                        sPaymentMode = "Cash";
                    else if (gData[i].ReceiptModeID == 2)
                        sPaymentMode = "Cheque";
                    else if (gData[i].ReceiptModeID == 3)
                        sPaymentMode = "NEFT/RTGS";
                    else if (gData[i].ReceiptModeID == 4)
                        sPaymentMode = "Others";
                    else if (gData[i].ReceiptModeID == 5)
                        sPaymentMode = "IMPS";

                    sTable = "<tr><td style='line-height:0.5;' id='" + gData[i].SNo + "'>" + sCount + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].VoucherNo + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].sVoucherDate + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].InvoiceNo + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].Bank.LedgerName + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + sPaymentMode + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].Amount + "</td>";
                    if (gData[i].sCollectionDate != "01-01-1900")
                        sTable += "<td style='line-height:0.5;'>" + gData[i].sCollectionDate + "</td>";
                    else
                        table += "td style='line-height:0.5;'> </td> ";
                    sTable += "</tr>";
                    sCount = sCount + 1;
                    $("#tblOPBillingList_body").append(sTable);
                }
            }
            else {
                $("#divOPBillingList").empty();
                $("#divRecentTransaction").hide();
            }

            return false;
        }

        function DisplayPendingBillList(gData) {
            var sTable = "";
            var sCount = 1;
            var sColorCode = "bg-info";
            gvalue = [];
            $("#txtBalanceAmount").val(0);
            if (gData.length >= 5) { $("#divPendingBillList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
            else { $("#divPendingBillList").css({ 'height': '', 'min-height': '' }); }

            if (gData.length > 0) {
                $("#divPendingBill").show();
                sTable = "<table id='tblPendingBillList' class='table no-margin table-condensed table-hover'>";
                sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                sTable += "<th class='" + sColorCode + "'>Invoice No</th>";
                sTable += "<th class='" + sColorCode + "'>Date</th>";
                sTable += "<th class='" + sColorCode + "'>Net Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Paid Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Exchange Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Return Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Balance Amount</th>";
                sTable += "</tr></thead><tbody id='tblPendingBillList_body'>";
                sTable += "</tbody></table>";
                var sPaymentMode = ""; var BalanceValue = 0;
                $("#divPendingBillList").html(sTable);
                for (var i = 0; i < gData.length; i++) {

                    sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                    if ($("#hdnID").val() > 0) {
                        sTable += "<td><label><input id='chk_" + gData[i].SalesEntryID + "' type='checkbox' value='" + gData[i].SalesEntryID + "'  name='PurchaseName'  disabled='disabled'/>&nbsp;" + gData[i].InvoiceNo + "</label></td>";
                    }
                    else {
                        sTable += "<td><label><input id='chk_" + gData[i].SalesEntryID + "' type='checkbox' value='" + gData[i].SalesEntryID + "'  name='PurchaseName'/>&nbsp;" + gData[i].InvoiceNo + "</label></td>";
                    }
                    sTable += "<td>" + gData[i].sInvoiceDate + "</td>";
                    sTable += "<td>" + gData[i].NetAmount + "</td>";
                    sTable += "<td>" + gData[i].PaidAmount + "</td>";
                    sTable += "<td>" + gData[i].SalesExchangeAmt + "</td>";
                    sTable += "<td>" + gData[i].ReturnAmount + "</td>";
                    sTable += "<td>" + gData[i].BalanceAmount + "</td>";
                    sTable += "</tr>";
                    sCount = sCount + 1;
                    $("#tblPendingBillList_body").append(sTable);
                    BalanceValue += gData[i].BalanceAmount;
                    var objAgent = new Object();
                    objAgent.SalesEntryID = gData[i].SalesEntryID;
                    objAgent.BalanceAmount = gData[i].BalanceAmount;
                    gvalue.push(objAgent);
                }
                $("#txtBalanceAmount").val(BalanceValue);
            }
            else {
                $("#divPendingBillList").empty();
                $("#divPendingBill").hide();
            }

            return false;
        }

        $("#divPendingBillList").on('change', "input[type='checkbox']", function (e) {
            var Balance = 0;
            $.each($("input[name='PurchaseName']:checked"), function () {
                for (var i = 0; i < gvalue.length; i++) {
                    if (gvalue[i].SalesEntryID == $(this).val()) {
                        Balance = parseFloat(Balance) + gvalue[i].BalanceAmount;
                    }
                }
            });
            $("#txtAmount").val(Balance).change();
<%-- if ($("#txtAmount").val() > 0)
                $('#<%= Label2.ClientID %>').text(convertNumberToWords($("#txtAmount").val()));
            else
                $('#<%= Label2.ClientID %>').text("");--%>
        });

        function convertNumberToWords(amount) {
            var words = new Array();
            words[0] = '';
            words[1] = 'One';
            words[2] = 'Two';
            words[3] = 'Three';
            words[4] = 'Four';
            words[5] = 'Five';
            words[6] = 'Six';
            words[7] = 'Seven';
            words[8] = 'Eight';
            words[9] = 'Nine';
            words[10] = 'Ten';
            words[11] = 'Eleven';
            words[12] = 'Twelve';
            words[13] = 'Thirteen';
            words[14] = 'Fourteen';
            words[15] = 'Fifteen';
            words[16] = 'Sixteen';
            words[17] = 'Seventeen';
            words[18] = 'Eighteen';
            words[19] = 'Nineteen';
            words[20] = 'Twenty';
            words[30] = 'Thirty';
            words[40] = 'Forty';
            words[50] = 'Fifty';
            words[60] = 'Sixty';
            words[70] = 'Seventy';
            words[80] = 'Eighty';
            words[90] = 'Ninety';
            amount = amount.toString();
            var atemp = amount.split(".");
            var number = atemp[0].split(",").join("");
            var n_length = number.length;
            var words_string = "";
            if (n_length <= 9) {
                var n_array = new Array(0, 0, 0, 0, 0, 0, 0, 0, 0);
                var received_n_array = new Array();
                for (var i = 0; i < n_length; i++) {
                    received_n_array[i] = number.substr(i, 1);
                }
                for (var i = 9 - n_length, j = 0; i < 9; i++ , j++) {
                    n_array[i] = received_n_array[j];
                }
                for (var i = 0, j = 1; i < 9; i++ , j++) {
                    if (i == 0 || i == 2 || i == 4 || i == 7) {
                        if (n_array[i] == 1) {
                            n_array[j] = 10 + parseInt(n_array[j]);
                            n_array[i] = 0;
                        }
                    }
                }
                value = "";
                for (var i = 0; i < 9; i++) {
                    if (i == 0 || i == 2 || i == 4 || i == 7) {
                        value = n_array[i] * 10;
                    } else {
                        value = n_array[i];
                    }
                    if (value != 0) {
                        words_string += words[value] + " ";
                    }
                    if ((i == 1 && value != 0) || (i == 0 && value != 0 && n_array[i + 1] == 0)) {
                        words_string += "Crores ";
                    }
                    if ((i == 3 && value != 0) || (i == 2 && value != 0 && n_array[i + 1] == 0)) {
                        words_string += "Lakhs ";
                    }
                    if ((i == 5 && value != 0) || (i == 4 && value != 0 && n_array[i + 1] == 0)) {
                        words_string += "Thousand ";
                    }
                    if (i == 6 && value != 0 && (n_array[i + 1] != 0 && n_array[i + 2] != 0)) {
                        words_string += "Hundred ";
                    } else if (i == 6 && value != 0) {
                        words_string += "Hundred ";
                    }
                }
                words_string = words_string.split("  ").join(" ");
            }
            return words_string + "only.";
        }

        $("#ddlReceiptMode").change(function () {
            $("#divChequeDetails").hide();
            var iReceiptMode = $("#ddlReceiptMode").val();
            if (iReceiptMode != undefined && iReceiptMode > 0) {
                if (iReceiptMode == 2 || iReceiptMode == 3 || iReceiptMode == 5)
                    $("#divChequeDetails").show();

                if (iReceiptMode == 2) {
                    $('#<%= lblVessel.ClientID %>').text("Collection Date");
                    $('#<%= Label1.ClientID %>').text("Cheque No #");
                    $("#txtChequeNo").attr("placeholder", "Cheque No");
                    $("#divIssueDate").show();
                    $("#divIssuedBy").show();
                    $("#divBankName").show();
                }
                else {
                    $('#<%= lblVessel.ClientID %>').text("Payment Date");
                    $('#<%= Label1.ClientID %>').text("Reference No #");
                    $("#txtChequeNo").attr("placeholder", "Reference No");
                    $("#divIssueDate").hide();
                    $("#divIssuedBy").hide();
                    $("#divBankName").hide();
                }

            }
            else {
                $("#divChequeDetails").hide();
            }

            return false;
        });

        function ClearFields() {
            $("#txtVoucherNo").val("");
            $("#txtVoucherDate").val("");
            $("#ddlCustomer").val(null).change();
            $("#ddlBank").val(null).change();
            $("#ddlReceiptMode").val(0);
            $("#txtAmount").val("0");
            $("#txtChequeNo").val("");
            $("#txtIssueDate").val("");
            $("#txtCollectionDate").val("");
            $("#txtIssuedBy").val("");
            $("#ddlPaymentStatus").val("Cleared");
            $("#txtBankName").val("");
            $("#txtDescription").val("");
            $("#txtCharges").val("0");
            $("#txtDiscountAmount").val("0");

            $("#divChequeDetails").hide();
            GetSettings();
            return false;
        }
        function GetSettings() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSettings",
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
                                    $("#ddlReceiptMode").val(obj.RetailReceiptModeID).change();
                                    $("#ddlBank").val(obj.RetailReceiptBank.LedgerID).change();
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

        function GetBankList() {
            dProgress(true);
            $("#ddlBank").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLedgerBank",
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
                                    //$("#ddlBank").append('<option value="' + '0' + '">' + '--Select--' + '</option>');
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].IsActive) {
                                            $("#ddlBank").append('<option value=' + obj[index].LedgerID + ' >' + obj[index].LedgerName + '</option>');
                                        }
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlBank").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlBank").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
        function GetCustomerList() {
            dProgress(true);
            $("#ddlCustomer").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetPendingRetailSales",
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
                                    for (var index = 0; index < obj.length; index++) {
                                        $("#ddlCustomer").append('<option value=' + obj[index].Customer.CustomerID + ' >' + obj[index].Customer.CustomerName + '</option>');
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCustomer").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlCustomer").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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

        function SaveandUpdateReceipt(Obj, sMethodName) {
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
                                ClearFields();
                                GetRecord();

                                if (sMethodName == "AddReceipt") { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateReceipt") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Receipt_A_01" || objResponse.Value == "Receipt_U_01") {
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
                url: "WebServices/VHMSService.svc/GetReceipt",
                data: JSON.stringify({ IsRetail: 1 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
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
                                    var sReceiptMode = "";
                                    for (var index = 0; index < obj.length; index++) {

                                        if (obj[index].ReceiptModeID == 1)
                                            sReceiptMode = "Cash";
                                        else if (obj[index].ReceiptModeID == 2)
                                            sReceiptMode = "Cheque";
                                        else if (obj[index].ReceiptModeID == 3)
                                            sReceiptMode = "NEFT/RTGS";
                                        else if (obj[index].ReceiptModeID == 4)
                                            sReceiptMode = "Others";
                                        else if (obj[index].ReceiptModeID == 5)
                                            sReceiptMode = "IMPS";
                                        var InvoiceNo1 = obj[index].InvoiceNos;
                                        var result = InvoiceNo1.split(',').join('');
                                        var result1 = result.split('.00').join('<br />');
                                        var table = "<tr id='" + obj[index].ReceiptID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].VoucherNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sVoucherDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                        table += "<td class='hidden-xs'>" + result1 + "</td>";
                                        if (obj[index].sCollectionDate != "01-01-1900")
                                            table += "<td class='hidden-xs'>" + obj[index].sCollectionDate + "</td>";
                                        else
                                            table += "<td class='hidden-xs'> </td> ";
                                        table += "<td class='hidden-xs'>" + parseFloat(obj[index].Amount).toFixed(2) + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Bank.LedgerName + "</td>";
                                        table += "<td class='hidden-xs'>" + sReceiptMode + "</td>";

                                        table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                        if (ActionView == "1") { table += "<td><a href='#' id=" + obj[index].ReceiptID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td><a href='#' id=" + obj[index].ReceiptID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td ><a href='#' id=" + obj[index].ReceiptID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Receipt");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionUpdate == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $("#ddlCustomer").attr("disabled", true);
                                            $("#txtAmount").attr("readonly", true);
                                            $("#txtDiscountAmount").attr("readonly", true);
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Delete").click(function () {
                                        if (ActionDelete == "1") {
                                            if (confirm('Are you sure to delete the selected record ?')) { ShowDeleteRecord($(this).parent().parent()[0].id); }
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
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
                                    { "sWidth": "7%" },
                                    { "sWidth": "7%" },
                                    { "sWidth": "18%" },
                                    { "sWidth": "12%" },
                                    { "sWidth": "7%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "16%" },
                                    { "sWidth": "9%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" }
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

        function ShowDeleteRecord(id) {
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp; Delete Record");
            $('#compose-password').modal({ show: true, backdrop: true });
            $("#txtID").val(id);
            $("#txtPassword").val("");
            $("#txtPassword").focus();
            return false;
        }

        $("#btnOK").click(function () {

            if ($("#txtPassword").val().trim() == "" || $("#txtPassword").val().trim() == undefined || $("#txtPassword").val().trim() != $("#hdRS").val()) {
                $.jGrowl("Please enter Valid Password", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPassword").addClass('has-error'); $("#txtPassword").focus(); return false;
            } else { $("#divPassword").removeClass('has-error'); }

            DeleteRecord($("#txtID").val());

        });

        function ClearCancelData() {
            $("#txtID").val("");
            $("#txtPassword").val("");
            $('#compose-password').modal('hide');
        }

        function GetPassword(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUserPassword",
                data: JSON.stringify({ ID: 0 }),
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
                                    $("#hdRS").val(obj.ConfirmPassword);
                                    // alert(obj.ConfirmPassword);
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


        $("#btnCancel").click(function () {
            $('#compose-password').modal('hide');
            return false;
        });

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetReceiptByID",
                data: JSON.stringify({ ID: id, IsRetail: 1 }),
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
                                    ClearFields();

                                    $("#hdnID").val(obj.ReceiptID);
                                    $("#txtVoucherNo").val(obj.VoucherNo);
                                    $("#txtVoucherDate").val(obj.sVoucherDate);
                                    $("#ddlCustomer").val(obj.Customer.CustomerID).change();
                                    $("#ddlBank").val(obj.Bank.LedgerID).change();
                                    $("#ddlReceiptMode").val(obj.ReceiptModeID).change();
                                    $("#txtAmount").val(obj.Amount).change();
                                    $("#ddlPaymentStatus").val(obj.Status);
                                    $("[id*=imgUpload1]").attr("src", obj.DocumentPath);
                                    if (obj.Status == 'Cleared')
                                        $("#ddlPaymentStatus").attr("disabled", true);
                                    else
                                        $("#ddlPaymentStatus").attr("disabled", false);
                                    $("#txtDiscountAmount").val(obj.DiscountAmount);
                                    if (obj.ReceiptModeID == 2 || obj.ReceiptModeID == 3 || obj.ReceiptModeID == 5) {
                                        $("#divChequeDetails").show();
                                        $("#txtChequeNo").val(obj.ChequeNo);
                                        $("#txtIssueDate").val(obj.sIssueDate);
                                        $("#txtCollectionDate").val(obj.sCollectionDate);
                                        $("#txtIssuedBy").val(obj.IssuedBy);
                                        $("#txtBankName").val(obj.BankName);
                                        $("#txtCharges").val(obj.Charges);
                                    }
                                    $("#txtDescription").val(obj.Description);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Receipt");
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
                url: "WebServices/VHMSService.svc/DeleteReceipt",
                data: JSON.stringify({ ID: id }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                ClearFields();
                                GetRecord();
                                $.jGrowl("Deleted Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Receipt_R_01" || objResponse.Value == "Receipt_D_01") {
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
    </script>
</asp:Content>

