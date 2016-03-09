<%@ Page Language="C#" AutoEventWireup="true" Codebehind="DoReferenceTransaction.aspx.cs"
    Inherits="PayPalAPISample.APICalls.DoReferenceTransaction" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - DoReferenceTransaction</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                DoReferenceTransaction</h3>
            <div id="apidetails">
                Process a payment from a buyer’s account based on a previous transaction.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Reference ID (Billing agreement ID / DoDirectPayment transaction ID)*</div>
                <div class="param_value">
                    <input type="text" name="referenceId" id="referenceId" value="" runat="server" />
                </div>
                <div class="section_header">
                    Payment Details</div>
                <div class="param_name">
                    Payment currency code</div>
                <div class="param_value">
                    <asp:DropDownList ID="currencyCode" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="USD" Value="USD" Selected="True"/>
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Total Shipping costs</div>
                <div class="param_value">
                    <input type="text" name="shippingTotal" id="shippingTotal" value="0.50" runat="server" />
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
                                <input type="text" name="itemName" id="itemName" value="Test item name" runat="server" />
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
                                <input type="text" name="salesTax" id="salesTax" value="0.23" runat="server" />
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
                <div class="params">
                    <div class="param_name">
                        IPN Notification Url</div>
                    <div class="param_value">
                        <input id="ipnNotificationUrl" name="ipnNotificationUrl" value="" runat="server" />
                    </div>
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
