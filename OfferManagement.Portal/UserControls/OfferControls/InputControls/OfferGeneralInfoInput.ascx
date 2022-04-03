<%@ Control Inherits="OfferManagement.Portal.UserControls.OfferControls.InputControls.OfferGeneralInfoInput" CodeBehind="OfferGeneralInfoInput.ascx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagName="HyperLinkInput" TagPrefix="my" Src="~/UserControls/GenericControls/HyperLinkInput.ascx" %>

<script type="text/javascript">
    var existingCodes = [];

    $(function () {
        var str = $('#hfExistingCodes').val();
        var splitted = str.split('|');

        for (var i = 0; i < splitted.length; i++) {
            existingCodes.push(splitted[i]);
        }
    });

    function validateOfferCode(s, e) {
        var code = s.GetValue();

        if (!code) {
            e.isValid = false;
            e.errorText = "Το πεδίο 'Κωδικός' είναι υποχρεωτικό";
        }
        else {
            var regexp = /^([A-Za-z0-9_\-\.\/]){1,60}$/;
            if (!regexp.test(code)) {
                e.isValid = false;
                e.errorText = "Ο κωδικός προσφοράς μπορεί να αποτελείται μόνο από λατινικούς χαρακτήρες (μικρούς και κεφαλαίους), αριθμούς και τους ειδικούς χαρακτήρες (.-_/). Μέγιστο μήκος 60 χαρακτήρες.";
                return;
            }

            e.isValid = true;
            code = code.toLowerCase();
            for (var i = 0; i < existingCodes.length; i++) {
                if (code == existingCodes[i].toLowerCase()) {
                    e.isValid = false;
                    e.errorText = "Υπάρχει ήδη προσφορά με τον κωδικό που εισάγατε. Ο κωδικός πρέπει να είναι μοναδικός για κάθε προσφορά σας.";
                    break;
                }
            }
        }
    }
</script>

<asp:HiddenField ID="hfExistingCodes" runat="server" ClientIDMode="Static" />
<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: 250px" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; Γενικά Στοιχεία Προσφοράς
        </th>
    </tr>
    <tr>
        <th>Κωδικός:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtCode" runat="server" MaxLength="60" Width="500px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Code %>" OnValidation="txtOfferCode_Validation">
                <ClientSideEvents Validation="validateOfferCode" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Τίτλος:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtTitle" runat="server" MaxLength="100" Width="500px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Title %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τίτλος' είναι υποχρεωτικό" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Περιγραφή:
        </th>
        <td>
            <dx:ASPxMemo ID="txtDescription" runat="server" Rows="5" MaxLength="5000" Width="500px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Description %>" />
        </td>
    </tr>
    <tr>
        <th>Διατίθεται τσάντα μεταφοράς:</th>
        <td>
            <dx:ASPxComboBox ID="ddlIsLaptopCaseIncluded" runat="server" NumberType="Integer" Width="300px" OnInit="ddlIsLaptopCaseIncluded_Init">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Παρακαλώ επιλέξτε αν διατίθεται τσάντα μεταφοράς." />
            </dx:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <th>Link στη σελίδα του Προμηθευτή με αναλυτική περιγραφή της Προσφοράς:
        </th>
        <td>
            <my:HyperLinkInput ID="txtOfferUrl" runat="server" Width="500px" IsRequired="false" Label="Link στη σελίδα του Προμηθευτή με αναλυτική περιγραφή της Προσφοράς" Tooltip="<%$ Resources:OfferInput, OfferUrl %>" />
        </td>
    </tr>
    <tr>
        <th>Τιμή:
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtPrice" runat="server" CssClass="smallField hint" DisplayFormatString="C" NumberType="Float" MinValue="1" MaxValue="9999" DecimalPlaces="2" ToolTip="<%$ Resources:OfferInput, Price %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τιμή' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
</table>
