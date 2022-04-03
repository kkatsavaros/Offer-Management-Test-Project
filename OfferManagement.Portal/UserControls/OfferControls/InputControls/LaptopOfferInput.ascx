<%@ Control Inherits="OfferManagement.Portal.UserControls.OfferControls.InputControls.LaptopOfferInput" CodeBehind="LaptopOfferInput.ascx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagName="HyperLinkInput" TagPrefix="my" Src="~/UserControls/GenericControls/HyperLinkInput.ascx" %>
<%@ Register TagName="AutoCompleteInput" TagPrefix="my" Src="~/UserControls/GenericControls/AutoCompleteInput.ascx" %>
<%@ Register TagName="TipIcon" TagPrefix="my" Src="~/UserControls/GenericControls/TipIcon.ascx" %>

<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: 250px" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; Στοιχεία Laptop/Notebook/Netbook
        </th>
    </tr>
    <tr>
        <th>Κατασκευαστής:
        </th>
        <td>
            <my:AutoCompleteInput ID="ucManufacturer" runat="server" MaxLength="500" Width="300px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Laptop_Manufacturer %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Κατασκευαστής' είναι υποχρεωτικό" />
            </my:AutoCompleteInput>
        </td>
    </tr>
    <tr>
        <th>Μοντέλο:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtModel" runat="server" MaxLength="500" Width="300px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Laptop_Model %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Μοντέλο' είναι υποχρεωτικό" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Επεξεργαστής:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtCpu" runat="server" MaxLength="500" Width="300px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Laptop_Cpu %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Επεξεργαστής' είναι υποχρεωτικό" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Ταχύτητα Χρονισμού (GHz) (>= 1.7GHz):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtCpuSpeed" runat="server" CssClass="smallField hint" NumberType="Float" MinValue="1" MaxValue="99" DecimalPlaces="2" ToolTip="<%$ Resources:OfferInput, Laptop_CpuSpeed %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Ταχύτητα Χρονισμού' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Μνήμη RAM (>= 4GB):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtRamSize" runat="server" NumberType="Float" CssClass="smallField hint" MinValue="1" MaxValue="99" DecimalPlaces="1" ToolTip="<%$ Resources:OfferInput, Laptop_RamSize %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Μνήμη RAM' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Σκληρός Δίσκος (>= 500GB):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtStorageSize" runat="server" NumberType="Integer" CssClass="smallField hint" MinValue="1" MaxValue="9999" ToolTip="<%$ Resources:OfferInput, Laptop_StorageSize %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Σκληρός Δίσκος' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Θύρες USB (>= 3):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtUsbCount" runat="server" NumberType="Float" CssClass="smallField hint" MinValue="1" MaxValue="10" DecimalPlaces="0" ToolTip="<%$ Resources:OfferInput, Laptop_UsbCount %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Θύρες USB' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Θύρες HDMI (>= 1):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtHdmiCount" runat="server" NumberType="Float" CssClass="smallField hint" MinValue="0" MaxValue="10" DecimalPlaces="0" ToolTip="<%$ Resources:OfferInput, Laptop_HdmiCount %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Θύρες HDMI' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Οθόνη Διαστάσεις (>= 15.6"):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtScreenSize" runat="server" CssClass="smallField hint" NumberType="Float" MinValue="1" MaxValue="99" DecimalPlaces="1" ToolTip="<%$ Resources:OfferInput, Laptop_ScreenSize %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Διαγώνιος Οθόνης' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Οθόνη Ανάλυση (>= 1366x768):
        </th>
        <td>
            <table>
                <tr>
                    <td>
                        <dx:ASPxSpinEdit ID="txtScreenResolutionX" runat="server" NumberType="Integer" CssClass="smallField hint" MinValue="1" MaxValue="9999" ToolTip="<%$ Resources:OfferInput, Laptop_ScreenResolutionX %>">
                            <ValidationSettings Display="Dynamic" RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Ανάλυση Οθόνης X' είναι υποχρεωτικό" />
                        </dx:ASPxSpinEdit>
                    </td>
                    <td>x</td>
                    <td>
                        <dx:ASPxSpinEdit ID="txtScreenResolutionY" Display="Dynamic" runat="server" NumberType="Integer" CssClass="smallField hint" MinValue="1" MaxValue="9999" ToolTip="<%$ Resources:OfferInput, Laptop_ScreenResolutionY %>">
                            <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Ανάλυση Οθόνης Y' είναι υποχρεωτικό" />
                        </dx:ASPxSpinEdit>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <th>Χρώμα Συσκευής:
            <my:TipIcon runat="server" Text="Αποκλείονται λευκά , κίτρινα, κόκκινα, μώβ, μπλέ και παραλλαγές αυτών των χρωμάτων." />
        </th>
        <td>
            <dx:ASPxCheckBoxList ID="cbxlLaptopColor" runat="server" ValueType="System.Int32" RepeatDirection="Horizontal">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Χρώμα Συσκευής' είναι υποχρεωτικό" />
            </dx:ASPxCheckBoxList>
        </td>
    </tr>
    <tr>
        <th>Λειτουργικό Σύστημα:
        </th>
        <td>
            <dx:ASPxTextBox ID="txtOperatingSystem" runat="server" MaxLength="500" Width="300px" CssClass="hint" ToolTip="<%$ Resources:OfferInput, Laptop_OperatingSystem %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Λειτουργικό Σύστημα' είναι υποχρεωτικό" />
            </dx:ASPxTextBox>
        </td>
    </tr>
    <tr>
        <th>Εγγύηση (>= 2 έτη):
        </th>
        <td>
            <dx:ASPxSpinEdit ID="txtGuaranteeYears" runat="server" NumberType="Integer" CssClass="smallField hint" MinValue="0" MaxValue="99" ToolTip="<%$ Resources:OfferInput, Laptop_Guarantee %>">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Το πεδίο 'Εγγύηση' είναι υποχρεωτικό" />
            </dx:ASPxSpinEdit>
        </td>
    </tr>
    <tr>
        <th>Το ασύρματο δίκτυο υποστηρίζει Wi-Fi 802.11 ac:</th>
        <td>
            <dx:ASPxComboBox ID="ddlIsWifi80211acCompliant" runat="server" NumberType="Integer" Width="300px" OnInit="ddlIsWifi80211acCompliant_Init">
                <ValidationSettings RequiredField-IsRequired="true" RequiredField-ErrorText="Παρακαλώ επιλέξτε αν το ασύρματο δίκτυο υποστηρίζει το 802.11 ac πρότυπο." />
            </dx:ASPxComboBox>
        </td>
    </tr>
    <tr>
        <th>Ιστοσελίδα κατασκευαστή με Official Specs ή datasheet της συσκευής:
        </th>
        <td>
            <my:HyperLinkInput ID="txtOfficialSpecsUrl" runat="server" Width="300px" IsRequired="true" Label="Ιστοσελίδα κατασκευαστή με Official Specs της συσκευής" Tooltip="<%$ Resources:OfferInput, Laptop_OfficialSpecsUrl %>" />
        </td>
    </tr>
</table>


