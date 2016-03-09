<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoNonReferencedCredit.aspx.cs"
    Inherits="PayPalAPISample.APICalls.DoNonReferencedCredit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoNonReferencedCredit</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />

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
                DoNonReferencedCredit</h3>
            <div id="apidetails">
                Non-referenced credits allow the merchant to issue credits to Credit cards of buyers
                even if there is no prior record of a transaction with the buyers. This API is useful
                to merchants who have recently migrated from other acquirers and need to refund
                transactions processed through other acquirers.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div id="section_header">
                    Credit Card Details</div>
                <table class="params">
                    <tr>
                        <th class="param_name">
                            Credit Card number</th>
                        <th class="param_name">
                            Expiry date</th>
                        <th class="param_name">
                            Credit Card type</th>
                        <th class="param_name">
                            CVV</th>
                    </tr>
                    <tr>
                        <td>
                            <div class="param_value">
                                <input type="text" name="creditCardNumber" id="creditCardNumber" value="" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <div id="dateField" style="display: none;">
                                    <asp:Calendar ID="calDate" OnSelectionChanged="calDate_SelectionChanged" runat="server" />
                                </div>
                                <asp:TextBox ID="cardExpiryDate" runat="server" />
                                <img src="calendar_icon.png" alt="calendar" onclick="popupCalendar()" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <asp:DropDownList ID="creditCardType" runat="server">
                                    <asp:ListItem Value="VISA" Text="Visa" />
                                    <asp:ListItem Value="MASTERCARD" Text="MasterCard" />
                                    <asp:ListItem Value="DISCOVER" Text="Discover" />
                                    <asp:ListItem Value="AMEX" Text="Amex" />
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <input type="text" name="cvv" id="cvv" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="params">
                    <div class="param_name">
                        Comment</div>
                    <div class="param_value">
                        <input type="text" name="comment" id="comment" runat="server" value="NonReferencedCredit" />
                    </div>
                </div>
                <table class="params">
                    <tr>
                        <th class="param_name">
                            Item Cost</th>
                        <th class="param_name">
                            Shipping cost</th>
                        <th class="param_name">
                            Tax Amount</th>
                        <th class="param_name">
                            Currency Code</th>
                    </tr>
                    <tr>
                        <td class="param_value">
                            <input type="text" id="itemAmount" name="itemAmount" value="0.50" runat="server" /></td>
                        <td class="param_value">
                            <input type="text" name="shippingAmount" id="shippingAMount" value="0.50" runat="server" /></td>
                        <td class="param_value">
                            <input type="text" id="taxAmount" name="taxAmount" value="0.0" runat="server" /></td>
                        <td class="param_value">
                            <input type="text" id="currencyId" name="currencyID" value="USD" runat="server" /></td>
                    </tr>
                </table>
                <div class="submit">
                    <input id="submitBtn" type="submit" name="submitBtn" value="Submit" runat="server" onserverclick="Submit_Click" />                    <br />
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
