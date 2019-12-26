using System;
using System.Collections.Generic;

namespace MediaPlannerCore.Model.Models
{
    public partial class Supplier
    {
        public Supplier()
        {
            AdSet = new HashSet<AdSet>();
        }

        public int SupplierId { get; set; }
        public string SupplierName { get; set; }
        public int? MediaChannelId { get; set; }

        public virtual MediaChannel MediaChannel { get; set; }
        public virtual ICollection<AdSet> AdSet { get; set; }
    }
}
