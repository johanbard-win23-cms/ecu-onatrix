using ecu_onatrix.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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

        /// <summary>
        /// NOT IN USE
        /// Takes json object from umbracoPage and sends object to contactProvider
        /// </summary>
        /// <param name="contactRequest"></param>
        /// <returns>
        /// Page reload
        /// ** Stores information in TempData["success"](bool) and TempData["status_message"](string)
        /// </returns>
        [HttpPost]
        public async Task<IActionResult> HandleSubmit(ContactRequestModel contactRequest)
        {
            if (!ModelState.IsValid)
            {
                ViewData["name"] = contactRequest.Name;
                ViewData["email"] = contactRequest.Email;
                ViewData["phone"] = contactRequest.Phone;
                ViewData["category"] = contactRequest.Category;
                ViewData["category"] = contactRequest.Question;

                ViewData["error_name"] = string.IsNullOrEmpty(contactRequest.Name);
                ViewData["error_email"] = string.IsNullOrEmpty(contactRequest.Email);
                ViewData["error_phone"] = string.IsNullOrEmpty(contactRequest.Phone);
                ViewData["error_category"] = string.IsNullOrEmpty(contactRequest.Category);

                return CurrentUmbracoPage();
            }

            var content = new StringContent(JsonConvert.SerializeObject(contactRequest), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://jb-onatrix-contactprovider.azurewebsites.net/api/CreateContactRequest?code=qHTqm6obHf3IzdHKj1xKHN2KJfYnNdFJKDGVU-Qszw2sAzFuMVXQ3Q%3D%3D", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = true;
                TempData["status_message"] = "\"Thank you! We will contact you soon.";
            }
                
            else
            {
                TempData["success"] = false;
                TempData["status_message"] = "Error - Something went wrong while attempting to contact us!";
            }
                

            return RedirectToCurrentUmbracoPage();
        }

        /// <summary>
        /// Takes json object from js and sends object to contactProvider 
        /// </summary>
        /// <param name="model"></param>
        /// <returns>
        /// JsonResult {success(bool), message(string), recieved data(ContactRequestModel)}
        /// ** Stores information in TempData["success"](bool) and TempData["status_message"](string)
        /// </returns>
        [HttpPost]
        public async Task<JsonResult>  HandleJsonSubmit([FromBody] ContactRequestModel model)
        {
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("https://jb-onatrix-contactprovider.azurewebsites.net/api/CreateContactRequest?code=qHTqm6obHf3IzdHKj1xKHN2KJfYnNdFJKDGVU-Qszw2sAzFuMVXQ3Q%3D%3D", content);
            if (response.IsSuccessStatusCode)
            {
                TempData["success"] = true;
                TempData["status_message"] = "Thank you! We will contact you soon.";
                return Json(new { success = true, message = "Data received and registered", receivedData = model });
            }

            TempData["success"] = false;
            TempData["status_message"] = "Error - Something went wrong while attempting to contact us!";
            return Json(new { success = false, message = "Data received but registration failed", receivedData = model });

        }
    }
}
