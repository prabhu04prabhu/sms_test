<%@ Page Title="LetterPad" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmLetterPad_old.aspx.cs" Inherits="frmLetterPad_old" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit.HTMLEditor"
    TagPrefix="HTMLEditor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <link href="css/print.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper hidden">
        <section class="content">
            <div class="box box-solid box-primary" id="divLetterPad">
                <div class="box-header with-border">
                    <div class="box-title">
                        <i class="fa fa-pencil-square"></i>&nbsp;&nbsp;Letter Pad
                    </div>
                </div>
                <div class="box-body">
                    <div class="row" style="margin-left: -100px; width: 99%;">

                        <HTMLEditor:Editor ID="html_Description" runat="server" AutoFocus="true" Height="500px" />

                    </div>
                    
                </div>
                <div class="modal-footer clearfix">

                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-danger pull-left" Text="Print" OnClick="btnPrint_Click" />
                    <asp:Button ID="Test" runat="server" CssClass="btn btn-danger pull-left" Text="Print" OnClick="btntest_Click" />
                </div>
                
            </div>
            <div class="content" style="display:none">
                        <div id="divPdf" runat="server" style="text-align: left;">
                            <div style="background-color: white;" id="divOPInvoice" runat="server"></div>
                        </div>
                    </div>
        </section>
    </div>

    <input type="hidden" id="hdnLetterPadID" />
    <script type="text/javascript">
        $(document).ready(function () {
            //var htmlEditorExtender = $('.ajax__html_editor_extender_texteditor');
            //htmlEditorExtender.html(tableToBeSetInsideEditPanel);
            //document.getElementById("VHMSWebContent_html_Description").contentWindow = 
            <%--var htmlEditor = $find("<%= html_Description.ClientID %>");
            htmlEditor.set_content("<table><tr><td>bfgbfgbfg</td></tr></table>");--%>
              <%--  var editor = $find("<%= html_Description.ClientID %>");
                var editPanel = editor.get_editPanel();
                var designMode = AjaxControlToolkit.HTMLEditor.ActiveModeType.Design;
                if (editPanel.get_activeMode() == designMode) {
                    var designPanel = editPanel.get_modePanels()[designMode];
                    designPanel.insertHTML("<table><tr><td>dsvdsvsd</td></tr></table>");
                }
              --%>

            ActionAdd = '<%=Session["ActionAdd"]%>';
            ActionUpdate = '<%=Session["ActionUpdate"]%>';
            ActionDelete = '<%=Session["ActionDelete"]%>';
            ActionView = '<%=Session["ActionView"]%>';

            pLoadingSetup(false);
            pLoadingSetup(true);
        });
        $('#html_Description').append("testftess");
    </script>
</asp:Content>

