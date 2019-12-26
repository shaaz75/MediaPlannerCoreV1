using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MediaPlannerCore.Data.Context;
using MediaPlannerCore.Model.Models;
using MediaPlannerCore.Service.Services;

namespace MediaPlannerCore.Web.Controllers
{
    public class AdSetsController : Controller
    {
        private readonly ICampaignService campaignService;
        private readonly IAdSetService adSetService;
        private readonly IMediaChannelService mediaChannelService;
        private readonly ISupplierService supplierService;

        public AdSetsController(ICampaignService campaignService, IAdSetService adSetService, IMediaChannelService mediaChannelService, ISupplierService supplierService)
        {
            this.campaignService = campaignService;
            this.adSetService = adSetService;
            this.mediaChannelService = mediaChannelService;
            this.supplierService = supplierService;
        }

        // GET: AdSets
        public IActionResult Index(int? id)
        {
            if (id != null)
            {
                Campaign campaign = this.campaignService.GetCampaignById(id);
                if(campaign!=null)
                {
                    ViewBag.CampaignName = campaign.CampaignName;
                }
                var adSets = this.adSetService.GetAdSets(id, "Campaign,MediaChannel,Supplier");
                ViewBag.CampaignId = id.ToString();
                return View(adSets.ToList());
            }
            else
            {
                return View("Error", null);
            }
        }

        // GET: AdSets/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var adSet = this.adSetService.GetAdSets(null, "Campaign,MediaChannel,Supplier").Where(s => s.AdSetId == id).FirstOrDefault();
           
            if (adSet == null)
            {
                return NotFound();
            }
            ViewBag.CampaignId = adSet.CampaignId;
            return View(adSet);
        }

        // GET: AdSets/Create
        public IActionResult Create(int? id )
        {
            ViewBag.CampaignId = id;
            ViewBag.MediaChannelId = new SelectList(this.mediaChannelService.GetMediaChannels(null,null), "MediaChannelId", "MediaChannelName");
            ViewBag.SupplierId = new SelectList(this.supplierService.GetSuppliers(null,null), "SupplierId", "SupplierName");
            return View();
        }

        // POST: AdSets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("AdSetId,CampaignId,MediaChannelId,SupplierId")] AdSet adSet)
        {
            if (ModelState.IsValid)
            {
                this.adSetService.AddAdSet(adSet);
                return RedirectToAction(nameof(Index),new { id=adSet.CampaignId});
            }
            ViewBag.CampaignId = adSet.CampaignId;
            ViewBag.MediaChannelId = new SelectList(this.mediaChannelService.GetMediaChannels(null,null), "MediaChannelId", "MediaChannelName", adSet.MediaChannelId);
            ViewBag.SupplierId = new SelectList(this.supplierService.GetSuppliers(null,null), "SupplierId", "SupplierName", adSet.SupplierId);
            return View(adSet);
        }

        // GET: AdSets/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSet = this.adSetService.GetAdSetById(id);
            if (adSet == null)
            {
                return NotFound();
            }
            ViewBag.CampaignId = adSet.CampaignId;
            ViewBag.MediaChannelId = new SelectList(this.mediaChannelService.GetMediaChannels(null, null), "MediaChannelId", "MediaChannelName", adSet.MediaChannelId);
            ViewBag.SupplierId = new SelectList(this.supplierService.GetSuppliers(null, null), "SupplierId", "SupplierName", adSet.SupplierId);
            return View(adSet);
        }

        // POST: AdSets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("AdSetId,CampaignId,MediaChannelId,SupplierId")] AdSet adSet)
        {
            if (id != adSet.AdSetId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    this.adSetService.UpdateAdSet(adSet);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdSetExists(adSet.AdSetId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index), new { id = adSet.CampaignId });
            }
            ViewBag.CampaignId = adSet.CampaignId;
            ViewBag.MediaChannelId = new SelectList(this.mediaChannelService.GetMediaChannels(null,null), "MediaChannelId", "MediaChannelName", adSet.MediaChannelId);
            ViewBag.SupplierId = new SelectList(this.supplierService.GetSuppliers(null,null), "SupplierId", "SupplierName", adSet.SupplierId);
            return View(adSet);
        }

        // GET: AdSets/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adSet = this.adSetService.GetAdSetById(id);
            if (adSet == null)
            {
                return NotFound();
            }

            return View(adSet);
        }

        // POST: AdSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            AdSet adSet = this.adSetService.GetAdSetById(id);
            this.adSetService.DeleteAdSet(id);
            int? campaignId = adSet.CampaignId;
            return RedirectToAction(nameof(Index));
        }

        private bool AdSetExists(int id)
        {
            return this.adSetService.GetAdSets(null,null).Any(e => e.AdSetId == id);
        }
    }
}
