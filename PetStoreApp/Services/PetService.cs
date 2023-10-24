using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace PetStoreApp
{
    public class PetService : IPetService
    {
        [ApiKeyAuthorization]
        public async Task<List<Pet>> GetAvailablePetsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://petstore.swagger.io/v2/");

                HttpResponseMessage response = await client.GetAsync("pet/findByStatus?status=available");

                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<List<Pet>>(data);
                }
                else
                {
                    Console.WriteLine("Error fetching data from the Pet Store API.");
                    return new List<Pet>();
                }
            }
        }

    }
}
