<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetExpressCheckoutPaymentAuthorization.aspx.cs" Inherits="PayPalAPISample.UseCaseSamples.SetExpressCheckoutPaymentAuthorization" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK - SetExpressCheckoutPaymentAuthorization</title>
    <link href="../APICalls/sdk.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>SetExpressCheckoutPaymentAuthorization</h3>
			    <div id="apidetails">
                    <ul><li><i>Set the details for <b>ExpressCheckout</b></i></li><li><i>Payment Type should be set to <b>Authorization</b> to Create Payment Authorization</i></li><li><i>Authorized Payment can be Captured directly using DoCapture API</i></li></ul>             
                </div>
		</div>
        <br />
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
					<div class="param_name">Total Shipping Cost</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="shippingTotal" Text="0.50" />
					</div>
				</div>
				<div class="params">
					<div class="param_name">Total Insurance Cost</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="insuranceTotal" />
					</div>
				</div>
				<div class="params">
					<div class="param_name">Total Handling Cost</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="handlingTotal" />
					</div>
				</div>
				<div class="params">
					<div class="param_name">Total Tax</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="taxTotal" />
					</div>
				</div>
				<div class="params">
					<div class="param_name">Order Description</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="orderDescription" Text="Description" />
					</div>
				</div>
				<div class="params">
					<div class="param_name">CurrencyCode</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="currencyCode" Text="USD"  />
					</div>
				</div>
				<div class="params">
					<div class="param_name">Payment Type (Select Authorization UseCase)</div>
					<div class="param_value">
                        <asp:DropDownList runat="server" ID="paymentType">
                            <asp:ListItem Text="Sale" Value="SALE" Enabled="false" />
                            <asp:ListItem Text="Authorization" Value="AUTHORIZATION" Selected="True" />
                            <asp:ListItem Text="Order" Value="ORDER" Enabled="false" />
                        </asp:DropDownList>
					</div>
				</div>
                <br />
				<div class="section_header">
					<asp:Label runat="server" ID="LabelItemDetails" Text="Item Details" Font-Underline="true"
                    Font-Bold="true" />
				</div>
                <br />
				<table class="params">
					<tr>
						<th class="param_name">Name</th>
						<th class="param_name">Cost</th>
						<th class="param_name">Quantity</th>
						<th class="param_name">Sales Tax</th>
						<th class="param_name">Item Category</th>
					</tr>

					<tr>
						<td>
                            <div class="param_value">
                                <asp:TextBox runat="server" ID="itemName" Text="Item Name" />
							</div>
                        </td>
						<td>
                            <div class="param_value">
	                            <asp:TextBox runat="server" ID="itemAmount" Text="5.27" />
							</div></td>
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
					<div class="param_name">IPN Notification Url (Receive IPN call back from PayPal)</div>
					<div class="param_value">
                        <asp:TextBox runat="server" ID="notifyURL" />
					</div>
				</div>				
				<br />
				<div class="submit">
                    <asp:Button ID="ButtonPayments" Text="SetExpressCheckoutPaymentAuthorization"  runat="server" PostBackUrl="~/UseCaseSamples/RequestResponse.aspx" />
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
