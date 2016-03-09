<%@ Page Language="C#" AutoEventWireup="true" Codebehind="MassPay.aspx.cs" Inherits="PayPalAPISample.APICalls.MassPay" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - MassPay</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                MassPay</h3>
            <div id="apidetails">
                Send payments to many recipients at once. This feature is available only for verified
                PayPal Premier accounts and Business accounts.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Subject line on email sent to recipients</div>
                <div class="param_value">
                    <input type="text" name="emailSubject" id="emailSubject" value="Your payment" runat="server" />
                </div>
                <div class="param_name">
                    How do you identify payment receivers</div>
                <div class="param_value">
                    <asp:DropDownList ID="receiverType" runat="server">
                        <asp:ListItem Text="Email Address" Value="EMAILADDRESS" Selected="true" />
                        <asp:ListItem Text="User ID" Value="USERID" />
                        <asp:ListItem Text="Phone Number" Value="PHONENUMBER" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Receiver Info (Include upto 250 receipients)</div>
                <table class="line_item">
                    <tr>
                        <th>
                            Email</th>
                        <th>
                            Phone Number</th>
                        <th>
                            PayPal User Id</th>
                        <th>
                            Payment amount</th>
                        <th>
                            Note to recipient</th>
                        <th>
                            Custom tracking number</th>
                    </tr>
                    <tr>
                        <td>
                            <span class="param_value">
                                <input type="text" name="emailId" id="emailId" value="platfo_1255077030_biz@gmail.com" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" name="phoneNumber" id="phoneNumber" value="" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" name="receiverId" id="receiverId" value="" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" name="amount" id="amount" value="1.0" runat="server" />
                                <asp:DropDownList ID="currencyCode" runat="server">
                                    <asp:ListItem Text="AUD" Value="AUD" />
                                    <asp:ListItem Text="EUR" Value="EUR" />
                                    <asp:ListItem Text="GBP" Value="GBP" />
                                    <asp:ListItem Text="HKD" Value="HKD" />
                                    <asp:ListItem Text="JPY" Value="JPY" />
                                    <asp:ListItem Text="SGD" Value="SGD" />
                                    <asp:ListItem Text="USD" Value="USD" Selected="true" />
                                </asp:DropDownList>
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" name="note" id="note" value="" runat="server" />
                            </span>
                        </td>
                        <td>
                            <span class="param_value">
                                <input type="text" name="uniqueId" id="uniqueId" value="" runat="server" />
                            </span>
                        </td>
                    </tr>
                </table>
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
