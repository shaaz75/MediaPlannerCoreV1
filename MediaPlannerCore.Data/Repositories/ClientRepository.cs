using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlannerCore.Data.Repositories
{
    public interface IClientRepository : IRepository<Client>
    {
    }
    public class ClientRepository : BaseRepository<Client>, IClientRepository
    {
        public ClientRepository(MediaPlannerCoreContext context) : base(context) { }
    }
}
