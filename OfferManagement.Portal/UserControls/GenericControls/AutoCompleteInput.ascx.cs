using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using OfferManagement.BusinessModel;

namespace OfferManagement.Portal.UserControls.GenericControls
{
    public partial class AutoCompleteInput : System.Web.UI.UserControl
    {
        #region [ Exposed Designer Properties ]

        public bool ReadOnly
        {
            get { return txtValue.ReadOnly; }
            set { txtValue.ReadOnly = value; }
        }

        public bool ClientEnabled
        {
            get { return txtValue.ClientEnabled; }
            set { txtValue.ClientEnabled = value; }
        }

        public int MaxLength
        {
            get { return txtValue.MaxLength; }
            set { txtValue.MaxLength = value; }
        }

        public Unit Width
        {
            get { return txtValue.Width; }
            set { txtValue.Width = value; }
        }

        public string CssClass
        {
            get { return txtValue.CssClass; }
            set { txtValue.CssClass = value; }
        }

        public string ToolTip
        {
            get { return txtValue.ToolTip; }
            set { txtValue.ToolTip = value; }
        }

        [AutoFormatEnable]
        [Category("Validation")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [PersistenceMode(PersistenceMode.InnerProperty)]
        public DevExpress.Web.ValidationSettings ValidationSettings
        {
            get { return txtValue.ValidationSettings; }
        }

        #endregion

        public enTagType? TagTypes { get; set; }

        public ASPxTextBox TextBox
        {
            get { return txtValue; }
        }
    }
}