using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Crewin_Task.Entities;

public class ProductController : Controller
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    public async Task<IActionResult> Index()
    {
        var categories = await _productService.GetCategoriesAsync();
        return View(categories);
    }

    public async Task<IActionResult> Products(string category)
    {
        var products = await _productService.GetProductsByCategoryAsync(category);
        return View(products);
    }

    public async Task<IActionResult> ProductDetails(string category, int id)
    {
        var product = await _productService.GetProductByIdAsync(category, id); // ProductService'de GetProductByIdAsync metodunu ekle
     
        return View(product);
    }
}
