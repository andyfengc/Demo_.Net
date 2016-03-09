<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoExpressCheckoutPayment.aspx.cs"
    Inherits="PayPalAPISample.APICalls.DoExpressCheckout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoExpressCheckoutPayment</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                DoExpressCheckoutPayment</h3>
            <div id="apidetails">
                Completes an Express Checkout transaction. If you set up a billing agreement in
                your SetExpressCheckout API call, the billing agreement is created when you call
                the DoExpressCheckoutPayment API operation.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Express Checkout Token *</div>
                <div class="param_value">
                    <input type="text" name="token" id="token" value="" runat="server" />
                </div>
                <div class="param_name">
                    Payer ID *</div>
                <div class="param_value">
                    <input type="text" name="payerId" id="payerId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Payment Action</div>
                <div class="param_value">
                    <asp:DropDownList runat="server" ID="paymentAction">
                        <asp:ListItem Text="Sale" Value="SALE" />
                        <asp:ListItem Text="Authorization" Value="AUTHORIZATION" />
                        <asp:ListItem Text="Order" Value="ORDER" />
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
