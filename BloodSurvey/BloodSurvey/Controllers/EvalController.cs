using BloodSurvey.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace BloodSurvey.Controllers
{
    public class EvalController : ApiController
    {
        public async Task<HttpResponseMessage> Post([FromBody]string json)
        {
            var message = new HttpResponseMessage(HttpStatusCode.OK);
            message.Content = new StringContent("success");

            try
            {
                var data = JsonConvert.DeserializeObject<Survey>(json);

                using (var client = new HttpClient())
                {
                    var scoreRequest = new
                    {

                        Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = new string[] {"Q1", "Q2", "Q3", "Q4", "Q5", "Q6", "Q7", "Q8", "Q9", "Q10", "Q11", "Q12", "Blood"},
                                Values = new string[,] {  { data.Q1, data.Q2, data.Q3, data.Q4, data.Q5, data.Q6, data.Q7, data.Q8, data.Q9, data.Q10, data.Q11, data.Q12, data.Blood} }
                            }
                        },
                    },
                        GlobalParameters = new Dictionary<string, string>()
                        {
                        }
                    };
                    const string apiKey = "mmsjHe+iy7W+gvu/fCTLhOjyPmkW5n/MjZ1Qd3ai/9lqOLrsU6N+DmcJoWaFAIPD05FUzFf/Nnwr/Y5/xx4SGw==";
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                    client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/ca83842440014061acfcf75a73fe6726/services/e29b5b1336bf4cb88e3199e5a38d90ea/execute?api-version=2.0&details=true");


                    HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                    string result = await response.Content.ReadAsStringAsync();
                    var output = JsonConvert.DeserializeObject<MlOut>(result);
                    var score = output.Results.output1.value.Values.Last();
                    message.Content = new StringContent(result);
                    return message;
                }

            }catch (Exception e)
            {
                message.StatusCode = HttpStatusCode.InternalServerError;
                message.Content = new StringContent(e.Message);
            }
            return message;
        }
    }
}
