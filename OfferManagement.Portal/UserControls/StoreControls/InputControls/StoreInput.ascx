<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StoreInput.ascx.cs" Inherits="OfferManagement.Portal.UserControls.StoreControls.InputControls.StoreInput" %>

<%@ Register TagName="HyperLinkInput" TagPrefix="my" Src="~/UserControls/GenericControls/HyperLinkInput.ascx" %>
<%@ Register TagName="GreekAddressInfoInput" TagPrefix="my" Src="~/UserControls/GenericControls/GreekAddressInfoInput.ascx" %>
<%@ Register TagName="ContactPersonInput" TagPrefix="my" Src="~/UserControls/GenericControls/ContactPersonInput.ascx" %>
<%@ Register TagName="TipIcon" TagPrefix="my" Src="~/UserControls/GenericControls/TipIcon.ascx" %>

<%@ Import Namespace="OfferManagement.BusinessModel" %>

<script type="text/javascript">

    $(function () {
        ibanChecker(txtIBAN);
        afmChecker(txtAFM);
    });

    var afmsChecked = {};
    function afmChecker(s, e) {
        dx_Blur(s, e);

        var afm = s.GetValue();

        if (!afm)
            return;

        if (!Imis.Lib.IsAfmValid(afm)) {
            return;
        }

        var checkResult = afmsChecked[afm];

        if (checkResult) {
            return;
        }

        $('#afm-checking').show();

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/PortalServices/Services.asmx/CheckAFM",
            data: JSON.stringify({ "afm": afm }),
            dataType: "json",
            async: true,
            success: function (data, textStatus) {
                if (textStatus == "success") {
                    var msg = {};
                    if (data.hasOwnProperty('d')) {
                        msg = data.d;
                    } else {
                        msg = data;
                    }
                    afmsChecked[afm] = msg;
                    s.Validate();
                }
                $('#afm-checking').hide();
            },
            error: function (data, status, error) {
                $('#afm-checking').hide();
            }
        });
    }

    function afmValidate(s, e) {
        var afm = s.GetValue();

        if (!afm)
            return;

        Imis.Lib.CheckAfm(s, e);
        if (!e.isValid) {
            e.errorText = 'Το Α.Φ.Μ. δεν είναι έγκυρο';
            return;
        }

        var checkResult = afmsChecked[afm];

        if (!checkResult) {
            e.isValid = false;
            return;
        }

        e.isValid = checkResult.IsValid;
        e.errorText = checkResult.Message;
    }

    var ibansChecked = {};
    function ibanChecker(s, e) {
        dx_Blur(s, e);

        var iban = s.GetValue();

        if (!iban)
            return;

        var checkResult = ibansChecked[iban];

        if (checkResult) {
            return;
        }

        $('#iban-checking').show();

        $.ajax({
            type: "POST",
            contentType: "application/json; charset=utf-8",
            url: "/PortalServices/Services.asmx/CheckIBAN",
            data: JSON.stringify({ "iban": iban }),
            dataType: "json",
            async: true,
            success: function (data, textStatus) {
                if (textStatus == "success") {
                    var msg = {};
                    if (data.hasOwnProperty('d')) {
                        msg = data.d;
                    } else {
                        msg = data;
                    }
                    ibansChecked[iban] = msg;
                    s.Validate();
                }
                $('#iban-checking').hide();
            },
            error: function (data, status, error) {
                $('#iban-checking').hide();
            }
        });
    }

    function ibanValidate(s, e) {
        var iban = s.GetValue();

        if (typeof (bankDetailsRequired) !== 'undefined') {
            if (bankDetailsRequired == true && (iban == null || iban == '' || typeof (iban) === 'undefined')) {
                e.isValid = false;
                e.errorText = "Το πεδίο 'Αρ. IBAN' είναι υποχρεωτικό";
                return;
            }
        }

        if (iban) {
            var checkResult = ibansChecked[iban];

            if (!checkResult) {
                e.isValid = false;
                return;
            }

            e.isValid = checkResult.IsValid;
            e.errorText = checkResult.Message;
        }
    }

    function bankValidate(s, e) {
        var val = s.GetValue();
        if (val) {
            e.isValid = true;
        }
        else {
            e.errorText = "Το πεδίο 'Τράπεζα' είναι υποχρεωτικό";
            if (typeof (bankDetailsRequired) !== 'undefined') {
                e.isValid = !bankDetailsRequired;
            }
            else {
                e.isValid = true;
            }
        }
    }
