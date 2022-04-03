function hideAllButtons() {
    Imis.Lib.HideButton(btnPrint);
    Imis.Lib.HideButton(btnSubmit);    
    Imis.Lib.HideButton(btnCancelWithRefresh);
    btnCancel.SetVisible(true);
}

function showChangePasswordPopup(requestOldPassword) {
    var popupUrl = [storeUrls.ChangePasswordUrl, '?id=', requestOldPassword].join('');

    popUp.show(popupUrl, 'Αλλαγή Κωδικού Πρόσβασης', null, 600, 350);
    hideAllButtons();
    btnSubmit.SetVisible(true);
}

function showViewStorePopup(providerID) {
    var popupUrl = [storeUrls.ViewStoreUrl, '?id=', providerID].join('');

    popUp.show(popupUrl, 'Προβολή Στοιχείων Καταστήματος', cmdRefresh, 1000, 700);
    hideAllButtons();
}

function showViewOfferPopup(offerID) {
    var popupUrl = [storeUrls.ViewOfferUrl, '?id=', offerID].join('');

    popUp.show(popupUrl, 'Προβολή Προσφοράς', null, 800, 870);
    hideAllButtons();
}

//AlterPassword.aspx
function clearErrors() {
    $('#divErrors').hide();
}

function onActionDone(result) {
    if (result == "ERROR") {
        showAlertBox('Προέκυψε ένα σφάλμα κατά την εκτέλεση της ενέργειας! Παρακαλώ προσπαθήστε ξανά.');
    }
    else if (result == "DATABIND") {
        gv.PerformCallback('databind:0');
    }
    else {
        window.location = result;
    }
}

function doAction(actionName, id, pageCode, confirmParam) {
    var params = [actionName, id].join(':');

    if (actionName == 'refresh') {
        gv.PerformCallback(params);
    }
    else {
        var doCallback = function () { gv.PerformCallback(params); };
        switch (pageCode) {            
            case "Offers":
                if (actionName == 'delete') {
                    if (confirmParam) {
                        showConfirmBox('Διαγραφή Προσφοράς', 'Είστε σίγουροι ότι θέλετε να διαγράψετε την προσφορά \'' + confirmParam + '\';', doCallback);
                    }
                    else {
                        showConfirmBox('Διαγραφή Προσφοράς', 'Είστε σίγουροι ότι θέλετε να διαγράψετε την προσφορά;', doCallback);
                    }
                }
                else if (actionName == 'submit') {
                    showConfirmBox('Υποβολή Προσφοράς', 'Είστε σίγουροι ότι θέλετε να υποβάλετε την προσφορά \'' + confirmParam + '\';', doCallback);
                }
                else if (actionName == 'revertsubmit') {
                    showConfirmBox('Επαναφορά Προσφοράς', 'Είστε σίγουροι ότι θέλετε να επαναφέρετε την προσφορά \'' + confirmParam + '\';', doCallback);
                }
                else if (actionName == 'publish') {
                    showConfirmBox('Δημοσίευση Προσφοράς', 'Με την ενέργεια αυτή δηλώνετε την πρόθεση να δημοσιεύσετε την προεγκεκριμένη προσφορά \'' + confirmParam + '\', μετά την οριστική έγκρισή της από τo ΤΕΕ.<br/><br/>Σημειώνεται ότι η προσφορά δεν θα ανακοινωθεί στους δικαιούχους πριν τις 04/02/2015, ημερομηνία ανακοίνωσης των εγκεκριμένων προσφορών.<br/><br/>Είστε σίγουροι ότι θέλετε να συνεχίσετε;', doCallback, function (s, e) { gv.PerformCallback("refresh") });
                }
                else if (actionName == 'unpublish') {
                    showConfirmBox('Αποδημοσίευση Προσφοράς', 'Είστε σίγουροι ότι θέλετε να αποδημοσιεύσετε την προσφορά \'' + confirmParam + '\';', doCallback, function (s, e) { gv.PerformCallback("refresh") });
                }
                else if (actionName == 'publishaccepted') {
                    showConfirmBox('Δημοσίευση Προσφοράς', 'Είστε σίγουροι ότι θέλετε να δημοσιεύσετε την προσφορά \'' + confirmParam + '\';', doCallback, function (s, e) { gv.PerformCallback("refresh") });
                }
                break;            
        }
    }

    return false;
}

function gvCallbackEnd(s, e) {
    if (gv.cpError) {
        showAlertBox(gv.cpError);
        gv.cpError = null;
    }
}