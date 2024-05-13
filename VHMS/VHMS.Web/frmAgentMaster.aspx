<%@ Page Title="Agent Master" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmAgentMaster.aspx.cs" Inherits="frmAgentMaster" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Agent
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li class="active">Agent</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-primary">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-primary">
                    <i class="fa fa-list"></i>&nbsp;&nbsp;List</button>
            </div>
            <br />
            <br />
        </section>
        <section class="content">
            <div class="row" id="divRecords">
                <div class="col-xs-12">
                    <div class="box box-warning">
                        <div class="box-body">
                            <div class="table-responsive">
                                <table id="tblRecord" class="table table-striped table-bordered table-hover bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Agent Name
                                            </th>
                                            <th>Agent Code
                                            </th>
                                            <th>Phone NO
                                            </th>
                                            <th>City
                                            </th>
                                            <th>WhatsAppNo
                                            </th>
                                            <th class='hidden-xs'>Email
                                            </th>
                                            <th class='hidden-xs'>Status
                                            </th>
                                            <th>View
                                            </th>
                                            <th>Edit
                                            </th>
                                            <th>Delete
                                            </th>
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
            <div class="nav-tabs-custom" id="tab-modal">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">General</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="row">
                            <div class="form-group col-md-4" id="divName">
                                <label>
                                    Name</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                    <input type="text" class="form-control" id="txtName" style="text-transform: uppercase" placeholder="Agent Name"
                                        maxlength="255" tabindex="1" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divCode">
                                <label>
                                    Code</label>
                                <input type="text" class="form-control" id="txtCode" placeholder="Agent Code" style="text-transform: uppercase"
                                    maxlength="4" tabindex="2" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divPhoneNo1">
                                <label>
                                    PhoneNo1</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtPhoneNo1" placeholder="Phone No" maxlength="13"
                                        tabindex="3" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divPhoneNo2">
                                <label>
                                    PhoneNo2</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtPhoneNo2" placeholder="Phone No" maxlength="13"
                                        tabindex="4" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divWhatsapp">
                                <label>
                                    WhatsApp No</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-whatsapp"></i></div>
                                    <input type="text" class="form-control" id="txtWhatsapp" placeholder="WhatsApp No" maxlength="13"
                                        tabindex="5" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-12" id="divAddress">
                                <label>
                                    Address</label><span class="text-danger">*</span>
                                <textarea id="txtAddress" class="form-control" maxlength="255" tabindex="6" rows="3" aria-autocomplete="none"></textarea>
                            </div>
                            <div class="form-group col-md-3" id="divEmail">
                                <label>
                                    Email</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                    <input type="text" class="form-control" id="txtEmail" placeholder="Email" maxlength="50" tabindex="7" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divState">
                                <label>
                                    State</label><span class="text-danger">*</span>
                                <select id="ddlState" class="form-control select2" data-placeholder="Select State" tabindex="8">
                                </select>
                            </div>
                            <div class="form-group col-md-2" id="divCity">
                                <label>
                                    City</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtCity" placeholder="City"
                                    maxlength="100" tabindex="9" autocomplete="off" />
                            </div>
                               <div class="form-group col-md-2 " id="divCommissionPercentage">
                                <label>
                                    Commission Percentage
                                </label>
                                <input type="text" class="form-control TRSearch" id="txtCommissionPercentage" placeholder="Commission Percentage"
                                    maxlength="12" tabindex="18" onkeypress="return IsNumeric(event)" />
                            </div>
                             <div class="divHide" style="display:none;">
                            <div class="form-group col-md-2" id="divAadharNo">
                                <label>
                                    Aadhar No</label>
                                <input type="text" class="form-control" id="txtAadharNo" placeholder="Please enter AadharNo"
                                    maxlength="12" tabindex="10" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divPanNo">
                                <label>
                                    Pan No</label>
                                <input type="text" class="form-control" id="txtPanNo" placeholder="Please enter PanNo"
                                    maxlength="15" tabindex="11" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-3" id="divAccountHolderName">
                                <label>
                                    Account Holder Name</label>
                                <input type="text" class="form-control" id="txtAccountHolderName" placeholder="Please enter AccountHolder Name"
                                    maxlength="120" tabindex="12" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divBankName">
                                <label>
                                    Bank Name</label>
                                <input type="text" class="form-control" id="txtBankName" placeholder="Bank Name"
                                    maxlength="100" tabindex="13" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divBranchName">
                                <label>
                                    Branch Name</label>
                                <input type="text" class="form-control" id="txtBranchName" placeholder="Please enter Branch Name"
                                    maxlength="120" tabindex="14" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divAccountNo">
                                <label>
                                    Account No</label>
                                <input type="text" class="form-control" id="txtAccountNo" placeholder="Account No" maxlength="20" onkeypress="return IsNumeric(event)"
                                    tabindex="15" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divIFSCCode">
                                <label>
                                    IFSC Code</label>
                                <input type="text" class="form-control" id="txtIFSCCode" placeholder="IFSC Code"
                                    maxlength="15" tabindex="16" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divCommissionType">
                                <label>
                                    Commission Type</label><span class="text-danger">*</span>
                                <select id="ddlCommissionType" class="form-control" tabindex="17">
                                    <option value="Amount" selected="selected">Amount Based</option>
                                    <option value="Item">Item Based</option>
                                </select>
                            </div>
                         
                            <div class="form-group col-md-2" id="divCommissionAmount">
                                <label>
                                    Commission Amount
                                </label>
                                <input type="text" class="form-control TRSearch" id="txtCommissionAmount" placeholder="CommissionAmount"
                                    maxlength="12" tabindex="19" onkeypress="return IsNumeric(event)" />
                            </div> </div>
                            <div class="form-group col-md-3 " style="margin-top:20px;">
                                <div class="checkbox">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="20" />Active
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSave" type="button" class="btn btn-info pull-left" tabindex="21">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button id="btnUpdate" type="button" class="btn btn-info pull-left" tabindex="22">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="23">
                                <i class="fa fa-times"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnAgentID" />
    <script src="UserDefined_Js/Billing/jAgent.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Billing/jAgent.js") %>" type="text/javascript"></script>
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

        function onlyAlphabets(e, t) {
            try {
                if (window.event) {
                    var charCode = window.event.keyCode;
                }
                else if (e) {
                    var charCode = e.which;
                }
                else { return true; }
                if ((charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123))
                    return true;
                else
                    return false;
            }
            catch (err) {
                alert(err.Description);
            }
        }
    </script>
</asp:Content>
