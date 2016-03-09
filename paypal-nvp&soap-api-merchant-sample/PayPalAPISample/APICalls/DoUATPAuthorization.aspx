<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoUATPAuthorization.aspx.cs"
    Inherits="PayPalAPISample.APICalls.DoUATPAuthorization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoUATPAuthorization</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                DoUATPAuthorization</h3>
            <div id="apidetails">
                Used to authorize a payment for airline merchants.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="params">
                    <div class="param_name">
                        UATP Number *
                    </div>
                    <div class="param_value">
                        <input type="text" name="uatpNumber" id="uatpNumber" value="" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Expiry month *</div>
                    <div class="param_value">
                        <input type="text" name="expMonth" id="expMonth" value="10" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Expiry year *</div>
                    <div class="param_value">
                        <input type="text" name="expYear" id="expYear" value="2015" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Amount *</div>
                    <div class="param_value">
                        <input type="text" name="amount" id="amount" value="10.0" runat="server" />
                        <input type="text" name="currencyID" id="currencyID" value="USD" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Transaction Entity type</div>
                    <div class="param_value">
                        <asp:DropDownList ID="transactionEntity" runat="server">
                            <asp:ListItem Text="Order" Value="ORDER" />
                        </asp:DropDownList>
                    </div>
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
