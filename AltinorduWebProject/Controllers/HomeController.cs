using Entities.ValueObjects; 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AltinorduWebProject.Models.Managers;
using Entities;
using System.Net.Mail;
using System.Net;
using BusinessLayer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AltinorduWebProject.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public class PersonelViewModel
        {
            public int Id { get; set; }
            public string Ad { get; set; }
            public string Soyad { get; set; }
            public string Unvan { get; set; }
            [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen E-mail Adresinizi doğru formatta giriniz!")]
            public string Eposta { get; set; }
            [Required(ErrorMessage = "Bu Alanın Doldurulması Gerekmektedir!")]
            [StringLength(1000,MinimumLength = 40, ErrorMessage = "Mesajınız minimum 40 karakter olmalıdır!")]
            public string Mesaj { get; set; }
            public string MesajTuru { get; set; }
            public DateTime MesajTarihi { get; set; }
            public bool Okundu { get; set; }
            [Required(ErrorMessage = "Lütfen seçim yapınız!")]
            public int birim_Id { get; set; }
            [ForeignKey("birim_Id")]
            public virtual Birim birim { get; set; }
        }
        //[HttpPost]
        //public ActionResult Index(PersonelViewModel personel)
        //{
        //    DatabaseContext db = new DatabaseContext();


        //    Personel personelDb = new Personel();
        //    personelDb.Mesaj = personel.Mesaj;
        //    personelDb.Eposta = personel.Eposta;
        //    personelDb.Ad = personel.Ad;
        //    personelDb.Konu = personel.Konu;
        //    personelDb.Soyad = personel.Soyad;
        //    personelDb.MesajTarihi = DateTime.Now;
        //    personelDb.Rumuz = personel.Rumuz;
        //    personelDb.Okundu = false;
        //    personelDb.Unvan = personel.Unvan;
        //    personelDb.MesajTuru = personel.MesajTuru;

        //    db.Personeller.Add(personelDb);
        //    int sonuc = db.SaveChanges();

        //    if (sonuc > 0)
        //    {
        //        ViewBag.Result = "Mesajınız başarıyla gönderildi.";
        //        ViewBag.Status = "success";
        //    }
        //    else
        //    {
        //        ViewBag.Result = "Mesajınız gönderilemedi.";
        //        ViewBag.Status = "danger";
        //    }
        //    return View();
        //    //BusinessLayer.Test test = new BusinessLayer.Test();
        //}

        public ActionResult AddPersonelMessage()
        {
            var birimList = db.Birimler.ToList();
            ViewBag.BirimList = new SelectList(birimList, "Id", "PersonelBirim");
            return View();
        }
        [HttpPost]
        public ActionResult AddPersonelMessage(PersonelViewModel personel)
        {
            DatabaseContext db = new DatabaseContext();

            var birimList = db.Birimler.ToList();
            ViewBag.BirimList = new SelectList(birimList, "Id", "PersonelBirim");


            Personel personelDb = new Personel();
            personelDb.Mesaj = personel.Mesaj;
            personelDb.Eposta = personel.Eposta;
            if (personelDb.Ad != null)
            {
                personelDb.Ad = personel.Ad.ToUpper();
            }
            else
            {
                personelDb.Ad = personel.Ad;
            }
            if (personelDb.Soyad != null)
            {
                personelDb.Soyad = personel.Soyad.ToUpper();
            }
            else
            {
                personelDb.Soyad = personel.Soyad;
            }
            personelDb.MesajTarihi = DateTime.Now;
            personelDb.Okundu = false;
            personelDb.MesajTuru = personel.MesajTuru;
            personelDb.birim_Id = personel.birim_Id;

            if (!ModelState.IsValid == false)
            {
                db.Personeller.Add(personelDb);
                int sonuc = db.SaveChanges();
                if (sonuc > 0)
                {
                    ViewBag.Result = "Mesajınız başarıyla gönderildi.";
                    ViewBag.Status = "success";
                }
                else
                {
                    ViewBag.Result = "Mesajınız gönderilemedi.";
                    ViewBag.Status = "danger";
                }
                return View();
            }
            else
            {
                return View();
            }
        }            
    }
}