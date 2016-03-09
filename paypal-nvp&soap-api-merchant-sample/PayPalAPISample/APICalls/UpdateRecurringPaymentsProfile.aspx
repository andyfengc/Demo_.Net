<%@ Page Language="C#" AutoEventWireup="true" Codebehind="UpdateRecurringPaymentsProfile.aspx.cs"
    Inherits="PayPalAPISample.APICalls.UpdateRecurringPaymentsProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - UpdateRecurringPaymentProfile</title>
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
                UpdateRecurringPaymentProfile</h3>
            <div id="apidetails">
                Update a recurring payments profile.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Recurring Payments Profile ID *</div>
                <div class="param_value">
                    <input type="text" name="profileId" id="profileId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Note</div>
                <div class="param_value">
                    <input type="text" name="note" id="note" value="" runat="server" />
                </div>
                <div class="section_header">
                    Recurring payments profile details</div>
                <div class="param_name">
                    Subscriber Name</div>
                <div class="param_value">
                    <input type="text" name="subscriberName" id="subscriberName" value="" runat="server" />
                </div>
                <div class="param_name">
                    Subscriber shipping address (if different from buyer's PayPal account address)</div>
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
                                <input type="text" id="shippingName" name="shippingName" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingStreet1" name="shippingStreet1" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingStreet2" name="shippingStreet2" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingCity" name="shippingCity" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingState" name="shippingState" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingPostalCode" name="shippingPostalCode" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingCountry" name="shippingCountry" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingPhone" name="shippingPhone" runat="server" />
                            </span>
                        </td>
                    </tr>
                </table>
                <div class="param_name">
                    Additional billing cycles</div>
                <div class="param_value">
                    <input type="text" name="additionalBillingCycles" id="additionalBillingCycles" value=""
                        runat="server" />
                </div>
                <div class="param_name">
                    Currency</div>
                <div class="param_value">
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
                    Amount to bill (If no value is specified, PayPal attempts to bill the entire outstanding
                    balance amount)</div>
                <div class="param_value">
                    <input type="text" name="amount" id="amount" value="" runat="server" />
                </div>
                <div class="param_name">
                    Outstanding Balance</div>
                <div class="param_value">
                    <input type="text" name="outstandingBalance" id="outstandingBalance" value="" runat="server" />
                </div>
                <div class="param_name">
                    Maximum failed payments before profile suspension</div>
                <div class="param_value">
                    <input type="text" name="maxFailedPayments" id="maxFailedPayments" value="3" runat="server" />
                </div>
                <div class="param_name">
                    Auto billing of outstanding amount</div>
                <div class="param_value">
                    <asp:DropDownList ID="autoBillOutstandingAmount" runat="server">
                        <asp:ListItem Text="No Auto billing" Value="NOAUTOBILL"></asp:ListItem>
                        <asp:ListItem Text="Add to next billing" Value="ADDTONEXTBILLING"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Billing start date</div>
                <div class="param_value">
                    <div id="dateField" style="display: none;">
                        <asp:Calendar ID="calDate" OnSelectionChanged="calDate_SelectionChanged" runat="server" />
                    </div>
                    <asp:TextBox ID="billingStartDate" runat="server" />
                    <img src="calendar_icon.png" alt="calendar" onclick="popupCalendar()" />
                </div>
                <div class="param_name">
                    Credit Card
                </div>
                <table class="line_item">
                    <tr>
                        <th>
                            Credit Card number</th>
                        <th>
                            Expiry date</th>
                        <th>
                            Name on card</th>
                        <th>
                            Credit Card type</th>
                        <th>
                            CVV</th>
                    </tr>
                    <tr>
                        <td>
                            <div class="param_value">
                                <input type="text" name="creditCardNumber" id="creditCardNumber" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <asp:DropDownList ID="expMonth" runat="server">
                                    <asp:ListItem Text="Jan" Value="1" />
                                    <asp:ListItem Text="Feb" Value="2" />
                                    <asp:ListItem Text="Mar" Value="3" />
                                    <asp:ListItem Text="Apr" Value="4" />
                                    <asp:ListItem Text="May" Value="5" />
                                    <asp:ListItem Text="Jun" Value="6" />
                                    <asp:ListItem Text="Jul" Value="7" />
                                    <asp:ListItem Text="Aug" Value="8" />
                                    <asp:ListItem Text="Sep" Value="9" />
                                    <asp:ListItem Text="Oct" Value="10" />
                                    <asp:ListItem Text="Nov" Value="11" />
                                    <asp:ListItem Text="Dec" Value="12" />
                                </asp:DropDownList>
                                <asp:DropDownList ID="expYear" runat="server">
                                    <asp:ListItem Text="2011" Value="2011" />
                                    <asp:ListItem Text="2012" Value="2012" />
                                    <asp:ListItem Text="2013" Value="2013" />
                                    <asp:ListItem Text="2014" Value="2014" />
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <input type="text" name="cardOnName" id="cardOnName" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <asp:DropDownList ID="creditCardType" runat="server">
                                    <asp:ListItem Text="Visa" Value="Visa" />
                                    <asp:ListItem Text="MasterCard" Value="MasterCard" />
                                    <asp:ListItem Text="Discover" Value="Discover" />
                                    <asp:ListItem Text="Amex" Value="Amex" />
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
                <div class="param_name">
                    Trial Period</div>
                <table class="line_tem">
                    <tr>
                        <th>
                            Billing frequency</th>
                        <th>
                            Billing period</th>
                        <th>
                            Total billing cycles</th>
                        <th>
                            Per billing cycle amount</th>
                        <th>
                            Shipping amount</th>
                        <th>
                            Tax</th>
                    </tr>
                    <tr>
                        <td>
                            <span class="param_value">
                                <input type="text" id="trialBillingFrequency" name="trialBillingFrequency" value="10"
                                    runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <asp:DropDownList ID="trialBillingPeriod" runat="server">
                                    <asp:ListItem Text="Day" Value="DAY" />
                                    <asp:ListItem Text="Week" Value="WEEK" />
                                    <asp:ListItem Text="SemiMonth" Value="SEMIMONTH" />
                                    <asp:ListItem Text="Month" Value="MONTH" />
                                    <asp:ListItem Text="Year" Value="YEAR" />
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="trialBillingCycles" name="trialBillingCycles" value="2" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="trialBillingAmount" name="trialBillingAmount" value="2.0"
                                    runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="trialShippingAmount" name="trialShippingAmount" value="0.0"
                                    runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="trialTaxAmount" name="trialTaxAmount" value="0.0" runat="server" />
                            </span>
                        </td>
                    </tr>
                </table>
                <div class="param_name">
                    Payment Period</div>
                <table class="line_tem">
                    <tr>
                        <th>
                            Billing frequency</th>
                        <th>
                            Billing period</th>
                        <th>
                            Total billing cycles</th>
                        <th>
                            Per billing cycle amount</th>
                        <th>
                            Shipping amount</th>
                        <th>
                            Tax</th>
                    </tr>
                    <tr>
                        <td>
                            <span class="param_value">
                                <input type="text" id="billingFrequency" name="billingFrequency" value="10" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <asp:DropDownList ID="billingPeriod" runat="server">
                                    <asp:ListItem Text="Day" Value="DAY" />
                                    <asp:ListItem Text="Week" Value="WEEK" />
                                    <asp:ListItem Text="SemiMonth" Value="SEMIMONTH" />
                                    <asp:ListItem Text="Month" Value="MONTH" />
                                    <asp:ListItem Text="Year" Value="YEAR" />
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="totalBillingCycles" name="totalBillingCycles" value="8" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="billingAmount" name="billingAmount" value="5.0" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="shippingAmount" name="shippingAmount" value="1.0" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" id="taxAmount" name="taxAmount" value="0.0" runat="server" />
                            </span>
                        </td>
                    </tr>
                </table>
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
