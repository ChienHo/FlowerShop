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

        public FlowerController(IGenericRepository<Flower> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        // GET: Flower
        public ActionResult Index()
        {
            return View(_genericRepository.GetAll());
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