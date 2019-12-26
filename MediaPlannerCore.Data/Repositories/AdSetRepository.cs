using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Data.Repositories
{
    public interface IAdSetRepository : IRepository<AdSet>
    {
    }
    public class AdSetRepository : BaseRepository<AdSet>, IAdSetRepository
    {
        public AdSetRepository(MediaPlannerCoreContext context) : base(context) { }

    }
}