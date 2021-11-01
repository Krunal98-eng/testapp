using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using VoipProjectEntities.Domain.Common;


namespace VoipProjectEntities.Domain.Entities
{
    public class Setting : CommonField
    {
        public Guid SettingID {get; set; }

        [Display(Name = "Customer")]
        public Guid CustomerID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customers { get; set; }
        public int SettingType { get; set; }
        public string Value1 { get; set; }
        public string Value2 { get; set; }
        public string Value3 { get; set; }

    }
}
