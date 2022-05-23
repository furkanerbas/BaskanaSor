using AltinorduWebProject.Models.Managers;
using BusinessLayer;
using Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AltinorduWebProject.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        private DatabaseContext db = new DatabaseContext();
        // GET: User
        public ActionResult DatabaseCreat()
        {
            Test test = new Test();
            return View();
        }
        public ActionResult homepage()
        {
            List<Personel> personeller = db.Personeller.ToList(); // Select * from Personel
            return View(personeller);
        }
        public ActionResult Detail(string id)
        {
            var mesaj = db.Personeller.SingleOrDefault(x => x.Ad.Equals(id));
            if (mesaj == null)
            {
                throw new Exception("Mesaj bulunamadı!.");
            }
            return View(mesaj);
        }
        public ActionResult SendMail()
        {
            return RedirectToAction("Detail");
        }
        [HttpPost]
        public ActionResult SendMail(string receiverEmail, string subject, string message)
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
                        Body = body
                    })
                    {
                        smtp.Send(mess);
                    }
                    return RedirectToAction("homepage");

                }
            }
            catch (Exception)
            {

                ViewBag.Error = "Mail Gönderilirken bir problem yaşandı";
            }
            return RedirectToAction("Detail");
        }


        public ActionResult Index()
        {
            DateTime date = Convert.ToDateTime("2021-07-05");
            var result = from o in db.Personeller
                             // Where condtiton to filter record based on Current Date.
                         where o.MesajTarihi == date.Date
                         select new
                         {
                             UserName = o.MesajTuru,
                             Type = db.Personeller.Count()
                         };

            GetJsonString(result);
            return View();
        }

        [HttpPost]
        public ActionResult Index(DateTime fromDate, DateTime toDate)
        {
            var result = from o in db.Personeller
                             // Where condtiton to filter record between Start Date and End Date.
                         where o.MesajTarihi >= fromDate.Date && o.MesajTarihi <= toDate.Date
                         select new
                         {
                             UserName = o.MesajTuru,
                             Type = db.Personeller.Count()
                         };

            GetJsonString(result);
            return View();
        }

        private void GetJsonString(dynamic result)
        {
            List<UserType> inspections = new List<UserType>();
            foreach (var item in result)
            {
                for (int i = 0; i < item.Type.ToString().Split(',').Length; i++)
                {
                    inspections.Add(new UserType { UserName = item.UserName, Type = Convert.ToDateTime(item.Type.ToString().Split(',')[i]).ToString("yyyy") });
                }
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("UserName");
            dt.Columns.Add("Type");
            dt.Columns.Add("Count");
            var results = inspections.GroupBy(x => new { x.UserName, x.Type }).Select(x => new { type = x.Key, count = x.Count() });
            foreach (var item in results)
            {
                dt.Rows.Add(item.type.UserName, item.type.Type, item.count);
            }

            string[] users = (dt.AsEnumerable().Select(p => p["UserName"].ToString())).Distinct().ToArray();
            string[] types = (dt.AsEnumerable().Where(x => x["Type"].ToString() != "")
                                .Select(p => p["Type"].ToString())).Distinct().OrderBy(x => x).ToArray();

            List<decimal[]> datas = new List<decimal[]>();
            for (int i = 0; i < types.Length; i++)
            {
                List<decimal> counts = new List<decimal>();
                for (int j = 0; j < users.Length; j++)
                {
                    counts.Add(dt.AsEnumerable()
                                .Where(p => p["UserName"].ToString() == users[j] && p["Type"].ToString() == types[i])
                                .Select(p => Convert.ToDecimal(p["Count"])).FirstOrDefault());
                }
                datas.Add(counts.ToArray());
            }

            ChartData chartData = new ChartData();
            chartData.Labels = users;
            chartData.DatasetLabels = types;
            chartData.DatasetDatas = datas;

            JavaScriptSerializer se = new JavaScriptSerializer();
            TempData["Data"] = se.Serialize(chartData);
        }
    }

    public class ChartData
    {
        public string[] Labels { get; set; }
        public string[] DatasetLabels { get; set; }
        public List<decimal[]> DatasetDatas { get; set; }
    }

    public class UserType
    {
        public string UserName { get; set; }
        public int Type { get; set; }
    }
}
