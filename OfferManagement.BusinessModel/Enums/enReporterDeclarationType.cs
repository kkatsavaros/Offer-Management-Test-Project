namespace OfferManagement.BusinessModel
{
    public enum enReporterDeclarationType
    {
        /// <summary>
        /// Εγγεγραμμένος Χρήστης
        /// </summary>
        FromRegistration = 1,

        /// <summary>
        /// Δηλώθηκε από το Γραφείο Υποστήριξης Χρηστών
        /// </summary>
        FromHelpdesk = 2,

        /// <summary>
        /// Δηλώθηκε μόνος του από το Portal
        /// </summary>
        FromPortal = 3
    }
}
