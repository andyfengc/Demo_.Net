<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoExpressCheckout.aspx.cs"
    Inherits="PayPalAPISample.UseCaseSamples.DoExpressCheckout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK - DoExpressCheckout</title>
    <link href="../APICalls/sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>DoExpressCheckout</h3>
            <div id="apidetails"><ul><li><i>Used to make ExpressCheckout payment. Please select the appropriate Payment Type to be set in SetExpressCheckout</i></li></ul></div>
        </div>
        <form id="form1" runat="server" method="post">
        <div id="request_form">
            <div class="params">
                <div class="param_name">Token*(Get Token via SetExpressCheckout)</div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="token" ReadOnly="true"/>
                </div>
            </div>
            <br />
            <div class="section_header">
                <asp:Label runat="server" ID="LabelPaymentDetails" Text="Payment Details" Font-Underline="true"
                    Font-Bold="true" />
            </div>
            <br />
            <div class="params">
                <div class="param_name">Payer ID*</div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="payerId" ReadOnly="true"/>
                </div>
            </div>
            <div class="params">
                <div class="param_name">Payment Type</div>
                <div class="param_value">             
                    <asp:DropDownList runat="server" ID="paymentType">
                        <asp:ListItem Text="Sale" Value="SALE" />
                        <asp:ListItem Text="Authorization" Value="AUTHORIZATION" />
                        <asp:ListItem Text="Order" Value="ORDER" />
                    </asp:DropDownList>
                </div>
            </div>
            <table class="params">
                <tr>
                    <th class="param_name">
                        Name
                    </th>
                    <th class="param_name">
                        Cost
                    </th>
                    <th class="param_name">
                        Currency Code
                    </th>
                    <th class="param_name">
                        Quantity
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
                            <asp:TextBox runat="server" ID="amt" Text="5.27" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="currencyCode" Text="USD" ReadOnly="true" />
                        </div>
                    </td>
                    <td>
                        <div class="param_value">
                            <asp:TextBox runat="server" ID="itemQuantity" Text="2" />
                        </div>
                    </td>
                </tr>
            </table>
            <div class="params">
                <div class="param_name">IPN Notification Url (Receive IPN call back from PayPal)</div>
                <div class="param_value">
                    <asp:TextBox runat="server" ID="notifyURL" />
                </div>
            </div>
        </div>
        <br />
        <div class="submit">
            <asp:Button ID="ButtonPayments" Text="DoExpressCheckout" runat="server" PostBackUrl="~/UseCaseSamples/RequestResponse.aspx" />
        </div>
        <br />
        <asp:HyperLink runat="server" ID="HyperLinkHome" NavigateUrl="~/Default.aspx" Text="Home" />
        <br />
        <br />
        <asp:HyperLink ID="HyperLinkBack" runat="server" NavigateUrl="javascript:history.back();" Text="Back" />
        </form>
    </div>
</body>
</html>
