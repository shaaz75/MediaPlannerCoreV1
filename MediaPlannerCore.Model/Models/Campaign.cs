using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MediaPlannerCore.Model.Models
{
    public partial class Campaign
    {
        public Campaign()
        {
            Ad = new HashSet<Ad>();
            AdSet = new HashSet<AdSet>();
        }

        public int CampaignId { get; set; }
        
        [Required]
        [Display(Name ="Campaign Name")]
        public string CampaignName { get; set; }

        [Required]
        [Display(Name ="Client Name")]
        public int? ClientId { get; set; }

        [Required]
        [Display(Name = "Country Name")]
        public int? CountryId { get; set; }

        [Required]
        [Display(Name = "Start Date Time")]
        
        public DateTime? StartDateTime { get; set; }

        [Required]
        [Display(Name = "End Date Time")]
        public DateTime? EndDateTime { get; set; }

        [Required]
        [Display(Name = "Budet")]
        public decimal? Budget { get; set; }

        public virtual Client Client { get; set; }
        public virtual Country Country { get; set; }
        public virtual ICollection<Ad> Ad { get; set; }
        public virtual ICollection<AdSet> AdSet { get; set; }
    }
}
