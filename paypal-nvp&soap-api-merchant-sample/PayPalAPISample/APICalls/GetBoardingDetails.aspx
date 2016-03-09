<%@ Page Language="C#" AutoEventWireup="true" Codebehind="GetBoardingDetails.aspx.cs"
    Inherits="PayPalAPISample.APICalls.GetBoardingDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - GetBoardingDetails</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                GetBoardingDetails</h3>
            <div id="apidetails">
                Retrieve boarding details.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Token* (as returned by the EnterBoarding API)</div>
                <div class="param_value">
                    <input type="text" name="token" id="token" runat="server" value="" />
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
            See also
            <ul>
                <li><a href="EnterBoarding.aspx">EnterBoarding</a></li>
            </ul>
        </div>
    </div>
</body>
</html>
