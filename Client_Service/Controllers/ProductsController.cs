using Client_Service.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using System.Text;
using System.Threading.Tasks;
using System;

namespace Client_Service.Controllers
{
    public class ProductsController : Controller
    {
        private readonly HttpClient client = null;
        private string ProductApiUrl = "";

        public ProductsController()
        {
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            ProductApiUrl = "https://localhost:7001/apiGateway/Products";
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await client.GetAsync(ProductApiUrl);
            string stringData = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            List<Products> list = JsonSerializer.Deserialize<List<Products>>(stringData, options);


            return View(list);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            try
            {
                // Make a request to your API to get the product by id
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:7001/apiGateway/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string Data = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Products Pro = JsonSerializer.Deserialize<Products>(Data, options);

                    return View(Pro);

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
        public async Task<IActionResult> Create(Products Product)
        {
            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(Product), Encoding.UTF8, "application/json");
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
            return View(Product);
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
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:7001/apiGateway/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Products Product = JsonSerializer.Deserialize<Products>(productData, options);
                    return View(Product);
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
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Products Product)
        {
            if (id != Product.ProId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var content = new StringContent(JsonSerializer.Serialize(Product), Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7001/apiGateway/Products/{id}", content);

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
            return View(Product);
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
                HttpResponseMessage productResponse = await client.GetAsync($"https://localhost:7001/apiGateway/Products/{id}");

                if (productResponse.IsSuccessStatusCode)
                {
                    string productData = await productResponse.Content.ReadAsStringAsync();
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    Products Product = JsonSerializer.Deserialize<Products>(productData, options);
                    return View(Product);
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
        }
    }
}
