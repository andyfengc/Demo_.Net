<%@ Page Language="C#" AutoEventWireup="true" Codebehind="GetBillingAgreementCustomerDetails.aspx.cs"
    Inherits="PayPalAPISample.APICalls.GetBillingAgreementCustomerDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - GetCustomerBillingAgreement</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                GetCustomerBillingAgreement</h3>
            <div id="apidetails">
                Obtains information about a billing agreement’s PayPal account holder.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Token from SetCustomerBillingAgreement *</div>
                <div class="param_value">
                    <input type="text" name="token" id="token" value="" runat="server" />
                </div>
                <div class="submit">
                    <input id="submitBtn" type="submit" name="submitBtn" value="Submit" runat="server" onserverclick="Submit_Click" />
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
