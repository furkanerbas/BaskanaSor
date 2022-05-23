using AltinorduWebProject.Models;
using AltinorduWebProject.Models.Managers;
using Entities;
using PagedList;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace AltinorduWebProject.Controllers
{
    public class ContactController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: Contact
        //MESAJ İŞLEMLERİ BAŞLANGIÇ
        public ActionResult Index(string ara, int p = 1)
        {
            var searchList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToPagedList(p, 10);
            return View(searchList);           
        }
        public ActionResult ChartIndex(string ara, int p = 1, string chart = null, string basTarih = null, string bitTarih = null)
        {
            ViewBag.menuitem = "";

            if (basTarih == "" || bitTarih == "")
            {

                if (chart == null)
                {
                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "İstek")
                {
                    ViewBag.menuitem = "İstek";

                    var q = db.Personeller.Where(x => x.MesajTuru == "İstek");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Teşekkür")
                {
                    ViewBag.menuitem = "Teşekkür";

                    var q = db.Personeller.Where(x => x.MesajTuru == "Teşekkür");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Şikayet")
                {
                    ViewBag.menuitem = "Şikayet";

                    var q = db.Personeller.Where(x => x.MesajTuru == "Şikayet");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }
            }
                else
                {
                    DateTime baslangicTarih = Convert.ToDateTime(basTarih);
                    DateTime bitisTarih = Convert.ToDateTime(bitTarih);
                    bitisTarih = bitisTarih.AddDays(1).AddSeconds(-1);
                    if (chart == null)
                    {
                        var q = db.Personeller
                       .Where(n => n.MesajTarihi >= baslangicTarih)
                       .Where(n => n.MesajTarihi <= bitisTarih);

                        //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                        var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                        return View(searchList);
                    }
                    else if (chart == "İstek")
                    {
                    ViewBag.menuitem = "İstek";

                    var q = db.Personeller
                            .Where(n => n.MesajTarihi >= baslangicTarih)
                            .Where(n => n.MesajTarihi <= bitisTarih)
                            .Where(n => n.MesajTuru == "İstek");
                        var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                        return View(searchList);
                    }
                    else if (chart == "Teşekkür")
                    {
                    ViewBag.menuitem = "Teşekkür";

                    var q = db.Personeller
                            .Where(n => n.MesajTarihi >= baslangicTarih)
                            .Where(n => n.MesajTarihi <= bitisTarih)
                            .Where(n => n.MesajTuru == "Teşekkür");
                        var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                        return View(searchList);
                    }
                    else if (chart == "Şikayet")
                    {
                    ViewBag.menuitem = "Şikayet";

                    var q = db.Personeller
                            .Where(n => n.MesajTarihi >= baslangicTarih)
                            .Where(n => n.MesajTarihi <= bitisTarih)
                            .Where(n => n.MesajTuru == "Şikayet");
                        var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                        return View(searchList);
                    }
                else
                {
                    return View();
                }

            }

        }
        public ActionResult ChartMessage(string ara, int p = 1, string chart = null, string basTarih = null, string bitTarih = null)
        {
            ViewBag.menuitem="";
            if (basTarih == "" || bitTarih == "")
            {
                if (chart == null)
                {
                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Cevaplanan Mesajlar")
                {
                    ViewBag.menuitem = "Cevaplanan Mesajlar";
                    var q = db.Personeller.Where(x => x.Okundu == true);
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Cevaplanmayan Mesajlar")
                {
                    ViewBag.menuitem = "Cevaplanmayan Mesajlar";
                    var q = db.Personeller.Where(x => x.Okundu == false);
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                DateTime baslangicTarih = Convert.ToDateTime(basTarih);
                DateTime bitisTarih = Convert.ToDateTime(bitTarih);
                bitisTarih = bitisTarih.AddDays(1).AddSeconds(-1);
                if (chart == null)
                {
                    var q = db.Personeller
                   .Where(n => n.MesajTarihi >= baslangicTarih)
                   .Where(n => n.MesajTarihi <= bitisTarih);

                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Cevaplanan Mesajlar")
                {
                    ViewBag.menuitem = "Cevaplanan Mesajlar";

                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(n => n.Okundu == true);
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Cevaplanmayan Mesajlar")
                {
                    ViewBag.menuitem = "Cevaplanmayan Mesajlar";

                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(n => n.Okundu == false);
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }

            }

        }
        public ActionResult ChartMessageOk(string ara, int p = 1, string chart = null, string basTarih = null, string bitTarih = null)
        {
            ViewBag.menuitem = "";

            if (basTarih == "" || bitTarih == "")
            {
                if (chart == null)
                {
                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "İstek")
                {
                    ViewBag.menuitem = "İstek";

                    var q = db.Personeller
                        .Where(x => x.Okundu == true)
                        .Where(x => x.MesajTuru == "İstek");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Teşekkür")
                {
                    ViewBag.menuitem = "Teşekkür";

                    var q = db.Personeller
                        .Where(x => x.Okundu == true)
                        .Where(x => x.MesajTuru == "Teşekkür");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Şikayet")
                {
                    ViewBag.menuitem = "Şikayet";

                    var q = db.Personeller
                        .Where(x => x.Okundu == true)
                        .Where(x => x.MesajTuru == "Şikayet");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                DateTime baslangicTarih = Convert.ToDateTime(basTarih);
                DateTime bitisTarih = Convert.ToDateTime(bitTarih);
                bitisTarih = bitisTarih.AddDays(1).AddSeconds(-1);
                if (chart == null)
                {
                    var q = db.Personeller
                   .Where(n => n.MesajTarihi >= baslangicTarih)
                   .Where(n => n.MesajTarihi <= bitisTarih);

                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "İstek")
                {
                    ViewBag.menuitem = "İstek";

                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(x => x.Okundu == true)
                        .Where(n => n.MesajTuru == "İstek");
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Teşekkür")
                {
                    ViewBag.menuitem = "Teşekkür";

                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(x => x.Okundu == true)
                        .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Şikayet")
                {
                    ViewBag.menuitem = "Şikayet";

                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(x => x.Okundu == true)
                        .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }

            }

        }
        public ActionResult ChartMessageFalse(string ara, int p = 1, string chart = null, string basTarih = null, string bitTarih = null)
        {
            ViewBag.menuitem = "";

            if (basTarih == "" || bitTarih == "")
            {
                if (chart == null)
                {
                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "İstek")
                {
                    ViewBag.menuitem = "İstek";

                    var q = db.Personeller
                        .Where(x => x.Okundu == false)
                        .Where(x => x.MesajTuru == "İstek");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Teşekkür")
                {
                    ViewBag.menuitem = "Teşekkür";

                    var q = db.Personeller
                        .Where(x => x.Okundu == false)
                        .Where(x => x.MesajTuru == "Teşekkür");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Şikayet")
                {
                    ViewBag.menuitem = "Şikayet";

                    var q = db.Personeller
                        .Where(x => x.Okundu == false)
                        .Where(x => x.MesajTuru == "Şikayet");
                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }
            }
            else
            {
                DateTime baslangicTarih = Convert.ToDateTime(basTarih);
                DateTime bitisTarih = Convert.ToDateTime(bitTarih);
                bitisTarih = bitisTarih.AddDays(1).AddSeconds(-1);
                if (chart == null)
                {
                    var q = db.Personeller
                   .Where(n => n.MesajTarihi >= baslangicTarih)
                   .Where(n => n.MesajTarihi <= bitisTarih);

                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "İstek")
                {
                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(x => x.Okundu == false)
                        .Where(n => n.MesajTuru == "İstek");
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Teşekkür")
                {
                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(x => x.Okundu == false)
                        .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Şikayet")
                {
                    var q = db.Personeller
                        .Where(n => n.MesajTarihi >= baslangicTarih)
                        .Where(n => n.MesajTarihi <= bitisTarih)
                        .Where(x => x.Okundu == false)
                        .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = q.Where(n => n.MesajTuru.Contains(ara) || ara == null).OrderByDescending(n => n.Id).ToList();
                    return View(searchList);
                }
                else
                {
                    return View();
                }

            }

        }
        public ActionResult ChartMounthList(string ara, int p = 1, string chart = null, string column = null, string basTarih = null, string bitTarih = null)
        {
            ViewBag.menuitem = "";

            if (chart == null)
                {
                    //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
                    var searchList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ocak" && column =="İstek")
                {
                ViewBag.menuitem = "Ocak Ayı İstek Türündeki Mesajlar";

                DateTime ocakbas = new DateTime(2021, 01, 01);
                    DateTime ocakbit = new DateTime(2021, 01, 31);
                    var q = db.Personeller
                   .Where(n => n.MesajTarihi >= ocakbas)
                   .Where(n => n.MesajTarihi <= ocakbit)
                   .Where(n => n.MesajTuru == "İstek");

                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ocak" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Ocak Ayı Teşekkür Türündeki Mesajlar";

                DateTime ocakbas = new DateTime(2021, 01, 01);
                    DateTime ocakbit = new DateTime(2021, 01, 31);
                    var q = db.Personeller
                   .Where(n => n.MesajTarihi >= ocakbas)
                   .Where(n => n.MesajTarihi <= ocakbit)
                   .Where(n => n.MesajTuru == "Teşekkür");

                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ocak" && column == "Şikayet")
                {
                ViewBag.menuitem = "Ocak Ayı Şikayet Türündeki Mesajlar";

                DateTime ocakbas = new DateTime(2021, 01, 01);
                    DateTime ocakbit = new DateTime(2021, 01, 31);
                    var q = db.Personeller
                   .Where(n => n.MesajTarihi >= ocakbas)
                   .Where(n => n.MesajTarihi <= ocakbit)
                   .Where(n => n.MesajTuru == "Şikayet");

                    var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }

                else if (chart == "Şubat" && column == "İstek")
                    {
                        ViewBag.menuitem = "Şubat Ayı İstek Türündeki Mesajlar";

                 DateTime subatbas = new DateTime(2021, 02, 01);
                        DateTime subatbit = new DateTime(2021, 02, 28);
                        var qsubat = db.Personeller
                       .Where(n => n.MesajTarihi >= subatbas)
                       .Where(n => n.MesajTarihi <= subatbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qsubat.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Şubat" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Şubat Ayı Teşekkür Türündeki Mesajlar";

                DateTime subatbas = new DateTime(2021, 02, 01);
                    DateTime subatbit = new DateTime(2021, 02, 28);
                    var qsubat = db.Personeller
                   .Where(n => n.MesajTarihi >= subatbas)
                   .Where(n => n.MesajTarihi <= subatbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qsubat.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Şubat" && column == "Şikayet")
                {
                ViewBag.menuitem = "Şubat Ayı Şikayet Türündeki Mesajlar";

                DateTime subatbas = new DateTime(2021, 02, 01);
                    DateTime subatbit = new DateTime(2021, 02, 28);
                    var qsubat = db.Personeller
                   .Where(n => n.MesajTarihi >= subatbas)
                   .Where(n => n.MesajTarihi <= subatbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qsubat.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Mart" && column == "İstek")
                    {
                ViewBag.menuitem = "Mart Ayı İstek Türündeki Mesajlar";

                DateTime martbas = new DateTime(2021, 03, 01);
                        DateTime martbit = new DateTime(2021, 03, 31);
                        var qmart = db.Personeller
                       .Where(n => n.MesajTarihi >= martbas)
                       .Where(n => n.MesajTarihi <= martbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qmart.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Mart" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Mart Ayı Teşekkür Türündeki Mesajlar";

                DateTime martbas = new DateTime(2021, 03, 01);
                    DateTime martbit = new DateTime(2021, 03, 31);
                    var qmart = db.Personeller
                   .Where(n => n.MesajTarihi >= martbas)
                   .Where(n => n.MesajTarihi <= martbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qmart.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Mart" && column == "Şikayet")
                {
                ViewBag.menuitem = "Mart Ayı Şikayet Türündeki Mesajlar";

                DateTime martbas = new DateTime(2021, 03, 01);
                    DateTime martbit = new DateTime(2021, 03, 31);
                    var qmart = db.Personeller
                   .Where(n => n.MesajTarihi >= martbas)
                   .Where(n => n.MesajTarihi <= martbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qmart.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Nisan" && column == "İstek")
                    {
                ViewBag.menuitem = "Nisan Ayı İstek Türündeki Mesajlar";

                DateTime nisanbas = new DateTime(2021, 04, 01);
                        DateTime nisanbit = new DateTime(2021, 04, 30);
                        var qnisan = db.Personeller
                       .Where(n => n.MesajTarihi >= nisanbas)
                       .Where(n => n.MesajTarihi <= nisanbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qnisan.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Nisan" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Nisan Ayı Teşekkür Türündeki Mesajlar";

                DateTime nisanbas = new DateTime(2021, 04, 01);
                    DateTime nisanbit = new DateTime(2021, 04, 30);
                    var qnisan = db.Personeller
                   .Where(n => n.MesajTarihi >= nisanbas)
                   .Where(n => n.MesajTarihi <= nisanbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qnisan.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Nisan" && column == "Şikayet")
                {
                ViewBag.menuitem = "Nisan Ayı Şikayet Türündeki Mesajlar";

                DateTime nisanbas = new DateTime(2021, 04, 01);
                    DateTime nisanbit = new DateTime(2021, 04, 30);
                    var qnisan = db.Personeller
                   .Where(n => n.MesajTarihi >= nisanbas)
                   .Where(n => n.MesajTarihi <= nisanbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qnisan.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Mayıs" && column == "İstek")
                    {
                    ViewBag.menuitem = "Mayıs Ayı İstek Türündeki Mesajlar";

                DateTime mayisbas = new DateTime(2021, 05, 01);
                        DateTime mayisbit = new DateTime(2021, 05, 31);
                        var qmayis = db.Personeller
                       .Where(n => n.MesajTarihi >= mayisbas)
                       .Where(n => n.MesajTarihi <= mayisbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qmayis.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Mayıs" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Mayıs Ayı Teşekkür Türündeki Mesajlar";

                DateTime mayisbas = new DateTime(2021, 05, 01);
                    DateTime mayisbit = new DateTime(2021, 05, 31);
                    var qmayis = db.Personeller
                   .Where(n => n.MesajTarihi >= mayisbas)
                   .Where(n => n.MesajTarihi <= mayisbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qmayis.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Mayıs" && column == "Şikayet")
                {
                ViewBag.menuitem = "Mayıs Ayı Şikayet Türündeki Mesajlar";

                DateTime mayisbas = new DateTime(2021, 05, 01);
                    DateTime mayisbit = new DateTime(2021, 05, 31);
                    var qmayis = db.Personeller
                   .Where(n => n.MesajTarihi >= mayisbas)
                   .Where(n => n.MesajTarihi <= mayisbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qmayis.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Haziran" && column == "İstek")
                    {
                ViewBag.menuitem = "Haziran Ayı İstek Türündeki Mesajlar";

                DateTime haziranbas = new DateTime(2021, 06, 01);
                        DateTime haziranbit = new DateTime(2021, 06, 30);
                        var qhaziran = db.Personeller
                       .Where(n => n.MesajTarihi >= haziranbas)
                       .Where(n => n.MesajTarihi <= haziranbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qhaziran.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Haziran" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Haziran Ayı Teşekkür Türündeki Mesajlar";

                DateTime haziranbas = new DateTime(2021, 06, 01);
                    DateTime haziranbit = new DateTime(2021, 06, 30);
                    var qhaziran = db.Personeller
                   .Where(n => n.MesajTarihi >= haziranbas)
                   .Where(n => n.MesajTarihi <= haziranbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qhaziran.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Haziran" && column == "Şikayet")
                {
                ViewBag.menuitem = "Haziran Ayı Şikayet Türündeki Mesajlar";

                DateTime haziranbas = new DateTime(2021, 06, 01);
                    DateTime haziranbit = new DateTime(2021, 06, 30);
                    var qhaziran = db.Personeller
                   .Where(n => n.MesajTarihi >= haziranbas)
                   .Where(n => n.MesajTarihi <= haziranbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qhaziran.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Temmuz" && column == "İstek")
                    {
                ViewBag.menuitem = "Temmuz Ayı İstek Türündeki Mesajlar";

                DateTime temmuzbas = new DateTime(2021, 07, 01);
                        DateTime temmuzbit = new DateTime(2021, 07, 31);
                        var qtemmuz = db.Personeller
                       .Where(n => n.MesajTarihi >= temmuzbas)
                       .Where(n => n.MesajTarihi <= temmuzbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qtemmuz.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Temmuz" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Temmuz Ayı Teşekkür Türündeki Mesajlar";

                DateTime temmuzbas = new DateTime(2021, 07, 01);
                    DateTime temmuzbit = new DateTime(2021, 07, 31);
                    var qtemmuz = db.Personeller
                   .Where(n => n.MesajTarihi >= temmuzbas)
                   .Where(n => n.MesajTarihi <= temmuzbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qtemmuz.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Temmuz" && column == "Şikayet")
                {
                ViewBag.menuitem = "Temmuz Ayı Şikayet Türündeki Mesajlar";

                DateTime temmuzbas = new DateTime(2021, 07, 01);
                    DateTime temmuzbit = new DateTime(2021, 07, 31);
                    var qtemmuz = db.Personeller
                   .Where(n => n.MesajTarihi >= temmuzbas)
                   .Where(n => n.MesajTarihi <= temmuzbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qtemmuz.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ağustos" && column == "İstek")
                    {
                ViewBag.menuitem = "Ağustos Ayı İstek Türündeki Mesajlar";

                DateTime agustosbas = new DateTime(2021, 08, 01);
                        DateTime agustosbit = new DateTime(2021, 08, 31);
                        var qagustos = db.Personeller
                       .Where(n => n.MesajTarihi >= agustosbas)
                       .Where(n => n.MesajTarihi <= agustosbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qagustos.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Ağustos" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Ağustos Ayı Teşekkür Türündeki Mesajlar";

                DateTime agustosbas = new DateTime(2021, 08, 01);
                    DateTime agustosbit = new DateTime(2021, 08, 31);
                    var qagustos = db.Personeller
                   .Where(n => n.MesajTarihi >= agustosbas)
                   .Where(n => n.MesajTarihi <= agustosbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qagustos.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ağustos" && column == "Şikayet")
                {
                ViewBag.menuitem = "Ağustos Ayı Şikayet Türündeki Mesajlar";

                DateTime agustosbas = new DateTime(2021, 08, 01);
                    DateTime agustosbit = new DateTime(2021, 08, 31);
                    var qagustos = db.Personeller
                   .Where(n => n.MesajTarihi >= agustosbas)
                   .Where(n => n.MesajTarihi <= agustosbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qagustos.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Eylül" && column == "İstek")
                    {
                ViewBag.menuitem = "Eylül Ayı İstek Türündeki Mesajlar";

                DateTime eylulbas = new DateTime(2021, 09, 01);
                        DateTime eylulbit = new DateTime(2021, 09, 30);
                        var qeylul = db.Personeller
                       .Where(n => n.MesajTarihi >= eylulbas)
                       .Where(n => n.MesajTarihi <= eylulbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qeylul.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Eylül" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Eylül Ayı Teşekkür Türündeki Mesajlar";

                DateTime eylulbas = new DateTime(2021, 09, 01);
                    DateTime eylulbit = new DateTime(2021, 09, 30);
                    var qeylul = db.Personeller
                   .Where(n => n.MesajTarihi >= eylulbas)
                   .Where(n => n.MesajTarihi <= eylulbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qeylul.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Eylül" && column == "Şikayet")
                {
                ViewBag.menuitem = "Eylül Ayı Şikayet Türündeki Mesajlar";

                DateTime eylulbas = new DateTime(2021, 09, 01);
                    DateTime eylulbit = new DateTime(2021, 09, 30);
                    var qeylul = db.Personeller
                   .Where(n => n.MesajTarihi >= eylulbas)
                   .Where(n => n.MesajTarihi <= eylulbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qeylul.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ekim" && column == "İstek")
                    {
                ViewBag.menuitem = "Ekim Ayı İstek Türündeki Mesajlar";

                DateTime ekimbas = new DateTime(2021, 10, 01);
                        DateTime ekimbit = new DateTime(2021, 10, 31);
                        var qekim = db.Personeller
                       .Where(n => n.MesajTarihi >= ekimbas)
                       .Where(n => n.MesajTarihi <= ekimbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qekim.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Ekim" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Ekim Ayı Teşekkür Türündeki Mesajlar";

                DateTime ekimbas = new DateTime(2021, 10, 01);
                    DateTime ekimbit = new DateTime(2021, 10, 31);
                    var qekim = db.Personeller
                   .Where(n => n.MesajTarihi >= ekimbas)
                   .Where(n => n.MesajTarihi <= ekimbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qekim.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Ekim" && column == "Şikayet")
                {
                ViewBag.menuitem = "Ekim Ayı Şikayet Türündeki Mesajlar";

                DateTime ekimbas = new DateTime(2021, 10, 01);
                    DateTime ekimbit = new DateTime(2021, 10, 31);
                    var qekim = db.Personeller
                   .Where(n => n.MesajTarihi >= ekimbas)
                   .Where(n => n.MesajTarihi <= ekimbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qekim.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Kasım" && column == "İstek")
                    {
                ViewBag.menuitem = "Kasım Ayı İstek Türündeki Mesajlar";

                DateTime kasimbas = new DateTime(2021, 11, 01);
                        DateTime kasimbit = new DateTime(2021, 11, 30);
                        var qkasim = db.Personeller
                       .Where(n => n.MesajTarihi >= kasimbas)
                       .Where(n => n.MesajTarihi <= kasimbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qkasim.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Kasım" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Kasım Ayı Teşekkür Türündeki Mesajlar";

                DateTime kasimbas = new DateTime(2021, 11, 01);
                    DateTime kasimbit = new DateTime(2021, 11, 30);
                    var qkasim = db.Personeller
                   .Where(n => n.MesajTarihi >= kasimbas)
                   .Where(n => n.MesajTarihi <= kasimbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qkasim.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Kasım" && column == "Şikayet")
                {
                ViewBag.menuitem = "Kasım Ayı Şikayet Türündeki Mesajlar";

                DateTime kasimbas = new DateTime(2021, 11, 01);
                    DateTime kasimbit = new DateTime(2021, 11, 30);
                    var qkasim = db.Personeller
                   .Where(n => n.MesajTarihi >= kasimbas)
                   .Where(n => n.MesajTarihi <= kasimbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qkasim.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Aralık" && column == "İstek")
                    {
                    ViewBag.menuitem = "Aralık Ayı İstek Türündeki Mesajlar";

                    DateTime aralikbas = new DateTime(2021, 12, 01);
                        DateTime aralikbit = new DateTime(2021, 12, 31);
                        var qaralik = db.Personeller
                       .Where(n => n.MesajTarihi >= aralikbas)
                       .Where(n => n.MesajTarihi <= aralikbit)
                       .Where(n => n.MesajTuru == "İstek");
                        var searchList = qaralik.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                        return View(searchList);
                    }
                else if (chart == "Aralık" && column == "Teşekkür")
                {
                ViewBag.menuitem = "Aralık Ayı Teşekkür Türündeki Mesajlar";

                DateTime aralikbas = new DateTime(2021, 12, 01);
                    DateTime aralikbit = new DateTime(2021, 12, 31);
                    var qaralik = db.Personeller
                   .Where(n => n.MesajTarihi >= aralikbas)
                   .Where(n => n.MesajTarihi <= aralikbit)
                   .Where(n => n.MesajTuru == "Teşekkür");
                    var searchList = qaralik.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
                else if (chart == "Aralık" && column == "Şikayet")
                {
                ViewBag.menuitem = "Aralık Ayı Şikayet Türündeki Mesajlar";

                DateTime aralikbas = new DateTime(2021, 12, 01);
                    DateTime aralikbit = new DateTime(2021, 12, 31);
                    var qaralik = db.Personeller
                   .Where(n => n.MesajTarihi >= aralikbas)
                   .Where(n => n.MesajTarihi <= aralikbit)
                   .Where(n => n.MesajTuru == "Şikayet");
                    var searchList = qaralik.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToList();
                    return View(searchList);
                }
            else
                {
                    return View();
                }
            }
        public ActionResult ChartDetailMessage(int id)
        {
            Personel chartpersonel = new Personel();
            chartpersonel = db.Personeller.Find(id);
            return PartialView("ChartDetail", chartpersonel);
        }


        public ActionResult Message(string ara, int p = 1)
        {
            //var result = db.Personeller.Where(x => x.Okundu == false).ToList();
            //return View(result);
            var answerokList = db.Personeller.Where(x=> x.MesajTuru.Contains(ara) || ara == null && x.Okundu == false).OrderByDescending(x => x.Id).ToPagedList(p, 10);

            return View(answerokList);

        }
        public ActionResult MessageOk(string ara, int p = 1)
        {
            //var ansver = db.Personeller.Where(x => x.Okundu == true).ToList();
            //return View(ansver);
            var answerList = db.Personeller.Where(x => x.MesajTuru.Contains(ara) || ara == null && x.Okundu == true).OrderByDescending(x => x.Id).ToPagedList(p, 10);

            return View(answerList);
        }
        public ActionResult Detail(int id)
        {
            var birimList = db.Birimler.ToList();
            ViewBag.BirimList = new SelectList(birimList, "Eposta", "PersonelBirim");

            var mesaj = db.Personeller.SingleOrDefault(x => x.Id.Equals(id));
            if (mesaj == null)
            {
                throw new Exception("Mesaj bulunamadı!.");
            }
            return View(mesaj);
        }
        //BİRİME HAVALE ET MODAL POPUP
        [HttpPost]
        public ActionResult Detail(MesajYanitViewModel mesajYanit, string Eposta, string subject, string Mesaj,int id,int Id)
        {
            var mesaj = db.Personeller.SingleOrDefault(x => x.Id.Equals(id));
            try
            {
                var birimList = db.Birimler.ToList();
                ViewBag.BirimList = new SelectList(birimList, "Eposta", "PersonelBirim");

                if (mesaj == null)
                {
                    throw new Exception("Mesaj bulunamadı!.");
                }


                if (ModelState.IsValid)
                {
                    subject = "Birim Amirlerinin Dikkatine";
                    var senderEmail = new MailAddress("furkan52250@gmail.com", "");
                    var receivereEmail = new MailAddress(Eposta, "Receiver");
                    var password = "Galatasaray1905";
                    var sub = subject;
                    var body = Mesaj;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receivereEmail)
                    {
                        Subject = subject,
                        Body = Mesaj,
                    })
                    {
                        smtp.Send(mess);
                    }

                    Personel personel = db.Personeller.Where(x => x.Id == Id).FirstOrDefault();
                    personel.Okundu = true;
                    db.SaveChanges();

                    ViewBag.Result = "Mesajınız başarıyla gönderildi.";
                    ViewBag.Status = "success";

                    MesajYanit mesajYanitdb = new MesajYanit();
                    mesajYanitdb.Eposta = mesajYanit.receiverEmail;
                    mesajYanitdb.Konu = mesajYanit.subject;
                    mesajYanitdb.Mesaj = mesajYanit.message;
                    mesajYanitdb.MesajTarihi = DateTime.Now;

                    db.MesajYanitlari.Add(mesajYanitdb);
                    db.SaveChanges();

                    return View(mesaj);
                }
            }
            catch (Exception)
            {
                ViewBag.Result = "Mesajınız gönderilemedi.";
                ViewBag.Status = "danger";
            }
            return View(mesaj);

        }
        public PartialViewResult MessageListMenu()
        {
            return PartialView();
        }
        public PartialViewResult ChartMessageListMenu()
        {
            return PartialView();
        }
        public PartialViewResult MessageBoxPartial(string ara, int p = 1)
        {
            var q = db.Personeller.Where(x => x.Okundu == false);
            var searchList = q.Where(x => x.MesajTuru.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToPagedList(p, 10);

            return PartialView(searchList);
        }
        public class MesajYanitViewModel
        {
            public int Id { get; set; }
            public string receiverEmail { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }
        public class MesajHavaleViewModel
        {
            public int Id { get; set; }
            public string receiverEmail { get; set; }
            public string subject { get; set; }
            public string message { get; set; }
        }
        public ActionResult SendMail(string email, int messageId)
        {
            ViewBag.email = email;
            ViewBag.messageId = messageId;
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(MesajHavaleViewModel mesajHavale, string receiverEmail, string subject, string message, int? messageId = null)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("furkan52250@gmail.com", "");
                    var receivereEmail = new MailAddress(receiverEmail, "Receiver");
                    var password = "Galatasaray1905";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receivereEmail)
                    {
                        Subject = subject,
                        Body = message
                    })
                    {
                        smtp.Send(mess);
                    }

                    Personel personel = db.Personeller.Where(x => x.Id == messageId).FirstOrDefault();
                    personel.Okundu = true;

                    db.SaveChanges();
                    
                    ViewBag.Result = "Mesajınız başarıyla gönderildi.";
                    ViewBag.Status = "success";

                    MesajHavale mesajHavaledb = new MesajHavale();
                    mesajHavaledb.Eposta = mesajHavale.receiverEmail;
                    mesajHavaledb.Konu = mesajHavale.subject;
                    mesajHavaledb.Mesaj = mesajHavale.message;
                    mesajHavaledb.MesajTarihi = DateTime.Now;

                    db.MesajlarHavale.Add(mesajHavaledb);
                    db.SaveChanges();

                    return View();
                }
            }
            catch (Exception)
            {
                
                    ViewBag.Result = "Mesajınız gönderilemedi.";
                    ViewBag.Status = "danger";             
            }
            return View();
        }
        public ActionResult NewMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult NewMessage(string receiverEmail, string subject, string message)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var senderEmail = new MailAddress("furkan52250@gmail.com", "");
                    var receivereEmail = new MailAddress(receiverEmail, "Receiver");
                    var password = "Galatasaray1905";
                    var sub = subject;
                    var body = message;
                    var smtp = new SmtpClient
                    {
                        Host = "smtp.gmail.com",
                        Port = 587,
                        EnableSsl = true,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(senderEmail.Address, password)
                    };
                    using (var mess = new MailMessage(senderEmail, receivereEmail)
                    {
                        Subject = subject,
                        Body = message
                    })
                    {
                        smtp.Send(mess);
                    }

                    ViewBag.Result = "Mesajınız başarıyla gönderildi.";
                    ViewBag.Status = "success";

                    return View();

                }
            }
            catch (Exception)
            {

                ViewBag.Result = "Mesajınız gönderilemedi.";
                ViewBag.Status = "danger";
            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteDetailMessage(int? id)
        {

            var silinecekMesaj = db.Personeller.Where(x => x.Id == id).FirstOrDefault();

            db.Personeller.Remove(silinecekMesaj);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        //MESAJ İŞLEMLERİ BİTİŞ


        //GRAFİK OLUŞTURMA
        public ActionResult Chart()
        {
            return View();
        }
        public ActionResult ChartReport()
        {
            return View();
        }
        public ActionResult MessageCount()
        {
            int mesajSayisi = db.Personeller.Count();
            int okunanMesaj = db.Personeller.Where(x => x.Okundu == false).Count();
            int okunmayanMesaj = db.Personeller.Where(x => x.Okundu == true).Count();
            return View();
        }
        public ActionResult Yeni()
        {
            
            return View();
        }
        
        //MESAJ - DURUM GRAFİĞİ
        public ActionResult VisualizeMessageResult(string baslangic,string bitis)
        {
            if (Request.QueryString["baslangic"] == null || Request.QueryString["baslangic"] == "")
            {
                return Json(ProList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime b1 = Convert.ToDateTime(Request.QueryString["baslangic"].ToString());
                DateTime b2 = Convert.ToDateTime(Request.QueryString["bitis"].ToString());
                return Json(ProList(b1, b2), JsonRequestBehavior.AllowGet);
            }
        }         
        public List<MesajDurum> ProList(DateTime baslangic,DateTime bitis)
        {
            bitis = bitis.AddDays(1).AddSeconds(-1);
            var q = db.Personeller
           .Where(n => n.MesajTarihi >= baslangic)
           .Where(n => n.MesajTarihi <= bitis);

            int istek = q.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkur = q.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayet = q.Where(x => x.MesajTuru == "Şikayet").Count();
            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "İstek",
                msjSayi = istek

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Teşekkür",
                msjSayi = tesekkur

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şikayet",
                msjSayi = sikayet

            });
            return md;

        }
        public List<MesajDurum> ProList()
        {
            int istek = db.Personeller.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkur = db.Personeller.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayet = db.Personeller.Where(x => x.MesajTuru == "Şikayet").Count();
            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "İstek",
                msjSayi = istek

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Teşekkür",
                msjSayi = tesekkur

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şikayet",
                msjSayi = sikayet

            });
            return md;

        }
        //MESAJ - DURUM GRAFİĞİ BİTİŞ

        //MESAJ CEVAP GRAFİĞİ

        public ActionResult VisualizeMessageResultNew(string baslangic,string bitis)
        {
            if (Request.QueryString["baslangic"] == null || Request.QueryString["baslangic"] == "")
            {
                return Json(ProListNew(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime b1 = Convert.ToDateTime(Request.QueryString["baslangic"].ToString());
                DateTime b2 = Convert.ToDateTime(Request.QueryString["bitis"].ToString());
                return Json(ProListNew(b1, b2), JsonRequestBehavior.AllowGet);
            }
        }
        public List<MesajDurum> ProListNew(DateTime baslangic,DateTime bitis)
        {
            bitis = bitis.AddDays(1).AddSeconds(-1);
            var q = db.Personeller
           .Where(n => n.MesajTarihi >= baslangic)
           .Where(n => n.MesajTarihi <= bitis);

            int okunanMesajlar = q.Where(x => x.Okundu == true).Count();
            int okunmayanMesajlar = q.Where(x => x.Okundu == false).Count();

            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "Cevaplanan Mesajlar",
                msjSayi = okunanMesajlar

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Cevaplanmayan Mesajlar",
                msjSayi = okunmayanMesajlar

            });
            return md;

        }
        public List<MesajDurum> ProListNew()
        {
            int okunanMesajlar = db.Personeller.Where(x => x.Okundu == true).Count();
            int okunmayanMesajlar = db.Personeller.Where(x => x.Okundu == false).Count();

            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "Cevaplanan Mesajlar",
                msjSayi = okunanMesajlar
            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Cevaplanmayan Mesajlar",
                msjSayi = okunmayanMesajlar

            });
            return md;

        }

        //MESAJ CEVAP GRAFİĞİ BİTİŞ

        //OKUNAN MESAJLAR GRAFİĞİ
        public ActionResult VisualizeMessageResultTrue(string baslangic, string bitis)
        {
            if (Request.QueryString["baslangic"] == null || Request.QueryString["baslangic"] == "")
            {
                return Json(ProListTrue(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime b1 = Convert.ToDateTime(Request.QueryString["baslangic"].ToString());
                DateTime b2 = Convert.ToDateTime(Request.QueryString["bitis"].ToString());
                return Json(ProListTrue(b1, b2), JsonRequestBehavior.AllowGet);
            }
        }
        public List<MesajDurum> ProListTrue(DateTime baslangic, DateTime bitis)
        {
            bitis = bitis.AddDays(1).AddSeconds(-1);
            var q = db.Personeller
           .Where(n => n.MesajTarihi >= baslangic)
           .Where(n => n.MesajTarihi <= bitis);

            int istek = q.Where(x => x.Okundu == true && x.MesajTuru == "İstek").Count();
            int tesekkur = q.Where(x => x.Okundu == true && x.MesajTuru == "Teşekkür").Count();
            int sikayet = q.Where(x => x.Okundu == true && x.MesajTuru == "Şikayet").Count();
            int istekok = q.Where(x => x.Okundu == false && x.MesajTuru == "İstek").Count();
            int tesekkurok = q.Where(x => x.Okundu == false && x.MesajTuru == "Teşekkür").Count();
            int sikayetok = q.Where(x => x.Okundu == false && x.MesajTuru == "Şikayet").Count();
            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "İstek",
                msjSayi = istek,
                msjSayiCevaplanmadi = istekok
            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Teşekkür",
                msjSayi = tesekkur,
                msjSayiCevaplanmadi = tesekkurok

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şikayet",
                msjSayi = sikayet,
                msjSayiCevaplanmadi = sikayetok

            });
            return md;

        }
        public List<MesajDurum> ProListTrue()
        {
            int istek = db.Personeller.Where(x => x.Okundu == true && x.MesajTuru == "İstek").Count();
            int tesekkur = db.Personeller.Where(x => x.Okundu == true && x.MesajTuru == "Teşekkür").Count();
            int sikayet = db.Personeller.Where(x => x.Okundu == true && x.MesajTuru == "Şikayet").Count();
            int istekok = db.Personeller.Where(x => x.Okundu == false && x.MesajTuru == "İstek").Count();
            int tesekkurok = db.Personeller.Where(x => x.Okundu == false && x.MesajTuru == "Teşekkür").Count();
            int sikayetok = db.Personeller.Where(x => x.Okundu == false && x.MesajTuru == "Şikayet").Count();

            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "İstek",
                msjSayi = istek,
                msjSayiCevaplanmadi = istekok
            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Teşekkür",
                msjSayi = tesekkur,
                msjSayiCevaplanmadi = tesekkurok
            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şikayet",
                msjSayi = sikayet,
                msjSayiCevaplanmadi = sikayetok
            });
            return md;

        }

        //OKUNAN MESAJLAR GRAFİĞİ BİTİŞ

        //OKUNMAYAN MESAJLAR GRAFİĞİ
        public ActionResult VisualizeMessageResultFalse(string baslangic, string bitis)
        {
            if (Request.QueryString["baslangic"] == null || Request.QueryString["baslangic"] == "")
            {
                return Json(ProListFalse(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                DateTime b1 = Convert.ToDateTime(Request.QueryString["baslangic"].ToString());
                DateTime b2 = Convert.ToDateTime(Request.QueryString["bitis"].ToString());
                return Json(ProListFalse(b1, b2), JsonRequestBehavior.AllowGet);
            }
        }
        public List<MesajDurum> ProListFalse(DateTime baslangic, DateTime bitis)
        {
            bitis = bitis.AddDays(1).AddSeconds(-1);
            var q = db.Personeller
           .Where(n => n.MesajTarihi >= baslangic)
           .Where(n => n.MesajTarihi <= bitis);

            int istek = q.Where(x => x.Okundu == false && x.MesajTuru == "İstek").Count();
            int tesekkur = q.Where(x => x.Okundu == false && x.MesajTuru == "Teşekkür").Count();
            int sikayet = q.Where(x => x.Okundu == false && x.MesajTuru == "Şikayet").Count();
            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "İstek",
                msjSayi = istek

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Teşekkür",
                msjSayi = tesekkur

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şikayet",
                msjSayi = sikayet

            });
            return md;

        }
        public List<MesajDurum> ProListFalse()
        {
            int istek = db.Personeller.Where(x => x.Okundu == false && x.MesajTuru == "İstek").Count();
            int tesekkur = db.Personeller.Where(x => x.Okundu == false && x.MesajTuru == "Teşekkür").Count();
            int sikayet = db.Personeller.Where(x => x.Okundu == false && x.MesajTuru == "Şikayet").Count();

            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "İstek",
                msjSayi = istek
            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Teşekkür",
                msjSayi = tesekkur

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şikayet",
                msjSayi = sikayet

            });
            return md;

        }

        //OKUNMAYAN MESAJLAR GRAFİĞİ BİTİŞ

        //AYLIK MESAJLAR - DURUM GRAFİĞİ
        public ActionResult VisualizeMessageResultMounth()
        {
            return Json(ProListMounth(), JsonRequestBehavior.AllowGet);
        }
        public List<MesajDurum> ProListMounth()
        {
            //OCAK
            DateTime ocakbas = new DateTime(2021, 01, 01);
            DateTime ocakbit = new DateTime(2021, 01, 31);
            var q = db.Personeller
           .Where(n => n.MesajTarihi >= ocakbas)
           .Where(n => n.MesajTarihi <= ocakbit);

            int istekocak = q.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurocak = q.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetocak = q.Where(x => x.MesajTuru == "Şikayet").Count();
            //OCAK BİTİŞ

            //ŞUBAT
            DateTime subatbas = new DateTime(2021, 02, 01);
            DateTime subatbit = new DateTime(2021, 02, 28);
            var qsubat = db.Personeller
           .Where(n => n.MesajTarihi >= subatbas)
           .Where(n => n.MesajTarihi <= subatbit);

            int isteksubat = qsubat.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkursubat = qsubat.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetsubat = qsubat.Where(x => x.MesajTuru == "Şikayet").Count();
            //ŞUBAT BİTİŞ
            //MART
            DateTime martbas = new DateTime(2021, 03, 01);
            DateTime martbit = new DateTime(2021, 03, 31);
            var qmart = db.Personeller
           .Where(n => n.MesajTarihi >= martbas)
           .Where(n => n.MesajTarihi <= martbit);

            int istekmart = qmart.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurmart = qmart.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetmart = qmart.Where(x => x.MesajTuru == "Şikayet").Count();
            //MART BİTİŞ
            //NİSAN
            DateTime nisanbas = new DateTime(2021, 04, 01);
            DateTime nisanbit = new DateTime(2021, 04, 30);
            var qnisan = db.Personeller
           .Where(n => n.MesajTarihi >= nisanbas)
           .Where(n => n.MesajTarihi <= nisanbit);

            int isteknisan = qnisan.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurnisan = qnisan.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetnisan = qnisan.Where(x => x.MesajTuru == "Şikayet").Count();
            //NİSAN BİTİŞ
            //MAYIS
            DateTime mayisbas = new DateTime(2021, 05, 01);
            DateTime mayisbit = new DateTime(2021, 05, 31);
            var qmayis = db.Personeller
           .Where(n => n.MesajTarihi >= mayisbas)
           .Where(n => n.MesajTarihi <= mayisbit);

            int istekmayis = qmayis.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurmayis = qmayis.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetmayis = qmayis.Where(x => x.MesajTuru == "Şikayet").Count();
            //MAYIS BİTİŞ
            //HAZİRAN
            DateTime haziranbas = new DateTime(2021, 06, 01);
            DateTime haziranbit = new DateTime(2021, 06, 30);
            var qhaziran = db.Personeller
           .Where(n => n.MesajTarihi >= haziranbas)
           .Where(n => n.MesajTarihi <= haziranbit);

            int istekhaziran = qhaziran.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurhaziran = qhaziran.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayethaziran = qhaziran.Where(x => x.MesajTuru == "Şikayet").Count();
            //HAZİRAN BİTİŞ
            //TEMMUZ
            DateTime temmuzbas = new DateTime(2021, 07, 01);
            DateTime temmuzbit = new DateTime(2021, 07, 31);
            var qtemmuz = db.Personeller
           .Where(n => n.MesajTarihi >= temmuzbas)
           .Where(n => n.MesajTarihi <= temmuzbit);

            int istektemmuz = qtemmuz.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurtemmuz = qtemmuz.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayettemmuz = qtemmuz.Where(x => x.MesajTuru == "Şikayet").Count();
            //TEMMUZ BİTİŞ
            //AĞUSTOS
            DateTime agustosbas = new DateTime(2021, 08, 01);
            DateTime agustosbit = new DateTime(2021, 08, 31);
            var qagustos = db.Personeller
           .Where(n => n.MesajTarihi >= agustosbas)
           .Where(n => n.MesajTarihi <= agustosbit);

            int istekagustos = qagustos.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkuragustos = qagustos.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetagustos = qagustos.Where(x => x.MesajTuru == "Şikayet").Count();
            //AĞUSTOS BİTİŞ
            //EYLÜL
            DateTime eylulbas = new DateTime(2021, 09, 01);
            DateTime eylulbit = new DateTime(2021, 09, 30);
            var qeylul = db.Personeller
           .Where(n => n.MesajTarihi >= eylulbas)
           .Where(n => n.MesajTarihi <= eylulbit);

            int istekeylul = qeylul.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkureylul = qeylul.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayeteylul = qeylul.Where(x => x.MesajTuru == "Şikayet").Count();
            //EYLÜL BİTİŞ
            //EKİM
            DateTime ekimbas = new DateTime(2021, 10, 01);
            DateTime ekimbit = new DateTime(2021, 10, 31);
            var qekim = db.Personeller
           .Where(n => n.MesajTarihi >= ekimbas)
           .Where(n => n.MesajTarihi <= ekimbit);

            int istekekim = qekim.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurekim = qekim.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetekim = qekim.Where(x => x.MesajTuru == "Şikayet").Count();
            //EKİM BİTİŞ
            //KASIM
            DateTime kasimbas = new DateTime(2021, 11, 01);
            DateTime kasimbit = new DateTime(2021, 11, 30);
            var qkasim = db.Personeller
           .Where(n => n.MesajTarihi >= kasimbas)
           .Where(n => n.MesajTarihi <= kasimbit);

            int istekkasim = qkasim.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkurkasim = qkasim.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetkasim = qkasim.Where(x => x.MesajTuru == "Şikayet").Count();
            //KASIM BİTİŞ
            //ARALIK
            DateTime aralikbas = new DateTime(2021, 12, 01);
            DateTime aralikbit = new DateTime(2021, 12, 31);
            var qaralik = db.Personeller
           .Where(n => n.MesajTarihi >= aralikbas)
           .Where(n => n.MesajTarihi <= aralikbit);

            int istekaralik = qaralik.Where(x => x.MesajTuru == "İstek").Count();
            int tesekkuraralik = qaralik.Where(x => x.MesajTuru == "Teşekkür").Count();
            int sikayetaralik = qaralik.Where(x => x.MesajTuru == "Şikayet").Count();
            //ARALIK BİTİŞ

            List<MesajDurum> md = new List<MesajDurum>();
            md.Add(new MesajDurum()
            {
                msjTuru = "Ocak",
                msjSayi = istekocak,

                msjSikayet = "Ocak",
                msjSikayetSayi = sikayetocak,
                msjTesekkurSayi = tesekkurocak,
            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Şubat",
                msjSayi = isteksubat,

                msjSikayetSayi = sikayetsubat,
                msjTesekkurSayi = tesekkursubat,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Mart",
                msjSayi = istekmart,
                msjSikayetSayi = sikayetmart,
                msjTesekkurSayi = tesekkurmart,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Nisan",
                msjSayi = isteknisan,
                msjSikayetSayi = sikayetnisan,
                msjTesekkurSayi = tesekkurnisan,


            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Mayıs",
                msjSayi = istekmayis,
                msjSikayetSayi = sikayetmayis,
                msjTesekkurSayi = tesekkurmayis,


            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Haziran",
                msjSayi = istekhaziran,
                msjSikayetSayi = sikayethaziran,
                msjTesekkurSayi = tesekkurhaziran,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Temmuz",
                msjSayi = istektemmuz,
                msjSikayetSayi = sikayettemmuz,
                msjTesekkurSayi = tesekkurtemmuz,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Ağustos",
                msjSayi = istekagustos,
                msjSikayetSayi = sikayetagustos,
                msjTesekkurSayi = tesekkuragustos,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Eylül",
                msjSayi = istekeylul,
                msjSikayetSayi = sikayeteylul,
                msjTesekkurSayi = tesekkureylul,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Ekim",
                msjSayi = istekekim,
                msjSikayetSayi = sikayetekim,
                msjTesekkurSayi = tesekkurekim,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Kasım",
                msjSayi = istekkasim,
                msjSikayetSayi = sikayetkasim,
                msjTesekkurSayi = tesekkurkasim,

            });
            md.Add(new MesajDurum()
            {
                msjTuru = "Aralık",
                msjSayi = istekaralik,
                msjSikayetSayi = sikayetaralik,
                msjTesekkurSayi = tesekkuraralik,

            });

            return md;
        }

        //AYLIK MESAJLAR DURUM - GRAFİĞİ BİTİŞ
        public ActionResult SendMailBox(string ara, int p = 1)
        {
            //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
            var searchList = db.MesajYanitlari.Where(x => x.Mesaj.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToPagedList(p, 10);

            return View(searchList);
        }
        public ActionResult SendMailBoxUnits(string ara, int p = 1)
        {
            //var messageList = db.Personeller.ToList().ToPagedList(p, 4);
            var searchList = db.MesajlarHavale.Where(x => x.Mesaj.Contains(ara) || ara == null).OrderByDescending(x => x.Id).ToPagedList(p, 10);

            return View(searchList);
        }
        public ActionResult DeleteSendedMessage(int? id)
        {
            var silinecekMesaj = db.MesajYanitlari.Where(x => x.Id == id).FirstOrDefault();

            db.MesajYanitlari.Remove(silinecekMesaj);
            db.SaveChanges();
            return RedirectToAction("SendMailBox");
        }
        public ActionResult SendedMailDetail(int id)
        {
            var mesaj = db.MesajYanitlari.SingleOrDefault(x => x.Id.Equals(id));
            if (mesaj == null)
            {
                throw new Exception("Mesaj bulunamadı!.");
            }
            return View(mesaj);
        }
    }
}