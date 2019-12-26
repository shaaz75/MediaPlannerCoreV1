using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Data.Repositories
{
    public interface IMediaChannelRepository : IRepository<MediaChannel>
    {
    }
    public class MediaChannelRepository : BaseRepository<MediaChannel>, IMediaChannelRepository
    {
        public MediaChannelRepository(MediaPlannerCoreContext context) : base(context) { }

    }
}