<%@ Page Title="Patient" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPatient.aspx.cs" Inherits="frmPatient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content-header" id="secHeader">
            <div id="divTitle">
                <h3>Patient
                </h3>
            </div>
            <br />

        </section>
        <section class="content">
            <div class="box box-info box-solid" id="divPatient">
                <div class="box-header with-border">
                    <div class="modal-title"></div>
                    <div class="box-tools pull-right">
                        <button class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                    </div>
                </div>
                <div class="modal-body">
                    <div class="divImage" style="width: auto; border: 1px solid #141514; padding: 10px; margin: 10px;">
                        <label style="position: relative; left: 45%"><u><b>Patient Photo</b></u></label>
                        <div class="row">
                            <div class="col-sm-3 form-group">
                                <label>Live Camera</label>
                                <div id="webcam"></div>
                            </div>
                            <div class="col-sm-3 form-group">
                                <label>Wife Image</label>
                                <asp:Image ID="WimgCapture" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                                <asp:Button ID="btnCapture" Text="Capture" runat="server" OnClientClick="return Capture();" /><span id="camStatus"></span>
                            </div>
                            <div class="col-sm-3 form-group">
                                <label>Husband Image</label>
                                <asp:Image ID="HimgCapture" runat="server" Style="visibility: hidden; width: 320px; height: 240px" />
                                <asp:Button ID="btnHCapture" Text="Capture" runat="server" OnClientClick="return Capture1();" /><span id="HcamStatus"></span>
                            </div>
                        </div>
                    </div>
                    <div class="divwife" style="width: auto; border: 1px solid #141514; padding: 10px; margin: 10px;">
                        <label style="position: relative; left: 46%"><u><b>WIFE DETAILS</b></u></label>
                        <div class="row">
                            <div class="form-group col-md-2" id="divCategory">
                                <label>
                                    Category</label><span class="text-danger">*</span>
                                <select id="ddlCategory" class="form-control" tabindex="1">
                                    <%--<option selected="selected" value="0">Select</option>--%>
                                    <option value="Fertility">Fertility</option>
                                    <option value="Gynaecology">Gynaecology</option>
                                    <option value="ANC">ANC</option>
                                </select>
                            </div>
                            <div class="form-group col-md-3" id="divWName">
                                <label>
                                    Name</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                    <input type="text" class="form-control" id="txtWName" placeholder="Wife Name"
                                        maxlength="200" tabindex="2" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divWBloodGroup">
                                <label>
                                    B. Group</label>
                                <input type="text" class="form-control" id="txtWBloodGroup" placeholder="Blood Group"
                                    maxlength="150" tabindex="3" />
                            </div>
                            <div class="form-group col-md-1" id="divWDOB">
                                <label>
                                    Age</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtWAge" placeholder="Age"
                                        maxlength="2" tabindex="4" onkeypress="return isNumberKey(event)" />
                              
                                <div id="divWDOB1" class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtWDOB"
                                    data-link-format="dd/MM/yyyy" hidden="hidden">
                                    <%--<div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>--%>
                                    <input type="text" class="form-control pull-right" tabindex="4" id="txtWDOB" placeholder="DD/MM/YYYY" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divWMobileNo">
                                <label>
                                    Mobile No</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-mobile"></i></div>
                                    <input type="text" class="form-control" id="txtWMobileNo" placeholder="Mobile No"
                                        maxlength="10" tabindex="5" onkeypress="return isNumberKey(event)" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divWEmail">
                                <label>
                                    Email</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                    <input type="text" class="form-control" id="txtWEmail" placeholder="Email"
                                        maxlength="200" tabindex="6" />
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-3" id="divWAddress">
                                <label>
                                    Address</label><span class="text-danger">*</span>
                                <textarea id="txtWAddress" class="form-control" maxlength="2000" tabindex="7" rows="3"></textarea>
                            </div>
                            <div class="form-group col-md-1" id="divWPincode">
                                <label>
                                    Pincode</label>
                                <input type="text" class="form-control" id="txtWPincode" placeholder="pincode"
                                    maxlength="6" tabindex="8"  onkeypress="return isNumberKey(event)" />
                            </div>
                            <div class="form-group col-md-2" id="divWCity">
                                <label>
                                    City</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtWCity" placeholder="City"
                                    maxlength="150" tabindex="9" />
                            </div>
                            <div class="form-group col-md-2" id="divWCountry">
                                <label>
                                    Country</label><span class="text-danger">*</span>
                               <%-- <input type="text" class="form-control" id="txtWCountry" placeholder="Country"
                                    maxlength="150" tabindex="10" />--%>
                                  <select id="ddlWCountry" class="form-control select2" data-placeholder="Select Country" tabindex="10"></select>
                            </div>
                            <div class="form-group col-md-2" id="divWNationality">
                                <label>
                                    Nationality</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtWNationality" placeholder="Nationality"
                                    maxlength="50" tabindex="11" value="Indian" />
                            </div>
                            <div class="form-group col-md-2" id="divRefer">
                                <label>
                                    Referred by</label><span class="text-danger">*</span>
                                <select id="ddlRefer" class="form-control" tabindex="12">
                                    <option selected="selected" value="0">Select</option>
                                    <option value="Media">Media</option>
                                    <option value="Internet">Internet</option>
                                    <option value="Doctor">Doctor</option>
                                    <option value="Others">Others</option>
                                </select>
                                <input type="text" class="form-control" id="txtWReferredBy" placeholder="Details"
                                    maxlength="150" tabindex="13" visible="false" />
                                <input type="text" class="form-control" id="txtDocMobileNo" placeholder="Mobile No"
                                    maxlength="15" tabindex="14" visible="false" onkeypress="return isNumberKey(event)" />
                            </div>
                        </div>
                        <div class="row">
                            <%-- <div class="col-sm-2">
                                    <label>Passport Size Photo</label>
                                </div>--%>
                          <%--  <div>
                                <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                    runat="server" ID="WPhoto" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                    OnUploadedComplete="WPhoto_UploadedComplete" OnClientUploadComplete="WImageuploadComplete" Visible="false" />
                                <asp:Label ID="lblWPhotoStatus" runat="server" Style="font-family: Arial; font-size: small;" Visible="false"></asp:Label>
                                <input type="hidden" id="hdnWPhoto" />
                                <img id="IWimage" class="IWimage" alt="" src="" style="width: 320px;height: 240px;" />

                            </div>--%>
                             <div class="form-group col-md-2" id="divWProfession">
                                <label>
                                    Profession</label>
                                <input type="text" class="form-control" id="txtWProfession" placeholder="profession"
                                    maxlength="50" tabindex="15" />
                            </div>
                            <div class="col-sm-3">
                                <label>
                                    Address Proof<br />
                                    <span style="font-size: 11px;">(Aadhaar Card / Driving License / Passport )</span></label>
                            </div>
                          
                            <div class="col-sm-3 form-group">
                                <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                    runat="server" ID="WProof" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                    OnUploadedComplete="WProof_UploadedComplete" OnClientUploadComplete="WProofuploadComplete" />
                                <asp:Label ID="lblWProofStatus" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
                                <input type="hidden" id="hdnWProof" />
                                <img id="IWProof" class="IWProof" alt="" src="" />
                                  <%--<a id="Wlink" href="images/PatientPhotos/WP190002.pdf" target="_blank"/>--%>
                                 <%--<a id="Wlink" href="images/PatientPhotos/WP190002.pdf" target="_blank">Print</a>--%>
                               <%-- <iframe id="Wprint" name="Wprint" src=""></iframe>--%>
                            </div>
                             <div >
                                <a id="Wlink" href="" target="_blank">Print</a>
                            </div>
                        </div>

                    </div>
                    <div class="divhusband" style="width: auto; border: 1px solid #141514; padding: 10px; margin: 10px;">
                        <label style="position: relative; left: 45%"><u><b>HUSBAND DETAILS</b></u></label>
                        <div class="row">
                            <div class="form-group col-md-3" id="divHName">
                                <label>
                                    Name</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-user"></i></div>
                                    <input type="text" class="form-control" id="txtHName" placeholder="Please enter Husband Name"
                                        maxlength="200" tabindex="16" />
                                </div>
                            </div>
                            <div class="form-group col-md-1" id="divHBloodGroup">
                                <label>
                                    B. Group</label>
                                <input type="text" class="form-control" id="txtHBloodGroup" placeholder="Blood Group"
                                    maxlength="150" tabindex="17"/>
                            </div>
                            <div class="form-group col-md-1" id="divHDOB">
                                 <label>
                                    Age</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtHAge" placeholder="Age"
                                        maxlength="2" tabindex="19" onkeypress="return isNumberKey(event)" />

                                <div id="divHDOB1" class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtHDOB"
                                    data-link-format="dd/MM/yyyy">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                    </div>
                                    <input type="text" class="form-control pull-right" tabindex="18" id="txtHDOB"  placeholder="DD/MM/YYYY"  />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divHMobileNo">
                                <label>
                                    Mobile No</label><span class="text-danger">*</span>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-mobile"></i></div>
                                    <input type="text" class="form-control" id="txtHMobileNo" placeholder="Mobile No"
                                        maxlength="10" tabindex="19" onkeypress="return isNumberKey(event)" />
                                </div>
                            </div>
                            <div class="form-group col-md-2" id="divHEmail">
                                <label>
                                    Email</label>
                                <div class="input-group">
                                    <div class="input-group-addon"><i class="fa fa-envelope"></i></div>
                                    <input type="text" class="form-control" id="txtHEmail" placeholder="Email"
                                        maxlength="200" tabindex="20" />
                                </div>
                            </div>
                            <div class="form-group col-md-1">
                                <div class="checkbox" style="position: relative; top: 22px;">
                                    <label>
                                        <input id="chkStatus" checked="checked" tabindex="21" type="checkbox" />Active                                   
                                    </label>
                                </div>
                            </div>
                            <div class="form-group col-md-2">
                                <div class="checkbox" style="position: relative; top: 22px;">
                                    <label>
                                        <input id="chkCopyDetails" tabindex="22" type="checkbox" />Same as wife details                                   
                                    </label>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group col-md-3" id="divHAddress">
                                <label>
                                    Address</label><span class="text-danger">*</span>
                                <textarea id="txtHAddress" class="form-control" maxlength="2000" tabindex="23" rows="3"></textarea>
                            </div>
                            <div class="form-group col-md-1" id="divHPincode">
                                <label>
                                    Pincode</label>
                                <input type="text" class="form-control" id="txtHPincode" placeholder="pincode"
                                    maxlength="6" tabindex="24"  onkeypress="return isNumberKey(event)" />
                            </div>
                            <div class="form-group col-md-2" id="divHCity">
                                <label>
                                    City</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtHCity" placeholder="City"
                                    maxlength="150" tabindex="25" />
                            </div>
                            <div class="form-group col-md-2" id="divHCountry">
                                <label>
                                    Country</label><span class="text-danger">*</span>
                                <select id="ddlHCountry" class="form-control select2" data-placeholder="Select Country" tabindex="26"></select>
                               <%-- <input type="text" class="form-control" id="txtHCountry" placeholder="Country"
                                    maxlength="150" tabindex="23" />--%>
                            </div>
                            <div class="form-group col-md-2" id="divHNationality">
                                <label>
                                    Nationality</label><span class="text-danger">*</span>
                                <input type="text" class="form-control" id="txtHNationality" placeholder="Nationality"
                                    maxlength="50" tabindex="27" value="Indian" />
                            </div>

                            <div class="form-group col-md-2" id="divHRefer">
                                <label>
                                    Referred by</label><span class="text-danger">*</span>
                                <select id="ddlHRefer" class="form-control" tabindex="28">
                                    <option selected="selected" value="selected">Select</option>
                                    <option value="Media">Media</option>
                                    <option value="Internet">Internet</option>
                                    <option value="Doctor">Doctor</option>
                                    <option value="Others">Others</option>
                                </select>
                                <input type="text" class="form-control" id="txtHReferredBy" placeholder="Details"
                                    maxlength="150" tabindex="29" visible="false" />
                            </div>
                        </div>
                        <%--<div class="col-sm-2">
                                <label>Passport Size Photo</label>
                            </div>--%>
                        <div class="row">
                            <%--<div>
                                <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                    runat="server" ID="HPhoto" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                    OnUploadedComplete="AsyncFileUpload1_UploadedComplete" OnClientUploadComplete="uploadComplete" Visible="false" />
                                <asp:Label ID="lblStatus" runat="server" Style="font-family: Arial; font-size: small;" Visible="false"></asp:Label>
                                <input type="hidden" id="hdnHImage" />
                                <img id="IHPhoto" class="IHPhoto" alt="" src="" style="width: 320px;height: 240px;"  />
                            </div>--%>
                            <div class="form-group col-md-2" id="divHProfession">
                                <label>
                                    Profession</label>
                                <input type="text" class="form-control" id="txtHProfession" placeholder="profession"
                                    maxlength="50" tabindex="30" />
                            </div>
                            <div class="col-sm-3">
                                <label>
                                    Address Proof<br />
                                    <span style="font-size: 11px;">(Aadhaar Card / Driving License / Passport )</span></label>
                            </div>
                            <%--<a id="Hlink" href="images/PatientPhotos/WP190002.pdf" target="_blank"/>--%>
                            
                            <div class="col-sm-3 form-group">                               
                                <ajaxToolkit:AsyncFileUpload CssClass="imageUploaderField"
                                    runat="server" ID="HProof" UploadingBackColor="#CCFFFF" ThrobberID="Throbber" Width="240px"
                                    OnUploadedComplete="HProof_UploadedComplete" OnClientUploadComplete="HProofuploadComplete" />
                                <asp:Label ID="lblStatus1" runat="server" Style="font-family: Arial; font-size: small;"></asp:Label>
                                <input type="hidden" id="hdnHProof" />
                                <img id="IHProof" class="IHProof" alt="" src="" />                                  
                                 <%--<input type="button" value="Open"/>--%>
                            </div>
                             <div >
                                <a id="Hlink" href="" target="_blank">Print</a>
                            </div>

                        </div>
                    </div>
                    <div class="modal-footer clearfix">
                        <button type="button" class="btn btn-danger pull-left" id="btnClose" tabindex="31">
                            <i class="fa fa-close"></i>&nbsp;&nbsp;
                                Close</button>
                        <button type="button" class="btn btn-info pull-right" id="btnSave" tabindex="32">
                            <i class="fa fa-save"></i>&nbsp;&nbsp;
                                Save</button>
                        <button type="button" class="btn btn-info pull-right" id="btnUpdate" tabindex="33">
                            <i class="fa fa-edit"></i>&nbsp;&nbsp;
                                Update</button>
                    </div>
                </div>
        </section>
    </div>
    <input type="hidden" id="hdnPatientID" />
    <input type="hidden" id="LastOPDNo" />
    <%--<input type="hidden" id="hiddenHPhoto" />
    <input type="hidden" id="hiddenWPhoto" />
    <input type="hidden" id="hiddenHProof" />
    <input type="hidden" id="hiddenWProof" />--%>
    <input type="hidden" id="HidImage" />
    <asp:HiddenField ID="hidOPDNo" runat="server" />
    <script src="UserDefined_Js/Discharge/JPatient.js?v=<%= BaseConfig.GetFileHash("UserDefined_Js/Discharge/JPatient.js") %>" type="text/javascript"></script>

    <script type="text/javascript">
        <%--  var _CMActionAdd = '<%=Session["ActionAdd"]%>';
        var _CMActionUpdate = '<%=Session["ActionUpdate"]%>';
        var _CMActionDelete = '<%=Session["ActionDelete"]%>';
        var _CMActionView = '<%=Session["ActionView"]%>';--%>

          var _CMActionAdd = 1;
        var _CMActionUpdate = 1;
        var _CMActionDelete = 1;
        var _CMActionView = 1;
        $("#hdnPatientID").val('<%=Session["PatientID"]%>');
        
        $(document).ready(function () {

            $("#txtHCity,#txtWCity,#txtHNationality,#txtWNationality").keypress(function (event) {
                var inputValue = event.which;
                if (!(inputValue >= 65 && inputValue <= 120) && (inputValue != 32 && inputValue != 0)) {
                    event.preventDefault();
                }
            });
        });
        
    </script>

    <script src='<%=ResolveUrl("~/Webcam_Plugin/jquery.webcam.js") %>' type="text/javascript"></script>

    <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmPatient.aspx") %>';
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                    //$('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    if ($("#HidImage").val() == "WImage") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=WimgCapture]").css("visibility", "visible");
                                $("[id*=WimgCapture]").attr("src", r.d);
                                $("#hdnWPhoto").val(r.d)
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/HGetCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=HimgCapture]").css("visibility", "visible");
                                $("[id*=HimgCapture]").attr("src", r.d);
                                $("#hdnHImage").val(r.d)
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
        });
        function Capture() {
            $("#HidImage").val("WImage");
            webcam.capture();
            return false;
        }
        function Capture1() {
            $("#HidImage").val("HImage");
            webcam.capture();

            return false;
        }
        function uploadStarted() {

        }

       <%-- function uploadComplete(sender, args) {
            var imgDisplay = $get("IHPhoto");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            $("#hdnHImage").val("HI" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
            img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "HI" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
        }--%>

        function HProofuploadComplete(sender, args) {
            var imgDisplay = $get("IHProof");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            $.ajax({
                type: "POST",
                url: pageUrl + "/GetProofPath",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    //$("[id*=hdnHProof]").attr("src", r.d);
                    $("#hdnHProof").val(r.d);
                    img.src = "<%=ResolveUrl(UploadFolderPath) %>" + r.d;;
                     var imgDisplay1 = $get("Hlink");
            imgDisplay1.href = "<%=ResolveUrl(UploadFolderPath) %>" + r.d;;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
           // $("#hdnHProof").val("HP" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
           // img.src = "<%=ResolveUrl(UploadFolderPath) %>"+ "HP" + $("#LastOPDNo").val()+ args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;

          
        }

       <%-- function WImageuploadComplete(sender, args) {
            var imgDisplay = $get("IWimage");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            $("#hdnWPhoto").val("WI" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
            img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "WI" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
        }--%>

        function WProofuploadComplete(sender, args) {
            var imgDisplay = $get("IWProof");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };

            $.ajax({
                type: "POST",
                url: pageUrl + "/GetProofPath",
                data: '',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                  //  $("[id*=hdnHProof]").attr("src", r.d);
                    $("#hdnWProof").val(r.d);
                    img.src = "<%=ResolveUrl(UploadFolderPath) %>" + r.d;;
                    var imgDisplay1 = $get("Wlink");
            imgDisplay1.href = "<%=ResolveUrl(UploadFolderPath) %>" + r.d;;
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
            <%--$("#hdnWProof").val("WP" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
            img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "WP" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
            var imgDisplay1 = $get("Wlink");
            imgDisplay1.href = "<%=ResolveUrl(UploadFolderPath) %>"
             + "WP" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;--%>
        }
       
        //function HPDF()
        //{
        //    var iframe = $('#Hprint')[0]; iframe.contentWindow.focus(); iframe.contentWindow.print();

        //}
        //function WPDF() {
        //    var iframe = $('#Wprint')[0]; iframe.contentWindow.focus(); iframe.contentWindow.print();

        //}
    </script>

    <%-- <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmPatient.aspx") %>';
        $(function () {
            jQuery("#webcam").webcam({
                width: 320,
                height: 240,
                mode: "save",
                swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                  //  $('#camStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                    if ("#HidImage".val() == "WImage") {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/GetCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=imgCapture]").css("visibility", "visible");
                                $("[id*=imgCapture]").attr("src", r.d);
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                    }
                    else
                    {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/HGetCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=HimgCapture]").css("visibility", "visible");
                                $("[id*=HimgCapture]").attr("src", r.d);
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
            function Capture() {
                $("#HidImage").val("WImage");
                webcam.capture();
                return false;
            }
        });
           
            
            function uploadComplete(sender, args) {
                var imgDisplay = $get("IHPhoto");
                imgDisplay.src = "images/loader.gif";
                imgDisplay.style.cssText = "";
                var img = new Image();
                img.onload = function () {
                    imgDisplay.style.cssText = "height:100px;width:100px";
                    imgDisplay.src = img.src;
                };
                $("#hdnHImage").val("HI" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
                img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "HI" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
        }

        function HProofuploadComplete(sender, args) {
            var imgDisplay = $get("IHProof");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            $("#hdnHProof").val("HP" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
            img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "HP" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
        }

        function WImageuploadComplete(sender, args) {
            var imgDisplay = $get("IWimage");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            $("#hdnWPhoto").val("WI" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
            img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "WI" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
        }

        function WProofuploadComplete(sender, args) {
            var imgDisplay = $get("IWProof");
            imgDisplay.src = "images/loader.gif";
            imgDisplay.style.cssText = "";
            var img = new Image();
            img.onload = function () {
                imgDisplay.style.cssText = "height:100px;width:100px";
                imgDisplay.src = img.src;
            };
            $("#hdnWProof").val("WP" + $("#LastOPDNo").val() + args.get_fileName().substring(args.get_fileName().lastIndexOf(".")));;
            img.src = "<%=ResolveUrl(UploadFolderPath) %>"
             + "WP" + $("#LastOPDNo").val()
             + args.get_fileName().substring(args.get_fileName().lastIndexOf("."));;
        }
       
    </script>
     <script type="text/javascript">
        var pageUrl = '<%=ResolveUrl("~/frmPatient.aspx") %>';
        jQuery("#Hwebcam").webcam({
            width: 320,
            height: 240,
            mode: "save",
            swffile: '<%=ResolveUrl("~/Webcam_Plugin/jscam.swf") %>',
                debug: function (type, status) {
                   // $('#HcamStatus').append(type + ": " + status + '<br /><br />');
                },
                onSave: function (data) {
                        $.ajax({
                            type: "POST",
                            url: pageUrl + "/HGetCapturedImage",
                            data: '',
                            contentType: "application/json; charset=utf-8",
                            dataType: "json",
                            success: function (r) {
                                $("[id*=HimgCapture]").css("visibility", "visible");
                                $("[id*=HimgCapture]").attr("src", r.d);
                            },
                            failure: function (response) {
                                alert(response.d);
                            }
                        });
                },
                onCapture: function () {
                    webcam.save(pageUrl);
                }
        });
         function Capture1() {
             HidImage.val()="HImage";
                webcam.capture();
                return false;
            }   
    </script>--%>
    <script language="javascript" type="text/javascript">
        function clearText(field) {
            if (field.defaultValue == field.value) field.value = '';
            else if (field.value == '') field.value = field.defaultValue;
        }
    </script>
</asp:Content>



