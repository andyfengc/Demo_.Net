<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetExpressCheckoutForRecurringPayments.aspx.cs"
    Inherits="PayPalAPISample.UseCaseSamples.SetExpressCheckoutForRecurringPayments" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK - SetExpressCheckoutForRecurringPayments</title>
    <link href="../APICalls/sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>SetExpressCheckoutForRecurringPayments</h3>
            <div id="apidetails">
                <ul><li><i>Set the details of <b>ExpressCheckout</b> Billing Agreements for Recurring Payments</i></li></ul>
            </div>
        </div>
        <form id="form1" runat="server" method="post">
        <div id="request_form">
            <div class="params">
                <div class="param_name">
                    <asp:Label runat="server" ID="LabelBuyerMail" Text="Buyer Email" Font-Bold="true" />
                    <asp:TextBox runat="server" ID="buyerMail" Text="platfo_1255077030_biz@gmail.com" Width="20%" />
                </div>
            </div>
            <br />
            <div class="section_header">
                <asp:Label runat="server" ID="LabelPaymentDetails" Text="Payment Details" Font-Underline="true"
                    Font-Bold="true" />
            </div>
            <br />
            <div class="params">
                <div class="param_name">
                    Total Shipping Cost
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="shippingTotal" Text="0.50" />
                </div>
            </div>
            <div class="params">
                <div class="param_name">
                    Total Insurance Cost
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="insuranceTotal" />
                </div>
            </div>
            <div class="params">
                <div class="param_name">
                    Total Handling Cost
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="handlingTotal" />
                </div>
            </div>
            <div class="params">
                <div class="param_name">
                    Total Tax
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="taxTotal" />
                </div>
            </div>
            <div class="params">
                <div class="param_name">
                    Order Description
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="orderDescription" Text="Description" />
                </div>
            </div>
            <div class="params">
                <div class="param_name">
                    Currency Code
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="currencyCode" Text="USD" />
                </div>
            </div>
            <div class="params">
                <div class="param_name">
                    Payment Type
                </div>
                <div class="param_value">
                    <asp:DropDownList runat="server" ID="paymentType">
                        <asp:ListItem Text="Sale" Value="SALE" />
                        <asp:ListItem Text="Authorization" Value="AUTHORIZATION" />
                        <asp:ListItem Text="Order" Value="ORDER" />
                    </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="section_header">
                <asp:Label runat="server" ID="LabelDetails" Text="Item Details" Font-Underline="true"
                    Font-Bold="true"></asp:Label>
            </div>
            <br />
            <table class="params">
                <tr>
                    <th class="param_name">
                        Name
                    </th>
                    <th class="param_name">
                        Cost
                    </th>
                    <th class="param_name">
                        Quantity
                    </th>
                    <th class="param_name">
                        Sales Tax
                    </th>
                    <th class="param_name">
                        Category
                    </th>
                </tr>
                <tr>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="itemName" Text="Name" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="itemAmount" Text="5.27" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="itemQuantity" Text="2" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="salesTax" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:DropDownList runat="server" ID="itemCategory">
                                <asp:ListItem Text="Physical" Value="PHYSICAL" />
                                <asp:ListItem Text="Digital" Value="DIGITAL" />
                            </asp:DropDownList>
                        </div>
                    </td>
                </tr>
            </table>
            <div class="params">
                <div class="param_name">
                    IPN Notification Url (Receive IPN callback from PayPal)
                </div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="notifyURL" />
                </div>
            </div>
            <br />
            <div class="section_header">
                <asp:Label runat="server" ID="LabelBillingAgreement" Text="Billing Agreement (Required for Recurring Payments)" Font-Underline="true" Font-Bold="true" />
            </div>
            <br />
            <div class="param_name">
                Billing Agreement Description*
            </div>
            <div class="param_value">
                <asp:TextBox runat="server" ID="billingAgreementText" Text="Description" />
            </div>
            <div class="param_name">
                Billing Type* (Select Recurring Payments UseCase)
            </div>
            <div class="param_value">
                <asp:DropDownList runat="server" ID="billingType">
                    <asp:ListItem Text="None" Value="NONE" Enabled="false" />
                    <asp:ListItem Text="Merchant Initiated Billing" Value="MERCHANTINITIATEDBILLING"
                        Enabled="false" />
                    <asp:ListItem Text="Recurring Payments" Value="RECURRINGPAYMENTS"
                        Selected="True" />
                    <asp:ListItem Text="Merchant Initiated Billing Single Agreement" Value="MERCHANTINITIATEDBILLINGSINGLEAGREEMENT"
                        Enabled="false" />
                    <asp:ListItem Text="Channel Initiated Billing" Value="CHANNELINITIATEDBILLING" Enabled="false" />
                </asp:DropDownList>
            </div>
            <br />
            <div class="submit">
                <asp:Button ID="ButtonPayments" Text="SetExpressCheckout" runat="server" PostBackUrl="~/UseCaseSamples/RequestResponse.aspx" />
            </div>
            <br />
            <asp:HyperLink runat="server" ID="HyperLinkHome" NavigateUrl="~/Default.aspx" Text="Home" />
            <br />
            <br />
            <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="javascript:history.back();" Text="Back" />
        </div>
        </form>
    </div>
</body>
</html>
