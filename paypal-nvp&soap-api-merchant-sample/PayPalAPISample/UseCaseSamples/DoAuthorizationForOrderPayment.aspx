<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoAuthorizationForOrderPayment.aspx.cs"
    Inherits="PayPalAPISample.UseCaseSamples.DoAuthorizationForOrderPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK - DoAuthorization</title>
    <link href="../APICalls/sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>DoAuthorization</h3>
            <div id="apidetails"><ul><li><i>Used to <b>Authorize</b> Order Payment created using <b>ExpressCheckout</b></i></li></ul></div>
        </div>
        <form id="form1" runat="server" method="post">
            <div id="request_form">
                <div class="params">
                    <div class="param_name">TransactionID*(Transaction ID via ExpressCheckout with PaymentType "Order")</div>
                    <div class="param_value">
                        <asp:TextBox runat="server" ID="authID" ReadOnly="true" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">Amount*</div>
                    <div class="param_value">
                        <input type="text" name="amt" value="1.00" size="50" maxlength="260" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">Currency Code*</div>
                    <div class="param_value">
                        <input type="text" name="currencyCode" value="USD" size="50" maxlength="260" />
                    </div>
                </div>
            </div>
            <br />
            <div class="submit">
                <asp:Button ID="ButtonPayments" Text="DoAuthorization" runat="server" PostBackUrl="~/UseCaseSamples/RequestResponse.aspx" />
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
