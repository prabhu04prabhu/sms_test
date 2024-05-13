<%@ Page Title="Jobwork Vendor" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmBVendor.aspx.cs" Inherits="frmBVendor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Jobwork Vendor
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Billing</a></li>
                <li class="active">Jobwork Vendor</li>
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
                                            <th>Vendor
                                            </th>
                                            <th>Vendor Code
                                            </th>
                                            <th class='hidden-xs'>Phone No
                                            </th>
                                            <th class='hidden-xs'>Address
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
                                    <input type="text" class="form-control" id="txtName" style="text-transform: uppercase" placeholder="Vendor Name"
                                        maxlength="255" tabindex="1" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divCode">
                                <label>
                                    Vendor Code</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtCode" placeholder="Vendor Code"
                                    maxlength="10" tabindex="2" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-3" id="divPhoneNo1">
                                <label>
                                    PhoneNo1</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtPhoneNo1" placeholder="Phone No" maxlength="13"
                                        tabindex="3" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-3" id="divPhoneNo2">
                                <label>
                                    PhoneNo2</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtPhoneNo2" placeholder="Phone No" maxlength="13"
                                        tabindex="4" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-6" id="divAddress">
                                <label>
                                    Address</label>
                                <textarea id="txtAddress" class="form-control" maxlength="250" tabindex="5" rows="3" aria-autocomplete="none"></textarea>
                            </div>
                            <div class="form-group col-md-3" id="divPANNo">
                                <label>
                                    PAN No</label>
                                <div class="input-group">
                                    <div class="input-group-addon"></div>
                                    <input type="text" class="form-control" id="txtPANNo" placeholder="PAN No" maxlength="13"
                                        tabindex="6" autocomplete="off" />
                                </div>
                            </div>


                            <div class="form-group col-md-2">
                                <div class="checkbox" style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="7" />Active
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-6">
                                <div class="box box-primary box-solid  ">
                                    <div class="box-header">
                                        Work Details
                                    </div>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-5" id="divWork">
                                                <label>
                                                    Job Work</label><span class="text-danger">*</span>
                                                <select id="ddlWorkName" class="form-control select2" data-placeholder="Select Work" tabindex="8"></select>
                                            </div>
                                            <div class="form-group col-md-4" id="divSubTotal">
                                                <label id="lblAmount">
                                                    Amount</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtSubTotal" placeholder="Amount"
                                                    maxlength="12" tabindex="9" onkeypress="return IsNumeric(event)" autocomplete="off" />
                                            </div>
                                            <div class="form-group col-md-1 pull-right">
                                                <div class="margin">
                                                    <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="11">
                                                        <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                    <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="10">
                                                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="table-responsive" style="min-height: 0px;">
                                        <div id="divOPBillingList">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="form-group col-md-6">
                            </div>
                            <div class="form-group col-md-4" id="divAddress1" style="display:none;">
                            <label>
                                Comments</label>
                            <textarea id="txtComments" class="form-control" maxlength="255" tabindex="-1" rows="3" aria-autocomplete="none" style="width:155%"></textarea>
                        </div>

                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSave" type="button" class="btn btn-info pull-left" tabindex="7">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button id="btnUpdate" type="button" class="btn btn-info pull-left" tabindex="8">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="9">
                                <i class="fa fa-times"></i>&nbsp;&nbsp;
                                Close</button>

                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnVendorID" />
    <input type="hidden" id="hdnVendorEntryID" />
    <input type="hidden" id="hdnVendorTransID" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnOPSNo" />
    <input type="hidden" id="hdnOPOrderID" />
    <script src="UserDefined_Js/Billing/JVendor.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Billing/JVendor.js") %>" type="text/javascript"></script>
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
</asp:Content>
