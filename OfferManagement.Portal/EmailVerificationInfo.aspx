<%@ Page Language="C#" MasterPageFile="~/PopUp.Master" AutoEventWireup="true" CodeBehind="EmailVerificationInfo.aspx.cs"
    Inherits="OfferManagement.Portal.EmailVerificationInfo" Title="Οδηγίες Επιβεβαίωσης Email" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="server">
    <div class="reminder" style="text-align: left; font-weight: normal;">
        Κατά την εγγραφή σας στην εφαρμογή, σας στάλθηκε email επιβεβαίωσης με τίτλο:
        <br />
        <br />
        <b><asp:Literal ID="ltSubject" runat="server" /></b>
        <br />
        <br />
        Εάν το έχετε λάβει , πατήστε το link που έχει στο κείμενό του, ώστε να επιβεβαιώσετε
        το email του λογαριασμού σας.
        <br />
        <br />
        Εάν δεν το έχετε λάβει μπορεί να έχουν συμβεί τα
        εξής:
        <ul>
            <li class="firstListItem"><asp:Literal ID="ltFirstBullet" runat="server" /></li>
            <li class="firstListItem"><asp:Literal ID="ltSecondBullet" runat="server" /></li>
        </ul>
    </div>
</asp:Content>
