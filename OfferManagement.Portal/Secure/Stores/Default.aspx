<%@ Page Title="Κεντρική Σελίδα" Language="C#" MasterPageFile="~/Secure/Stores/Stores.master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="OfferManagement.Portal.Secure.Stores.Default" %>

<asp:Content ContentPlaceHolderID="cphSecureMain" runat="server">
    <div class="rp" id="rpCertificationDetails">
        <div class="rp-header">
            <div class="rp-headertext">Σχετικά με την ένταξή σας στο Μητρώο Προμηθευτών</div>
            <a href="#" class="collapsibleTrigger img-btn-noborder tooltip rp-toggle" title="Εμφάνιση / Απόκρυψη">
                <img id="Img3" runat="server" src="~/_img/iconMinimize.png" alt="Εμφάνιση / Απόκρυψη" /></a>
            <div class="rp-clr"></div>
        </div>
        <div class="rp-main">
            <div class="collapsibleHidden" style="text-align: center;">
                <span style="color: #5C5C5C">Πατήστε <a href="#" class="collapsibleTrigger">εδώ</a> για να δείτε τα περιεχόμενα</span>
            </div>
            <div class="collapsibleContainer">
                <asp:multiview id="mvVerificationStatus" runat="server">

                        <asp:View ID="vNoSubmittedRequest" runat="server">
                            <div class="reminder">Δεν έχετε ακόμη υποβάλει Αίτηση Συμμετοχής στο Μητρώο Προμηθευτών.</div>
                            <div class="br"></div>
                            <div class="br"></div>
                            Για να υποβάλετε Αίτηση Συμμετοχής στο Μητρώο Προμηθευτών της Δράσης, θα πρέπει στην καρτέλα <a runat="server" href="~/Secure/Stores/StoreDetails.aspx">Στοιχεία Προμηθευτή</a> να συμπληρώσετε όλα τα υποχρεωτικά πεδία και να επιλέξετε <b>Υποβολή Αίτησης Συμμετοχής στο Μητρώο</b>.
                            <div class="br"></div>
                            <div class="br"></div>
                            Αμέσως μετά θα πρέπει να πατήσετε <b>Εκτύπωση Βεβαίωσης Συμμετοχής</b>, την οποία θα πρέπει να αποστείλετε υπογεγραμμένη από το νόμιμο εκπρόσωπο και σφραγισμένη με τη σφραγίδα της επιχείρησης στον αριθμό fax που θα υποδεικνύεται στη βεβαίωση συμμετοχής, μαζί με ένα αντίγραφο της αστυνομικής ταυτότητας ή του διαβατηρίου του νόμιμου εκπροσώπου.
                            <div class="br"></div>
                            <div class="br"></div>
                            Μέχρι να υποβάλετε Αίτηση και να εγκριθεί η Ένταξή σας στο Μητρώο Προμηθευτών, οι μόνες εργασίες που μπορείτε να εκτελέσετε στο Πληροφοριακό Σύστημα, είναι να διαχειριστείτε τα <a runat="server" href="~/Secure/Stores/StoreDetails.aspx">Στοιχεία Προμηθευτή</a> και τα <a runat="server" href="~/Secure/Stores/AccountDetails.aspx">Στοιχεία Λογαριασμού</a> σας.                            
                        </asp:View>

                        <asp:View ID="vSubmittedRequest" runat="server">
                            <div class="reminder">Η Αίτηση σας για συμμετοχή στο Μητρώο Προμηθευτών έχει εγκριθεί.</div>
                            <div class="br"></div>
                            <div class="br"></div>
                            Είστε μέλος του Μητρώου Προμηθευτών με τα εξής χαρακτηριστικά:
                            <ul>
                                <li class="firstListItem">
                                    <asp:Label ID="lblStoreName" runat="server" /></li>                                
                            </ul>
                        </asp:View>

                    </asp:multiview>
            </div>
        </div>
    </div>

    <asp:placeholder id="phActions" runat="server" visible="false">
        <div class="rp" id="rpApplicationInstructions">
            <div class="rp-header">
                <div class="rp-headertext">Οδηγίες για τη χρήση της εφαρμογής</div>
                <a href="#" class="collapsibleTrigger img-btn-noborder tooltip rp-toggle" title="Εμφάνιση / Απόκρυψη">
                    <img id="Img2" runat="server" src="~/_img/iconMinimize.png" alt="Εμφάνιση / Απόκρυψη" /></a>
                <div class="rp-clr"></div>
            </div>
            <div class="rp-main">
                <div class="collapsibleHidden" style="text-align: center;">
                    <span style="color: #5C5C5C">Πατήστε <a href="#" class="collapsibleTrigger">εδώ</a> για να δείτε τα περιεχόμενα</span>
                </div>
                <div class="collapsibleContainer">
                    Οι εργασίες που μπορείτε να εκτελέσετε στο πληροφοριακό σύστημα της δράσης είναι οι εξής:
                    <ul>
                        <li class="firstListItem">Να δείτε και να επεξεργαστείτε τα 
                            <a runat="server" style="font-weight: bold" href="~/Secure/Stores/AccountDetails.aspx">Στοιχεία του Λογαριασμού</a> σας
                        </li>
                        <li id="liStoreDetails" runat="server" class="firstListItem">Να δείτε τα 
                            <a runat="server" style="font-weight: bold" href="~/Secure/Stores/StoreDetails.aspx">Στοιχεία Προμηθευτή</a>                            
                        </li>
                        <li id="liOffers" runat="server" class="firstListItem">Να προσθέσετε
                            <a runat="server" style="font-weight: bold" href="~/Secure/Stores/Offers.aspx">Προσφορές</a> και να διαχειριστείτε τις Προσφορές σας
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    </asp:placeholder>
</asp:Content>
