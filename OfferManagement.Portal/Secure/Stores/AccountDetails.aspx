<%@ Page Title="Στοιχεία Λογαριασμού" Language="C#" MasterPageFile="~/Secure/Stores/Stores.master" AutoEventWireup="true" CodeBehind="AccountDetails.aspx.cs" Inherits="OfferManagement.Portal.Secure.Stores.AccountDetails" %>

<%@ Register TagPrefix="my" TagName="UserAccountDetails" Src="~/UserControls/GenericControls/UserAccountDetails.ascx" %>

<asp:Content runat="server" ContentPlaceHolderID="cphSecureMain">

    <asp:ScriptManagerProxy runat="server">
        <CompositeScript>
            <Scripts>
                <asp:ScriptReference Path="~/_js/Views/UserContactDetails.js" />
            </Scripts>
        </CompositeScript>
    </asp:ScriptManagerProxy>

    <my:UserAccountDetails ID="ucAccountDetails" runat="server" ShowMobilePhone="false" Width="800px" />
    
</asp:Content>
