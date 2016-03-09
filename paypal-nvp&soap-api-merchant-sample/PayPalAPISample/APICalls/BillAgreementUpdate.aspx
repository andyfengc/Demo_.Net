<%@ Page Language="C#" AutoEventWireup="true" Codebehind="BillAgreementUpdate.aspx.cs"
    Inherits="PayPalAPISample.APICalls.BAUpdate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>PayPal SDK - BillAgreementUpdate</title>
    <link rel="stylesheet" type="text/css" href="sdk.css" />
</head>
<body>
	<img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <div id="wrapper">
        <div id="header">
            <h3>
                BillAgreementUpdate</h3>
            <div id="apidetails">
                Update/Delete a billing agreement</div>
        </div>
        <form id="form1" runat="server">
            <div id="request_form">
                <div class="param_name">
                    Reference ID (Billing agreement ID / Reference Transaction ID)*</div>
                <div class="param_value">
                    <input type="text" name="referenceId" id="referenceId" value="" runat="server" />
                </div>
                <div class="param_name">
                    Billing agreement status</div>
                <div class="param_value">
                    <asp:DropDownList ID="billingAgreementStatus" runat="server">
                        <asp:ListItem Text="Select a value" Value="" />
                        <asp:ListItem Text="Active" Value="ACTIVE" />
                        <asp:ListItem Text="Canceled" Value="CANCELED" />
                    </asp:DropDownList>
                </div>
                <div class="param_name">
                    Billing agreement description</div>
                <div class="param_value">
                    <textarea id="billingAgreementText" runat="server" rows="5" cols="60" />
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
