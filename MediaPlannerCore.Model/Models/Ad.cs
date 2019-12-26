using System;

namespace MediaPlannerCore.Model.Models
{
    public partial class Ad
    {
        public int AdId { get; set; }

        public string AdTitle { get; set; }

        public int? CampaignId { get; set; }

        public DateTime? AdStartDateTime { get; set; }

        public DateTime? AdEndDateTime { get; set; }

        public string RedirectUrl { get; set; }

        public string AdImage { get; set; }

        public int? AdSetId { get; set; }

        public string AdContent { get; set; }

        public decimal? AdBudget { get; set; }

        public virtual AdSet AdSet { get; set; }

        public virtual Campaign Campaign { get; set; }
    }
}
