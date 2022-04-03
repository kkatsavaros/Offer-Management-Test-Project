<%@ Page Title="Διαχείριση Προσφορών" MasterPageFile="~/Secure/Stores/Stores.Master" Inherits="OfferManagement.Portal.Secure.Stores.Offers" CodeBehind="Offers.aspx.cs" Language="C#" AutoEventWireup="true" %>

<%@ Register TagPrefix="my" TagName="OfferSearchFiltersControl" Src="~/UserControls/SearchFilters/OfferSearchFiltersControl.ascx" %>
<%@ Register TagPrefix="my" TagName="OffersGridView" Src="~/UserControls/GridViews/OffersGridView.ascx" %>

<%@ Import Namespace="OfferManagement.BusinessModel" %>

<asp:Content runat="server" ContentPlaceHolderID="cphSecureMain">

    <asp:MultiView ID="mvOffers" runat="server" ActiveViewIndex="0">

        <asp:View ID="vCanEdit" runat="server">

            <script type="text/javascript">
                function cmdRefresh() {
                    doAction('refresh', '');
                }

                function publish(publish, id, title, isAccepted) {
                    if (publish) {
                        if (isAccepted) {
                            return doAction("publishaccepted", id, "Offers", title);
                        }
                        else {
                            return doAction("publish", id, "Offers", title);
                        }
                    }
                    else {
                        return doAction("unpublish", id, "Offers", title);
                    }
                }

            </script>

            <my:OfferSearchFiltersControl ID="ucSearchFiltersControl" runat="server" Mode="Store" ClientSideFiltersChanged="cmdRefresh" />

            <div class="filterButtons">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="Αναζήτηση" Image-Url="~/_img/iconView.png">
                                <ClientSideEvents Click="cmdRefresh" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnAddNewOffer" runat="server" Text="Δημιουργία Προσφοράς" Image-Url="~/_img/iconAddNewItem.png" OnClick="btnAddNewOffer_Click" />
                        </td>                        
                        <td>
                            <dx:ASPxButton ID="btnColourExplanation" runat="server" Text="Επεξήγηση Χρωμάτων" Image-Url="~/_img/iconColourExplanation.png">
                                <ClientSideEvents Click="function(s,e) { window.open('ColorExplanation.aspx','colourExplanation','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=500, height=270'); return false; }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" Image-Url="~/_img/iconExcel.png" OnClick="btnExport_Click" />
                        </td>                        
                    </tr>
                </table>
            </div>

            <my:OffersGridView ID="gvOffers" runat="server" DataSourceID="odsOffers"
                OnCustomCallback="gvOffers_CustomCallback" PageSize="20" OnExporterRenderBrick="gvOffers_ExporterRenderBrick">
                <ClientSideEvents EndCallback="gvCallbackEnd" />
                <Columns>
                    <dx:GridViewDataTextColumn Name="Submission" Caption=" " Width="40px" VisibleIndex="0">
                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <img class="img-btn tooltip" runat="server" src="~/_img/error.gif" alt="Ελλιπής" title="Η προσφορά δεν είναι πλήρως συμπληρωμένη"
                                visible='<%# HasErrors((Offer)Container.DataItem) %>' />                            
                            <a runat="server" href="javascript:void(0);" class="icon-btn bg-accept" title="Υποβολή Προσφοράς"
                                onclick='<%# string.Format("return doAction(\"submit\", {0}, \"Offers\", \"{1}\");", Eval("ID"), Server.HtmlEncode(((string)Eval("Title"))))%>'
                                visible='<%# CanSubmit((Offer)Container.DataItem) %>'>Υποβολή</a>
                            <input id="chbxIsPublished" runat="server" type="checkbox" clientidmode="Static"
                                checked='<%# IsPublished((Offer)Container.DataItem) %>'
                                visible='<%# CanPublishOrUnpublish((Offer)Container.DataItem) %>'
                                onclick='<%# string.Format("publish(this.checked, {0}, \"{1}\", {2});", Eval("ID"), GetTitle((Offer)Container.DataItem), (((enOfferStatus)Eval("OfferStatus")) == enOfferStatus.Submitted).ToString().ToLower()) %>'>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>                    
                    <dx:GridViewDataTextColumn Name="Actions" Caption="Ενέργειες" Width="80px" VisibleIndex="10">
                        <HeaderStyle HorizontalAlign="Center" Wrap="true" />
                        <CellStyle HorizontalAlign="Left" />
                        <DataItemTemplate>
                            <a class="img-btn tooltip" runat="server" href="javascript:void(0)" title="Προβολή Προσφοράς"
                                onclick='<%# string.Format("showViewOfferPopup({0})", Eval("ID"))%>'>
                                <img src="/_img/iconView.png" alt="Προβολή Προσφοράς" /></a>                            
                            <a class="img-btn tooltip" runat="server" title="Επεξεργασία Προσφοράς"
                                href='<%# string.Format("OfferDetails.aspx?id={0}", Eval("ID")) %>'
                                visible='<%# CanEdit((Offer)Container.DataItem) %>'>
                                <img src="/_img/iconEdit.png" alt="Επεξεργασία Προσφοράς" /></a>
                            <a class="img-btn tooltip" runat="server" href="javascript:void(0);" title="Διαγραφή Προσφοράς"
                                onclick='<%# string.Format("return doAction(\"delete\", {0}, \"Offers\", \"{1}\");", Eval("ID"), Server.HtmlEncode(((string)Eval("Title"))))%>'
                                visible='<%# CanDelete((Offer)Container.DataItem) %>'>
                                <img src="/_img/iconDelete.png" alt="Διαγραφή Προσφοράς" /></a>                            
                            <a class="img-btn tooltip" runat="server" href="javascript:void(0);" title="Επαναφορά Προσφοράς σε Επεξεργασία"
                                onclick='<%# string.Format("return doAction(\"revertsubmit\", {0}, \"Offers\", \"{1}\");", Eval("ID"), Server.HtmlEncode(((string)Eval("Title"))))%>'
                                visible='<%# CanRevertSubmit((Offer)Container.DataItem) %>'>
                                <img src="/_img/iconUndo.png" alt="Επαναφορά Προσφοράς σε Επεξεργασία" /></a>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>
                </Columns>
            </my:OffersGridView>

            <asp:ObjectDataSource ID="odsOffers" runat="server" TypeName="OfferManagement.Portal.DataSources.Offers"
                SelectMethod="FindWithCriteria" SelectCountMethod="CountWithCriteria"
                EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsOffers_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="criteria" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>

        <asp:View ID="vCannotEdit" runat="server">
            <div class="reminder">
                Δεν μπορείτε να διαχειριστείτε τις προσφορές σας, γιατί δεν έχετε ακόμα υποβάλει αίτηση σας για συμμετοχή στο Μητρώο Προμηθευτών.
            </div>
        </asp:View>

        <asp:View ID="vNotEnabled" runat="server">
            <div class="reminder">
                <asp:Label ID="lblNotEnabledMessage" runat="server" />
            </div>
        </asp:View>

    </asp:MultiView>

</asp:Content>
