using AltinorduWebProject.Models.Managers;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Security;

namespace AltinorduWebProject.Controllers
{
    [AllowAnonymous]
    public class SecurityController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        // GET: Security
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Kullanici kullanici)
        {
            var kullaniciInDb = db.Kullanicilar.FirstOrDefault(x => x.KullaniciAdi == kullanici.KullaniciAdi && x.Sifre == kullanici.Sifre);
            if (kullaniciInDb != null)
            {
                FormsAuthentication.SetAuthCookie(kullanici.KullaniciAdi, false); // Eğer kullanıcı doğru bilgileri vermiş ise kullanıcıyı sisteme tanıt.
                return RedirectToAction("Chart", "Contact");
            }
            else
            {
                ViewBag.Result = "Kullanıcı adı veya şifre hatalı!";
                ViewBag.Status = "danger";
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Login");
        }
    }
}