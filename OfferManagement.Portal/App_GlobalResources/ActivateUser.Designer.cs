//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Resources {
    using System;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option or rebuild the Visual Studio project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Web.Application.StronglyTypedResourceProxyBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ActivateUser {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ActivateUser() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Resources.ActivateUser", global::System.Reflection.Assembly.Load("App_GlobalResources"));
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &lt;div class=&quot;reminder&quot;&gt;Η διεύθυνση ενεργοποίησης δεν είναι έγκυρη.&lt;/div&gt;.
        /// </summary>
        internal static string InvalidUrl {
            get {
                return ResourceManager.GetString("InvalidUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &lt;div class=&quot;reminder&quot;&gt;Η ενεργοποίηση του λογαριασμού σας έγινε επιτυχώς. Για να συνδεθείτε στο σύστημα πατήστε &lt;a href=&quot;../Default.aspx&quot;&gt;εδώ&lt;/a&gt;&lt;/div&gt;.
        /// </summary>
        internal static string Success {
            get {
                return ResourceManager.GetString("Success", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &lt;div class=&quot;reminder&quot;&gt;Ο χρήστης είναι ήδη ενεργοποιημένος.&lt;/div&gt;.
        /// </summary>
        internal static string UserAlreadyActivated {
            get {
                return ResourceManager.GetString("UserAlreadyActivated", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to &lt;div class=&quot;reminder&quot;&gt;Δεν βρέθηκε χρήστης με το email που δηλώσατε. Αν τυχόν αλλάξατε το email που είχατε δηλώσει ή πατήσατε 2 φορές του κουμπί για επαναποστολή Email Επιβεβαίωσης, βεβαιωθείτε ότι πατάτε στο σύνδεσμο του τελευταίου email που θα σας έρθει.&lt;/div&gt;.
        /// </summary>
        internal static string UserNotFound {
            get {
                return ResourceManager.GetString("UserNotFound", resourceCulture);
            }
        }
    }
}
