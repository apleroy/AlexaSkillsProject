using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AlexaSkillProject.Repository;
using AlexaSkillProject.Domain;

namespace AlexaSkillProject.Controllers
{
    public class AlexaRequestsController : Controller
    {
        private AlexaSkillProjectDataContext db = new AlexaSkillProjectDataContext();

        // GET: AlexaRequests
        public ActionResult Index()
        {
            var alexaRequests = db.AlexaRequests.Include(a => a.AlexaMember);
            return View(alexaRequests.ToList());
        }

        // GET: AlexaRequests/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlexaRequest alexaRequest = db.AlexaRequests.Find(id);
            if (alexaRequest == null)
            {
                return HttpNotFound();
            }
            return View(alexaRequest);
        }

        // GET: AlexaRequests/Create
        public ActionResult Create()
        {
            ViewBag.AlexaMemberId = new SelectList(db.AlexaMembers, "Id", "AlexaUserId");
            return View();
        }

        // POST: AlexaRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,AlexaMemberId,SessionId,AppId,RequestId,UserId,Timestamp,Intent,Slots,IsNew,Version,Type,DateCreated")] AlexaRequest alexaRequest)
        {
            if (ModelState.IsValid)
            {
                db.AlexaRequests.Add(alexaRequest);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AlexaMemberId = new SelectList(db.AlexaMembers, "Id", "AlexaUserId", alexaRequest.AlexaMemberId);
            return View(alexaRequest);
        }

        // GET: AlexaRequests/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlexaRequest alexaRequest = db.AlexaRequests.Find(id);
            if (alexaRequest == null)
            {
                return HttpNotFound();
            }
            ViewBag.AlexaMemberId = new SelectList(db.AlexaMembers, "Id", "AlexaUserId", alexaRequest.AlexaMemberId);
            return View(alexaRequest);
        }

        // POST: AlexaRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,AlexaMemberId,SessionId,AppId,RequestId,UserId,Timestamp,Intent,Slots,IsNew,Version,Type,DateCreated")] AlexaRequest alexaRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(alexaRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AlexaMemberId = new SelectList(db.AlexaMembers, "Id", "AlexaUserId", alexaRequest.AlexaMemberId);
            return View(alexaRequest);
        }

        // GET: AlexaRequests/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AlexaRequest alexaRequest = db.AlexaRequests.Find(id);
            if (alexaRequest == null)
            {
                return HttpNotFound();
            }
            return View(alexaRequest);
        }

        // POST: AlexaRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            AlexaRequest alexaRequest = db.AlexaRequests.Find(id);
            db.AlexaRequests.Remove(alexaRequest);
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
