using ecu_onatrix.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using Umbraco.Cms.Core.Cache;
using Umbraco.Cms.Core.Logging;
using Umbraco.Cms.Core.Routing;
using Umbraco.Cms.Core.Services;
using Umbraco.Cms.Core.Web;
using Umbraco.Cms.Infrastructure.Persistence;
using Umbraco.Cms.Web.Website.Controllers;

namespace ecu_onatrix.Controllers
{
    public class ContactSurfaceController(IUmbracoContextAccessor umbracoContextAccessor, IUmbracoDatabaseFactory databaseFactory, ServiceContext services, AppCaches appCaches, IProfilingLogger profilingLogger, IPublishedUrlProvider publishedUrlProvider, HttpClient httpClient) : SurfaceController(umbracoContextAccessor, databaseFactory, services, appCaches, profilingLogger, publishedUrlProvider)
    {
        private readonly HttpClient _httpClient = httpClient;

        [HttpPost]
        public async Task<IActionResult> HandleSubmit(ContactRequestModel contactRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = contactRequest.Name;
                ViewData["email"] = contactRequest.Email;
                ViewData["phone"] = contactRequest.Phone;
                ViewData["category"] = contactRequest.Category;

                ViewData["error_name"] = string.IsNullOrEmpty(contactRequest.Name);
                ViewData["error_email"] = string.IsNullOrEmpty(contactRequest.Email);
                ViewData["error_phone"] = string.IsNullOrEmpty(contactRequest.Phone);

                return CurrentUmbracoPage();
            }

            var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://jb-onatrix-contactprovider.azurewebsites.net/api/CreateContactRequest?code=qHTqm6obHf3IzdHKj1xKHN2KJfYnNdFJKDGVU-Qszw2sAzFuMVXQ3Q%3D%3D", content);
            if (response.IsSuccessStatusCode)
                TempData["status_message"] = "You are now subscribed!";
            else
                TempData["status_message"] = "Error - Something went wrong while attempting to subscribe!";

            return RedirectToCurrentUmbracoPage();
        }

        [HttpPost]
        public JsonResult HandleJsonSubmit([FromBody] ContactRequestModel model)
        {

            return Json(new { success = true, message = "Data received", receivedData = model });

        }
    }
}
