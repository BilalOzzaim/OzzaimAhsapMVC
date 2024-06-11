using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OzzaimAhsap.Models;

namespace OzzaimAhsapMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AhsapContext _context;

        public HomeController()
        {
            _context = new AhsapContext();
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        [HttpPost]
        public ActionResult SendContact(IFormCollection form)
        {

            _context.Geridonusler.Add(new Geridonusler()
            {
                ad_soyad = form["isim"],
                email = form["email"],
                telefon = form["telefon"],
                mesaj = form["mesaj"],
            });
            _context.SaveChanges();


            return RedirectToAction(nameof(Index));
        }


        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {
            return View();
        }
        public ActionResult Service()
        {
            return View();
        }
        public ActionResult Project()
        {
            return View();
        }
        public ActionResult Testimonial()
        {
            return View();
        }
        public async Task<ActionResult> Productdetail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var products = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (products == null)
            {
                return NotFound();
            }

            return View(products);
        }
    }
}
