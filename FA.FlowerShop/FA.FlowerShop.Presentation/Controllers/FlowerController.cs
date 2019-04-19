using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FA.FlowerShop.Core;

namespace FA.FlowerShop.Presentation.Controllers
{
    public class FlowerController : Controller
    {
        private readonly IGenericRepository<Flower> _genericRepository;
        private readonly IGenericRepository<Category> _caterepository;

        public FlowerController(IGenericRepository<Flower> genericRepository, IGenericRepository<Category> caterepository)
        {
            _genericRepository = genericRepository;
            _caterepository = caterepository;
        }

        // GET: Flower
        public ActionResult Index(int? category, string flowerName, int? price1, int? price2)
        {
            var listflower = _genericRepository.GetAll();

            if (category != null && !String.IsNullOrEmpty(flowerName))
            {
                listflower = _genericRepository.GetAll().Where(p => p.CategoryId == category.Value && p.FlowerName.Contains(flowerName) && p.Price > price1 && p.Price < price2);
            }
            if (category == null && !String.IsNullOrEmpty(flowerName))
            {
                listflower = _genericRepository.GetAll().Where(p => p.FlowerName.Contains(flowerName) && p.Price > price1 && p.Price < price2);
            }
            ViewBag.Category = _caterepository.GetAll();
            return View(listflower);
        }
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Flower flower = _genericRepository.GetById(id.Value);
            if (flower == null)
            {
                return HttpNotFound();
            }
            return View(flower);
        }
    }
}