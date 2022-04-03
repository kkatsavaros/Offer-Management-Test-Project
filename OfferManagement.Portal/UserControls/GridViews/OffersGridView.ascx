<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OffersGridView.ascx.cs" Inherits="OfferManagement.Portal.UserControls.GridViews.OffersGridView" %>

<%@ Import Namespace="OfferManagement.BusinessModel" %>
<%@ Import Namespace="OfferManagement.Portal" %>

<script type="text/javascript">
    function doAction(actionName, id, pageCode, confirmParam) {

        var params = [actionName, id].join(':');
        var doCallback = function () { gv.PerformCallback(params); };
    }

    function gvCallbackEnd(s, e) {
        if (gv.cpError) {
            showAlertBox(gv.cpError);
            gv.cpError = null;
        }
    }
</script>



<dx:ASPxGridView ID="gvOffers" runat="server" DataSourceForceStandardPaging="true"
    OnHtmlRowPrepared="gvOffers_HtmlRowPrepared">
    <Columns>
        <dx:GridViewDataTextColumn FieldName="OfferStatusInt" Name="OfferStatus" Caption="Κατάσταση" Width="95px">
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).GetOfferStatus() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Name="ProviderDetails" Caption="Στοιχεία Προμηθευτή">
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).Store.Reporter.GetReporterDetails() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Name="ContactDetails" Caption="Στοιχεία Ατόμου Επικοινωνίας">
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).Store.Reporter.GetContactDetails() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="Code" Name="GeneralInfo" Caption="Γενικά Στοιχεία">
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).GetGeneralInfo() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn Name="OfferDetails" Caption="Στοιχεία Προσφοράς">
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).GetSummary() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="SubmittedAt" Name="SubmittedAt" Caption="Ημ/νία Υποβολής" Width="50px">
            <HeaderStyle HorizontalAlign="Center" Wrap="True" />
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).GetSubmissionDetails() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="EvaluatedAt" Name="EvaluatedAt" Caption="Ημ/νία Έγκρισης" Width="50px">
            <HeaderStyle HorizontalAlign="Center" Wrap="True" />
            <DataItemTemplate>
                <%# ((Offer)Container.DataItem).GetEvaluationDetails() %>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
        <dx:GridViewDataTextColumn FieldName="UndoAction" Name="UndoAction" Caption="Προηγούμενο στάδιο" Width="50px">
            <HeaderStyle HorizontalAlign="Center" Wrap="True" />
            <DataItemTemplate>
                <a class="img-btn tooltip" printable="False" runat="server" href="javascript:void(0);" title="Επαναφορά Προσφοράς σε προηγούμενο στάδιο"
                    onclick='<%# string.Format("return doAction(\"revertsubmit\", {0}, \"Offers\", \"{1}\");", Eval("ID"), Server.HtmlEncode(((string)Eval("Title"))))%>'>
                    <img src="/_img/iconUndo.png" alt="Επαναφορά Προσφοράς σε προηγούμενο στάδιο" /></a>
            </DataItemTemplate>
        </dx:GridViewDataTextColumn>
    </Columns>
</dx:ASPxGridView>

<dx:ASPxGridViewExporter ID="gveOffers" runat="server" GridViewID="gvOffers" OnRenderBrick="gveOffers_RenderBrick" />
