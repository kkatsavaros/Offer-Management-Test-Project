<%@ Page Title="Επεξήγηση Χρωμάτων" MasterPageFile="~/PopUp.Master" Inherits="OfferManagement.Portal.Secure.Stores.ColorExplanation" CodeBehind="ColorExplanation.aspx.cs" Language="C#" AutoEventWireup="true" %>

<asp:Content ContentPlaceHolderID="cphMain" runat="server">
    <table width="450px" class="dv">
        <tr>
            <td style="background-color: #FFFFFF">Σε επεξεργασία
            </td>
        </tr>        
        <tr>
            <td style="background-color: #ADD8E6">Υποβεβλημένη (Μη Δημοσιευμένη)
            </td>
        </tr>
        <tr>
            <td style="background-color: #90EE90">Υποβεβλημένη (Δημοσιευμένη)
            </td>
        </tr>
        <tr>
            <td style="background-color: #FF9933">Υποβεβλημένη (Ανενεργή)
            </td>
        </tr>        
    </table>
</asp:Content>
