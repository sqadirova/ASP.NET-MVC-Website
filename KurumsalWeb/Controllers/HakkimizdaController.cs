using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class HakkimizdaController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();

        // GET: Hakkimizda
        [Route("AdminHakkimizda")]
        public ActionResult Index()
        {
            var h = db.Hakkimizda.ToList();
            return View(h);
        }
        
        //Get: Hakkimizda/Edit/2
        public ActionResult Edit(int id)
        {
            var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).FirstOrDefault();

            return View(h);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(int id,Hakkimizda hakkimizda)
        {
            if (ModelState.IsValid)
            {
                var h = db.Hakkimizda.Where(x => x.HakkimizdaId == id).SingleOrDefault();
                h.Aciklama = hakkimizda.Aciklama;
                db.SaveChanges();

                return RedirectToAction("Index");
            }
                    

            return View(hakkimizda);
        }
    }
}