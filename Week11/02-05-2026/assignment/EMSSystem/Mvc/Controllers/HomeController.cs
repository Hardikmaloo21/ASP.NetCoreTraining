using Microsoft.AspNetCore.Mvc;

namespace Mvc.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _client;

    public HomeController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient();
        _client.BaseAddress = new Uri("http://api:5000/");
    }

    public async Task<IActionResult> Index()
    {
        var data = await _client.GetStringAsync("api/employees");
        ViewBag.Data = data;
        return View();
    }
}