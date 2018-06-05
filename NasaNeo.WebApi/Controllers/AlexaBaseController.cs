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
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace NasaNeo.WebApi.Controllers
{
    [Produces("application/json")]
    [Route("api/AlexaBase")]
    public class AlexaBaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            base.OnActionExecuting(context);
        }

        protected async Task<SkillRequest> GetSkillRequestFromPost()
        {
            var request = string.Empty;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                request = await reader.ReadToEndAsync();
            }

            var skillRequest = PopulateSkillRequest(request);
            return skillRequest;
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

        protected async Task<ActionResult> CheckBadRequest(SkillRequest skillRequest)
        {
            try
            {
                if (!Alexa.NET.Request.RequestVerification.RequestTimestampWithinTolerance(skillRequest))
                {
                    LogMessage("Request failed due to TimestampTolerance", SeverityLevel.Error, null);
                    return BadRequest();
                }

                var sigCertChainUrl = Request.Headers["SignatureCertChainUrl"];
                if (string.IsNullOrEmpty(sigCertChainUrl))
                {
                    LogMessage("Request failed due to no SignatureCertChainUrl", SeverityLevel.Error, null);
                    return BadRequest();
                }

                var sigCertChainPath = new Uri(sigCertChainUrl);
                if (!Alexa.NET.Request.RequestVerification.VerifyCertificateUrl(sigCertChainPath))
                {
                    LogMessage("Request failed due to no invalid Certificate Url", SeverityLevel.Error, null);
                    return BadRequest();
                }

                var cert = await Alexa.NET.Request.RequestVerification.GetCertificate(sigCertChainPath);
                if (!Alexa.NET.Request.RequestVerification.VerifyChain(cert))
                {
                    LogMessage("Request failed due to no invalid Certificate Chain", SeverityLevel.Error, null);
                    return BadRequest();
                }

            }
            catch (Exception exc)
            {
                LogMessage("Request failed because of unknown error", SeverityLevel.Error, new Dictionary<string, string>() { { "StackTrace", exc.StackTrace } });
                return BadRequest();
            }

            return null;
        }

    }
}