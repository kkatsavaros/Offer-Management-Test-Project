function FillCities(s, e, promptValue) {
    ddlCity.ClearItems();

    var _prefectureID = s.GetValue();

    if (!_prefectureID) {
        return;
    }

    promptValue = promptValue || '-- παρακαλώ επιλέξτε --';
    s.SetEnabled(false);

    return $.ajax({
        type: "POST",
        url: "/PortalServices/Services.asmx/GetCities",
        data: "{ 'prefectureID': '" + _prefectureID.toString() + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            var cities = response.d;

            ddlCity.AddItem(promptValue, null);            

            for (var i = 0; i < cities.length; i++) {
                ddlCity.AddItem(cities[i].name, cities[i].id);
            }

            ddlCity.SetValue(null);

            s.SetEnabled(true);
        },
        error: function (response) {
            showAlertBox("Παρουσιάστηκε κάποιο σφάλμα. Παρακαλώ δοκιμάστε ξανά.");
            s.SetEnabled(true);
        }
    });
}