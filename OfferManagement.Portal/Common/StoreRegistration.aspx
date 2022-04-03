<%@ Page Title="Δημιουργία Λογαριασμού Προμηθευτή" Language="C#" MasterPageFile="~/Common/Common.Master" AutoEventWireup="true" CodeBehind="StoreRegistration.aspx.cs" Inherits="OfferManagement.Portal.Common.StoreRegistration" %>

<%@ Register TagName="RegisterUserInput" TagPrefix="my" Src="~/UserControls/GenericControls/RegisterUserInput.ascx" %>
<%@ Register TagName="StoreInput" TagPrefix="my" Src="~/UserControls/StoreControls/InputControls/StoreInput.ascx" %>
<%@ Register TagName="ParticipationTerms" TagPrefix="my" Src="~/UserControls/GenericControls/ParticipationTerms.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="cphCommonMain">

    <asp:ScriptManagerProxy runat="server">
        <Scripts>
            <asp:ScriptReference Path="~/_js/Terms.js" />
        </Scripts>
    </asp:ScriptManagerProxy>

    <asp:MultiView ID="mvRegistration" runat="server" ActiveViewIndex="0">

        <asp:View ID="vTerms" runat="server">
            <table style="width: 100%">
                <tr>
                    <td style="width: 100%;">
                        <a id="btnPrintTermsAndConditions" runat="server" class="icon-btn bg-print" style="margin-bottom: 10px" href="javascript:ShowTerms();">Εκτύπωση Όρων Χρήσης του Πληροφοριακού Συστήματος</a>
                    </td>
                </tr>
            </table>
            <div id="terms">
                <my:ParticipationTerms ID="ucParticipationTerms" runat="server" />
            </div>
            <div id="declaration">
                <div style="margin: 10px; font-weight: bold;">
                    Δηλώνω Υπεύθυνα ότι:
                </div>
                <div>
                    <table>
                        <tr>
                            <td>
                                <dx:ASPxCheckBox runat="server" ID="chkAgreeTerms" ClientInstanceName="chkAgreeTerms" Checked="false" Text=" ">
                                    <ClientSideEvents ValueChanged="function(s,e) { CheckAccepted(); }" />
                                </dx:ASPxCheckBox>
                            </td>
                            <td>Έχω διαβάσει και αποδέχομαι τους <em>Όρους Χρήσης του Πληροφοριακού Συστήματος</em> (κάντε scroll για να τους διαβάσετε)
                            </td>
                        </tr>
                    </table>
                </div>                
            </div>
            <dx:ASPxButton ID="btnAcceptTerms" runat="server" ClientInstanceName="btnAcceptTerms" ClientEnabled="false" Text="Συνέχεια Εγγραφής" Image-Url="~/_img/iconAccept.png" OnClick="btnAcceptTerms_Click" />
        </asp:View>

        <asp:View ID="vRegister" runat="server">
            <asp:PlaceHolder ID="phErrors" runat="server">
                <asp:Label ID="lblErrors" runat="server" Font-Bold="true" ForeColor="Red" />
                <div class="br"></div>
            </asp:PlaceHolder>

            <my:RegisterUserInput ID="ucRegisterUserInput" runat="server" ShowHintPopup="true" Header="Στοιχεία Λογαριασμού Χρήστη" HideMobilePhoneFields="true" EmailInfo="Στο email που θα δηλώσετε, θα σας σταλεί το email ενεργοποίησης του λογαριασμού σας. Βεβαιωθείτε ότι το πληκτρολογήσατε σωστά." ValidationGroup="vgRegistration" LabelWidth="195px" />

            <div class="br"></div>

            <my:StoreInput ID="ucStoreInput" runat="server" ValidationGroup="vgRegistration" />

            <div class="br"></div>

            <lc:BotShield ID="bsStoreInput" runat="server" ClientIDMode="Static" ValidationGroup="vgRegistration" />

            <div class="br"></div>
            <div class="br"></div>

            <dx:ASPxButton ID="btnCreate" runat="server" Text="Δημιουργία Λογαριασμού" Image-Url="~/_img/iconAccept.png" OnClick="btnCreate_Click" ValidationGroup="vgRegistration" />

            <div class="br"></div>
            <div class="br"></div>

            <div class="summaryContainer">
                <dx:ASPxValidationSummary runat="server" ClientInstanceName="validationSummary" RenderMode="BulletedList" ShowErrorsInEditors="true" ValidationGroup="vgRegistration" />
            </div>

        </asp:View>

        <asp:View ID="vNotAllowed" runat="server">
            <div class="reminder" style="font-weight: bold; color: Red">
                Η εγγραφή στην εφαρμογή θα είναι σύντομα διαθέσιμη
            </div>
        </asp:View>

    </asp:MultiView>

</asp:Content>
