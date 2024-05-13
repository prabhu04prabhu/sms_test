<%@ Page Title="" Language="C#" MasterPageFile="~/VHMSMasterPage.master" AutoEventWireup="true" CodeFile="frmAccessDenied.aspx.cs" Inherits="frmAccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="VHMSWebHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="VHMSWebContent" runat="Server">
    <div class="container-wrapper">
        <section class="content-header">
        </section>
        <section class="content">
            <div class="box box-danger">
                <div class="box-header with-border">
                    <i class="fa fa-warning text-red"></i>
                    <h3 class="box-title">Access Denied !</h3>
                </div>
                <!-- /.box-header -->
                <div class="box-body">
                    <p>
                        This URL is valid but you are not authorized for this content. For more information,
                        <b>Please contact Administrator.</b>
                    </p>
                    <p>
                        <i class="fa fa-user"></i>You're signed in as <b>
                            <%= Session["UserName"].ToString().ToUpper()%></b>
                    </p>
                    <p>
                        Please sign in as the user with proper permissions.<a class="btn btn-danger pull-right"
                            href="frmDefault.aspx"><i class="fa fa-home fa-2x"> </i>&nbsp;&nbsp;Go
                            back</a>
                    </p>
                </div>
                <!-- /.box-body -->
            </div>
        </section>
    </div>
    <script type="text/javascript">
        $("#btngoback").click(function () {
            window.location = "frmDefault.aspx";
            return false;
        });
    </script>
</asp:Content>

