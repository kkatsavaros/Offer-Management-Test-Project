namespace OfferManagement.BusinessModel
{
    public enum enEmailType
    {
        /// <summary>
        /// Custom Message
        /// </summary>
        CustomMessage = 1,        

        /// <summary>
        /// Email με κωδικό πρόσβασης
        /// </summary>
        ForgotPassword = 2,        

        /// <summary>
        /// Πιστοποίηση Email Προμηθευτή
        /// </summary>
        StoreEmailVerification = 3
    }
}