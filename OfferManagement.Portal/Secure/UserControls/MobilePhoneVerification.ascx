<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MobilePhoneVerification.ascx.cs" Inherits="OfferManagement.Portal.Secure.UserControls.MobilePhoneVerification" %>

<asp:MultiView ID="mvVerification" runat="server" ActiveViewIndex="0">

    <asp:View ID="vNotVerified" runat="server">
        <table class="dv" style="width: 100%">
            <colgroup>
                <col style="width: 130px" />
            </colgroup>
            <tr>
                <td colspan="2">
                    <div class="sub-description">
                        <span>Για να επιβεβαιωθεί ο αριθμός του κινητού σας τηλεφώνου, εισάγετε τον 8-ψήφιο κωδικό που ήρθε με SMS στο κινητό που δηλώσατε κατά την εγγραφή.
                                <br />
                            <br />
                            Σε περίπτωση που δεν λάβατε το SMS μπορείτε να μεταβείτε στην καρτέλα
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Secure/Providers/ProviderDetails.aspx">Στοιχεία Προμηθευτή</asp:HyperLink>
                            και να ζητήσετε επαναποστολή του SMS.</span>
                    </div>
                </td>
            </tr>
            <tr>
                <th>8-ψήφιος Κωδικός:
                </th>
                <td>
                    <dx:ASPxTextBox ID="txtVerificationCode" runat="server" Width="100">
                        <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Πρέπει να εισάγετε τον 8-ψήφιο κωδικό που ήρθε με SMS στο κινητό σας." ErrorDisplayMode="Text" />
                    </dx:ASPxTextBox>
                </td>
            </tr>
        </table>

        <div class="br"></div>

        <dx:ASPxButton ID="btnSubmit" runat="server" Text="Επιβεβαίωση Κινητού" Image-Url="~/_img/iconAccept.png" OnClick="btnSubmit_Click" />

        <div class="br"></div>

        <dx:ASPxLabel ID="lblErrors" runat="server" ForeColor="Red" />
    </asp:View>

    <asp:View ID="vVerified" runat="server">
        <div class="reminder">
            Ο αριθμός του κινητού σας τηλεφώνου επιβεβαιώθηκε επιτυχώς. 
        </div>
        <br />
        <table class="dv" style="width: 100%">
            <colgroup>
                <col style="width: 150px" />
            </colgroup>
            <tr>
                <th>Κωδικός Επιβεβαίωσης:
                </th>
                <td>
                    <dx:ASPxLabel ID="lblVerificationCode" runat="server" />
                </td>
            </tr>
            <tr>
                <th>Ημ/νία Επιβεβαίωσης:
                </th>
                <td>
                    <dx:ASPxLabel ID="lblVerificationDate" runat="server" />
                </td>
            </tr>
        </table>
    </asp:View>

</asp:MultiView>