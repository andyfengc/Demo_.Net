<%@ Page Language="C#" AutoEventWireup="true" Codebehind="GetTransactionDetails.aspx.cs"
    Inherits="PayPalAPISample.APICalls.GetTransactionDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - GetTransactionDetails</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div class="header">
            <h3>
                GetTransactionDetails</h3>
            <div id="apidetails">
                Returns information about a specific transaction.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Transaction Id *</div>
                <div class="param_value">
                    <input type="text" name="transactionId" id="transactionId" value="0A3763720H988743L"
                        runat="server" />
                </div>
                <div class="submit">
                    <input id="Submit" type="submit" name="search" value="Search" runat="server" onserverclick="Search_Submit" />
                    <br />
                    <br />
                    <a href="../Default.aspx">Home</a>&nbsp;&nbsp;<a href="javascript:history.back();">Back</a>
                </div>
            </div>
        </form>
        <div id="relatedcalls">
            <ul>
                <li><a href="TransactionSearch.aspx">TransactionSearch</a></li>
            </ul>
        </div>
    </div>
</body>
</html>
