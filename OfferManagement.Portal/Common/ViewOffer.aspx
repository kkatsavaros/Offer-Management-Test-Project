<%@ Page Title="Προβολή Προσφοράς" MasterPageFile="~/PopUp.Master" Language="C#" AutoEventWireup="true" Inherits="OfferManagement.Portal.Common.ViewOffer" CodeBehind="ViewOffer.aspx.cs" %>

<%@ Register TagName="OfferView" TagPrefix="my" Src="~/UserControls/OfferControls/ViewControls/OfferView.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="cphMain">

    <asp:MultiView ID="mvOffer" runat="server" ActiveViewIndex="0">
        
        <asp:View ID="vOfferDetailsCompleted" runat="server">
            <my:OfferView ID="ucOfferView" runat="server" />
        </asp:View>

        <asp:View ID="vOfferDetailsNotCompleted" runat="server">
            <div class="reminder">
                Τα στοιχεία της προσφοράς δεν έχουν συμπληρωθεί.
            </div>
        </asp:View>

    </asp:MultiView>

</asp:Content>
