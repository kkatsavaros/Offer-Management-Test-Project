<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AutoCompleteInput.ascx.cs" Inherits="OfferManagement.Portal.UserControls.GenericControls.AutoCompleteInput" %>

<script type="text/javascript">
    $(function () {

        var tagTypes = <%= TagTypes == null ? "null" : TagTypes.Value.ToString() %>;
        OfferManagement.Portal.PortalServices.Services.GetTags(tagTypes, function (tags) {
            var inputEl = <%= txtValue.ClientID %>.GetInputElement();

            $(inputEl).autocomplete({
                delay: 0,
                minLength: 1,
                source: function(request, response) {
                    var filteredArray = $.map(tags, function(item) {
                        
                        if (item.toUpperCase().indexOf(request.term.toUpperCase()) == 0) {
                            return item;
                        }
                        else {
                            return null;
                        }
                    });
                    
                    response(filteredArray);
                }
            });
        });

    });
</script>

<dx:ASPxTextBox ID="txtValue" runat="server" EnableClientSideAPI="true" />
