<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfferRequirementsView.ascx.cs" Inherits="OfferManagement.Portal.UserControls.OfferControls.ViewControls.OfferRequirementsView" %>

<style type="text/css">
    .requirementsList {
        margin: auto !important;
        padding: 5px 0px 5px 25px !important;
    }

        .requirementsList li {
            list-style: disc !important;
            margin: auto !important;
            padding: 2px !important;
        }

    .requirementsCell {
        font-size: 1.1em !important;
        font-weight: normal !important;
    }

        .requirementsCell p {
            font-weight: bold;
            margin-top: 0px;
        }
</style>

<table id="tbl" runat="server" class="dv" style="width: 100%">
    <tr>
        <th class="header">&raquo; Υποχρεωτικά στοιχεία προσφορών</th>
    </tr>
    <tr>
        <th class="requirementsCell">
            <p>Δηλώνω υπεύθυνα ότι ισχύουν τα εξής για την συσκευή:</p>
            <ul class="requirementsList">
                <li>Το laptop θα πρέπει υποχρεωτικά να είναι σκούρου χρώματος.</li>
                <li>Θα πρέπει να δίνεται σκουρόχρωμη τσάντα μεταφοράς.</li>
            </ul>
        </th>
    </tr>
        <tr>
        <th class="requirementsCell">
            <p>Δηλώνω υπεύθυνα ότι ισχύουν οι παρακάτω ελάχιστες προδιαγραφές για τον επεξεργαστή του laptop:</p>
            <ul>
                <li>ΙNTEL i3 , 2 cores , σειρά 4005U, Ταχύτητα χρονισμού >= 1,7 GΗΖ ή</li>
                <li>AMD , σειρά   A6 6310, Ταχύτητα χρονισμού >= 1,7 GΗΖ ή </li>
                <li>ισοδύναμος επεξεργαστής άλλου κατασκευαστή</li>
            </ul>
        </th>
    </tr>
</table>