</script>

<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: 195px" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; Στοιχεία Προμηθευτή
        </th>
    </tr>
    <tr>
        <th>Τύπος Επιχείρησης:
        </th>
        <td>
            <dx:ASPxComboBox ID="ddlCompanyType" runat="server" ValueType="System.Int32" OnInit="ddlCompanyType_Init" Width="300px" ClientInstanceName="ddlCompanyType">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τύπος Επιχείρησης' είναι υποχρεωτικό" />
            </dx:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <th>Επωνυμία:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtName" runat="server" CssClass="hint" MaxLength="500" ToolTip="<%$ Resources:StoreInput, Name %>" Width="300px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Επωνυμία' είναι υποχρεωτικό" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Α.Φ.Μ.:
        </th>
        <td>
            <table>
                <tr>
                    <td>
                        <dx:ASPxTextBox ID="txtAFM" ClientInstanceName="txtAFM" runat="server" CssClass="hint" MaxLength="10" OnValidation="txtAFM_Validation" ToolTip="<%$ Resources:StoreInput, AFM %>" Width="100px">
                            <ClientSideEvents KeyUp="Imis.Lib.OnlyDigits" LostFocus="afmChecker" Validation="afmValidate" />
                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Α.Φ.Μ.' είναι υποχρεωτικό." ErrorText="Το 'Α.Φ.Μ.' δεν είναι έγκυρο" />
                        </dx:ASPxTextBox>
                    </td>
                    <td>
                        <div id="afm-checking" class="loader" style="display: none;"></div>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <th>Δ.Ο.Υ.:
        </th>
        <td>
            <dx:ASPxComboBox ID="ddlDOY" runat="server" ValueType="System.String" OnInit="ddlDOY_Init" Width="300px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Δ.Ο.Υ.' είναι υποχρεωτικό" />
            </dx:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <th>Τηλέφωνο (σταθερό):
        </th>
        <td>
            <dx:ASPxTextBox ID="txtStorePhone" runat="server" CssClass="hint" MaxLength="10" ToolTip="<%$ Resources:StoreInput, StorePhone %>" Width="100px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τηλέφωνο (σταθερό)' είναι υποχρεωτικό"
                    RegularExpression-ValidationExpression="^(2[0-9]{9})|(800[0-9]{7})|(801[0-9]{7})|([0-9]{5})|([0-9]{4})$"
                    RegularExpression-ErrorText="Το πεδίο 'Τηλέφωνο (σταθερό)' πρέπει να ξεκινάει από 2 ή 800 ή 801 και να αποτελείται από ακριβώς 10 ψηφία ή να είναι πενταψήφιο ή τετραψήφιο νούμερο" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td class="notRequired">Fax:
        </td>
        <td>
            <dx:ASPxTextBox ID="txtStoreFax" runat="server" CssClass="hint" MaxLength="10" ToolTip="<%$ Resources:StoreInput, StoreFax %>" Width="100px">
                <ValidationSettings RegularExpression-ErrorText="Το πεδίο 'Fax' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Email:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtStoreEmail" runat="server" CssClass="hint" MaxLength="256" ToolTip="<%$ Resources:StoreInput, StoreEmail %>" Width="300px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Email' είναι υποχρεωτικό"
                    RegularExpression-ErrorText="Το Email του Προμηθευτή δεν είναι έγκυρο" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <td class="notRequired">Ιστοσελίδα:
        </td>
        <td>
            <my:HyperLinkInput ID="txtStoreUrl" runat="server" cssclass="hint" Tooltip="<%$ Resources:StoreInput, StoreURL %>" Width="300px" />
        </td>
    </tr>
</table>

<div class="br"></div>

<my:GreekAddressInfoInput ID="ucGreekAddressInfoInput" runat="server" Title="Στοιχεία Διεύθυνσης Έδρας Προμηθευτή" LabelWidth="195px" />

<div class="br"></div>

