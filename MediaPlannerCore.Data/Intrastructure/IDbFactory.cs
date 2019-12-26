using MediaPlannerCore.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaPlannerCore.Data.Infrastructure
{
    public interface IDbFactory : IDisposable
    {
        MediaPlannerCoreContext Init();
    }
}
