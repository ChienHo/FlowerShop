using FA.FlowerShop.Core;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Transactions;
using System.Web.Mvc;

namespace FA.FlowerShop.Presentation.Areas.Admin.Controllers
{
    [Authorize(Roles = "Administrators")]
    public class ManageController : Controller
    {
        private readonly FlowerShopContext _context;

        public ManageController()
        {
            _context = new FlowerShopContext();
        }

        public ActionResult SaleOff()
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName");
            return View();
        }

        [HttpPost]
        public ActionResult SaleOff(int? categoryId, int? flowerId, int saleOff)
        {
            if (categoryId == null)
            {
                ModelState.AddModelError("abc", "Please select category id");
                return View();
            }
            else
            {
                var flowers = flowerId == null ? _context.Flowers.Where(c => c.CategoryId == categoryId).ToList() : _context.Flowers.Where(f => f.FlowerId == flowerId).ToList();

                using (var scope = new TransactionScope())
                {
                    foreach (var flower in flowers)
                    {
                        flower.SalePrice = flower.Price - (flower.Price * saleOff / 100);
                        _context.Entry(flower).State = EntityState.Modified;
                    }

                    _context.SaveChanges();
                    scope.Complete();
                };
            }
            return RedirectToAction("Index", "Flowers");
        }

        public JsonResult GetFlowersByCategory(int categoryId)
        {
            var flowers = _context.Flowers.Where(f => f.CategoryId == categoryId).Select(t=> new{flowerId = t.FlowerId,flowerName=t.FlowerName}).ToList();
            return Json(flowers, JsonRequestBehavior.AllowGet);
        }
    }
}
