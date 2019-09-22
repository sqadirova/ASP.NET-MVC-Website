using KurumsalWeb.Models.DataContext;
using KurumsalWeb.Models.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
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

            if ( login.Eposta==admin.Eposta && login.Sifre==Crypto.Hash(admin.Sifre,"MD5") )
            {
                Session["adminId"] = login.AdminId;
                Session["eposta"] = login.Eposta;
                Session["yetki"] = login.Yetki;

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

        public ActionResult ForGotPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ForGotPassword(string eposta)
        {
            var mail = db.Admin.Where(x =>x.Eposta == eposta).SingleOrDefault();
            if (mail != null)
            {
                Random rnd = new Random();
                int yenisifre = rnd.Next();
                mail.Sifre = Crypto.Hash(Convert.ToString(yenisifre),"MD5");
                db.SaveChanges();

                WebMail.SmtpServer = "smtp.gmail.com";
                WebMail.EnableSsl = true;
                WebMail.UserName = "kurumsalwebsidiqa@gmail.com";
                WebMail.Password = "Kurumsalweb16";
                WebMail.SmtpPort = 587;
                WebMail.Send(eposta, "Admin Paneli Giriş Şifrəniz", "Yeni Şifrəniz: "+ yenisifre);
                ViewBag.Mesaj = "Yeni Şifrəniz Göndərildi!";
            }
            else
            {
                ViewBag.Mesaj = "Şifrəniz göndərilərkən xəta baş verdi!";
            }

            return View();
        }
        public ActionResult AdminList()
        {
            return View(db.Admin.ToList());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Admin admin,string eposta,string sifre)
        {
            if (ModelState.IsValid)
            {
                admin.Sifre = Crypto.Hash(sifre, "MD5");
                db.Admin.Add(admin);
                db.SaveChanges();
                return RedirectToAction("AdminList");
            }

            return View(admin);
        }

        public ActionResult Edit(int id)
        {
            var a = db.Admin.Where(x =>x.AdminId == id).SingleOrDefault();
            return View(a);
        }

        [HttpPost]
        public ActionResult Edit(int id,Admin admin,string sifre,string eposta,string yetki)
        {
            if (ModelState.IsValid)
            {
                var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();
                a.Sifre = Crypto.Hash(sifre, "MD5");
                a.Eposta = eposta;
                a.Yetki = yetki;
                db.SaveChanges();
                return RedirectToAction("AdminList");

            }
            return View(admin);
        }

        public ActionResult Delete(int id)
        {
            var a = db.Admin.Where(x => x.AdminId == id).SingleOrDefault();

            if (a!=null)
            {
                db.Admin.Remove(a);
                db.SaveChanges();
                return RedirectToAction("AdminList");
            }

            return View(a);
        }
    }
}