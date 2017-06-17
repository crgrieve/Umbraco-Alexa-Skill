
using System.Linq;
using System.Web;
using Umbraco.Web;
using Umbraco.Web.WebApi;
using System.Net.Http;
using IotProject.Models;
using Newtonsoft.Json;

namespace IotProject.Controllers
{
    public class AlexaController : UmbracoApiController
    {


        public HttpResponseMessage GetWelcomeMessage(string appId)
        {
            AlexaResponseModel response = new AlexaResponseModel();
            var alexaDevices = UmbracoContext.ContentCache.GetAtRoot().FirstOrDefault().Children.Where(x => x.DocumentTypeAlias == "alexaDevice");
            foreach (var alexa in alexaDevices)
            {
                if (alexa.GetPropertyValue<string>("uniqueId") == appId)
                {
                    response.Message = alexa.GetPropertyValue<string>("welcomeMessage");
                }
            }

            string output = JsonConvert.SerializeObject(response);
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Write(output);

            return new HttpResponseMessage();
        }

        public HttpResponseMessage GetUnknownMessage(string appId)
        {
            AlexaResponseModel response = new AlexaResponseModel();
            var alexaDevices = UmbracoContext.ContentCache.GetAtRoot().FirstOrDefault().Children.Where(x => x.DocumentTypeAlias == "alexaDevice");
            foreach (var alexa in alexaDevices)
            {
                if (alexa.GetPropertyValue<string>("uniqueId") == appId)
                {
                    response.Message = alexa.GetPropertyValue<string>("unknownRequestMessage");
                }
            }
            string output = JsonConvert.SerializeObject(response);
            HttpContext.Current.Response.ContentType = "application/json";
            HttpContext.Current.Response.Write(output);

            return new HttpResponseMessage();
        }
    }
}
