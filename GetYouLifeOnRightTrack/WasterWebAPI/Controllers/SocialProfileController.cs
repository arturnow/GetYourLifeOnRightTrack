using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WasterDAL.Model;
using WasterLOB;
using WasterWebAPI.Models;

namespace WasterWebAPI.Controllers
{
    public class SocialProfileController : Controller
    {
        private readonly ISocialProfileService _socialProfileService;

        public SocialProfileController(ISocialProfileService socialProfileService)
        {
            if (socialProfileService == null) throw new ArgumentNullException("socialProfileService");
            _socialProfileService = socialProfileService;
        }

        public ActionResult Index()
        {
            SocialProfileModel[] model = _socialProfileService.GetAll().Select(MapSocialProfileToSocialProfileModel).ToArray();

            return View(model);
        }

        private SocialProfileModel MapSocialProfileToSocialProfileModel(SocialProfile profile)
        {
            return new SocialProfileModel
            {
                Nickname =  profile.Nickname,
                Id = profile.Id
            };

        }

        //
        // GET: /SocialProfile/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SocialProfile/Create

        public ActionResult Create()
        {
            return View();
        }

        //
        // POST: /SocialProfile/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SocialProfile/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /SocialProfile/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SocialProfile/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /SocialProfile/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
