<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UserAccountDetails.ascx.cs" Inherits="OfferManagement.Portal.UserControls.GenericControls.UserAccountDetails" %>

<asp:MultiView ID="mvAccountDetails" runat="server" ActiveViewIndex="0">

    <asp:View ID="vAccountDetails" runat="server">
        <table class="dv" id="tbl" runat="server" style="width: 100%">
            <tr>
                <th colspan="6" class="header">&raquo; Στοιχεία Λογαριασμού
                </th>
            </tr>
            <tr id="trUsername" runat="server">
                <th>Username:
                </th>
                <td>
                    <asp:Label ID="ltrUsername" runat="server" ForeColor="Blue" />
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <th style="width: 120px">Email:
                </th>
                <td>
                    <table>
                        <tr>
                            <td>
                                <a id="btnEmail" runat="server" href="#" title="Αλλαγή Email" class="img-btn bg-emailEdit tooltip" clientidmode="Static">
                                    <img alt="" src="/_img/s.gif" style="width: 16px; height: 16px" /></a>
                                <asp:Label ID="ltrEmail" runat="server" ForeColor="Blue" ClientIDMode="Static" />
                                <span id="emailError" />
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnAddEmail" ClientInstanceName="btnAddEmail" runat="server" Text="Προσθήκη Email" Image-Url="~/_img/iconAdd.png" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center; width: 50px">
                    <img id="imgEmailVerified" runat="server" src="~/_img/iconYes.png" width="24" alt="Επιβεβαιωμένο στοιχείο" class="tooltip" title="Επιβεβαιωμένο στοιχείο" />
                    <img id="imgEmailNotVerified" runat="server" src="~/_img/iconNo.png" width="24" alt="Μη Επιβεβαιωμένο στοιχείο" class="tooltip" title="Μη Επιβεβαιωμένο στοιχείο" />
                </td>
                <td>
                    <dx:ASPxButton ID="btnSendEmailVerification" runat="server" Text="Επαναποστολή Email Επιβεβαίωσης" Image-Url="~/_img/iconEmailSend.png" OnClick="btnSendEmailVerification_Click" />
                </td>
            </tr>
            <tr id="trMobilePhone" runat="server">
                <th>Κινητό:
                </th>
                <td>
                    <table>
                        <tr>
                            <td>
                                <a id="btnMobilePhone" runat="server" href="#" title="Αλλαγή Κινητού" class="img-btn bg-cellPhone tooltip" clientidmode="Static">
                                    <img alt="" src="/_img/s.gif" style="width: 16px; height: 16px" /></a>
                                <asp:Label ID="ltrMobilePhone" runat="server" ForeColor="Blue" ClientIDMode="Static" />
                                <span id="mobilePhoneError" />
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnAddMobilePhone" ClientInstanceName="btnAddMobilePhone" runat="server" Text="Προσθήκη Κινητού" Image-Url="~/_img/iconAdd.png" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: center">
                    <img id="imgMobilePhoneVerified" runat="server" src="~/_img/iconYes.png" width="24" alt="Επιβεβαιωμένο στοιχείο" class="tooltip" title="Επιβεβαιωμένο στοιχείο" />
                    <img id="imgMobilePhoneNotVerified" runat="server" src="~/_img/iconNo.png" width="24" alt="Μη Επιβεβαιωμένο στοιχείο" class="tooltip" title="Μη Επιβεβαιωμένο στοιχείο" />
                </td>
                <td>
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxButton ID="btnSendMobilePhoneVerification" runat="server" Text="Επαναποστολή SMS Επιβεβαίωσης" Image-Url="~/_img/iconCellPhone.png" OnClick="btnSendMobilePhoneVerification_Click" />
                            </td>
                            <td>
                                <dx:ASPxButton ID="btnInsertVerificationCode" runat="server" Text="Επιβεβαίωση Κινητού" Image-Url="~/_img/iconAccept.png" OnClick="btnInsertVerificationCode_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

        <div class="br"></div>

        <asp:Label ID="lblContactInfoErrors" runat="server" Font-Bold="true" ForeColor="Red" />
    </asp:View>

    <asp:View ID="vMobilePhoneVerification" runat="server">
        <div class="reminder" style="text-align: left">
            Για να επιβεβαιωθεί ο αριθμός του κινητού σας τηλεφώνου, εισάγετε τον 8-ψήφιο κωδικό που ήρθε με SMS στο κινητό που δηλώσατε κατά την εγγραφή.
        </div>

        <table class="dv">
            <colgroup>
                <col style="width: 125px" />
                <col style="width: 120px" />
            </colgroup>
            <tr>
                <th>8-ψήφιος Κωδικός:
                </th>
                <td>
                    <dx:ASPxTextBox ID="txtMobilePhoneVerificationCode" runat="server" />
                </td>
            </tr>
        </table>

        <div class="filterButtons">

            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnVerifyMobilePhone" runat="server" Text="Επιβεβαίωση Κινητού" Image-Url="~/_img/iconAccept.png" OnClick="btnVerifyMobilePhone_Click" />
                    </td>
                    <td>
                        <dx:ASPxButton ID="btnCancel" runat="server" Image-Url="~/_img/iconCancel.png" Text="Ακύρωση" CausesValidation="false" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>

        </div>

        <div class="br"></div>

        <asp:Label ID="lblMobilePhoneVerificationErrors" runat="server" Font-Bold="true" ForeColor="Red" />
    </asp:View>

</asp:MultiView>