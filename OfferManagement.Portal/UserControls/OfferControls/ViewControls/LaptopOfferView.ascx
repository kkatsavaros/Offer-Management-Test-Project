<%@ Control Inherits="OfferManagement.Portal.UserControls.OfferControls.ViewControls.LaptopOfferView" CodeBehind="LaptopOfferView.ascx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagName="HyperLinkInput" TagPrefix="my" Src="~/UserControls/GenericControls/HyperLinkInput.ascx" %>

<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: 195px" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; Στοιχεία Laptop
        </th>
    </tr>
    <tr>
        <th>Κατασκευαστής:
        </th>
        <td>
            <dx:ASPxLabel ID="lblManufacturer" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Μοντέλο:
        </th>
        <td>
            <dx:ASPxLabel ID="lblModel" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Επεξεργαστής:
        </th>
        <td>
            <dx:ASPxLabel ID="lblCpu" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Ταχύτητα Χρονισμού:
        </th>
        <td>
            <dx:ASPxLabel ID="lblCpuSpeed" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Μνήμη RAM:
        </th>
        <td>
            <dx:ASPxLabel ID="lblRamSize" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Σκληρός Δίσκος:
        </th>
        <td>
            <dx:ASPxLabel ID="lblStorageSize" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Θύρες USB:
        </th>
        <td>
            <dx:ASPxLabel ID="lblUsb" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Θύρες HDMI:
        </th>
        <td>
            <dx:ASPxLabel ID="lblHdmi" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Διαστάσεις Οθόνης:
        </th>
        <td>
            <dx:ASPxLabel ID="lblScreenSize" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Ανάλυση Οθόνης:
        </th>
        <td>
            <dx:ASPxLabel ID="lblScreenResolution" runat="server" />
        </td>
    </tr>
    <tr>
        <th>
            Χρώμα Συσκευής:
        </th>
        <td>
            <dx:ASPxLabel ID="lblColor" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Λειτουργικό Σύστημα:
        </th>
        <td>
            <dx:ASPxLabel ID="lblOperatingSystem" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Εγγύηση:
        </th>
        <td>
            <dx:ASPxLabel ID="lblGuaranteeYears" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Το ασύρματο δίκτυο υποστηρίζει Wi-Fi 802.11 ac:
        </th>
        <td>
            <dx:ASPxLabel ID="lblIsWiFi80211acCompliant" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Ιστοσελίδα κατασκευαστή με Official Specs ή datasheet της συσκευής:
        </th>
        <td>
            <my:HyperLinkInput ID="txtOfficialSpecsUrl" runat="server" ReadOnly="true" />
        </td>
    </tr>
</table>
