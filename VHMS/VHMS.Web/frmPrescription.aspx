<%@ Page Title="Prescription" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPrescription.aspx.cs" Inherits="frmPrescription" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Prescription
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
                                                <th>Prescription #</th>
                                                <th class="hidden-xs">Date</th>
                                                <th class="hidden-xs">Patient ID</th>
                                                <th class="hidden-xs">Husband Name</th>
                                                <th class="hidden-xs">Wife Name</th>
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
                    </div>
                </div>
            </div>
            <div class="box box-primary" id="divPrescription">
                <div class="box-header with-border">
                    <div class="box-title">Prescription Information</div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-2" id="divPrescriptionNo">
                            <label>
                                Prescription No</label>
                            <input type="text" class="form-control" id="txtPrescriptionNo" placeholder="Prescription No"
                                maxlength="15" tabindex="1" readonly="true" />
                        </div>
                        <div class="form-group col-md-2" id="divPrescriptionDate">
                            <label>
                                Date</label><span class="text-danger">*</span>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtPrescriptionDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="2" id="txtPrescriptionDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-5" id="divPatient">
                            <label>
                                Patient</label><span class="text-danger">*</span>
                            <select id="ddlPatient" class="form-control select2" data-placeholder="Select Patient" tabindex="3">
                            </select>
                        </div>
                    </div>
                    <div class="box box-primary box-solid">
                        <div class="box-header">
                            Particulars
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-4" id="divDrugName">
                                    <label>
                                        Drug Name</label><span class="text-danger">*</span>
                                    <%-- <input type="text" class="form-control" id="txtDrugName" placeholder="Please enter Drug Name"
                                        maxlength="150" tabindex="36" />--%>
                                    <div id="divSelectDrugName">
                                        <select id="ddlDrugName" class="form-control select2" data-placeholder="Select Drug Name" tabindex="49"></select>
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divDosage">
                                    <label>
                                        Dosage</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDosage" placeholder="Dosage"
                                        maxlength="200" tabindex="50" />
                                </div>
                                <div class="form-group col-md-2" id="divFrequency">
                                    <label>
                                        Frequency</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtFrequency" placeholder="Frequency"
                                        maxlength="200" tabindex="51" />
                                </div>
                                <div class="form-group col-md-2" id="divDuration">
                                    <label>
                                        Duration</label><span class="text-danger">*</span>
                                    <input type="text" class="form-control TRSearch" id="txtDuration" placeholder="Duration"
                                        maxlength="200" tabindex="52" />
                                </div>
                                <div class="form-group col-md-2" id="divInstruction">
                                    <label>
                                        Instruction</label><span class="text-danger">*</span>
                                    <select id="ddlInstruction" class="form-control" tabindex="53">
                                        <option selected="selected" value="0">Select</option>
                                        <option value="1">After Food</option>
                                        <option value="2">Before Food</option>
                                        <option value="3">Intra Muscular</option>
                                        <option value="4">Intra Venous</option>
                                        <option value="5">Subcutaeneous</option>
                                        <option value="6">Others</option>
                                    </select>
                                </div>
                                <div class="form-group col-md-3" id="divIngredient">
                                    <label>
                                        Ingredient</label>
                                    <input type="text" class="form-control" id="txtIngredient" placeholder="Ingredient"
                                        maxlength="200" tabindex="54" />
                                </div>
                                <div class="form-group col-md-1 pull-right">
                                    <div class="margin">
                                        <button id="btnAddMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="12">
                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                        <button id="btnUpdateMagazine" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="13">
                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div id="divPrescriptionList">
                        </div>
                    </div>
                    <div class="modal-footer clearfix">
                        <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="16">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;Close</button>
                        <button id="btnSave" type="button" class="btn btn-info margin pull-right" tabindex="14">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Save Changes</button>
                        <button id="btnPrint" type="button" class="btn btn-info margin pull-right" tabindex="14">
                            <i class="fa fa-print"></i>&nbsp;&nbsp;Print</button>
                        <button id="btnUpdate" type="button" class="btn btn-info margin pull-right" tabindex="15">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;Update Changes</button>
                    </div>
                </div>
            </div>
        </section>
    </div>

    <input type="hidden" id="hdnPrescriptionID" />
    <input type="hidden" id="hdnPrescriptionTransID" />
    <input type="hidden" id="hdnPatientID" />
    <input type="hidden" id="hdnMagazineID" />
    <input type="hidden" id="hdnPrescriptionSNo" />
    <input type="hidden" id="hdnPrescriptionOrderID" />
    <script src="UserDefined_Js/JPrescription.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JPrescription.js") %>" type="text/javascript"></script>
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



