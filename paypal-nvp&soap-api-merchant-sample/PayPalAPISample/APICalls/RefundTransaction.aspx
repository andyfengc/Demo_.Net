<%@ Page Language="C#" AutoEventWireup="true" Codebehind="RefundTransaction.aspx.cs"
    Inherits="PayPalAPISample.APICalls.RefundTransaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - Refund Transaction</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />

    <script type="text/javascript">
       
        function popupCalendar() {
            var dateField = document.getElementById('dateField');
            if (dateField.style.display == 'none')
                dateField.style.display = 'block';
            else
                dateField.style.display = 'none';
        }
    </script>

</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                Refund Transaction</h3>
            <div id="apidetails">
                Issues a refund to the PayPal account holder associated with a transaction.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Transaction Id *</div>
                <div class="param_value">
                    <input type="text" name="transactionId" id="transactionId" value="" runat="server" />
                    <a href="SetExpressCheckout.aspx?paymentAction=SALE">Create a test payment for refund</a>
                </div>
                <div class="param_name">
                    Refund type</div>
                <div class="param_value">
                    <asp:DropDownList ID="refundType" runat="server">
                        <asp:ListItem Text="Select a value" Value="" />
                        <asp:ListItem Text="Full (Default)" Value="FULL" />
                        <asp:ListItem Text="Partial" Value="PARTIAL" />
                        <asp:ListItem Text="External Dispute" Value="EXTERNALDISPUTE" />
                        <asp:ListItem Text="Other" Value="OTHER" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Refund Amount (only if Partial refund is chosen)</div>
                <div class="param_value">
                    <input type="text" name="refundAmount" id="refundAmount" value="" runat="server" />
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
                    Memo</div>
                <div class="param_value">
                    <input type="text" name="memo" id="memo" value="" runat="server" />
                </div>
                <div class="param_name">
                    Maximum time until which refund must be retried</div>
                <div class="param_value">
                    <div id="dateField" style="display: none;">
                        <asp:Calendar ID="calDate" OnSelectionChanged="calDate_SelectionChanged" runat="server" />
                    </div>
                    <asp:TextBox ID="retryUntil" runat="server" />
                    <img src="calendar_icon.png" alt="calendar" onclick="popupCalendar()" />
                </div>
                <div class="param_name">
                    Refund Source</div>
                <div class="param_value">
                    <asp:DropDownList ID="refundSource" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Text="Any" Value="ANY" />
                        <asp:ListItem Text="Default" Value="DEFAULT" />
                        <asp:ListItem Text="Instant" Value="INSTANT" />
                        <asp:ListItem Text="eCheck" Value="ECHECK" />
                    </asp:DropDownList>
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
