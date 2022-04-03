<%@ Page Title="Εισαγωγή Στοιχείων Προσφοράς" Language="C#" MasterPageFile="~/Secure/Stores/Stores.master" AutoEventWireup="true" CodeBehind="OfferDetails.aspx.cs" Inherits="OfferManagement.Portal.Secure.Stores.OfferDetails" %>

<%@ Register TagName="LaptopOfferInput" TagPrefix="my" Src="~/UserControls/OfferControls/InputControls/LaptopOfferInput.ascx" %>
<%@ Register TagName="OfferGeneralInfoInput" TagPrefix="my" Src="~/UserControls/OfferControls/InputControls/OfferGeneralInfoInput.ascx" %>
<%@ Register TagPrefix="my" TagName="OfferRequirementsView" Src="~/UserControls/OfferControls/ViewControls/OfferRequirementsView.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphSecureMain" runat="server">
    <asp:MultiView ID="mvOffer" runat="server" ActiveViewIndex="0">
        <asp:View ID="vOffer" runat="server">

            <my:OfferRequirementsView ID="ucRequirements" runat="server" />

            <div class="br"></div>

            <my:OfferGeneralInfoInput ID="ucOfferGeneralInfoInput" runat="server" ValidationGroup="vgOffer" />

            <div class="br"></div>

            <my:LaptopOfferInput ID="ucOfferInput" runat="server" ValidationGroup="vgOffer" />

            <asp:PlaceHolder ID="phEvaluationComments" runat="server" Visible="false">
                <div class="br"></div>
                <table class="dv" style="width: 100%">
                    <colgroup>
                        <col style="width: 195px" />
                    </colgroup>
                    <tr>
                        <th class="header">&raquo; Σχόλια Αξιολόγησης
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <div class="memo">
                                <pre><asp:Literal ID="lblEvaluationComments" runat="server" /></pre>
                            </div>
                        </td>
                    </tr>
                </table>
            </asp:PlaceHolder>

            <div class="br"></div>

            <table>
                <tr>
                    <td>
                        <dx:ASPxButton ID="btnSave" runat="server" Image-Url="~/_img/iconSave.png" Text="Αποθήκευση & Συνέχεια" OnClick="btnSave_Click" ValidationGroup="vgOffer" />
                    </td>
                    <td id="tdSaveAndSubmit" runat="server" visible="false">
                        <dx:ASPxButton ID="btnSaveAndSubmit" runat="server" Image-Url="~/_img/iconAccept.png" Text="Αποθήκευση & Υποβολή" OnClick="btnSaveAndSubmit_Click" ValidationGroup="vgOffer" />
                    </td>
                    <td id="tdCancel" runat="server" visible="false">
                        <dx:ASPxButton ID="btnCancel" runat="server" Image-Url="~/_img/iconCancel.png" Text="Ακύρωση" CausesValidation="false" OnClick="btnCancel_Click" />
                    </td>
                </tr>
            </table>

            <div class="br"></div>
            <div class="br"></div>

            <div class="summaryContainer">
                <dx:ASPxValidationSummary runat="server" ClientInstanceName="validationSummary" RenderMode="BulletedList" ShowErrorsInEditors="true" ValidationGroup="vgOffer" />
            </div>

            <asp:PlaceHolder ID="phErrors" runat="server" Visible="false">
                <div class="br"></div>
                <asp:Label ID="lblErrors" runat="server" ForeColor="Red" Font-Bold="true" />
            </asp:PlaceHolder>
        </asp:View>

        <asp:View ID="vOfferCreationNotAllowed" runat="server">
            <div class="reminder">
                Η δημιουργία νέας Προσφοράς δεν είναι διαθέσιμη.
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
