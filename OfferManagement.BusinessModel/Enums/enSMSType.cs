namespace OfferManagement.BusinessModel
{
    public enum enSMSType
    {
        /// <summary>
        /// SMS με κωδικό πιστοποίησης
        /// </summary>
        VerificationCode = 1,

        /// <summary>
        /// Custom Message
        /// </summary>
        CustomMessage = 2,

        /// <summary>
        /// Αποστολή Κουπονιού Δικαιούχου
        /// </summary>
        StudentVoucherCode = 3
    }
}
