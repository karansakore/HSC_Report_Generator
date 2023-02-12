using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HSC_Report3;

namespace HSC_Report3.Controllers
{
    public class HSC_RecordController : Controller
    {
        private studInfoEntities db = new studInfoEntities();

        // GET: HSC_Record
        public ActionResult Index()
        {
            return View(db.HSC_Record.ToList());
        }

        // GET: HSC_Record/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HSC_Record hSC_Record = db.HSC_Record.Find(id);
            if (hSC_Record == null)
            {
                return HttpNotFound();
            }
            
            return View(hSC_Record);
        }
        

        // GET: HSC_Record/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: HSC_Record/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "seatNo,FirstName,LasttName,Math,English,Physics,Chemistry,Computer")] HSC_Record hSC_Record)
        {
            if (ModelState.IsValid)
            {
                db.HSC_Record.Add(hSC_Record);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hSC_Record);
        }

        // GET: HSC_Record/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HSC_Record hSC_Record = db.HSC_Record.Find(id);
            if (hSC_Record == null)
            {
                return HttpNotFound();
            }
            return View(hSC_Record);
        }

        // POST: HSC_Record/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "seatNo,FirstName,LasttName,Math,English,Physics,Chemistry,Computer")] HSC_Record hSC_Record)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hSC_Record).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(hSC_Record);
        }

        // GET: HSC_Record/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HSC_Record hSC_Record = db.HSC_Record.Find(id);
            if (hSC_Record == null)
            {
                return HttpNotFound();
            }
            return View(hSC_Record);
        }

        // POST: HSC_Record/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            HSC_Record hSC_Record = db.HSC_Record.Find(id);
            db.HSC_Record.Remove(hSC_Record);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        /*public ActionResult ReceiveWithRequestForm()
        {
            return View();
        }*/

        [HttpPost]
        public ActionResult ReceiveWithRequestFormData()
        {
            string id = Request.Form["txtSeatNo"];
            //HSC_Record id1 = db.HSC_Record.Find(id);
            HSC_Record avg = db.HSC_Record.Find(id);
            List<float> sum = new List<float>();
            sum.Add((float)avg.English);
            sum.Add((float)avg.Math);
            sum.Add((float)avg.Computer);
            sum.Add((float)avg.Chemistry);
            sum.Add((float)avg.Physics);
            float add = 0;
            for (int i = 0; i < 5; i++)
            {
                add = add + (float)sum[i];
            }

            ViewBag.addition = add;
            float Average = add / 5;
            ViewBag.Average = Average;
            int flag = 0;
            if(Average >= 35)
            {
                flag=1;
            }
            if (flag == 1)
            {
                ViewBag.Pass = "Pass";
            }
            else
            {
                ViewBag.Pass = "Fail";
            }
            return View(avg);
        }
    }
}
