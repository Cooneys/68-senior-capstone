using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
namespace HttpClientSample
{
    public class Tickersymbol
    {
        //public string Id { get; set; }
        public string Name { get; set; }
       // public decimal Price { get; set; }
        //public string Category { get; set; }
    }

    class Program
    {
        static HttpClient client = new HttpClient();

        static void ShowProduct(Tickersymbol product)
        {
            Console.WriteLine($"Name: {product.Name}");
        }

        static async Task<Uri> CreateProductAsync(Tickersymbol product)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/products", product);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static async Task<Tickersymbol> GetProductAsync(string path)
        {
            Tickersymbol product = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                product = await response.Content.ReadAsAsync<Tickersymbol>();
            }
            return product;
        }

        /*static async Task<Tickersymbol> UpdateProductAsync(Tickersymbol product)
        {
            HttpResponseMessage response = await client.PutAsJsonAsync(
                $"api/products/{product.Id}", product);
            response.EnsureSuccessStatusCode();

            // Deserialize the updated product from the response body.
            product = await response.Content.ReadAsAsync<Tickersymbol>();
            return product;
        }*/

        static async Task<HttpStatusCode> DeleteProductAsync(string id)
        {
            HttpResponseMessage response = await client.DeleteAsync(
                $"api/products/{id}");
            return response.StatusCode;
        }

        /*static void Main()
        {
            RunAsync().GetAwaiter().GetResult();
        }*/

        static async Task RunAsync()
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:64195/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            try
            {
                // Create a new product
                Tickersymbol product = new Tickersymbol
                {
                    Name = "Gizmo",
                    //Price = 100,
                    //Category = "Widgets"
                };

                //var url = await CreateProductAsync(product);
                //Console.WriteLine($"Created at {url}");

                // Get the product
                product = await GetProductAsync("http://web.engr.oregonstate.edu/~jonesty/api.php/Investments");
                ShowProduct(product);

                // Update the product
                //Console.WriteLine("Updating price...");
                //product.Price = 80;
                //await UpdateProductAsync(product);

                // Get the updated product
                //product = await GetProductAsync(url.PathAndQuery);
                //ShowProduct(product);

                // Delete the product
                //var statusCode = await DeleteProductAsync(product.Id);
               // Console.WriteLine($"Deleted (HTTP Status = {(int)statusCode})");

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.ReadLine();
        }
    }
}
