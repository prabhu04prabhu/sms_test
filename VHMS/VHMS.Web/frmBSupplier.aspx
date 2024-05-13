<%@ Page Title="Supplier" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmBSupplier.aspx.cs" Inherits="frmBSupplier" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header">
            <h1>Supplier
            </h1>
            <div class="form-group  col-md-4" style="margin-left: 255px; margin-top: -34px;">
                <label>
                    Supplier Name</label>
                <select id="ddlCategoryName" class="form-control select2" data-placeholder="Select Category Name" tabindex="-1"></select>
            </div>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Billing</a></li>
                <li class="active">Supplier</li>
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
                                            <th>Supplier
                                            </th>
                                            <th>Code
                                            </th>
                                            <th>GST NO
                                            </th>
                                            <th>City
                                            </th>
                                            <th>Area
                                            </th>
                                            <th class='hidden-xs'>Phone No
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
                                    <input type="text" class="form-control" id="txtName" style="text-transform: uppercase" placeholder="Supplier Name"
                                        maxlength="255" tabindex="1" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divCode">
                                <label>
                                    Code</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtCode" placeholder="Supplier Code" style="text-transform: uppercase"
                                    maxlength="5" tabindex="2" onkeypress="return onlyAlphabets(event,this);" autocomplete="off" />
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
                            <div class="form-group col-md-12" id="divAddress">
                                <label>
                                    Address</label>
                                <textarea id="txtAddress" class="form-control" maxlength="255" tabindex="5" rows="3" aria-autocomplete="none"></textarea>
                            </div>
                            <div class="form-group col-md-3" id="divState">
                                <label>
                                    State</label><span class="text-danger">*</span>
                                <select id="ddlState" class="form-control select2" data-placeholder="Select State" tabindex="6">
                                </select>
                            </div>
                            <div class="form-group col-md-3" id="divCity">
                                <label>
                                    City</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtCity" placeholder="City"
                                    maxlength="100" tabindex="7" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divTaluk">
                                <label>
                                    Taluk</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtTaluk" placeholder="Taluk"
                                    maxlength="100" tabindex="8" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divArea">
                                <label>
                                    Area</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtArea" placeholder="Please enter Area"
                                    maxlength="120" tabindex="9" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divPincode">
                                <label>
                                    Pincode</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtPincode" placeholder="Pincode"
                                    maxlength="6" tabindex="10" onkeypress="return IsNumeric(event)" autocomplete="off" />
                            </div>

                            <div class="form-group col-md-2" id="divtxtLandline">
                                <label>
                                    LandLine</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-phone"></i></div>
                                    <input type="text" class="form-control" id="txtLandline" placeholder="Landline" maxlength="13" onkeypress="return IsNumeric(event)"
                                        tabindex="11" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divFax">
                                <label>
                                    GST No</label>
                                <div class="input-group">
                                    <input type="text" class="form-control" id="txtFax" placeholder="GST No" maxlength="20"
                                        tabindex="12" autocomplete="off" />
                                </div>
                                <button id="btnLink" type="button" class="btn btn-link">
                                    <i class="fa fa-link" aria-hidden="true"></i>&nbsp;&nbsp;
                              Verify GSTNO</button>
                            </div>
                            <div class="form-group col-md-3" id="divEmail">
                                <label>
                                    Email</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                    <input type="text" class="form-control" id="txtEmail" placeholder="Email" maxlength="50" tabindex="13" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-3" id="divWeb">
                                <label>
                                    Website</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-internet-explorer"></i></div>
                                    <input type="text" class="form-control" id="txtWebSite" placeholder="Website" maxlength="150" tabindex="14" autocomplete="off" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divtxtDays">
                                <label>
                                    Due Days</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-calendar"></i></div>
                                    <input type="text" class="form-control" id="txtDays" placeholder="Days" maxlength="13" onkeypress="return IsNumeric(event)"
                                        tabindex="11" autocomplete="off" />
                                </div>
                            </div>
                            <div class="row"></div>
                                <div class="form-group col-md-2" id="divAccountNo">
                                <label>
                                    Account No</label>
                                <input type="text" class="form-control" id="txtAccountNo" placeholder="Account No"
                                    maxlength="20" tabindex="11" onkeypress="return isNumberKey(event)" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divBankName">
                                <label>
                                    Bank Name</label>
                                <input type="text" class="form-control" id="txtBankName" placeholder="Please enter BankName"
                                    maxlength="100" tabindex="12" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divBranchName">
                                <label>
                                    Branch Name</label>
                                <input type="text" class="form-control" id="txtBranchName" placeholder="Please enter BranchName"
                                    maxlength="50" tabindex="13" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divAccountHolderName">
                                <label>
                                    Account Holder Name</label>
                                <input type="text" class="form-control" id="txtAccountHolderName" placeholder="Please enter AccountHolderName"
                                    maxlength="50" tabindex="14" autocomplete="off" />
                            </div>
                            <div class="form-group col-md-2" id="divIFSCCode">
                                <label>
                                    IFSC Code</label>
                                <input type="text" class="form-control" id="txtIFSCCode" placeholder="Please enter IFSCCode"
                                    maxlength="20" tabindex="15" autocomplete="off" />
                            </div>
                              <div class="form-group col-md-2" id="divTransport">
                            <label>
                                Transport Name</label><span class="text-danger">*</span>
                            <select id="ddlTransport" class="form-control select2" data-placeholder="Select Transport Name" tabindex="4"></select>
                        </div>

                            <div class="form-group col-md-2">
                                <div class="checkbox" style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkStatus" checked="checked" tabindex="15" />Active
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-md-2">
                                <div class="checkbox" style="margin-top: 30px">
                                    <label>
                                        <input type="checkbox" id="chkRateUpdate" tabindex="16" />Rate review flag
                                    </label>
                                </div>
                            </div>
                             <div class="form-group col-md-4">
                            <label>
                                Image 1</label>
                            <button id="btnClearImage1" type="button" style="margin-top: -11px; color: deeppink;" class="btn btn-link">
                                Clear</button>
                            <input name="imagePurchasefile" type="file" id="imagePurchasefile" data-image-src="imgUploadPurchase1_view" accept="image/*" onchange="ResizeImage('imagePurchasefile');" />
                            <a href="#" data-fancybox="images">
                                <img src="" id="imgUploadPurchase1_view" class="preview_img" alt="" style="width: 280px;" />
                            </a>
                        </div>
                        </div>
                        <div class="modal-footer clearfix">
                            <button id="btnSave" type="button" class="btn btn-info pull-left" tabindex="17">
                                <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                            <button id="btnUpdate" type="button" class="btn btn-info pull-left" tabindex="18">
                                <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                            <button type="button" class="btn btn-danger pull-right" id="btnClose" tabindex="19">
                                <i class="fa fa-times"></i>&nbsp;&nbsp;
                                Close</button>
                        </div>
                    </div>
                </div>
            </div>
        </section>
    </div>
    <input type="hidden" id="hdnSupplierID" />
    <script src="UserDefined_Js/Billing/JSupplier.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Billing/JSupplier.js") %>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';
        var pageUrl = '<%=ResolveUrl("~/frmBSupplier.aspx") %>';
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

        function DocumentuploadComplete(sender, args) {
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetProofPath",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    $("#hdnImgupload1").val(r.d);
                    $get("imgUpload1").src = "./images/Documents/Supplier/" + r.d;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }


    </script>
      <script type="text/javascript" src="JS/fancybox/jquery.fancybox.js?v=2.1.4"></script>
    <link rel="stylesheet" type="text/css" href="JS/fancybox/jquery.fancybox.css?v=2.1.4" media="screen" />
       <script type="text/javascript">

           $('img.preview_img').on('load', function () {
               //console.log($(this).attr('src'));
               $(this).parent("a").attr("href", $(this).attr("src"));
           });

           function ResizeImage(img_id) {

               var filesToUpload = document.getElementById(img_id).files;
               var file = filesToUpload[0];

               // Create an image
               var img = document.createElement("img");
               // Create a file reader
               var reader = new FileReader();
               // Set the image once loaded into file reader
               reader.onload = function (e) {
                   //img.src = e.target.result;
                   var img = new Image();

                   img.src = this.result;

                   setTimeout(function () {
                       var canvas = document.createElement("canvas");

                       var MAX_WIDTH = 1500;
                       var MAX_HEIGHT = 1000;
                       var width = img.width;
                       var height = img.height;

                       if (width > height) {
                           if (width > MAX_WIDTH) {
                               height *= MAX_WIDTH / width;
                               width = MAX_WIDTH;
                           }
                       } else {
                           if (height > MAX_HEIGHT) {
                               width *= MAX_HEIGHT / height;
                               height = MAX_HEIGHT;
                           }
                       }
                       canvas.width = width;
                       canvas.height = height;
                       var ctx = canvas.getContext("2d");
                       ctx.drawImage(img, 0, 0, width, height);
                       var dataurl = canvas.toDataURL("image/jpeg");
                       var image_view = $("#" + img_id).attr("data-image-src");
                       document.getElementById(image_view).src = dataurl;
                       $("#" + image_view).css({ "visibility": "visible", "display": "block" });
                       saveimage(image_view);

                   }, 100);
               }
               // Load files into file reader
               reader.readAsDataURL(file);
           }
    </script>
</asp:Content>
