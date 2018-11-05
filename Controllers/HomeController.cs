using CoffeShop2.Models;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml.Linq;

namespace CoffeShop2.Controllers
{
    public class HomeController : Controller
    {
        private CoffeeRepository cr = new CoffeeRepository();
        public ActionResult Index()
        {  
            return View(cr.CoffeeDB.Where(x => x.IsEnable));
        }
        [HttpPost]
        public ActionResult EditCoffee(CoffeItem item)
        {   
            cr.Update(item);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult EditCoffee(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            CoffeItem ci = cr.CoffeeDB.Find(x=>x.Id==id);
            if (ci != null)
            {
                return View(ci);
            }
            return HttpNotFound();
        }
        
        public ActionResult Delete(int id)
        {
            var coffee = cr.GetCoffeeItem(id);
            if (coffee == null)
                return HttpNotFound();

            return View(coffee);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var coffee = cr.GetCoffeeItem(id);
            coffee.IsEnable = false;
            cr.Update(coffee);
            return RedirectToAction("Index");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        

    }
}