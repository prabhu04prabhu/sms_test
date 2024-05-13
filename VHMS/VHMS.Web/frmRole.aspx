<%@ Page Title="User Privileges" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmRole.aspx.cs" Inherits="frmRole" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>User Privileges
            </h1>
                <div class="box box-warning box-solid" id="divViewDamage" style="Width: auto; padding: 5px 15px 5px 15px; border-radius: 23px; position: absolute; top: 13%; left: 40%; font-weight: bold; text-align: center; font-size: 25px;">
                       <label id="lblRoleName"></label>
            </div>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Accounts</a></li>
                <li class="active">User Privileges</li>
            </ol>
            <div class="pull-right">
                <button id="btnAddNew" class="btn btn-info">
                    <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add New</button>
                <button id="btnList" class="btn btn-info">
                    <i class="fa fa-list"></i>&nbsp;&nbsp;List</button>
            </div>
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
                                            <th>Role Name
                                            </th>
                                            <th>Description
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Edit
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
                    <li><a id="aRoleResources" href="#RoleResources" data-toggle="tab"><i class="fa fa-wrench"></i>&nbsp; User Privileges</a></li>
                </ul>
                <div class="tab-content">
                    <div class="tab-pane active" id="General">
                        <div class="form-horizontal">
                            <div class="form-group" id="divRole">
                                <label class="col-md-2">
                                    Role Name<span class="text-danger">*</span></label>
                                <div class="col-md-5">
                                    <input type="text" class="form-control" id="txtName" placeholder="Role Name" maxlength="100"
                                        tabindex="1" />
                                </div>
                            </div>
                            <div class="form-group" id="divDescription">
                                <label class="col-md-2">
                                    Description</label>
                                <div class="col-md-5">
                                    <input type="text" class="form-control" id="txtDescription" placeholder="Description" maxlength="200"
                                        tabindex="2" />
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="col-sm-offset-2 col-sm-2">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkStatus" checked="checked" tabindex="3" />Active                      
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkMarginView" checked="checked" tabindex="4" />View Margin                      
                                        </label>
                                    </div>
                                </div>
                                <div class="col-sm-2">
                                    <div class="checkbox">
                                        <label>
                                            <input type="checkbox" id="chkCommonMargin" checked="checked" tabindex="5" />Common Margin                      
                                        </label>
                                    </div>
                                </div>
                            </div>
                            <div class="form-group" id="divClone" style="display:none;">
                                <label class="col-md-2">Copy Role</label>
                                <div class="col-md-2">
                                    <select id="ddlRole" class="form-control select2" data-placeholder="Select Role" tabindex="6"></select>
                                </div>
                                <div class="col-md-1">
                                    <button id="btnCopy" type="button" class="btn btn-danger pull-right" tabindex="-1">
                                    <i class="fa fa-clone"></i>&nbsp;&nbsp;Copy</button>
                                </div>
                            </div>
                            <div class="modal-footer clearfix">
                                <button id="btnSave" type="button" class="btn btn-info margin pull-right" tabindex="8">
                                    <i class="fa fa-save"></i>&nbsp;&nbsp;Save</button>
                                <button id="btnUpdate" type="button" class="btn btn-info margin pull-right" tabindex="9">
                                    <i class="fa fa-edit"></i>&nbsp;&nbsp;Update</button>

                            </div>
                        </div>
                    </div>
                    <div class="tab-pane" id="RoleResources">
                        <div class="row">
                            <div class="form-group col-md-3" id="divMenu">
                                <label>
                                    Menu</label><span class="text-danger">*</span>
                                <select id="ddlMenu" class="form-control select2" data-placeholder="Select Menu" tabindex="1">
                                </select>
                            </div>
                            <div class="form-group col-md-3" id="divModule">
                                <label>
                                    Module/Screen</label><span class="text-danger">*</span>
                                <select id="ddlModule" class="form-control select2" data-placeholder="Select Module" tabindex="2">
                                </select>
                            </div>
                            <div class="form-group col-md-5">
                                <label>
                                    User Privileges</label>
                                <div class="checkbox">
                                    <label class="checkbox-inline">
                                        <input type="checkbox" id="chkAccess" tabindex="3" />Access&nbsp;&nbsp;
                                    </label>
                                    <label class="checkbox-inline">
                                        <input type="checkbox" id="chkView" tabindex="4" />View&nbsp;&nbsp;
                                    </label>
                                    <label class="checkbox-inline">
                                        <input type="checkbox" id="chkAdd" tabindex="5" />Add&nbsp;&nbsp;
                                    </label>
                                    <label class="checkbox-inline">
                                        <input type="checkbox" id="chkEdit" tabindex="6" />Edit&nbsp;&nbsp;
                                    </label>
                                    <label class="checkbox-inline">
                                        <input type="checkbox" id="chkDelete" tabindex="7" />Delete&nbsp;&nbsp;
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-md-1">
                                <div class="margin">
                                    <button id="btnAddModule" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="8">
                                        <i class="fa fa-plus-square"></i>&nbsp;&nbsp;Add Privilege</button>
                                    <button id="btnUpdateModule" type="button" class="btn btn-primary btn-sm margin pull-right" tabindex="9">
                                        <i class="fa fa-edit"></i>&nbsp;&nbsp;Update Privilege</button>
                                </div>
                            </div>
                        </div>
                        <div class="table-responsive">
                            <div id="divModules">
                            </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSaveRoleConfiguration" type="button" class="btn btn-info margin pull-right" tabindex="10">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;Save Changes</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnRoleID" />
    <input type="hidden" id="hdnSNo" />
    <input type="hidden" id="hdnRoleConfigurationID" />
    <script src="UserDefined_Js/JRoles.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/JRoles.js") %>" type="text/javascript"></script>
    <script type="text/javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';

        $(document).ready(function () {
        });
    </script>
</asp:Content>

