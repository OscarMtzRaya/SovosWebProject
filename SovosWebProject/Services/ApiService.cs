using Newtonsoft.Json;
using SovosWebProject.Models;
using System.Net.Http.Headers;
using System.Text;
using static System.Net.WebRequestMethods;

namespace SovosWebProject.Services
{
    public class ApiService : IApiService
    {
        private string _baseUrl;
        public ApiService()
        {

            //var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            //string? value = builder.GetSection("ApiSettings:baseUrl").Value;
            _baseUrl = "https://sovoswebapi.azurewebsites.net";
        }

        public async Task<bool> Create(Category objeto)
        {
            bool output = false;


            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PostAsync("/categories", content);

            if (response.IsSuccessStatusCode)
            {
                output = true;
            }

            return output;
        }

        public async Task<bool> Delete(int id)
        {
            bool output = false;


            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);


            var response = await client.DeleteAsync($"/categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                output = true;
            }

            return output;
        }

        public async Task<Category> GetById(int id)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync($"/categories/{id}");

            var json_response = await response.Content.ReadAsStringAsync();
            var output = JsonConvert.DeserializeObject<Category>(json_response);
            return output;

        }

        public async Task<List<Category>> ListAll()
        {
            List<Category> list = new List<Category>();

            var client = new HttpClient();
            client.BaseAddress = new Uri(_baseUrl);
            var response = await client.GetAsync("/categories");

            if (response.IsSuccessStatusCode)
            {

                var json_response = await response.Content.ReadAsStringAsync();
                var output = JsonConvert.DeserializeObject<List<Category>>(json_response);
                list = output;
            }

            return list;
        }

        public async Task<bool> Update(Category objeto)
        {
            bool output = false;

            var cliente = new HttpClient();
            cliente.BaseAddress = new Uri(_baseUrl);

            var content = new StringContent(JsonConvert.SerializeObject(objeto), Encoding.UTF8, "application/json");

            var response = await cliente.PutAsync("/categories", content);

            if (response.IsSuccessStatusCode)
            {
                output = true;
            }

            return output;
        }
    }
}
