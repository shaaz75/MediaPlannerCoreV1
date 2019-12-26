using System.Collections.Generic;

namespace MediaPlannerCore.Model.Models

{
    public partial class Client
    {
        public int ClientId { get; set; }

        public string ClientName { get; set; }


        public virtual ICollection<Campaign> Campaigns { get; set; }
    }
}

