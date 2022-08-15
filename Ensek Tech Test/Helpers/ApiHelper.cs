using Ensek_Tech_Test.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Ensek_Tech_Test
{
    public static class ApiHelper
    {
        private static HttpClient httpClient = new HttpClient();
        private const string BaseApiUrl = "https://ensekapicandidatetest.azurewebsites.net/";

        public static async Task<GetEnergyResponse?> GetEnergies()
        {
            var response = await httpClient.GetAsync(BaseApiUrl + "energy");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<GetEnergyResponse>(responseBody);
        }

        public static async Task<List<Order>> GetOrders()
        {
            var response = await httpClient.GetAsync(BaseApiUrl + "orders");

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Order>>(responseBody);
        }

        public static async Task<BuyResponse> BuyEnergy(int energyId, int quantity)
        {
            var response = await httpClient.PutAsync(BaseApiUrl + $"buy/{energyId}/{quantity}", null);

            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<BuyResponse>(responseBody);
        }
    }
}
