using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;

namespace Last10kAPIs
{
    static class Program
    {
        static void Main()
        {
            MakeRequest();
            Console.WriteLine("Hit ENTER to exit...");
            Console.ReadLine();
            Console.WriteLine("Json data........");
            Console.ReadLine();
        }
        
        static async void MakeRequest()
        {
            var client = new HttpClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            // Request headers
            client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ced63a48eaa34a288772c71c62da184a");

            // Request parameters
            queryString["formType"] = "10-K";
            queryString["filingOrder"] = "0";
            var uri1 = "https://services.last10k.com/v1/company/{ticker}/balancesheet?" + queryString;
            var uri2 = "https://services.last10k.com/v1/company/{ticker}/income?" + queryString;
            var uri3 = "https://services.last10k.com/v1/company/{ticker}/cashflows?" + queryString;
            var uri4 = "https://services.last10k.com/v1/company/{ticker}/ratios?" + queryString;
            var uri5 = "https://services.last10k.com/v1/company/{ticker}/operations?" + queryString;


            var response1 = await client.GetAsync(uri1);
            var response2 = await client.GetAsync(uri2);
            var response3 = await client.GetAsync(uri3);
            var response4 = await client.GetAsync(uri4);
            var response5 = await client.GetAsync(uri5);
        }
    }
}   
