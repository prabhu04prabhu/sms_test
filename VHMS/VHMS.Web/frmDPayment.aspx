<%@ Page Title="D Payment" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmDPayment.aspx.cs" Inherits="frmDPayment" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <style>
        .btnPrint, .btnPrintbill {
            background-color: #ef00bc !important;
            margin-top: 0px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>D Payment
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
                            <div class="table-responsive" style="min-height: 10px !important">
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>S.No</th>
                                            <th>Ref No</th>
                                            <th>Entry Date</th>
                                            <th class="hidden-xs">Supplier</th>
                                            <th class="hidden-xs">Bill No</th>
                                            <th class="hidden-xs">Payment Date</th>
                                            <th class="hidden-xs">Amount</th>
                                            <th class="hidden-xs">Account</th>
                                            <th class="hidden-xs">Payment Mode</th>
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
            </div>
            <div class="modal fade" id="compose-modal" role="dialog" aria-hidden="true">
                <div class="modal-dialog modal-lg" style="width:1100px">
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
                            <div class="row">
                                <div class="form-group col-md-3" id="divVoucherNo">
                                    <label>
                                        Ref No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtVoucherNo" placeholder="Reference No."
                                        maxlength="150" tabindex="1" disabled="disabled"/>
                                </div>
                                <div class="form-group col-md-3" id="divVoucherDate" style="display: none;">
                                    <label>
                                        Entry  Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtVoucherDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="2" id="txtVoucherDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divSupplier">
                                    <label>
                                        Supplier</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlSupplier" class="form-control select2" data-placeholder="Select Supplier" tabindex="3">
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divBank">
                                    <label>
                                        Select A/c</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="4">
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divPaymentMode">
                                    <label>
                                        Payment Mode</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlPaymentMode" class="form-control" tabindex="5">
                                        <option value="0" selected="selected">--Select--</option>
                                        <option value="3">NEFT/RTGS</option>
                                        <option value="4">IMPS</option>
                                        <option value="1">Cash</option>
                                        <option value="2">Cheque</option>
                                        <option value="5">Others</option>
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-2" id="divAmount">
                                    <label>Paid Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtAmount" placeholder="Paid Amount"
                                            maxlength="15" tabindex="6" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divDiscountAmount">
                                    <label>Discount Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtDiscountAmount" placeholder="Discount Amount"
                                            maxlength="15" tabindex="7" autocomplete="off" />
                                    </div>
                                </div>
                                 <div class="form-group col-md-2" id="divOtherCharges">
                                    <label>Courier/Other Charges</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtOtherDiscount" placeholder="Other Discount"
                                            maxlength="15" tabindex="7" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divTotalAmount">
                                    <label>Total Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtTotalAmount" style="font-size: 104%; font-weight: bold;" placeholder="Total Amount"
                                            maxlength="15" tabindex="-1" autocomplete="off" disabled="disabled" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-3" id="divCollectionDate">
                                      <label id="lblCollection">Payment Date</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtCollectionDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="8" id="txtCollectionDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
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
                                    <div class="form-group col-md-3" id="divIssueDate" style="display: none">
                                        <label>
                                            Issued Date</label><span class="text-danger">*</span>
                                        <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtIssueDate" data-link-format="dd/MM/yyyy">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar"></i>
                                            </div>
                                            <input class="form-control pull-right" tabindex="10" id="txtIssueDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
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
                                <div class="form-group col-md-2" id="divBalanceAmount">
                                    <label>Balance Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtBalanceAmount" style="font-size: 104%; font-weight: bold;" placeholder="Amount"
                                            maxlength="15" tabindex="-1" disabled="disabled" autocomplete="off" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divPendingAmount">
                                    <label>Pending Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtPendingAmount" style="font-size: 104%; font-weight: bold;" placeholder="Amount"
                                            maxlength="15" tabindex="-1" disabled="disabled" autocomplete="off" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                                 <div class="form-group col-md-2" id="divOnAccount">
                                    <label>On Account</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtOnAccount" style="font-size: 104%; font-weight: bold;" placeholder="Amount"
                                            maxlength="15" tabindex="-1" disabled="disabled" autocomplete="off" />
                                    </div>
                                </div>
                                <div class="form-group col-md-1" id="divcheckbox">
                                        <input type="checkbox" id="chkAddOnAccount" style="margin: 30px 0 0 0; width:18px; height:18px;"  tabindex="-1" />
                                </div>
                                <div class="form-group col-md-2">
                                    <label>
                                        Upload</label>
                                    <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                        runat="server" ID="imgUpload1" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="214px"
                                        OnUploadedComplete="UploadedComplete" OnClientUploadComplete="DocumentuploadComplete" />
                                    <input type="hidden" id="hdnImgupload1" tabindex="12" />
                                    <img src="" id="imgUpload1_view" alt="" class="preview_img" style="width: 280px;" />

                                </div>
                                <div class="form-group col-md-3" id="divDescription">
                                    <label>
                                        Notes</label>
                                    <input id="txtDescription" class="form-control" maxlength="255" tabindex="13" autocomplete="off" />
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
                            <div class="box box-primary box-solid" id="A">
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
                            <button type="submit" class="btn btn-info pull-left" id="btnSave" tabindex="14">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-left" id="btnUpdate" tabindex="15">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button id="btnPrint" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="16">
                                <i class="fa fa-print"></i>&nbsp;&nbsp;Save & Print</button>
                            <button id="btnPrintbill" type="button" class="btn btn-info btnPrint margin pull-left" tabindex="17">
                                <i class="fa fa-print"></i>&nbsp;&nbsp; Print</button>
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
        var gvalue = []; var iAmount = 0; var iCount = 0;

        var pageUrl = '<%=ResolveUrl("~/frmDPayment.aspx") %>';
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
            GetSupplierList("ddlSupplier");

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
                    $("#hdnImgupload1").val("./images/Documents/Payment/" + r.d);
                    $("#imgUpload1_view").val("/images/Documents/Payment/" + r.d);
                    // $get("imgUpload1").src = "./images/Documents/Payment/" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        $("#divPendingBillList").on('change', "input[type='checkbox']", function (e) {
            var Balance = 0; var iDiscount = 0; var iothers = 0;
            $.each($("input[name='PurchaseName']:checked"), function () {
                for (var i = 0; i < gvalue.length; i++) {
                    if (gvalue[i].PurchaseID == $(this).val()) {
                        //    iAmount = parseFloat(iAmount) + gvalue[i].BalanceAmount;
                        Balance = parseFloat(Balance) + gvalue[i].BalanceAmount;
                        iAmount = Balance;
                        iCount = 1
                    }
                }
            });
            $.each($("input[name='DiscountName']:checked"), function () {
                for (var i = 0; i < gvalue.length; i++) {
                    if (gvalue[i].PurchaseID == $(this).val()) {
                        iDiscount = parseFloat(iDiscount) + parseFloat(gvalue[i].PaymentDiscount);
                        iothers = parseFloat(iothers) + parseFloat(gvalue[i].OtherDiscount);
                        //$("#chk_" + gvalue[i].PurchaseID).prop("checked", true);
                        iCount = 1
                    }
                }
            });
            if (Balance == 0) {
                iAmount = 0;
                iCount = 0;
            }
            $("#txtDiscountAmount").val(iDiscount);
            $("#txtOtherDiscount").val(iothers);
            Balance = Balance - parseFloat($("#txtDiscountAmount").val()) - parseFloat($("#txtOtherDiscount").val());
            $("#txtAmount").val(Balance).change();
           <%-- if ($("#txtAmount").val() > 0)
                $('#<%= Label2.ClientID %>').text(convertNumberToWords($("#txtAmount").val()));
            else
                $('#<%= Label2.ClientID %>').text("");--%>
            if (iCount == 1)
                CalculateTrans();
            else
                CalculateBalanceTrans();

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

        <%--$("#txtAmount").change(function () {
            if ($("#txtAmount").val() > 0)
                $('#<%= Label2.ClientID %>').text(convertNumberToWords($("#txtAmount").val()));
            else
                $('#<%= Label2.ClientID %>').text("");
        });--%>

        function CalculateTrans() {
            var Amount = parseFloat($("#txtTotalAmount").val());
            //var DiscountAmount = parseFloat($("#txtDiscountAmount").val());
            //Amount = Amount + DiscountAmount;
            //console.log(Amount);
            if (Amount < iAmount)
                $("#txtPendingAmount").val(iAmount - Amount);
            else if (Amount == iAmount) {
                var Balance_Amt = 0;
                $("#txtPendingAmount").val(Balance_Amt);
            }
            else {
                $.jGrowl("Paid Amount exceed Balance Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            }
            return false;
        };

        function CalculateBalanceTrans() {
            var Amount = parseFloat($("#txtAmount").val());
            var iBalance = parseFloat($("#txtBalanceAmount").val());

            if (Amount == iBalance || Amount == 0) {
                var Balance_Amt = 0;
                $("#txtPendingAmount").val(Balance_Amt);
            }
            else if (Amount < iBalance)
                $("#txtPendingAmount").val(iBalance - Amount);
            else {
                $.jGrowl("Paid Amount exceed Balance Amount", { sticky: false, theme: 'warning', life: jGrowlLife });
            }
            return false;
        };

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#divRecentTransaction").hide();
            $("#divPendingBill").hide();
            $("#txtBalanceAmount").val(0);
            $("#txtPendingAmount").val(0);
            $("#txtDiscountAmount").val(0);
            $("#txtOtherDiscount").val(0);

            $get("imgUpload1_view").src = "";
            gOPBillingList = [];
            DisplayPendingBillList(gOPBillingList);
            DisplayOPBillingList(gOPBillingList);
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $("#btnPrintbill").hide();
            $("#btnPrint").show();
            $("#ddlSupplier").attr("disabled", false);
            $("#txtAmount").attr("readonly", false);
            $("#txtDiscountAmount").attr("readonly", false);
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Payment");
            $('#compose-modal').modal({ show: true, backdrop: true });
            var d = new Date().getDate();
            var m = new Date().getMonth() + 1; // JavaScript months are 0-11
            var y = new Date().getFullYear();
            $("#txtVoucherDate").val(d + "/" + m + "/" + y);
            $("#txtVoucherNo").focus();
            //$("#btnPrint").prop('disabled', true);
            //$("#btnPrint").visible = false;
            //  $("input").prop('disabled', false);
            return false;
        });
        $("#btnSave,#btnUpdate,#btnPrint").click(function () {
            if (this.id == "btnSave") { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate") { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtVoucherDate").val().trim() == "" || $("#txtVoucherDate").val().trim() == undefined) {
                $.jGrowl("Please select Voucher Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divVoucherDate").addClass('has-error'); $("#txtVoucherDate").focus(); return false;
            } else { $("#divVoucherDate").removeClass('has-error'); }

            if ($("#ddlSupplier").val() == "0" || $("#ddlSupplier").val() == undefined || $("#ddlSupplier").val() == null) {
                $.jGrowl("Please select Voucher Type", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divSupplier").addClass('has-error'); $("#ddlSupplier").focus(); return false;
            } else { $("#divSupplier").removeClass('has-error'); }

            if ($("#ddlBank").val() == "0" || $("#ddlBank").val() == undefined || $("#ddlBank").val() == null) {
                $.jGrowl("Please select Account", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divBank").addClass('has-error'); $("#ddlBank").focus(); return false;
            } else { $("#divBank").removeClass('has-error'); }

            if ($("#ddlPaymentMode").val() == "0" || $("#ddlPaymentMode").val() == undefined || $("#ddlPaymentMode").val() == null) {
                $.jGrowl("Please select Payment Mode", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divPaymentMode").addClass('has-error'); $("#ddlPaymentMode").focus(); return false;
            } else { $("#divPaymentMode").removeClass('has-error'); }

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

            if ($("#ddlPaymentMode").val() != 2) {
                if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                    $.jGrowl("Please select Payment Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                }
            }

            if ($("#ddlPaymentMode").val() == 3 || $("#ddlPaymentMode").val() == 4) {
                if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Reference No.", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
                } else { $("#divChequeNo").removeClass('has-error'); }

                //if ($("#txtIssueDate").val().trim() == "" || $("#txtIssueDate").val().trim() == undefined) {
                //    $.jGrowl("Please select Issue Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                //    $("#divIssueDate").addClass('has-error'); $("#txtIssueDate").focus(); return false;
                //} else { $("#divIssueDate").removeClass('has-error'); }

                //if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                //    $.jGrowl("Please select Payment Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                //    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                //} else { $("#divCollectionDate").removeClass('has-error'); }
            }

            if ($("#ddlPaymentMode").val() == 2) {
                if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Cheque No.", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
                } else { $("#divChequeNo").removeClass('has-error'); }

                if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                    $.jGrowl("Please select Collection Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                } else { $("#divCollectionDate").removeClass('has-error'); }
            }


            var sAgentID = ""; var Balance = 0; var iCount = 0;
            $.each($("input[name='PurchaseName']:checked"), function () {
                sAgentID = sAgentID + $(this).val() + ',';
                for (var i = 0; i < gvalue.length; i++) {
                    if (gvalue[i].PurchaseID == $(this).val()) {
                        Balance = parseFloat(Balance) + gvalue[i].BalanceAmount;
                        iCount = 1
                    }
                }
            });

            if (iCount == 1) {
                if (Balance != (parseFloat($("#txtAmount").val()) + parseFloat($("#txtDiscountAmount").val()) + parseFloat($("#txtOnAccount").val()) + parseFloat($("#txtOtherDiscount").val()))) {
                    $.jGrowl("Paid amount must tally with Balance amount", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divAmount").addClass('has-error'); $("#txtAmount").focus(); return false;
                }
            }

            sAgentID = sAgentID.slice(0, -1);

            var Obj = new Object();
            Obj.PaymentID = 0;
            Obj.VoucherNo = $("#txtVoucherNo").val().trim();
            Obj.sVoucherDate = $("#txtVoucherDate").val();

            var objSupplier = new Object();
            objSupplier.SupplierID = $("#ddlSupplier").val();
            Obj.Supplier = objSupplier;

            var objBank = new Object();
            objBank.LedgerID = $("#ddlBank").val();
            Obj.Bank = objBank;
            Obj.PurchaseIDs = sAgentID;
            Obj.BillType = 0;
            Obj.DocumentPath = $("#hdnImgupload1").val();
            Obj.DiscountAmount = parseFloat($("#txtDiscountAmount").val().trim());
            Obj.OtherDiscount = parseFloat($("#txtOtherDiscount").val().trim());
            Obj.PaymentModeID = $("#ddlPaymentMode").val();
            Obj.Amount = parseFloat($("#txtAmount").val().trim());
            Obj.sCollectionDate = $("#txtCollectionDate").val();
            if ($("#ddlPaymentMode").val() == 2 || $("#ddlPaymentMode").val() == 3 || $("#ddlPaymentMode").val() == 4) {
                Obj.ChequeNo = $("#txtChequeNo").val().trim();
                Obj.sIssueDate = $("#txtIssueDate").val();

                Obj.IssuedBy = $("#txtIssuedBy").val().trim();
                Obj.Charges = parseFloat($("#txtCharges").val().trim());
            }
            Obj.Description = $("#txtDescription").val().trim();

            if ($("#chkAddOnAccount").prop('checked') == true)
                Obj.OnAccount = parseFloat($("#txtOnAccount").val().trim());
            else
                Obj.OnAccount = 0;

            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.PaymentID = $("#hdnID").val();
                sMethodName = "UpdatePayment";
            }
            else { sMethodName = "AddPayment"; }

            SaveandUpdatePayment(Obj, sMethodName);

            if (this.id == "btnPrint") {
                SetSessionValue("PrintPaymentID", $("#hdnID").val());
                var myWindow = window.open("PrintPayment.aspx", "MsgWindow");
            }
            else {
                $("#hdnID").val(0);
            }
            return false;
        });
        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            GetRecord();
            return false;
        });

        $("#ddlSupplier").change(function () {
            $("#txtAmount").val("0");
            $("#txtDiscountAmount").val("0");
            $("#txtTotalAmount").val("0");
            if ($("#txtAmount").val() == 0)
                $('#<%= Label2.ClientID %>').text("");
            if ($("#ddlSupplier").val() > 0) {
                GetOnAccountAmount($("#ddlSupplier").val());
                GetLastTransaction();
                GetPendingBill();
            }
        });


        function GetOnAccountAmount(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetOnAccountAmount",
                data: JSON.stringify({ ID: id, Type: 'P' }),
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
                                    $("#txtOnAccount").val(obj.OnAccount);
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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



        $("#txtDiscountAmount,#txtAmount,#txtOtherDiscount, #chkAddOnAccount").change(function () {
            var iDiscount = 0; var iAmount = 0, iOnAccount = 0;
            iDiscount = parseFloat($("#txtDiscountAmount").val());
            iOther = parseFloat($("#txtOtherDiscount").val());
            iAmount = parseFloat($("#txtAmount").val());
            iOnAccount = parseFloat($("#txtOnAccount").val());
            if (isNaN(iDiscount)) iDiscount = 0;
            if (isNaN(iAmount)) iAmount = 0;
            if (isNaN(iOnAccount)) iOnAccount = 0;
            if ($("#chkAddOnAccount").prop('checked') == false)
                iOnAccount = 0;
            $("#txtTotalAmount").val((parseFloat(iDiscount) + parseFloat(iAmount) + parseFloat(iOnAccount) + parseFloat(iOther)).toFixed(2));
            if (iCount == 1)
                CalculateTrans();
            //else
            //    CalculateBalanceTrans();
            if ($("#txtAmount").val() > 0)
                $('#<%= Label2.ClientID %>').text(convertNumberToWords($("#txtAmount").val()));
             else
                 $('#<%= Label2.ClientID %>').text("");
        });

        //$("txtDiscountAmount").change(function () {
        //    $("txtCollectionDate").focus();
        //});

        function GetLastTransaction() {
            gOPBillingList = [];
            DisplayOPBillingList(gOPBillingList);
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLastPaymentDetails",
                data: JSON.stringify({ ID: $("#ddlSupplier").val(), Type:0 }),
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
                url: "WebServices/VHMSService.svc/GetPendingPurchase",
                data: JSON.stringify({ PublisherID: $("#ddlSupplier").val(), BillType: 0 }),
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
                sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;line-height:0.5;text-align: center'>SN</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Voucher No</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Date</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Bill No</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Bill Date</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>A/c</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Mode</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Amount</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Payment Date</th>";
                sTable += "<th style='line-height:0.5;' class='" + sColorCode + "'>Purchase No</th>";
                sTable += "</tr></thead><tbody id='tblOPBillingList_body'>";
                sTable += "</tbody></table>";
                var sPaymentMode = "";
                $("#divOPBillingList").html(sTable);
                for (var i = 0; i < gData.length; i++) {

                    if (gData[i].PaymentModeID == 1)
                        sPaymentMode = "Cash";
                    else if (gData[i].PaymentModeID == 2)
                        sPaymentMode = "Cheque";
                    else if (gData[i].PaymentModeID == 3)
                        sPaymentMode = "NEFT/RTGS";
                    else if (gData[i].PaymentModeID == 4)
                        sPaymentMode = "IMPS";
                    else if (gData[i].PaymentModeID == 5)
                        sPaymentMode = "Others";

                    sTable = "<tr><td style='line-height:0.5;' id='" + gData[i].SNo + "'>" + sCount + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].VoucherNo + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].sVoucherDate + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].BillNo + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].sBillDate + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].Bank.LedgerName + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + sPaymentMode + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].Amount + "</td>";
                    if (gData[i].sCollectionDate != "01-01-1900")
                        sTable += "<td style='line-height:0.5;'>" + gData[i].sCollectionDate + "</td>";
                    else
                        table += "td style='line-height:0.5;'> </td> ";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].PurchaseNo + "</td>";
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
            $("#txtPendingAmount").val(0);
            if (gData.length >= 5) { $("#divPendingBillList").css({ 'height': '0px', 'min-height': '200px', 'overflow': 'auto' }); }
            else { $("#divPendingBillList").css({ 'height': '', 'min-height': '' }); }

            if (gData.length > 0) {
                $("#divPendingBill").show();
                sTable = "<table id='tblPendingBillList' class='table no-margin table-condensed table-hover'>";
                sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                sTable += "<th class='" + sColorCode + "'>Bill No</th>";
                sTable += "<th class='" + sColorCode + "'>Date</th>";
                sTable += "<th class='" + sColorCode + "'>Purchase No</th>";
                sTable += "<th class='" + sColorCode + "'>Net Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Rtn. Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Paid Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Balance Amount</th>";
                sTable += "<th class='" + sColorCode + "'>Payment Discount</th>";
                sTable += "<th class='" + sColorCode + "'>Courier/Other Charges</th>";
                sTable += "</tr></thead><tbody id='tblPendingBillList_body'>";
                sTable += "</tbody></table>";
                var sPaymentMode = ""; var BalanceValue = 0;
                $("#divPendingBillList").html(sTable);
                for (var i = 0; i < gData.length; i++) {



                    sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                    if ($("#hdnID").val() > 0) {
                        sTable += "<td><label><input id='chk_" + gData[i].PurchaseID + "' type='checkbox' value='" + gData[i].PurchaseID + "'  name='PurchaseName' disabled='disabled'/>&nbsp;" + gData[i].BillNo + "</label></td>";
                    }
                    else {
                        sTable += "<td><label><input id='chk_" + gData[i].PurchaseID + "' type='checkbox' value='" + gData[i].PurchaseID + "'  name='PurchaseName'/>&nbsp;" + gData[i].BillNo + "</label></td>";
                    }//sTable += "<td>" + gData[i].PurchaseNo + "</td>";
                    sTable += "<td>" + gData[i].sBillDate + "</td>";
                    sTable += "<td>" + gData[i].PurchaseNo + "</td>";
                    sTable += "<td>" + gData[i].NetAmount + "</td>";
                    sTable += "<td>" + gData[i].ReturnAmount + "</td>";
                    sTable += "<td>" + gData[i].PaidAmount + "</td>";
                    sTable += "<td>" + gData[i].BalanceAmount + "</td>";
                    
                    if ($("#hdnID").val() > 0) {
                        sTable += "<td><label><input id='chkdiscount_" + gData[i].PurchaseID + "' type='checkbox' value='" + gData[i].PurchaseID + "'  name='DiscountName' disabled='disabled'/>&nbsp;" + gData[i].PaymentDiscount + "</label></td>";
                    } else {
                        sTable += "<td><label><input id='chkdiscount_" + gData[i].PurchaseID + "' type='checkbox' value='" + gData[i].PurchaseID + "'  name='DiscountName'/>&nbsp;" + gData[i].PaymentDiscount + "</label></td>";
                    }
                    var otCharges = parseFloat(gData[i].OtherCharges) + parseFloat(gData[i].CourierCharges);
                    sTable += "<td>" + otCharges + "</td>";
                    sTable += "</tr>";
                    
                    sCount = sCount + 1;
                    $("#tblPendingBillList_body").append(sTable);
                    BalanceValue = parseFloat(BalanceValue) + parseFloat(gData[i].BalanceAmount);
                    var objAgent = new Object();
                    objAgent.PurchaseID = gData[i].PurchaseID;
                    objAgent.BalanceAmount = gData[i].BalanceAmount;
                    objAgent.PaymentDiscount = gData[i].PaymentDiscount;
                    objAgent.OtherDiscount = otCharges;
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

        $("#ddlPaymentMode").change(function () {
            $("#divChequeDetails").hide();
            var iPaymentMode = $("#ddlPaymentMode").val();
            if (iPaymentMode != undefined && iPaymentMode > 0) {
                if (iPaymentMode == 2 || iPaymentMode == 3 || iPaymentMode == 4)
                    $("#divChequeDetails").show();
                if (iPaymentMode == 2) {
                    $("#lblCollection").text("Collection Date");
                    $('#<%= Label1.ClientID %>').text("Cheque No #");
                    $("#txtChequeNo").attr("placeholder", "Cheque No");
                    $("#divIssuedBy").show();
                }
                else {
                    $("#lblCollection").text("Payment Date");
                    $('#<%= Label1.ClientID %>').text("Reference No #");
                    $("#txtChequeNo").attr("placeholder", "Reference No");
                    $("#divIssuedBy").hide();
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
            $("#ddlSupplier").val(null).change();
            $("#ddlBank").val(null).change();
            $("#ddlPaymentMode").val(0);
            $("#txtAmount").val("0");
            $("#txtAmount").val("0");
            $("#txtChequeNo").val("");
            $("#txtIssueDate").val("");
            $("#chkAddOnAccount").prop("checked", false);
            $("#txtOnAccount").val("0");
            $("#txtCollectionDate").val("");
            $("#txtIssuedBy").val("");
            $("#txtDescription").val("");
            $("#txtCharges").val("0");
            $("#txtTotalAmount").val("0");

            $("#divChequeDetails").hide();
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

        function GetSupplierList(ddlname) {
            var sControlName = "#" + ddlname;
            dProgress(true);
            $(sControlName).empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSupplier",
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
                                        if (obj[index].IsActive)
                                            $(sControlName).append("<option value='" + obj[index].SupplierID + "'>" + obj[index].SupplierName + "</option>");
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

        function SaveandUpdatePayment(Obj, sMethodName) {
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
                                $("#hdnID").val(objResponse.Value);
                                if (sMethodName == "AddPayment") {
                                    $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife });

                                }
                                else if (sMethodName == "UpdatePayment") { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Payment_A_01" || objResponse.Value == "Payment_U_01") {
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
                url: "WebServices/VHMSService.svc/GetPayment",
                data: JSON.stringify({ Type: 0 }),
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
                                    var sPaymentMode = "", sBillNo = "", InvoiceNo = "";
                                    for (var index = 0; index < obj.length; index++) {

                                        if (obj[index].PaymentModeID == 1)
                                            sPaymentMode = "Cash";
                                        else if (obj[index].PaymentModeID == 2)
                                            sPaymentMode = "Cheque";
                                        else if (obj[index].PaymentModeID == 3)
                                            sPaymentMode = "NEFT/RTGS";
                                        else if (obj[index].PaymentModeID == 4)
                                            sPaymentMode = "IMPS";
                                        else if (obj[index].PaymentModeID == 5)
                                            sPaymentMode = "Others";
                                        var InvoiceNo1 = obj[index].BillNos;
                                        //  for (var i = InvoiceNo1; i <= InvoiceNo1.length; i++)
                                        var result = InvoiceNo1.split(',').join('');
                                        var result1 = result.split('.00').join('<br />');
                                        var table = "<tr id='" + obj[index].PaymentID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].VoucherNo + "</td>";
                                        table += "<td>" + obj[index].sVoucherDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Supplier.SupplierName + "</td>";
                                        table += "<td class='hidden-xs'>" + result1 + "</td>";
                                        if (obj[index].sCollectionDate != "01-01-1900")
                                            table += "<td class='hidden-xs'>" + obj[index].sCollectionDate + "</td>";
                                        else
                                            table += "<td class='hidden-xs'> </td> ";
                                        table += "<td class='hidden-xs'>" + parseFloat(obj[index].Amount).toFixed(2) + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Bank.LedgerName + "</td>";
                                        table += "<td class='hidden-xs'>" + sPaymentMode + "</td>";

                                        if (ActionView == "1") { table += "<td><a href='#' id=" + obj[index].PaymentID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionUpdate == "1") { table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PaymentID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        if (ActionDelete == "1") { table += "<td ><a href='#' id=" + obj[index].PaymentID + " class='Delete' title='Click here to Delete'><i class='fa fa-lg fa-trash-o text-red'/></a></td>"; }
                                        else { table += "<td></td>"; }

                                        table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PaymentID + " class='Print' title='Click here to Print Payment'></i><i class='fa fa-print text-green'/></a></td>";

                                        if (obj[index].PaymentModeID == 2)
                                            table += "<td style='text-align:center;'><a href='#' id=" + obj[index].PaymentID + " class='PrintCheque' title='Click here to Print Cheque Payment'></i><i class='fa fa-print text-blue'/></a></td>";
                                        else
                                            table += "<td></td>";

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Payment");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").hide();
                                            $("#btnPrint").hide();
                                            $("#btnPrintbill").show();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Edit").click(function () {
                                        if (ActionView == "1") {
                                            EditRecord($(this).parent().parent()[0].id);
                                            $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;Edit Payment");
                                            $("#btnSave").hide();
                                            $("#btnUpdate").show();
                                            $("#btnPrint").hide();
                                            $("#ddlSupplier").attr("disabled", true);
                                            $("#txtAmount").attr("readonly", true);
                                            $("#txtDiscountAmount").attr("readonly", true);
                                            $("#btnPrintbill").show();
                                        }
                                        else {
                                            $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                            return false;
                                        }
                                    });
                                    $(".Print").click(function () {
                                        SetSessionValue("PrintPaymentID", parseInt($(this).parent().parent()[0].id));
                                        var myWindow = window.open("PrintPayment.aspx", "MsgWindow");
                                    });
                                    $(".PrintCheque").click(function () {
                                        SetSessionValue("PrintPaymentID", parseInt($(this).parent().parent()[0].id));
                                        var myWindow = window.open("PrintChequePayment.aspx", "MsgWindow");
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
                                    { "sWidth": "8%" },
                                    { "sWidth": "8%" },
                                    { "sWidth": "15%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "10%" },
                                    { "sWidth": "16%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" },
                                    { "sWidth": "3%" },
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

        $("#btnPrintbill").click(function () {
            SetSessionValue("PrintPaymentID", $("#hdnID").val());
            var myWindow = window.open("PrintPayment.aspx", "MsgWindow");
        });

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetPaymentByID",
                data: JSON.stringify({ ID: id, Type:0 }),
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

                                    $("#hdnID").val(obj.PaymentID);
                                    $("#txtVoucherNo").val(obj.VoucherNo);
                                    $("#txtVoucherDate").val(obj.sVoucherDate);
                                    $("#ddlSupplier").val(obj.Supplier.SupplierID).change();
                                    $("#ddlBank").val(obj.Bank.LedgerID).change();
                                    $("#ddlPaymentMode").val(obj.PaymentModeID).change();
                                    $("#txtAmount").val(obj.Amount).change();
                                    $("[id*=imgUpload1]").attr("src", obj.DocumentPath);
                                    $("#txtOnAccount").val(obj.OnAccount);
                                    if (obj.OnAccount > 0)
                                        $("#chkAddOnAccount").prop("checked", true);

                                    $("#txtDiscountAmount").val(obj.DiscountAmount).change();
                                    $("#txtOtherDiscount").val(obj.OtherDiscount).change();
                                    $("[id*=imgUpload1]").attr("src", obj.DocumentPath);

                                   

                                    if (obj.PaymentModeID == 2 || obj.PaymentModeID == 3 || obj.PaymentModeID == 4) {
                                        $("#divChequeDetails").show();
                                        $("#txtChequeNo").val(obj.ChequeNo);
                                        //  $("#txtIssueDate").val(obj.sIssueDate);
                                        $("#txtCollectionDate").val(obj.sCollectionDate);
                                        $("#txtIssuedBy").val(obj.IssuedBy);
                                        $("#txtCharges").val(obj.Charges);

                                    }
                                    $("#txtDescription").val(obj.Description);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Payment");
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
                url: "WebServices/VHMSService.svc/DeletePayment",
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
                            else if (objResponse.Value == "Payment_R_01" || objResponse.Value == "Payment_D_01") {
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

