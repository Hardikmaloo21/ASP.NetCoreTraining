using Microsoft.AspNetCore.Mvc;
using Mvc.Models;
using System.Text;
using System.Text.Json;

namespace Mvc.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly HttpClient _http;
        private readonly string _apiUrl;

        public EmployeeController(IHttpClientFactory factory, IConfiguration config)
        {
            _http   = factory.CreateClient();
            _apiUrl = config["ApiSettings:BaseUrl"] ?? "http://localhost:5001";
        }

        private static readonly JsonSerializerOptions _jsonOpts =
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        // INDEX — getAll
        public async Task<IActionResult> Index()
        {
            var res  = await _http.GetAsync($"{_apiUrl}/api/employee");
            var json = await res.Content.ReadAsStringAsync();
            var list = JsonSerializer.Deserialize<List<EmployeeViewModel>>(json, _jsonOpts)
                       ?? new List<EmployeeViewModel>();
            return View(list);
        }

        // DETAILS — getById
        public async Task<IActionResult> Details(int id)
        {
            var res = await _http.GetAsync($"{_apiUrl}/api/employee/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var emp  = JsonSerializer.Deserialize<EmployeeViewModel>(json, _jsonOpts);
            return View(emp);
        }

        // CREATE — GET
        public IActionResult Create() => View();

        // CREATE — POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var body = new StringContent(
                JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var res = await _http.PostAsync($"{_apiUrl}/api/employee", body);
            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error creating employee.");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // EDIT — GET (update)
        public async Task<IActionResult> Edit(int id)
        {
            var res = await _http.GetAsync($"{_apiUrl}/api/employee/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var emp  = JsonSerializer.Deserialize<EmployeeViewModel>(json, _jsonOpts);
            return View(emp);
        }

        // EDIT — POST (update)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var body = new StringContent(
                JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var res = await _http.PutAsync($"{_apiUrl}/api/employee/{id}", body);
            if (!res.IsSuccessStatusCode)
            {
                ModelState.AddModelError("", "Error updating employee.");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // DELETE — GET
        public async Task<IActionResult> Delete(int id)
        {
            var res = await _http.GetAsync($"{_apiUrl}/api/employee/{id}");
            if (!res.IsSuccessStatusCode) return NotFound();

            var json = await res.Content.ReadAsStringAsync();
            var emp  = JsonSerializer.Deserialize<EmployeeViewModel>(json, _jsonOpts);
            return View(emp);
        }

        // DELETE — POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _http.DeleteAsync($"{_apiUrl}/api/employee/{id}");
            return RedirectToAction(nameof(Index));
        }
    }
}