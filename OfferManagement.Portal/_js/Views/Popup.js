function hideAllButtons() {
    Imis.Lib.HideButton(btnPopupSubmit);
    btnPopupCancel.SetVisible(true);
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
            
        }
    }

    return false;
}

function gvCallbackEnd(s, e) {
    if (gv.cpError) {
        showAlertBox(gv.cpError);
        gv.cpError = null;
    }
    else if (gv.cpMessage) {
        Imis.Lib.notify(gv.cpMessage);
    }
}