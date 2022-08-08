using BL;
using Entities;
using Microsoft.AspNetCore.Mvc;

namespace NetCoreUrunSitesi.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<IActionResult> IndexAsync(int? id) // ?  nullable yani boş geçilebilir
        {
            if (id != null)
            {
                return View(await _productRepository.GetAllAsync(p => p.CategoryId == id));
            }
            return View(await _productRepository.GetAllAsync());
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return View(await _productRepository.FindAsync(id.Value));
        }

        public async Task<IActionResult> Search(string q) // ?  nullable yani boş geçilebilir
        {
            if (q != null)
            {
                return View(await _productRepository.GetAllAsync(p => p.Name.Contains(q) || p.Description.Contains(q)));
            }
            return View(await _productRepository.GetAllAsync());
        }
    }
}
