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

        protected void LogMessage(string message, SeverityLevel severityLevel, string @class, string method)
        {
            LogMessage(message, severityLevel, new Dictionary<string, string>() { { "Class", @class }, { "Method", method } });
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
                if (!RequestVerification.RequestTimestampWithinTolerance(skillRequest))
                {
                    LogMessage("Request failed due to TimestampTolerance", SeverityLevel.Error, null);
                    return BadRequest();
                }

                var sigCertChainUrl = Request.Headers["SignatureCertChainUrl"];
                if (String.IsNullOrWhiteSpace(sigCertChainUrl))
                {
                    LogMessage("Request failed due to no SignatureCertChainUrl", SeverityLevel.Error, null);
                    return BadRequest();
                }

                var signature = Request.Headers["Signature"];
                if (String.IsNullOrWhiteSpace(signature))
                {
                    LogMessage("Request failed due to no Signature", SeverityLevel.Error, null);
                    return BadRequest();
                }

                var sigCertChainPath = new Uri(sigCertChainUrl);
                if (!RequestVerification.VerifyCertificateUrl(sigCertChainPath))
                {
                    LogMessage($"Request failed due to an invalid Certificate Url ({sigCertChainPath})", SeverityLevel.Error, null);
                    return BadRequest();
                }

                //var cert = await RequestVerification.GetCertificate(sigCertChainPath);
                //if (!RequestVerification.VerifyChain(cert))
                //{
                //    LogMessage("Request failed due to invalid Certificate Chain", SeverityLevel.Error, null);
                //    return BadRequest();
                //}

                var rdr = new StreamReader(Request.Body);
                if (!await RequestVerification.Verify(signature, sigCertChainPath, await rdr.ReadToEndAsync()))
                {
                    LogMessage($"Request verification failed! ({signature}, {sigCertChainPath}, {await GetRequestDetails()})", SeverityLevel.Error, null);
                    return BadRequest();
                }
            }
            catch (Exception exc)
            {
                LogMessage($"Request failed because of unknown error. Request: {await GetRequestDetails()}", SeverityLevel.Error, new Dictionary<string, string>() { { "StackTrace", exc.StackTrace } });
                return BadRequest();
            }

            //Success, let's log the request info temporarily
            LogMessage($"Successful request: {await GetRequestDetails()}", SeverityLevel.Verbose, "AlexaBaseController", "CheckBadRequest");
            return null;
        }

        protected async Task<string> GetRequestDetails()
        {
            var sb = new StringBuilder();

            foreach (var hdr in Request.Headers)
            {
                sb.Append($"{hdr.Key}: {hdr.Value}");
            }

            var rdr = new StreamReader(Request.Body);
            sb.Append(await rdr.ReadToEndAsync());

            return sb.ToString();
        }

    }
}