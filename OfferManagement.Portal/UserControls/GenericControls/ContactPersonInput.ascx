<%@ Control Inherits="OfferManagement.Portal.UserControls.GenericControls.ContactPersonInput" CodeBehind="ContactPersonInput.ascx.cs" Language="C#" AutoEventWireup="true" %>

<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: <%= LabelWidth %>" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; <%= Title %>
        </th>
    </tr>
    <tr>
        <th>Ονοματεπώνυμο:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtContactName" runat="server" CssClass="hint" MaxLength="100" ToolTip="<%$ Resources:ContactPersonInput, ContactName %>" Width="300px">
                <ClientSideEvents KeyUp="Imis.Lib.ToUpperForNames" />
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Ονοματεπώνυμο του Υπευθύνου για τη δράση' είναι υποχρεωτικό"
                    RegularExpression-ErrorText="Το πεδίο 'Ονοματεπώνυμο' πρέπει να περιέχει κεφαλαία γράμματα, χωρίς ειδικούς χαρακτήρες (π.χ. παύλα, κόμμα κλπ)" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Τηλέφωνο (σταθερό):
        </th>
        <td>
            <dx:ASPxTextBox ID="txtContactPhone" runat="server" CssClass="hint" MaxLength="10" ToolTip="<%$ Resources:ContactPersonInput, ContactPhone %>" Width="100px">                
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για τη δράση' είναι υποχρεωτικό" 
                    RegularExpression-ErrorText="Το πεδίο 'Τηλέφωνο (σταθερό) του Υπευθύνου για τη δράση' πρέπει να ξεκινάει από 2 και να αποτελείται από ακριβώς 10 ψηφία" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Τηλέφωνο (κινητό):
        </th>
        <td>
            <dx:ASPxTextBox ID="txtContactMobilePhone" runat="server" CssClass="hint" MaxLength="10" ToolTip="<%$ Resources:ContactPersonInput, ContactMobilePhone %>" Width="100px">                
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για τη δράση' είναι υποχρεωτικό" 
                    RegularExpression-ErrorText="Το πεδίο 'Τηλέφωνο (κινητό) του Υπευθύνου για τη δράση' πρέπει να ξεκινάει από 69 και να αποτελείται από ακριβώς 10 ψηφία" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Email:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtContactEmail" runat="server" CssClass="hint" MaxLength="256" ToolTip="<%$ Resources:ContactPersonInput, ContactEmail %>" Width="300px">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Email του Υπευθύνου για τη δράση' είναι υποχρεωτικό" 
                    RegularExpression-ErrorText="Το Email του Υπευθύνου για τη δράση δεν είναι έγκυρο" />
            </dx:ASPxTextBox>
        </td>
    </tr>
</table>
