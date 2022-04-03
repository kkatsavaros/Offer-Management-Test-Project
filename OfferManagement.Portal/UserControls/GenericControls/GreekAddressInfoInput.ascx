<%@ Control Inherits="OfferManagement.Portal.UserControls.GenericControls.GreekAddressInfoInput" CodeBehind="GreekAddressInfoInput.ascx.cs" Language="C#" AutoEventWireup="true" %>

<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: <%= LabelWidth %>" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; <%= Title %>
        </th>
    </tr>
    <tr>
        <th>Οδός - Αριθμός:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtAddressName" runat="server" ClientInstanceName="txtAddressName" MaxLength="200" Width="250px">
                <ClientSideEvents KeyUp="Imis.Lib.ToElUpper" />
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Οδός - Αριθμός' είναι υποχρεωτικό"
                    RegularExpression-ErrorText="Το πεδίο 'Οδός - Αριθμός' μπορεί να περιέχει μόνο κεφαλαία ελληνικά γράμματα ή αριθμούς και τους εξής ειδικούς χαρακτήρες (-_,.()/&). Μέγιστο μήκος 200 χαρακτήρες." />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Τ.Κ.:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtZipCode" runat="server" ClientInstanceName="txtZipCode" MaxLength="5" Width="100px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τ.Κ.' είναι υποχρεωτικό"
                    RegularExpression-ErrorText="Ο Τ.Κ. πρέπει να αποτελείται από ακριβώς 5 ψηφία" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Περιφερειακή Ενότητα:
        </th>
        <td>
            <dx:ASPxComboBox ID="ddlPrefecture" runat="server" ClientInstanceName="ddlPrefecture" ValueType="System.Int32" OnInit="ddlPrefecture_Init" Width="250px">
                <ClientSideEvents SelectedIndexChanged="function(s,e) { FillCities(s, e, '-- επιλέξτε καλλικρατικό δήμο --') }" />
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Περιφερειακή Ενότητα' είναι υποχρεωτικό" />
            </dx:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <th>Καλλικρατικός Δήμος:
        </th>
        <td>
            <dx:ASPxComboBox ID="ddlCity" runat="server" ClientInstanceName="ddlCity" ValueType="System.Int32" Width="250px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Καλλικρατικός Δήμος' είναι υποχρεωτικό" />
            </dx:ASPxComboBox>
        </td>
    </tr>
</table>

