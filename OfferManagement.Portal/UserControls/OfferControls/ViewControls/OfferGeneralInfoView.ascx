<%@ Control Inherits="OfferManagement.Portal.UserControls.OfferControls.ViewControls.OfferGeneralInfoView" CodeBehind="OfferGeneralInfoView.ascx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagName="HyperLinkInput" TagPrefix="my" Src="~/UserControls/GenericControls/HyperLinkInput.ascx" %>

<table class="dv" style="width: 100%">
    <colgroup>
        <col style="width: 195px" />
    </colgroup>
    <tr>
        <th colspan="2" class="header">&raquo; Γενικά Στοιχεία Προσφοράς
        </th>
    </tr>
    <tr>
        <th>Κωδικός:
        </th>
        <td>
            <dx:ASPxLabel ID="lblCode" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Τίτλος:
        </th>
        <td>
            <dx:ASPxLabel ID="lblTitle" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Περιγραφή:
        </th>
        <td>
            <div class="memo">
                <pre><asp:Literal ID="lblDescription" runat="server" /></pre>
            </div>
        </td>
    </tr>
    <tr>
        <th>Διατίθεται τσάντα μεταφοράς:
        </th>
        <td>
            <dx:ASPxLabel ID="lblIsLaptopCaseIncluded" runat="server" />
        </td>
    </tr>
    <tr>
        <th>Link στη σελίδα του Προμηθευτή με αναλυτική περιγραφή της Προσφοράς:
        </th>
        <td>
            <my:HyperLinkInput ID="txtOfferUrl" runat="server" ReadOnly="true" />
        </td>
    </tr>
    <tr>
        <th>Τιμή:
        </th>
        <td>
            <dx:ASPxLabel ID="lblPrice" runat="server" />
        </td>
    </tr>
</table>
