using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Service.Services
{
    public class AdSetService : IAdSetService
    {
        private readonly IAdSetRepository adSetRepository;
        public AdSetService(IAdSetRepository adSetRepository)
        {
            this.adSetRepository = adSetRepository;
        }

        public void AddAdSet(AdSet adSet)
        {
            this.adSetRepository.Insert(adSet);
        }
        public void UpdateAdSet(AdSet adSet)
        {
            this.adSetRepository.Update(adSet);
        }

        public void DeleteAdSet(int? id)
        {
            this.adSetRepository.Delete(id);
        }

        public AdSet GetAdSetById(int? id)
        {
            return this.adSetRepository.GetByID(id);
        }

        public IEnumerable<AdSet> GetAdSets(int?campaignId, string includeProperties)
        {
            IEnumerable<AdSet> adSets = null;
            if (campaignId != null)
            {
                adSets = this.adSetRepository.Get(s => s.CampaignId == campaignId, includeProperties).AsEnumerable();

            }
            else
            {
                adSets = this.adSetRepository.Get(null, includeProperties).AsEnumerable();
            }
            return adSets;
        }
    }
    public interface IAdSetService
    {
        IEnumerable<AdSet> GetAdSets(int? campaignId, string includeProperties);
        AdSet GetAdSetById(int? id);
        void AddAdSet(AdSet adSet);
        void UpdateAdSet(AdSet adSet);
        void DeleteAdSet(int? id);

    }
}
