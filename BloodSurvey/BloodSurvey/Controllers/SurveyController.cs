using BloodSurvey.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace BloodSurvey.Controllers
{
    
    public class SurveyController : ApiController
    {
        public MobileServiceClient client = new MobileServiceClient("http://bloodsurveydata.azurewebsites.net");
        public string Get()
        {
            return "moge";
        }
        public async Task<HttpResponseMessage> Post([FromBody]string json)
        {
            var message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent("success");

            try {
                var data = JsonConvert.DeserializeObject<Survey>(json);

                await client.GetTable<Survey>().InsertAsync(data);
            }
            catch (Exception e)
            {
                message.StatusCode = HttpStatusCode.InternalServerError;
                message.Content = new StringContent(e.Message);
            }
            return message;
        }
    }
}
