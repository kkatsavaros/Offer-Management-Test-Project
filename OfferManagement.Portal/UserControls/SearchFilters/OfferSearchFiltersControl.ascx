<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OfferSearchFiltersControl.ascx.cs" Inherits="OfferManagement.Portal.UserControls.SearchFilters.OfferSearchFiltersControl" %>

<%@ Register TagName="MultiSelectCombo" TagPrefix="my" Src="~/Controls/ScriptControls/MultiSelectCombo.ascx" %>
<%@ Register TagName="NumberRangeInput" TagPrefix="my" Src="~/UserControls/GenericControls/NumberRangeInput.ascx" %>
<%@ Register TagName="DateRangeInput" TagPrefix="my" Src="~/UserControls/GenericControls/DateRangeInput.ascx" %>

<table id="tbFilters" runat="server" cellspacing="2">
    <tr>
        <td style="vertical-align: top">
            <table class="dv">
                <colgroup>
                    <col style="width: 85px" />
                    <col style="width: 290px" />
                    <col style="width: 140px" />
                    <col style="width: 292px" />
                </colgroup>
                <tr>
                    <th colspan="4" class="popupHeader">Φίλτρα Αναζήτησης
                    </th>
                </tr>
                <tr>
                    <th>Κατάσταση:
                    </th>
                    <td colspan="3">
                        <dx:ASPxCheckBoxList ID="chklOfferStatuses" runat="server" RepeatDirection="Horizontal" ValueType="System.Int32" />
                    </td>
                </tr>
                <tr>
                    <th>Κωδικός:
                    </th>
                    <td>
                        <dx:ASPxTextBox ID="txtCode" runat="server" />
                    </td>
                    <th>Ημ/νία Υποβολής:
                    </th>
                    <td>
                        <my:DateRangeInput ID="ucSubmissionDate" runat="server" />
                    </td>
                </tr>



                <tr>
                    <th>Τίτλος:
                    </th>
                    <td>
                        <dx:ASPxTextBox ID="txtTitle" runat="server" />
                    </td>
                    <th>Ημ/νία Έγκρισης:
                    </th>
                    <td>
                        <my:DateRangeInput ID="ucEvaluationDate" runat="server" />
                    </td>
                </tr>



                 <tr>
                    <th>Description Kostas:
                    </th>
                    <td>
                        <dx:ASPxTextBox ID="txtDescription" runat="server" />
                    </td>                  
                </tr>

            </table>
        </td>

















        <td id="tdHelpdeskFilters" runat="server" style="vertical-align: top">
            <table class="dv">
                <colgroup>
                    <col style="width: 120px" />
                    <col style="width: 200px" />
                </colgroup>
                <tr>
                    <th colspan="4" class="popupHeader">Φίλτρα Αξιολόγησης
                    </th>
                </tr>
                <tr>
                    <th>ID Προσφοράς:
                    </th>
                    <td>
                        <my:NumberRangeInput ID="ucOfferID" runat="server" />
                    </td>
                    <th>Προσφορά Franchisor:
                    </th>
                    <td>
                        <dx:ASPxComboBox ID="ddlIsFranchisorOffer" runat="server" Width="100%" OnInit="ddlIsFranchisorOffer_Init" />
                    </td>
                </tr>
                <tr>
                    <th>Επωνυμία:
                    </th>
                    <td>
                        <dx:ASPxTextBox ID="txtStoreName" runat="server" />
                    </td>
                    <th>Έχει Σχόλια:
                    </th>
                    <td>
                        <dx:ASPxComboBox ID="ddlHasEvaluationComments" runat="server" Width="100%" OnInit="ddlHasEvaluationComments_Init" />
                    </td>
                </tr>













                <tr>
                    <th>Α.Φ.Μ.:
                    </th>
                    <td>
                        <dx:ASPxTextBox ID="txtAFM" runat="server" />
                    </td>
                    <th>Τελευταίος Αξιολογητής:
                    </th>
                    <td>
                        <dx:ASPxComboBox ID="ddlLastEvaluatedBy" runat="server" Width="100%" />
                    </td>
                </tr>
                <tr>
                    <th>Δημοσιευμένη:
                    </th>
                    <td>
                        <dx:ASPxComboBox ID="ddlIsPublished" runat="server" Width="100%" OnInit="ddlIsPublished_Init" />
                    </td>
                    <th>Απόκρυψη Κλωνοποιημένων:
                    </th>
                    <td>
                        <dx:ASPxCheckBox ID="chbxHideClonedOffers" runat="server" />
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
