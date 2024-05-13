<%@ Page Title="New Patient Report" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmNewPatientReport.aspx.cs" Inherits="frmNewPatientReport" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
    <%--<style>
        div.dt-buttons {
            float: right !important;
            margin-left: 10px !important;
        }
    </style>

    <link rel="stylesheet" href="https://cdn.datatables.net/buttons/1.1.0/css/buttons.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/1.10.10/css/jquery.dataTables.min.css" type="text/css" />
    <link rel="stylesheet" href="https://cdn.datatables.net/select/1.1.0/css/select.dataTables.min.css" type="text/css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">

    <div class="container-wrapper">
        <section class="content-header">
            <h1>New Patient Report
            </h1>
            <ol class="breadcrumb">
                <li><a href="frmDefault.aspx"><i class="fa fa-home"></i>Home</a></li>
                <li><a href="#">Report</a></li>
                <li class="active">Patient Report</li>
            </ol>
            <div class="pull-right">
            </div>
            <br />
            <br />
            <section class="content">

                <table border="0" class="table table-striped table-bordered table-responsive dTableR">
                    <div class="box box-warning box-solid" id="divFilter">

                        <div class="box-header with-border">
                            <div class="box-title">
                                Filter Options
                            </div>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                <div class="form-group col-md-2 col-sm-4" id="divDOB">
                                    <label>
                                        From</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOB"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOB" AutoComplete="off" runat="server" Width="150" Height="30" OnTextChanged="ddlPayment_SelectedIndexChanged" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDOB"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divDOR">
                                    <label>
                                        To</label><span class="text-danger">*</span>
                                    <div class="input-group date form_date" data-date-format="dd/MM/yyyy HH:ii P" data-link-field="txtDOR"
                                        data-link-format="dd/MM/yyyy">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar glyphicon glyphicon-calendar"></i>
                                        </div>
                                        <asp:TextBox ID="txtDOR" AutoComplete="off" runat="server" Width="150" Height="30" OnTextChanged="ddlPayment_SelectedIndexChanged" AutoPostBack="true"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDOR"
                                            CssClass="MyCalendar" Format="dd/MM/yyyy" PopupButtonID="Image1" />
                                    </div>
                                </div>
                                <div class="form-group col-md-2" id="divCategory">
                                    <label>
                                        Description</label><span class="text-danger">*</span>

                                    <asp:DropDownList ID="ddlCategory" Width="200" Height="30"
                                        OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged" AutoPostBack="true" runat="server">
                                        <asp:ListItem Text="All" Value="0"></asp:ListItem>                                        
                                        <asp:ListItem Text="New Gynaecology" Value="1"></asp:ListItem>
                                        <asp:ListItem Text="New ANC" Value="2"></asp:ListItem>
                                        <asp:ListItem Text="New Fertility" Value="3"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-2" id="divHRefer">
                                    <label>
                                        Referred by</label><span class="text-danger">*</span>

                                    <asp:DropDownList ID="ddlHRefer" OnSelectedIndexChanged="ddlPayment_SelectedIndexChanged"
                                        AutoPostBack="true" Width="200" Height="30" runat="server">
                                        <asp:ListItem Text="All" Value="All"></asp:ListItem>
                                        <asp:ListItem Text="Media" Value="Media"></asp:ListItem>
                                        <asp:ListItem Text="Internet" Value="Internet"></asp:ListItem>
                                        <asp:ListItem Text="Doctor" Value="Doctor"></asp:ListItem>
                                        <asp:ListItem Text="Others" Value="Others"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnAddNew" Text="Excel" CausesValidation="false" CssClass="btn btn-info" OnClick="btnAddNew_Click" runat="server" />
                                </div>
                                <div class="form-group col-md-1">
                                    <asp:Button ID="btnPDF" Text="PDF" CausesValidation="false" CssClass="btn btn-info" OnClick="btnPDF_Click" runat="server" />
                                </div>
                            </div>
                        </div>

                    </div>

                    <div class="row" id="divRecords">
                        <asp:GridView ID="gvProductMas" runat="server" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_PatientID"
                            EmptyDataText="No Records Found" AllowSorting="true">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="OPD NO#" ReadOnly="true" DataField="OPDNo" />
                                <asp:BoundField HeaderText="Category" ReadOnly="true" DataField="Category" />
                                <asp:BoundField HeaderText="Patient Name" ReadOnly="true" DataField="WName" />
                                <asp:BoundField HeaderText="Husband Name" ReadOnly="true" DataField="HName" />
                                <asp:BoundField HeaderText="Phone" ReadOnly="true" DataField="WMobileNo" />
                                <asp:BoundField HeaderText="Address" ReadOnly="true" DataField="WAddress" />
                                <%--<asp:BoundField HeaderText="Email" ReadOnly="true" DataField="WEmail" />--%>
                                <asp:BoundField HeaderText="Referred BY" ReadOnly="true" DataField="WReferredBy" />
                                <asp:BoundField HeaderText="Reference details" ReadOnly="true" DataField="WReferredDetails" />
                                <%--<asp:BoundField HeaderText="Doctor Mobile No" ReadOnly="true" DataField="RefDoctorMobileNo" />--%>
                            </Columns>
                            <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>
                </table>
            </section>
        </section>
    </div>

    <script type="text/javascript">
       
    </script>


    <script type="text/javascript">

        $(document).ready(function () {

            //$("#txtDOB,#txtDOR").attr("data-link-format", "dd/MM/yyyy");
            //$("#txtDOB,#txtDOR").datetimepicker({
            //    pickTime: false,
            //    useCurrent: true,
            //    format: 'DD/MM/YYYY'
            //});
        });
    </script>
    <style>
        .MyCalendar {
            background-color: white !important;
        }
    </style>
</asp:Content>


