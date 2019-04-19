using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FA.FlowerShop.Core;

namespace FA.FlowerShop.Presentation.Areas.Admin.Controllers
{
    public class FlowersController : Controller
    {
        private readonly IGenericRepository<Flower> _flowergenericRepository;
        private readonly IGenericRepository<Category> _CategenericRepository;

        public FlowersController(IGenericRepository<Flower> genericRepository, IGenericRepository<Category> repository)
        {
            _flowergenericRepository = genericRepository;
            _CategenericRepository = repository;
        }

        // GET: Admin/Flowers
        public ActionResult Index()
        {
            var flowers = _flowergenericRepository.GetAll();
            return View(flowers);
        }

        // GET: Admin/Flowers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flower flower = _flowergenericRepository.GetById(id.Value);
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }

        // GET: Admin/Flowers/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_CategenericRepository.GetAll(), "CategoryId", "CategoryName");
            return View();
        }

        // POST: Admin/Flowers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FlowerId,FlowerName,Description,ImageUrl,Price,Color,StoreDate,StoreInventory,CategoryId")] Flower flower, HttpPostedFileBase ImgUrl)
        {
            string fileName = "";
            if (ImgUrl != null && ImgUrl.ContentLength > 0)
            {
                try
                {
                    fileName = Path.GetFileName(ImgUrl.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/Img"), Path.GetFileName(ImgUrl.FileName));
                    ImgUrl.SaveAs(path);
                }
                catch (Exception)
                {
                    throw;
                }
            }
            flower.ImageUrl = fileName;
            if (ModelState.IsValid)
            {
                _flowergenericRepository.Add(flower);
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(_CategenericRepository.GetAll(), "CategoryId", "CategoryName", flower.CategoryId);
            return View(flower);
        }

        // GET: Admin/Flowers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = _flowergenericRepository.GetById(id.Value);
            if (flower == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(_CategenericRepository.GetAll(), "CategoryId", "CategoryName", flower.CategoryId);
            return View(flower);
        }

        // POST: Admin/Flowers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "FlowerId,FlowerName,Description,ImageUrl,Price,Color,StoreDate,StoreInventory,CategoryId")] Flower flower, HttpPostedFileBase ImgUrl)
        {
            if (ImgUrl != null && ImgUrl.ContentLength > 0)
            {
                string filePath = Path.Combine(Server.MapPath("~/Content/Img"), Path.GetFileName(ImgUrl.FileName));
                ImgUrl.SaveAs(filePath);
                flower.ImageUrl = ImgUrl.FileName;
            }
            if (ModelState.IsValid)
            {
                _flowergenericRepository.Update(flower);
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(_CategenericRepository.GetAll(), "CategoryId", "CategoryName", flower.CategoryId);
            return View(flower);
        }

        // GET: Admin/Flowers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Flower flower = _flowergenericRepository.GetById(id.Value);
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }

        // POST: Admin/Flowers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Flower flower = _flowergenericRepository.GetById(id);
            _flowergenericRepository.Delete(flower);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _flowergenericRepository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
