using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Data.Repositories
{
    public interface ISupplierRepository : IRepository<Supplier>
    {
    }
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        public SupplierRepository(MediaPlannerCoreContext context) : base(context) { }

    }
}