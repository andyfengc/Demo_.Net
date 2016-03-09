<%@ Page Language="C#" AutoEventWireup="true" Codebehind="GetBalance.aspx.cs" Inherits="PayPalAPISample.APICalls.GetBalance" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - GetBalance</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                GetBalance</h3>
            <div id="apidetails">
                Retrieve account balance for a PayPal account.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Return all currencies</div>
                <div class="param_value">
                    <asp:DropDownList ID="returnAllCurrencies" runat="server">
                        <asp:ListItem Text="No - Return balance for only the primary currency holding" Value="0" />
                        <asp:ListItem Text="Yes - Return balance holding for all currencies" Value="1"
                            Selected="true" />
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
