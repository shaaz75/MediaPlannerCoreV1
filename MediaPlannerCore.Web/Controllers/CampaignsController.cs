using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FastMember;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Data.Infrastructure;
using MediaPlannerCore.Model.Helper.CustomModel;
using MediaPlannerCore.Model.Models;
using MediaPlannerCore.Service.Services;
using OfficeOpenXml;

namespace MediaPlannerCore.Web.Controllers
{
    public class CampaignsController : Controller
    {
        private readonly ICampaignService campaignService;
        private readonly ICountryService countryService;
        private readonly IClientService clientService;
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger logger;

        public CampaignsController(ICampaignService campaignService, ICountryService countryService, IClientService clientService,IUnitOfWork unitOfWork)
        {
            this.campaignService=campaignService;
            this.clientService = clientService;
            this.countryService = countryService;
            this.unitOfWork = unitOfWork;
        }

        // GET: Campaigns
        public IActionResult Index(string filter, int? page)
        {

            var campaigns = this.campaignService.GetCampaigns(filter,"Client,Country");

            List<PieChart> lstPieChart = new List<PieChart>();
            foreach (var item in campaigns)
            {
                lstPieChart.Add(new PieChart { X = item.CampaignName, Y = item.Budget.ToString(), Label = item.CampaignName });
            }
            ViewBag.PieData = JsonConvert.SerializeObject(lstPieChart);
            //Paging
            int pageSize = 10;
            int pageNumber = (page ?? 1);


            return View(PaginatedList<Campaign>.Create(campaigns,pageNumber==0?1:pageNumber,pageSize));
        }

        public IActionResult ExportToExcel()
        {
            DataTable dt = new DataTable();

            List<CampaignsToExport> campaignsToExport = this.campaignService.GetCampaigsForExcel(null, null).ToList();
            using (var reader = ObjectReader.Create<CampaignsToExport>(campaignsToExport, null))
            {
                dt.Load(reader);
            }
            byte[] fileContents;
            using (var package = new ExcelPackage())
            {
                var workSheet = package.Workbook.Worksheets.Add("Campaign");
                workSheet.Cells["A1"].LoadFromDataTable(dt, true);
                fileContents = package.GetAsByteArray();
            }
            if (fileContents == null || fileContents.Length == 0)
            {
                return NotFound();
            }
            else
            {
                return File(fileContents: fileContents,
                    contentType: "application/vnd.openxmlformats-officedocument.spreedsheetml.sheet",
                    fileDownloadName: "CampaignsList.xlsx");
            }
        }

        // GET: Campaigns/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            //var campaign = this.campaignService.GetCampaignById(id);

            var campaign = this.campaignService.GetCampaigns(null, "Client,Country").Where(s => s.CampaignId == id).ToList();
            if (campaign.Count()==0)
            {
                ViewData["Message"] = "No Record Found";
                return PartialView("_CustomMessage", ViewData["Message"]);
            }

            return View(campaign.First());
        }

        // GET: Campaigns/Create
        public IActionResult Create()
        {
            ViewData["ClientId"] = new SelectList(this.clientService.GetClients(null,null), "ClientId", "ClientName");
            ViewData["CountryId"] = new SelectList(this.countryService.GetCountrys(null,null), "CountryId", "CountryName");
            return View();
        }

        // POST: Campaigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Create([Bind("CampaignId,CampaignName,ClientId,CountryId,StartDateTime,EndDateTime,Budget")] Campaign campaign)
        {
            if (ModelState.IsValid)
            {
                this.campaignService.AddCampaign(campaign);
                this.unitOfWork.Commit();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(this.clientService.GetClients(null,null), "ClientId", "ClientName", campaign.ClientId);
            ViewData["CountryId"] = new SelectList(this.countryService.GetCountrys(null,null), "CountryId", "CountryName", campaign.CountryId);
            return View(campaign);
        }

        // GET: Campaigns/Edit/5
        public  IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = this.campaignService.GetCampaignById(id);
            if (campaign == null)
            {
                return NotFound();
            }
            ViewBag.ClientId = new SelectList(this.clientService.GetClients(null,null), "ClientId", "ClientName", campaign.ClientId);
            ViewBag.CountryId = new SelectList(this.countryService.GetCountrys(null,null), "CountryId", "CountryName", campaign.CountryId);
            return View(campaign);
        }

        // POST: Campaigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("CampaignId,CampaignName,ClientId,CountryId,StartDateTime,EndDateTime,Budget")] Campaign campaign)
        {
            if (id != campaign.CampaignId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.campaignService.UpdateCampaign(campaign);
                    this.unitOfWork.Commit();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CampaignExists(campaign.CampaignId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClientId"] = new SelectList(this.clientService.GetClients(null,null), "ClientId", "ClientName", campaign.ClientId);
            ViewData["CountryId"] = new SelectList(this.countryService.GetCountrys(null,null), "CountryId", "CountryName", campaign.CountryId);
            return View(campaign);
        }

        // GET: Campaigns/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var campaign = this.campaignService.GetCampaignById(id);
            if (campaign == null)
            {
                return NotFound();
            }

            return View(campaign);
        }

        // POST: Campaigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            this.campaignService.DeleteCampaign(id);
            this.unitOfWork.Commit();
            return RedirectToAction(nameof(Index));
        }

        private bool CampaignExists(int id)
        {
            return this.campaignService.GetCampaignById(id) == null ? false : true;
        }
    }
}
