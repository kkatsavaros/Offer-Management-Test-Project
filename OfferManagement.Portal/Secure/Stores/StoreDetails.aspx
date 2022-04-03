<%@ Page Title="Στοιχεία Προμηθευτή" Language="C#" MasterPageFile="~/Secure/Stores/Stores.master" AutoEventWireup="true" CodeBehind="StoreDetails.aspx.cs" Inherits="OfferManagement.Portal.Secure.Stores.StoreDetails" %>

<%@ Register TagName="StoreInput" TagPrefix="my" Src="~/UserControls/StoreControls/InputControls/StoreInput.ascx" %>

<asp:Content ID="Content3" ContentPlaceHolderID="cphSecureMain" runat="server">
    <script type="text/javascript">
        var bankDetailsRequired = false;

        function ConfirmSubmit(s, e) {
            bankDetailsRequired = false;

            ASPxClientEdit.ValidateGroup('vgStoreInput');
            if (!ASPxClientEdit.AreEditorsValid()) {
                e.processOnServer = false;
                return false;
            }
        }

        function ConfirmRequest(s, e) {
            if ('<%= DeadlineExpired() %>' == 'True') {
                showAlertBox('Δεν μπορείτε να υποβάλετε την Αίτησή σας, διότι έχει παρέλθει η προθεσμία υποβολής αιτήσεων.', true);
                e.processOnServer = false;
                return false;
            }
            
            bankDetailsRequired = true;

            ASPxClientEdit.ValidateGroup('vgStoreInput');
            if (!ASPxClientEdit.AreEditorsValid()) {
                e.processOnServer = false;
                return false;
            }

            var dialog = $('<div>Με την ενέργειά σας αυτή, υποβάλετε Αίτηση για τη συμμετοχή σας στο Μητρώο Προμηθευτών της δράσης, με βάση τα στοιχεία που έχετε δηλώσει.<br /><br />' +
'Μετά την υποβολή της Αίτησης και όσο αυτή αξιολογείται, δεν θα μπορείτε να τροποποιήσετε τα εταιρικά σας στοιχεία.<br /><br />' +
'Θέλετε να υποβληθεί η Αίτησή σας;</div>')
              .dialog({
                  modal: true,
                  resizable: false,
                  draggable: true,
                  width: 600,
                  title: 'Υποβολή Αίτησης Συμμετοχής στο Μητρώο Προμηθευτών',
                  show: 'fade',
                  hide: 'fade',
                  dialogClass: 'main-dialog-class',
                  buttons: {
                      "Υποβολή": function () {
                          btnVerificationRequest.Click = null;
                          btnVerificationRequest.DoClick();
                          dialog.dialog('close');
                      },
                      "Ακύρωση": function () {
                          dialog.dialog('close');
                      }
                  },
                  open: function () {
                      $(this).parent().find('.ui-dialog-buttonpane button:nth-child(1)').button({
                          icons: { primary: 'bg-accept' }
                      });
                      $(this).parent().find('.ui-dialog-buttonpane button:nth-child(2)').button({
                          icons: { primary: 'bg-cancel' }
                      });
                  }
              });
            e.processOnServer = false;
        }

        function ConfirmRevertVerification(s, e) {
            
            if ('<%= DeadlineExpired() %>' == 'True') {
                showAlertBox('Δεν μπορείτε να αναιρέσετε την Αίτησή σας, διότι έχει επέλθει η προθεσμία υποβολής αιτήσεων.', true);
                e.processOnServer = false;
                return false;
            }

            var dialog = $('<div><b>Προσοχή:</b><br/>' +
    'Στην περίπτωση που πραγματοποιηθεί αναίρεση της αίτησης, <b>δεν</b> θα ισχύει ο παρών Κωδικός Αριθμός Υποβολής Αίτησης Συμμετοχής και θα πρέπει να υποβάλετε εκ νέου αίτηση συμμετοχής στο Μητρώο Προμηθευτών εντός των προβλεπόμενων στον Οδηγό της δράσης προθεσμιών.<br /><br />' +
    'Για κάθε νέα Υποβολή Αίτησης Συμμετοχής θα ισχύει διαφορετικός κωδικός αίτησης, γεγονός που μπορεί να έχει ως συνέπεια τη μετάθεση της ημερομηνίας αξιολόγησης της Αίτησης σε μεταγενέστερο χρόνο.<br /><br />' +
    'Είστε σίγουροι ότι θέλετε να συνεχίσετε;</div>')
              .dialog({
                  modal: true,
                  resizable: false,
                  draggable: true,
                  width: 600,
                  title: 'Αναίρεση Αίτησης Συμμετοχής στο Μητρώο Προμηθευτών',
                  show: 'fade',
                  hide: 'fade',
                  dialogClass: 'main-dialog-class',
                  buttons: {
                      "Αναίρεση Αίτησης": function () {
                          btnRevertVerification.Click = null;
                          btnRevertVerification.DoClick();
                          dialog.dialog('close');
                      },
                      "Ακύρωση": function () {
                          dialog.dialog('close');
                      }
                  },
                  open: function () {
                      $(this).parent().find('.ui-dialog-buttonpane button:nth-child(1)').button({
                          icons: { primary: 'bg-accept' }
                      });
                      $(this).parent().find('.ui-dialog-buttonpane button:nth-child(2)').button({
                          icons: { primary: 'bg-cancel' }
                      });
                  }
              });
            e.processOnServer = false;
        }

        function doCertificationPDF() {
            window.location.href = '/Secure/Stores/GenerateStoreCertificationPDF.ashx?id=' + <%= Entity.ID %>;
        }

    </script>

    <div class="summaryContainer">
        <dx:aspxvalidationsummary runat="server" validationgroup="vgStoreInput" />
        <asp:ValidationSummary runat="server" ValidationGroup="vgStoreInput" ForeColor="Red" Style="margin: 0 0 0 -15px" />
    </div>

    <div id="divRequestStatus" runat="server"></div>

    <div class="br"></div>

    <div id="divRequestData" runat="server">
        <table class="dv" width="100%">
            <colgroup>
                <col width="195px" />
            </colgroup>
            <tr>
                <th colspan="2" class="header">&raquo Στοιχεία Αίτησης Συμμετοχής στο Μητρώο Προμηθευτών
                </th>
            </tr>
            <tr>
                <th>Κωδικός Αριθμός Υποβολής Αίτησης Συμμετοχής:
                </th>
                <td>
                    <span id="spCertificationRequestCode" runat="server"></span>
                </td>
            </tr>
            <tr>
                <th>Ημ/νία και ώρα Υποβολής Αίτησης Συμμετοχής:
                </th>
                <td>
                    <span id="spCertificationDate" runat="server"></span>
                </td>
            </tr>
        </table>
    </div>

    <asp:PlaceHolder ID="phErrors" runat="server" Visible="true">
        <asp:Label ID="lblErrors" runat="server" Font-Bold="true" ForeColor="Red" />
        <div class="br"></div>
        <div class="br"></div>
    </asp:PlaceHolder>
    <my:storeinput id="ucStoreInput" runat="server" validationgroup="vgStoreInput" />

    <div class="br"></div>

    <table>
        <tr>
            <td>
                <dx:aspxbutton id="btnSubmit" runat="server" image-url="~/_img/iconSave.png" text="Αποθήκευση" onclick="btnSubmit_Click"
                    causesvalidation="true" validationgroup="vgStoreInput">
                            <ClientSideEvents Click="ConfirmSubmit" />
                        </dx:aspxbutton>
            </td>
            <td>
                <dx:aspxbutton id="btnVerificationRequest" runat="server" clientinstancename="btnVerificationRequest" image-url="~/_img/iconAccept.png" text="Υποβολή Αίτησης Συμμετοχής στο Μητρώο Προμηθευτών"
                    onclick="btnVerificationRequest_Click" validationgroup="vgStoreInput" causesvalidation="true">
                            <ClientSideEvents Click="ConfirmRequest" />
                        </dx:aspxbutton>
            </td>
            <td>
                <dx:aspxbutton id="btnRevertVerification" runat="server" clientinstancename="btnRevertVerification" image-url="~/_img/iconUndo.png" text="Αναίρεση Αίτησης" onclick="btnRevertVerification_Click" causesvalidation="false">
                            <ClientSideEvents Click="ConfirmRevertVerification" />
                        </dx:aspxbutton>
            </td>
            <td>
                <dx:aspxbutton id="btnPrintCertification" runat="server" text="Εκτύπωση Βεβαίωσης Συμμετοχής" image-url="~/_img/iconPrint.png">
                            <ClientSideEvents Click="doCertificationPDF" />
                        </dx:aspxbutton>
            </td>
        </tr>
    </table>

    <div class="br"></div>

    <div class="summaryContainer">
        <dx:aspxvalidationsummary runat="server" validationgroup="vgStoreInput" />
        <asp:ValidationSummary runat="server" ValidationGroup="vgStoreInput" ForeColor="Red" Style="margin: 0 0 0 -15px" />
    </div>

</asp:Content>
