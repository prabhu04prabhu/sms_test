<%@ Page Title="Branch Move" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmBranchMove.aspx.cs" Inherits="frmBranchMove" %>


<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Branch Move
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Transaction</a></li>
                <li class="active">Branch Move</li>
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
                                                <th>Move No
                                                </th>
                                                <th>Date
                                                </th>
                                                <th class="hidden-xs">From Branch
                                                </th>
                                                <th class="hidden-xs">To Branch
                                                </th>
                                                  <th class="hidden-xs">Total Weight
                                                    </th>
                                                <th class="hidden-xs">Total Quantity
                                                </th>
                                                <th>Status
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
                                                    <th>Move No
                                                    </th>
                                                    <th>Date
                                                    </th>
                                                    <th class="hidden-xs">From Branch
                                                    </th>
                                                    <th class="hidden-xs">To Branch
                                                    </th>
                                                     <th class="hidden-xs">Total Weight
                                                    </th>
                                                    <th class="hidden-xs">Total Quantity
                                                    </th>
                                                    <th>Status
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
                                    <div class="form-group col-md-2">
                                        <label>
                                            Move No</label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <input type="text" class="form-control" id="txtMoveNo" maxlength="50" readonly="true" tabindex="-1" />
                                    </div>
                                    <div class="form-group col-md-2"></div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            Date</label><span class="text-danger">*</span>
                                    </div>
                                    <div class="form-group col-md-3" id="divDOB">
                                        <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDate"
                                            data-link-format="dd/MM/yyyy">
                                            <div class="input-group-addon">
                                                <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                            </div>
                                            <input type="text" class="form-control pull-right" tabindex="1" id="txtDate" readonly="true" disabled="disabled" />
                                        </div>
                                        <div class="form-group col-md-1"></div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label>
                                            From Branch</label><span class="text-danger">*</span>
                                    </div>
                                    <div class="form-group col-md-4" id="divFromBranch">
                                        <select id="ddlFromBranch" class="form-control" tabindex="2" disabled="disabled">
                                            <option selected="selected" value="0">--Select From Branch--</option>
                                        </select>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            To Branch</label><span class="text-danger">*</span>
                                    </div>
                                    <div class="form-group col-md-4" id="divToBranch">
                                        <select id="ddlToBranch" class="form-control" tabindex="3">
                                            <option selected="selected" value="0">--Select To Branch--</option>
                                        </select>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-md-2">
                                        <label>
                                            From Address</label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <textarea id="txtFromAddress" class="form-control" maxlength="250" tabindex="-1" rows="3" readonly="readonly"></textarea>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            To Address</label>
                                    </div>
                                    <div class="form-group col-md-4">
                                        <textarea id="txtToAddress" class="form-control" maxlength="250" tabindex="-1" rows="3" readonly="readonly"></textarea>
                                    </div>
                                </div>
                                <div class="box box-primary box-solid">
                                    <div class="box-header">
                                        Particulars
                                    </div>
                                    <div class="box-body">
                                        <div class="row">
                                            <div class="form-group col-md-2" id="divBarcode">
                                                <label>
                                                    Barcode</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtBarcode" placeholder="Barcode"
                                                    maxlength="20" tabindex="4" />
                                                <input type="hidden" id="hdntransID" />
                                            </div>
                                            <div class="form-group col-md-3" id="divCategory">
                                                <label>
                                                    Category</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtCategory" placeholder="Category"
                                                    maxlength="50" tabindex="-1" readonly="true" />
                                            </div>
                                            <div class="form-group col-md-3" id="divProduct">
                                                <label>
                                                    Product</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtProduct" placeholder="Product"
                                                    maxlength="50" tabindex="-1" readonly="true" />
                                            </div>
                                            <div class="form-group col-md-3" id="divGross">
                                                <label>
                                                    Gross Weight</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtGross" placeholder="Gross Weight"
                                                    maxlength="50" tabindex="-1" readonly="true" />
                                            </div>
                                            <div class="form-group col-md-2" id="divQuantity">
                                                <label>
                                                    Quantity</label><span class="text-danger">*</span>
                                                <input type="text" class="form-control TRSearch" id="txtQuantity" placeholder="Total"
                                                    maxlength="50" tabindex="-1" readonly="true" />
                                            </div>

                                            <div class="form-group col-md-2 pull-right">
                                                <div class="margin">
                                                    <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="6">
                                                        <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                    <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="7">
                                                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="table-responsive">
                                    <div id="divBranchMoveList">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-12"></div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-12"></div>
                                </div>
                                <div class="row">
                                    <div class="form-group col-md-4">
                                    </div>
                                     <div class="form-group col-md-2">
                                        <label>
                                            Total Gross Weight</label>
                                    </div>
                                     <div class="form-group col-md-2">
                                        <input type="text" class="form-control" id="txtGrossWeight" maxlength="50" readonly="true" tabindex="-1" />
                                    </div>
                                    <div class="form-group col-md-2">
                                        <label>
                                            Total Quantity</label>
                                    </div>
                                    <div class="form-group col-md-2">
                                        <input type="text" class="form-control" id="txtTotalQuantity" maxlength="50" readonly="true" tabindex="-1" />
                                    </div>
                                    <div class="form-group col-md-2"></div>
                                </div>
                            </div>
                            <div class="modal-footer clearfix">
                                <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="11">
                                    <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                                <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="9">
                                    <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                                <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="10">
                                    <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            </div>
                        </div>
                    </div>
                </div>
                   <div class="modal fade" id="Renewalmodal" tabindex="-1" role="dialog" aria-hidden="true">
                <div class="modal-dialog">
                    <div class="modal-content">
                       <%-- <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                &times;</button>
                            <h4 class="modal-title"></h4>
                        </div>--%>
                        <div class="modal-body">
                      <div class="form-group col-md-4" id="divOtherPassword">
                                    <label>
                                        Enter Special Password</label><span class="text-danger">*</span>
                                    <input type="password" class="form-control" id="txtOtherPassword" placeholder="Special Password" maxlength="512"
                                        tabindex="5" />
                                </div>
                        <div class="modal-footer clearfix">
                            <button type="submit" class="btn btn-info pull-right" id="btnSubmit" tabindex="21">
                                &nbsp;&nbsp;
                                OK</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnBranchMoveID" />
     <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnIDS" />
    <input type="hidden" id="hdnBranchMoveTransID" />
    <input type="hidden" id="hdnStockID" />
    <input type="hidden" id="hdnOpeningDate" />
    <script src="UserDefined_Js/JBranchMove.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JBranchMove.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var _BranchID = '<%=Session["BranchID"]%>';
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




