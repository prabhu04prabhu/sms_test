<%@ Page Title="Product" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmProduct1.aspx.cs" Inherits="frmProduct1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <div id="divTitle">
                <h1>Product
                </h1>
            </div>
        </section>
        <section class="content">
            <div class="box box-info box-solid">
                <div class="box-header with-border">
                    <div class="modal-title"></div>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="form-group col-md-3" id="divCategory">
                            <label>
                                Category</label><span class="text-danger">*</span>
                            <select id="ddlCategory" class="form-control select2" data-placeholder="Select Category" tabindex="1">
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divSubCategory">
                            <label>
                                Subcategory</label><span class="text-danger">*</span>
                            <select id="ddlSubCategory" class="form-control select2" data-placeholder="Select Subcategory" tabindex="2">
                            </select>
                        </div>
                        <div class="form-group col-md-3" id="divName">
                            <label>
                                Product</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtName" placeholder="Please enter Product Name"
                                maxlength="150" tabindex="3" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                SMS Code</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtCode" placeholder="Please enter Code"
                                maxlength="3" tabindex="5" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="form-group col-md-3" id="divSupplier">
                            <label>
                                Supplier</label><span class="text-danger">*</span>
                            <select id="ddlSupplier" class="form-control select2" data-placeholder="Select Supplier" tabindex="4">
                            </select>
                        </div>
                        <div class="form-group col-md-3">
                            <label>
                                Supplier Code</label><span class="text-danger">*</span>
                            <input type="text" class="form-control" id="txtSupplierCode" placeholder="Please enter Code"
                                maxlength="3" tabindex="3" />
                        </div>
                        <div class="form-group col-md-3" id="divUnit">
                            <label>
                                Unit</label><span class="text-danger">*</span>
                            <select id="ddlUnit" class="form-control select2" data-placeholder="Select Unit" tabindex="6">
                            </select>
                        </div>
                        <div class="checkbox" style="margin-top:30px">
                            <label>
                                <input type="checkbox" id="chkStatus" checked="checked" tabindex="4" />Active
                            </label>
                        </div>
                    </div>
                    <div class="row">
                       <div class="form-group col-md-3" style="display:none">
                            <label>Live Camera</label>
                            <div id="webcam"></div>
                        </div>
                        <div class="form-group col-md-3">
                            <label>Image 1</label>
                            <asp:Image ID="ProofimgCapture"  runat="server" Style="visibility: hidden; width: 300px; height: 225px" />

                            <asp:Button ID="btnProofCapture" Visible="false" Text="Capture" runat="server" OnClientClick="return Capture();" /><span id="ProofStatus"></span>
                            <br />
                             <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                runat="server" ID="imgbtn1" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                OnUploadedComplete="UploadedComplete" OnClientUploadComplete="DocumentuploadComplete" />
                            <input type="hidden" id="hdnimgbtn1" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Image 2</label>
                            <asp:Image ID="ProofBackimgCapture" runat="server" Style="visibility: hidden; width: 300px; height: 225px" />
                            <asp:Button ID="btnProofBackCapture" Visible="false" Text="Capture" runat="server" OnClientClick="return Capture3();" /><span id="ProofBackStatus"></span>
                            <br />
                             <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                runat="server" ID="imgbtn2" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                OnUploadedComplete="UploadedComplete1" OnClientUploadComplete="DocumentuploadComplete1" />
                            <input type="hidden" id="hdnimgbtn2" />
                        </div>
                        <div class="form-group col-md-3">
                            <label>Image 3</label>
                            <asp:Image ID="Register1Capture" runat="server" Style="visibility: hidden; width: 300px; height: 225px" />
                            <asp:Button ID="btnRegister1Capture" Visible="false" Text="Capture" runat="server" OnClientClick="return Capture1();" /><span id="Register1Status"></span>
                            <br />
                            <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                runat="server" ID="imgbtn3" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                OnUploadedComplete="UploadedComplete2" OnClientUploadComplete="DocumentuploadComplete2" />
                            <input type="hidden" id="hdnimgbtn3" />
                        </div>

                    </div>
                </div>
                <div class="modal-footer clearfix">
                    <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="7">
                        <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                    <button type="submit" class="btn btn-info pull-right" id="btnSave" tabindex="5">
                        <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                    <button type="submit" class="btn btn-info pull-right" id="btnUpdate" tabindex="6">
                        <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnID" />

    <script type="text/javascript">
        $(document).ready(function () {
            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            if (ActionAdd != "1") {
                $("#btnAddNew").remove();
                $("#btnSave").remove();
            }

            if (ActionUpdate != "1") {
                $("#btnUpdate").remove();
            }

            $("#hdnID").val('<%=Session["ProductID"]%>');
            var isView = '<%=Session["isView"]%>';

             if (isView == "View") {
                 $("#btnSave").remove();
                 $("#btnUpdate").remove();
            }
             SetSessionValue("RegisterID", "");
             SetSessionValue("isView", "");

            pLoadingSetup(false);
            pLoadingSetup(true);
            GetCategoryList();
            GetSubCategoryList();
            GetunitList();
            GetSupplierList();
        });

        function DocumentuploadComplete(sender, args) {
            const size =
                (this.files[0].size / 1024 / 1024).toFixed(2);

            if (size > 1) {
                alert("File must be less than 1 MB");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: pageUrl + "/GetProofPath",
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $("#hdnimgbtn1").val(r.d);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
        }

        function DocumentuploadComplete1(sender, args) {
            const size =
                (this.files[0].size / 1024 / 1024).toFixed(2);

            if (size > 1) {
                alert("File must be less than 1 MB");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: pageUrl + "/GetProofPath1",
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $("#hdnimgbtn2").val(r.d);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
        }

        function DocumentuploadComplete2(sender, args) {
            const size =
                (this.files[0].size / 1024 / 1024).toFixed(2);

            if (size > 1) {
                alert("File must be less than 1 MB");
            }
            else {
                $.ajax({
                    type: "POST",
                    url: pageUrl + "/GetProofPath2",
                    data: '',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (r) {
                        $("#hdnimgbtn3").val(r.d);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            }
        }

        $("#ddlCategory").change(function () {
            GetSubCategoryList();
        });

        function GetSupplierList() {
            $("#ddlSupplier").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSupplier",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ CountryID: 0 }),
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
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSupplier").append('<option value=' + obj[index].SupplierID + ' >' + obj[index].SupplierName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlSupplier").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlSupplier").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }
        function GetCategoryList() {
            $("#ddlCategory").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetCategory",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ CountryID: 0 }),
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
                                        if (obj[index].IsActive == "1")
                                            $("#ddlCategory").append('<option value=' + obj[index].CategoryID + ' >' + obj[index].CategoryName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetunitList() {
            $("#ddlUnit").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetUnit",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ID: $("#ddlCategory").val() }),
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
                                        if (obj[index].IsActive == "1")
                                            $("#ddlUnit").append('<option value=' + obj[index].UnitID + ' >' + obj[index].UnitName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlUnit").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlUnit").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        function GetSubCategoryList() {
            $("#ddlSubCategory").empty();
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetSubCategoryByCategoryID",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ ID: $("#ddlCategory").val() }),
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
                                        if (obj[index].IsActive == "1")
                                            $("#ddlSubCategory").append('<option value=' + obj[index].SubCategoryID + ' >' + obj[index].SubCategoryName + '</option>');
                                    }
                                }
                            }
                            else if (objResponse.Value == "NoRecord") {
                                $("#ddlSubCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
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
                        $("#ddlSubCategory").append('<option value="' + '0' + '">' + '--No Records--' + '</option>');
                    }
                },
                error: function (e) {
                    $.jGrowl("Error  Occured", { sticky: false, theme: 'danger', life: jGrowlLife });
                }
            });
            return false;
        }

        $("#btnAddNew").click(function () {
            ClearFields();
            $("#hdnID").val("");
            $("#btnSave").show();
            $("#btnUpdate").hide();
            $(".modal-title").html("<i class='fa fa-plus-square'></i>&nbsp;&nbsp;Add Product");
            $('#compose-modal').modal({ show: true, backdrop: true });
            $("#txtName").focus();
            return false;
        });

        $("#btnSave,#btnUpdate").click(function () {
            if (this.id == "btnSave")
            { if (ActionAdd != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }
            else if (this.id == "btnUpdate")
            { if (ActionUpdate != "1") { $.jGrowl(_CMAccessDeined, { sticky: false, theme: 'danger', life: jGrowlLife }); return false; } }

            if ($("#txtName").val().trim() == "" || $("#txtName").val().trim() == undefined) {
                $.jGrowl("Please enter Product", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divName").addClass('has-error'); $("#txtName").focus(); return false;
            } else { $("#divName").removeClass('has-error'); }

            if ($("#txtCode").val().trim() == "" || $("#txtCode").val().length != 3 || $("#txtCode").val().trim() == undefined) {
                $.jGrowl("Please enter Code", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCode").addClass('has-error'); $("#txtCode").focus(); return false;
            } else { $("#divCode").removeClass('has-error'); }

            if ($("#ddlCategory").val() == "0" || $("#ddlCategory").val() == undefined) {
                $.jGrowl("Please Select Category", { sticky: false, theme: 'warning', life: jGrowlLife });
                $("#divCategory").addClass('has-error'); $("#ddlCategory").focus(); return false;
            } else { $("#divCategory").removeClass('has-error'); }

            var Obj = new Object();
            Obj.ProductID = 0;
            Obj.ProductName = $("#txtName").val().trim();
            Obj.ProductCode = $("#txtCode").val();

            var ObjCategory = new Object();
            ObjCategory.CategoryID = $("#ddlCategory").val();
            Obj.Category = ObjCategory;

            var ObjSubCategory = new Object();
            ObjSubCategory.SubCategoryID = $("#ddlSubCategory").val();
            Obj.SubCategory = ObjSubCategory;

            var ObjSupplier = new Object();
            ObjSupplier.SupplierID = $("#ddlSupplier").val();
            Obj.Supplier = ObjSupplier;

            var ObjUnit = new Object();
            ObjUnit.UnitID = $("#ddlUnit").val();
            Obj.Unit = ObjUnit;
            Obj.SMSCode = $("#txtSupplierCode").val();
            Obj.ProductImage1 = "images/ProductImages/" + $("#hdnimgbtn1").val();
            Obj.ProductImage2 = "images/ProductImages/" + $("#hdnimgbtn2").val();
            Obj.ProductImage3 = "images/ProductImages/" + $("#hdnimgbtn3").val();
            Obj.IsActive = $("#chkStatus").is(':checked') ? "1" : "0";
            
            var sMethodName;
            if ($("#hdnID").val() > 0) {
                Obj.ProductID = $("#hdnID").val();
                sMethodName = "UpdateProduct";
            }
            else { sMethodName = "AddProduct"; }

            SaveandUpdateProduct(Obj, sMethodName);

            return false;
        });

        $("#btnClose").click(function () {
            $('#compose-modal').modal('hide');
            return false;
        });

        function ClearFields() {
            $("#txtName").val("");
            $("#txtCode").val("");
            $("#chkStatus").prop("checked", true);
            $("#ddlCategory").val(null).change();
            $("#divName").removeClass('has-error');
            $("#divCategory").removeClass('has-error');
            return false;
        }

        function SaveandUpdateProduct(Obj, sMethodName) {
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

                                if (sMethodName == "AddProduct")
                                { $.jGrowl("Added Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }
                                else if (sMethodName == "UpdateProduct")
                                { $.jGrowl("Updated Successfully", { sticky: false, theme: 'success', life: jGrowlLife }); }

                                $('#compose-modal').modal('hide');
                            }
                        }
                        else if (objResponse.Status == "Error") {
                            if (objResponse.Value == "0") {
                                window.location = "frmLogin.aspx";
                            }
                            else if (objResponse.Value == "Product_A_01" || objResponse.Value == "Product_U_01") {
                                $.jGrowl(_CMAlreadyExits, { sticky: false, theme: 'danger', life: jGrowlLife });
                            }
                            else if (objResponse.Value == "ProductC_A_01" || objResponse.Value == "ProductC_U_01") {
                                $.jGrowl("Code Already Exists", { sticky: false, theme: 'danger', life: jGrowlLife });
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

        $("#btnUpload").click(function (evt) {
            var fileUpload = $("#fupload").get(0);
            var files = fileUpload.files;

            var data = new FormData();
            for (var i = 0; i < files.length; i++) {
                data.append(files[i].name, files[i]);
            }

            $.ajax({
                url: "FileUploadHandler.ashx",
                type: "POST",
                data: data,
                contentType: false,
                processData: false,
                success: function (result) { alert(result); },
                error: function (err) {
                    alert(err.statusText)
                }
            });

            evt.preventDefault();
        });  

        function EditRecord(id) {
            dProgress(true);
            $.ajax({
                type: "POST",
                url: "WebServices/VHMSService.svc/GetProductByID",
                data: JSON.stringify({ ID: id }),
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

                                    $("#hdnID").val(obj.ProductID);
                                    $("#txtName").val(obj.ProductName);
                                    $("#txtCode").val(obj.ProductCode);
                                    $("#txtSupplierCode").val(obj.SMSCode);
                                    $("#ddlCategory").val(obj.Category.CategoryID).change();
                                    $("#ddlSubCategory").val(obj.SubCategory.SubCategoryID).change();
                                    $("#ddlSupplier").val(obj.Supplier.SupplierID).change();
                                    $("#ddlUnit").val(obj.Unit.UnitID).change();
                                    $("[id*=ProofimgCapture]").css("visibility", "visible");
                                    $("[id*=ProofimgCapture]").attr("src", obj.ProductImage1);
                                    $("[id*=ProofBackimgCapture]").css("visibility", "visible");
                                    $("[id*=ProofBackimgCapture]").attr("src", obj.ProductImage2);
                                    $("[id*=Register1Capture]").css("visibility", "visible");
                                    $("[id*=Register1Capture]").attr("src", obj.ProductImage3);
                                    $("#chkStatus").prop("checked", obj.IsActive ? true : false);

                                    $('#compose-modal').modal({ show: true, backdrop: true });
                                    $(".modal-title").html("<i class='fa fa-pencil'></i>&nbsp;&nbsp;Edit Product");
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

    </script>
    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmProduct.aspx") %>';
        <%--$(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                },
                onSave: function (data) {
                    if ($("#HidImage").val() == "ProofImage") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=ProofimgCapture]").css("visibility", "visible");
                                $("[id*=ProofimgCapture]").attr("src", r.d);
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else if ($("#HidImage").val() == "ProofBackImage") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=ProofBackimgCapture]").css("visibility", "visible");
                                $("[id*=ProofBackimgCapture]").attr("src", r.d);
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=Register1Capture]").css("visibility", "visible");
                                $("[id*=Register1Capture]").attr("src", r.d);
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }
            });
        });--%>
        //function Capture() {
        //    $("#HidImage").val("ProofImage");
        //    webcam.capture();
        //    return false;
        //}

        //function Capture1() {
        //    $("#HidImage").val("Register1Image");
        //    webcam.capture();

        //    return false;
        //}
        //function Capture2() {
        //    $("#HidImage").val("Register2Image");
        //    webcam.capture();

        //    return false;
        //}


        //$("#imgbtn2").change(function () {
        //    if (this.files && this.files[0]) {
        //        var reader = new FileReader();

        //        reader.onload = function () {
        //            $("[id*=ProofBackimgCapture]").css("visibility", "visible");
        //            $("[id*=ProofBackimgCapture]").attr("src", reader.result);
        //        }
        //        reader.readAsDataURL(this.files[0]);

        //        $.ajax({
        //            type: "POST",
        //            url: pageUrl + "/GetImage",
        //            data: '',
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (r) {
        //            },
        //            failure: function (response) {
        //                alert(response.d);
        //            }
        //        });
        //    }
        //});

       

        function uploadStarted() {

        }
    </script>
</asp:Content>


