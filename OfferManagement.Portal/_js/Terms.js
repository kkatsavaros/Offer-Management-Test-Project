function ShowTerms() {
    var dialog = $("#terms").clone()
                    .dialog({
                        title: 'Εκτύπωση Όρων Συμμετοχής',
                        modal: true,
                        resizable: false,
                        width: 1150,
                        maxHeight: 700,
                        draggable: true,
                        show: 'fade',
                        hide: 'fade',
                        dialogClass: 'main-dialog-class',
                        buttons: {
                            "Εκτύπωση": function () {
                                dialog.dialog('close');
                                PrintTerms();
                            },
                            "Κλείσιμο": function () {
                                dialog.dialog('close');
                            }
                        },
                        open: function () {
                            $(this).parent().find('.ui-dialog-buttonpane button:first-child').button({
                                icons: { primary: 'bg-print' }
                            });
                            $(this).parent().find('.ui-dialog-buttonpane button:first-child').next().button({
                                icons: { primary: 'bg-cancel' }
                            });
                        }
                    });
}

function PrintTerms() {
    $("#terms").printElement();
}

function CheckAccepted() {
    var enabled = chkAgreeTerms.GetChecked();    

    btnAcceptTerms.SetEnabled(enabled);
}