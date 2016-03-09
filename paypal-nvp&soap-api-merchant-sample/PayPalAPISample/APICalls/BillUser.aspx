<%@ Page Language="C#" AutoEventWireup="true" Codebehind="BillUser.aspx.cs" Inherits="PayPalAPISample.APICalls.BillUser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - BillUser</title>
    <link href="sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                BillUser</h3>
            <div id="apidetails">
                Initiate money transfer from a user's account to the merchant's account in accordance
                with a previously established billing agreement.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Amount to Bill * (See API Reference document for complete list of supported currencies
                    )</div>
                <div class="param_value">
                    <input type="text" name="amount" id="amount" runat="server" value="1.0" />
                    <asp:DropDownList ID="currencyCode" runat="server">
                        <asp:ListItem Text="USD" Value="USD" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Merchant Pull Id *</div>
                <div class="param_value">
                    <input type="text" name="mpId" id="mpId" runat="server" />
                </div>
                <div class="param_name">
                    Payment Type</div>
                <div class="param_value">
                    <asp:DropDownList ID="paymentType" runat="server">
                        <asp:ListItem Text="-Select a value-" Value="" />
                        <asp:ListItem Text="Any" Value="ANY" />
                        <asp:ListItem Text="InstantOnly" Value="INSTANTONLY" />
                        <asp:ListItem Text="EcheckOnly" Value="ECHECKONLY" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Memo</div>
                <div class="param_value">
                    <input type="text" name="memo" id="memo" runat="server" />
                </div>
                <div class="param_name">
                    Subject line on Confirmation email</div>
                <div class="param_value">
                    <input type="text" name="emailSubject" id="emailSubject" runat="server" />
                </div>
                <div class="param_name">
                    Tax on transaction</div>
                <div class="param_value">
                    <input type="text" name="tax" id="tax" runat="server" />
                    <asp:DropDownList ID="taxCurrencyCode" runat="server">
                        <asp:ListItem Text="USD" Value="USD" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Per-transaction shipping charge</div>
                <div class="param_value">
                    <input type="text" name="shipping" id="shipping" runat="server" />
                    <asp:DropDownList ID="shippingCurrencyCode" runat="server">
                        <asp:ListItem Text="USD" Value="USD" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Per-transaction handling charge</div>
                <div class="param_value">
                    <input type="text" name="handling" id="handling" runat="server" />
                    <asp:DropDownList ID="handlingCurrencyCode" runat="server">
                        <asp:ListItem Text="USD" Value="USD" />
                        <asp:ListItem Text="AUD" Value="AUD" />
                        <asp:ListItem Text="EUR" Value="EUR" />
                        <asp:ListItem Text="GBP" Value="GBP" />
                        <asp:ListItem Text="JPY" Value="JPY" />
                        <asp:ListItem Text="SGD" Value="SGD" />
                        <asp:ListItem Text="HKD" Value="HKD" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Item Name</div>
                <div class="param_value">
                    <input type="text" name="itemName" id="itemName" runat="server" />
                </div>
                <div class="param_name">
                    Item Number</div>
                <div class="param_value">
                    <input type="text" name="itemNumber" id="itemNumber" runat="server" />
                </div>
                <div class="param_name">
                    Invoice Number</div>
                <div class="param_value">
                    <input type="text" name="invoiceNumber" id="invoiceNumber" runat="server" />
                </div>
                <div class="param_name">
                    Custom annotation</div>
                <div class="param_value">
                    <input type="text" name="custom" id="custom" runat="server" />
                </div>
                <div class="param_name">
                    Custom identification code (for use by third-party applications)</div>
                <div class="param_value">
                    <input type="text" name="buttonSource" id="buttonSource" runat="server" />
                </div>
                <div class="param_name">
                    Soft Descriptor</div>
                <div class="param_value">
                    <input type="text" name="softDescriptor" id="softDescriptor" runat="server" />
                </div>
                <div class="param_name">
                    Return "Fraud Management Filter" Details</div>
                <div class="param_value">
                    <asp:DropDownList ID="returnFMFDetails" runat="server">
                        <asp:ListItem Text="False" Value="0" />
                        <asp:ListItem Text="True" Value="1" />
                    </asp:DropDownList>
                </div>
                <div class="submit">
                    <input id="submit" type="submit" name="submit" value="Submit" runat="server" onserverclick="BillUser_Submit" />
                    <br />
                    <br />
                    <a href="../Default.aspx">Home</a>&nbsp;&nbsp;<a href="javascript:history.back();">Back</a>
                </div>
            </div>
        </form>
    </div>
    <div id="relatedcalls">
    </div>
</body>
</html>