<dx:ASPxCallbackPanel ID="cbpIdentificationType" runat="server" ClientInstanceName="cbpIdentificationType" OnCallback="cbpIdentificationType_Callback">
    <PanelCollection>
        <dx:PanelContent runat="server">
            <asp:MultiView ID="mvIdentificationType" runat="server">
                <asp:View ID="vID" runat="server">
                    <table class="dv" style="margin-top: 0px; border-top: 0; width: 100%">
                        <colgroup>
                            <col style="width: 195px" />
                        </colgroup>
                        <tr>
                            <th style="margin-top: 0px; border-top: 0;">Αριθμός Ταυτότητας:
                            </th>
                            <td style="margin-top: 0px; border-top: 0;">
                                <dx:ASPxTextBox ID="txtIDNumber" runat="server" MaxLength="8" Width="300px">
                                    <ClientSideEvents KeyUp="Imis.Lib.StudentNumberTransformation" />
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Αριθμός Ταυτότητας' είναι υποχρεωτικό"
                                        RegularExpression-ErrorText="Ο Αριθμός Ταυτότητας πρέπει να ξεκινάει από 1-2 κεφαλαία ελληνικά γράμματα και να ακολουθείται από 6 ψηφία" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Αρχή Έκδοσης:
                            </th>
                            <td>
                                <dx:ASPxTextBox ID="txtLegalPersonIdentificationIssuer" runat="server" MaxLength="200" Width="300px">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Αρχή Έκδοσης' είναι υποχρεωτικό" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>Ημ/νία Έκδοσης:
                            </th>
                            <td>
                                <dx:ASPxDateEdit ID="txtLegalPersonIdentificationIssueDate" runat="server" Width="100px">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Ημ/νία Έκδοσης' είναι υποχρεωτικό" />
                                </dx:ASPxDateEdit>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vPassport" runat="server">
                    <table class="dv" style="margin-top: 0px; border-top: 0; width: 100%">
                        <colgroup>
                            <col style="width: 195px" />
                        </colgroup>
                        <tr>
                            <th style="margin-top: 0px; border-top: 0;">Αριθμός Διαβατηρίου / Στρατιωτικής Ταυτότητας:
                            </th>
                            <td style="margin-top: 0px; border-top: 0;">
                                <dx:ASPxTextBox ID="txtPassportNumber" runat="server" MaxLength="50" Width="300px">
                                    <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Αριθμός Διαβατηρίου / Στρατιωτικής Ταυτότητας' είναι υποχρεωτικό" />
                                </dx:ASPxTextBox>
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </dx:PanelContent>
    </PanelCollection>
</dx:ASPxCallbackPanel>

<div class="br"></div>

<my:ContactPersonInput ID="ucContactPersonInput" runat="server" Title="Στοιχεία Υπευθύνου του Προμηθευτή για τη δράση" LabelWidth="195px" />

<div class="br"></div>

<asp:PlaceHolder ID="phBankDetails" runat="server" Visible="false">
    <div class="br"></div>

    <table class="dv" style="width: 100%">
        <colgroup>
            <col style="width: 195px" />
        </colgroup>
        <tr>
            <th colspan="2" class="header">&raquo; Στοιχεία Τραπεζικού Λογαριασμού Προμηθευτή
            </th>
        </tr>
        <tr>
            <th>Τράπεζα:
            </th>
            <td>
                <dx:ASPxComboBox ID="ddlBank" runat="server" ValueType="System.Int32" OnInit="ddlBank_Init" Width="300px">
                    <ClientSideEvents Validation="bankValidate" />
                </dx:ASPxComboBox>
            </td>
        </tr>
        <tr>
            <th>Αρ. IBAN:
                <my:TipIcon runat="server" Text="Το IBAN της τράπεζας πρέπει να αποτελείται από 27 χαρακτήρες, χωρίς κενά και να αρχίζει από GR" />
            </th>
            <td>
                <table>
                    <tr>
                        <td>
                            <dx:ASPxTextBox ID="txtIBAN" runat="server" ClientInstanceName="txtIBAN" MaxLength="27" OnValidation="txtIBAN_Validation" Width="300px">
                                <ClientSideEvents GotFocus="dx_Focus" LostFocus="ibanChecker" Validation="ibanValidate" />
                            </dx:ASPxTextBox>
                        </td>
                        <td>
                            <div id="iban-checking" class="loader" style="display: none;"></div>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:PlaceHolder>
