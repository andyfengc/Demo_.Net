<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="PayPalAPISample._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PayPal Merchant SDK - API Samples - Home page</title>
    <link rel="stylesheet" type="text/css" href="APICalls/sdk.css" />
</head>
<body>
    <img src="https://devtools-paypal.com/image/bdg_payments_by_pp_2line.png" alt="PAYMENTS BY PayPal" />
    <form id="form1" runat="server">
        <div>
            <br />
            <h2>Merchant UseCase Samples</h2>
            <div>
                <asp:BulletedList runat="server" ID="BulletedListUseCaseSamples" DisplayMode="HyperLink"> 
                    <asp:ListItem Value="~/UseCaseSamples/SetExpressCheckoutForRecurringPayments.aspx" Text="CreateRecurringPaymentsProfile - ExpressCheckout" />
                    <asp:ListItem Value="~/UseCaseSamples/RecurringPaymentsUsingCreditCard.aspx" Text="CreateRecurringPaymentsProfile - CreditCard" />
                    <asp:ListItem Value="~/UseCaseSamples/SetExpressCheckoutPaymentAuthorization.aspx" Text="SetExpressCheckout - PaymentAuthorization - DoExpressCheckoutPayment - DoCapture" />
                    <asp:ListItem Value="~/UseCaseSamples/SetExpressCheckoutPaymentOrder.aspx" Text="SetExpressCheckout - PaymentOrder - DoExpressCheckoutPayment - DoAuthorize - DoCapture" />
                    <asp:ListItem Value="~/UseCaseSamples/ParallelPayment.aspx" Text="ParallelPayment" /> 
                </asp:BulletedList>
            </div>
            <br />
            <h2>PayPal SDK - API Samples</h2>
            <table>
                <tr>
                    <td>
                        <div class="section_header">
                            Express Checkout
                        </div>
                        <ul>
                            <li><a href="APICalls/SetExpressCheckout.aspx">SetExpressCheckout</a></li>
                            <li><a href="APICalls/GetExpressCheckoutDetails.aspx">GetExpressCheckoutDetails</a></li>
                            <li><a href="APICalls/DoExpressCheckoutPayment.aspx">DoExpressCheckoutPayment</a></li>
                        </ul>
                    </td>
                    <td>
                        <div class="section_header">
                            Transaction reporting
                        </div>
                        <ul>
                            <li><a href="APICalls/GetTransactionDetails.aspx">GetTransactionDetails</a></li>
                            <li><a href="APICalls/TransactionSearch.aspx">TransactionSearch</a></li>
                            <li><a href="APICalls/GetBalance.aspx">GetBalance</a></li>
                            <li><a href="APICalls/GetPalDetails.aspx">GetPalDetails</a></li>
                            <li><a href="APICalls/ManagePendingTransactionStatus.aspx">ManagePendingTransactionStatus</a></li>
                            <li><a href="APICalls/AddressVerify.aspx">AddressVerify</a></li>
                        </ul>
                    </td>
                    <td>
                        <div class="section_header">
                            Direct Payment (DCC)
                        </div>
                        <ul>
                            <li><a href="APICalls/DoDirectPayment.aspx">DoDirectPayment</a></li>
                        </ul>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div class="section_header">
                            Recurring Payments & Reference transactions
                        </div>
                        <ul>
                            <li><a href="APICalls/BillOutStandingAmount.aspx">BillOutStandingAmount</a></li>
                            <li><a href="APICalls/CreateRecurringPaymentsProfile.aspx">CreateRecurringPaymentsProfile</a></li>
                            <li><a href="APICalls/GetRecurringPaymentsProfileDetails.aspx">GetRecurringPaymentsProfileDetails</a></li>
                            <li><a href="APICalls/ManageRecurringPaymentsProfileStatus.aspx">ManageRecurringPaymentsProfileStatus</a></li>
                            <li><a href="APICalls/UpdateRecurringPaymentsProfile.aspx">UpdateRecurringPaymentsProfile</a></li>
                            <li><a href="APICalls/BillAgreementUpdate.aspx">BillAgreementUpdate</a></li>
                            <li><a href="APICalls/CreateBillingAgreement.aspx">CreateBillingAgreement</a></li>
                            <li><a href="APICalls/GetBillingAgreementCustomerDetails.aspx">GetBillingAgreementCustomerDetails</a></li>
                            <li><a href="APICalls/SetCustomerBillingAgreement.aspx">SetCustomerBillingAgreement</a></li>
                            <li><a href="APICalls/DoReferenceTransaction.aspx">DoReferenceTransaction</a></li>
                        </ul>
                    </td>
                    <td>
                        <div class="section_header">
                            Settlements and refund
                        </div>
                        <ul>
                            <li><a href="APICalls/DoAuthorization.aspx">DoAuthorization</a></li>
                            <li><a href="APICalls/DoCapture.aspx">DoCapture</a></li>
                            <li><a href="APICalls/DoReauthorization.aspx">DoReauthorization</a></li>
                            <li><a href="APICalls/DoVoid.aspx">DoVoid</a></li>
                            <li><a href="APICalls/RefundTransaction.aspx">RefundTransaction</a></li>
                            <li><a href="APICalls/ReverseTransaction.aspx">ReverseTransaction</a></li>
                            <li><a href="APICalls/DoNonReferencedCredit.aspx">DoNonReferencedCredit</a></li>
                        </ul>
                    </td>
                    <td>
                        <div class="section_header">
                            Mass Pay
                        </div>
                        <ul>
                            <li><a href="APICalls/MassPay.aspx">MassPay</a></li>
                        </ul>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
