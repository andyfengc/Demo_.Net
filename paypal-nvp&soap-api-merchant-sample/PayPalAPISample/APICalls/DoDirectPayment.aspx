<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoDirectPayment.aspx.cs"
    Inherits="PayPalAPISample.APICalls.DoDirectPayment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoDirectPayment</title>
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
                DoDirectPayment</h3>
            <div id="apidetails">
                Process a credit card payment.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="params">
                    <div class="param_name">
                        First name</div>
                    <div class="param_value">
                        <input type="text" id="firstName" name="firstName" value="John" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Last name</div>
                    <div class="param_value">
                        <input type="text" id="lastName" name="lastName" value="Doe" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Card type</div>
                    <div class="param_value">
                        <asp:DropDownList ID="creditCardType" runat="server">
                            <asp:ListItem Value="VISA" Text="Visa" />
                            <asp:ListItem Value="MASTERCARD" Text="MasterCard" />
                            <asp:ListItem Value="DISCOVER" Text="Discover" />
                            <asp:ListItem Value="AMEX" Text="Amex" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Card number</div>
                    <div class="param_value">
                        <input type="text" id="creditCardNumber" name="creditCardNumber" runat="server" value="4825854086744369" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Card expiration date</div>
                    <div class="param_value">
                        <div id="dateField" style="display: none;">
                            <asp:Calendar ID="calDate" OnSelectionChanged="calDate_SelectionChanged" runat="server" />
                        </div>
                        <asp:TextBox ID="cardExpiryDate" runat="server" />
                        <img src="calendar_icon.png" alt="calendar" onclick="popupCalendar()" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Card Verification Number</div>
                    <div class="param_value">
                        <input type="text" id="cvv2Number" name="cvv2Number" value="962" runat="server" />
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Payment type</div>
                    <div class="param_value">
                        <asp:DropDownList ID="paymentType" runat="server">
                            <asp:ListItem Text="Sale" Value="SALE" />
                            <asp:ListItem Text="Authorization" Value="AUTHORIZATION" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="params">
                    <div class="param_name">
                        Amount</div>
                    <div class="param_value">
                        <input type="text" id="amount" name="amount" value="1.00" runat="server" />
                        <asp:DropDownList ID="currencyCode" runat="server">
                            <asp:ListItem Text="-Select a value-" Value="" />
                            <asp:ListItem Text="AUD" Value="AUD" />
                            <asp:ListItem Text="EUR" Value="EUR" />
                            <asp:ListItem Text="GBP" Value="GBP" />
                            <asp:ListItem Text="JPY" Value="JPY" />
                            <asp:ListItem Text="USD" Value="USD" Selected="True" />
                            <asp:ListItem Text="SGD" Value="SGD" />
                            <asp:ListItem Text="HKD" Value="HKD" />
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="section_header">
                    Billing Address</div>
                <table class="line_item">
                    <tr>
                        <th>
                            Name</th>
                        <th>
                            Street 1</th>
                        <th>
                            Street 2</th>
                        <th>
                            City</th>
                        <th>
                            State</th>
                        <th>
                            Postal Code</th>
                        <th>
                            Country</th>
                        <th>
                            Phone</th>
                    </tr>
                    <tr>
                        <td>
                            <span class="param_value">
                                <input type="text" id="payerName" name="payerName" runat="server" value="Test payer name" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="street1" name="street1" value="1 Main St" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="street2" name="street2" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="city" name="city" value="San Jose" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="state" name="state" value="CA" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="postalCode" name="postalCode" value="95131" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="country" name="country" value="US" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="phone" name="phone" runat="server" />
                            </span>
                        </td>
                    </tr>
                </table>
                <br />
                <div class="params">
                    <div class="param_name">
                        IPN Notification Url</div>
                    <div class="param_value">
                        <input id="ipnNotificationUrl" name="ipnNotificationUrl" value="" runat="server" />
                    </div>
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
