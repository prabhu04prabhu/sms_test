<%@ Page Title="Dashboard" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmDefault.aspx.cs" Inherits="frmDefault" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <link rel="stylesheet" href="plugins/fontawesome-free/css/all.min.css" />
    <link rel="stylesheet" href="https://code.ionicframework.com/ionicons/2.0.1/css/ionicons.min.css" />
    <link rel="stylesheet" href="plugins/overlayScrollbars/css/OverlayScrollbars.min.css" />
    <%--<link rel="stylesheet" href="plugins/tempusdominus-bootstrap-4/css/tempusdominus-bootstrap-4.min.css" />--%>
    <link rel="stylesheet" href="plugins/icheck-bootstrap/icheck-bootstrap.min.css" />
    <link rel="stylesheet" href="plugins/jqvmap/jqvmap.min.css" />
    <link rel="stylesheet" href="css/adminlte.min.css" />
    <link rel="stylesheet" href="plugins/summernote/summernote-bs4.css" />
    <link href="https://fonts.googleapis.com/css?family=Source+Sans+Pro:300,400,400i,700" rel="stylesheet" />
    <script src="plugins/jquery/jquery.min.js"></script>
    <script src="plugins/bootstrap/js/bootstrap.bundle.min.js"></script>
    <script src="plugins/overlayScrollbars/js/jquery.overlayScrollbars.min.js"></script>
    <script src="JS/adminlte.js"></script>
    <script src="JS/demo.js"></script>
    <script src="plugins/jquery-mousewheel/jquery.mousewheel.js"></script>
    <%--<script src="plugins/tempusdominus-bootstrap-4/js/tempusdominus-bootstrap-4.min.js"></script>--%>
    <script src="plugins/summernote/summernote-bs4.min.js"></script>
    <script src="plugins/raphael/raphael.min.js"></script>
    <script src="plugins/jquery-mapael/jquery.mapael.min.js"></script>
    <script src="plugins/jquery-mapael/maps/usa_states.min.js"></script>
    <script src="plugins/chart.js/Chart.min.js"></script>
    <script src="plugins/jqvmap/jquery.vmap.min.js"></script>
    <script src="plugins/jqvmap/maps/jquery.vmap.usa.js"></script>
    <script src="plugins/jquery-knob/jquery.knob.min.js"></script>
    <script src="plugins/jquery-ui/jquery-ui.min.js"></script>
    <script src="plugins/sparklines/sparkline.js"></script>
    <script src="plugins/moment/moment.min.js"></script>
    <script>
        $.widget.bridge('uibutton', $.ui.button)
    </script>

