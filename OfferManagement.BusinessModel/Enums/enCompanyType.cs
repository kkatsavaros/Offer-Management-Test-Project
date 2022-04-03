using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfferManagement.BusinessModel
{
    public enum enCompanyType
    {
        /// <summary>
        /// None
        /// </summary> 
        None = 0,

        /// <summary>
        /// Ανώνυμη Εταιρεία - Α.Ε.
        /// </summary> 
        AE = 1,

        /// <summary>
        /// Εταιρεία Περιορισμένης Ευθύνης - Ε.Π.Ε.
        /// </summary> 
        EPE = 2,

        /// <summary>
        /// Ιδιωτική Κεφαλαιουχική Εταιρεία - Ι.Κ.Ε.
        /// </summary> 
        IKE = 3,

        /// <summary>
        /// Ομόρρυθμη Εταιρεία - Ο.Ε.
        /// </summary> 
        OE = 4,

        /// <summary>
        /// Ετερόρρυθμη Εταιρεία - Ε.Ε.
        /// </summary> 
        EE = 5,

        /// <summary>
        /// Ατομική Επιχείρηση
        /// </summary> 
        PersonalCompany = 6
    }
}
