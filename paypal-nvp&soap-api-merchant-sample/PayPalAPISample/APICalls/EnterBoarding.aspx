<%@ Page Language="C#" AutoEventWireup="true" Codebehind="EnterBoarding.aspx.cs"
    Inherits="PayPalAPISample.APICalls.EnterBoarding" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - EnterBoarding</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                EnterBoarding</h3>
            <div id="apidetails">
                Request to start merchant onboarding process.</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Program Code issued by PayPal *</div>
                <div class="param_value">
                    <input type="text" name="programCode" id="programCode" value="TestProgram" runat="server" />
                </div>
                <div class="param_name">
                    Product List (comma separated) *</div>
                <div class="param_value">
                    <input type="text" name="productList" id="productList" value="dp,ec" runat="server" />
                </div>
                <div class="param_name">
                    Custom information for partner</div>
                <div class="param_value">
                    <input type="text" name="partnerCustom" id="partnerCustom" runat="server" />
                </div>
                <div class="param_name">
                    Logo URL (Displayed in the PayPal partner welcome page)</div>
                <div class="param_value">
                    <input type="text" name="imageUrl" id="imageUrl" runat="server" value="https://www.paypal.com/en_US/i/logo/paypal_logo.gif" />
                </div>
                <div class="param_name">
                    Custom information for partner</div>
                <div class="param_value">
                    <input type="text" name="partnerCustom" id="Text2" runat="server" />
                </div>
                <div class="param_name">
                    Marketing Category</div>
                <div class="param_value">
                    <asp:DropDownList ID="marketingCategory" runat="server">
                        <asp:ListItem Value="Marketing-Category-Default" Text="Marketing-Category-Default" />
                        <asp:ListItem Value="Marketing-Category1" Text="Marketing-Category1" />
                        <asp:ListItem Value="Marketing-Category2" Text="Marketing-Category2" />
                        <asp:ListItem Value="Marketing-Category3" Text="Marketing-Category3" />
                        <asp:ListItem Value="Marketing-Category4" Text="Marketing-Category4" />
                        <asp:ListItem Value="Marketing-Category5" Text="Marketing-Category5" />
                        <asp:ListItem Value="Marketing-Category6" Text="Marketing-Category6" />
                        <asp:ListItem Value="Marketing-Category7" Text="Marketing-Category7" />
                        <asp:ListItem Value="Marketing-Category8" Text="Marketing-Category8" />
                        <asp:ListItem Value="Marketing-Category9" Text="Marketing-Category9" />
                        <asp:ListItem Value="Marketing-Category10" Text="Marketing-Category10" />
                        <asp:ListItem Value="Marketing-Category11" Text="Marketing-Category11" />
                        <asp:ListItem Value="Marketing-Category12" Text="Marketing-Category12" />
                        <asp:ListItem Value="Marketing-Category13" Text="Marketing-Category13" />
                        <asp:ListItem Value="Marketing-Category14" Text="Marketing-Category14" />
                        <asp:ListItem Value="Marketing-Category15" Text="Marketing-Category15" />
                        <asp:ListItem Value="Marketing-Category16" Text="Marketing-Category16" />
                        <asp:ListItem Value="Marketing-Category17" Text="Marketing-Category17" />
                        <asp:ListItem Value="Marketing-Category18" Text="Marketing-Category18" />
                        <asp:ListItem Value="Marketing-Category19" Text="Marketing-Category19" />
                        <asp:ListItem Value="Marketing-Category20" Text="Marketing-Category20" />
                    </asp:DropDownList>
                </div>
                <div class="submit">
                    <input id="Submit" type="submit" name="submit" value="Submit" runat="server" onserverclick="Submit_Click" />
                    <br />
                    <br />
                    <a href="../Default.aspx">Home</a>&nbsp;&nbsp;<a href="javascript:history.back();">Back</a>
                </div>
            </div>
        </form>
        <div id="relatedcalls">
            See also
            <ul>
                <li><a href="GetBoardingDetails.aspx">GetBoardingDetails</a></li>
            </ul>
        </div>
    </div>
</body>
</html>
