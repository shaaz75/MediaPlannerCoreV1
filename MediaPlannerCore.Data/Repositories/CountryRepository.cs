using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MediaPlannerCore.Data.Repositories
{
    public interface ICountryRepository : IRepository<Country>
    {
    }
    public class CountryRepository : BaseRepository<Country>, ICountryRepository
    {
        public CountryRepository(MediaPlannerCoreContext context) : base(context) { }
    }
}