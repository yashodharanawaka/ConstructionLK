﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ConstructionLK.Models;

namespace ConstructionLK.Controllers
{
    public class RequestProgressesController : Controller
    {
        private ConstructionLKContext db = new ConstructionLKContext();

        // GET: RequestProgresses
        public ActionResult Index()
        {
            var requestProgreses = db.RequestProgreses.Include(r => r.ItemRequest);
            return View(requestProgreses.ToList());
        }

        // GET: RequestProgresses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestProgress requestProgress = db.RequestProgreses.Find(id);
            if (requestProgress == null)
            {
                return HttpNotFound();
            }
            return View(requestProgress);
        }

        // GET: RequestProgresses/Create
        public ActionResult Create(int? id)
        {
            ViewBag.ReqId = id;
            return View();
        }

        // POST: RequestProgresses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ReqId,ModifiedDate,value,Comment")] RequestProgress requestProgress)
        {
            if (ModelState.IsValid)
            {
                db.RequestProgreses.Add(requestProgress);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReqId = new SelectList(db.ItemRequests, "Id", "Message", requestProgress.ReqId);
            return View(requestProgress);
        }

        // GET: RequestProgresses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestProgress requestProgress = db.RequestProgreses.Find(id);
            if (requestProgress == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReqId = new SelectList(db.ItemRequests, "Id", "Message", requestProgress.ReqId);
            return View(requestProgress);
        }

        // POST: RequestProgresses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ReqId,ModifiedDate,value,Comment")] RequestProgress requestProgress)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestProgress).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReqId = new SelectList(db.ItemRequests, "Id", "Message", requestProgress.ReqId);
            return View(requestProgress);
        }

        // GET: RequestProgresses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestProgress requestProgress = db.RequestProgreses.Find(id);
            if (requestProgress == null)
            {
                return HttpNotFound();
            }
            return View(requestProgress);
        }

        // POST: RequestProgresses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RequestProgress requestProgress = db.RequestProgreses.Find(id);
            db.RequestProgreses.Remove(requestProgress);
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
