namespace OfferManagement.BusinessModel
{
    internal interface ITrackable
    {
        string ValueXML { get; set; }
        bool AllowTrackingChanges { get; set; }
    }
}
