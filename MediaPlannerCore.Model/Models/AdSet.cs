using System.ComponentModel.DataAnnotations;

namespace MediaPlannerCore.Model.Models
{

    public partial class AdSet
    {
        public int AdSetId { get; set; }

        public int? CampaignId { get; set; }

        [Required]
        [Display(Name = "Media Channel")]
        public int? MediaChannelId { get; set; }

        [Required]
        [Display(Name = "Supplier Name")]
        public int? SupplierId { get; set; }

        public virtual Ad Ad { get; set; }

        public virtual Campaign Campaign { get; set; }

        public virtual MediaChannel MediaChannel { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
