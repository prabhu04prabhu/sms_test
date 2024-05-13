<%@ Page Title="Doctor" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmDoctor.aspx.cs" Inherits="frmDoctor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <h1>Doctor
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="frmDischargeMenu.aspx">Discharge</a></li>
                <li><a href="#">Master</a></li>
                <li class="active">Doctor</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-info">
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
                                <table id="tblRecord" class="table table-striped table-bordered bg-info" width="100%">
                                    <thead>
                                        <tr>
                                            <th>SNo
                                            </th>
                                            <th>Doctor
                                            </th>
                                            <th>Type
                                            </th>
                                            <th>Specialization
                                            </th>
                                            <th>State
                                            </th>
                                            <th>City
                                            </th>
                                            <th>Mobile No
                                            </th>
                                            <th>Department
                                            </th>
                                            <th>DOH/MOH No
                                            </th>
                                            <th>Status
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
            <div class="box box-solid box-primary" id="divDoctor">
                <div class="box-header with-border">
                    <div class="box-title">
                        <i class="fa fa-user"></i>&nbsp;&nbsp;Doctor
                    </div>
                </div>
                <div class="box-body">
                    <div class="row">
                        <div class="form-group col-md-3" id="divDoctorName">
                            <label>
                                Name</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtDoctorName" placeholder="Please enter Doctor Name"
                                maxlength="200" tabindex="1" />
                        </div>
                        <div class="form-group col-md-3" id="divDoctorType">
                            <label>
                                Type</label><span class="text-danger">*</span>
                            <select id="ddlDoctorType" class="form-control select2" data-placeholder="Select Doctor Type" tabindex="2">
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divSpecialization">
                            <label>
                                Specialization</label><span class="text-danger">*</span>
                            <select id="ddlSpecialization" class="form-control select2" data-placeholder="Select Specialization" tabindex="3">
                            </select>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Qualification</label>
                            <input type="text" class="form-control" id="txtQualification" placeholder="Please enter Qualification"
                                maxlength="250" tabindex="4" />
                        </div>
                        <div class="form-group col-md-12">
                            <label>
                                Address</label>
                            <textarea id="txtAddress" class="form-control" maxlength="300" tabindex="5" rows="4"></textarea>
                        </div>
                        <div class="form-group col-md-3" id="divCountry">
                            <label>
                                Country</label><span class="text-danger">*</span>
                            <select id="ddlCountry" class="form-control select2" data-placeholder="Select Country" tabindex="5">
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divState">
                            <label>
                                State</label><span class="text-danger">*</span>
                            <select id="ddlState" class="form-control select2" data-placeholder="Select State" tabindex="6">
                            </select>
                        </div>
                        <div class="form-group col-md-5">
                            <label>
                                City</label>
                            <input type="text" class="form-control" id="txtCity" placeholder="Please enter City"
                                maxlength="150" tabindex="7" />
                        </div>
                        <div class="form-group col-md-1">
                            <label>
                                Pincode</label>
                            <input type="text" class="form-control" id="txtPincode" placeholder="Pincode"
                                maxlength="6" tabindex="8"  onkeypress="return isNumberKey(event)"/>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Phone No1</label>
                            <input type="text" class="form-control" id="txtPhoneNo1" placeholder="Please enter Phone No"
                                maxlength="20" tabindex="9"  onkeypress="return isNumberKey(event)"/>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Phone No2</label>
                            <input type="text" class="form-control" id="txtPhoneNo2" placeholder="Please enter alteranative No"
                                maxlength="20" tabindex="10"  onkeypress="return isNumberKey(event)"/>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Phone No(Res)</label>
                            <input type="text" class="form-control" id="txtPhoneNo3" placeholder="Please enter Resident Phone No"
                                maxlength="20" tabindex="11"  onkeypress="return isNumberKey(event)"/>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Mobile No</label>
                            <input type="text" class="form-control" id="txtMobileNo" placeholder="Please enter Mobile No"
                                maxlength="20" tabindex="12"  onkeypress="return isNumberKey(event)"/>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Fax No</label>
                            <input type="text" class="form-control" id="txtFaxNo" placeholder="Please enter Fax No"
                                maxlength="20" tabindex="13"  onkeypress="return isNumberKey(event)"/>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Email</label>
                            <input type="text" class="form-control" id="txtEmail" placeholder="Please enter email"
                                maxlength="300" tabindex="14" />
                        </div>
                        <div class="form-group col-md-3" id="divDepartment">
                            <label>
                                Department</label><span class="text-danger">*</span>
                            <select id="ddlDepartment" class="form-control select2" data-placeholder="Select Department" tabindex="15">
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divDoctorNo">
                            <label>
                                DOH/MOH No</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtDoctorNo" placeholder="Please enter DOH/MOH No"
                                maxlength="100" tabindex="16" />
                        </div>
                        <div class="form-group col-md-2"  id="divDisplayOrder">
                            <label>
                                Display Order</label><span class="text-danger">*</span>
                            <select id="ddlDisplayOrder" class="form-control" tabindex="17">
                            </select>
                        </div>
                        <div class="form-group col-md-3">
                            <div class="checkbox">
                                <label class="checkbox-inline">
                                    <input type="checkbox" id="chkIsRMODoctor" tabindex="18" />RMO Doctor
                                </label>
                                <label class="checkbox-inline">
                                    <input type="checkbox" id="chkIsExternalDoctor" tabindex="19" />External Doctor
                                </label>
                                <label class="checkbox-inline">
                                    <input type="checkbox" id="chkStatus" checked="checked" tabindex="20" />Active
                                </label>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="23">
                        <i class="fa fa-times"></i>&nbsp;&nbsp;Close</button>
                    <button type="button" class="btn btn-info pull-right" id="btnSave" tabindex="21">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                    <button type="button" class="btn btn-info pull-right" id="btnUpdate" tabindex="22">
                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>
                </div>
            </div>
        </section>
    </div>

    <input type="hidden" id="hdnDoctorID" />
    <script src="UserDefined_Js/Discharge/JDoctor.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Discharge/JDoctor.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

        $(document).ready(function () {
            $("#txtCity").keypress(function (event) {
                var inputValue = event.which;
                if (!(inputValue >= 65 && inputValue <= 120) && (inputValue != 32 && inputValue != 0)) {
                    event.preventDefault();
                }
            });

        });
    </script>
</asp:Content>

