<%@ Page Title="Print" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmPrintUnpaid.aspx.cs" Inherits="frmPrintUnpaid" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" Runat="Server">
     <style>
        @media (max-width:600px) {
            .table > thead > tr > th, .table > tbody > tr > th, .table > tfoot > tr > th, .table > thead > tr > td, .table > tbody > tr > td, .table > tfoot > tr > td {
                padding: 1px 5px;
            }

            .fixed .main-header, .content-header > h1, .content-header > .breadcrumb, .content-header br {
                display: none;
                visibility: hidden;
            }

            .fixed .content-wrapper {
                padding-top: 0px;
            }


            .box.box-warning.box-solid {
                height: auto !important;
            }

            .box-body .btn-group {
                margin: 0px !important;
                padding: 0px !important;
            }

            .box-body .btn-info {
                margin-left: 15px !important;
                float: left;
                padding: 5px 0px;
                width: 58px;
            }
        }

        .lblTotal {
            text-align: right;
            font-weight: bold;
            display: block;
            font-size: 16px;
        }

        .table-responsive {
            overflow: hidden;
            overflow-x: scroll;
            -webkit-overflow-scrolling: touch;
        }

        .MyCalendar {
            z-index: 100000;
        }
    </style>
    <section class="content">
        <div class="row table-responsive" id="divRecords"><br /><asp:Label ID="lblTotal" CssClass="lblTotal" runat="server" />
                        <asp:GridView ID="gvUnPaid"  runat="server" Caption="Unpaid Reports" CaptionAlign="Top" CssClass="table table-striped table-bordered table-responsive dTableR"
                            AutoGenerateColumns="false" GridLines="None" DataKeyNames="PK_RegisterID"
                            EmptyDataText="No Records Found" OnRowDataBound="GridView2_RowDataBound" ShowFooter="true"  AllowSorting="true" OnDataBound="OnDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="S.No">
                                    <ItemTemplate>
                                        <%#Container.DataItemIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="Acc. No" ReadOnly="true" DataField="AccountNo" />
                                <asp:BoundField HeaderText="Customer Name" ReadOnly="true" DataField="CustomerName" />
                                <asp:BoundField HeaderText="Customer PhoneNo" ReadOnly="true" DataField="MobileNo" />
                                <asp:BoundField HeaderText="Emp. Code" ReadOnly="true" DataField="EmployeeCode" />
                                <asp:BoundField HeaderText="Branch Name" ReadOnly="true" DataField="BranchName" />
                                <asp:BoundField HeaderText="Sch." ReadOnly="true" DataField="ChitName" />
                                <asp:BoundField HeaderText="Dur." ReadOnly="true" DataField="Duration"/>
                                <asp:BoundField HeaderText="Installment Amount" ReadOnly="true" DataField="InstallmentAmount" />
                                 <asp:BoundField HeaderText="Dues" ReadOnly="true" DataField="NoOfDues" />
                                <asp:TemplateField HeaderText="Balance Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBalance" runat="server" Style="width: 10px;" Text='<%# ""+Eval("Balance").ToString()%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle CssClass="cssPager" HorizontalAlign="Center" />
                        </asp:GridView>
                    </div>                
            </section>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" Runat="Server">
</asp:Content>

