using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KurumsalWeb.Controllers
{
    public class AdminController : Controller
    {
        KurumsalDBContext db = new KurumsalDBContext();
        // GET: Admin
        public ActionResult Index()
        {
            ViewBag.BlogSay = db.Blog.Count();
            ViewBag.KategoriSay = db.Kategori.Count();
            ViewBag.YorumSay = db.Yorum.Count();
            ViewBag.HizmetSay = db.Hizmet.Count();

            ViewBag.YorumOnay = db.Yorum.Where(x => x.Onay == false).Count();

            var sorgu = db.Kategori.ToList();
            return View(sorgu);
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Admin admin)
        {
            var login = db.Admin.Where(x => x.Eposta == admin.Eposta).SingleOrDefault();

            if (login.Eposta==admin.Eposta && login.Sifre==admin.Sifre)
            {
                Session["adminId"] = login.AdminId;
                Session["eposta"] = login.Eposta;

                return RedirectToAction("Index", "Admin");
            }

            ViewBag.Warning = "Email və ya şifrə yalnışdır!";

            return View(admin);
        }

        public ActionResult Logout()
        {
            Session["adminId"] = null;
            Session["eposta"] = null;
            Session.Abandon();

            return RedirectToAction("Login", "Admin");
        }

        public ActionResult AdminList()
        {
            return View(db.Admin.ToList());
        }
    }
}