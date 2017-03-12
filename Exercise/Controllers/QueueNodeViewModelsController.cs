using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Exercise.Models;

namespace Exercise.Controllers
{
    public class QueueNodeViewModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: QueueNodeViewModels
        public ActionResult Index()
        {
            var listWait = db.QueueNodeViewModels.Where(x => x.Status == StatusType.Wait).ToList();
            var listGetService = db.QueueNodeViewModels.Where(x => x.Status == StatusType.GetService).ToList();

            listWait.Sort();
            ViewBag.listGetService = listGetService;


            return View(listWait);
        }

        public ActionResult Next()
        {
            var Waitlist = db.QueueNodeViewModels.Where(x => x.Status == StatusType.Wait).ToList();
            var ServiceList = db.QueueNodeViewModels.Where(x => x.Status == StatusType.GetService).ToList();

            if ((Waitlist.Count == 0) && (ServiceList.Count == 0)) return RedirectToAction("Index");

            ServiceList.Sort();
            var toDone = ServiceList.FirstOrDefault();
            //done.Status = StatusType.Done;
            var updateDone = ServiceList.FirstOrDefault(x => x.Id == toDone.Id);
            if (updateDone != null)
            {
                updateDone.Status = StatusType.Done;

            }

            Waitlist.Sort();
            var toWait = Waitlist.FirstOrDefault();
            //done.Status = StatusType.Done;
            var updateGetService = Waitlist.FirstOrDefault(x => x.Id == toWait.Id);
            if (updateGetService != null)
            {
                updateGetService.Status = StatusType.GetService;

            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        // GET: QueueNodeViewModels/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QueueNodeViewModel queueNodeViewModel = db.QueueNodeViewModels.Find(id);
            if (queueNodeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(queueNodeViewModel);
        }

        // GET: QueueNodeViewModels/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QueueNodeViewModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string CustomerName)
        {

            if (!string.IsNullOrWhiteSpace(CustomerName))
            {
                var list = db.QueueNodeViewModels.ToList();
                list.Sort();
                QueueNodeViewModel addData  =new QueueNodeViewModel(list.Last().QueueNumber + 1);
                addData.CustomerName = CustomerName;
                db.QueueNodeViewModels.Add(addData);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: QueueNodeViewModels/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QueueNodeViewModel queueNodeViewModel = db.QueueNodeViewModels.Find(id);
            if (queueNodeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(queueNodeViewModel);
        }

        // POST: QueueNodeViewModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CustomerName,QueueNumber,Status,TimeStamp")] QueueNodeViewModel queueNodeViewModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(queueNodeViewModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(queueNodeViewModel);
        }

        // GET: QueueNodeViewModels/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            QueueNodeViewModel queueNodeViewModel = db.QueueNodeViewModels.Find(id);
            if (queueNodeViewModel == null)
            {
                return HttpNotFound();
            }
            return View(queueNodeViewModel);
        }

        // POST: QueueNodeViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            QueueNodeViewModel queueNodeViewModel = db.QueueNodeViewModels.Find(id);
            db.QueueNodeViewModels.Remove(queueNodeViewModel);
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
    }
}
