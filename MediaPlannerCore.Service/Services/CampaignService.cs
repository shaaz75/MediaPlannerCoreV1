using MediaPlannerCore.Data.Repositories;
using MediaPlannerCore.Model.Models;
using MediaPlannerCore.Model.Helper.CustomModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MediaPlannerCore.Service.Services
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository campaignRepository;
        public CampaignService(ICampaignRepository campaignRepository)
        {
            this.campaignRepository = campaignRepository;
        }

        public void AddCampaign(Campaign campaign)
        {
            this.campaignRepository.Insert(campaign);
        }
        public void UpdateCampaign(Campaign campaign)
        {
            this.campaignRepository.Update(campaign);
        }

        public void DeleteCampaign(int?id)
        {
            this.campaignRepository.Delete(id);
        }

        public Campaign GetCampaignById(int? id)
        {
            return this.campaignRepository.GetByID(id);
        }

        public IEnumerable<Campaign> GetCampaigns(string filter, string includeProperties)
        {
            IEnumerable<Campaign> campaigns = null;
            if (filter != null)
            {
                campaigns = this.campaignRepository.Get(s => s.CampaignName.Contains(filter), includeProperties).AsEnumerable();

            }
            else
            {
                campaigns = this.campaignRepository.Get(null, includeProperties).AsEnumerable();
            }
            return campaigns;
        }

        public IEnumerable<CampaignsToExport> GetCampaigsForExcel(string filter, string includeProperties)
        {
            IEnumerable<CampaignsToExport> campaigns = null;
            if (filter != null)
            {
                campaigns = this.campaignRepository.Get(s => s.CampaignName.Contains(filter), includeProperties)
                    .Select(s => new CampaignsToExport { CampaignName = s.CampaignName, StartDateTime = s.StartDateTime, EndDateTime = s.EndDateTime, Budget = s.Budget, ClientName = s.Client.ClientName, CountryName = s.Country.CountryName }).AsEnumerable();

            }
            else
            {
                campaigns = this.campaignRepository.Get(null, includeProperties)
                    .Select(s => new CampaignsToExport { CampaignName = s.CampaignName, StartDateTime = s.StartDateTime, EndDateTime = s.EndDateTime, Budget = s.Budget, ClientName = s.Client.ClientName, CountryName = s.Country.CountryName }).AsEnumerable();
            }
            return campaigns;
        }
    }
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetCampaigns(string filter, string includeProperties);
        IEnumerable<CampaignsToExport> GetCampaigsForExcel(string filter, string includeProperties);
        Campaign GetCampaignById(int?id);
        void AddCampaign(Campaign campaign);
        void UpdateCampaign(Campaign campaign);
        void DeleteCampaign(int?id);

    }
}
