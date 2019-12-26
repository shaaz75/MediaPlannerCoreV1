using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MediaPlannerCore.Model.Helper.CustomModel
{
    public class CampaignsToExport
    {
        public string CampaignName { get; set; }

        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }

        public decimal? Budget { get; set; }

        public string ClientName { get; set; }

        public string CountryName { get; set; }
    }
}
