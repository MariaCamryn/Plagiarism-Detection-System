using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Newtonsoft.Json;
using PlagiarismChecker.Web.Models;
using PlagiarismChecker.Web.Services;
using System.Net.Http;
using System.Text;

namespace PlagiarismChecker.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private readonly MongoService _mongo;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration config, MongoService mongo)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
            _mongo = mongo;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Check([FromForm] string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return BadRequest(new { error = "Text is required" });

            var mlUrl = _config["MLService:PredictUrl"];
            var client = _httpClientFactory.CreateClient("ml");

            try
            {
                var payload = new { text = text, top_k = 3 };
                var json = JsonConvert.SerializeObject(payload);
                var resp = await client.PostAsync(mlUrl, new StringContent(json, Encoding.UTF8, "application/json"));

                if (!resp.IsSuccessStatusCode)
                {
                    // parse body but gracefully
                    var body = await resp.Content.ReadAsStringAsync();
                    return StatusCode((int)resp.StatusCode, new { error = "ML error", detail = body });
                }

                var content = await resp.Content.ReadAsStringAsync();
                // validate JSON safely
                dynamic result;
                try
                {
                    result = JsonConvert.DeserializeObject<dynamic>(content);
                }
                catch
                {
                    return StatusCode(502, new { error = "Invalid response from ML service", detail = content });
                }

                double score = (double)result.score;

                var submission = new Submission
                {
                    Content = text,
                    Score = score,
                    TopMatchesJson = JsonConvert.SerializeObject(result.top_matches),
                    HighlightsJson = JsonConvert.SerializeObject(result.highlights),
                    CreatedAt = DateTime.UtcNow
                };

                await _mongo.Submissions.InsertOneAsync(submission);

                return Ok(new
                {
                    id = submission.Id,
                    score = score,
                    top_matches = result.top_matches,
                    highlights = result.highlights
                });
            }
            catch (HttpRequestException)
            {
                // ML service unreachable
                return StatusCode(503, new { error = "ML service unavailable. Please try later." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Server error", detail = ex.Message });
            }
        }

        public IActionResult History()
        {
            var list = _mongo.Submissions.Find(_ => true)
                .SortByDescending(s => s.CreatedAt)
                .Limit(50)
                .ToList();
            return View(list);
        }

        public IActionResult Details(string id)
        {
            var s = _mongo.Submissions.Find(x => x.Id == id).FirstOrDefault();
            if (s == null) return NotFound();
            return View(s);
        }
    }
}
