using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;
using Client_Service.Models;

namespace Client_Service.Controllers
{
    public class CustomersController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public CustomersController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:44304/apiGateway/Customers";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<Customers> list = JsonSerializer.Deserialize<List<Customers>>(stringData, options);

            ViewBag.Data = list;
            return View();
        }

       /* // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Members/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Member member = JsonSerializer.Deserialize<Member>(productData, options);
                    return View(member);

                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();


            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Member member)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(member), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync(ProductApiUrl, content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful creation (e.g., redirect to the product list)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle errors
                    ModelState.AddModelError(string.Empty, "Error creating the member.");
                }
            }
            return View(member);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Members/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Member member = JsonSerializer.Deserialize<Member>(productData, options);
                    return View(member);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MemberId,Email,CompanyName,City,Country,Password")] Member member)
        {
            if (id != member.MemberId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(member), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"{ProductApiUrl}/{id}", content);

                if (response.IsSuccessStatusCode)
                {
                    // Handle successful update (e.g., redirect to the product list)
                    return RedirectToAction("Index");
                }
                else
                {
                    // Handle errors
                    ModelState.AddModelError(string.Empty, "Error updating the product.");
                }
            }
            return View(member);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:5001/api/Members/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Member member = JsonSerializer.Deserialize<Member>(productData, options);
                    return View(member);
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions or errors, e.g., log the error
            }

            return NotFound();
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await client.DeleteAsync($"{ProductApiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                // Handle successful deletion (e.g., redirect to the product list)
                return RedirectToAction("Index");
            }
            else
            {
                // Handle errors
                return NotFound();
            }
        }*/
    }
}
