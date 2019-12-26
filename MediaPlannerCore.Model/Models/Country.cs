using System.Collections.Generic;

namespace MediaPlannerCore.Model.Models
{
    public partial class Country
    {
        public Country()
        {
            Campaign = new HashSet<Campaign>();
        }

        public int CountryId { get; set; }
        public string CountryName { get; set; }

        public virtual ICollection<Campaign> Campaign { get; set; }
    }
}
