using MediaPlannerCore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlannerCore.Data.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
       
        MediaPlannerCoreContext dbContext;

        public MediaPlannerCoreContext Init()
        {
            return dbContext ?? (dbContext = new MediaPlannerCoreContext());
        }

        //protected override void DisposeCore()
        //{
        //    if (dbContext != null)
        //        dbContext.Dispose();
        //}
    }
}
