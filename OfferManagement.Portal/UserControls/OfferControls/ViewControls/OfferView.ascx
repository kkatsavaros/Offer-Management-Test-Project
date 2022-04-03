<%@ Control Inherits="OfferManagement.Portal.UserControls.OfferControls.ViewControls.OfferView" CodeBehind="OfferView.ascx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagName="OfferGeneralInfoView" TagPrefix="my" Src="~/UserControls/OfferControls/ViewControls/OfferGeneralInfoView.ascx" %>
<%@ Register TagName="LaptopOfferView" TagPrefix="my" Src="~/UserControls/OfferControls/ViewControls/LaptopOfferView.ascx" %>

<my:OfferGeneralInfoView ID="ucOfferGeneralInfoView" runat="server" />

<div class="br"></div>

<my:LaptopOfferView ID="ucLaptopOfferView" runat="server" />

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
