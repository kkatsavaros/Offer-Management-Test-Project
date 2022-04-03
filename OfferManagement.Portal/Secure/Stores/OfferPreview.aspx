<%@ Page Title="Προεπισκόπιση Προσφοράς" MasterPageFile="~/Secure/Stores/Stores.Master" Inherits="OfferManagement.Portal.Secure.Providers.OfferPreview" CodeBehind="OfferPreview.aspx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagName="OfferView" TagPrefix="my" Src="~/UserControls/OfferControls/ViewControls/OfferView.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="cphSecureMain">

    <my:OfferView ID="ucOfferView" runat="server" />

    <div class="br"></div>

    <table>
        <tr>
            <td>
                <dx:ASPxButton ID="btnSave" runat="server" Image-Url="~/_img/iconSave.png" Text="Αποθήκευση" OnClick="btnSave_Click" />
            </td>
            <td>
                <dx:ASPxButton ID="btnSaveAndSubmit" runat="server" Image-Url="~/_img/iconAccept.png" Text="Αποθήκευση & Υποβολή" OnClick="btnSaveAndSubmit_Click" />
            </td>
            <td>
                <dx:ASPxButton ID="btnCancel" runat="server" Image-Url="~/_img/iconCancel.png" Text="Προηγούμενο Βήμα" CausesValidation="false" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>
    
</asp:Content>
