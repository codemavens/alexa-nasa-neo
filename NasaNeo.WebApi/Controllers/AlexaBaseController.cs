using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Alexa.NET.Request;
using Microsoft.ApplicationInsights.DataContracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace NasaNeo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/AlexaBase")]
    public class AlexaBaseController : Controller
    {
        protected async Task<SkillRequest> GetSkillRequestFromPost()
        {
            var request = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                request = await reader.ReadToEndAsync();
            }

            return PopulateSkillRequest(request);
        }

        protected SkillRequest PopulateSkillRequest(string requestBodyJson)
        {
            dynamic rawJson = JsonConvert.DeserializeObject(requestBodyJson);
            SkillRequest result = (SkillRequest)JsonConvert.DeserializeObject(requestBodyJson, typeof(SkillRequest));

            return result;
        }

        protected void LogMessage(string message, SeverityLevel severityLevel, Dictionary<string, string> details)
        {
            var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
            telemetry.TrackTrace(message,
                           severityLevel,
                           details);
        }

    }
}