</asp:Content>
<asp:Content ID="Cokntent2" ContentPlaceHolderID="VHMSWebContent" runat="Server">

    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
        </section>
        <section class="content">
            <div class="container-fluid">
                <div class="row" style="display:none;">
                    <button type="button" class="btn btn-success pull-right" id="btnClear" tabindex="18">
                        <i class="fa fa-success"></i>&nbsp;&nbsp;
                                clear bill</button>
                    <!-- Info boxes -->
                </div>
                <div class="row">
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box" style="background-color: #17a2b8!important;">
                            <div class="inner">
                                <h3>
                                    <label id="lblTotalProduct"></label>
                                </h3>
                                <p>Products</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-bag"></i>
                            </div>
                            <a href="frmProduct.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box" style="background-color: #28a745!important;">
                            <div class="inner">
                                <h3>
                                    <label id="lblTotalPurchase"></label>
                                </h3>
                                <p>Purchase</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-stats-bars"></i>
                            </div>
                            <a href="frmPurchase.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box" style="background-color: #ffc107!important;">
                            <div class="inner">
                                <h3>
                                    <label id="lblTotalSales"></label>
                                </h3>
                                <p>Sales</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-pie-graph"></i>
                            </div>
                            <a href="frmSalesEntry.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                    <div class="col-lg-3 col-6">
                        <!-- small box -->
                        <div class="small-box" style="background-color: #dc3545!important;">
                            <div class="inner">
                                <h3>
                                    <label id="lblTotalCustomer"></label>
                                </h3>
                                <p>Reorder Stock</p>
                            </div>
                            <div class="icon">
                                <i class="ion ion-ios-cart"></i>
                            </div>
                            <a href="frmReorderReport.aspx" class="small-box-footer">More info <i class="fas fa-arrow-circle-right"></i></a>
                        </div>
                    </div>
                    <!-- ./col -->
                </div>
                <!-- /.row -->
                <div class="modal fade" id="compose-modal1" role="dialog" aria-hidden="true" style="display:none;">
                    <div class="modal-dialog modal-lg">
                        <div class="modal-content" style="height: 900px;">
                            <div class="modal-header">
                                <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                                    &times;</button>
                                <h4 class="modal-title"></h4>
                            </div>
                            <div class="modal-body" style="padding: 0px !important;">
                                <div class="box box-primary box-solid">
                                    <div class="box-header">
                                        Pending Bills
                                    </div>
                                    <div class="box-body">
                                        <div class="table-responsive" style="min-height: 247px !important">
                                            <div id="divPendingBillList">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer clearfix">
                                <button type="submit" class="btn btn-info pull-left" id="btnBillSave" tabindex="16">
                                    <i class="fa fa-save"></i>&nbsp;&nbsp;
                                update</button>
                                <button type="button" class="btn btn-danger pull-right" id="btnBillClose" tabindex="18">
                                    <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-8">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title" style="font-size: 1.75rem;">Monthly Recap Report</h3>
                                <div class="card-tools">
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                        <i class="fas fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn btn-tool" data-card-widget="remove">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </div>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <p class="text-center">
                                            <strong>
                                                <label id="lblSaleDuration"></label>
                                            </strong>
                                        </p>

                                        <div class="chart">
                                            <canvas id="salesChart" height="180" style="height: 180px;"></canvas>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                    </div>
                                </div>
                                <!-- /.row -->
                            </div>
                        </div>
                        <!-- /.card -->
                    </div>
                    <div class="col-md-4">
                        <div class="card">
                            <div class="card-header">
                                <h3 class="card-title" style="font-size: 1.75rem;">Customer details</h3>

                                <div class="card-tools">
                                    <button type="button" class="btn btn-tool" data-card-widget="collapse">
                                        <i class="fas fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn btn-tool" data-card-widget="remove">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </div>
                            </div>
                            <!-- /.card-header -->
                            <div class="card-body">
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="chart-responsive">
                                            <canvas id="pieChart" height="150"></canvas>
                                        </div>
                                        <!-- ./chart-responsive -->
                                    </div>
                                    <!-- /.col -->
                                    <div class="col-md-4">
                                        <ul class="chart-legend clearfix">
                                            <li><i class="far fa-circle text-danger"></i>New Customers</li>
                                            <li><i class="far fa-circle text-success"></i>Old Customers</li>
                                            <li><i class="far fa-circle text-warning"></i>VIP Customers</li>
                                            <li><i class="far fa-circle text-info"></i>Wholesale Customers</li>
                                        </ul>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <!-- /.col -->
                </div>
                <!-- /.row -->
                <div class="row">
                    <div class="col-md-6">
                        <div class="card">
                            <div class="card-header border-0">
                                <div class="d-flex justify-content-between">
                                    <h3 class="card-title" style="font-size: 1.75rem;">Weekly Sales </h3>

                                </div>
                            </div>
                            <div class="card-body">
                                <div class="d-flex">

                                    <p class="ml-auto d-flex flex-column text-right">
                                        <span class="text-success">
                                            <i id="PercentageImage" class="fas fa-arrow-down"></i>
                                            <label id="lblPercentage"></label>
                                        </span>
                                        <span class="text-muted">Since last week</span>
                                    </p>
                                </div>
                                <!-- /.d-flex -->

                                <div class="position-relative mb-4">
                                    <canvas id="visitors-chart" height="200"></canvas>
                                </div>

                                <div class="d-flex flex-row justify-content-end">
                                    <span class="mr-2">
                                        <i class="fas fa-square text-primary"></i>This Week
                                    </span>

                                    <span>
                                        <i class="fas fa-square text-gray"></i>Last Week
                                    </span>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="card bg-gradient-info">
                            <div class="card-header border-0">
                                <h3 class="card-title" style="font-size: 1.75rem;">Sales Graph</h3>

                                <div class="card-tools">
                                    <button type="button" class="btn bg-info btn-sm" data-card-widget="collapse">
                                        <i class="fas fa-minus"></i>
                                    </button>
                                    <button type="button" class="btn bg-info btn-sm" data-card-widget="remove">
                                        <i class="fas fa-times"></i>
                                    </button>
                                </div>
                            </div>
                            <div class="card-body">
                                <canvas class="chart" id="line-chart" style="min-height: 250px; height: 250px; max-height: 250px; max-width: 100%;"></canvas>
                            </div>
                            <!-- /.card-body -->
                            <%-- <div class="card-footer bg-transparent">
                                <div class="row">
                                    <div class="col-4 text-center">
                                        <input type="text" class="knob" data-readonly="true" value="20" data-width="60" data-height="60"
                                            data-fgcolor="#39CCCC">

                                        <div class="text-white">Mail-Orders</div>
                                    </div>
                                    <!-- ./col -->
                                    <div class="col-4 text-center">
                                        <input type="text" class="knob" data-readonly="true" value="50" data-width="60" data-height="60"
                                            data-fgcolor="#39CCCC">

                                        <div class="text-white">Online</div>
                                    </div>
                                    <!-- ./col -->
                                    <div class="col-4 text-center">
                                        <input type="text" class="knob" data-readonly="true" value="30" data-width="60" data-height="60"
                                            data-fgcolor="#39CCCC">

                                        <div class="text-white">In-Store</div>
                                    </div>
                                    <!-- ./col -->
                                </div>
                                <!-- /.row -->
                            </div>--%>
                            <!-- /.card-footer -->
                        </div>
                    </div>
                </div>

            </div>
            <!--/. container-fluid -->
        </section>
        <section class="content">
            <div class="nav-tabs-custom">
                <ul class="nav nav-tabs">
                    <li class="active"><a id="aGeneral" href="#General" data-toggle="tab">Receipt Details</a></li>
                    <li><a id="aSearchResult" href="#SearchResult" data-toggle="tab">LR Details</a></li>
                    <li><a id="aBookingBill" href="#BookingBill" data-toggle="tab">Retails Booking Bill</a></li>
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
                                                <th>Voucher #</th>
                                                <th>Voucher Dt</th>
                                                <th class="hidden-xs">Customer</th>
                                                <th class="hidden-xs">Account</th>
                                                <th class="hidden-xs">Receipt Mode</th>
                                                <th class="hidden-xs">Amount</th>
                                                <th class="hidden-xs">Status</th>
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
                                <div class="form-group col-md-12">
                                    <div class="table-responsive">
                                        <table id="tblSearchResult" class="table table-striped table-bordered bg-info" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>S.No</th>
                                                    <th>LR #</th>
                                                    <th>Date</th>
                                                    <th>Customer</th>
                                                    <th>Invoice No</th>
                                                    <th>Status</th>
                                                    <th>Delivered</th>
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
                    <div class="tab-pane" id="BookingBill">
                        <div class="box box-warning">
                            <div class="box-body">
                                <div class="form-group col-md-12">
                                    <div class="table-responsive">
                                        <table id="tblBookingBill" class="table table-striped table-bordered bg-info" width="100%">
                                            <thead>
                                                <tr>
                                                    <th>S.No</th>
                                                    <th>Booking No</th>
                                                    <th>Booking Date</th>
                                                    <th>Customer</th>
                                                    <th>Mobile No</th>
                                                    <th>Total Qty</th>
                                                    <th>Total Amount</th>
                                                    <th>Delivery Date</th>
                                                </tr>
                                            </thead>
                                            <tbody id="tblBookingBill_tbody">
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
                                <div class="form-group col-md-4" id="divVoucherNo">
                                    <label>
                                        Voucher No</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtVoucherNo" placeholder="Voucher No."
                                        maxlength="150" tabindex="1" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divVoucherDate">
                                    <label>
                                        Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtVoucherDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="2" id="txtVoucherDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" disabled="disabled" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divCustomer">
                                    <label>
                                        Customer</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlCustomer" class="form-control select2" data-placeholder="Select Customer" tabindex="3" disabled="disabled">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-4" id="divBank">
                                    <label>
                                        Select A/c</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlBank" class="form-control select2" data-placeholder="Select Account" tabindex="4" disabled="disabled">
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divReceiptMode">
                                    <label>
                                        Receipt Mode</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlReceiptMode" class="form-control" tabindex="5" disabled="disabled">
                                        <option value="0" selected="selected">--Select--</option>
                                        <option value="3">NEFT/RTGS</option>
                                        <option value="5">IMPS</option>
                                        <option value="1">Cash</option>
                                        <option value="2">Cheque</option>
                                        <option value="4">Others</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-4" id="divAmount">
                                    <label>Amount</label>
                                    <span class="text-danger">*</span>
                                    <div class="input-group">
                                        <div class="input-group-addon"><i class="fa fa-rupee"></i></div>
                                        <input type="text" class="form-control decimal" id="txtAmount" placeholder="Amount"
                                            maxlength="15" tabindex="6" readonly="true" />
                                        <span class="input-group-addon">.00</span>
                                    </div>
                                </div>
                            </div>
                            <div class="row" id="divChequeDetails">
                                <div class="form-group col-md-4" id="divChequeNo">
                                    <label>
                                        Cheque/DD #</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control" id="txtChequeNo" placeholder="Cheque/DD No."
                                        maxlength="150" tabindex="7" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divIssueDate">
                                    <label>
                                        Issued Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtIssueDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="8" id="txtIssueDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" disabled="disabled" />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divCollectionDate">
                                    <label>
                                        Collection Date</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtCollectionDate" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="9" id="txtCollectionDate" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-4" id="divIssuedBy">
                                    <label>
                                        Issued By</label>
                                    <input type="text" class="form-control" id="txtIssuedBy" placeholder="Issued By"
                                        maxlength="150" tabindex="10" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divBankName">
                                    <label>
                                        Bank Name</label>
                                    <input type="text" class="form-control" id="txtBankName" placeholder="Bank Name"
                                        maxlength="150" tabindex="10" readonly="true" />
                                </div>
                                <div class="form-group col-md-4" id="divStatus">
                                    <label>
                                        Payment Status</label>
                                    <span class="text-danger">*</span>
                                    <select id="ddlPaymentStatus" class="form-control" tabindex="5">
                                        <option value="Cleared" selected="selected">Cleared</option>
                                        <option value="Bounced">Bounced</option>
                                    </select>
                                </div>
                            </div>
                            <div class="form-group" id="divDescription">
                                <label>
                                    Description</label>
                                <textarea id="txtDescription" class="form-control" maxlength="250" tabindex="11" rows="3"></textarea>
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="14">
                                <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="12">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="13">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />
    <input type="hidden" id="hdRS" />
    <input type="hidden" id="hdnOpeningDate" />
    <input type="hidden" id="SdSMS" />
    <input type="hidden" id="SMSsendername" />
    <input type="hidden" id="SMSpassword" />
    <input type="hidden" id="SMSurl" />
    <input type="hidden" id="SMSusername" />
    <input type="hidden" id="RoleID" />
    <input type="hidden" id="NewCustomer" />
    <input type="hidden" id="RetailCustomer" />
    <input type="hidden" id="WholeSaleCustomer" />
    <input type="hidden" id="VIPCustomer" />
    <input type="hidden" id="Day1OldValue" />
    <input type="hidden" id="Day2OldValue" />
    <input type="hidden" id="Day3OldValue" />
    <input type="hidden" id="Day4OldValue" />
    <input type="hidden" id="Day5OldValue" />
    <input type="hidden" id="Day6OldValue" />
    <input type="hidden" id="Day7OldValue" />
    <input type="hidden" id="Day1Value" />
    <input type="hidden" id="Day2Value" />
    <input type="hidden" id="Day3Value" />
    <input type="hidden" id="Day4Value" />
    <input type="hidden" id="Day5Value" />
    <input type="hidden" id="Day6Value" />
    <input type="hidden" id="Day7Value" />
    <input type="hidden" id="Day1" />
    <input type="hidden" id="Day2" />
    <input type="hidden" id="Day3" />
    <input type="hidden" id="Day4" />
    <input type="hidden" id="Day5" />
    <input type="hidden" id="Day6" />
    <input type="hidden" id="Day7" />
    <input type="hidden" id="Q1Value" />
    <input type="hidden" id="Q2Value" />
    <input type="hidden" id="Q3Value" />
    <input type="hidden" id="Q4Value" />
    <input type="hidden" id="Q5Value" />
    <input type="hidden" id="Q6Value" />
    <input type="hidden" id="Q7Value" />
    <input type="hidden" id="Q8Value" />
    <input type="hidden" id="Q9Value" />
    <input type="hidden" id="Q10Value" />
    <input type="hidden" id="Q11Value" />
    <input type="hidden" id="Q12Value" />
    <input type="hidden" id="Q1" />
    <input type="hidden" id="Q2" />
    <input type="hidden" id="Q3" />
    <input type="hidden" id="Q4" />
    <input type="hidden" id="Q5" />
    <input type="hidden" id="Q6" />
    <input type="hidden" id="Q7" />
    <input type="hidden" id="Q8" />
    <input type="hidden" id="Q9" />
    <input type="hidden" id="Q10" />
    <input type="hidden" id="Q11" />
    <input type="hidden" id="Q12" />
    <input type="hidden" id="Month1" />
    <input type="hidden" id="Month2" />
    <input type="hidden" id="Month3" />
    <input type="hidden" id="Month4" />
    <input type="hidden" id="Month5" />
    <input type="hidden" id="Month6" />
    <input type="hidden" id="Month7" />
    <input type="hidden" id="Month8" />
    <input type="hidden" id="Month9" />
    <input type="hidden" id="Month10" />
    <input type="hidden" id="Month11" />
    <input type="hidden" id="Month12" />
    <input type="hidden" id="Silk1Value" />
    <input type="hidden" id="Silk2Value" />
    <input type="hidden" id="Silk3Value" />
    <input type="hidden" id="Silk4Value" />
    <input type="hidden" id="Silk5Value" />
    <input type="hidden" id="Silk6Value" />
    <input type="hidden" id="Silk7Value" />
    <input type="hidden" id="Silk8Value" />
    <input type="hidden" id="Silk9Value" />
    <input type="hidden" id="Silk10Value" />
    <input type="hidden" id="Silk11Value" />
    <input type="hidden" id="Silk12Value" />
    <input type="hidden" id="Cotton1Value" />
    <input type="hidden" id="Cotton2Value" />
    <input type="hidden" id="Cotton3Value" />
    <input type="hidden" id="Cotton4Value" />
    <input type="hidden" id="Cotton5Value" />
    <input type="hidden" id="Cotton6Value" />
    <input type="hidden" id="Cotton7Value" />
    <input type="hidden" id="Cotton8Value" />
    <input type="hidden" id="Cotton9Value" />
    <input type="hidden" id="Cotton10Value" />
    <input type="hidden" id="Cotton11Value" />
    <input type="hidden" id="Cotton12Value" />
    <input type="hidden" id="PercentageValue" />

    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            var _SendSMS = '<%=Session["SendSMS"]%>';
            var _SMSpassword = '<%=Session["SMSPassword"]%>';
            var _SMSsendername = '<%=Session["SenderName"]%>';
            var _SMSurl = '<%=Session["APILink"]%>';
            var _SMSusername = '<%=Session["SMSUsername"]%>';
            var _RoleID = '<%=Session["RoleID"]%>';
            $("#SdSMS").val(_SendSMS);
            $("#SMSsendername").val(_SMSsendername);
            $("#SMSpassword").val(_SMSpassword);
            $("#SMSurl").val(_SMSurl);
            $("#SMSusername").val(_SMSusername);
            $("#RoleID").val(_RoleID);
            $("#divMobileNo").hide();
            $("#txtVoucherDate,#txtIssueDate,#txtCollectionDate").attr("data-link-format", "dd/MM/yyyy");
            $("#txtVoucherDate,#txtIssueDate,#txtCollectionDate").datetimepicker({
                pickTime: false,
                useCurrent: true,
                format: 'DD/MM/YYYY'
            });
            GetRecord();
            GetBankList();
           GetCustomerList();
           // //GetDashboardCount();
           GetLRRecord("Pending");
            GetSalesEntryBookingBill();
            pLoadingSetup(false);
            pLoadingSetup(true);
            $("#divTab").hide();
            $("#btnSend").hide();
            $("#divTabNotification").hide();
            $("#btnSendNotification").hide();
            $("#ContainerID1").hide();

            var ticksStyle = {
                fontColor: '#495057',
                fontStyle: 'bold'
            }

            var mode = 'index'
            var intersect = true

            var salesChartCanvas = $('#salesChart').get(0).getContext('2d')

            var salesChartData = {
                labels: [$("#Month1").val(), $("#Month2").val(), $("#Month3").val(), $("#Month4").val(), $("#Month5").val(), $("#Month6").val(), $("#Month7").val(), $("#Month8").val(), $("#Month9").val(), $("#Month10").val(), $("#Month11").val(), $("#Month12").val()],
                datasets: [
                    {
                        label: 'Retail Sales',
                        backgroundColor: 'rgba(60,141,188,0.9)',
                        borderColor: 'rgba(60,141,188,0.8)',
                        pointRadius: false,
                        pointColor: '#3b8bba',
                        pointStrokeColor: 'rgba(60,141,188,1)',
                        pointHighlightFill: '#fff',
                        pointHighlightStroke: 'rgba(60,141,188,1)',
                        data: [$("#Silk1Value").val(), $("#Silk2Value").val(), $("#Silk3Value").val(), $("#Silk4Value").val(), $("#Silk5Value").val(), $("#Silk6Value").val(), $("#Silk7Value").val(), $("#Silk8Value").val(), $("#Silk9Value").val(), $("#Silk10Value").val(), $("#Silk11Value").val(), $("#Silk12Value").val()]
                    },
                    {
                        label: 'Wholesale',
                        backgroundColor: 'rgba(210, 214, 222, 1)',
                        borderColor: 'rgba(210, 214, 222, 1)',
                        pointRadius: false,
                        pointColor: 'rgba(210, 214, 222, 1)',
                        pointStrokeColor: '#c1c7d1',
                        pointHighlightFill: '#fff',
                        pointHighlightStroke: 'rgba(220,220,220,1)',
                        data: [$("#Cotton1Value").val(), $("#Cotton2Value").val(), $("#Cotton3Value").val(), $("#Cotton4Value").val(), $("#Cotton5Value").val(), $("#Cotton6Value").val(), $("#Cotton7Value").val(), $("#Cotton8Value").val(), $("#Cotton9Value").val(), $("#Cotton10Value").val(), $("#Cotton11Value").val(), $("#Cotton12Value").val()]
                    },
                ]
            }

            var salesChartOptions = {
                maintainAspectRatio: false,
                responsive: true,
                legend: {
                    display: false
                },
                scales: {
                    xAxes: [{
                        gridLines: {
                            display: false,
                        }
                    }],
                    yAxes: [{
                        gridLines: {
                            display: false,
                        }
                    }]
                }
            }

            // This will get the first returned node in the jQuery collection.
            var salesChart = new Chart(salesChartCanvas, {
                type: 'line',
                data: salesChartData,
                options: salesChartOptions
            }
            )

            //---------------------------
            //- END MONTHLY SALES CHART -
            //---------------------------


            //-------------
            //- PIE CHART -
            //-------------
            // Get context with jQuery - using jQuery's .get() method.
            var salesGraphChartCanvas = $('#line-chart').get(0).getContext('2d');
            //$('#revenue-chart').get(0).getContext('2d');

            var salesGraphChartData = {
                labels: [$("#Q1").val(), $("#Q2").val(), $("#Q3").val(), $("#Q4").val(), $("#Q5").val(), $("#Q6").val(), $("#Q7").val(), $("#Q8").val(), $("#Q9").val(), $("#Q10").val(), $("#Q11").val(), $("#Q12").val()],
                datasets: [
                    {
                        label: 'Sales Value',
                        fill: false,
                        borderWidth: 2,
                        lineTension: 0,
                        spanGaps: true,
                        borderColor: '#efefef',
                        pointRadius: 3,
                        pointHoverRadius: 7,
                        pointColor: '#efefef',
                        pointBackgroundColor: '#efefef',
                        data: [$("#Q1Value").val(), $("#Q2Value").val(), $("#Q3Value").val(), $("#Q4Value").val(), $("#Q5Value").val(), $("#Q6Value").val(), $("#Q7Value").val(), $("#Q8Value").val(), $("#Q9Value").val(), $("#Q10Value").val(), $("#Q11Value").val(), $("#Q12Value").val()]
                    }
                ]
            }

            var salesGraphChartOptions = {
                maintainAspectRatio: false,
                responsive: true,
                legend: {
                    display: false,
                },
                scales: {
                    xAxes: [{
                        ticks: {
                            fontColor: '#efefef',
                        },
                        gridLines: {
                            display: false,
                            color: '#efefef',
                            drawBorder: false,
                        }
                    }],
                    yAxes: [{
                        ticks: {
                            stepSize: 5000,
                            fontColor: '#efefef',
                        },
                        gridLines: {
                            display: true,
                            color: '#efefef',
                            drawBorder: false,
                        }
                    }]
                }
            }

            // This will get the first returned node in the jQuery collection.
            var salesGraphChart = new Chart(salesGraphChartCanvas, {
                type: 'line',
                data: salesGraphChartData,
                options: salesGraphChartOptions
            })

            var $visitorsChart = $('#visitors-chart')
            var visitorsChart = new Chart($visitorsChart, {
                data: {
                    labels: [$("#Day1").val(), $("#Day2").val(), $("#Day3").val(), $("#Day4").val(), $("#Day5").val(), $("#Day6").val(), $("#Day7").val()],
                    datasets: [{
                        type: 'line',
                        data: [$("#Day1Value").val(), $("#Day2Value").val(), $("#Day3Value").val(), $("#Day4Value").val(), $("#Day5Value").val(), $("#Day6Value").val(), $("#Day7Value").val()],
                        backgroundColor: 'transparent',
                        borderColor: '#007bff',
                        pointBorderColor: '#007bff',
                        pointBackgroundColor: '#007bff',
                        fill: false
                        // pointHoverBackgroundColor: '#007bff',
                        // pointHoverBorderColor    : '#007bff'
                    },
                    {
                        type: 'line',
                        data: [$("#Day1OldValue").val(), $("#Day2OldValue").val(), $("#Day3OldValue").val(), $("#Day4OldValue").val(), $("#Day5OldValue").val(), $("#Day6OldValue").val(), $("#Day7OldValue").val()],
                        backgroundColor: 'tansparent',
                        borderColor: '#ced4da',
                        pointBorderColor: '#ced4da',
                        pointBackgroundColor: '#ced4da',
                        fill: false
                        // pointHoverBackgroundColor: '#ced4da',
                        // pointHoverBorderColor    : '#ced4da'
                    }]
                },
                options: {
                    maintainAspectRatio: false,
                    tooltips: {
                        mode: mode,
                        intersect: intersect
                    },
                    hover: {
                        mode: mode,
                        intersect: intersect
                    },
                    legend: {
                        display: false
                    },
                    scales: {
                        yAxes: [{
                            // display: false,
                            gridLines: {
                                display: true,
                                lineWidth: '4px',
                                color: 'rgba(0, 0, 0, .2)',
                                zeroLineColor: 'transparent'
                            },
                            ticks: $.extend({
                                beginAtZero: true,
                                suggestedMax: 200
                            }, ticksStyle)
                        }],
                        xAxes: [{
                            display: true,
                            gridLines: {
                                display: false
                            },
                            ticks: ticksStyle
                        }]
                    }
                }
            })


            var pieChartCanvas = $('#pieChart').get(0).getContext('2d')
            var pieData = {
                labels: [
                    'New Customers',
                    'Old Customers',
                    'VIP Customers',
                    'Wholesale Customers',
                ],
                datasets: [
                    {
                        data: [$("#NewCustomer").val(), $("#RetailCustomer").val(), $("#WholeSaleCustomer").val(), $("#VIPCustomer").val()],
                        backgroundColor: ['#f56954', '#00a65a', '#f39c12', '#00c0ef'],
                    }
                ]
            }
            var pieOptions = {
                legend: {
                    display: false
                }
            }
            var pieChart = new Chart(pieChartCanvas, {
                type: 'doughnut',
                data: pieData,
                options: pieOptions
            })
        });


        $("#btnClear").click(function () {
            // ClearFields();
            $("#btnSave").show();
            // $("#divPendingBill").hide();
            // gOPBillingList = [];
            //GetPendingBill();
            // DisplayPendingBillList(gOPBillingList);
            // DisplayOPBillingList(gOPBillingList);
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Clear Amount");
            $('#compose-modal1').modal({ show: true, backdrop: true });

            return false;
        });

        function GetPendingBill() {
            gOPBillingList = [];
            DisplayPendingBillList(gOPBillingList);
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetAmountClearEntry",
                data: JSON.stringify({ PublisherID: 0 }),
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

        function DisplayPendingBillList(gData) {
            var sTable = "";
            var sCount = 1;
            var sColorCode = "bg-info";
            gvalue = [];
            $("#txtBalanceAmount").val(0);
            if (gData.length >= 20) { $("#divPendingBillList").css({ 'height': '10px', 'min-height': '690px', 'overflow': 'auto' }); }
            else { $("#divPendingBillList").css({ 'height': '', 'min-height': '' }); }

            if (gData.length > 0) {
                $("#divPendingBill").show();
                sTable = "<table id='tblPendingBillList' class='table no-margin table-condensed table-hover'>";
                sTable += "<thead><tr><th class='" + sColorCode + "' style='width:3px;text-align: center'>S.No</th>";
                sTable += "<th class='" + sColorCode + "'>Invoice No</th>";
                sTable += "<th class='" + sColorCode + "'>Date</th>";
                sTable += "<th class='" + sColorCode + "'>Payment Mode</th>";
                sTable += "<th class='" + sColorCode + "'>Amount</th>";
                sTable += "</tr></thead><tbody id='tblPendingBillList_body'>";
                sTable += "</tbody></table>";
                var sPaymentMode = ""; var BalanceValue = 0;
                $("#divPendingBillList").html(sTable);
                for (var i = 0; i < gData.length; i++) {

                    sTable = "<tr><td id='" + gData[i].SNo + "'>" + sCount + "</td>";
                    if ($("#hdnID").val() > 0) {
                        sTable += "<td style='line-height:0.5;'><label><input id='chk_" + gData[i].SalesEntry.SalesEntryID + "' type='checkbox' value='" + gData[i].SalesEntry.SalesEntryID + "'  name='PurchaseName'  disabled='disabled'/>&nbsp;" + gData[i].SalesEntry.InvoiceNo + "</label></td>";
                    }
                    else {
                        sTable += "<td style='line-height:0.5;'><label><input id='chk_" + gData[i].SalesEntry.SalesEntryID + "' type='checkbox' value='" + gData[i].SalesEntry.SalesEntryID + "'  name='PurchaseName'/>&nbsp;" + gData[i].SalesEntry.InvoiceNo + "</label></td>";
                    }
                    sTable += "<td style='line-height:0.5;'>" + gData[i].SalesEntry.sInvoiceDate + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].PaymentMode + "</td>";
                    sTable += "<td style='line-height:0.5;'>" + gData[i].Amount + "</td>";
                    sTable += "</tr>";
                    sCount = sCount + 1;
                    $("#tblPendingBillList_body").append(sTable);
                    BalanceValue += gData[i].Amount;
                    var objAgent = new Object();
                    objAgent.SalesEntryID = gData[i].SalesEntryID;
                    objAgent.Amount = gData[i].Amount;
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

        function GetDashboardCount() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetDashboardCount",
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

                                    $("#lblTotalProduct").text(obj.TotalProducts);
                                    $("#lblTotalPurchase").text(obj.TotalPurchase);
                                    $("#lblTotalSales").text(obj.TotalSales);
                                    $("#lblTotalCustomer").text(obj.TotalCustomer);
                                    $("#VIPCustomer").val(obj.VIPCustomer);
                                    $("#WholeSaleCustomer").val(obj.WholeSaleCustomer);
                                    $("#RetailCustomer").val(obj.RetailCustomer);
                                    $("#NewCustomer").val(obj.NewCustomer);
                                    $("#Day1OldValue").val(obj.Day1OldValue);
                                    $("#Day2OldValue").val(obj.Day2OldValue);
                                    $("#Day3OldValue").val(obj.Day3OldValue);
                                    $("#Day4OldValue").val(obj.Day4OldValue);
                                    $("#Day5OldValue").val(obj.Day5OldValue);
                                    $("#Day6OldValue").val(obj.Day6OldValue);
                                    $("#Day7OldValue").val(obj.Day7OldValue);
                                    $("#Day1Value").val(obj.Day1Value);
                                    $("#Day2Value").val(obj.Day2Value);
                                    $("#Day3Value").val(obj.Day3Value);
                                    $("#Day4Value").val(obj.Day4Value);
                                    $("#Day5Value").val(obj.Day5Value);
                                    $("#Day6Value").val(obj.Day6Value);
                                    $("#Day7Value").val(obj.Day7Value);
                                    $("#Day1").val(obj.Day1);
                                    $("#Day2").val(obj.Day2);
                                    $("#Day3").val(obj.Day3);
                                    $("#Day4").val(obj.Day4);
                                    $("#Day5").val(obj.Day5);
                                    $("#Day6").val(obj.Day6);
                                    $("#Day7").val(obj.Day7);
                                    $("#Q1Value").val(obj.Q1Value);
                                    $("#Q2Value").val(obj.Q2Value);
                                    $("#Q3Value").val(obj.Q3Value);
                                    $("#Q4Value").val(obj.Q4Value);
                                    $("#Q5Value").val(obj.Q5Value);
                                    $("#Q6Value").val(obj.Q6Value);
                                    $("#Q7Value").val(obj.Q7Value);
                                    $("#Q8Value").val(obj.Q8Value);
                                    $("#Q9Value").val(obj.Q9Value);
                                    $("#Q10Value").val(obj.Q10Value);
                                    $("#Q11Value").val(obj.Q11Value);
                                    $("#Q12Value").val(obj.Q12Value);
                                    $("#Q1").val(obj.Q1);
                                    $("#Q2").val(obj.Q2);
                                    $("#Q3").val(obj.Q3);
                                    $("#Q4").val(obj.Q4);
                                    $("#Q5").val(obj.Q5);
                                    $("#Q6").val(obj.Q6);
                                    $("#Q7").val(obj.Q7);
                                    $("#Q8").val(obj.Q8);
                                    $("#Q9").val(obj.Q9);
                                    $("#Q10").val(obj.Q10);
                                    $("#Q11").val(obj.Q11);
                                    $("#Q12").val(obj.Q12);
                                    $("#Month1").val(obj.Month1);
                                    $("#Month2").val(obj.Month2);
                                    $("#Month3").val(obj.Month3);
                                    $("#Month4").val(obj.Month4);
                                    $("#Month5").val(obj.Month5);
                                    $("#Month6").val(obj.Month6);
                                    $("#Month7").val(obj.Month7);
                                    $("#Month8").val(obj.Month8);
                                    $("#Month9").val(obj.Month9);
                                    $("#Month10").val(obj.Month10);
                                    $("#Month11").val(obj.Month11);
                                    $("#Month12").val(obj.Month12);
                                    $("#Cotton1Value").val(obj.Cotton1Value);
                                    $("#Cotton2Value").val(obj.Cotton2Value);
                                    $("#Cotton3Value").val(obj.Cotton3Value);
                                    $("#Cotton4Value").val(obj.Cotton4Value);
                                    $("#Cotton5Value").val(obj.Cotton5Value);
                                    $("#Cotton6Value").val(obj.Cotton6Value);
                                    $("#Cotton7Value").val(obj.Cotton7Value);
                                    $("#Cotton8Value").val(obj.Cotton8Value);
                                    $("#Cotton9Value").val(obj.Cotton9Value);
                                    $("#Cotton10Value").val(obj.Cotton10Value);
                                    $("#Cotton11Value").val(obj.Cotton11Value);
                                    $("#Cotton12Value").val(obj.Cotton12Value);
                                    $("#Silk1Value").val(obj.Silk1Value);
                                    $("#Silk2Value").val(obj.Silk2Value);
                                    $("#Silk3Value").val(obj.Silk3Value);
                                    $("#Silk4Value").val(obj.Silk4Value);
                                    $("#Silk5Value").val(obj.Silk5Value);
                                    $("#Silk6Value").val(obj.Silk6Value);
                                    $("#Silk7Value").val(obj.Silk7Value);
                                    $("#Silk8Value").val(obj.Silk8Value);
                                    $("#Silk9Value").val(obj.Silk9Value);
                                    $("#Silk10Value").val(obj.Silk10Value);
                                    $("#Silk11Value").val(obj.Silk11Value);
                                    $("#Silk12Value").val(obj.Silk12Value);

                                    var OldTotal = parseFloat(obj.Day1OldValue) + parseFloat(obj.Day2OldValue) + parseFloat(obj.Day4OldValue) + parseFloat(obj.Day5OldValue) + parseFloat(obj.Day6OldValue) + parseFloat(obj.Day7OldValue);
                                    var NewTotal = parseFloat(obj.Day1Value) + parseFloat(obj.Day2Value) + parseFloat(obj.Day3Value) + parseFloat(obj.Day4Value) + parseFloat(obj.Day5Value) + parseFloat(obj.Day6Value) + parseFloat(obj.Day7Value);
                                    var DiffereceValue = parseFloat(NewTotal) - parseFloat(OldTotal);

                                    if (DiffereceValue < 0) {
                                        $("#PercentageImage").addClass('fa-arrow-down text-red');
                                        $("#PercentageImage").removeClass('fa-arrow-up');
                                        DiffereceValue = DiffereceValue * -1;
                                    }
                                    else {
                                        $("#PercentageImage").addClass('fa-arrow-up');
                                        $("#PercentageImage").removeClass('fa-arrow-down text-red');
                                    }


                                    DiffereceValue = ((DiffereceValue * 100) / OldTotal).toFixed(2);
                                    $("#lblPercentage").html(DiffereceValue);
                                    $("#lblSaleDuration").html(obj.Saleduration);

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

        function GetRecord() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetReceiptByStatus",
                data: JSON.stringify({ Status: "Pending" }),
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
                                            sReceiptMode = "DD";
                                        else if (obj[index].ReceiptModeID == 4)
                                            sReceiptMode = "Others";

                                        var table = "<tr id='" + obj[index].ReceiptID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].VoucherNo + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].sVoucherDate + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Customer.CustomerName + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Bank.LedgerName + "</td>";
                                        table += "<td class='hidden-xs'>" + sReceiptMode + "</td>";
                                        table += "<td class='hidden-xs'>" + parseFloat(obj[index].Amount).toFixed(2) + "</td>";
                                        table += "<td class='hidden-xs'>" + obj[index].Status + "</td>";

                                        table += "<td><a href='#' id=" + obj[index].ReceiptID + " class='View' title='Click here to View'><i class='fa fa-lg fa-clone text-yellow'/></a></td>";

                                        table += "<td><a href='#' id=" + obj[index].ReceiptID + " class='Edit' title='Click here to Edit'><i class='fa fa-lg fa-edit'/></a></td>";

                                        table += "</tr>";
                                        $("#tblRecord_tbody").append(table);
                                    }
                                    $(".View").click(function () {
                                        EditRecord($(this).parent().parent()[0].id);
                                        $(".modal-title").html("<i class='fa fa-clone'></i>&nbsp;&nbsp;View Receipt");
                                        $("#btnSave").hide();
                                        $("#btnUpdate").hide();

                                    });
                                    $(".Edit").click(function () {
                                        EditRecord($(this).parent().parent()[0].id);
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
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "5%" },
                                    { "sWidth": "1%" },
                                    { "sWidth": "1%" }
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

        function GetLRRecord(iDetails) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/SearchLREntry",
                data: JSON.stringify({ ID: iDetails }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblSearchResult").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblSearchResult_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        if (obj[index].Status == "Pending") { TypeStatus = "<span class='label label-danger'>Pending</span>"; }
                                        else { TypeStatus = "<span class='label label-danger'>Delivered</span>"; }

                                        var table = "<tr id='" + obj[index].LREntryID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].LREntryNo + "</td>";
                                        table += "<td>" + obj[index].sLREntryDate + "</td>";
                                        table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                        table += "<td>" + obj[index].InvoiceNo + "</td>";
                                        table += "<td>" + TypeStatus + "</td>";
                                        table += "<td style='text-align:center;'><a href='#' id=" + obj[index].LREntryID + " class='Edit' title='Click here to update Status'><i class='fa fa-save'></a></td>";
                                        table += "</tr>";
                                        $("#tblSearchResult_tbody").append(table);
                                    }
                                    $(".Edit").click(function () {
                                        if (ActionView == "1") {
                                            UpdateLRStatus($(this).parent().parent()[0].id);
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
                                $("#tblSearchResult_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblSearchResult").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" }
                                ]
                            });
                            $("#tblSearchResult_filter").addClass('pull-right');
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
                        $("#tblSearchResult_tbody").empty();
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

        function GetSalesEntryBookingBill() {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSalesEntryBookingBill",
                data: JSON.stringify({ PublisherID: 0, IsRetail: 1 }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            var notetable = $("#tblBookingBill").dataTable();
                            notetable.fnDestroy();
                            if (objResponse.Value != null && objResponse.Value != "NoRecord") {
                                var obj = $.parseJSON(objResponse.Value);
                                if (obj.length > 0) {
                                    $("#tblBookingBill_tbody").empty();
                                    var TypeStatus = "";
                                    for (var index = 0; index < obj.length; index++) {
                                        var table = "<tr id='" + obj[index].SalesEntryID + "'>";
                                        table += "<td>" + (index + 1) + "</td>";
                                        table += "<td>" + obj[index].InvoiceNo + "</td>";
                                        table += "<td>" + obj[index].sInvoiceDate + "</td>";
                                        table += "<td>" + obj[index].Customer.CustomerName + "</td>";
                                        table += "<td>" + obj[index].Customer.MobileNo + "</td>";
                                        table += "<td>" + obj[index].TotalQty + "</td>";
                                        table += "<td>" + obj[index].NetAmount + "</td>";
                                        table += "<td>" + obj[index].sDeliveryDate + "</td>";
                                        //table += "<td style='text-align:center;'><a href='#' id=" + obj[index].SalesEntryID + " class='Edit' title='Click here to update Status'><i class='fa fa-save'></a></td>";
                                        table += "</tr>";
                                        $("#tblBookingBill_tbody").append(table);
                                    }
                                    //$(".Edit").click(function () {
                                    //    if (ActionView == "1") {
                                    //        UpdateBookingBill($(this).parent().parent()[0].id);
                                    //    }
                                    //    else {
                                    //        $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife });
                                    //        return false;
                                    //    }
                                    //});
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#tblBookingBill_tbody").empty();
                                dProgress(false);
                            }
                            $("#tblBookingBill").dataTable({
                                "bPaginate": true,
                                "bFilter": true,
                                "bSort": true,
                                "iDisplayLength": 25,
                                aoColumns: [
                                    { "sWidth": "5%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" },
                                    { "sWidth": "0%" }
                                ]
                            });
                            $("#tblBookingBill_filter").addClass('pull-right');
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
                        $("#tblBookingBill_tbody").empty();
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

            $("#divChequeDetails").hide();
            return false;
        }
        function GetBankList() {
            dProgress(true);
            $("#ddlBank").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetLedger",
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
        function GetCustomerList() {
            dProgress(true);
            $("#ddlCustomer").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomer",
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
                                            $("#ddlCustomer").append('<option value=' + obj[index].CustomerID + ' >' + obj[index].CustomerName + '</option>');
                                        }
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

        function UpdateLRStatus(id) {
            var ObjOPBilling = new Object();
            ObjOPBilling.Status = "Delivered";
            ObjOPBilling.LREntryID = id;
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/UpdateLREntryStatus",
                data: JSON.stringify({ Objdata: ObjOPBilling }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {
                    if (data.d != "") {
                        var objResponse = jQuery.parseJSON(data.d);
                        if (objResponse.Status == "Success") {
                            if (objResponse.Value > 0) {
                                $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
                                GetLRRecord("Pending");
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
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

        //function UpdateBookingBill(id) {

        //    $.ajax({
        //        type: "POST",
        //        url: "WebServices/VHMSService.svc/UpdateLREntryStatus",
        //        data: JSON.stringify({ Objdata: ObjOPBilling }),
        //        contentType: "application/json; charset=utf-8",
        //        dataType: "json",
        //        async: false,
        //        success: function (data) {
        //            if (data.d != "") {
        //                var objResponse = jQuery.parseJSON(data.d);
        //                if (objResponse.Status == "Success") {
        //                    if (objResponse.Value > 0) {
        //                        $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife });
        //                        GetSalesEntryBookingBill();
        //                    }
        //                }
        //                else if (objResponse.Status == "Error") {
        //                    if (objResponse.Value == "0") {
        //                        window.location = "frmLogin.aspx";
        //                    }
        //                    else if (objResponse.Value == "Error") {
        //                        window.location = "frmErrorPage.aspx";
        //                    }
        //                }
        //            }
        //            else {
        //                $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        //            }
        //        },
        //        error: function (e) {
        //            $.jGrowl("Error  Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
        //        }
        //    });
        //    return false;
        //}

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetReceiptByID",
                data: JSON.stringify({ ID: id, IsRetail: 0 }),
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
                                    $("#txtAmount").val(obj.Amount);
                                    $("#ddlPaymentStatus").val(obj.Status);
                                    if (obj.Status == 'Cleared')
                                        $("#ddlPaymentStatus").attr("disabled", true);
                                    else {
                                        $("#ddlPaymentStatus").attr("disabled", false);
                                        $("#ddlPaymentStatus").val("Cleared");
                                    }
                                    $("#txtDiscountAmount").val(obj.DiscountAmount).change();
                                    $("[id*=imgUpload1]").attr("src", obj.DocumentPath);
                                    $("#txtCollectionDate").val(obj.sCollectionDate);
                                    if (obj.ReceiptModeID == 2 || obj.ReceiptModeID == 3 || obj.ReceiptModeID == 5) {
                                        $("#divChequeDetails").show();
                                        $("#txtChequeNo").val(obj.ChequeNo);
                                        $("#txtIssueDate").val(obj.sIssueDate);
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
                                    //$("#txtDate").val(obj.sOpeningDate);
                                    if (obj.Messagesentcnt <= 0 && ($("#RoleID").val() == 12 || $("#RoleID").val() == 7)) {

                                        $("#divTab").show();
                                        $("#btnSend").show();
                                        $("#ContainerID1").show();
                                    }
                                    if (obj.NotificationSentCount <= 0 && ($("#RoleID").val() == 12 || $("#RoleID").val() == 7)) {
                                        $("#divTabNotification").show();
                                        $("#btnSendNotification").show();
                                        $("#ContainerID").show();
                                    }

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
                        $.jGrowl("Error Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                        dProgress(false);
                    }
                },
                error: function () {
                    $.jGrowl("Error Occured", { sticky: true, theme: 'danger', life: jGrowlLife });
                    dProgress(false);
                }
            });
            return false;
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

        $("#btnSave,#btnUpdate").click(function () {

            //if (this.id == "btnSave")
            //{ if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            //else if (this.id == "btnUpdate")
            //{ if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            //if ($("#ddlPaymentStatus").val() == "" || $("#ddlPaymentStatus").val() == undefined || $("#ddlPaymentStatus").val() == null) {
            //    $.jGrowl("Please select Voucher Type", { sticky: false, theme: 'warning', life: jGrowlLife });
            //    $("#divStatus").addClass('has-error'); $("#ddlPaymentStatus").focus(); return false;
            //} else { $("#divStatus").removeClass('has-error'); }

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
            if ($("#ddlReceiptMode").val() == 2 || $("#ddlReceiptMode").val() == 3) {
                if ($("#txtChequeNo").val().trim() == "" || $("#txtChequeNo").val().trim() == undefined) {
                    $.jGrowl("Please enter Cheque/DD No.", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divChequeNo").addClass('has-error'); $("#txtChequeNo").focus(); return false;
                } else { $("#divChequeNo").removeClass('has-error'); }

                if ($("#txtIssueDate").val().trim() == "" || $("#txtIssueDate").val().trim() == undefined) {
                    $.jGrowl("Please select Issue Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divIssueDate").addClass('has-error'); $("#txtIssueDate").focus(); return false;
                } else { $("#divIssueDate").removeClass('has-error'); }

                if ($("#txtCollectionDate").val().trim() == "" || $("#txtCollectionDate").val().trim() == undefined) {
                    $.jGrowl("Please select Collection Date", { sticky: false, theme: 'warning', life: jGrowlLife });
                    $("#divCollectionDate").addClass('has-error'); $("#txtCollectionDate").focus(); return false;
                } else { $("#divCollectionDate").removeClass('has-error'); }
            }


            var Obj = new Object();
            Obj.ReceiptID = $("#hdnID").val();
            Obj.VoucherNo = $("#txtVoucherNo").val().trim();
            Obj.sVoucherDate = $("#txtVoucherDate").val();

            var objCustomer = new Object();
            objCustomer.CustomerID = $("#ddlCustomer").val();
            Obj.Customer = objCustomer;

            var objBank = new Object();
            objBank.LedgerID = $("#ddlBank").val();
            Obj.Bank = objBank;

            Obj.ReceiptModeID = $("#ddlReceiptMode").val();
            Obj.Amount = parseFloat($("#txtAmount").val().trim());
            Obj.Status = "Cleared";
            if ($("#ddlReceiptMode").val() == 2 || $("#ddlReceiptMode").val() == 3) {
                Obj.ChequeNo = $("#txtChequeNo").val().trim();
                Obj.sIssueDate = $("#txtIssueDate").val().trim();
                Obj.sCollectionDate = $("#txtCollectionDate").val().trim();
                Obj.IssuedBy = $("#txtIssuedBy").val().trim();
                Obj.BankName = $("#txtBankName").val().trim();

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

        function LoadCustomer(ddlname) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCustomerByDate",
                data: JSON.stringify({ ID: ddlname }),
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
                                        if (ddlname == "DOB")
                                            GetSMSRecord(obj[index].MobileNo, "On this special day we would like to extend our heartiest wishes to you. Wish you a very happy birthday. - SVS Jewellery");
                                        else
                                            GetSMSRecord(obj[index].MobileNo, "On this special day we would like to extend our heartiest wishes to you. Wish you a very happy anniversary. - SVS Jewellery");
                                    }
                                }
                                dProgress(false);
                            }
                            else if (objResponse.Value == "NoRecord") {
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

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

    </script>
</asp:Content>
