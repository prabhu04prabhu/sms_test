<%@ Page Title="Discharge Entry" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmDischargeEntry.aspx.cs" Inherits="frmDischargeEntry" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Discharge Entry
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Discharge</a></li>
                <li class="active">Discharge Entry</li>
            </ol>
            <div class="pull-right hidden">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
            </div>
        </section>
        <section class="content">
            <div class="box box-primary box-solid" id="divDischargeEdit">
                <div class="box-header with-border">
                    <div class="box-title">
                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Edit Discharge Entry
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-4" id="divSearchAdmissionNo">
                            <label>
                                Search Admission No</label><span class="text-danger">*</span>
                            <div class="input-group">
                                <input class="form-control TRSearch" id="txtSearchAdmissionNo" maxlength="10" tabindex="6" placeholder="Please enter Admission No, Patient Name, IP No, Room No" type="text" />
                                <span class="input-group-btn">
                                    <button class="btn btn-success btn-flat" id="btnSearch" type="button">Go!</button>
                                </span>
                            </div>
                        </div>
                        <div class="form-group col-md-4 margin">
                            <button type="button" class="btn btn-warning btn-sm margin" title="Clear filter" id="btnClearSearch" tabindex="6">
                                <i class="fa fa-undo"></i>
                            </button>
                            <button type="button" class="btn btn-danger btn-sm margin" title="Click here to Add New Discharge Entry" id="btnAddDischargeEntry" tabindex="6">
                                <i class="fa fa-plus"></i>&nbsp;&nbsp;Add New Discharge
                            </button>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div id="divAdmissionResult">
                        </div>
                    </div>
                </div>
            </div>
            <div class="box box-primary box-solid" id="divDischargeEntry">
                <div class="box-header with-border">
                    <div class="box-title">
                        <i class="fa fa-wheelchair"></i>&nbsp;&nbsp;Discharge Entry
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-3" id="divAdmissionNo">
                            <label>
                                Admission No</label>
                            <%--<span class="text-danger">*</span>--%>
                            <input type="text" class="form-control" id="txtAdmissionNo" placeholder="Admission No"
                                maxlength="200" tabindex="1" />
                        </div>
                        <div class="form-group col-md-2" id="divUHIDNo">
                            <label>
                                UHID No</label>
                            <%--<span class="text-danger">*</span>--%>
                            <input type="text" class="form-control" id="txtUHIDNo" placeholder="UHID No"
                                maxlength="200" tabindex="2" />
                        </div>
                        <div class="form-group col-md-2" id="divRoomNo">
                            <label>
                                Room No</label>
                            <%--<span class="text-danger">*</span>--%>
                            <input type="text" class="form-control" id="txtRoomNo" placeholder="Room No"
                                maxlength="200" tabindex="3" />
                        </div>
                        <div class="form-group col-md-2" id="divMLCNo">
                            <label>
                                MLC No</label>
                            <input type="text" class="form-control" id="txtMLCNo" placeholder="MLC No"
                                maxlength="200" tabindex="4" />
                        </div>
                        <div class="form-group col-md-3" id="divContactNo">
                            <label>
                                Contact No</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtContactNo" placeholder="Contact No"
                                maxlength="200" tabindex="5" />
                        </div>
                        <div class="form-group col-md-4" id="divPatientName">
                            <label>
                                Patient Name</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtPatientName" placeholder="Patient Name"
                                maxlength="200" tabindex="6" />
                        </div>
                        <div class="form-group col-md-1" id="divAge">
                            <label>
                                Age</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtAge" placeholder="Age"
                                maxlength="200" tabindex="7" />
                        </div>
                        <div class="form-group col-md-2" id="divGender">
                            <label>
                                Sex</label><span class="text-danger">*</span>
                            <select id="ddlGender" class="form-control" tabindex="8">
                                <option selected="selected" value="0">--Select--</option>
                                <option value="1">Male</option>
                                <option value="2">Female</option>
                                <option value="3">Transgender</option>
                            </select>
                        </div>
                        <div class="form-group col-md-5" id="divPrimaryConsultant">
                            <label>
                                Primary Consultant</label><span class="text-danger">*</span>
                            <select id="ddlPrimaryConsultant" class="form-control select2" multiple="multiple" tabindex="14">
                            </select>
                        </div>
                        <div class="form-group col-md-12" id="divPatientAddress">
                            <label>
                                Patient Address</label><span class="text-danger">*</span>
                            <textarea id="txtPatientAddress" class="form-control" maxlength="1000" tabindex="10" placeholder="Please enter Patient Address.." rows="4"></textarea>
                        </div>
                        <div class="form-group col-md-3" id="divAdmissionDate">
                            <label>
                                Date of Admission</label>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDateofAdmission"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <div class="input-group-addon" id="divClearAdmissionDate">
                                    <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="11" id="txtAdmissionDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-3" id="divAdmissionTime">
                            <label>
                                Time</label>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtAdmissionTime" data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-clock-o"></i>
                                </div>
                                <div class="input-group-addon" id="divClearAdmissionTime">
                                    <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                </div>
                                <input class="form-control pull-right" tabindex="12" id="txtAdmissionTime" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-3" id="divSurgeryDate">
                            <label>
                                Date of Surgery</label>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtSurgeryDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <div class="input-group-addon" id="divClearSurgeryDate">
                                    <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="13" id="txtSurgeryDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-3" id="divDischargeDate">
                            <label>
                                Date of Discharge</label>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDischargeDate"
                                data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                </div>
                                <div class="input-group-addon" id="divClearDischargeDate">
                                    <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                </div>
                                <input type="text" class="form-control pull-right" tabindex="14" id="txtDischargeDate" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-3" id="divDischargeTime">
                            <label>
                                Time</label>
                            <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDischargeTime" data-link-format="dd/MM/yyyy">
                                <div class="input-group-addon">
                                    <i class="fa fa-clock-o"></i>
                                </div>
                                <div class="input-group-addon" id="divClearDischargeTime">
                                    <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                </div>
                                <input class="form-control pull-right" tabindex="15" id="txtDischargeTime" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                            </div>
                        </div>
                        <div class="form-group col-md-3" id="divSummaryType">
                            <label>
                                Summary Type</label><span class="text-danger">*</span>
                            <select id="ddlSummaryType" class="form-control" tabindex="16">
                                <option selected="selected" value="0">--Select--</option>
                                <option value="1">Discharge Summary</option>
                                <option value="2">Discharge at Request</option>
                                <option value="3">Discharge Againt Medical Advice</option>
                                <option value="4">Death Summary</option>
                                <option value="5">Daycase Summary</option>
                            </select>
                        </div>
                        <div class="form-group col-md-1 margin pull-right">
                            <button id="btnChangeCase" type="button" class="btn btn-xs bg-maroon margin pull-right" tabindex="21">
                                <i class="fa fa-sort-alpha-asc"></i>&nbsp;&nbsp;Change Case&nbsp;&nbsp;<i class="fa fa-sort-alpha-desc"></i></button>
                        </div>
                    </div>
                </div>
            </div>           
            <div class="panel with-nav-tabs panel-success" id="tab-modal">
                <div class="panel-heading">
                    <ul class="nav nav-tabs">
                        <li class="active"><a id="aTab1" href="#Tab1" data-toggle="tab">Co-Consultants and Registrars</a></li>
                        <li><a id="aTab2" href="#Tab2" data-toggle="tab">DrugAllergy and Diagnosis</a></li>
                        <li><a id="aTab3" href="#Tab3" data-toggle="tab">Surgery Name and Surgey Notes</a></li>
                        <li><a id="aTab5" href="#Tab5" data-toggle="tab">Past History and Examination</a></li>
                        <li><a id="aTab6" href="#Tab6" data-toggle="tab">Local Examination</a></li>
                        <li><a id="aTab4" href="#Tab4" data-toggle="tab">Course During stay and Investigation</a></li>
                        <li><a id="aTab7" href="#Tab7" data-toggle="tab">Advise on Discharge and Review Appointment</a></li>
                        <li><a id="aTab8" href="#Tab8" data-toggle="tab">Advice Medications</a></li>
                        <li><a id="aTab9" href="#Tab9" data-toggle="tab">Cause of Death</a></li>
                        <li><a id="aTab10" href="#Tab10" data-toggle="tab">Doctor Review</a></li>
                    </ul>
                </div>
                <div class="panel-body">
                    <div class="tab-content">
                        <div class="tab-pane fade in active" id="Tab1">
                            <div class="row">
                                <div class="form-group col-md-6" id="divCoConsultant">
                                    <label>
                                        Co-Consultant</label><span class="text-danger">*</span>
                                    <select id="ddlCoConsultant" class="form-control select2" multiple="multiple" tabindex="14">
                                    </select>
                                </div>
                                <div class="form-group col-md-6" id="divRegistrar">
                                    <label>
                                        Registrar</label><span class="text-danger">*</span>
                                    <select id="ddlRegistrar" class="form-control select2" multiple="multiple" tabindex="15">
                                    </select>
                                </div>
                            </div>
                            <div class="box box-warning" id="divOtherDoctorList">
                                <div class="box-body">
                                    <div class="row">
                                        <div class="form-group col-md-6">
                                            <label>
                                                Doctors List</label>
                                            <div id="divDoctorsList">
                                            </div>
                                        </div>
                                        <div class="form-group col-md-6">
                                            <label class="bg-green">
                                                &nbsp; Selected Doctors List&nbsp;</label>
                                            <div id="divSelectedDoctorsList">
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab2">
                            <div class="row">
                                <div class="form-group col-md-12" id="divDrugAllergy">
                                    <label>
                                        Drug Allergy</label><span class="text-danger">*</span>
                                    <textarea id="txtDrugAllergy" class="form-control" tabindex="16" placeholder="Please enter text.." rows="4"></textarea>
                                </div>
                                <div class="form-group col-md-12" id="divDiagnosis">
                                    <label>
                                        Diagnosis</label><span class="text-danger">*</span>
                                    <textarea id="txtDiagnosis" class="form-control" tabindex="17" placeholder="Please enter text.." rows="10"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab3">
                            <div class="panel with-nav-tabs panel-danger">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a id="aTab31" href="#Tab31" data-toggle="tab">Hip Replacement</a></li>
                                        <li><a id="aTab32" href="#Tab32" data-toggle="tab">Knee Replacement</a></li>
                                        <li><a id="aTab33" href="#Tab33" data-toggle="tab">Others</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="Tab31">
                                            <div class="row">
                                                <div class="form-group col-md-2" id="divHipSurgeryType">
                                                    <label>
                                                        Type</label><span class="text-danger">*</span>
                                                    <select id="ddlHipSurgeryType" class="form-control" tabindex="18">
                                                        <option selected="selected" value="0">--Select--</option>
                                                        <option value="1">Left</option>
                                                        <option value="2">Right</option>
                                                        <option value="3">Both</option>
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-7" id="divHipSurgeryName">
                                                    <label>
                                                        Surgery Name</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control" id="txtHipSurgeryName" placeholder="Surgery Name"
                                                        maxlength="200" tabindex="18" />
                                                </div>
                                                <div class="form-group col-md-2" id="divHipSurgeryDate">
                                                    <label>
                                                        Surgery Date</label><span class="text-danger">*</span>
                                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtHipSurgeryDate"
                                                        data-link-format="dd/MM/yyyy">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                                        </div>
                                                        <input type="text" class="form-control pull-right" tabindex="19" id="txtHipSurgeryDate" readonly />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-1 pull-right">
                                                    <div class="margin">
                                                        <button id="btnAddHipSurgery" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="24">
                                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                        <button id="btnUpdateHipSurgery" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="25">
                                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="divLeftHip">
                                                <div class="form-group col-md-1" id="divLeftHipLabel">
                                                    <label class="badge bg-red">Left Hip</label>
                                                </div>
                                                <div class="form-group col-md-3" id="divLHipImplant">
                                                    <label>
                                                        Implant</label>
                                                    <textarea id="txtLHipImplant" class="form-control" maxlength="250" tabindex="19" rows="3"></textarea>
                                                </div>
                                                <div class="form-group col-md-2" id="divLAcetabulum">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLAcetabulumTitle" value="Acetabulum cup"
                                                        maxlength="100" tabindex="20" />
                                                    <input type="text" class="form-control" id="txtLAcetabulum" placeholder="Acetabulum cup"
                                                        maxlength="100" tabindex="20" />
                                                </div>
                                                <div class="form-group col-md-2" id="divLLiner">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLLinerTitle" value="Liner"
                                                        maxlength="100" tabindex="21" />
                                                    <input type="text" class="form-control" id="txtLLiner" placeholder="Liner"
                                                        maxlength="100" tabindex="21" />
                                                </div>
                                                <div class="form-group col-md-2" id="divLFemoralStem">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLFemoralStemTitle" value="Femoral stem"
                                                        maxlength="100" tabindex="22" />
                                                    <input type="text" class="form-control" id="txtLFemoralStem" placeholder="Femoral stem"
                                                        maxlength="100" tabindex="22" />
                                                </div>
                                                <div class="form-group col-md-2" id="divLFemoralHead">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLFemoralHeadTitle" value="Femoral head"
                                                        maxlength="100" tabindex="23" />
                                                    <input type="text" class="form-control" id="txtLFemoralHead" placeholder="Femoral head"
                                                        maxlength="100" tabindex="23" />
                                                </div>
                                            </div>
                                            <div class="row" id="divRightHip">
                                                <div class="form-group col-md-1" id="divRightHipLabel">
                                                    <label class="badge bg-red">Right Hip</label>
                                                </div>
                                                <div class="form-group col-md-3" id="divRHipImplant">
                                                    <label>
                                                        Implant</label>
                                                    <textarea id="txtRHipImplant" class="form-control" maxlength="250" tabindex="19" rows="3"></textarea>
                                                </div>
                                                <div class="form-group col-md-2" id="divRAcetabulum">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRAcetabulumTitle" value="Acetabulum cup"
                                                        maxlength="100" tabindex="20" />
                                                    <input type="text" class="form-control" id="txtRAcetabulum" placeholder="Acetabulum cup"
                                                        maxlength="100" tabindex="20" />
                                                </div>
                                                <div class="form-group col-md-2" id="divRLiner">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRLinerTitle" value="Liner"
                                                        maxlength="100" tabindex="21" />
                                                    <input type="text" class="form-control" id="txtRLiner" placeholder="Liner"
                                                        maxlength="100" tabindex="21" />
                                                </div>
                                                <div class="form-group col-md-2" id="divRFemoralStem">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRFemoralStemTitle" value="Femoral stem"
                                                        maxlength="100" tabindex="22" />
                                                    <input type="text" class="form-control" id="txtRFemoralStem" placeholder="Femoral stem"
                                                        maxlength="100" tabindex="22" />
                                                </div>
                                                <div class="form-group col-md-2" id="divRFemoralHead">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRFemoralHeadTitle" value="Femoral head"
                                                        maxlength="100" tabindex="23" />
                                                    <input type="text" class="form-control" id="txtRFemoralHead" placeholder="Femoral head"
                                                        maxlength="100" tabindex="23" />
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <div id="divHipSurgeryList">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="Tab32">
                                            <div class="row">
                                                <div class="form-group col-md-2" id="divKneeSurgeryType">
                                                    <label>
                                                        Type</label><span class="text-danger">*</span>
                                                    <select id="ddlKneeSurgeryType" class="form-control" tabindex="26">
                                                        <option selected="selected" value="0">--Select--</option>
                                                        <option value="1">Left</option>
                                                        <option value="2">Right</option>
                                                        <option value="3">Both</option>
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-7" id="divKneeSurgeryName">
                                                    <label>
                                                        Surgery Name</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control" id="txtKneeSurgeryName" placeholder="Surgery Name"
                                                        maxlength="200" tabindex="27" />
                                                </div>
                                                <div class="form-group col-md-2 col-sm-4" id="divKneeSurgeryDate">
                                                    <label>
                                                        Surgery Date</label><span class="text-danger">*</span>
                                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtKneeSurgeryDate"
                                                        data-link-format="dd/MM/yyyy">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                                        </div>
                                                        <input type="text" class="form-control pull-right" tabindex="28" id="txtKneeSurgeryDate" readonly />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-1 pull-right">
                                                    <div class="margin">
                                                        <button id="btnAddKneeSurgery" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="34">
                                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                        <button id="btnUpdateKneeSurgery" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="35">
                                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row" id="divLeftKnee">
                                                <div class="form-group col-md-1" id="divLeftKneeLabel">
                                                    <label class="badge bg-red">Left Knee</label>
                                                </div>
                                                <div class="form-group col-md-3" id="divLKneeImplant">
                                                    <label>
                                                        Implant</label>
                                                    <textarea id="txtLKneeImplant" class="form-control" maxlength="250" tabindex="29" rows="3"></textarea>
                                                </div>
                                                <div class="form-group col-md-2" id="divLFemur">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLFemurTitle" value="Femur"
                                                        maxlength="100" tabindex="30" />
                                                    <input type="text" class="form-control" id="txtLFemur" placeholder="Femur"
                                                        maxlength="100" tabindex="30" />
                                                </div>
                                                <div class="form-group col-md-2" id="divLTibia">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLTibiaTitle" value="Tibia"
                                                        maxlength="100" tabindex="31" />
                                                    <input type="text" class="form-control" id="txtLTibia" placeholder="Tibia"
                                                        maxlength="100" tabindex="31" />
                                                </div>
                                                <div class="form-group col-md-2" id="divLPoly">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLPolyTitle" value="Poly"
                                                        maxlength="100" tabindex="32" />
                                                    <input type="text" class="form-control" id="txtLPoly" placeholder="Poly"
                                                        maxlength="100" tabindex="32" />
                                                </div>
                                                <div class="form-group col-md-2" id="divLStem">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtLStemTitle" value="Stem"
                                                        maxlength="100" tabindex="33" />
                                                    <input type="text" class="form-control" id="txtLStem" placeholder="Stem"
                                                        maxlength="100" tabindex="33" />
                                                </div>
                                            </div>
                                            <div class="row" id="divRightKnee">
                                                <div class="form-group col-md-1" id="divRightKneeLabel">
                                                    <label class="badge bg-red">Right Knee</label>
                                                </div>
                                                <div class="form-group col-md-3" id="divRKneeImplant">
                                                    <label>
                                                        Implant</label>
                                                    <textarea id="txtRKneeImplant" class="form-control" maxlength="250" tabindex="29" rows="3"></textarea>
                                                </div>
                                                <div class="form-group col-md-2" id="divRFemur">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRFemurTitle" value="Femur"
                                                        maxlength="100" tabindex="30" />
                                                    <input type="text" class="form-control" id="txtRFemur" placeholder="Femur"
                                                        maxlength="100" tabindex="30" />
                                                </div>
                                                <div class="form-group col-md-2" id="divRTibia">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRTibiaTitle" value="Tibia"
                                                        maxlength="100" tabindex="31" />
                                                    <input type="text" class="form-control" id="txtRTibia" placeholder="Tibia"
                                                        maxlength="100" tabindex="31" />
                                                </div>
                                                <div class="form-group col-md-2" id="divRPoly">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRPolyTitle" value="Poly"
                                                        maxlength="100" tabindex="32" />
                                                    <input type="text" class="form-control" id="txtRPoly" placeholder="Poly"
                                                        maxlength="100" tabindex="32" />
                                                </div>
                                                <div class="form-group col-md-2" id="divRStem">
                                                    <label></label>
                                                    <input type="text" class="form-control bg-blue" id="txtRStemTitle" value="Stem"
                                                        maxlength="100" tabindex="33" />
                                                    <input type="text" class="form-control" id="txtRStem" placeholder="Stem"
                                                        maxlength="100" tabindex="33" />
                                                </div>
                                            </div>
                                            <div class="row">
                                            </div>
                                            <div class="table-responsive">
                                                <div id="divKneeSurgeryList">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="Tab33">
                                            <div class="row">
                                                <div class="form-group col-md-8" id="divOthers">
                                                    <label>
                                                        Surgery Name</label>
                                                    <input type="text" class="form-control" id="txtOtherSurgeryName" placeholder="Surgery Name"
                                                        maxlength="200" tabindex="36" />
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="form-group col-md-8" id="divOtherProcedure">
                                                    <label>
                                                        Procedure</label><span class="text-danger">*</span>
                                                    <label class="radio-inline pull-right">
                                                        <input type="radio" id="rdbExistingOtherProcedure" name="OtherProcedure" checked="checked" tabindex="4" />Existing</label>
                                                    <label class="radio-inline pull-right">
                                                        <input type="radio" id="rdbNewOtherProcedure" name="OtherProcedure" tabindex="4" />New&nbsp;&nbsp;</label>
                                                    <div id="divSelectOtherProcedure">
                                                        <select id="ddlOtherProcedure" class="form-control select2" data-placeholder="Select Procedure" tabindex="3">
                                                        </select>
                                                    </div>
                                                    <div id="divNewOtherProcedure">
                                                        <input type="text" class="form-control" id="txtNewOtherProcedure" placeholder="Procedure"
                                                            maxlength="200" tabindex="36" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-2 col-sm-4" id="divOtherSurgeryDate">
                                                    <label>
                                                        Surgery Date</label><span class="text-danger">*</span>
                                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtOtherSurgeryDate"
                                                        data-link-format="dd/MM/yyyy">
                                                        <div class="input-group-addon">
                                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                                        </div>
                                                        <input type="text" class="form-control pull-right" tabindex="37" id="txtOtherSurgeryDate" readonly />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-2 pull-right">
                                                    <div class="margin">
                                                        <button id="btnAddOtherSurgery" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="38">
                                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                        <button id="btnUpdateOtherSurgery" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="39">
                                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-12" id="divDetailProcedure">
                                                    <label>
                                                        Detail Procedure</label>
                                                    <textarea id="txtDetailProcedure" class="form-control" maxlength="250" tabindex="19" placeholder="Please enter text.." rows="5"></textarea>
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <div id="divOtherSurgeryList">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab4">
                            <div class="row">
                                <div class="form-group col-md-12" id="divInvestigation">
                                    <label>
                                        Investigation</label><span class="text-danger">*</span>
                                    <textarea id="txtInvestigation" class="form-control" tabindex="41" placeholder="Please enter text.." rows="8"></textarea>
                                </div>
                                <div class="form-group col-md-12" id="divCourseDuringStay">
                                    <label>
                                        Course During Stay</label><span class="text-danger">*</span>
                                    <textarea id="txtCourseDuringStay" class="form-control" tabindex="40" placeholder="Please enter text.." rows="8"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab5">
                            <div class="row">
                                <div class="form-group col-md-12" id="divPastHistory">
                                    <label>
                                        Past History</label><span class="text-danger">*</span>
                                    <textarea id="txtPastHistory" class="form-control" tabindex="42" placeholder="Please enter text.." rows="8"></textarea>
                                </div>
                                <div class="form-group col-md-12" id="divGeneralExamination">
                                    <label>
                                        On Examination</label><span class="text-danger">*</span>
                                    <textarea id="txtGeneralExamination" class="form-control" tabindex="43" placeholder="Please enter text.." rows="8"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab6">
                            <div class="row">
                                <div class="form-group col-md-12" id="divLocalExamination">
                                    <label>
                                        Local Examination</label><span class="text-danger">*</span>
                                    <textarea id="txtLocalExamination" class="form-control" tabindex="44" placeholder="Please enter text.." rows="12"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab7">
                            <div class="row">
                                <div class="form-group col-md-12" id="divAdviseonDischarge">
                                    <label>
                                        Advise on Discharge</label><span class="text-danger">*</span>
                                    <textarea id="txtAdviseonDischarge" class="form-control" tabindex="45" placeholder="Please enter text.." rows="8"></textarea>
                                </div>
                                <div class="form-group col-md-2 col-sm-4" id="divReviewAppointmentDate">
                                    <label>
                                        Review Appointment</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtReviewAppointmentDate"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <div class="input-group-addon" id="divClearReviewAppointmentDate">
                                            <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                        </div>
                                        <input type="text" class="form-control pull-right" tabindex="46" id="txtReviewAppointmentDate" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-2 col-sm-4" id="divReviewAppointmentTime">
                                    <label>
                                        Time</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtReviewAppointmentTime" data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-clock-o"></i>
                                        </div>
                                        <div class="input-group-addon" id="divClearReviewAppointmentTime">
                                            <i class="fa fa-eraser glyphicon glyphicon-remove" title="Click here to clear"></i>
                                        </div>
                                        <input class="form-control pull-right" tabindex="47" id="txtReviewAppointmentTime" data-link-format="dd/MM/yyyy HH:ii P" type="text" readonly />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divChkDischarge">
                                    <input type="checkbox" id="chkAdvStatus" tabindex="48" />
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab8">
                            <div class="panel with-nav-tabs panel-danger">
                                <div class="panel-heading">
                                    <ul class="nav nav-tabs">
                                        <li class="active"><a id="aTab81" href="#Tab81" data-toggle="tab">Advice Medications (VITO)</a></li>
                                        <li><a id="aTab82" href="#Tab82" data-toggle="tab">Patient Own Drugs</a></li>
                                    </ul>
                                </div>
                                <div class="panel-body">
                                    <div class="tab-content">
                                        <div class="tab-pane fade in active" id="Tab81">
                                            <div class="row">
                                                <div class="form-group col-md-4" id="divDrugName">
                                                    <label>
                                                        Drug Name</label><span class="text-danger">*</span>
                                                    <label class="radio-inline pull-right">
                                                        <input type="radio" id="rdbExistingDrugName" name="DrugName" checked="checked" />Existing</label>
                                                    <label class="radio-inline pull-right">
                                                        <input type="radio" id="rdbNewDrugName" name="DrugName" />New&nbsp;&nbsp;</label>
                                                    <div id="divSelectDrugName">
                                                        <select id="ddlDrugName" class="form-control select2" data-placeholder="Select Drug Name" tabindex="49">
                                                        </select>
                                                    </div>
                                                    <div id="divNewDrugName">
                                                        <input type="text" class="form-control" id="txtNewDrugName" placeholder="Please enter Drug Name"
                                                            maxlength="150" tabindex="36" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-2" id="divDosage">
                                                    <label>
                                                        Dosage</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control TRSearch" id="txtDosage" placeholder="Dosage"
                                                        maxlength="200" tabindex="50" />
                                                </div>
                                                <%--<div class="form-group col-md-2" id="divFrequency">
                                                    <label>
                                                        Frequency</label><span class="text-danger">*</span>
                                                    <select id="ddlFrequency" class="form-control" tabindex="51">
                                                        <option selected="selected" value="0">--Select--</option>
                                                        <option value="1">0-0-1</option>
                                                        <option value="2">0-1-1</option>
                                                        <option value="3">1-1-1</option>
                                                        <option value="4">1-0-0</option>
                                                        <option value="5">1-1-0</option>
                                                        <option value="6">1-0-1</option>
                                                        <option value="7">0-1-0</option>
                                                        <option value="8">SOS</option>
                                                        <option value="9">Others</option>
                                                        <option value="10">1/2-1/2-1/2</option>
                                                        <option value="11">1/2-0-1/2</option>
                                                        <option value="12">1/2-0-0</option>
                                                        <option value="13">0-1/2-0</option>
                                                        <option value="14">0-0-1/2</option>
                                                    </select>
                                                </div>--%>
                                                <div class="form-group col-md-2" id="divFrequency">
                                                    <label>
                                                        Frequency</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control TRSearch" id="txtFrequency" placeholder="Frequency"
                                                        maxlength="200" tabindex="51" />
                                                </div>
                                                <div class="form-group col-md-2" id="divOtherFrqeuency">
                                                    <label>
                                                        Other</label>
                                                    <input type="text" class="form-control" id="txtOtherFrqeuency" placeholder="Other Frequency"
                                                        maxlength="200" tabindex="52" />
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
                                                        <button id="btnAddPrescription" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="55">
                                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                        <button id="btnUpdatePrescription" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="56">
                                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <div id="divPrescriptionList">
                                                </div>
                                            </div>
                                        </div>
                                        <div class="tab-pane fade" id="Tab82">
                                            <div class="row">
                                                <div class="form-group col-md-4" id="divPatientDrugName">
                                                    <label>
                                                        Drug Name</label><span class="text-danger">*</span>
                                                    <label class="radio-inline pull-right">
                                                        <input type="radio" id="rdbExistingPatientDrugName" name="PatientDrugName" checked="checked" />Existing</label>
                                                    <label class="radio-inline pull-right">
                                                        <input type="radio" id="rdbNewPatientDrugName" name="PatientDrugName" />New&nbsp;&nbsp;</label>
                                                    <div id="divSelectPatientDrugName">
                                                        <select id="ddlPatientDrugName" class="form-control select2" data-placeholder="Select Drug Name" tabindex="49">
                                                        </select>
                                                    </div>
                                                    <div id="divNewPatientDrugName">
                                                        <input type="text" class="form-control" id="txtNewPatientDrugName" placeholder="Please enter Drug Name"
                                                            maxlength="150" tabindex="36" />
                                                    </div>
                                                </div>
                                                <div class="form-group col-md-2" id="divPatientDosage">
                                                    <label>
                                                        Dosage</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control TRSearch" id="txtPatientDosage" placeholder="Dosage"
                                                        maxlength="200" tabindex="50" />
                                                </div>
                                                <%--<div class="form-group col-md-2" id="divPatientFrequency">
                                                    <label>
                                                        Frequency</label><span class="text-danger">*</span>
                                                    <select id="ddlPatientFrequency" class="form-control" tabindex="51">
                                                        <option selected="selected" value="0">--Select--</option>
                                                        <option value="1">0-0-1</option>
                                                        <option value="2">0-1-1</option>
                                                        <option value="3">1-1-1</option>
                                                        <option value="4">1-0-0</option>
                                                        <option value="5">1-1-0</option>
                                                        <option value="6">1-0-1</option>
                                                        <option value="7">0-1-0</option>
                                                        <option value="8">SOS</option>
                                                        <option value="9">Others</option>
                                                    </select>
                                                </div>--%>
                                                <div class="form-group col-md-2" id="divPatientFrequency">
                                                    <label>
                                                        Frequency</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control TRSearch" id="txtPatientFrequency" placeholder="Frequency"
                                                        maxlength="200" tabindex="51" />
                                                </div>
                                                <div class="form-group col-md-2" id="divPatientOtherFrqeuency">
                                                    <label>
                                                        Other</label>
                                                    <input type="text" class="form-control" id="txtPatientOtherFrqeuency" placeholder="Other Frequency"
                                                        maxlength="200" tabindex="52" />
                                                </div>
                                                <div class="form-group col-md-2" id="divPatientDuration">
                                                    <label>
                                                        Duration</label><span class="text-danger">*</span>
                                                    <input type="text" class="form-control TRSearch" id="txtPatientDuration" placeholder="Duration"
                                                        maxlength="200" tabindex="52" />
                                                </div>
                                                <div class="form-group col-md-2" id="divPatientInstruction">
                                                    <label>
                                                        Instruction</label><span class="text-danger">*</span>
                                                    <select id="ddlPatientInstruction" class="form-control" tabindex="53">
                                                        <option selected="selected" value="0">Select</option>
                                                        <option value="1">After Food</option>
                                                        <option value="2">Before Food</option>
                                                        <option value="3">Intra Muscular</option>
                                                        <option value="4">Intra Venous</option>
                                                        <option value="5">Subcutaeneous</option>
                                                        <option value="6">Others</option>
                                                    </select>
                                                </div>
                                                <div class="form-group col-md-3" id="divPatientIngredient">
                                                    <label>
                                                        Ingredient</label>
                                                    <input type="text" class="form-control" id="txtPatientIngredient" placeholder="Ingredient"
                                                        maxlength="200" tabindex="54" />
                                                </div>
                                                <div class="form-group col-md-1 pull-right">
                                                    <div class="margin">
                                                        <button id="btnAddPatientPrescription" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="55">
                                                            <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add</button>
                                                        <button id="btnUpdatePatientPrescription" type="button" class="btn btn-success btn-sm margin pull-right" tabindex="56">
                                                            <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="table-responsive">
                                                <div id="divPatientPrescriptionList">
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab9">
                            <div class="row">
                                <div class="form-group col-md-12" id="divCauseofDeath">
                                    <label>
                                        Cause of Death</label><span class="text-danger">*</span>
                                    <textarea id="txtCauseofDeath" class="form-control" maxlength="500" tabindex="57" placeholder="Please enter text.." rows="8"></textarea>
                                </div>
                            </div>
                        </div>
                        <div class="tab-pane fade" id="Tab10">
                            <div class="row">
                                <div class="form-group col-md-4" id="divWrittenBy">
                                    <label>
                                        Written by</label>
                                    <input type="text" class="form-control" id="txtWrittenBy" placeholder="Written By"
                                        maxlength="200" tabindex="36" />
                                </div>
                                <div class="form-group col-md-4" id="divCheckedBy">
                                    <label>
                                        Checked By</label><span class="text-danger">*</span>
                                    <select id="ddlCheckedBy" class="form-control select2" data-placeholder="Select" tabindex="49">
                                    </select>
                                </div>
                            </div>
                            <div class="row">
                                <div class="form-group col-md-12">
                                    <label>
                                        Review on</label>
                                    <div class="checkbox">
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_1" name="WeekDay" value="1" tabindex="18" />Monday
                                        </label>
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_2" name="WeekDay" value="2" tabindex="19" />Tuesday
                                        </label>
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_3" name="WeekDay" value="3" tabindex="20" />Wednesday
                                        </label>
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_4" name="WeekDay" value="4" tabindex="20" />Thursday
                                        </label>
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_5" name="WeekDay" value="5" tabindex="20" />Friday
                                        </label>
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_6" name="WeekDay" value="6" tabindex="20" />Saturday
                                        </label>
                                        <label class="checkbox-inline">
                                            <input type="checkbox" id="chkWeekDay_7" name="WeekDay" value="7" tabindex="20" />Sunday
                                        </label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer clearfix">
                        <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="59">
                            <i class="fa fa-times"></i>&nbsp;&nbsp;
                                Close</button>
                        <button type="button" class="btn btn-info pull-right" id="btnSavePrint" tabindex="57">
                            <i class="fa fa-print"></i>&nbsp;&nbsp;
                                Save & Print</button>
                        <button type="button" class="btn btn-info pull-right" id="btnSave" tabindex="57">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                        <button type="button" class="btn btn-info pull-right" id="btnUpdatePrint" tabindex="58">
                            <i class="fa fa-print"></i>&nbsp;&nbsp;
                                Update & Print</button>
                        <button type="button" class="btn btn-info pull-right" id="btnUpdate" tabindex="58">
                            <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnDoctorID" />
    <input type="hidden" id="hdnDischargeEntryID" />
    <input type="hidden" id="hdnAdmissionID" />
    <input type="hidden" id="hdnAdmissionNo" />

    <input type="hidden" id="hdnHipSNo" />
    <input type="hidden" id="hdnHipID" />

    <input type="hidden" id="hdnKneeSNo" />
    <input type="hidden" id="hdnKneeID" />

    <input type="hidden" id="hdnOtherSNo" />
    <input type="hidden" id="hdnOtherID" />

    <input type="hidden" id="hdnPrescriptionSNo" />
    <input type="hidden" id="hdnPrescriptionID" />

    <input type="hidden" id="hdnDosageID" />
    <input type="hidden" id="hdnDurationID" />
    <input type="hidden" id="hdnFrequencyID" />

    <input type="hidden" id="hdnPatientPrescriptionSNo" />
    <input type="hidden" id="hdnPatientPrescriptionID" />

    <input type="hidden" id="hdnPatientDosageID" />
    <input type="hidden" id="hdnPatientDurationID" />
    <input type="hidden" id="hdnPatientFrequencyID" />

    <script src="UserDefined_Js/Discharge/JDischarge.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Discharge/JDischarge.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

        $(document).ready(function () {

        });
    </script>
</asp:Content>
