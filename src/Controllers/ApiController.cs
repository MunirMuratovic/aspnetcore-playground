using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using src.Model;

namespace src.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ApiController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        // GET: api/
        [Route("api")] // nested route [controller]/[route]
        public ActionResult<string> Get()
        {
            return Ok("string");
        }

        // GET: api/list
        [Route("list")] // returns list
        public IActionResult GetTest() // IActionResult can return anything without adding <> brackets element
        {
            List<int> list = new List<int>() { 1, 2, 3 };
            return Ok(list);
        }

        // GET: test
        [Route("deserial")] // JSON deserialize test /api/deserial
        // returns only one JSON object out from JSON list/array of objects
        public async Task<IActionResult> GetDeserial()
        {
            HttpClient httpClient = _httpClientFactory.CreateClient();
            HttpRequestMessage request = new HttpRequestMessage(new HttpMethod("GET"), "https://jsonplaceholder.typicode.com/posts");
            HttpResponseMessage response = await httpClient.SendAsync(request);

            var jsonString = response.Content.ReadAsStringAsync().Result;
            var posts = JsonSerializer.Deserialize<List<Post>>(jsonString);

            return Ok(posts.FirstOrDefault());
        }
    }
}
