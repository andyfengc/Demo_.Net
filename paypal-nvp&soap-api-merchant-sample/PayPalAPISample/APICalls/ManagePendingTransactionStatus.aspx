<%@ Page Language="C#" AutoEventWireup="true" Codebehind="ManagePendingTransactionStatus.aspx.cs"
    Inherits="PayPalAPISample.APICalls.ManagePendingTransactionStatus" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - ManagePendingTransactionStatus</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                ManagePendingTransactionStatus</h3>
            <div id="apidetails">
                Accept or deny a pending transaction held by the Fraud Management Filters. You can
                configure Fraud Management Filters from your account profile and can set up your
                SetExpressCheckout & DoDirectPayment API calls to return Fraud Management Filter
                results.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Transaction Id *</div>
                <div class="param_value">
                    <input type="text" name="transactionId" id="transactionId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Action to take</div>
                <div class="param_value">
                    <asp:DropDownList ID="action" runat="server">
                        <asp:ListItem Text="Accept" Value="ACCEPT" />
                        <asp:ListItem Text="Deny" Value="DENY" />
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
