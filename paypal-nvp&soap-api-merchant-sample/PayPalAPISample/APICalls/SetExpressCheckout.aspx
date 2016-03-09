<%@ Page Language="C#" AutoEventWireup="true" Codebehind="SetExpressCheckout.aspx.cs"
    Inherits="PayPalAPISample.APICalls.SetExpressCheckout" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - SetExpressCheckout</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                SetExpressCheckout</h3>
            <div id="apidetails">
                Initiates a Express Checkout transaction.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Return URL *</div>
                <div class="param_value">
                    <input type="text" name="returnUrl" id="returnUrl" value="" runat="server" />
                </div>
                <div class="param_name">
                    Cancel URL *</div>
                <div class="param_value">
                    <input type="text" name="cancelUrl" id="cancelUrl" value="" runat="server" />
                </div>
                <div class="param_name">
                    IPN Notification URL</div>
                <div class="param_value">
                    <input type="text" name="ipnNotificationUrl" id="ipnNotificationUrl" value="" runat="server" />
                </div>
                <div class="param_name">
                    Buyer's Email address</div>
                <div class="param_value">
                    <input type="text" name="buyerEmail" id="buyerEmail" value="platfo_1255077030_biz@gmail.com" runat="server" />
                </div>
                <div class="param_name">
                    Solution type</div>
                <div class="param_value">
                    <asp:DropDownList ID="solutionType" runat="server">
                        <asp:ListItem Value="" Text="-Select a value-" />
                        <asp:ListItem Value="MARK" Text="Mark" />
                        <asp:ListItem Value="SOLE" Text="Sole" />
                    </asp:DropDownList>
                </div>
                <div class="section_header">
                    Shipping details</div>
                <div class="param_name">
                    Requires shipping</div>
                <div class="param_value">
                    <asp:DropDownList ID="noShipping" runat="server">
                        <asp:ListItem Value="" Text="-Select a value-" />
                        <asp:ListItem Value="0" Text="Address optional - Display shipping address in PayPal pages" />
                        <asp:ListItem Value="1" Text="Address not required - Do not display shipping address in PayPal pages" />
                        <asp:ListItem Value="2" Text="Address required - If shipping address not passed, use value in buyer's profile" />
                    </asp:DropDownList>
                </div>
                <%--//Fix for release--%>
                <div class="param_name">
                    Require buyer's PayPal Shipping address to be a confirmed address</div>
                <div class="param_value">
                    <asp:DropDownList ID="reqConfirmShipping" runat="server">
                        <asp:ListItem Value="0" Text="No" />
                        <asp:ListItem Value="1" Text="Yes" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Allow Address override</div>
                <div class="param_value">
                    <asp:DropDownList ID="addressoverride" runat="server">
                        <asp:ListItem Value="" Text="-Select a value-" />
                        <asp:ListItem Value="0" Text="No - PayPal should not display shipping address" />
                        <asp:ListItem Value="1" Text="Yes - PayPal should display shipping address" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Shipping address (if different from buyer's PayPal account address)</div>
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
                <div class="section_header">
                    Payment details</div>
                <div class="param_name">
                    Payment currency code</div>
                <div class="param_value">
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
                <div class="param_name">
                    Total shipping costs</div>
                <div class="param_value">
                    <input type="text" name="shippingTotal" id="shippingTotal" value="0.50" runat="server" />
                </div>
                <div class="param_name">
                    Total insurance cost</div>
                <div class="param_value">
                    <input type="text" name="insuranceTotal" id="insuranceTotal" value="0.00" runat="server" />
                </div>
                <div class="param_name">
                    Total handling cost</div>
                <div class="param_value">
                    <input type="text" name="handlingTotal" id="handlingTotal" value="0.00" runat="server" />
                </div>
                <div class="param_name">
                    Total Tax</div>
                <div class="param_value">
                    <input type="text" name="taxTotal" id="taxTotal" value="0.0" runat="server" />
                </div>
                <div class="param_name">
                    Order description</div>
                <div class="param_value">
                    <textarea cols="40" rows="5" id="orderDescription" runat="server"></textarea>
                </div>
                <div class="param_name">
                    Payment Action</div>
                <div class="param_value">
                    <asp:DropDownList runat="server" ID="paymentAction">
                        <asp:ListItem Text="Sale" Value="SALE" />
                        <asp:ListItem Text="Authorization" Value="AUTHORIZATION" />
                        <asp:ListItem Text="Order" Value="ORDER" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Item Details</div>
                <table class="line_item">
                    <tr>
                        <th class="param_name">
                            Name</th>
                        <th class="param_name">
                            Cost</th>
                        <th class="param_name">
                            Quantity</th>
                        <th class="param_name">
                            Sales tax</th>
                        <th class="param_name">
                            Item Category</th>
                        <th class="param_name">
                            Description (optional)</th>
                    </tr>
                    <tr>
                        <td>
                            <div class="param_value">
                                <input type="text" name="itemName" id="itemName" value="Item Name" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <input type="text" name="itemAmount" id="itemAmount" value="5.27" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <input type="text" name="itemQuantity" id="itemQuantity" value="2" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <input type="text" name="salesTax" id="salesTax" value="" runat="server" />
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <asp:DropDownList ID="itemCategory" runat="server">
                                    <asp:ListItem Text="Physical" Value="PHYSICAL" />
                                    <asp:ListItem Text="Digital" Value="DIGITAL" />
                                </asp:DropDownList>
                            </div>
                        </td>
                        <td>
                            <div class="param_value">
                                <input type="text" name="itemDescription" id="itemDescription" value="" runat="server" />
                            </div>
                        </td>
                    </tr>
                </table>
                <div class="section_header">
                    Billing Agreement (Required for Recurring payments/Reference transactions only)</div>
                <div class="param_name">
                    Billing Agreement Description *</div>
                <div class="param_value">
                    <textarea cols="80" rows="10" id="billingAgreementText" runat="server">Test</textarea>
                </div>
                <div class="param_name">
                    Billing type *</div>
                <div class="param_value">
                    <asp:DropDownList ID="billingType" runat="server">
                        <asp:ListItem Value="NONE" Text="None" />
                        <asp:ListItem Value="RECURRINGPAYMENTS" Text="Recurring payments" />
                        <asp:ListItem Value="MERCHANTINITIATEDBILLING" Text="Reference transactions" />
                        <asp:ListItem Value="MERCHANTINITIATEDBILLINGSINGLEAGREEMENT" Text="Merchant Initiated Billing Single Agreement" />
                        <asp:ListItem Value="CHANNELINITIATEDBILLING" Text="Channel Initiated Billing" /> 
                    </asp:DropDownList>
                </div>

                <!-- PayPal page styling attributes -->
                <div class="section_header">
                    PayPal page styling attributes (optional)</div>
                <div class="param_name">
                    Locale Code for PayPal pages. Refer docs for complete list of supported locales
                    (Defaults to US)</div>
                <div class="param_value">
                    <asp:DropDownList ID="localeCode" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Text="CN" Value="CN" />
                        <asp:ListItem Text="DE" Value="DE" />
                        <asp:ListItem Text="ES" Value="ES" />
                        <asp:ListItem Text="FR" Value="FR" />
                        <asp:ListItem Text="US" Value="US" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Custom page style for payment pages (as configured in Merchant's account profile)</div>
                <div class="param_value">
                    <input type="text" name="pageStyle" id="pageStyle" value="" runat="server" />
                </div>
                <div class="param_name">
                    URL for header image</div>
                <div class="param_value">
                    <input type="text" name="cppheaderimage" id="cppheaderimage" value="" runat="server" />
                </div>
                <div class="param_name">
                    Border color around header</div>
                <div class="param_value">
                    <input type="text" name="cppheaderbordercolor" id="cppheaderbordercolor" value=""
                        runat="server" />
                </div>
                <div class="param_name">
                    Background color for header</div>
                <div class="param_value">
                    <input type="text" name="cppheaderbackcolor" id="cppheaderbackcolor" value="" runat="server" />
                </div>
                <div class="param_name">
                    Background color for payment page</div>
                <div class="param_value">
                    <input type="text" name="cpppayflowcolor" id="cpppayflowcolor" value="" runat="server" />
                </div>
                <div class="param_name">
                    Business name to display in the PayPal account on the PayPal hosted checkout pages</div>
                <div class="param_value">
                    <input type="text" name="brandName" id="brandName" value="" runat="server" />
                </div>
                <!-- Advanced features -->
                <div class="section_header">
                    Advanced features (optional)</div>
                <div class="param_name">
                    Allow buyer to enter note to merchant on PayPal pages</div>
                <div class="param_value">
                    <asp:DropDownList ID="allowNote" runat="server">
                        <asp:ListItem Text="False" Value="0" />
                        <asp:ListItem Text="True" Value="1" />
                    </asp:DropDownList>
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
