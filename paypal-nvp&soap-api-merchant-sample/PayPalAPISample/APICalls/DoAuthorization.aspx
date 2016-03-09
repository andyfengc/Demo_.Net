<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoAuthorization.aspx.cs"
    Inherits="PayPalAPISample.APICalls.DoAuthorization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoAuthorization</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                DoAuthorization</h3>
            <div id="apidetails">
                Used to authorize a payment. To use DoAuthorization you must have previously created
                a customer order (for example, by using DoExpressCheckoutPayment or DoDirectPayment
                with paymentAction set to "order").</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Order's Transaction Id * <a href="SetExpressCheckout.aspx?paymentAction=ORDER">Create
                        an order</a></div>
                <div class="param_value">
                    <input type="text" name="transactionId" id="transactionId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Amount to authorize *</div>
                <div class="param_value">
                    <input type="text" name="amount" id="amount" value="1.00" runat="server" />
                    <asp:DropDownList ID="currencyCode" runat="server">
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="USD" Value="USD" Selected="true" />
                    </asp:DropDownList>
                </div>
                <div class="submit">
                    <input id="submit" type="submit" name="submit" value="Submit" runat="server" onserverclick="Submit_Click" />
                    <br />
                    <br />
                    <a href="../Default.aspx">Home</a>&nbsp;&nbsp;<a href="javascript:history.back();">Back</a>
                </div>
            </div>
        </form>
        <div id="relatedcalls">
        </div>
    </div>
</body>
</html>
