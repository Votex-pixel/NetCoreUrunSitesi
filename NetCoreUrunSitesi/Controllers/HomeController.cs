using BL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using NetCoreUrunSitesi.Models;
using System.Diagnostics;

namespace NetCoreUrunSitesi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Slider> _sliderRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<News> _newsRepository;
        private readonly IRepository<Contact> _contactRepository;

        public HomeController(IRepository<Slider> sliderRepository, IRepository<Product> productRepository, IRepository<News> newsRepository, IRepository<Contact> contactRepository)
        {
            _sliderRepository = sliderRepository;
            _productRepository = productRepository;
            _newsRepository = newsRepository;
            _contactRepository = contactRepository;
        }

        public async Task<IActionResult> IndexAsync()
        {
            //var sliders = await _sliderRepository.GetAllAsync();

            var model = new HomePageViewModel()
            {
                Sliders = _sliderRepository.GetAll(),
                Products = _productRepository.GetAll(),
                News = _newsRepository.GetAll()
            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ContactAsync(Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _contactRepository.AddAsync(contact);
                    await _contactRepository.SaveChangesAsync();
                    TempData["Mesaj"] = "<div class='alert alert-info mt-3'>Mesajınız Gönderildi..</div>";
                    return RedirectToAction("Contact");
                }
                catch
                {
                    ModelState.AddModelError("", "Hata Oluştu!");
                }

            }
            return View(contact);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}