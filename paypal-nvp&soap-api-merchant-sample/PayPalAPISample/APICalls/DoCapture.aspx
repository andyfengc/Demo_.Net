<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoCapture.aspx.cs" Inherits="PayPalAPISample.APICalls.DoCapture" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoCapture</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                DoCapture</h3>
            <div id="apidetails">
                Request to completely or partially settle an order, an authorization, or reauthorization.
                This API an be used with both basic and order authorizations.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Authorization ID *</div>
                <div class="param_value">
                    <input type="text" name="authorizationId" id="authorizationId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Amount to capture *</div>
                <div class="param_value">
                    <input type="text" name="amount" id="amount" value="" runat="server" />
                    <asp:DropDownList ID="currencyCode" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="USD" Value="USD" Selected="true" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Is this the last capture? *</div>
                <div class="param_value">
                    <asp:DropDownList ID="completeType" runat="server">
                        <asp:ListItem Text="Yes" Value="COMPLETE" />
                        <asp:ListItem Text="No" Value="NOTCOMPLETE" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    A note describing this settlement</div>
                <div class="param_value">
                    <input type="text" name="note" id="note" value="" runat="server" />
                </div>
                <div class="param_name">
                    Invoice Id for this capture</div>
                <div class="param_value">
                    <input type="text" name="invoiceId" id="invoiceId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Soft descriptor (passed to buyer's credit card statement)</div>
                <div class="param_value">
                    <input type="text" name="softDescriptor" id="softDescriptor" value="" runat="server" />
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
