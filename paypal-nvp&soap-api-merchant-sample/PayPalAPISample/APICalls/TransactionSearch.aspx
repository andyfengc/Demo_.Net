<%@ Page Language="C#" AutoEventWireup="true" Codebehind="TransactionSearch.aspx.cs"
    Inherits="PayPalAPISample.APICalls.TransactionSearch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - TransactionSearch</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />

    <script type="text/javascript">       
        function popupCalendar(id) {
            var dateField = document.getElementById(id);
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
        <div class="header">
            <h3>
                TransactionSearch</h3>
            <div id="apidetails">
                Searches transaction history for transactions that meet the specified criteria.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Transaction Start date</div>
                <div class="param_value">
                    <div id="startDateField" style="display: none;">
                        <asp:Calendar ID="calStartDate" OnSelectionChanged="calDate_SelectionChanged" runat="server" />
                    </div>
                    <asp:TextBox ID="startDate" runat="server" />
                    <img src="calendar_icon.png" alt="calendar" onclick="popupCalendar('startDateField')" />
                </div>
                <div class="param_name">
                    Transaction End Date</div>
                <div class="param_value">
                    <div id="endDateField" style="display: none;">
                        <asp:Calendar ID="calEndDate" OnSelectionChanged="calDate_SelectionChanged" runat="server" />
                    </div>
                    <asp:TextBox ID="endDate" runat="server" />
                    <img src="calendar_icon.png" alt="calendar" onclick="popupCalendar('endDateField')" />
                </div>
                <div class="param_name">
                    Payer EmailId</div>
                <div class="param_value">
                    <input type="text" name="payer" id="payer" runat="server" />
                </div>
                <div class="param_name">
                    Receiver EmailId</div>
                <div class="param_value">
                    <input type="text" name="receiver" id="receiver" runat="server" />
                </div>
                <div class="param_name">
                    Receipt ID</div>
                <div class="param_value">
                    <input type="text" name="receiptId" id="receiptId" runat="server" />
                </div>
                <div class="param_name">
                    Transaction Id</div>
                <div class="param_value">
                    <input type="text" name="transactionID" id="transactionID" runat="server" />
                </div>
                <div class="param_name">
                    Profile Id (Recurring Payments)
                </div>
                <div class="param_value">
                    <input type="text" name="profileId" id="profileId" runat="server" />
                </div>
                <div class="param_name">
                    Payer Name</div>
                <div class="param_value">
                    <input type="text" name="payerName" id="payerName" runat="server" />
                </div>
                <div class="param_name">
                    Item Number</div>
                <div class="param_value">
                    <input type="text" name="auctionItemNumber" id="auctionItemNumber" runat="server" />
                </div>
                <div class="param_name">
                    Invoice ID (Exact match)</div>
                <div class="param_value">
                    <input type="text" name="invoiceID" id="invoiceID" runat="server" />
                </div>
                <div class="param_name">
                    Card Number</div>
                <div class="param_value">
                    <input type="text" name="cardNumber" id="cardNumber" runat="server" />
                </div>
                <div class="param_name">
                    Transaction Class</div>
                <div class="param_value">
                    <asp:DropDownList ID="transactionClass" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Value="ALL" Text="All" />
                        <asp:ListItem Value="SENT" Text="Sent" />
                        <asp:ListItem Value="RECEIVED" Text="Received" />
                        <asp:ListItem Value="MASSPAY" Text="MassPay" />
                        <asp:ListItem Value="MONEYREQUEST" Text="MoneyRequest" />
                        <asp:ListItem Value="FUNDSADDED" Text="FundsAdded" />
                        <asp:ListItem Value="FUNDSWITHDRAWN" Text="FundsWithdrawn" />
                        <asp:ListItem Value="PAYPALDEBITCARD" Text="PayPalDebitCard" />
                        <asp:ListItem Value="REFERRAL" Text="Referral" />
                        <asp:ListItem Value="FEE" Text="Fee" />
                        <asp:ListItem Value="SUBSCRIPTION" Text="Subscription" />
                        <asp:ListItem Value="DIVIDEND" Text="Dividend" />
                        <asp:ListItem Value="BILLPAY" Text="Billpay" />
                        <asp:ListItem Value="REFUND" Text="Refund" />
                        <asp:ListItem Value="CURRENCYCONVERSIONS" Text="CurrencyConversions" />
                        <asp:ListItem Value="BALANCETRANSFER" Text="BalanceTransfer" />
                        <asp:ListItem Value="REVERSAL" Text="Reversal" />
                        <asp:ListItem Value="SHIPPING" Text="Shipping" />
                        <asp:ListItem Value="BALANCEAFFECTING" Text="BalanceAffecting" />
                        <asp:ListItem Value="ECHECK" Text="ECheck" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Transaction Amount</div>
                <div class="param_value">
                    <input type="text" name="amount" id="amount" runat="server" />
                </div>
                <div class="param_name">
                    Currency Code</div>
                <div class="param_value">
                    <asp:DropDownList ID="currencyCode" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="USD" Value="USD" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Transaction status</div>
                <div class="param_value">
                    <asp:DropDownList ID="transactionStatus" runat="server">
                        <asp:ListItem Value="" Text="-Select a value-" />
                        <asp:ListItem Value="PENDING" Text="Pending" />
                        <asp:ListItem Value="PROCESSING" Text="Processing" />
                        <asp:ListItem Value="SUCCESS" Text="Success" />
                        <asp:ListItem Value="DENIED" Text="Denied" />
                        <asp:ListItem Value="REVERSED" Text="Reversed" />
                    </asp:DropDownList>
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
                <li><a href="GetTransactionDetails.aspx">GetTransactionDetails</a></li>
            </ul>
        </div>
    </div>
</body>
</html>
