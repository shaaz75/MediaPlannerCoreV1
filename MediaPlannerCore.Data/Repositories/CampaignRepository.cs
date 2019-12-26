using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Data.Repositories
{
    public interface ICampaignRepository : IRepository<Campaign>
    {
    }
    public class CampaignRepository : BaseRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(MediaPlannerCoreContext context) : base(context) { }
    }
}