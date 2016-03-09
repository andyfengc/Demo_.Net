<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RecurringPaymentsUsingCreditCard.aspx.cs"
    Inherits="PayPalAPISample.UseCaseSamples.RecurringPaymentsUsingCreditCard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK - RecurringPaymentsProfileUsingCreditCard</title>
    <link href="../APICalls/sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>Create Recurring Payments Profile Using Credit Card</h3>
            <div id="apidetails">
                <ul><li><i><b>CreateRecurringPaymentsProfile</b> API operation creates Recurring Payments Profile</i></li><li><i>Directly use <b>Credit Card</b> for creating Profile</i></li></ul>
            </div>
        </div>
        <form id="form1" runat="server" method="post">
        <div id="request_form">
            <div class="params">
                <div class="param_name">
                    <asp:Label runat="server" ID="LabelCreditCard" Text="Credit Card *" Font-Underline="true"
                        Font-Bold="true"></asp:Label>
                </div>
            </div>
            <br />
            <table class="params">
                <tr>
                    <th class="param_name">
                        Credit Card Number
                    </th>
                    <th class="param_name">
                        Expiry
                    </th>
                    <th class="param_name">
                        Credit Card
                    </th>
                    <th class="param_name">
                        CVV
                    </th>
                </tr>
                <tr>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="creditCardNumber" Text="4917760970795152" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:DropDownList runat="server" ID="expMonth">
                                <asp:ListItem Value="01" Text="January" />
                                <asp:ListItem Value="02" Text="February" />
                                <asp:ListItem Value="03" Text="March" />
                                <asp:ListItem Value="04" Text="April" />
                                <asp:ListItem Value="05" Text="May" />
                                <asp:ListItem Value="06" Text="June" />
                                <asp:ListItem Value="07" Text="July" />
                                <asp:ListItem Value="08" Text="August" />
                                <asp:ListItem Value="09" Text="September" />
                                <asp:ListItem Value="10" Text="October" />
                                <asp:ListItem Value="11" Text="November" />
                                <asp:ListItem Value="12" Text="December" Selected="True" />
                            </asp:DropDownList>
                            <asp:DropDownList runat="server" ID="expYear">
                                <asp:ListItem Value="2013" Text="2013" />
                                <asp:ListItem Value="2014" Text="2014" Selected="True" />
                                <asp:ListItem Value="2015" Text="2015" />
                                <asp:ListItem Value="2016" Text="2016" />
                                <asp:ListItem Value="2017" Text="2017" />
                                <asp:ListItem Value="2018" Text="2018" />
                                <asp:ListItem Value="2019" Text="2019" />
                                <asp:ListItem Value="2020" Text="2020" />
                                <asp:ListItem Value="2021" Text="2021" />
                                <asp:ListItem Value="2022" Text="2022" />
                                <asp:ListItem Value="2023" Text="2023" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:DropDownList runat="server" ID="creditCardType">
                                <asp:ListItem Value="VISA" Text="Visa" />
                                <asp:ListItem Value="MASTERCARD" Text="Mastercard" />
                                <asp:ListItem Value="DISCOVER" Text="Discover" />
                                <asp:ListItem Value="AMEX" Text="Amex" />
                                <asp:ListItem Value="SWITCH" Text="Switch" />
                                <asp:ListItem Value="SOLO" Text="Solo" />
                                <asp:ListItem Value="MAESTRO" Text="Maestro" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="cvv" Text="962" />
                        </div>
                    </td>
                </tr>
            </table>
            <br />
            <asp:Label runat="server" ID="LabelRecurringPaymentsProfileDetails" Text="Recurring Payments Profile Details"
                Font-Underline="true" Font-Bold="true" />
            <br />
            <br />
            <div class="param_name">
                Billing Start Date</div>
            <div id="DivCalendar" style="display: none;">
                <asp:Calendar runat="server" ID="CalendarDate" OnSelectionChanged="CalendarDate_SelectionChanged" />
            </div>
            <div class="param_value">
                <asp:TextBox ID="billingStartDate" runat="server" />
                <img src="../APICalls/calendar_icon.png" alt="calendar" onclick="popupCalendar()" />
            </div>
            <br />
            <div class="section_header">
                <asp:Label runat="server" ID="LabelScheduleDetails" Text="Schedule Details" Font-Underline="true"
                    Font-Bold="true" />
            </div>
            <br />
            <div class="params">
                <div class="param_name">
                    Description*</div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="profileDescription" Text="Description" />
                </div>
            </div>
            <br />
            <asp:Label runat="server" ID="LabelActivationDetails" Text="Activation Details" Font-Underline="true" Font-Bold="true" />
            <br />
            <br />
            <table class="params">
                <tr>
                    <th>
                        Failed Payment Action
                    </th>
                </tr>
                <tr>   
                    <td>
                        <div class="param_value">
                            <asp:DropDownList runat="server" ID="failedInitialAmountAction">
                                <asp:ListItem Text="Continue On Failure" Value="CONTINUEONFAILURE" />
                                <asp:ListItem Text="Cancel On Failure" Value="CANCELONFAILURE" />
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="section_header">
                <b>Trial Period</b>
            </div>
            <table class="params">
                <tr>
                    <th>
                        Billing Frequency
                    </th>
                    <th>
                        Billing Period
                    </th>
                    <th>
                        Total Billing Cycles
                    </th>
                    <th>
                        Amount Per Billing Cycle
                    </th>
                </tr>
                <tr>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="trialBillingFrequency" Text="1" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                             <asp:DropDownList runat="server" ID="trialBillingPeriod">
                                <asp:ListItem Text="NoBillingPeriodType" Value="NOBILLINGPERIODTYPE" />
                                <asp:ListItem Text="Day" Value="DAY" Selected="True" />
                                <asp:ListItem Text="WEEK" Value="WEEK" />
                                <asp:ListItem Text="SEMIMONTH" Value="SEMIMONTH" />
                                <asp:ListItem Text="MONTH" Value="MONTH" />
                                <asp:ListItem Text="YEAR" Value="YEAR" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="trialBillingCycles" Text="2" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="trialBillingAmount" Text="2.0" />
                        </div>
                    </td>
                </tr>
            </table>
            <div class="section_header">
                <b>Payment Period*</b>
            </div>
            <table class="params_name">
                <tr>
                    <th>
                        Billing Frequency
                    </th>
                    <th>
                        Billing Period
                    </th>
                    <th>
                        Total Billing Cycles
                    </th>
                    <th>
                        Amount Per Billing Cycle
                    </th>     
                </tr>
                <tr>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="billingFrequency" Text="1" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                             <asp:DropDownList runat="server" ID="billingPeriod">
                                    <asp:ListItem Text="NoBillingPeriodType" Value="NOBILLINGPERIODTYPE" />
                                    <asp:ListItem Text="Day" Value="DAY" Selected="True" />
                                    <asp:ListItem Text="WEEK" Value="WEEK" />
                                    <asp:ListItem Text="SEMIMONTH" Value="SEMIMONTH" />
                                    <asp:ListItem Text="MONTH" Value="MONTH" />
                                    <asp:ListItem Text="YEAR" Value="YEAR" />
                            </asp:DropDownList>
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="totalBillingCycles" Text="8" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                             <asp:TextBox runat="server" ID="billingAmount" Text="5.0" />
                        </div>
                    </td>
                </tr>
            </table>
            <div class="params">
                <div class="param_name">
                    Maximum Failed Payments Before Profile Suspension
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="maxFailedPayments" Text="3" />
                </div>
            </div>
            <div class="param_name">
                Auto Billing Of Outstanding Amount
            </div>
            <div class="param_value">
                <asp:DropDownList ID="autoBillOutstandingAmount" runat="server">
                    <asp:ListItem Text="No Auto Billing" Value="NOAUTOBILL"></asp:ListItem>
                    <asp:ListItem Text="Add To Next billing" Value="ADDTONEXTBILLING"></asp:ListItem>
                </asp:DropDownList>
            </div>
            <br />
            <div class="submit">
                <asp:Button ID="ButtonPayments" Text="CreateRecurringPaymentsProfile" runat="server"
                    PostBackUrl="~/UseCaseSamples/RequestResponse.aspx" />
            </div>
            <br />
            <asp:HyperLink runat="server" ID="HyperLinkHome" NavigateUrl="~/Default.aspx" Text="Home"/>
            <br />
            <br />
            <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="javascript:history.back();"
                Text="Back" />
        </div>
        </form>
        <script type="text/javascript">
            function popupCalendar() {
                var dateField = document.getElementById('DivCalendar');
                if (dateField.style.display == 'none')
                    dateField.style.display = 'block';
                else
                    dateField.style.display = 'none';
            }
        </script>
    </div>
</body>
</html>
