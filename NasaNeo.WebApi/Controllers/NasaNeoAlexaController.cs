using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NasaNeo.Business.ControllerServices;
using NasaNeo.WebApi.Controllers;
using Alexa.NET.Response;
using Microsoft.ApplicationInsights.DataContracts;
using Alexa.NET.Request.Type;
using Alexa.NET;
using NasaNeo.Business.Models;
using NasaNeo.Business;
using NasaNeo.Business.NasaApi;
using Alexa.NET.Request;

namespace NasaNeo.WebApi.Controllers
{
    [Route("api/alexa/duck-and-cover")]
    public class NasaNeoAlexaController : AlexaBaseController
    {
        private IConfiguration _configuration;
        private NasaNeoAlexaControllerService _controllerService;
        private INasaNeoRepo _repo;
        private Utils _util;

        public NasaNeoAlexaController(IConfiguration configuration, IServiceProvider serviceProvider, INasaNeoRepo repo)
        {
            _configuration = configuration;
            _controllerService = new NasaNeoAlexaControllerService(serviceProvider, repo);
            _repo = repo;
            _util = new Utils();
        }

        [HttpGet]
        [Route("verify")]
        public JsonResult Verify()
        {
            //var result = _alexaControllerService.GetVerifyViewModel();
            return Json("The service is running");
        }

        [HttpPost]
        [Route("alexa-home")]
        public async Task<ActionResult> AlexaHome()
        {
            try
            {
                var skillRequest = await GetSkillRequestFromPost();
                if (skillRequest == null || skillRequest.Request == null)
                {
                    LogMessage("input request is null", SeverityLevel.Error, null);
                }

                //var validationResult = await CheckBadRequest(skillRequest);
                //if( validationResult != null )
                //{
                //    return validationResult;
                //}


                // check what type of a request it is like an IntentRequest or a LaunchRequest
                var requestType = skillRequest.GetRequestType();
                //LogMessage($"requestType == {requestType}", SeverityLevel.Information, null);

                if (requestType == typeof(IntentRequest))
                {
                    // do some intent-based stuff
                    var intentRequest = skillRequest.Request as IntentRequest;

                    // check the name to determine what you should do
                    if (intentRequest.Intent.Name.Equals("TodayIntent"))
                    {
                        // get the slots
                        //var firstValue = intentRequest.Intent.Slots["FirstSlot"].Value;

                        return Json(await _controllerService.GetNeoForDate(DateTime.Today));
                    }
                    else if(intentRequest.Intent.Name.Equals(Alexa.NET.Request.Type.BuiltInIntent.Cancel) ||
                            intentRequest.Intent.Name.Equals(Alexa.NET.Request.Type.BuiltInIntent.Stop))
                    {
                        return Json(Exit());
                    }
                    else if (intentRequest.Intent.Name.Equals(Alexa.NET.Request.Type.BuiltInIntent.Help))
                    {
                        return Json(Help());
                    }
                    else if (intentRequest.Intent.Name.Equals(Alexa.NET.Request.Type.BuiltInIntent.Fallback))
                    {
                        return Json(FallbackHelp());
                    }
                    else
                    {
                        return Json(ErrorResponse());
                    }
                }
                else if (requestType == typeof(Alexa.NET.Request.Type.LaunchRequest))
                {
                    return Json(Usage());
                }
                else
                {
                    return Json(ErrorResponse());
                }

            }
            catch (Exception exc)
            {
                return Json(ErrorResponse(exc));
            }
        }

        private SkillResponse ErrorResponse(Exception exc = null)
        {
            if (exc != null)
            {
                LogMessage(exc.Message, SeverityLevel.Error, new Dictionary<string, string>() { { "Stack Trace", exc.StackTrace } });
            }

            var util = new Utils();
            // build the speech response 
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>{util.GetRandomMessage(Globals.IDidntUnderstand, false)}. Try saying 'What are today's threats' to list today's threats.</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = "Try saying 'What are today's threats' to list today's threats.";

            // create the reprompt
            var reprompt = new Alexa.NET.Response.Reprompt();
            reprompt.OutputSpeech = repromptMessage;

            // create the response using the ResponseBuilder
            var finalResponse = ResponseBuilder.Ask(speech, reprompt);
            return finalResponse;
        }

        private SkillResponse Usage()
        {
            var commonUsage = "You can say 'What are today's threats?' to list today's space-based threats to earth, or say Help to get more information.";

            // create the speech response - cards still need a voice response
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>Welcome to {Globals.FriendlyAppTitle}. {commonUsage}</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = commonUsage;

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }

        private SkillResponse Help()
        {
            // create the speech response - cards still need a voice response
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>Do you worry about space-based threats to earth such as asteroids and other near earth objects? {Globals.FriendlyAppTitle} will let you know what's out there zooming towards us.</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = "Try saying 'What are today's threats?'";

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }

        private SkillResponse FallbackHelp()
        {
            // create the speech response - cards still need a voice response
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>{_util.GetRandomMessage(Globals.IDidntUnderstand)}. You might try 'What are today's threats?'</speak>";

            // create the speech reprompt
            var repromptMessage = new Alexa.NET.Response.PlainTextOutputSpeech();
            repromptMessage.Text = "Try saying 'What are today's threats?'";

            // create the reprompt
            var repromptBody = new Alexa.NET.Response.Reprompt();
            repromptBody.OutputSpeech = repromptMessage;

            var finalResponse = ResponseBuilder.Ask(speech, repromptBody);
            return finalResponse;
        }

        private SkillResponse Exit()
        {
            var util = new Utils();
            // build the speech response 
            var speech = new Alexa.NET.Response.SsmlOutputSpeech();
            speech.Ssml = $"<speak>{util.GetRandomMessage(Globals.GoodBye, false)}</speak>";

            // create the response using the ResponseBuilder
            var finalResponse = ResponseBuilder.Tell(speech);
            return finalResponse;
        }

    }
}
