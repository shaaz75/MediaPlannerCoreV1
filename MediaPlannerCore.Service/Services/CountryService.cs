using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Service.Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository CountryRepository;
        public CountryService(ICountryRepository CountryRepository)
        {
            this.CountryRepository = CountryRepository;
        }

        public Country GetCountryById(int? id)
        {
            return this.CountryRepository.GetByID(id);
        }

        public IEnumerable<Country> GetCountrys(string filter, string includeProperties)
        {
            IEnumerable<Country> countries = null;
            if(filter!=null)
            {
                countries= this.CountryRepository.Get(s => s.CountryName.Contains(filter), includeProperties).AsEnumerable<Country>();
            }
            else
            {
                countries= this.CountryRepository.Get(null, includeProperties).AsEnumerable<Country>();

            }
            return countries;
        }
    }
    public interface ICountryService
    {
        IEnumerable<Country> GetCountrys(string filter, string includeProperties);
        Country GetCountryById(int? id);
    }
}
