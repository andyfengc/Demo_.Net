<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ParallelPayment.aspx.cs"
    Inherits="PayPalAPISample.UseCaseSamples.ParallelPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK = Parallel Payment using SetExpressCheckout</title>
    <link href="../APICalls/sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>ParallelPaymentUsingSetExpressCheckout</h3>
            <div id="apidetails">
                <ul><li><i>Use <b>ExpressCheckout</b> for making Payments to multiple receivers</i></li></ul>
            </div>
        </div>
        <form id="form1" runat="server" method="post">
        <div id="request_form">
            <div class="params">
                <div class="param_name">
                   <asp:Label runat="server" ID="LabelBuyerMail" Text="Buyer Email" Font-Bold="true" />
                   <asp:TextBox runat="server" ID="buyerMail" Text="platfo_1255077030_biz@gmail.com" Width="20%" />
                </div>
            </div>
        </div>
         <br />
            <div class="section_header">
                <asp:Label runat="server" ID="LabelPaymentDetails" Text="Payment Details" Font-Underline="true"
                    Font-Bold="true" />
            </div>
        <br />
        <div class="params">
            <div class="param_name">
                Payment Type</div>
            <div class="param_value">
                <asp:DropDownList runat="server" ID="paymentType">
                    <asp:ListItem Text="Sale" Value="SALE" />
                    <asp:ListItem Text="Authorization" Value="AUTHORIZATION" />
                    <asp:ListItem Text="Order" Value="ORDER" />
                </asp:DropDownList>
            </div>
        </div>       
        <div class="params">
            <div class="param_name">
                Currency Code</div>
            <div class="param_value">
                <asp:TextBox runat="server" ID="currencyCode" Text="USD" />
            </div>
        </div>
        <div class="params">
            <div class="param_name">
                Order Total</div>
            <div class="param_value">
                <asp:TextBox runat="server" ID="orderTotal" Text="1.00" />
            </div>
        </div>
        <div class="param_name">
            Receiver Emails</div>
        <div class="param_value">
            1.<asp:TextBox runat="server" ID="receiverEmail_0" Text="platfo_1255170694_biz@gmail.com" Width="20%" />
            <asp:TextBox runat="server" ID="paymentRequestID_0" Text="CART286-PAYMENT0" Width="20%" />
        </div>
        <br />
        <div class="param_value">
            2.<asp:TextBox runat="server" ID="receiverEmail_1" Text="platfo_1255611349_biz@gmail.com" Width="20%" />
            <asp:TextBox runat="server" ID="paymentRequestID_1" Text="CART286-PAYMENT1" Width="20%" />
        </div>
        <br />
        <div class="submit">
            <asp:Button ID="ButtonPayments" Text="ParallelPayment" runat="server" PostBackUrl="~/UseCaseSamples/RequestResponse.aspx" />
        </div>
        <br />
        <asp:HyperLink runat="server" ID="HyperLinkHome" NavigateUrl="~/Default.aspx" Text="Home" />
        <br />
        <br />
        <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="javascript:history.back();" Text="Back" />
        </form>
    </div>
</body>
</html>
