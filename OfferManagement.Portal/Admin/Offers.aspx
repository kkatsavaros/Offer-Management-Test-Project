<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Offers.aspx.cs" Inherits="OfferManagement.Portal.Admin.Offers" MasterPageFile="~/Admin/Admin.Master" Title="Προσφορές" %>

<%@ Register TagPrefix="my" TagName="OfferSearchFiltersControl" Src="~/UserControls/SearchFilters/OfferSearchFiltersControl.ascx" %>
<%@ Register TagPrefix="my" TagName="OffersGridView" Src="~/UserControls/GridViews/OffersGridView.ascx" %>
<%@ Import Namespace="OfferManagement.BusinessModel" %>


<asp:Content runat="server" ContentPlaceHolderID="cphMain">
    <script type="text/javascript">
        function cmdRefresh() {
            gv.PerformCallback("refresh");
            //doAction("refresh");
        }


    </script>

    <asp:MultiView ID="AdminOffers" runat="server" ActiveViewIndex="0">
        <asp:View ID="vCanEdit" runat="server">

           <my:OfferSearchFiltersControl ID="ucSearchFiltersControl" runat="server" Mode="Store" ClientSideFiltersChanged="cmdRefresh" />          



            <div class="filterButtons">
                <table>
                    <tr>
                        <td>
                            <dx:ASPxButton ID="btnSearch" runat="server" Text="Αναζήτηση" Image-Url="~/_img/iconView.png">
                                <ClientSideEvents Click="cmdRefresh" />
                            </dx:ASPxButton>
                        </td>
                        <td></td>
                        <td>
                            <dx:ASPxButton ID="btnColourExplanation" runat="server" Text="Επεξήγηση Χρωμάτων" Image-Url="~/_img/iconColourExplanation.png">
                                <ClientSideEvents Click="function(s,e) { var baseUrl =  window.open('ColorExplanation.aspx','colourExplanation','toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=500, height=270'); return false; }" />
                            </dx:ASPxButton>
                        </td>
                        <td>
                            <dx:ASPxButton ID="btnExport" runat="server" Text="Εξαγωγή σε Excel" Image-Url="~/_img/iconExcel.png" OnClick="btnExport_Click" />
                        </td>
                    </tr>
                </table>
            </div>


            <my:OffersGridView ID="gvOffers" runat="server" DataSourceID="odsOffers"
                OnCustomCallback="gvOffers_CustomCallback" PageSize="20">
            </my:OffersGridView>

            <asp:ObjectDataSource ID="odsOffers" runat="server" TypeName="OfferManagement.Portal.DataSources.Offers"
                SelectMethod="FindWithCriteria" SelectCountMethod="CountWithCriteria"
                EnablePaging="true" SortParameterName="sortExpression" OnSelecting="odsOffers_Selecting">
                <SelectParameters>
                    <asp:Parameter Name="criteria" Type="Object" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>
    </asp:MultiView>
</asp:Content>
